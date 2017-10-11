using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace OnePoint.AccountSdk.Project
{
    public class ProjectRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public ProjectRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public RootObject GetUserProjects()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserProject/GetProjects", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return this.jsonRead(x.Result);
        }

        public RootObject DeleteProject(long projectID)
        {
            if (projectID <= 0)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserProject/DeleteProject?id=" + projectID, HttpMethod.Delete, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public RootObject AddProject(string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserProject/AddProject", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public RootObject UpdateProject(long projectID, string name, string description)
        {
            if (projectID <= 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { ProjectID = projectID, Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserProject/UpdateProject", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public byte[] ExportResults(string folderPath, long projectID, int languageId, DateTime? fromDate = null, DateTime? toDate = null, bool factor = false, bool systemVariable = false, bool alldata = false)
        {
            if (projectID <= 0 || languageId <= 0 || !Directory.Exists(folderPath))
            {
                return null;
            }

            if (fromDate == null || toDate == null)
            {
                fromDate = new DateTime(2000, 01, 01);
                toDate = DateTime.Now;
            }

            var requestArg = JsonConvert.SerializeObject(new { ProjectID = projectID, FromDate = fromDate, ToDate = toDate, AllData = alldata, LanguageID = languageId, Factor = factor, SystemVariables = systemVariable });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserProject/ExportProject", HttpMethod.Post, RouteStyle.Download, requestArg);
            x.Wait();
            x.Result.DownloadFile(folderPath + projectID.ToString() + "_ProjectResult_" + DateTime.Now.ToString("dd/mm/yy") + ".xlsx");

            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;

        }

        private RootObject jsonRead(Result result)
        {
            if (!result.IsError)
            {
                JToken token = JToken.Parse(result.ObjectResult);
                result.ObjectResult = JsonConvert.SerializeObject(new { Projects = token, IsSuccess = true }); ;
            }
            return result.Decoder(new RootObject());
        }
    }
}

