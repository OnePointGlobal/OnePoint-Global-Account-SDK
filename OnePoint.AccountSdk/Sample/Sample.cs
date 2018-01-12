// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sample.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The sample, manages the survy sample data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Sample
{
    using OnePoint.AccountSdk.PanelPanellist;

    /// <summary>
    /// The sample.
    /// </summary>
    class Sample
    {
    }

    /// <summary>
    /// The filter class, provides the porperties for sample filters data.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Gets or sets the sample query element id.
        /// </summary>
        public int SampleQueryElementId { get; set; }

        /// <summary>
        /// Gets or sets the and or.
        /// </summary>
        public string AndOr { get; set; }

        /// <summary>
        /// Gets or sets the field name.
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets the condition id.
        /// </summary>
        public int ConditionId { get; set; }

        /// <summary>
        /// Gets or sets the field value.
        /// </summary>
        public string FieldValue { get; set; }

        /// <summary>
        /// Gets or sets the sample id.
        /// </summary>
        public int SampleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is basic.
        /// </summary>
        public bool IsBasic { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the variable id.
        /// </summary>
        public int VariableId { get; set; }

        /// <summary>
        /// Gets or sets the panel id.
        /// </summary>
        public int PanelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether in use.
        /// </summary>
        public bool InUse { get; set; }

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
    /// The variable class, provides the porperties for panellist profile variables data, used in sample filter operation.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Gets or sets the variable id.
        /// </summary>
        public int VariableId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the panel id.
        /// </summary>
        public int PanelId { get; set; }

        /// <summary>
        /// Gets or sets the type id.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is basic.
        /// </summary>
        public bool IsBasic { get; set; }

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
    /// The sample root object class, provides the porperties for Panel, Filter, Panellists, Variable and SampleRoute method execution success or failure information.
    /// </summary>
    public class SampleRoot
    {
        /// <summary>
        /// Gets or sets the panels imported for survey sample generation.
        /// </summary>
        public List<Panel> Panels { get; set; }

        /// <summary>
        /// Gets or sets the filters, applied on panels to generate sample panellists.
        /// </summary>
        public List<Filter> Filters { get; set; }

        /// <summary>
        /// Gets or sets the panellists of a survey sample.
        /// </summary>
        public List<Panellist> Panellists { get; set; }

        /// <summary>
        /// Gets or sets the variables of panellist profile for fitlering.
        /// </summary>
        public List<Variable> Variables { get; set; }

        /// <summary>
        /// Gets or sets the sample id.
        /// </summary>
        public long sampleId { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}