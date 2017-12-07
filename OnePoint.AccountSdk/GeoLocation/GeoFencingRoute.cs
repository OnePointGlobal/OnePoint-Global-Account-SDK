using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.GeoLocation
{
    public class GeoFencingRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public GeoFencingRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public GeoFencingRoot GetSurveyGeoFencing(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserGeofencing/GetImportedGeofencingList?SurveyID=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }

        public void AddSurveyGeoFencing()
        {
            //Undeer developement.

        }

        public void DeleteSurveyGeoFencing()
        {
            //Under developement.

        }
    }
}
