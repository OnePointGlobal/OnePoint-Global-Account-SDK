using System.Collections.Generic;

namespace OnePoint.AccountSdk.Email
{
    public class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public int EmailType { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
   }

    public class EmailRoot
    {
        public List<EmailTemplate> EmailTemplates { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class PanellistProfileFields
    {
        public List<string> SpecialFields { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
