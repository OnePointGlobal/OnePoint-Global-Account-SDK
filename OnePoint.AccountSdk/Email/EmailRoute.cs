using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Email
{
    public class EmailRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public EmailRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public RootObject GetEmailTemplates()
        {
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/GetTemplates", HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }

        public RootObject AddEmailTemplate(string name, string description, string subject, string emailContent)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(emailContent))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description, EmailContent = emailContent, Subject = subject });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/AddTemplate", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }

        public RootObject CopyEmailTemplate(long emailTemplateId)
        {
            if (emailTemplateId < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/CopyTemplate?emailTemplateId=" + emailTemplateId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();
            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }

        public RootObject DeleteEmailTemplates(List<long> emailTemplateIds)
        {
            if (emailTemplateIds.Count < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)"); ;
            }

            var requestArg = JsonConvert.SerializeObject(new { TemplateIDs = String.Join(",", emailTemplateIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/DeleteTemplates", HttpMethod.Delete, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }

        public RootObject UpdateEmailTemplate(long emailTemplateID, string name, string description, string subject, string emailContent)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(emailContent))
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }


            var requestArg = JsonConvert.SerializeObject(new { EmailTemplateID = emailTemplateID, Name = name, EditDescription = description, EmailContent = emailContent, Subject = subject });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/UpdateTemplate", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }

        public RootObject SendEmail(string emailIds, long surveyId, EmailType emailType, long emailServerId, string subject, string emailContent)
        {
            if (string.IsNullOrEmpty(emailIds) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(emailContent) || emailServerId < 1)
            {
                return result.ErrorToObject(new RootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { SurveyID = surveyId, EmailTemplateID = 0, EmailServerId = emailServerId, EmailAddress = emailIds, EmailType = emailType, Subject = subject, EmailBody = emailContent });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserEmail/SendEmail", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new RootObject(), "EmailTemplates");
        }
    }
}
