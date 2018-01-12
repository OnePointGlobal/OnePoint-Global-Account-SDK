// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SurveyRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The survey, manages survey data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Survey
{
    /// <summary>
    /// The survey route class, provides the code for CRUD operation on survey, exporting survey reports.
    /// </summary>
    public class SurveyRoute
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
        /// Initializes a new instance of the <see cref="SurveyRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public SurveyRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get user all projects surveys.
        /// </summary>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot GetUserSurveys()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetSurveys",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The get specific project surveys.
        /// </summary>
        /// <param name="projectId">
        /// The project id.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot GetProjectSurveys(long projectId)
        {
            if (projectId < 1)
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetProjectSurveys?projectID=" + projectId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The add new survey to a project.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="reference">
        /// The unique reference for survey identification.
        /// </param>
        /// <param name="projectId">
        /// The project id, to which to add new survey.
        /// </param>
        /// <param name="type">
        /// The type of survey.
        /// </param>
        /// <param name="estimatedTime">
        /// The survey estimated time to complete survey.
        /// </param>
        /// <param name="offline">
        /// The offline, indicates the survey is offline.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot AddSurvey(
            string name,
            string description,
            string reference,
            long projectId,
            SurveyType type = SurveyType.App,
            short estimatedTime = 1,
            bool offline = false)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(reference)
                || projectId == 0)
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyName = name,
                        SurveyDescription = description,
                        SurveyChannel = (short)type,
                        SurveyReference = reference,
                        ProjectID = projectId,
                        IsOffline = offline,
                        EstimateSurvey = estimatedTime
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/AddSurvey",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The delete survey(s).
        /// </summary>
        /// <param name="surveyids">
        /// The surveyids.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot DeleteSurvey(List<long> surveyids)
        {
            if (surveyids.Count < 1)
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
                
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/DeleteSurvey?SurveyIDs=" + string.Join(",", surveyids),
                HttpMethod.Delete,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The update survey details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="estimatedTime">
        /// The survey estimated time to complete survey.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot UpdateSurvey(
            long surveyId,
            string name,
            string description,
            SurveyType type,
            short estimatedTime)
        {
            if (surveyId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        Name = name,
                        Description = description,
                        SurveyChannel = (short)type,
                        EstimateSurvey = estimatedTime
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/UpdateSurvey",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The get a survey details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot GetSurveyDetails(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetSurveyDetail?surveyId=" + string.Join(",", surveyId),
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The duplicate existing survey.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot DuplicateSurvey(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/CopySurvey?surveyId=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The export survey panellist reponse results in excel sheet format.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path to which file is exported.
        /// </param>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="languageId">
        /// The language id.
        /// </param>
        /// <param name="fromDate">
        /// The from date, consider survey results from specified datetime.
        /// </param>
        /// <param name="toDate">
        /// The to date, consider survey results upto specified datetime.
        /// </param>
        /// <param name="factor">
        /// The factor, includes script factor value in report.
        /// </param>
        /// <param name="systemVariable">
        /// The system variable, includes to include script system variable in report.
        /// </param>
        /// <param name="alldata">
        /// The alldata, consideres survey results from begining date of the survey start to the present date.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ExportResults(
            string folderPath,
            long surveyId,
            int languageId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool factor = false,
            bool systemVariable = false,
            bool alldata = false)
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

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        FromDate = fromDate,
                        ToDate = toDate,
                        AllData = alldata,
                        LanguageID = languageId,
                        Factor = factor,
                        SystemVariables = systemVariable,
                        ReportType = 1,
                        OnlyComplete = 1
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/ExportSurvey",
                HttpMethod.Post,
                RouteStyle.Download,
                requestArg);
            x.Wait();
            x.Result.DownloadFile(
                folderPath + surveyId.ToString() + "_SurveyResult_" + DateTime.Now.ToString("ddMMyyyy-HHmm") + ".xlsx");

            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// The export tracking report, of survey invitation, in excel format.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="folderPath">
        /// The folder path to export report file.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ExportTrackingReport(long surveyId, string folderPath)
        {
            if (surveyId <= 0 || string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetTrackingReport?surveyid=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Download,
                null);
            x.Wait();
            x.Result.DownloadFile(folderPath + surveyId.ToString() + "_EmailTrackReport" + ".xlsx");
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// The export invites, by filtering data.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <param name="status">
        /// The sent status.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ExportInvites(long surveyId, string folderPath, SentStatuses status)
        {
            if (surveyId <= 0 || string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                return null;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/DownloadInvites?surveyId=" + surveyId + "&InvitationType=" + (int)status,
                HttpMethod.Get,
                RouteStyle.Download,
                null);
            x.Wait();
            x.Result.DownloadFile(folderPath + surveyId.ToString() + "_Invites" + ".xlsx");
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// The export survey results in SSS report format.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<MemoryStream> ExportSSSReport(long surveyId)
        {
            if (surveyId <= 0)
            {
                return null;
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        FromDate = DateTime.Now.AddYears(-1),
                        ToDate = DateTime.Now.AddDays(1),
                        AllData = true,
                        LanguageID = 49,
                        Factor = false,
                        SystemVariables = false,
                        ReportType = 2,
                        OnlyComplete = 1
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/DownloadSSS?surveyid=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Download,
                null);
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

        /// <summary>
        /// The check fo survey reference uniqueness.
        /// </summary>
        /// <param name="refernce">
        /// The refernce.
        /// </param>
        /// <returns>
        /// The <see cref="SurveyRoot"/>.
        /// </returns>
        public SurveyRoot CheckSurveyReference(string refernce)
        {
            if (string.IsNullOrEmpty(refernce))
            {
                return _result.ErrorToObject(new SurveyRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/CheckSurveyReference?surveyRef=" + refernce,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveyRoot(), "Surveys");
        }

        /// <summary>
        /// The get survey summary.
        /// </summary>
        /// <param name="surveyid">
        /// The surveyid.
        /// </param>
        /// <returns>
        /// The <see cref="SurveySammary"/>.
        /// </returns>
        public SurveySammary GetSurveySummary(long surveyid)
        {
            if (surveyid < 1)
            {
                return _result.ErrorToObject(new SurveySammary(), "Invalid parameter(s)");
            }

            var x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetInvitationCount?surveyId=" + surveyid,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveySammary());
        }

        /// <summary>
        /// The get survey sub event details.
        /// </summary>
        /// <param name="surveyid">
        /// The surveyid.
        /// </param>
        /// <returns>
        /// The <see cref="SurveySubEvent"/>.
        /// </returns>
        public SurveySubEvent GetSubEventStatus(long surveyid)
        {
            if (surveyid < 1)
            {
                // return _result.ErrorToObject(new SurveySammary(), "Invalid parameter(s)");
            }

            var x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSurvey/GetSubEventStatus?surveyid=" + surveyid,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SurveySubEvent());
        }
    }
}