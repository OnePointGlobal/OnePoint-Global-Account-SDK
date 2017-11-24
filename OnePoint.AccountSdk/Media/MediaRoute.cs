using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Media
{
    public class MediaRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public MediaRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public MediaRoot AddMedia(string name, string description, string filePath)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return _result.ErrorToObject(new MediaRoot(), "File does not exist!");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserMedia/AddMedia?Name=" + name + "&Description=" + description, HttpMethod.Post, RouteStyle.Upload, filePath);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        public MediaRoot GetMedia(long mediaId)
        {
            if (mediaId < 1)
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserMedia/GetMediaDetails?mediaId=" + mediaId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        public MediaRoot GetAllMedia()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserMedia/GetMedia", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        public MediaRoot UpdateMedia(long mediaId, string name, string description, string filePath = "")
        {
            if (mediaId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid filepath!");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserMedia/UpdateMedia?mediaID=" + mediaId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Upload, filePath);
            x.Wait();
            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        public MediaRoot DeleteMedia(List<long> mediaId)
        {
            if (mediaId.Count < 1)
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { deleteMediaIDs = String.Join(",", mediaId) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserMedia/DeleteMedia", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }
    }
}
