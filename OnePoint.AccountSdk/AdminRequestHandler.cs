using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Configuration;


namespace OnePoint.AccountSdk
{
    public class AdminRequestHandler
    {
        public HttpClient HttpClient { get; private set; }

        public string hostname { get; private set; }

        public string SessionId { get; set; }

        public IDictionary<string, string> HostMap { get; private set; }

        private string LiveHost
        {
            get
            {
                //Get hosturl from cofig else get the default.
                var configapihost = ConfigurationManager.AppSettings["hostname"];
                return string.IsNullOrEmpty(configapihost) ? DefaultApiLiveDomain : configapihost;
            }
        }

        private const string DefaultApiLocaDomain = "";
        private const string DefaultApiDevelopementDomain = "";
        private const string DefaultApiStagingDomain = "";
        private const string DefaultApiLiveDomain = "https://api.1pt.mobi/V3.1/";


        public AdminRequestHandler()
        {
            this.HostMap = new Dictionary<string, string>
            {
                { HostType.ApiLive, this.LiveHost },
                { HostType.ApiDev, DefaultApiDevelopementDomain },
                { HostType.ApiStaging, DefaultApiStagingDomain },
                { HostType.ApiLocal, DefaultApiLocaDomain }
            };
        }

        public AdminRequestHandler(HttpClient client, string hostType)
            : this()
        {
            this.HttpClient = client;
            this.hostname = this.HostMap[hostType.ToLower()];
        }

        public async Task<Result> SendRequestAsync(
           string host,
           string routeName,
           HttpMethod method,
           RouteStyle routeStyle,
           string requestArg,
           Stream body = null)
        {
            var uri = new Uri(this.hostname + routeName);

            var request = new HttpRequestMessage(method, uri);

            if (!string.IsNullOrEmpty(this.SessionId))
            {
                request.Headers.Add("SessionID", this.SessionId);
            }

            switch (routeStyle)
            {
                case RouteStyle.Rpc:
                    if ((method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete) && requestArg != null)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Download:
                    if (method == HttpMethod.Post)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Upload:
                    FileInfo fi = new FileInfo(requestArg);
                    string fileName = fi.Name;
                    byte[] fileContents = File.ReadAllBytes(fi.FullName);

                    request.Headers.ExpectContinue = false;
                    MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyGreatBoundary");
                    ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
                    byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
                    multiPartContent.Add(byteArrayContent, fileName, fileName);
                    request.Content = multiPartContent;

                    break;
                default:
                    throw new InvalidOperationException(string.Format(
                        CultureInfo.InvariantCulture,
                        "Unknown route style: {0}",
                        routeStyle));
            }

            var disposeResponse = true;
            var response = await this.HttpClient
                .SendAsync(request).ConfigureAwait(false);

            try
            {
                if ((int)response.StatusCode >= 500)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = this.CheckForError(reason);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = reason,
                        HttpResponse = response
                    };
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = this.CheckForError(reason);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = reason,
                        HttpResponse = response
                    };
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = this.CheckForError(reason);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = reason,
                        HttpResponse = response
                    };
                }
                else if ((int)response.StatusCode == 429)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = this.CheckForError(reason);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = reason,
                        HttpResponse = response
                    };
                }
                else if (response.StatusCode == HttpStatusCode.Conflict ||
                    response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.NotFound)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = this.CheckForError(reason);
                    return new Result
                    {
                        IsError = true,
                        ObjectResult = reason,
                        HttpResponse = response
                    };
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
                    text = this.CheckForError(text);
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

        private Uri GetRouteUri(string hostname, string routeName)
        {
            var builder = new UriBuilder("https", hostname);
            builder.Path = "/" + routeName;
            return builder.Uri;
        }

        private string CheckForError(string text)
        {
            try
            {
                var obj = JObject.Parse(text);
                JToken error;
                if (obj.TryGetValue("error", out error))
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

    internal class HostType
    {
        /// <summary>
        /// Host type for live api.
        /// </summary>
        public const string ApiLive = "live";

        /// <summary>
        /// Host type for developemt api.
        /// </summary>
        public const string ApiDev = "dev";

        /// <summary>
        /// Host type for staging api.
        /// </summary>
        public const string ApiStaging = "staging";

        /// <summary>
        /// Host type for local api.
        /// </summary>
        public const string ApiLocal = "local";
    }
}
