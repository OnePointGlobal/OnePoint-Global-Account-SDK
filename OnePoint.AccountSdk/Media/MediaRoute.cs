// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaRoute.cs" company="OnePoint Global Ltd">
//  Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.  
// </copyright>
// <summary>
//   The media route, manages media data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Media
{
    /// <summary>
    /// The media route class,  provides the code for CRUD operation on media data.
    /// </summary>
    public class MediaRoute
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
        /// Initializes a new instance of the <see cref="MediaRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public MediaRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The add new media, img, audio or video.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="MediaRoot"/>.
        /// </returns>
        public MediaRoot AddMedia(string name, string description, string filePath)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                return _result.ErrorToObject(new MediaRoot(), "File does not exist!");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserMedia/AddMedia?Name=" + name + "&Description=" + description,
                HttpMethod.Post,
                RouteStyle.Upload,
                filePath);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        /// <summary>
        /// The get media details.
        /// </summary>
        /// <param name="mediaId">
        /// The media id.
        /// </param>
        /// <returns>
        /// The <see cref="MediaRoot"/>.
        /// </returns>
        public MediaRoot GetMedia(long mediaId)
        {
            if (mediaId < 1)
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserMedia/GetMediaDetails?mediaId=" + mediaId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        /// <summary>
        /// The get all media of a account.
        /// </summary>
        /// <returns>
        /// The <see cref="MediaRoot"/>.
        /// </returns>
        public MediaRoot GetAllMedia()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserMedia/GetMedia",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        /// <summary>
        /// The update media and details.
        /// </summary>
        /// <param name="mediaId">
        /// The media id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="MediaRoot"/>.
        /// </returns>
        public MediaRoot UpdateMedia(long mediaId, string name, string description, string filePath = "")
        {
            if (mediaId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            if (!string.IsNullOrEmpty(filePath) && !File.Exists(filePath))
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid filepath!");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserMedia/UpdateMedia?mediaID=" + mediaId + "&Name=" + name + "&Description=" + description,
                HttpMethod.Put,
                RouteStyle.Upload,
                filePath);
            x.Wait();
            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }

        /// <summary>
        /// The delete media.
        /// </summary>
        /// <param name="mediaId">
        /// The media id.
        /// </param>
        /// <returns>
        /// The <see cref="MediaRoot"/>.
        /// </returns>
        public MediaRoot DeleteMedia(List<long> mediaId)
        {
            if (mediaId.Count < 1)
            {
                return _result.ErrorToObject(new MediaRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { deleteMediaIDs = string.Join(",", mediaId) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserMedia/DeleteMedia",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new MediaRoot(), "Media");
        }
    }
}