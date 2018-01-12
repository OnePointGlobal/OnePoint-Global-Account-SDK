// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailRoute.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The email route. Manages email template data and sending emails to panellist.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Email
{
    /// <summary>
    /// The email route class, provides the code for CRUD operation on email templates and sending email to panellist.
    /// </summary>
    public class EmailRoute
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
        /// Initializes a new instance of the <see cref="EmailRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public EmailRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get email templates details.
        /// </summary>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot GetEmailTemplates()
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/GetTemplates",
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The add new email template.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="emailContent">
        /// The email content.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot AddEmailTemplate(string name, string description, string subject, string emailContent)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(subject)
                || string.IsNullOrEmpty(emailContent))
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        Name = name,
                        Description = description,
                        EmailContent = emailContent,
                        Subject = subject,
                        EmailTemplateType = (short)EmailTemplateType.Email
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/AddTemplate",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The add panellist reset password template.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="emailContent">
        /// The email content.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot AddPanellistResetPasswordTemplate(
            string name,
            string description,
            string subject,
            string emailContent)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(subject)
                || string.IsNullOrEmpty(emailContent))
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        Name = name,
                        Description = description,
                        EmailContent = emailContent,
                        Subject = subject,
                        EmailTemplateType = (short)EmailTemplateType.PanellistPasswordReset
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/AddTemplate",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The copy existing email template.
        /// </summary>
        /// <param name="emailTemplateId">
        /// The email template id.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot CopyEmailTemplate(long emailTemplateId)
        {
            if (emailTemplateId < 1)
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/CopyTemplate?emailTemplateId=" + emailTemplateId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The delete email templates.
        /// </summary>
        /// <param name="emailTemplateIds">
        /// The email template ids.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot DeleteEmailTemplates(List<long> emailTemplateIds)
        {
            if (emailTemplateIds.Count < 1)
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
                
            }

            var requestArg = JsonConvert.SerializeObject(new { TemplateIDs = string.Join(",", emailTemplateIds) });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/DeleteTemplates",
                HttpMethod.Delete,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The update email template details.
        /// </summary>
        /// <param name="emailTemplateId">
        /// The email template id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="emailContent">
        /// The email content.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot UpdateEmailTemplate(
            long emailTemplateId,
            string name,
            string description,
            string subject,
            string emailContent)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(subject)
                || string.IsNullOrEmpty(emailContent))
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        EmailTemplateID = emailTemplateId,
                        Name = name,
                        EditDescription = description,
                        EmailContent = emailContent,
                        Subject = subject
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/UpdateTemplate",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The send email to a panellists by email type.
        /// </summary>
        /// <param name="emailIds">
        /// The email ids.
        /// </param>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="emailType">
        /// The email type.
        /// </param>
        /// <param name="emailSubject">
        /// The email subject.
        /// </param>
        /// <param name="emailBody">
        /// The email body.
        /// </param>
        /// <param name="emailServerId">
        /// The email server id.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot SendEmail(
            List<string> emailIds,
            long surveyId,
            EmailType emailType,
            string emailSubject,
            string emailBody,
            long emailServerId = 0)
        {
            if (emailIds.Count < 1 || string.IsNullOrEmpty(emailSubject) || string.IsNullOrEmpty(emailBody))
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            var ids = string.Join(",", emailIds);

            var requestArg = JsonConvert.SerializeObject(
                new
                    {
                        SurveyID = surveyId,
                        EmailServerId = emailServerId,
                        EmailAddress = ids,
                        EmailType = emailType,
                        Subject = emailSubject,
                        EmailBody = emailBody
                    });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/SendEmail",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The email to a panellist.
        /// </summary>
        /// <param name="panelId">
        /// The panel id.
        /// </param>
        /// <param name="panellistId">
        /// The panellist id.
        /// </param>
        /// <param name="emailSubject">
        /// The email subject.
        /// </param>
        /// <param name="emailBody">
        /// The email body.
        /// </param>
        /// <returns>
        /// The <see cref="EmailRoot"/>.
        /// </returns>
        public EmailRoot EmailPanellist(long panelId, long panellistId, string emailSubject, string emailBody)
        {
            if (string.IsNullOrEmpty(emailSubject) || string.IsNullOrEmpty(emailBody))
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            if (panelId < 1 || panellistId < 1)
            {
                return _result.ErrorToObject(new EmailRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { panelID = panelId, panellistID = panellistId, subject = emailSubject, body = emailBody });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/EmailPanellist",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new EmailRoot(), "EmailTemplates");
        }

        /// <summary>
        /// The get special filds for panellist email macro.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="PanellistProfileFields"/>.
        /// </returns>
        public PanellistProfileFields GetSpecialFilds(long surveyId = 0)
        {
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserEmail/GetSpecialFields?surveyID=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();
            return x.Result.JsonToObject(new PanellistProfileFields());
        }
    }
}