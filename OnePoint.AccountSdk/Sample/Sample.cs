using System.Collections.Generic;

namespace OnePoint.AccountSdk.Sample
{
    class Sample
    {
    }

    public class Filter
    {
        public int SampleQueryElementId { get; set; }
        public string AndOr { get; set; }
        public string FieldName { get; set; }
        public int ConditionId { get; set; }
        public string FieldValue { get; set; }
        public int SampleId { get; set; }
        public bool IsBasic { get; set; }
        public int Type { get; set; }
        public int VariableId { get; set; }
        public int PanelId { get; set; }
        public bool IsDeleted { get; set; }
        public bool InUse { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class Variable
    {
        public int VariableId { get; set; }
        public string Name { get; set; }
        public int PanelId { get; set; }
        public int TypeId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBasic { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class SampleRootObject
    {
        public List<PanelPanellist.Panel> Panels { get; set; }
        public List<Filter> Filters { get; set; }
        public List<PanelPanellist.Panellist> Panellists { get; set; }
        public List<Variable> Variables { get; set; }
        public long sampleId { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
