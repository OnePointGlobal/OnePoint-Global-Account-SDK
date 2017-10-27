using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace OnePoint.AccountSdk.Project
{
    public class ProjectRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public ProjectRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public RootObject GetUserProjects()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserProject/GetProjects", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return JsonRead(x.Result);
        }

        public RootObject DeleteProject(long projectId)
        {
            if (projectId <= 0)
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserProject/DeleteProject?id=" + projectId, HttpMethod.Delete, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public RootObject AddProject(string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserProject/AddProject", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public RootObject UpdateProject(long projectID, string name, string description)
        {
            if (projectID <= 0 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { ProjectID = projectID, Name = name, Description = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserProject/UpdateProject", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Projects");
        }

        public byte[] ExportResults(string folderPath, long projectId, int languageId, DateTime? fromDate = null, DateTime? toDate = null, bool factor = false, bool systemVariable = false, bool alldata = false)
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

            var requestArg = JsonConvert.SerializeObject(new { ProjectID = projectId, FromDate = fromDate, ToDate = toDate, AllData = alldata, LanguageID = languageId, Factor = factor, SystemVariables = systemVariable });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserProject/ExportProject", HttpMethod.Post, RouteStyle.Download, requestArg);
            x.Wait();
            x.Result.DownloadFile(folderPath + projectId + "_ProjectResult_" + DateTime.Now.ToString("dd/mm/yy") + ".xlsx");

            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }

        private static RootObject JsonRead(Result result)
        {
            if (result.IsError) return result.Decoder(new RootObject());

            var token = JToken.Parse(result.ObjectResult);
            result.ObjectResult = JsonConvert.SerializeObject(new { Projects = token, IsSuccess = true }); ;
            return result.Decoder(new RootObject());
        }
    }
}

