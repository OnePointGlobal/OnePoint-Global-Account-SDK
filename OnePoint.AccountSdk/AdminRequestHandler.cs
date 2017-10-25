using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public HttpClient HttpClient { get; }

        public string Hostname { get; }

        public string SessionId { get; set; }

        public IDictionary<string, string> HostMap { get; }

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
            HostMap = new Dictionary<string, string>
            {
                { HostType.ApiLive, LiveHost },
                { HostType.ApiDev, DefaultApiDevelopementDomain },
                { HostType.ApiStaging, DefaultApiStagingDomain },
                { HostType.ApiLocal, DefaultApiLocaDomain }
            };
        }

        public AdminRequestHandler(HttpClient client, string hostType)
            : this()
        {
            HttpClient = client;
            Hostname = HostMap[hostType.ToLower()];
        }

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
                    if ((method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete) && requestArg != null)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Download:
                    if (method == HttpMethod.Post)
                        request.Content = new StringContent(requestArg, Encoding.UTF8, "application/json");
                    break;
                case RouteStyle.Upload:
                    MultipartFormDataContent multiPartContent = new MultipartFormDataContent("----MyGreatBoundary");
                    char[] charSeparators = new char[] { ';' };
                    var files = requestArg.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var file in files)
                    {
                        var byteArrayContent = CreateFileContent(file, out string filename);
                        multiPartContent.Add(byteArrayContent, filename, filename);
                    }
                    request.Content = multiPartContent;
                    break;
                default:
                    throw new InvalidOperationException(string.Format(
                        CultureInfo.InvariantCulture,
                        "Unknown route style: {0}",
                        routeStyle));
            }

            var disposeResponse = true;
            var response = await HttpClient
                .SendAsync(request).ConfigureAwait(false);

            try
            {
                if ((int)response.StatusCode >= 500)
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    reason = CheckForError(reason);
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
                    reason = CheckForError(reason);
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
                    reason = CheckForError(reason);
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
                    reason = CheckForError(reason);
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
                    reason = CheckForError(reason);
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

        private Uri GetRouteUri(string hostname, string routeName)
        {
            var builder = new UriBuilder("https", hostname)
            {
                Path = "/" + routeName
            };
            return builder.Uri;
        }

        private ByteArrayContent CreateFileContent(string file, out string filename)
        {
            FileInfo fi = new FileInfo(file);
            filename = fi.Name;
            byte[] fileContents = File.ReadAllBytes(fi.FullName);

            ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
            byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");

            return byteArrayContent;
        }

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
