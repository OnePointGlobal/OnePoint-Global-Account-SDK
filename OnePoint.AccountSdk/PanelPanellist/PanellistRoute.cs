using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace OnePoint.AccountSdk.PanelPanellist
{
    public class PanellistRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public PanellistRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        public PanellistRootObject GetPanelPanellist(long panelId)
        {
            if (panelId < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetPanellists?panelID=" + panelId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject DeletePanellist(long panelId, List<long> panellistIds)
        {
            if (panelId < 1 || panellistIds.Count < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { PanelID = panelId, PanellistIDs = String.Join(",", panellistIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/DeletePanellists?panelID=" + panelId, HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject UploadPanllistFile(long panelId, string filePath)
        {
            if (panelId < 1 || !File.Exists(filePath))
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/FileUpload?panelId=" + panelId, HttpMethod.Post, RouteStyle.Upload, filePath);
            x.Wait();

            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject AddPanellist(long panelId, string firstName, string lastName, string email, long mobileNumber, DateTime dob, int countryCode, string address1 = "", string address2 = "", string postalcode = "", string website = "", Title title = Title.Mr, Gender gender = Gender.NotSpecified, MaritalStatus martialStatus = MaritalStatus.NotSpecified)
        {
            if (panelId < 1 || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new
            {
                PanelID = panelId,
                Email = email,
                Firstname = firstName,
                Lastname = lastName,
                Title = (int)title,
                Mobilenumber = mobileNumber,
                Dob = dob,
                Website = website,
                Address1 = address1,
                Address2 = address2,
                Postalcode = postalcode,
                CountryCode = countryCode,
                Gender = gender,
                MaritalStatus = martialStatus,
                PanellistID = 0
            });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/InsertPanellist", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject UpdatePanellist(long panelId, long panellistId, string firstName, string lastName, long mobileNumber, DateTime dob, int countryCode, string address1 = "", string address2 = "", string postalcode = "", string website = "", Title title = Title.Mr, Gender gender = Gender.NotSpecified, MaritalStatus martialStatus = MaritalStatus.NotSpecified)
        {
            if (panelId < 1 || panellistId < 1 || string.IsNullOrEmpty(firstName) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new
            {
                PanelID = panelId,
                PanellistID = panellistId,
                Firstname = firstName,
                Lastname = lastName,
                Title = (int)title,
                Mobilenumber = mobileNumber,
                Dob = dob,
                Website = website,
                Address1 = address1,
                Address2 = address2,
                Postalcode = postalcode,
                CountryCode = countryCode,
                Gender = gender,
                MaritalStatus = martialStatus
            });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/UpdatePanellist", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject GetPanellistByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetPanellistByEmail?email=" + email, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject GetPanelPanellist(long panelId, long panellistId)
        {
            if (panelId < 1 || panellistId < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetPanelPanellist?panelId=" + panelId + "&panellistId=" + panellistId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }
    }
}
