using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnePoint.AccountSdk.Schedule
{
    public class NotificationRoute
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
        /// The NotificationRoute constructor.
        /// </summary>
        /// <param name="hanlder">The request handler</param>
        public NotificationRoute(AdminRequestHandler hanlder)
        {
            RequestHandler = hanlder;
        }

        /// <summary>
        /// Get notification(s) of a survey
        /// </summary>
        /// <param name="surveyId">The surveyID</param>
        /// <returns>The <see cref="NotificationRoot"/>.</returns>
        public NotificationRoot GetSurveyNotifications(long surveyId)
        {
            if (surveyId < 1)
            {
                return _result.ErrorToObject(new NotificationRoot(), "Invalid parameter(s)");
            }

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserNotification/GetNotifications?surveyID=" + surveyId,
                HttpMethod.Get,
                RouteStyle.Rpc,
                null);
            x.Wait();

            return x.Result.JsonToObject(new NotificationRoot(), "Notifications");
        }

        /// <summary>
        /// The add notification to a survey.
        /// </summary>
        /// <param name="surveyId">The survey Id</param>
        /// <param name="name">Name of the notification</param>
        /// <param name="description">The description</param>
        /// <param name="medium">The notification medium.</param>
        /// <returns>The <see cref="NotificationRoot"/>.</returns>
        public NotificationRoot AddSurveyNotification(long surveyId, string name, string description, NotificationMedium medium)
        {
            if (surveyId < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return _result.ErrorToObject(new NotificationRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { Name = name, Description = description, SurveyID = surveyId, Medium = medium, JobType = 2 });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });

            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserNotification/AddNewNotification",
                HttpMethod.Post,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new NotificationRoot(), "Notifications");
        }

        /// <summary>
        /// Update or edit the basic details of a notification.
        /// </summary>
        /// <param name="surveyId">The survey id</param>
        /// <param name="notificationId">The notification Id</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description.</param>
        /// <returns>The <see cref="NotificationRoot"/>.</returns>
        public NotificationRoot UpdateNotification(long surveyId, long notificationId, string name, string description)
        {
            if (surveyId < 1 || notificationId < 1)
            {
                return _result.ErrorToObject(new NotificationRoot(), "Invalid parameter(s)");
            }

            var requestArg = JsonConvert.SerializeObject(
                new { Name = name, Description = description, NotificationID = notificationId, SurveyID = surveyId });
            requestArg = JsonConvert.SerializeObject(new { Data = requestArg });
            Task<Result> x = RequestHandler.SendRequestAsync(
                string.Empty,
                "api/UserNotification/EditNotification",
                HttpMethod.Put,
                RouteStyle.Rpc,
                requestArg);
            x.Wait();

            return x.Result.JsonToObject(new NotificationRoot(), "Notifications");
        }
    }
}
