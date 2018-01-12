// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoFencingRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The geo fencing route.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OnePoint.AccountSdk.GeoLocation
{
    /// <summary>
    /// The geo fencing route class, provides the code for CRUD operation on survey geo fencing data.
    /// </summary>
    public class GeoFencingRoute
    {
        /// <summary>
        /// Gets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// The _result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoFencingRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public GeoFencingRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get survey geo fencing details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="GeoFencingRoot"/>.
        /// </returns>
        public GeoFencingRoot GetSurveyGeoFencing(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeofencing/GetImportedGeofencingList?SurveyID=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }

        /// <summary>
        /// The add/set survey geo fencing.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <param name="enterEvent">
        /// The enter event.
        /// </param>
        /// <param name="existEvent">
        /// The exist event.
        /// </param>
        /// <param name="eventTimeInMinutes">
        /// The event time in minutes.
        /// </param>
        /// <param name="rangeInMeter">
        /// The range in meter.
        /// </param>
        /// <returns>
        /// The <see cref="GeoFencingRoot"/>.
        /// </returns>
        public GeoFencingRoot AddSurveyGeoFencing(
            long surveyId,
            long addressListId,
            bool enterEvent,
            bool existEvent,
            short eventTimeInMinutes = 0,
            short rangeInMeter = 100)
        {
            if (surveyId < 1 || addressListId < 1 || rangeInMeter < 0 || eventTimeInMinutes < 0)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        AddressListID = addressListId,
                        SurveyID = surveyId,
                        EnterEvent = enterEvent,
                        ExitEvent = existEvent,
                        EventTime = eventTimeInMinutes,
                        Range = rangeInMeter
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeofencing/UpdateGeofencing",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }

        /// <summary>
        /// The update survey geo fencing details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <param name="enterEvent">
        /// The enter event.
        /// </param>
        /// <param name="existEvent">
        /// The exist event.
        /// </param>
        /// <param name="eventTimeInMinutes">
        /// The event time in minutes.
        /// </param>
        /// <param name="rangeInMeter">
        /// The range in meter.
        /// </param>
        /// <returns>
        /// The <see cref="GeoFencingRoot"/>.
        /// </returns>
        public GeoFencingRoot UpdateSurveyGeoFencing(
            long surveyId,
            long addressListId,
            bool enterEvent,
            bool existEvent,
            short eventTimeInMinutes = 0,
            short rangeInMeter = 100)
        {
            if (surveyId < 1 || addressListId < 1 || rangeInMeter < 0 || eventTimeInMinutes < 0)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        AddressListID = addressListId,
                        SurveyID = surveyId,
                        EnterEvent = enterEvent,
                        ExitEvent = existEvent,
                        EventTime = eventTimeInMinutes,
                        Range = rangeInMeter
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeofencing/UpdateGeofencing",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }

        /// <summary>
        /// The delete survey geo fencing.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="GeoFencingRoot"/>.
        /// </returns>
        public GeoFencingRoot DeleteSurveyGeoFencing(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new GeoFencingRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeofencing/DeleteSurveyGeofencing",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoFencingRoot(), "GeoFencing");
        }
    }
}