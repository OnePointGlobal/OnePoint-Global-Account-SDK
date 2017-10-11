using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Media
{
    public class MediaRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public MediaRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public RootObject AddMedia(string name, string description, string filePath)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserMedia/AddMedia?Name=" + name + "&Description=" + description, HttpMethod.Post, RouteStyle.Upload, filePath);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Media");
        }

        public RootObject GetMedia(long mediaId)
        {
            if (mediaId < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserMedia/GetMediaDetails?mediaId=" + mediaId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Media");
        }

        public RootObject GetAllMedia()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserMedia/GetMedia", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Media");
        }

        public RootObject UpdateMedia(long mediaId, string filePath, string name, string description)
        {
            if (mediaId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !File.Exists(filePath))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserMedia/UpdateMedia?mediaID=" + mediaId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Upload, filePath);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Media");
        }

        public RootObject DeleteMedia(List<long> mediaId)
        {
            if (mediaId.Count < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { deleteMediaIDs = String.Join(",", mediaId) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserMedia/DeleteMedia", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Media");
        }
    }
}
