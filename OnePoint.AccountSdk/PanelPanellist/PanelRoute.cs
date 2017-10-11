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
    public class PanelRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public PanelRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public PanelRootObject GetUserPanels()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanel/GetPanels", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject AddPanel(string name, string description, PanelType panelType)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            //var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description, panelType = (Int16)panelType, submitButtonUpload = string.Empty });
            //requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanel/AddPanel?Name=" + name + "&Description=" + description + "&panelType=" + (Int32)panelType, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject DeletePanels(List<long> panelIds)
        {
            if (panelIds.Count < 1)
            {
                return result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelIDs = String.Join(",", panelIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanel/DeletePanels", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject UpdatePanel(string name, string description, long panelId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || panelId < 1)
            {
                return result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanel/UpdatePanel?Editname=" + name + "&Editdescription=" + description + "&PanelId=" + panelId, HttpMethod.Put, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

    }
}
