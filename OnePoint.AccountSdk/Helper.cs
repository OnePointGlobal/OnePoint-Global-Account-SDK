using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OnePoint.AccountSdk
{
    public enum SurveyType
    {
        SMS = 1,
        Web = 2,
        App = 3
    }

    public enum EmailType
    {
        Invitation = 1,
        Reminder = 2,
        Other = 3
    }

    public class HttpError
    {
        public string ErrorMessage { get; set; }

        public string HttpStatusCode { get; set; }
    }

    public enum RouteStyle
    {
        Rpc,

        Download,

        Upload
    }

    public enum SentStatuses
    {
        Sent = 1,

        Delivered = 2,

        Read = 3,

        AppVisited = 4,

        AppAndroidDownloaded = 5,

        AppIphoneDownloaded = 6,

        AppsiteOpened = 7,

        NotDelivered = 8,


    }

    public enum PanelType
    {
        Normal = 1,
        CAPI = 2
    }

    public enum ProfileElementType
    {
        Text = 1,
        Float = 2,
        Date = 3,
        Object = 4,
        Long = 5
    }

    public enum Title
    {
        Mr = 1,
        Ms = 2,
        Mrs = 3
    }

    public enum Gender
    {
        Male = 1,
        Female = 2,
        NotSpecified = 3
    }

    public enum MaritalStatus
    {
        Single = 1,
        Married = 2,
        Divorced = 3,
        NotSpecified = 4
    }

    public enum SampleOperation
    {
        And = 1,
        Or = 2
    }

    public enum SampleOperator
    {
        Equal = 1,
        Contains = 2
    }

    public enum SurveyNotificationJobType
    {
        InvitationSent = 1,
        InvitationNotSent = 2,
        InvitationDelivered = 3,
        InvitationNotDelivered = 4,
        InvitationRead = 5,
        InvitationNotRead = 6,
        ReminderSent = 7,
        ReminderNotSent = 8,
        ReminderDelivered = 9,
        ReminderNotDelivered = 10,
        ReminderRead = 11,
        ReminderNotRead = 12,
        OtherSent = 13,
        OtherNotSent = 14,
        OtherDelivered = 15,
        OtherNotDelivered = 16,
        OtherRead = 17,
        OtherNotRead = 18,
        AppSignedIn = 19,
        AppNotSignedIn = 20,
        AppVisited = 21,
        AppNotVisited = 22,
    }

    public enum TriggerType
    {
        Minutes = 1,
        Hourly = 2,
        Daily = 3,
        Weekly = 4,
        Monthly = 5,
        Yearly = 6
    }

    public enum TimePeriodType
    {
        Every = 1,
        At = 2
    }

    public enum WeekDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum OccurenceType
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5
    }

    public enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public enum ThemeType
    {
        App = 1,
        Adminsuite = 2
    }


    public static class Helper
    {
        /// <summary>
        /// The assembly directory folder.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Copy a file to assembly directory.
        /// </summary>
        /// <param name="source">The source file path to be moved.</param>
        /// <param name="filename">The name of the file.</param>
        /// <returns></returns>
        public static string MoveFile(string source, string filename)
        {
            string extension = Path.GetExtension(source);
            var destnationfile = AssemblyDirectory + "\\" + filename + extension;
            if (File.Exists(destnationfile))
            {
                File.Delete(destnationfile);
            }
            File.Copy(source, destnationfile);
            return destnationfile;
        }

    }
}
