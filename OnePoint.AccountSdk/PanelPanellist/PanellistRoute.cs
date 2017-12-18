// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PanellistRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The panel panellist, manages user panel and panellsit data.
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
    /// The panellist route class,  provides the code for CRUD operation on panel and panellist data, importing panellist file.
    /// </summary>
    public class PanellistRoute
    {
        /// <summary>
        /// Gets or sets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; set; }

        /// <summary>
        /// The result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        /// <summary>
        /// Initializes a new instance of the <see cref="PanellistRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public PanellistRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get panel panellists.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject GetPanelPanellist(long panelId)
        {
            if (panelId < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/GetPanellists?panelID=" + panelId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The delete panellist from a panel.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="panellistIds">
        /// The panellist ids.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject DeletePanellist(long panelId, List<long> panellistIds)
        {
            if (panelId < 1 || panellistIds.Count < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { PanelID = panelId, PanellistIDs = string.Join(",", panellistIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/DeletePanellists?panelID=" + panelId,
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The upload/add panllists details file to a panel.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject UploadPanllistFile(long panelId, string filePath)
        {
            if (panelId < 1 || !File.Exists(filePath))
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/FileUpload?panelId=" + panelId,
                HttpMethod.Post,
                RouteStyle.Upload,
                filePath);
            x.Wait();

            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The add a new panellist to panel.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="mobileNumber">
        /// The mobile number.
        /// </param>
        /// <param name="dob">
        /// The date of birth.
        /// </param>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <param name="address1">
        /// The address 1.
        /// </param>
        /// <param name="address2">
        /// The address 2.
        /// </param>
        /// <param name="postalcode">
        /// The postalcode.
        /// </param>
        /// <param name="website">
        /// The website.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="gender">
        /// The gender.
        /// </param>
        /// <param name="martialStatus">
        /// The martial status.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject AddPanellist(
            long panelId,
            string firstName,
            string lastName,
            string email,
            long mobileNumber,
            DateTime dob,
            int countryCode,
            string address1 = "",
            string address2 = "",
            string postalcode = "",
            string website = "",
            Title title = Title.Mr,
            Gender gender = Gender.NotSpecified,
            MaritalStatus martialStatus = MaritalStatus.NotSpecified)
        {
            if (panelId < 1 || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
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

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/InsertPanellist",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The update panel panellist details.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="panellistId">
        /// The panellist id.
        /// </param>
        /// <param name="firstName">
        /// The first name.
        /// </param>
        /// <param name="lastName">
        /// The last name.
        /// </param>
        /// <param name="mobileNumber">
        /// The mobile number.
        /// </param>
        /// <param name="dob">
        /// The dob.
        /// </param>
        /// <param name="countryCode">
        /// The country code.
        /// </param>
        /// <param name="address1">
        /// The address 1.
        /// </param>
        /// <param name="address2">
        /// The address 2.
        /// </param>
        /// <param name="postalcode">
        /// The postalcode.
        /// </param>
        /// <param name="website">
        /// The website.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="gender">
        /// The gender.
        /// </param>
        /// <param name="martialStatus">
        /// The martial status.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject UpdatePanellist(
            long panelId,
            long panellistId,
            string firstName,
            string lastName,
            long mobileNumber,
            DateTime dob,
            int countryCode,
            string address1 = "",
            string address2 = "",
            string postalcode = "",
            string website = "",
            Title title = Title.Mr,
            Gender gender = Gender.NotSpecified,
            MaritalStatus martialStatus = MaritalStatus.NotSpecified)
        {
            if (panelId < 1 || panellistId < 1 || string.IsNullOrEmpty(firstName) || mobileNumber < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
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

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/UpdatePanellist",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The get panellist details by email id.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject GetPanellistByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/GetPanellistByEmail?email=" + email,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }

        /// <summary>
        /// The get panel panellist details.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="panellistId">
        /// The panellist id.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistRootObject"/>.
        /// </returns>
        public PanellistRootObject GetPanelPanellist(long panelId, long panellistId)
        {
            if (panelId < 1 || panellistId < 1)
            {
                return result.ErrorToObject(new PanellistRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserPanelPanellists/GetPanelPanellist?panelId=" + panelId + "&panellistId=" + panellistId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistRootObject(), "Panellist");
        }
    }
}