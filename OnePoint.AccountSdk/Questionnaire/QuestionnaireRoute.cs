// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestionnaireRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The questionnaire, manages survey questions/script data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Questionnaire
{
    /// <summary>
    /// The questionnaire route, provides the code for CRUD operation on questionnaire script, script content and downloading script file.
    /// </summary>
    public class QuestionnaireRoute
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
        /// Initializes a new instance of the <see cref="QuestionnaireRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public QuestionnaireRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get user list of questionnaires.
        /// </summary>
        /// <returns>
        /// The <see cref="Questionnaire"/>.
        /// </returns>
        public Questionnaire GetUserQuestionnaires()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/GetQuestionnaire",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        /// <summary>
        /// The delete questionnaires.
        /// </summary>
        /// <param name="scriptIds">
        /// The script ids.
        /// </param>
        /// <returns>
        /// The <see cref="Questionnaire"/>.
        /// </returns>
        public Questionnaire DeleteQuestionnaires(List<long> scriptIds)
        {
            if (scriptIds.Count < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { scriptIds = string.Join(",", scriptIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/DeleteQuestionScripts",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        /// <summary>
        /// The duplicate existing questionnaire.
        /// </summary>
        /// <param name="scriptId">
        /// The script id.
        /// </param>
        /// <returns>
        /// The <see cref="Questionnaire"/>.
        /// </returns>
        public Questionnaire DuplicateQuestionnaires(long scriptId)
        {
            if (scriptId < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
                
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/CopyQuestionnaire?scriptId=" + scriptId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        /// <summary>
        /// The detach survey questionnaire.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="Questionnaire"/>.
        /// </returns>
        public Questionnaire DetachSurveyQuestionnaires(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
                
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/DetachQuestionnaire?surveyID=" + surveyId,
                HttpMethod.Post,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        /// <summary>
        /// The get questionnaire details.
        /// </summary>
        /// <param name="scriptId">
        /// The script id.
        /// </param>
        /// <returns>
        /// The <see cref="ScriptRoot"/>.
        /// </returns>
        public ScriptRoot GetQuestionnaire(long scriptId)
        {
            if (scriptId < 1)
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)");
                
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/GetQuestionnaireDetails?scriptId=" + scriptId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ScriptRoot());
        }

        /// <summary>
        /// The update questionnaire with script opgs file.
        /// </summary>
        /// <param name="scriptId">
        /// The script id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="scriptPath">
        /// The opgs script file path.
        /// </param>
        /// <returns>
        /// The <see cref="ScriptRoot"/>.
        /// </returns>
        public ScriptRoot UpdateQuestionnaire(long scriptId, string name, string description, string scriptPath = null)
        {
            if (scriptId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)");
            }

            Task<Result> x;
            if (scriptPath == null)
            {
                x = RequestHandler.SendRequestAsync(
                    string.Empty,
                    "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description="
                    + description,
                    HttpMethod.Put,
                    RouteStyle.Rpc,
                    null);
            }
            else
            {
                x = RequestHandler.SendRequestAsync(
                    string.Empty,
                    "api/UserQuestionnaire/UpdateQuestionnaire?scriptId=" + scriptId + "&Name=" + name + "&Description="
                    + description,
                    HttpMethod.Put,
                    RouteStyle.Upload,
                    scriptPath);
            }

            x.Wait();
            return x.Result.JsonToObject(new ScriptRoot());
        }

        /// <summary>
        /// The attach the questionnaire to a survey.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="scriptId">
        /// The script id.
        /// </param>
        /// <returns>
        /// The <see cref="Questionnaire"/>.
        /// </returns>
        public Questionnaire AttachSurveyQuestionnaire(long surveyId, long scriptId)
        {
            if (surveyId < 1 || scriptId < 0)
            {
                return _result.ErrorToObject(new Questionnaire(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { surveyId = surveyId, scriptId = scriptId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/AttachQuestionnaire",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new Questionnaire(), "Scripts");
        }

        /// <summary>
        /// The add new questionnair with script file.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="scriptPath">
        /// The script path.
        /// </param>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="ScriptRoot"/>.
        /// </returns>
        public ScriptRoot AddQuestionnaire(string name, string description, string scriptPath, long surveyId = 0)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || !File.Exists(scriptPath))
            {
                return _result.ErrorToObject(new ScriptRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/AddQuestionnaire?Name=" + name + "&Description=" + description + "&surveyId="
                + surveyId,
                HttpMethod.Post,
                RouteStyle.Upload,
                scriptPath);
            x.Wait();

            return x.Result.JsonToObject(new ScriptRoot());
        }

        /// <summary>
        /// The download script content/file.
        /// </summary>
        /// <param name="scriptId">
        /// The script id.
        /// </param>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] DownloadScript(long scriptId, string folderPath)
        {
            if (scriptId <= 0 || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserQuestionnaire/DownloadScript?scriptId=" + scriptId,
                HttpMethod.Get,
                RouteStyle.Download,
                null);
            x.Wait();

            x.Result.DownloadFile(folderPath + x.Result.DonwloadFileName);
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }
    }
}