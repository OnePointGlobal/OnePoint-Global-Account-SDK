// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRoute.cs" company="OnePoint Global Ltd">
//  Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.  
// </copyright>
// <summary>
//   The user, manages user profile data and authentication.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnePoint.AccountSdk.User
{
    /// <summary>
    /// The user route class provides the code to authenticate and update user profle data.
    /// </summary>
    public class UserRoute
    {
        /// <summary>
        /// Gets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public UserRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The account toolkit login.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="UserRoot"/>.
        /// </returns>
        public UserRoot Login(string username, string password)
        {
            var requestArg = JsonConvert.SerializeObject(new { UserName = username, Password = password });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserAuthentication/Login",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            SetSession(x.Result);
            return x.Result.JsonToObject(new UserRoot());
        }

        /// <summary>
        /// The log out.
        /// </summary>
        /// <returns>
        /// The <see cref="UserRoot"/>.
        /// </returns>
        public UserRoot LogOut()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserAuthentication/Logout",
                HttpMethod.Post,
                RouteStyle.Rpc,
                null);
            x.Wait();
            var response = x.Result.JsonToObject(new UserRoot());

            if (response.IsSuccess)
            {
                ClearSession();
            }

            return response;
        }

        /// <summary>
        /// The login by user unique shared key.
        /// </summary>
        /// <param name="sharedKey">
        /// The shared key.
        /// </param>
        /// <returns>
        /// The <see cref="UserRoot"/>.
        /// </returns>
        public UserRoot LoginBySharedKey(string sharedKey)
        {
            var requestArg = JsonConvert.SerializeObject(new { SharedKey = sharedKey });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserAuthentication/LoginByKey",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            SetSession(x.Result);
            return x.Result.JsonToObject(new UserRoot());
        }

        /// <summary>
        /// The json read.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="UserRoot"/>.
        /// </returns>
        private UserRoot JsonRead(Result result)
        {
            SetSession(result);
            var rootObj = result.Decoder(new UserRoot());

            rootObj.IsSuccess = rootObj.UserProfile != null;

            return rootObj;
        }

        /// <summary>
        /// The set session.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        private void SetSession(Result result)
        {
            if (result.IsError)
            {
                return;
            }

            var data = JObject.Parse(result.ObjectResult);
            if (data["SessionID"] != null)
            {
                RequestHandler.SessionId = data["SessionID"].ToString();
            }
        }

        /// <summary>
        /// The clear session.
        /// </summary>
        private void ClearSession()
        {
            RequestHandler.SessionId = null;
        }
    }
}