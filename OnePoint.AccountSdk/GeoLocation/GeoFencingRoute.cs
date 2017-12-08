using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public GeoFencingRoot AddSurveyGeoFencing(long surveyId, long addressListId, bool enterEvent, bool existEvent, short eventTimeInMinutes = 0, short rangeInMeter = 100)
        {
            if (surveyId < 1 || addressListId < 1 || rangeInMeter < 0 || eventTimeInMinutes < 0)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { AddressListID = addressListId, SurveyID = surveyId, EnterEvent = enterEvent, ExitEvent = existEvent, EventTime = eventTimeInMinutes, Range = rangeInMeter });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserGeofencing/UpdateGeofencing", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }


        public GeoFencingRoot UpdateSurveyGeoFencing(long surveyId, long addressListId, bool enterEvent, bool existEvent, short eventTimeInMinutes = 0, short rangeInMeter = 100)
        {
            if (surveyId < 1 || addressListId < 1 || rangeInMeter < 0 || eventTimeInMinutes < 0)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { AddressListID = addressListId, SurveyID = surveyId, EnterEvent = enterEvent, ExitEvent = existEvent, EventTime = eventTimeInMinutes, Range = rangeInMeter });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserGeofencing/UpdateGeofencing", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }

        public void DeleteSurveyGeoFencing()
        {
            //Under developement.

        }
    }
}
