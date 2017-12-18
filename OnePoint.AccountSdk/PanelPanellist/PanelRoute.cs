// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PanelRoute.cs" company="">
//   
// </copyright>
// <summary>
//   The panel route.Manages panel data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OnePoint.AccountSdk.PanelPanellist
{
    /// <summary>
    /// The panel route class, provides the code for CRUD operation on user panel data.
    /// </summary>
    public class PanelRoute
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
        /// Initializes a new instance of the <see cref="PanelRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public PanelRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get user panels.
        /// </summary>
        /// <returns>
        /// The <see cref="PanelRootObject"/>.
        /// </returns>
        public PanelRootObject GetUserPanels()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanel/GetUserPanels",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        /// <summary>
        /// The add new panel.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="panelType">
        /// The panel type.
        /// </param>
        /// <param name="logoImagePath">
        /// The logo image path.
        /// </param>
        /// <param name="backGroundImagePath">
        /// The back ground image path.
        /// </param>
        /// <returns>
        /// The <see cref="PanelRootObject"/>.
        /// </returns>
        public PanelRootObject AddPanel(
            string name,
            string description,
            PanelType panelType,
            string logoImagePath = "",
            string backGroundImagePath = "")
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
                var logoimage = Helper.MoveFile(logoImagePath, "Panel-logo"); // File name must contains 'logo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(backGroundImagePath))
            {
                if (!File.Exists(backGroundImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }

                routeStyle = RouteStyle.Upload;
                var bkgImage = Helper.MoveFile(
                    backGroundImagePath,
                    "Panel-background"); // File name must contains 'background' keyword.
                files = files + bkgImage;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanel/AddPanel?Name=" + name + "&Description=" + description + "&panelType="
                + (Int32)panelType,
                HttpMethod.Post,
                routeStyle,
                string.IsNullOrEmpty(files) ? null : files);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        /// <summary>
        /// The delete panels.
        /// </summary>
        /// <param name="panelIds">
        /// The panel ids.
        /// </param>
        /// <returns>
        /// The <see cref="PanelRootObject"/>.
        /// </returns>
        public PanelRootObject DeletePanels(List<long> panelIds)
        {
            if (panelIds.Count < 1)
            {
                return _result.ErrorToObject(new PanelRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelIDs = string.Join(",", panelIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanel/DeletePanels",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        /// <summary>
        /// The update panel details.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="logoImagePath">
        /// The logo image path.
        /// </param>
        /// <param name="backGroundImagePath">
        /// The back ground image path.
        /// </param>
        /// <returns>
        /// The <see cref="PanelRootObject"/>.
        /// </returns>
        public PanelRootObject UpdatePanel(
            long panelId,
            string name,
            string description,
            string logoImagePath = "",
            string backGroundImagePath = "")
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
                var logoimage = Helper.MoveFile(logoImagePath, "Panel-logo"); // File name must contains 'logo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(backGroundImagePath))
            {
                if (!File.Exists(backGroundImagePath))
                {
                    return _result.ErrorToObject(new PanelRootObject(), "File not exist!");
                }

                routeStyle = RouteStyle.Upload;
                var Bkgimage = Helper.MoveFile(
                    backGroundImagePath,
                    "Panel-background"); // File name must contains 'background' keyword.
                files = files + Bkgimage;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanel/UpdatePanel?Editname=" + name + "&Editdescription=" + description + "&PanelId="
                + panelId,
                HttpMethod.Put,
                routeStyle,
                string.IsNullOrEmpty(files) ? null : files);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }

        /// <summary>
        /// The set panel theme.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="themeTemplateID">
        /// The theme template id.
        /// </param>
        /// <returns>
        /// The <see cref="PanelRootObject"/>.
        /// </returns>
        public PanelRootObject SetPanelTheme(long panelId, int themeTemplateID)
        {
            var requestArg = JsonConvert.SerializeObject(new { PanelId = panelId, ThemeId = themeTemplateID });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanel/SetTheme",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanelRootObject(), "Panels");
        }
    }
}