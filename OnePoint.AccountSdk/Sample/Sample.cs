using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePoint.AccountSdk.Sample
{
    class Sample
    {
    }

    public class Filter
    {
        public int SampleQueryElementID { get; set; }
        public string AndOr { get; set; }
        public string FieldName { get; set; }
        public int ConditionID { get; set; }
        public string FieldValue { get; set; }
        public int SampleID { get; set; }
        public bool IsBasic { get; set; }
        public int Type { get; set; }
        public int VariableID { get; set; }
        public int PanelID { get; set; }
        public bool IsDeleted { get; set; }
        public bool InUse { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class Variable
    {
        public int VariableID { get; set; }
        public string Name { get; set; }
        public int PanelID { get; set; }
        public int TypeID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBasic { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdatedDate { get; set; }
    }

    public class SampleRootObject
    {
        public List<OnePoint.AccountSdk.PanelPanellist.Panel> Panels { get; set; }
        public List<Filter> Filters { get; set; }
        public List<OnePoint.AccountSdk.PanelPanellist.Panellist> Panellists { get; set; }
        public List<Variable> Variables { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
