using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.PanelPanellist
{
    public class ProfileElementRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get;}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public ProfileElementRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public ProfileVariableRootObject GetPanelProfileVariables(long panelId)
        {
            if (panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetProfileVariables?panelID=" + panelId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        public ProfileVariableRootObject UpdateProfileVariable(long panelId, long variableId, string name, ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1 || variableId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { EditPanelID = panelId, EditProfileElementID = variableId, EditName = name, EditType = (int)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/UpdateProfileElement", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        public ProfileVariableRootObject AddProfileVariable(long panelId, string name, ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelId = panelId, Name = name, Type = (int)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/AddProfileElement", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        public ProfileVariableRootObject DeleteProfileVariable(long panelId, long variableId)
        {
            if (variableId < 1 || panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { ProfileElementID = variableId, ElementPanelID = panelId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/DeleteProfileElement", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }
    }
}
