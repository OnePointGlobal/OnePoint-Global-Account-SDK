using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Sample
{
    public class SampleRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public SampleRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public SampleRootObject GetSurveySampleDetails(long surveyId)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/Get?id=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());

        }

        public SampleRootObject AttachSurveyPanels(long surveyId, List<long> panelIds, long sampleId = 0)
        {
            if (surveyId < 1 || panelIds.Count < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SelectedPanels = String.Join(",", panelIds), SampleID = sampleId, SurveyID = surveyId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/ImportPanels", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());
        }

        public SampleRootObject DeleteSurveyPanels(long surveyId, long panelId)
        {
            if (surveyId < 1 || panelId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, PanelID = panelId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/DeleteSurveyPanels", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());
        }

        public SampleRootObject AddSampleFilters(long surveyId, long sampleId, SampleOperation operation, SampleOperator Operator, int variableId, string value)
        {
            //Problem in adding filter.Need to resolve.
            if (surveyId < 1 || sampleId < 1 || variableId < 1 || string.IsNullOrEmpty(value))
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            var fields = JsonConvert.SerializeObject(new { AndOr = operation.ToString(), FieldName = "FirstName", variableID = variableId, Operator = Operator, Value = value });

            var requestArg = JsonConvert.SerializeObject(new { SampleID = sampleId, SurveyID = surveyId, Fields = "[" + fields + "]" });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/AddFilter", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());
        }

        public SampleRootObject ApplySampleFilter(long surveyId, long sampleId)
        {
            if (surveyId < 1 || sampleId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, SampleID = sampleId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/ApplyFilter", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());
        }

        public SampleRootObject DeleteSampleFilter(long SampleQueryElementID)
        {
            if (SampleQueryElementID < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/DeleteFilter?SampleQueryElementID=" + SampleQueryElementID, HttpMethod.Delete, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());

        }

        public SampleRootObject GetSurveyPanels(long surveyId)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/GetSurveyPanels?surveyId=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject(), "Panels");
        }

        public SampleRootObject GetSampleFilters(long sampleId)
        {
            if (sampleId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/GetFilter?sampleID=" + sampleId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject(), "Filters");

        }

        public SampleRootObject GetSamplePanellist(long surveyId)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/GetSamplePanellists?SurveyID=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject(), "Filters");

        }

        public SampleRootObject BlockSamplePanellist(long surveyId, long sampleId, List<long> samplePanellistIds)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }


            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, SampleID = sampleId, selectedPanelMembers = string.Join(",", samplePanellistIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/BlockPanelMembers", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());

        }


        public SampleRootObject UnBlockSamplePanellist(long surveyId, long sampleId, List<long> samplePanellistIds)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }


            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, SampleID = sampleId, selectedPanelMembers = string.Join(",", samplePanellistIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/UnBlockPanelMembers", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject());

        }

        public SampleRootObject GetPanellistByJobType(long surveyId, SurveyNotificationJobType type, long sampleId = 0)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SampleRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSample/GetPanellistPageCustom?SurveyID=" + surveyId + "&ActivityId=" + (int)type + "&SampleID=" + sampleId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SampleRootObject(), "Panellists");
        }
    }
}
