using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.PanelPanellist
{
    public class PanelRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get;}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public PanelRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public PanelRootObject GetUserPanels()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanel/GetUserPanels", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject AddPanel(string name, string description, PanelType panelType, string logoImagePath = "", string backGroundImagePath = "")
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            string files = string.Empty;
            RouteStyle routeStyle = RouteStyle.Rpc;

            if (!string.IsNullOrEmpty(logoImagePath))
            {
                if (!File.Exists(logoImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }
                routeStyle = RouteStyle.Upload;
                var logoimage = Helper.MoveFile(logoImagePath, "Panel-logo"); //File name must contains 'logo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(backGroundImagePath))
            {
                if (!File.Exists(backGroundImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }
                routeStyle = RouteStyle.Upload;
                var bkgImage = Helper.MoveFile(backGroundImagePath, "Panel-background"); //File name must contains 'background' keyword.
                files = files + bkgImage;
            }


            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanel/AddPanel?Name=" + name + "&Description=" + description + "&panelType=" + (Int32)panelType, HttpMethod.Post, routeStyle, string.IsNullOrEmpty(files) ? null : files);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject DeletePanels(List<long> panelIds)
        {
            if (panelIds.Count < 1)
            {
                return _result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelIDs = String.Join(",", panelIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanel/DeletePanels", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject UpdatePanel(long panelId, string name, string description, string logoImagePath = "", string backGroundImagePath = "")
        {
            string files = string.Empty;
            RouteStyle routeStyle = RouteStyle.Rpc;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || panelId < 1)
            {
                return _result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            if (!string.IsNullOrEmpty(logoImagePath))
            {
                if (!File.Exists(logoImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }
                routeStyle = RouteStyle.Upload;
                var logoimage = Helper.MoveFile(logoImagePath, "Panel-logo"); //File name must contains 'logo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(backGroundImagePath))
            {
                if (!File.Exists(backGroundImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }
                routeStyle = RouteStyle.Upload;
                var Bkgimage = Helper.MoveFile(backGroundImagePath, "Panel-background"); //File name must contains 'background' keyword.
                files = files + Bkgimage;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanel/UpdatePanel?Editname=" + name + "&Editdescription=" + description + "&PanelId=" + panelId, HttpMethod.Put, routeStyle, string.IsNullOrEmpty(files) ? null : files);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        public PanelRootObject SetPanelTheme(long panelId, int themeTemplateID)
        {
            var requestArg = JsonConvert.SerializeObject(new { PanelId = panelId, ThemeId = themeTemplateID });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanel/SetTheme", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

    }
}
