using System.Collections.Generic;

namespace OnePoint.AccountSdk.Common
{
    class Common
    {
    }

    public class Country
    {
        public string Name { get; set; }
        public string Std { get; set; }
    }

    public class CountryRootObject
    {
        public List<Country> Country { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

}
