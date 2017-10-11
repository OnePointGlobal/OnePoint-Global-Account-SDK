using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OnePoint.AccountSdk.PanelPanellist
{
    public class PanellistRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public PanellistRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public PanellistRootObject GetPanelPanellist(long panelId)
        {
            if (panelId < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/GetPanellists?panelID=" + panelId, HttpMethod.Get, RouteStyle.Rpc, null);
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

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/DeletePanellists?panelID=" + panelId, HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");

        }

        public PanellistRootObject UploadPanllistFile(long panelId, string filePath)
        {
            if (panelId < 1 || !File.Exists(filePath))
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/FileUpload?panelId=" + panelId, HttpMethod.Post, RouteStyle.Upload, filePath);
            x.Wait();

            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject AddPanellist(long PanelId, string firstName, string lastName, Title title, string email, long mobileNumber, DateTime dob, string website, Gender gender, string address1, string address2, int postalcode, int countryCode, MaritalStatus martialStatus)
        {
            if (PanelId < 1 || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new
            {
                PanelID = PanelId,
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

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/AddPanellist", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        public PanellistRootObject UpdatePanellist(long PanelId, long panellistId, string firstName, string lastName, Title title, string email, long mobileNumber, DateTime dob, string website, Gender gender, string address1, string address2, int postalcode, int countryCode, MaritalStatus martialStatus)
        {
            if (PanelId < 1 || panellistId < 1 || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new
            {
                PanelID = PanelId,
                PanellistID = panellistId,
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
                MaritalStatus = martialStatus
            });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserPanelPanellists/AddPanellist", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

    }
}
