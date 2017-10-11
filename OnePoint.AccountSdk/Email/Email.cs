using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Email
{
    public class EmailTemplate
    {
        public int EmailTemplateID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string EmailContent { get; set; }
        public int UserID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public string Subject { get; set; }
        public int EmailType { get; set; }
    }

    public class RootObject
    {
        public List<EmailTemplate> EmailTemplates { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
