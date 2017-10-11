using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Common
{
    public class CommonRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public CommonRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }


        public string GetTinyUrl(string longUrl)
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTinyUrl/GetTinyUrl?longurl=" + longUrl, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }

        public string GetQRCode(string url, int dimention = 450)
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQRCode/GetQRCode?url=" + url + "&dimension=" + dimention, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }
    }
}
