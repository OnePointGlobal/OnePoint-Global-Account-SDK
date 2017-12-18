// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SampleRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//    The sample, manages the survy sample data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Sample
{
    /// <summary>
    /// The sample route class, provides the code for CRUD operation on sample data.
    /// </summary>
    public class SampleRoute
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
        /// Initializes a new instance of the <see cref="SampleRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public SampleRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get survey sample details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GetSurveySampleDetails(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/Get?id=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The attach/import survey panels for sample generation.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="panelIds">
        /// The panel ids.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot AttachSurveyPanels(long surveyId, List<long> panelIds, long sampleId = 0)
        {
            if (surveyId < 1 || panelIds.Count < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { SelectedPanels = string.Join(",", panelIds), SampleID = sampleId, SurveyID = surveyId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/ImportPanels",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The de attach survey panels.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot DeAttachSurveyPanels(long surveyId, long panelId)
        {
            if (surveyId < 1 || panelId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, PanelID = panelId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/DeleteSurveyPanels",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The add sample filters.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="Operator">
        /// The operator.
        /// </param>
        /// <param name="variableId">
        /// The variable id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot AddSampleFilters(
            long surveyId,
            long sampleId,
            SampleOperation operation,
            SampleOperator Operator,
            int variableId,
            string value)
        {
            // Problem in adding filter.Need to resolve.
            if (surveyId < 1 || sampleId < 1 || variableId < 1 || string.IsNullOrEmpty(value))
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var fields = JsonConvert.SerializeObject(
                new
                    {
                        AndOr = operation.ToString(),
                        FieldName = "FirstName",
                        variableID = variableId,
                        Operator = Operator,
                        Value = value
                    });

            var requestArg = JsonConvert.SerializeObject(
                new { SampleID = sampleId, SurveyID = surveyId, Fields = "[" + fields + "]" });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/AddFilter",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The generate sample panellist by applying filter.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GenerateSamplePanellistByFilter(long surveyId, long sampleId)
        {
            if (surveyId < 1 || sampleId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, SampleID = sampleId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/ApplyFilter",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The generate sample panellist by panel directly.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GenerateSamplePanellistByPanel(long surveyId, long panelId)
        {
            if (surveyId < 1 || panelId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/GenerateSamplePanellist?surveyId=" + surveyId + "&panelId=" + panelId,
                HttpMethod.Post,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The delete sample filter.
        /// </summary>
        /// <param name="sampleQueryElementId">
        /// The sample query element id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot DeleteSampleFilter(long sampleQueryElementId)
        {
            if (sampleQueryElementId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/DeleteFilter?SampleQueryElementID=" + sampleQueryElementId,
                HttpMethod.Delete,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The get attached survey panels.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GetSurveyPanels(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/GetSurveyPanels?surveyId=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot(), "Panels");
        }

        /// <summary>
        /// The get added sample filters.
        /// </summary>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GetSampleFilters(long sampleId)
        {
            if (sampleId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/GetFilter?sampleID=" + sampleId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot(), "Filters");
        }

        // public SampleRootObject GetSamplePanellist(long surveyId)
        // {
        // if (surveyId < 1)
        // {
        // return _result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
        // }

        // Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSample/GetSamplePanellists?SurveyID=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
        // x.Wait();

        // return x.Result.JsonToObject(new SampleRootObject(), "Filters");

        // }

        /// <summary>
        /// The block sample panellist, block panellist from survey subevents.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <param name="samplePanellistIds">
        /// The sample panellist ids.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot BlockSamplePanellist(long surveyId, long sampleId, List<long> samplePanellistIds)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        SampleID = sampleId,
                        selectedPanelMembers = string.Join(",", samplePanellistIds)
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/BlockPanelMembers",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The un block sample panellist.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <param name="samplePanellistIds">
        /// The sample panellist ids.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot UnBlockSamplePanellist(long surveyId, long sampleId, List<long> samplePanellistIds)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        SampleID = sampleId,
                        selectedPanelMembers = string.Join(",", samplePanellistIds)
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/UnBlockPanelMembers",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot());
        }

        /// <summary>
        /// The get panellist by survey notification job type.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="sampleId">
        /// The sample id.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot GetPanellistByJobType(long surveyId, SurveyNotificationJobType type, long sampleId = 0)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/GetPanellistPageCustom?SurveyID=" + surveyId + "&ActivityId=" + (int)type + "&SampleID="
                + sampleId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRoot(), "Panellists");
        }

        /// <summary>
        /// The update survey sample fitler and its value.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="sampleQueryElementId">
        /// The sample query element id.
        /// </param>
        /// <param name="variableId">
        /// The variable id.
        /// </param>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="Operator">
        /// The operator.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="SampleRoot"/>.
        /// </returns>
        public SampleRoot UpdateFitler(
            long surveyId,
            long sampleQueryElementId,
            long variableId,
            SampleOperation operation,
            SampleOperator Operator,
            string value)
        {
            if (surveyId < 1 || sampleQueryElementId < 1 || variableId < 1 || string.IsNullOrEmpty(value))
            {
                return _result.ErrorToObject(new SampleRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyId = surveyId,
                        SampleQueryElementId = sampleQueryElementId,
                        VariableId = variableId,
                        AndOr = operation.ToString(),
                        OperatorId = (short)Operator,
                        Value = value
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSample/UpdateSampleFilter",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new SampleRoot(), "Filters");
        }
    }
}