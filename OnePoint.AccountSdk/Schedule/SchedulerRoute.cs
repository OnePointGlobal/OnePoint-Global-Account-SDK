using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Schedule
{
    public class SchedulerRoute
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        AdminRequestHandler requestHandler { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Result result = new Result();

        public SchedulerRoute(AdminRequestHandler hanlder)
        {
            this.requestHandler = hanlder;
        }

        public SchedulerRootObject GetSchedules(long surveyId)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/GetScheduledList?surveyID=" + surveyId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public EmailContentRoot GetScheduleDetails(long surveyId, long notificationId = 0)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new EmailContentRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/GetSheduleContent?surveyID=" + surveyId + "&notificationID=" + notificationId, HttpMethod.Get, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new EmailContentRoot(), "");
        }

        public SchedulerRootObject AddSchedule(long surveyId, string name, string description)
        {
            if (surveyId < 1)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { ScheduleName = name, ScheduleDescription = description, SurveyID = surveyId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/AddNewSchedule", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject UpdateSchedule(long notificationID, string name, string description)
        {
            if (notificationID < 1)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { Name = name, Description = description, NotificationID = notificationID });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/EditSchedule", HttpMethod.Put, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject DeleteScedules(List<long> jobIDs)
        {
            if (jobIDs.Count < 1)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/DeleteSchedules?JobIDs=" + string.Join(",", jobIDs), HttpMethod.Delete, RouteStyle.Rpc, null);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleMinutesJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, short everyMinute, bool launch = false)
        {
            if (everyMinute < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Minutes, MinuteEvery = everyMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleHourlyJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, TimePeriodType everyOrAt, short everyHour, short atHr = 0, short atMinute = 0, bool launch = false)
        {
            if (everyOrAt == TimePeriodType.Every && everyHour < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && (atHr < 0 || atMinute < 0))
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Hourly, HourlyType = (short)everyOrAt, HourEvery = everyHour, HourlyAtHour = atHr, HourlyAtMinute = atMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleDailyJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, TimePeriodType everyOrAt, short everyDays, short atEveryDayHr = 0, short atEveryDayMinute = 0, bool launch = false)
        {
            if (everyOrAt == TimePeriodType.Every && everyDays < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && (atEveryDayHr < 0 || atEveryDayMinute < 0))
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Daily, DailyType = (short)everyOrAt, DailyEvery = everyDays, DailyTabHour = atEveryDayHr, DailyTabMinute = atEveryDayMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleWeeklyJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, WeekDays day, short atHr = 0, short atMinute = 0, bool launch = false)
        {
            if (atHr < 0 || atMinute < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Weekly, Weekly = day.ToString(), WeekTabHour = atHr, WeekTabMinute = atMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleMonthlyJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, TimePeriodType everyOrAt, short everyDay = 0, short everyMonths = 0, OccurenceType AtType = OccurenceType.First, WeekDays Atday = WeekDays.Monday, short atMonths = 0, short monthlyHr = 0, short monthlyMinute = 0, bool launch = false)
        {

            if (everyOrAt == TimePeriodType.Every && (everyDay < 0 || everyMonths < 0))
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }
            else if (everyOrAt == TimePeriodType.At && atMonths < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }
            else if (monthlyHr < 0 || monthlyMinute < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Monthly, MonthlyType = everyOrAt, MonthlyEveryDay = everyDay, MonthlyEvery = everyMonths, MonthlyAtOccurenceType = AtType, MonthlyAtDaySelected = Atday, MonthlyAtEvery = atMonths, MonthTabHour = monthlyHr, MonthTabMinute = monthlyMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerRootObject ScheduleYearlyJob(long jobDetailID, DateTime startDateTime, DateTime endDateTime, TimePeriodType everyOrAt, short everyYear = 0, Months everyYearMonth = Months.January, OccurenceType AtOccurence = OccurenceType.First, WeekDays Atday = WeekDays.Monday, Months AtMonth = Months.January, short yearlyHr = 0, short yearlyMinute = 0, bool launch = false)
        {
            //Prblem in scedulet AT part, need to review.
            if (everyOrAt == TimePeriodType.Every && everyYear < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }
            else if (yearlyHr < 0 || yearlyMinute < 0)
            {
                return result.ErrorToObject(new SchedulerRootObject(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(new { JobDetailID = jobDetailID, StartDateTime = startDateTime, EndDateTime = endDateTime, TriggerType = (short)TriggerType.Yearly, YearlyType = everyOrAt, YearlyEvery = everyYear, YearlyEveryMonth = (short)everyYearMonth, YearlyAtOccurenceType = (short)AtOccurence, YearlyAtWeekSelected = Atday.ToString(), YearlyAtMonth = (short)AtMonth, YearTabHour = yearlyHr, YearTabMinute = yearlyMinute, Enablechk = launch });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/ScheduleJob", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerRootObject(), "Schedules");
        }

        public SchedulerEmailDetails ScedulerEmailSetUp(int notificationId, int emailTemplateId, int EmailServerId, string emailSubject, string emailContent)
        {
            var requestArg = JsonConvert.SerializeObject(new { Notificationid = notificationId, Templateid = emailTemplateId, EmailServerId = EmailServerId, Subject = emailSubject, Editor = emailContent });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = this.requestHandler.SendRequestAsync(string.Empty, "api/UserSchedule/UpdateEmailNotifcation", HttpMethod.Post, RouteStyle.Rpc, requestArg);
            x.Wait();

            return x.Result.JsonToObject(new SchedulerEmailDetails(), "");
        }
    }
}
