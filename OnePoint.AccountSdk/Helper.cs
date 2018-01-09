// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="OnePoint Global Ltd">
//  Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.  
// </copyright>
// <summary>
//   The enum types, manages enumuration used in entire application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace OnePoint.AccountSdk
{
    /// <summary>
    /// The survey type, indicates types of survey.
    /// </summary>
    public enum SurveyType
    {
        /// <summary>
        /// The sms.
        /// </summary>
        SMS = 1,

        /// <summary>
        /// The web.
        /// </summary>
        Web = 2,

        /// <summary>
        /// The app.
        /// </summary>
        App = 3
    }

    /// <summary>
    /// The email type, indicates types of email.
    /// </summary>
    public enum EmailType
    {
        /// <summary>
        /// The invitation.
        /// </summary>
        Invitation = 1,

        /// <summary>
        /// The reminder.
        /// </summary>
        Reminder = 2,

        /// <summary>
        /// The other.
        /// </summary>
        Other = 3
    }


    /// <summary>
    /// The route style, indicates http request method types.
    /// </summary>
    public enum RouteStyle
    {
        /// <summary>
        /// The rpc.
        /// </summary>
        Rpc,

        /// <summary>
        /// The download file.
        /// </summary>
        Download,

        /// <summary>
        /// The upload file.
        /// </summary>
        Upload
    }

    /// <summary>
    /// The sent statuses of panellist email and its events.
    /// </summary>
    public enum SentStatuses
    {
        /// <summary>
        /// The sent.
        /// </summary>
        Sent = 1,

        /// <summary>
        /// The delivered.
        /// </summary>
        Delivered = 2,

        /// <summary>
        /// The read.
        /// </summary>
        Read = 3,

        /// <summary>
        /// The app visited.
        /// </summary>
        AppVisited = 4,

        /// <summary>
        /// The app android downloaded.
        /// </summary>
        AppAndroidDownloaded = 5,

        /// <summary>
        /// The app iphone downloaded.
        /// </summary>
        AppIphoneDownloaded = 6,

        /// <summary>
        /// The appsite opened.
        /// </summary>
        AppsiteOpened = 7,

        /// <summary>
        /// The not delivered.
        /// </summary>
        NotDelivered = 8,
    }

    /// <summary>
    /// The panel type.
    /// </summary>
    public enum PanelType
    {
        /// <summary>
        /// The normal.
        /// </summary>
        Normal = 1,

        /// <summary>
        /// The capi.
        /// </summary>
        CAPI = 2
    }

    /// <summary>
    /// The panellist profile element data type.
    /// </summary>
    public enum ProfileElementType
    {
        /// <summary>
        /// The text.
        /// </summary>
        Text = 1,

        /// <summary>
        /// The float.
        /// </summary>
        Float = 2,

        /// <summary>
        /// The date.
        /// </summary>
        Date = 3,

        /// <summary>
        /// The object.
        /// </summary>
        Object = 4,

        /// <summary>
        /// The long.
        /// </summary>
        Long = 5
    }

    /// <summary>
    /// The panellist title.
    /// </summary>
    public enum Title
    {
        /// <summary>
        /// The mr.
        /// </summary>
        Mr = 1,

        /// <summary>
        /// The ms.
        /// </summary>
        Ms = 2,

        /// <summary>
        /// The mrs.
        /// </summary>
        Mrs = 3
    }

    /// <summary>
    /// The panellist gender.
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// The male.
        /// </summary>
        Male = 1,

        /// <summary>
        /// The female.
        /// </summary>
        Female = 2,

        /// <summary>
        /// The not specified.
        /// </summary>
        NotSpecified = 3
    }

    /// <summary>
    /// The panellist marital status.
    /// </summary>
    public enum MaritalStatus
    {
        /// <summary>
        /// The single.
        /// </summary>
        Single = 1,

        /// <summary>
        /// The married.
        /// </summary>
        Married = 2,

        /// <summary>
        /// The divorced.
        /// </summary>
        Divorced = 3,

        /// <summary>
        /// The not specified.
        /// </summary>
        NotSpecified = 4
    }

    /// <summary>
    /// The sample filter operation types.
    /// </summary>
    public enum SampleOperation
    {
        /// <summary>
        /// The and.
        /// </summary>
        And = 1,

        /// <summary>
        /// The or.
        /// </summary>
        Or = 2
    }

    /// <summary>
    /// The sample filter operator types.
    /// </summary>
    public enum SampleOperator
    {
        /// <summary>
        /// The equal.
        /// </summary>
        Equal = 5,

        /// <summary>
        /// The contains.
        /// </summary>
        Contains = 6
    }

    /// <summary>
    /// The survey notification job type.
    /// </summary>
    public enum SurveyNotificationJobType
    {
        /// <summary>
        /// The invitation sent.
        /// </summary>
        InvitationSent = 1,

        /// <summary>
        /// The invitation not sent.
        /// </summary>
        InvitationNotSent = 2,

        /// <summary>
        /// The invitation delivered.
        /// </summary>
        InvitationDelivered = 3,

        /// <summary>
        /// The invitation not delivered.
        /// </summary>
        InvitationNotDelivered = 4,

        /// <summary>
        /// The invitation read.
        /// </summary>
        InvitationRead = 5,

        /// <summary>
        /// The invitation not read.
        /// </summary>
        InvitationNotRead = 6,

        /// <summary>
        /// The app signed in.
        /// </summary>
        AppSignedIn = 19,

        /// <summary>
        /// The app not signed in.
        /// </summary>
        AppNotSignedIn = 20,

        /// <summary>
        /// The app visited.
        /// </summary>
        AppVisited = 21,

        /// <summary>
        /// The app not visited.
        /// </summary>
        AppNotVisited = 22,

        /// <summary>
        /// The blocked.
        /// </summary>
        Blocked = 31,

        /// <summary>
        /// The un blocked.
        /// </summary>
        UnBlocked = 32
    }

    /// <summary>
    /// The trigger types of a scheduler.
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// The minutes.
        /// </summary>
        Once = 1,

        /// <summary>
        /// The hourly.
        /// </summary>
        Daily = 2,

        /// <summary>
        /// The daily.
        /// </summary>
        Weekly = 3,

        /// <summary>
        /// The weekly.
        /// </summary>
        Monthly = 4
    }

    /// <summary>
    /// The scheduler time period type.
    /// </summary>
    public enum TimePeriodType
    {
        /// <summary>
        /// The every.
        /// </summary>
        Every = 1,

        /// <summary>
        /// The at.
        /// </summary>
        At = 2
    }

    /// <summary>
    /// The scheduler week days.
    /// </summary>
    public enum WeekDays
    {
        /// <summary>
        /// The monday.
        /// </summary>
        Monday,

        /// <summary>
        /// The tuesday.
        /// </summary>
        Tuesday,

        /// <summary>
        /// The wednesday.
        /// </summary>
        Wednesday,

        /// <summary>
        /// The thursday.
        /// </summary>
        Thursday,

        /// <summary>
        /// The friday.
        /// </summary>
        Friday,

        /// <summary>
        /// The saturday.
        /// </summary>
        Saturday,

        /// <summary>
        /// The sunday.
        /// </summary>
        Sunday
    }

    /// <summary>
    /// The scheduler occurence type.
    /// </summary>
    public enum OccurenceType
    {
        /// <summary>
        /// The first.
        /// </summary>
        First = 1,

        /// <summary>
        /// The second.
        /// </summary>
        Second = 2,

        /// <summary>
        /// The third.
        /// </summary>
        Third = 3,

        /// <summary>
        /// The fourth.
        /// </summary>
        Fourth = 4,

        /// <summary>
        /// The fifth.
        /// </summary>
        Fifth = 5
    }

    /// <summary>
    /// The scheduler months.
    /// </summary>
    public enum Months
    {
        /// <summary>
        /// The january.
        /// </summary>
        January = 1,

        /// <summary>
        /// The february.
        /// </summary>
        February = 2,

        /// <summary>
        /// The march.
        /// </summary>
        March = 3,

        /// <summary>
        /// The april.
        /// </summary>
        April = 4,

        /// <summary>
        /// The may.
        /// </summary>
        May = 5,

        /// <summary>
        /// The june.
        /// </summary>
        June = 6,

        /// <summary>
        /// The july.
        /// </summary>
        July = 7,

        /// <summary>
        /// The august.
        /// </summary>
        August = 8,

        /// <summary>
        /// The september.
        /// </summary>
        September = 9,

        /// <summary>
        /// The october.
        /// </summary>
        October = 10,

        /// <summary>
        /// The november.
        /// </summary>
        November = 11,

        /// <summary>
        /// The december.
        /// </summary>
        December = 12
    }

    /// <summary>
    /// The theme type.
    /// </summary>
    public enum ThemeType
    {
        /// <summary>
        /// The app.
        /// </summary>
        App = 1,

        /// <summary>
        /// The adminsuite.
        /// </summary>
        Adminsuite = 2
    }

    /// <summary>
    /// The email template type.
    /// </summary>
    public enum EmailTemplateType
    {
        /// <summary>
        /// The email.
        /// </summary>
        Email = 0,

        /// <summary>
        /// The panellist password reset.
        /// </summary>
        PanellistPasswordReset = 1
    }

    /// <summary>
    /// The notification medium.
    /// </summary>
    public enum NotificationMedium
    {
        /// <summary>
        /// The email
        /// </summary>
        Email = 1,

        /// <summary>
        /// The sms.
        /// </summary>
        Sms = 2
    }

    /// <summary>
    /// The helper, provides code to perform common operations.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        ///     The assembly directory folder.
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
        /// <param name="source">
        /// The source file path to be moved.
        /// </param>
        /// <param name="filename">
        /// The name of the file.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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

    /// <summary>
    /// The http error.
    /// </summary>
    public class HttpError
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the http status code.
        /// </summary>
        public string HttpStatusCode { get; set; }
    }
}