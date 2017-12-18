// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scheduler.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The Schedule, manages survey invitation and notification scheduling data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Schedule
{
    /// <summary>
    /// The notification.
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// Gets or sets the notification id.
        /// </summary>
        public int NotificationID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        public int Medium { get; set; }

        /// <summary>
        /// Gets or sets the json content.
        /// </summary>
        public string JsonContent { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the sub type.
        /// </summary>
        public int SubType { get; set; }

        /// <summary>
        /// Gets or sets the ref id.
        /// </summary>
        public int RefID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public string LastUpdated { get; set; }
    }

    /// <summary>
    /// The scheduler.
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        /// Gets or sets the job detail id.
        /// </summary>
        public int JobDetailID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the job id.
        /// </summary>
        public int JobID { get; set; }

        /// <summary>
        /// Gets or sets the job type.
        /// </summary>
        public int JobType { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

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
    /// The email template.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Gets or sets the email template id.
        /// </summary>
        public int EmailTemplateID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email type.
        /// </summary>
        public int EmailType { get; set; }
    }

    /// <summary>
    /// The email server.
    /// </summary>
    public class EmailServer
    {
        /// <summary>
        /// Gets or sets the email server id.
        /// </summary>
        public int EmailServerID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name from.
        /// </summary>
        public string NameFrom { get; set; }

        /// <summary>
        /// Gets or sets the address from.
        /// </summary>
        public string AddressFrom { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is default.
        /// </summary>
        public bool IsDefault { get; set; }
    }

    /// <summary>
    /// The trigger detail extension.
    /// </summary>
    public class TriggerDetailExtension
    {
        /// <summary>
        /// Gets or sets the trigger detail extension id.
        /// </summary>
        public int TriggerDetailExtensionID { get; set; }

        /// <summary>
        /// Gets or sets the trigger detail id.
        /// </summary>
        public int TriggerDetailID { get; set; }

        /// <summary>
        /// Gets or sets the trigger type.
        /// </summary>
        public int TriggerType { get; set; }

        /// <summary>
        /// Gets or sets the every.
        /// </summary>
        public int Every { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether every specified.
        /// </summary>
        public bool EverySpecified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether hour specified.
        /// </summary>
        public bool HourSpecified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether minute specified.
        /// </summary>
        public bool MinuteSpecified { get; set; }

        /// <summary>
        /// Gets or sets the week days.
        /// </summary>
        public string WeekDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether day specified.
        /// </summary>
        public bool DaySpecified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether occurence type specified.
        /// </summary>
        public bool OccurenceTypeSpecified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether month specified.
        /// </summary>
        public bool MonthSpecified { get; set; }

        /// <summary>
        /// Gets or sets the every at.
        /// </summary>
        public int EveryAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether every at specified.
        /// </summary>
        public bool EveryAtSpecified { get; set; }
    }

    /// <summary>
    /// The trigger contents.
    /// </summary>
    public class TriggerContents
    {
        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether live.
        /// </summary>
        public bool Live { get; set; }

        /// <summary>
        /// Gets or sets the trigger detail extension.
        /// </summary>
        public TriggerDetailExtension TriggerDetailExtension { get; set; }
    }

    /// <summary>
    /// The email content.
    /// </summary>
    public class EmailContent
    {
        /// <summary>
        /// Gets or sets the templateid.
        /// </summary>
        public int Templateid { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the email server.
        /// </summary>
        public int EmailServer { get; set; }
    }

    /// <summary>
    /// The sceduler email details.
    /// </summary>
    public class ScedulerEmailDetails
    {
        /// <summary>
        /// Gets or sets the notification id.
        /// </summary>
        public int NotificationID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the medium.
        /// </summary>
        public int Medium { get; set; }

        /// <summary>
        /// Gets or sets the json content.
        /// </summary>
        public string JsonContent { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the sub type.
        /// </summary>
        public int SubType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sub type specified.
        /// </summary>
        public bool SubTypeSpecified { get; set; }

        /// <summary>
        /// Gets or sets the ref id.
        /// </summary>
        public int RefID { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public string LastUpdated { get; set; }
    }

    /// <summary>
    /// The email content root.
    /// </summary>
    public class EmailContentRoot
    {
        /// <summary>
        /// Gets or sets the email templates.
        /// </summary>
        public List<EmailTemplate> EmailTemplates { get; set; }

        /// <summary>
        /// Gets or sets the email servers.
        /// </summary>
        public List<EmailServer> EmailServers { get; set; }

        /// <summary>
        /// Gets or sets the special fields.
        /// </summary>
        public List<string> SpecialFields { get; set; }

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        public EmailContent EmailContent { get; set; }

        /// <summary>
        /// Gets or sets the trigger contents.
        /// </summary>
        public TriggerContents TriggerContents { get; set; }

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
    /// The scheduler root object.
    /// </summary>
    public class SchedulerRootObject
    {
        /// <summary>
        /// Gets or sets the schedules.
        /// </summary>
        public List<Scheduler> Schedules { get; set; }

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
    /// The scheduler email details.
    /// </summary>
    public class SchedulerEmailDetails
    {
        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        public Notification Notification { get; set; }

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        public EmailContent EmailContent { get; set; }

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