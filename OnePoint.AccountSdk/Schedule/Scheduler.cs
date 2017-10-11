using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Schedule
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Medium { get; set; }
        public string JsonContent { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public int RefID { get; set; }
        public int UserID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
    }

    public class Scheduler
    {
        public int JobDetailID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int JobID { get; set; }
        public int JobType { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

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

    public class EmailServer
    {
        public int EmailServerID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NameFrom { get; set; }
        public string AddressFrom { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public bool IsDefault { get; set; }
    }

    public class TriggerDetailExtension
    {
        public int TriggerDetailExtensionID { get; set; }
        public int TriggerDetailID { get; set; }
        public int TriggerType { get; set; }
        public int Every { get; set; }
        public bool EverySpecified { get; set; }
        public bool HourSpecified { get; set; }
        public bool MinuteSpecified { get; set; }
        public string WeekDays { get; set; }
        public bool DaySpecified { get; set; }
        public bool OccurenceTypeSpecified { get; set; }
        public bool MonthSpecified { get; set; }
        public int EveryAt { get; set; }
        public bool EveryAtSpecified { get; set; }
    }

    public class TriggerContents
    {
        public string Start { get; set; }
        public string End { get; set; }
        public bool Live { get; set; }
        public TriggerDetailExtension TriggerDetailExtension { get; set; }
    }

    public class EmailContent
    {
        public int Templateid { get; set; }
        public string Subject { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public int EmailServer { get; set; }
    }

    public class ScedulerEmailDetails
    {
        public int NotificationID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Medium { get; set; }
        public string JsonContent { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public bool SubTypeSpecified { get; set; }
        public int RefID { get; set; }
        public int UserID { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
    }

    public class EmailContentRoot
    {
        public List<EmailTemplate> EmailTemplates { get; set; }
        public List<EmailServer> EmailServers { get; set; }
        public List<string> SpecialFields { get; set; }
        public EmailContent EmailContent { get; set; }
        public TriggerContents TriggerContents { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class SchedulerRootObject
    {
        public List<Scheduler> Schedules { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class SchedulerEmailDetails
    {
        public Notification Notification { get; set; }
        public EmailContent EmailContent { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
