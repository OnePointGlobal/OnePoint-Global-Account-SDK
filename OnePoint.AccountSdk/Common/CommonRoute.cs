// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The common route, manages qr code, tiny url and getting country details.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Common
{
    /// <summary>
    /// The common route class, provides the code to generate qr code, tiny url and getting list of countries used in other modules.
    /// </summary>
    public class CommonRoute
    {
        /// <summary>
        /// Gets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public CommonRoute(AdminRequestHandler hanlder)
        {
            this.RequestHandler = hanlder;
        }

        /// <summary>
        /// The get/generate tiny url, for original lengthy url.
        /// </summary>
        /// <param name="longUrl">
        /// The long url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetTinyUrl(string longUrl)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTinyUrl/GetTinyUrl?longurl=" + longUrl,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }

        /// <summary>
        /// The get qr code.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="dimention">
        /// The dimention.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetQRCode(string url, int dimention = 450)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQRCode/GetQRCode?url=" + url + "&dimension=" + dimention,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToKeyValue("Url");
        }

        /// <summary>
        /// The get list countries and codes used accross account.
        /// </summary>
        /// <returns>
        /// The <see cref="CountryRoot"/>.
        /// </returns>
        public CountryRoot GetCountries()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/GetAllCountries",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new CountryRoot(), "Country");
        }
    }
}