using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.PanelPanellist
{
    public class ProfileElementRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public ProfileElementRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public ProfileElementRootObject GetPanelProfileElements(long panelId)
        {
            if (panelId < 1)
            {
                return result.ErrorToObject(new ProfileElementRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetProfileVariables?panelID=" + panelId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ProfileElementRootObject(), "ProfileElements");
        }

        public ProfileElementRootObject UpdateProfileElement(long panelId, long elementId, string name, ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1 || elementId < 1)
            {
                return result.ErrorToObject(new ProfileElementRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { EditPanelID = panelId, EditProfileElementID = elementId, EditName = name, EditType = (int)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/UpdateProfileElement", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileElementRootObject(), "ProfileElements");
        }

        public ProfileElementRootObject AddProfileElement(long panelId, string name, ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1)
            {
                return result.ErrorToObject(new ProfileElementRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelId = panelId, Name = name, Type = (int)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/AddProfileElement", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileElementRootObject(), "ProfileElements");
        }

        public ProfileElementRootObject DeleteProfileElements(long elementId, long panelId)
        {
            if (elementId < 1 || panelId < 1)
            {
                return result.ErrorToObject(new ProfileElementRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { ProfileElementID = elementId, ElementPanelID = panelId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/DeleteProfileElement", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileElementRootObject(), "ProfileElements");
        }
    }
}
