using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Theme
{
    public class Theme
    {
        public int ThemeTemplateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public int UserID { get; set; }
    }

    public class ThemeList
    {
        public object MediaUrl { get; set; }
        public int ThemeID { get; set; }
        public int ThemeTemplateID { get; set; }
        public int ThemeElementTypeID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class RootObject
    {
        public List<Theme> Themes { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ThemeListRootObject
    {
        public List<ThemeList> ThemeList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
