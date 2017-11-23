using System.Collections.Generic;


namespace OnePoint.AccountSdk.Theme
{
    public class Theme
    {
        public int ThemeTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class ThemeList
    {
        public object MediaUrl { get; set; }
        public int ThemeID { get; set; }
        public int ThemeTemplateId { get; set; }
        public int ThemeElementTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class ThemeRoot
    {
        public List<Theme> Themes { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ThemeListRoot
    {
        public List<ThemeList> ThemeList { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
