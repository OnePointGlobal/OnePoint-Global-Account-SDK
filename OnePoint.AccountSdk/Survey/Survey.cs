using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Survey
{
    public class Survey
    {
        public int SurveyID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public int ScriptID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOffline { get; set; }
        public bool IsCapi { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public int Occurences { get; set; }
        public string DeadLine { get; set; }
        public string SearchTags { get; set; }
        public string SurveyReference { get; set; }
    }

    public class RootObject
    {
        public List<Survey> Surveys { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
