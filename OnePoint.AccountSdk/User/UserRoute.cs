using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace OnePoint.AccountSdk.User
{
    public class UserRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        public UserRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        /// <summary>
        /// User Authentication
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>User object on successfull authntication</returns>
        public RootObject Login(string username, string password)
        {
            var requestArg = JsonConvert.SerializeObject(new { UserName = username, Password = password });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserAuthentication/Login", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            SetSession(x.Result);
            return x.Result.JsonToObject(new RootObject());
        }

        public RootObject LogOut()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserAuthentication/Logout", HttpMethod.Post, RouteStyle.Rpc, null);
            x.Wait();
            var response = x.Result.JsonToObject(new RootObject());

            if (response.IsSuccess == true)
            {
                ClearSession();
            }
            return response;
        }

        public RootObject LoginBySharedKey(string sharedKey)
        {
            var requestArg = JsonConvert.SerializeObject(new { SharedKey = sharedKey});
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserAuthentication/LoginByKey", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            SetSession(x.Result);
            return x.Result.JsonToObject(new RootObject());
        }

        private RootObject jsonRead(Result result)
        {
            this.SetSession(result);
            var rootObj = result.Decoder(new RootObject());

            if (rootObj.UserProfile == null)
            {
                rootObj.IsSuccess = false;
            }
            else
            {
                rootObj.IsSuccess = true;
            }

            return rootObj;
        }

        private void SetSession(Result result)
        {
            if (result.IsError)
            {
                return;
            }

            var data = JObject.Parse(result.ObjectResult);
            if (data["SessionID"] != null)
            {
                this.requestHandler.SessionId = data["SessionID"].ToString();
            }
        }

        private void ClearSession()
        {
            this.requestHandler.SessionId = null;
        }

    }
}
