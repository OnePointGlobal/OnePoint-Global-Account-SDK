using System.Collections.Generic;

namespace OnePoint.AccountSdk.Questionnaire
{
    public class Script
    {
        public object Color { get; set; }
        public object Label { get; set; }
        public string ScriptType { get; set; }
        public int ScriptId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class ScriptContent
    {
        public int ScriptContentId { get; set; }
        public int ScriptId { get; set; }
        public string Script { get; set; }
        public string ByteCode { get; set; }
        public bool Included { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
        public string FileType { get; set; }
    }

    public class Questionnaire
    {
        public List<Script> Scripts { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }

    public class ScriptRoot
    {
        public Script Script { get; set; }
        public ScriptContent ScriptContent { get; set; }
        public string FileName { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
