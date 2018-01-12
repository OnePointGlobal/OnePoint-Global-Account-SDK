// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoLocation.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved. 
// </copyright>
// <summary>
//   The geo location, manages geo locatino and address data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.GeoLocation
{
    /// <summary>
    /// The geo location class, provides the porperties for geo location details.
    /// </summary>
    public class GeoLocation
    {
        /// <summary>
        /// Gets or sets the address count.
        /// </summary>
        public int AddressCount { get; set; }

        /// <summary>
        /// Gets or sets the address list id.
        /// </summary>
        public int AddressListId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// The geo address class, provides the porperties for location address data.
    /// </summary>
    public class GeoAddress
    {
        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the survey id.
        /// </summary>
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the survey reference.
        /// </summary>
        public object SurveyReference { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// Gets or sets the survey name.
        /// </summary>
        public object SurveyName { get; set; }

        /// <summary>
        /// Gets or sets the encode address.
        /// </summary>
        public string EncodeAddress { get; set; }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address list id.
        /// </summary>
        public int AddressListId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the geo code.
        /// </summary>
        public string GeoCode { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the user def 1.
        /// </summary>
        public string UserDef1 { get; set; }

        /// <summary>
        /// Gets or sets the user def 2.
        /// </summary>
        public string UserDef2 { get; set; }

        /// <summary>
        /// Gets or sets the user def 3.
        /// </summary>
        public string UserDef3 { get; set; }

        /// <summary>
        /// Gets or sets the user def 4.
        /// </summary>
        public string UserDef4 { get; set; }

        /// <summary>
        /// Gets or sets the user def 5.
        /// </summary>
        public string UserDef5 { get; set; }

        /// <summary>
        /// Gets or sets the user def 6.
        /// </summary>
        public string UserDef6 { get; set; }

        /// <summary>
        /// Gets or sets the user def 7.
        /// </summary>
        public string UserDef7 { get; set; }

        /// <summary>
        /// Gets or sets the user def 8.
        /// </summary>
        public string UserDef8 { get; set; }

        /// <summary>
        /// Gets or sets the user def 9.
        /// </summary>
        public string UserDef9 { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }
    }

    /// <summary>
    /// The address root class, provides the porperties for GeoAddress, GeoLocationRoute method execution success or failure information.
    /// </summary>
    public class AddressRoot
    {
        /// <summary>
        /// Gets or sets the geo addresses.
        /// </summary>
        public List<GeoAddress> GeoAddresses { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }

    /// <summary>
    /// The geo root class, provides the porperties for GeoLocation, GeoLocationRoute method execution success or failure information.
    /// </summary>
    public class GeoRoot
    {
        /// <summary>
        /// Gets or sets the geo locations.
        /// </summary>
        public List<GeoLocation> GeoLocations { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }

    /// <summary>
    /// The geo fencing class, provides the porperties for geo fencing data.
    /// </summary>
    public class GeoFencing
    {
        /// <summary>
        /// Gets or sets the address count.
        /// </summary>
        public int AddressCount { get; set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// Gets or sets the survey address id.
        /// </summary>
        public int SurveyAddressId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enter event.
        /// </summary>
        public bool EnterEvent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether exit event.
        /// </summary>
        public bool ExitEvent { get; set; }

        /// <summary>
        /// Gets or sets the event time.
        /// </summary>
        public int EventTime { get; set; }

        /// <summary>
        /// Gets or sets the address list id.
        /// </summary>
        public int AddressListId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// The geo fencing root, provides the porperties for GeoFencing, GeoFencingRoute method execution success or failure information.
    /// </summary>
    public class GeoFencingRoot
    {
        /// <summary>
        /// Gets or sets the geo fencing.
        /// </summary>
        public List<GeoFencing> GeoFencing { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}