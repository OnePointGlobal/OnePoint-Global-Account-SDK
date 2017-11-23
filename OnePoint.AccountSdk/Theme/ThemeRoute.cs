using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Theme
{
    public class ThemeRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        public ThemeRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public ThemeRoot GetThemes(ThemeType type)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/GetThemes?type=" + (short)type, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        public ThemeListRoot GetThemeDetails(long themeTemplateId)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/GetThemeDetail?themeTemplateId=" + themeTemplateId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRoot(), "ThemeList");
        }

        public ThemeRoot DuplicateTheme(long themeTemplateId)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/CopyTheme?themeTemplateId=" + themeTemplateId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        public ThemeRoot DeleteThemes(List<long> deleteThemeTemplateIds)
        {
            var requestArg = JsonConvert.SerializeObject(new { deleteThemeTemplateIds = string.Join(",", deleteThemeTemplateIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/DeleteTheme", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }

        public ThemeRoot AddTheme(string name, string description, ThemeType type)
        {
            if (string.IsNullOrEmpty(name))
            {
                return _result.ErrorToObject(new ThemeRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description, Type = (short)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/AddTheme?Name=" + name + "&Description=" + description, HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new ThemeRoot(), "Themes");
        }


        public ThemeListRoot SaveAppTheme(long themeTemplateID, string actionButton, string logoText, string linksColor)
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(actionButton) || string.IsNullOrEmpty(logoText) || string.IsNullOrEmpty(linksColor))
            {
                return _result.ErrorToObject(new ThemeListRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/SaveAppTheme?themeTemplateId=" + themeTemplateID + "&Actionbtn=" + actionButton + "&Logotext=" + logoText + "&Linkscolor=" + linksColor, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRoot(), "ThemeList");

        }

        public ThemeListRoot SaveAdminTheme(long themeTemplateID, string brandColor, string secondaryColor, string logoText)
        {
            if (themeTemplateID <= 0 || string.IsNullOrEmpty(brandColor) || string.IsNullOrEmpty(logoText) || string.IsNullOrEmpty(secondaryColor))
            {
                return _result.ErrorToObject(new ThemeListRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserTheme/SaveAdminTheme?themeTemplateId=" + themeTemplateID + "&Brandcolor=" + brandColor + "&Secondarycolor=" + secondaryColor + "&Logotext=" + logoText, HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new ThemeListRoot(), "ThemeList");

        }
    }
}
