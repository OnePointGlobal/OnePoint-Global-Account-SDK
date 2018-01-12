// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeRoute.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The theme, manages user app and account theming data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Theme
{
    /// <summary>
    /// The theme route class, provides the code for CRUD operation on app and account themes.
    /// </summary>
    public class ThemeRoute
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
        /// Initializes a new instance of the <see cref="ThemeRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public ThemeRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get user themes.
        /// </summary>
        /// <param name="type">
        /// The theme type, app or account.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeRoot"/>.
        /// </returns>
        public ThemeRoot GetThemes(ThemeType type)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/GetThemes?type=" + (short)type,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        /// <summary>
        /// The get theme elements and details.
        /// </summary>
        /// <param name="themeTemplateId">
        /// The theme template id.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeListRoot"/>.
        /// </returns>
        public ThemeListRoot GetThemeDetails(long themeTemplateId)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/GetThemeDetail?themeTemplateId=" + themeTemplateId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRoot(), "ThemeList");
        }

        /// <summary>
        /// The duplicate exisitng theme.
        /// </summary>
        /// <param name="themeTemplateId">
        /// The theme template id.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeRoot"/>.
        /// </returns>
        public ThemeRoot DuplicateTheme(long themeTemplateId)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/CopyTheme?themeTemplateId=" + themeTemplateId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        /// <summary>
        /// The delete themes.
        /// </summary>
        /// <param name="deleteThemeTemplateIds">
        /// The delete theme template ids.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeRoot"/>.
        /// </returns>
        public ThemeRoot DeleteThemes(List<long> deleteThemeTemplateIds)
        {
            var requestArg = JsonConvert.SerializeObject(
                new { deleteThemeTemplateIds = string.Join(",", deleteThemeTemplateIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/DeleteTheme",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        /// <summary>
        /// The add new app or account theme.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="type">
        /// The type of theme.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeRoot"/>.
        /// </returns>
        public ThemeRoot AddTheme(string name, string description, ThemeType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _result.ErrorToObject(new ThemeRoot(), "Invalid parameter(s)");
            }

            var requestArg =
                JsonConvert.SerializeObject(new { Name = name, Description = description, Type = (short)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/AddTheme?Name=" + name + "&Description=" + description,
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        /// <summary>
        /// The update theme.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="themeTemplateID">
        /// The theme template id.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeRoot"/>.
        /// </returns>
        public ThemeRoot UpdateTheme(string name, long themeTemplateID)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _result.ErrorToObject(new ThemeRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, themeTemplateId = themeTemplateID });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/UpdateTheme",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        /// <summary>
        /// The save app theme, by adding theme elements.
        /// </summary>
        /// <param name="themeTemplateID">
        /// The theme template id.
        /// </param>
        /// <param name="actionButton">
        /// The action button hex color code.
        /// </param>
        /// <param name="linksColor">
        /// The links hex color code.
        /// </param>
        /// <param name="logoText">
        /// The logo text.
        /// </param>
        /// <param name="headerLogoFilePath">
        /// The header logo image file path.
        /// </param>
        /// <param name="loginBackgroundFilePath">
        /// The login background image file path.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeListRoot"/>.
        /// </returns>
        public ThemeListRoot SaveAppTheme(
            long themeTemplateID,
            string actionButton,
            string linksColor,
            string logoText = "",
            string headerLogoFilePath = "",
            string loginBackgroundFilePath = "")
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(actionButton) || string.IsNullOrEmpty(linksColor))
            {
                return _result.ErrorToObject(new ThemeListRoot(), "Invalid parameter(s)");
            }

            var files = string.Empty;
            var routeStyle = RouteStyle.Rpc;

            if (!string.IsNullOrEmpty(headerLogoFilePath))
            {
                if (!File.Exists(headerLogoFilePath))
                {
                    return _result.ErrorToObject(new ThemeListRoot(), "Invalid headerLogoFilePath parameter!");
                }

                routeStyle = RouteStyle.Upload;
                var logoimage = Helper.MoveFile(
                    headerLogoFilePath,
                    "theme-headerlogo"); // File name must contains 'headerlogo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(loginBackgroundFilePath))
            {
                if (!File.Exists(loginBackgroundFilePath))
                {
                    return _result.ErrorToObject(new ThemeListRoot(), "Invalid loginBackgroundFilePath parameter!");
                }

                routeStyle = RouteStyle.Upload;
                var bkgImage = Helper.MoveFile(
                    loginBackgroundFilePath,
                    "theme-loginbackground"); // File name must contains 'loginbackground' keyword.
                files = files + bkgImage;
            }

            Task<Result> x2 = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/SaveAppTheme?themeTemplateId=" + themeTemplateID + "&Actionbtn="
                + WebUtility.UrlEncode(actionButton) + "&Linkscolor=" + WebUtility.UrlEncode(linksColor)
                + "&useDefault=0" + "&Logotext=" + logoText,
                HttpMethod.Post,
                routeStyle,
                string.IsNullOrEmpty(files) ? null : files);
            x2.Wait();
            return x2.Result.JsonToObject(new ThemeListRoot(), "ThemeList");
        }

        /// <summary>
        /// The save admin theme and its elements.
        /// </summary>
        /// <param name="themeTemplateID">
        /// The theme template id.
        /// </param>
        /// <param name="brandColor">
        /// The brand hex color code.
        /// </param>
        /// <param name="secondaryColor">
        /// The secondary hex color code.
        /// </param>
        /// <param name="logoText">
        /// The logo text.
        /// </param>
        /// <param name="headerLogoFilePath">
        /// The header logo image file path.
        /// </param>
        /// <param name="loginBackgroundFilePath">
        /// The login background image file path.
        /// </param>
        /// <returns>
        /// The <see cref="ThemeListRoot"/>.
        /// </returns>
        public ThemeListRoot SaveAdminTheme(
            long themeTemplateID,
            string brandColor,
            string secondaryColor,
            string logoText = "",
            string headerLogoFilePath = "",
            string loginBackgroundFilePath = "")
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(brandColor) || string.IsNullOrEmpty(secondaryColor))
            {
                return _result.ErrorToObject(new ThemeListRoot(), "Invalid parameter(s)");
            }

            var files = string.Empty;
            var routeStyle = RouteStyle.Rpc;

            if (!string.IsNullOrEmpty(headerLogoFilePath))
            {
                if (!File.Exists(headerLogoFilePath))
                {
                    return _result.ErrorToObject(new ThemeListRoot(), "Invalid headerLogoFilePath parameter!");
                }

                routeStyle = RouteStyle.Upload;
                var logoimage = Helper.MoveFile(
                    headerLogoFilePath,
                    "admintheme-logo"); // File name must contains 'logo' keyword.
                files = logoimage + ";";
            }

            if (!string.IsNullOrEmpty(loginBackgroundFilePath))
            {
                if (!File.Exists(loginBackgroundFilePath))
                {
                    return _result.ErrorToObject(new ThemeListRoot(), "Invalid loginBackgroundFilePath parameter!");
                }

                routeStyle = RouteStyle.Upload;
                var bkgImage = Helper.MoveFile(
                    loginBackgroundFilePath,
                    "admintheme-pageloader"); // File name must contains 'pagaloader' keyword.
                files = files + bkgImage;
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserTheme/SaveAdminTheme?themeTemplateId=" + themeTemplateID + "&Brandcolor="
                + WebUtility.UrlEncode(brandColor) + "&Secondarycolor=" + WebUtility.UrlEncode(secondaryColor)
                + "&useDefault=0" + "&Logotext=" + logoText,
                HttpMethod.Post,
                routeStyle,
                string.IsNullOrEmpty(files) ? null : files);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRoot(), "ThemeList");
        }
    }
}