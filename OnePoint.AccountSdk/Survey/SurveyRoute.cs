using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Diagnostics;

namespace OnePoint.AccountSdk.Survey
{
    public class SurveyRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public SurveyRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// Get user surveys
        /// </summary>
        /// <returns>All surveys of a user</returns>
        public RootObject GetUserSurveys()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/GetSurveys", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Get project surveys.
        /// </summary>
        /// <param name="projectId">The project Id</param>
        /// <returns>Surveys of a project</returns>
        public RootObject GetProjectSurveys(long projectId)
        {
            if (projectId < 1)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/GetProjectSurveys?projectID=" + projectId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Add new survey
        /// </summary>
        /// <param name="survey">New survey object with property value Name, description and SurveyRefernce set</param>
        /// <param name="projectId">The project to which, add a new survey</param>
        /// <param name="type">The Survey Type</param>
        /// <returns></returns>
        public RootObject AddSurvey(string name, string description, string reference, long projectId, SurveyType type = SurveyType.App, short estimatedTime = 1, bool offline = false)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(reference) || projectId == 0)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyName = name, SurveyDescription = description, SurveyChannel = (short)type, SurveyReference = reference, ProjectID = projectId, IsOffline = offline, EstimateSurvey = estimatedTime });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/AddSurvey", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Delete survey(s)
        /// </summary>
        /// <param name="surveyids">List of surveyId(s)</param>
        /// <returns>Success</returns>
        public RootObject DeleteSurvey(List<long> surveyids)
        {
            if (surveyids.Count < 1)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)"); ;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/DeleteSurvey?SurveyIDs=" + string.Join(",", surveyids), HttpMethod.Delete, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Update survey details
        /// </summary>
        /// <param name="surveyId">the surveyId</param>
        /// <param name="name">The survey name</param>
        /// <param name="description">the survey description</param>
        /// <param name="type">The survey type</param>
        /// <returns>Updated survey object</returns>
        public RootObject UpdateSurvey(long surveyId, string name, string description, SurveyType type, short estimatedTime)
        {
            if (surveyId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, Name = name, Description = description, SurveyChannel = (short)type, EstimateSurvey = estimatedTime });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/UpdateSurvey", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Get survey details
        /// </summary>
        /// <param name="surveyId">The survey id</param>
        /// <returns>The survey object</returns>
        public RootObject GetSurveyDetails(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/GetSurveyDetail?surveyId=" + String.Join(",", surveyId), HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");

        }

        /// <summary>
        /// Duplicate the existing survey
        /// </summary>
        /// <param name="surveyId">The survey to duplicate</param>
        /// <returns>The duplicated survey object</returns>
        public RootObject DuplicateSurvey(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/CopySurvey?surveyId=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }

        /// <summary>
        /// Export survey results/report.
        /// </summary>
        /// <param name="folderPath">Folder to save report file</param>
        /// <param name="surveyId">The surveyid to export</param>
        /// <param name="languageId">The report data langauage</param>
        /// <param name="fromDate">The from date</param>
        /// <param name="toDate">The to date</param>
        /// <param name="factor">Include or exclude factor</param>
        /// <param name="systemVariable">Include or exclude system varaibles</param>
        /// <param name="alldata">Export results form begining of the survey</param>
        /// <returns>Byte array of excel file and report file to specified folder</returns>
        public byte[] ExportResults(string folderPath, long surveyId, int languageId, DateTime? fromDate = null, DateTime? toDate = null, bool factor = false, bool systemVariable = false, bool alldata = false)
        {
            if (surveyId <= 0 || languageId <= 0 || !Directory.Exists(folderPath))
            {
                return null;
            }

            if (fromDate == null || toDate == null)
            {
                fromDate = new DateTime(2000, 01, 01);
                toDate = DateTime.Now;
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, FromDate = fromDate, ToDate = toDate, AllData = alldata, LanguageID = languageId, Factor = factor, SystemVariables = systemVariable, ReportType = 1, OnlyComplete = 1 });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/ExportSurvey", HttpMethod.Post, RouteStyle.Download, requestArg);
            x.Wait();
            x.Result.DownloadFile(folderPath + surveyId.ToString() + "_SurveyResult_" + DateTime.Now.ToString("ddMMyyyy-HHmm") + ".xlsx");

            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// Export email tracking report.
        /// </summary>
        /// <param name="surveyId">The survey id</param>
        /// <param name="folderPath">The folder to save report file</param>
        /// <returns>Byte array of excel file and report file to specified folder</returns>
        public byte[] ExportTrackingReport(long surveyId, string folderPath)
        {
            if (surveyId <= 0 || string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/GetTrackingReport?surveyid=" + surveyId, HttpMethod.Get, RouteStyle.Download, null);
            x.Wait();
            x.Result.DownloadFile(folderPath + surveyId.ToString() + "_EmailTrackReport" + ".xlsx");
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// Export panellist email invites report.
        /// </summary>
        /// <param name="surveyId">The surveyid</param>
        /// <param name="folderPath">Folder to save report file</param>
        /// <param name="status">Invite email status</param>
        /// <returns>Byte array of excel file and report file to specified folder</returns>
        public byte[] ExportInvites(long surveyId, string folderPath, SentStatuses status)
        {
            if (surveyId <= 0 || string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return null;
            }
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/DownloadInvites?surveyId=" + surveyId + "&InvitationType=" + (int)status, HttpMethod.Get, RouteStyle.Download, null);
            x.Wait();
            x.Result.DownloadFile(folderPath + surveyId.ToString() + "_Invites" + ".xlsx");
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// Export survey report in Triple-s format.
        /// </summary>
        /// <param name="surveyId">The survey id</param>
        /// <returns></returns>
        public List<MemoryStream> ExportSSSReport(long surveyId)
        {
            if (surveyId <= 0)
            {
                return null;
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, FromDate = DateTime.Now.AddYears(-1), ToDate = DateTime.Now.AddDays(1), AllData = true, LanguageID = 49, Factor = false, SystemVariables = false, ReportType = 2, OnlyComplete = 1 });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/DownloadSSS?surveyid=" + surveyId, HttpMethod.Get, RouteStyle.Download, null);
            x.Wait();

            var bufferList = x.Result.JsonReadByJarray("_buffer");
            var list = new List<MemoryStream>();

            foreach (string buffer in bufferList)
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(buffer);
                list.Add(new MemoryStream(byteArray));
            }

            return list;
        }

        public RootObject CheckSurveyReference(string refernce)
        {
            if (string.IsNullOrEmpty(refernce))
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserSurvey/CheckSurveyReference?surveyRef=" + refernce, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Surveys");
        }
    }
}