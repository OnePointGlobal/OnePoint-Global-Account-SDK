using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Theme
{
    public class ThemeRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public ThemeRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public RootObject GetThemes(ThemeType type)
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/GetThemes?type=" + (short)type, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Themes");
        }

        public ThemeListRootObject GetThemeDetails(long themeTemplateId)
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/GetThemeDetail?themeTemplateId=" + themeTemplateId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRootObject(), "ThemeList");
        }

        public RootObject DuplicateTheme(long themeTemplateId)
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/CopyTheme?themeTemplateId=" + themeTemplateId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "Themes");
        }

        public RootObject DeleteThemes(List<long> deleteThemeTemplateIds)
        {
            var requestArg = JsonConvert.SerializeObject(new { deleteThemeTemplateIds = string.Join(",", deleteThemeTemplateIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/DeleteTheme", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Themes");
        }

        public RootObject AddTheme(string name, string description, ThemeType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description, Type = (short)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/AddTheme?Name=" + name + "&Description=" + description, HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "Themes");
        }


        public ThemeListRootObject SaveAppTheme(long themeTemplateID, string actionButton, string logoText, string linksColor)
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(actionButton) || string.IsNullOrEmpty(logoText) || string.IsNullOrEmpty(linksColor))
            {
                return result.ErrorToObject(new ThemeListRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/SaveAppTheme?themeTemplateId=" + themeTemplateID + "&Actionbtn=" + actionButton + "&Logotext=" + logoText + "&Linkscolor=" + linksColor, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRootObject(), "ThemeList");

        }

        public ThemeListRootObject SaveAdminTheme(long themeTemplateID, string brandColor, string secondaryColor, string logoText)
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(brandColor) || string.IsNullOrEmpty(logoText) || string.IsNullOrEmpty(secondaryColor))
            {
                return result.ErrorToObject(new ThemeListRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserTheme/SaveAdminTheme?themeTemplateId=" + themeTemplateID + "&Brandcolor=" + brandColor + "&Secondarycolor=" + secondaryColor + "&Logotext=" + logoText, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRootObject(), "ThemeList");

        }
    }
}
