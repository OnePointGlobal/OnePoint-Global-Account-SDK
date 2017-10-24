using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Common
{
    public class CommonRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get; }


        public CommonRoute(AdminRequestHandler hanlder)
        {
            this.RequestHandler = hanlder;
        }

        public string GetTinyUrl(string longUrl)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTinyUrl/GetTinyUrl?longurl=" + longUrl, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }

        public string GetQRCode(string url, int dimention = 450)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQRCode/GetQRCode?url=" + url + "&dimension=" + dimention, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }

        public CountryRootObject GetCountries()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetAllCountries", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new CountryRootObject(), "Country");
        }
    }
}
