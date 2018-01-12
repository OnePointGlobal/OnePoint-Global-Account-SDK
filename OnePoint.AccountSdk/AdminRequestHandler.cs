// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdminRequestHandler.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The admin request handler class, manages API requesting and reponses analysis.
//   This class has the code to Api request call and respionse data operation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace OnePoint.AccountSdk
{
    /// <summary>
    /// The admin request handler class, provides the code to setup api host, request api and insight response data.
    /// </summary>
    public class AdminRequestHandler
    {
        /// <summary>
        /// Gets the http client.
        /// </summary>
        public HttpClient HttpClient { get; }

        /// <summary>
        /// Gets the hostname.
        /// </summary>
        public string Hostname { get; }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Gets the host map.
        /// </summary>
        public IDictionary<string, string> HostMap { get; }

        /// <summary>
        /// Gets the live host.Overrides dafault live host, if config key="hostname" is set.
        /// </summary>
        private string LiveHost
        {
            get
            {
                // Get hosturl from cofig else get the default.
                var configapihost = ConfigurationManager.AppSettings["hostname"];
                return string.IsNullOrEmpty(configapihost) ? DefaultApiLiveDomain : configapihost;
            }
        }

        /// <summary>
        /// The default api local domain.
        /// </summary>
        private const string DefaultApiLocalDomain = "";

        /// <summary>
        /// The default api developement domain.
        /// </summary>
        private const string DefaultApiDevelopementDomain = "";

        /// <summary>
        /// The default api staging domain.
        /// </summary>
        private const string DefaultApiStagingDomain = "";

        /// <summary>
        /// The default api live domain.
        /// </summary>
        private const string DefaultApiLiveDomain = "https://api.1pt.mobi/V3.1/";

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRequestHandler"/> class.
        /// </summary>
        public AdminRequestHandler()
        {
            HostMap = new Dictionary<string, string>
                          {
                              { HostType.ApiLive, LiveHost },
                              { HostType.ApiDev, DefaultApiDevelopementDomain },
                              { HostType.ApiStaging, DefaultApiStagingDomain },
                              { HostType.ApiLocal, DefaultApiLocalDomain }
                          };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRequestHandler"/> class.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="hostType">
        /// The host type.
        /// </param>
        public AdminRequestHandler(HttpClient client, string hostType)
            : this()
        {
            HttpClient = client;
            Hostname = HostMap[hostType.ToLower()];
        }

        /// <summary>
        /// The send api request async for calling route method.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="routeName">
        /// The route name.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="routeStyle">
        /// The route style.
        /// </param>
        /// <param name="requestArg">
        /// The request arg.
        /// </param>
        /// <param name="body">
        /// The body.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public async Task<Result> SendRequestAsync(
            string host,
            string routeName,
            HttpMethod method,
            RouteStyle routeStyle,
            string requestArg,
            Stream body = null)
        {
            var uri = new Uri(Hostname + routeName);

            var request = new HttpRequestMessage(method, uri);

            if (!string.IsNullOrEmpty(SessionId))
            {
                request.Headers.Add("SessionID", SessionId);
            }

            switch (routeStyle)
            {
                case RouteStyle.Rpc:
                    if ((method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
                        && requestArg != null)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Download:
                    if (method == HttpMethod.Post)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Upload:
                    MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyGreatBoundary");
                    char[] charSeparators = new[] { ';' };
                    var files = requestArg.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var file in files)
                    {
                        var byteArrayContent = CreateFileContent(file, out string filename);
                        multiPartContent.Add(byteArrayContent, filename, filename);
                    }

                    request.Content = multiPartContent;
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format(CultureInfo.InvariantCulture, "Unknown route style: {0}", routeStyle));
            }

            var disposeResponse = true;
            var response = await HttpClient.SendAsync(request).ConfigureAwait(false);

            try
            {
                if ((int)response.StatusCode >= 500)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
                    return new Result { IsError = true, ObjectResult = reason, HttpResponse = response };
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
                    return new Result { IsError = true, ObjectResult = reason, HttpResponse = response };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
                    return new Result { IsError = true, ObjectResult = reason, HttpResponse = response };
                }
                else if ((int)response.StatusCode == 429)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
                    return new Result { IsError = true, ObjectResult = reason, HttpResponse = response };
                }
                else if (response.StatusCode == HttpStatusCode.Conflict
                         || response.StatusCode == HttpStatusCode.Forbidden
                         || response.StatusCode == HttpStatusCode.NotFound)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
                    return new Result { IsError = true, ObjectResult = reason, HttpResponse = response };
                }
                else if ((int)response.StatusCode >= 200 && (int)response.StatusCode <= 299)
                {
                    if (routeStyle == RouteStyle.Download)
                    {
                        disposeResponse = false;
                        return new Result
                        {
                            IsError = false,
                            ObjectResult = await response.Content.ReadAsStringAsync(),
                            HttpResponse = response
                        };
                    }
                    else
                    {
                        return new Result
                        {
                            IsError = false,
                            ObjectResult = await response.Content.ReadAsStringAsync(),
                            HttpResponse = response
                        };
                    }
                }
                else
                {
                    var text = await response.Content.ReadAsStringAsync();
                    CheckForError(text);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = await response.Content.ReadAsStringAsync(),
                        HttpResponse = response
                    };
                }
            }
            finally
            {
                if (disposeResponse)
                {
                    response.Dispose();
                }
            }
        }

        /// <summary>
        /// The get route uri.
        /// </summary>
        /// <param name="hostname">
        /// The hostname.
        /// </param>
        /// <param name="routeName">
        /// The route name.
        /// </param>
        /// <returns>
        /// The <see cref="Uri"/>.
        /// </returns>
        private Uri GetRouteUri(string hostname, string routeName)
        {
            var builder = new UriBuilder("https", hostname) { Path = "/" + routeName };
            return builder.Uri;
        }

        /// <summary>
        /// The create file ByteArray content.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="filename">
        /// The filename.
        /// </param>
        /// <returns>
        /// The <see cref="ByteArrayContent"/>.
        /// </returns>
        private ByteArrayContent CreateFileContent(string file, out string filename)
        {
            FileInfo fi = new FileInfo(file);
            filename = fi.Name;
            byte[] fileContents = File.ReadAllBytes(fi.FullName);

            ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
            byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");

            return byteArrayContent;
        }

        /// <summary>
        /// The check for error.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CheckForError(string text)
        {
            try
            {
                var obj = JObject.Parse(text);
                if (obj.TryGetValue("error", out JToken error))
                {
                    return error.ToString();
                }

                return text;
            }
            catch (Exception)
            {
                return text;
            }
        }
    }

    /// <summary>
    /// The host type.
    /// </summary>
    internal class HostType
    {
        /// <summary>
        ///     Host type for live api.
        /// </summary>
        public const string ApiLive = "live";

        /// <summary>
        ///     Host type for developemt api.
        /// </summary>
        public const string ApiDev = "dev";

        /// <summary>
        ///     Host type for staging api.
        /// </summary>
        public const string ApiStaging = "staging";

        /// <summary>
        ///     Host type for local api.
        /// </summary>
        public const string ApiLocal = "local";
    }
}