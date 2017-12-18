// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Survey.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The survey. manages project survey data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Survey
{
    /// <summary>
    /// The survey class, provides the porperties for survey data.
    /// </summary>
    public class Survey
    {
        /// <summary>
        /// Gets or sets the survey id.
        /// </summary>
        public int SurveyId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the script id.
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is offline.
        /// </summary>
        public bool IsOffline { get; set; }

        /// <summary>
        /// Gets or sets the occurences.
        /// </summary>
        public int Occurences { get; set; }

        /// <summary>
        /// Gets or sets the survey reference.
        /// </summary>
        public string SurveyReference { get; set; }

        /// <summary>
        /// Gets or sets the estimated time.
        /// </summary>
        public int EstimatedTime { get; set; }

        /// <summary>
        /// Gets or sets the sample id.
        /// </summary>
        public int SampleId { get; set; }

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
    /// The Survey root, provides the porperties for Survey, SurveyRoute method execution success or failure information.
    /// </summary>
    public class SurveyRoot
    {
        /// <summary>
        /// Gets or sets the surveys.
        /// </summary>
        public List<Survey> Surveys { get; set; }

        /// <summary>
        /// Gets or sets the error message on IsSuccess false.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }

    /// <summary>
    /// The survey sammary class, provides the porperties for Survey events summary, SurveyRoute method execution success or failure information.
    /// </summary>
    public class SurveySammary
    {
        /// <summary>
        /// Gets or sets the error message on IsSuccess false.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is method call is success/failure.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the app visited.
        /// </summary>
        public int AppVisited { get; set; }

        /// <summary>
        /// Gets or sets the app downloaded.
        /// </summary>
        public int AppDownloaded { get; set; }

        /// <summary>
        /// Gets or sets the app signed in.
        /// </summary>
        public int AppSignedIn { get; set; }

        /// <summary>
        /// Gets or sets the survey invitation sent count.
        /// </summary>
        public int InvitationSentCount { get; set; }

        /// <summary>
        /// Gets or sets the survey invitation delivered count.
        /// </summary>
        public int InvitationDeliveredCount { get; set; }

        /// <summary>
        /// Gets or sets the survey invitation opened count.
        /// </summary>
        public int InvitationOpenedCount { get; set; }

        /// <summary>
        /// Gets or sets the survey invitation not delivered count.
        /// </summary>
        public int InvitationNotDeliveredCount { get; set; }

        /// <summary>
        /// Gets or sets the survey sample panellist count.
        /// </summary>
        public int PanellistCount { get; set; }

        /// <summary>
        /// Gets or sets the survey test waplink.
        /// </summary>
        public string Waplink { get; set; }
    }

    /// <summary>
    /// The survey sub event class, provides the porperties for Survey sub event data.
    /// </summary>
    public class SurveySubEvent
    {
        /// <summary>
        /// Gets or sets the survey questionnaire.
        /// </summary>
        public int Questionnaire { get; set; }

        /// <summary>
        /// Gets or sets the survey sample panellist.
        /// </summary>
        public int Sample { get; set; }

        /// <summary>
        /// Gets or sets the survey quota.
        /// </summary>
        public int Quota { get; set; }

        /// <summary>
        /// Gets or sets the survey invitation scheduler.
        /// </summary>
        public int Scheduler { get; set; }

        /// <summary>
        /// Gets or sets the notification scheduler.
        /// </summary>
        public int Notification { get; set; }

        /// <summary>
        /// Gets or sets the realtime survey result reporting.
        /// </summary>
        public int RealtimeReporting { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is geofencing survey.
        /// </summary>
        public bool Isgeofensing { get; set; }
    }
}