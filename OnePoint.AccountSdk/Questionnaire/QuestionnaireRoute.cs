using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace OnePoint.AccountSdk.Questionnaire
{
    public class QuestionnaireRoute
    {

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public QuestionnaireRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public RootObject GetUserQuestionnaires()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/GetQuestionnaire", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public RootObject DeleteQuestionnaires(List<long> scriptIds)
        {
            if (scriptIds.Count < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { deleteScriptIDs = String.Join(",", scriptIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DeleteQuestionnaire", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public RootObject DuplicateQuestionnaires(long scriptId)
        {
            if (scriptId < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/CopyQuestionnaire?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public RootObject DetachSurveyQuestionnaires(long surveyId)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DetachQuestionnaire?surveyID=" + surveyId, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public RootObjectScript GetQuestionnaire(long scriptId)
        {
            if (scriptId < 1)
            {
                return result.ErrorToObject(new RootObjectScript(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/GetQuestionnaireDetails?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObjectScript());
        }

        public RootObjectScript UpdateQuestionnaire(long scriptId, string name, string description, string scriptPath = null)
        {
            if (scriptId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return result.ErrorToObject(new RootObjectScript(), "Invalid parameter(s)");
            }

            Task<Result> x;
            if (scriptPath == null)
            {
                x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Rpc, null);
            }
            else
            {
                x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Upload, scriptPath);
            }

            x.Wait();
            return x.Result.JsonToObject(new RootObjectScript());
        }

        public RootObject AttachSurveyQuestionnaire(long surveyId, long scriptId)
        {
            if (surveyId < 1 || scriptId < 0)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { surveyId = surveyId, scriptId = scriptId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/AttachQuestionnaire", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public RootObject AddQuestionnaire(string name, string description, string scriptPath, long surveyId = 0)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !File.Exists(scriptPath))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/AddQuestionnaire?Name=" + name + "&Description=" + description + "&surveyId=" + surveyId, HttpMethod.Post, RouteStyle.Upload, scriptPath);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Scripts");
        }

        public byte[] DownloadScript(long scriptId, string folderPath)
        {
            if (scriptId <= 0 || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DownloadScript?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Download, null);
            x.Wait();

            x.Result.DownloadFile(folderPath + x.Result.DonwloadFileName);
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }
    }
}
