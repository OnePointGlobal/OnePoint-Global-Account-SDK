// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SchedulerRoute.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved. 
// </copyright>
// <summary>
//   The Schedule, manages survey invitation and notification scheduling data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Schedule
{
    /// <summary>
    /// The scheduler route class, provides the code for CRUD operation on survey invitation and notification data.
    /// This class has all required methods to set up survey schedule by sms or email medium.
    /// </summary>
    public class SchedulerRoute
    {
        /// <summary>
        /// The result.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Result _result = new Result();

        /// <summary>
        /// Gets or sets the request handler.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private AdminRequestHandler RequestHandler { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerRoute"/> class.
        /// </summary>
        /// <param name="hanlder">
        /// The hanlder.
        /// </param>
        public SchedulerRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// The get list of survey schedules.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot GetSchedules(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/GetScheduledList?surveyID=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The get schedule email details.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="notificationId">
        /// The notification id.
        /// </param>
        /// <returns>
        /// The <see cref="EmailContentRoot"/>.
        /// </returns>
        public EmailContentRoot GetScheduleDetails(long surveyId, long notificationId = 0)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new EmailContentRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/GetSheduleContent?surveyID=" + surveyId + "&notificationID=" + notificationId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new EmailContentRoot(), string.Empty);
        }

        /// <summary>
        /// The add new schedule to a survey.
        /// </summary>
        /// <param name="surveyId">
        /// The survey id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot AddSchedule(long surveyId, string name, string description, NotificationMedium medium)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { ScheduleName = name, ScheduleDescription = description, SurveyID = surveyId, Medium = medium });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/AddNewSchedule",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The update survey schedule.
        /// </summary>
        /// <param name="notificationID">
        /// The notification id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot UpdateSchedule(long notificationID, string name, string description)
        {
            if (notificationID < 1)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { Name = name, Description = description, NotificationID = notificationID });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/EditSchedule",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The delete scedules.
        /// </summary>
        /// <param name="jobIDs">
        /// The job i ds.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot DeleteScedules(List<long> jobIDs)
        {
            if (jobIDs.Count < 1)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/DeleteSchedules?JobIDs=" + string.Join(",", jobIDs),
                HttpMethod.Delete,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule minutes job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="everyMinute">
        /// The every minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleMinutesJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            short everyMinute,
            bool launch = false)
        {
            if (everyMinute < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Minutes,
                    MinuteEvery = everyMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule hourly job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="everyOrAt">
        /// The every or at.
        /// </param>
        /// <param name="everyHour">
        /// The every hour.
        /// </param>
        /// <param name="atHr">
        /// The at hr.
        /// </param>
        /// <param name="atMinute">
        /// The at minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleHourlyJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            TimePeriodType everyOrAt,
            short everyHour,
            short atHr = 0,
            short atMinute = 0,
            bool launch = false)
        {
            if (everyOrAt == TimePeriodType.Every && everyHour < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && (atHr < 0 || atMinute < 0))
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Hourly,
                    HourlyType = (short)everyOrAt,
                    HourEvery = everyHour,
                    HourlyAtHour = atHr,
                    HourlyAtMinute = atMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule daily job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="everyOrAt">
        /// The every or at.
        /// </param>
        /// <param name="everyDays">
        /// The every days.
        /// </param>
        /// <param name="atEveryDayHr">
        /// The at every day hr.
        /// </param>
        /// <param name="atEveryDayMinute">
        /// The at every day minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleDailyJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            TimePeriodType everyOrAt,
            short everyDays,
            short atEveryDayHr = 0,
            short atEveryDayMinute = 0,
            bool launch = false)
        {
            if (everyOrAt == TimePeriodType.Every && everyDays < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && (atEveryDayHr < 0 || atEveryDayMinute < 0))
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Daily,
                    DailyType = (short)everyOrAt,
                    DailyEvery = everyDays,
                    DailyTabHour = atEveryDayHr,
                    DailyTabMinute = atEveryDayMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule weekly job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="day">
        /// The day.
        /// </param>
        /// <param name="atHr">
        /// The at hr.
        /// </param>
        /// <param name="atMinute">
        /// The at minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleWeeklyJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            WeekDays day,
            short atHr = 0,
            short atMinute = 0,
            bool launch = false)
        {
            if (atHr < 0 || atMinute < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Weekly,
                    Weekly = day.ToString(),
                    WeekTabHour = atHr,
                    WeekTabMinute = atMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule monthly job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="everyOrAt">
        /// The every or at.
        /// </param>
        /// <param name="everyDay">
        /// The every day.
        /// </param>
        /// <param name="everyMonths">
        /// The every months.
        /// </param>
        /// <param name="AtType">
        /// The at type.
        /// </param>
        /// <param name="Atday">
        /// The atday.
        /// </param>
        /// <param name="atMonths">
        /// The at months.
        /// </param>
        /// <param name="monthlyHr">
        /// The monthly hr.
        /// </param>
        /// <param name="monthlyMinute">
        /// The monthly minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleMonthlyJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            TimePeriodType everyOrAt,
            short everyDay = 0,
            short everyMonths = 0,
            OccurenceType AtType = OccurenceType.First,
            WeekDays Atday = WeekDays.Monday,
            short atMonths = 0,
            short monthlyHr = 0,
            short monthlyMinute = 0,
            bool launch = false)
        {
            if (everyOrAt == TimePeriodType.Every && (everyDay < 0 || everyMonths < 0))
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && atMonths < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }
            else if (monthlyHr < 0 || monthlyMinute < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Monthly,
                    MonthlyType = everyOrAt,
                    MonthlyEveryDay = everyDay,
                    MonthlyEvery = everyMonths,
                    MonthlyAtOccurenceType = AtType,
                    MonthlyAtDaySelected = Atday,
                    MonthlyAtEvery = atMonths,
                    MonthTabHour = monthlyHr,
                    MonthTabMinute = monthlyMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The schedule yearly job.
        /// </summary>
        /// <param name="jobDetailID">
        /// The job detail id.
        /// </param>
        /// <param name="startDateTime">
        /// The start date time.
        /// </param>
        /// <param name="endDateTime">
        /// The end date time.
        /// </param>
        /// <param name="everyOrAt">
        /// The every or at.
        /// </param>
        /// <param name="everyYear">
        /// The every year.
        /// </param>
        /// <param name="everyYearMonth">
        /// The every year month.
        /// </param>
        /// <param name="AtOccurence">
        /// The at occurence.
        /// </param>
        /// <param name="Atday">
        /// The atday.
        /// </param>
        /// <param name="AtMonth">
        /// The at month.
        /// </param>
        /// <param name="yearlyHr">
        /// The yearly hr.
        /// </param>
        /// <param name="yearlyMinute">
        /// The yearly minute.
        /// </param>
        /// <param name="launch">
        /// The launch.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerRoot"/>.
        /// </returns>
        public SchedulerRoot ScheduleYearlyJob(
            long jobDetailID,
            DateTime startDateTime,
            DateTime endDateTime,
            TimePeriodType everyOrAt,
            short everyYear = 0,
            Months everyYearMonth = Months.January,
            OccurenceType AtOccurence = OccurenceType.First,
            WeekDays Atday = WeekDays.Monday,
            Months AtMonth = Months.January,
            short yearlyHr = 0,
            short yearlyMinute = 0,
            bool launch = false)
        {
            // Prblem in scedulet AT part, need to review.
            if (everyOrAt == TimePeriodType.Every && everyYear < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }
            else if (yearlyHr < 0 || yearlyMinute < 0)
            {
                return _result.ErrorToObject(new SchedulerRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    JobDetailID = jobDetailID,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    TriggerType = (short)TriggerType.Yearly,
                    YearlyType = everyOrAt,
                    YearlyEvery = everyYear,
                    YearlyEveryMonth = (short)everyYearMonth,
                    YearlyAtOccurenceType = (short)AtOccurence,
                    YearlyAtWeekSelected = Atday.ToString(),
                    YearlyAtMonth = (short)AtMonth,
                    YearTabHour = yearlyHr,
                    YearTabMinute = yearlyMinute,
                    Enablechk = launch
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/ScheduleJob",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRoot(), "Schedules");
        }

        /// <summary>
        /// The sceduler email set up.
        /// </summary>
        /// <param name="notificationId">
        /// The notification id.
        /// </param>
        /// <param name="emailTemplateId">
        /// The email template id.
        /// </param>
        /// <param name="EmailServerId">
        /// The email server id.
        /// </param>
        /// <param name="emailSubject">
        /// The email subject.
        /// </param>
        /// <param name="emailContent">
        /// The email content.
        /// </param>
        /// <returns>
        /// The <see cref="SchedulerEmailDetails"/>.
        /// </returns>
        public SchedulerEmailDetails ScedulerEmailSetUp(
            int notificationId,
            int emailTemplateId,
            int EmailServerId,
            string emailSubject,
            string emailContent)
        {
            var requestArg = JsonConvert.SerializeObject(
                new
                {
                    Notificationid = notificationId,
                    Templateid = emailTemplateId,
                    EmailServerId = EmailServerId,
                    Subject = emailSubject,
                    Editor = emailContent
                });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserSchedule/UpdateEmailNotifcation",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerEmailDetails(), string.Empty);
        }
    }
}