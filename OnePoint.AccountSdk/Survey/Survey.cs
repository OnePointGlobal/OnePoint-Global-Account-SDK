using System.Collections.Generic;

namespace OnePoint.AccountSdk.Survey
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int ScriptId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOffline { get; set; }
        public int Occurences { get; set; }
        public string SurveyReference { get; set; }
        public int EstimatedTime { get; set; }
        public int SampleId { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class RootObject
    {
        public List<Survey> Surveys { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class SurveySammary
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public int AppVisited { get; set; }
        public int AppDownloaded { get; set; }
        public int AppSignedIn { get; set; }
        public int InvitationSentCount { get; set; }
        public int InvitationDeliveredCount { get; set; }
        public int InvitationOpenedCount { get; set; }
        public int InvitationNotDeliveredCount { get; set; }
        public int PanellistCount { get; set; }
        public string Waplink { get; set; }
    }


    public class SurveySubEvent
    {
        public int Questionnaire { get; set; }
        public int Sample { get; set; }
        public int Quota { get; set; }
        public int Scheduler { get; set; }
        public int Notification { get; set; }
        public int RealtimeReporting { get; set; }
        public bool Isgeofensing { get; set; }
    }
}
