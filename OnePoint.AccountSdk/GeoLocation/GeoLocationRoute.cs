// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoLocationRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The geo location route. Manages geo location and address data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OnePoint.AccountSdk.GeoLocation
{
    /// <summary>
    /// The geo location route class,  provides the code for CRUD operation on Geo location and its address, importing address data file.
    /// </summary>
    public class GeoLocationRoute
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
        /// Initializes a new instance of the <see cref="GeoLocationRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public GeoLocationRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get the user geo locations list.
        /// </summary>
        /// <returns>
        /// The <see cref="GeoRoot"/>.
        /// </returns>
        public GeoRoot GetGeoLocationList()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/GetAllGeolocationList",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new GeoRoot(), "GeoLocations");
        }

        /// <summary>
        /// The add new geo location list.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="GeoRoot"/>.
        /// </returns>
        public GeoRoot AddGeoLocationList(string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new GeoRoot(), "Invalid parameter(s)");
            }

            var requestArg =
                JsonConvert.SerializeObject(new { GeolocationName = name, GeolocationDescription = description });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/AddGeolocationList",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoRoot(), "GeoLocations");
        }

        /// <summary>
        /// The delete geo locations.
        /// </summary>
        /// <param name="addressListIds">
        /// The address list ids.
        /// </param>
        /// <returns>
        /// The <see cref="GeoRoot"/>.
        /// </returns>
        public GeoRoot DeleteGeoLocations(List<long> addressListIds)
        {
            if (addressListIds.Count < 1)
            {
                return _result.ErrorToObject(new GeoRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { AddressListIds = string.Join(",", addressListIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/DeleteGeolocationList",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoRoot(), "GeoLocations");
        }

        /// <summary>
        /// The update geo lcoation list details.
        /// </summary>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="GeoRoot"/>.
        /// </returns>
        public GeoRoot UpdateGeoLcoationList(int addressListId, string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) | addressListId < 1)
            {
                return _result.ErrorToObject(new GeoRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        EditGeolocationName = name,
                        EditGeolocationDescription = description,
                        AddressListID = addressListId
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/UpdateGeolocationList",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new GeoRoot(), "GeoLocations");
        }

        /// <summary>
        /// The get  all geo location addresses.
        /// </summary>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <returns>
        /// The <see cref="AddressRoot"/>.
        /// </returns>
        public AddressRoot GetGeoLocationAddresses(long addressListId)
        {
            if (addressListId < 1)
            {
                return _result.ErrorToObject(new AddressRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/GetAllAddresses?AddressListId=" + addressListId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new AddressRoot(), "GeoAddresses");
        }

        /// <summary>
        /// The delete address from geo location list.
        /// </summary>
        /// <param name="addressIds">
        /// The address ids.
        /// </param>
        /// <returns>
        /// The <see cref="AddressRoot"/>.
        /// </returns>
        public AddressRoot DeleteAddress(List<long> addressIds)
        {
            if (addressIds.Count < 1)
            {
                return _result.ErrorToObject(new AddressRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { AddressIds = string.Join(",", addressIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/DeleteAddresses",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new AddressRoot(), "GeoAddresses");
        }

        /// <summary>
        /// The add address to geo location list.
        /// </summary>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <param name="fullAddress">
        /// The full address.
        /// </param>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <returns>
        /// The <see cref="AddressRoot"/>.
        /// </returns>
        public AddressRoot AddAddress(long addressListId, string fullAddress, double latitude = 0, double longitude = 0)
        {
            if (addressListId < 1 || string.IsNullOrEmpty(fullAddress))
            {
                return _result.ErrorToObject(new AddressRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        AddresslistId = addressListId,
                        AddressName = fullAddress,
                        Latitude = latitude,
                        Longitude = longitude
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/AddAddressLatiLong",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();
            return x.Result.JsonToObject(new AddressRoot(), "GeoAddresses");
        }

        /// <summary>
        /// The update address details.
        /// </summary>
        /// <param name="addressId">
        /// The address id.
        /// </param>
        /// <param name="fullAddress">
        /// The full address.
        /// </param>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <returns>
        /// The <see cref="AddressRoot"/>.
        /// </returns>
        public AddressRoot UpdateAddress(long addressId, string fullAddress, double latitude = 0, double longitude = 0)
        {
            if (string.IsNullOrEmpty(fullAddress) || addressId < 1)
            {
                return _result.ErrorToObject(new AddressRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { AddressId = addressId, AddressName = fullAddress, Latitude = latitude, Longitude = longitude });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/UpdateAddressList",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new AddressRoot(), "GeoAddresses");
        }

        /// <summary>
        /// The upload address data excel file.
        /// </summary>
        /// <param name="geoLocationListId">
        /// The geo location list id.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="AddressRoot"/>.
        /// </returns>
        public AddressRoot UploadAddress(long geoLocationListId, string filePath)
        {
            if (geoLocationListId < 1 || !File.Exists(filePath))
            {
                return _result.ErrorToObject(new AddressRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/ImportFileGeolocation?addressListID=" + geoLocationListId,
                HttpMethod.Post,
                RouteStyle.Upload,
                filePath);
            x.Wait();

            return x.Result.JsonToObject(new AddressRoot(), "GeoAddresses");
        }

        /// <summary>
        /// The download address excel file.
        /// </summary>
        /// <param name="addressListId">
        /// The address list id.
        /// </param>
        /// <param name="downloadFolderPath">
        /// The download folder path.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public byte[] DownloadAddress(long addressListId, string downloadFolderPath)
        {
            if (addressListId < 1 || !Directory.Exists(downloadFolderPath))
            {
                return null;
            }

            var requestArg = JsonConvert.SerializeObject(new { addresslistid = addressListId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserGeolocation/ExportFileGeolocation",
                HttpMethod.Post,
                RouteStyle.Download,
                requestArg);
            x.Wait();

            x.Result.DownloadFile(downloadFolderPath + addressListId + "_GeoLocations.xlsx");
            return x.Result.HttpResponse.Content.ReadAsByteArrayAsync().Result;
        }
    }
}