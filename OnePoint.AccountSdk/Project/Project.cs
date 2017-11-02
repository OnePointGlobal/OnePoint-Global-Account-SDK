using System.Collections.Generic;

namespace OnePoint.AccountSdk.Project
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }


    public class RootObject
    {
        public List<Project> Projects { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
