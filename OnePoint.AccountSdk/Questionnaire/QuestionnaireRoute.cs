using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Questionnaire
{
    public partial class QuestionnaireRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public QuestionnaireRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public Questionnaire GetUserQuestionnaires()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/GetQuestionnaire", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        public Questionnaire DeleteQuestionnaires(List<long> scriptIds)
        {
            if (scriptIds.Count < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { scriptIds = String.Join(",", scriptIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DeleteQuestionScripts", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        public Questionnaire DuplicateQuestionnaires(long scriptId)
        {
            if (scriptId < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/CopyQuestionnaire?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        public Questionnaire DetachSurveyQuestionnaires(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DetachQuestionnaire?surveyID=" + surveyId, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        public ScriptRoot GetQuestionnaire(long scriptId)
        {
            if (scriptId < 1)
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/GetQuestionnaireDetails?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ScriptRoot());
        }

        public ScriptRoot UpdateQuestionnaire(long scriptId, string name, string description, string scriptPath = null)
        {
            if (scriptId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)");
            }

            Task<Result> x;
            if (scriptPath == null)
            {
                x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Rpc, null);
            }
            else
            {
                x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description=" + description, HttpMethod.Put, RouteStyle.Upload, scriptPath);
            }

            x.Wait();
            return x.Result.JsonToObject(new ScriptRoot());
        }

        public Questionnaire AttachSurveyQuestionnaire(long surveyId, long scriptId)
        {
            if (surveyId < 1 || scriptId < 0)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { surveyId = surveyId, scriptId = scriptId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/AttachQuestionnaire", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        public ScriptRoot AddQuestionnaire(string name, string description, string scriptPath, long surveyId = 0)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !File.Exists(scriptPath))
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/AddQuestionnaire?Name=" + name + "&Description=" + description + "&surveyId=" + surveyId, HttpMethod.Post, RouteStyle.Upload, scriptPath);
            x.Wait();

            return x.Result.JsonToObject(new ScriptRoot());
        }

        public byte[] DownloadScript(long scriptId, string folderPath)
        {
            if (scriptId <= 0 || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserQuestionnaire/DownloadScript?scriptId=" + scriptId, HttpMethod.Get, RouteStyle.Download, null);
            x.Wait();

            x.Result.DownloadFile(folderPath + x.Result.DonwloadFileName);
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }
    }
}
