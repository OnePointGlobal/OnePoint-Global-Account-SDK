// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileElementRoute.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights rese 
// </copyright>
// <summary>
//   The profile element route.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.PanelPanellist
{
    /// <summary>
    /// The profile element route class,  provides the code for CRUD operation on panellist profile variables/elements data. 
    /// </summary>
    public class ProfileElementRoute
    {
        /// <summary>
        /// Gets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// The _result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileElementRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public ProfileElementRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get panel profile variables.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileVariableRootObject"/>.
        /// </returns>
        public ProfileVariableRootObject GetPanelProfileVariables(long panelId)
        {
            if (panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/GetProfileVariables?panelID=" + panelId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        /// <summary>
        /// The update profile variable details.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="variableId">
        /// The variable id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileVariableRootObject"/>.
        /// </returns>
        public ProfileVariableRootObject UpdateProfileVariable(
            long panelId,
            long variableId,
            string name,
            ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1 || variableId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        EditPanelID = panelId,
                        EditProfileElementID = variableId,
                        EditName = name,
                        EditType = (int)type
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/UpdateProfileElement",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        /// <summary>
        /// The add new profile variable.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileVariableRootObject"/>.
        /// </returns>
        public ProfileVariableRootObject AddProfileVariable(long panelId, string name, ProfileElementType type)
        {
            if (string.IsNullOrEmpty(name) || panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelId = panelId, Name = name, Type = (int)type });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/AddProfileElement",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }

        /// <summary>
        /// The delete profile variable.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="variableId">
        /// The variable id.
        /// </param>
        /// <returns>
        /// The <see cref="ProfileVariableRootObject"/>.
        /// </returns>
        public ProfileVariableRootObject DeleteProfileVariable(long panelId, long variableId)
        {
            if (variableId < 1 || panelId < 1)
            {
                return _result.ErrorToObject(new ProfileVariableRootObject(), "Invalid parameter(s)");
            }

            var requestArg =
                JsonConvert.SerializeObject(new { ProfileElementID = variableId, ElementPanelID = panelId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/DeleteProfileElement",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new ProfileVariableRootObject(), "ProfileElements");
        }
    }
}