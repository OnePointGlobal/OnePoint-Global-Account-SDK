// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The project, manages project data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnePoint.AccountSdk.Project
{
    /// <summary>
    /// The project route class, provides the code for CRUD operation on project data and export project report.
    /// </summary>
    public class ProjectRoute
    {
        /// <summary>
        /// Gets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// The _result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public ProjectRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get user projects list.
        /// </summary>
        /// <returns>
        /// The <see cref="ProjectRoot"/>.
        /// </returns>
        public ProjectRoot GetUserProjects()
        {
            Task<Result> x = this.RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserProject/GetProjects",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return jsonRead(x.Result);
        }

        /// <summary>
        /// The delete project.
        /// </summary>
        /// <param name="projectId">
        /// The project id.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectRoot"/>.
        /// </returns>
        public ProjectRoot DeleteProject(long projectId)
        {
            if (projectId <= 0)
            {
                return _result.ErrorToObject(new ProjectRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserProject/DeleteProject?id=" + projectId,
                HttpMethod.Delete,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ProjectRoot(), "Projects");
        }

        /// <summary>
        /// The add new project.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectRoot"/>.
        /// </returns>
        public ProjectRoot AddProject(string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new ProjectRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserProject/AddProject",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ProjectRoot(), "Projects");
        }

        /// <summary>
        /// The update project.
        /// </summary>
        /// <param name="projectId">
        /// The project id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectRoot"/>.
        /// </returns>
        public ProjectRoot UpdateProject(long projectId, string name, string description)
        {
            if (projectId <= 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new ProjectRoot(), "Invalid parameter(s)");
            }

            var requestArg =
                JsonConvert.SerializeObject(new { ProjectID = projectId, Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserProject/UpdateProject",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ProjectRoot(), "Projects");
        }

        /// <summary>
        /// The export project surveys panellist response results.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <param name="projectId">
        /// The project id.
        /// </param>
        /// <param name="languageId">
        /// The surveys language id.
        /// </param>
        /// <param name="fromDate">
        /// The from date,consider surveys results from the specified datetime.
        /// </param>
        /// <param name="toDate">
        /// The to date, consider surveys results upto the specified datetime.
        /// </param>
        /// <param name="factor">
        /// The factor, includes script factor value in report.
        /// </param>
        /// <param name="systemVariable">
        /// The system variable, includes system variable in report.
        /// </param>
        /// <param name="alldata">
        /// he alldata, consideres survey results from begining date of the survey start to the present date.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] ExportResults(
            string folderPath,
            long projectId,
            int languageId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            bool factor = false,
            bool systemVariable = false,
            bool alldata = false)
        {
            if (projectId <= 0 || languageId <= 0 || !Directory.Exists(folderPath))
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
                        ProjectID = projectId,
                        FromDate = fromDate,
                        ToDate = toDate,
                        AllData = alldata,
                        LanguageID = languageId,
                        Factor = factor,
                        SystemVariables = systemVariable
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserProject/ExportProject",
                HttpMethod.Post,
                RouteStyle.Download,
                requestArg);
            x.Wait();
            x.Result.DownloadFile(
                folderPath + projectId + "_ProjectResult_" + DateTime.Now.ToString("dd/mm/yy") + ".xlsx");

            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        /// <summary>
        /// The json read.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="ProjectRoot"/>.
        /// </returns>
        private ProjectRoot jsonRead(Result result)
        {
            if (result.IsError) return result.Decoder(new ProjectRoot());
            var token = JToken.Parse(result.ObjectResult);
            result.ObjectResult = JsonConvert.SerializeObject(new { Projects = token, IsSuccess = true });
            
            return result.Decoder(new ProjectRoot());
        }
    }
}