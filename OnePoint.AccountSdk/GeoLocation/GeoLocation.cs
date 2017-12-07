using System.Collections.Generic;

namespace OnePoint.AccountSdk.GeoLocation
{
    public class GeoLocation
    {
        public int AddressCount { get; set; }
        public int AddressListId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GeoAddress
    {
        public double Distance { get; set; }
        public int SurveyId { get; set; }
        public object SurveyReference { get; set; }
        public int Range { get; set; }
        public object SurveyName { get; set; }
        public string EncodeAddress { get; set; }
        public int AddressId { get; set; }
        public int AddressListId { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GeoCode { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public string UserDef1 { get; set; }
        public string UserDef2 { get; set; }
        public string UserDef3 { get; set; }
        public string UserDef4 { get; set; }
        public string UserDef5 { get; set; }
        public string UserDef6 { get; set; }
        public string UserDef7 { get; set; }
        public string UserDef8 { get; set; }
        public string UserDef9 { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class AddressRoot
    {
        public List<GeoAddress> GeoAddresses { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class GeoRoot
    {
        public List<GeoLocation> GeoLocations { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class GeoFencing
    {
        public int AddressCount { get; set; }
        public int Range { get; set; }
        public int SurveyAddressId { get; set; }
        public bool EnterEvent { get; set; }
        public bool ExitEvent { get; set; }
        public int EventTime { get; set; }
        public int AddressListId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GeoFencingRoot
    {
        public List<GeoFencing> GeoFencing { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
