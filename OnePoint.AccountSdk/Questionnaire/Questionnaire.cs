// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Questionnaire.cs" company="OnePoint Global Ltd">
//  Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The questionnaire, manages survey questions/script data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Questionnaire
{
    /// <summary>
    /// The script class,provides the porperties for script data.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public object Color { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public object Label { get; set; }

        /// <summary>
        /// Gets or sets the script type.
        /// </summary>
        public string ScriptType { get; set; }

        /// <summary>
        /// Gets or sets the script id.
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

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
    /// The script content class, provides the porperties for scrit questions content.
    /// </summary>
    public class ScriptContent
    {
        /// <summary>
        /// Gets or sets the script content id.
        /// </summary>
        public int ScriptContentId { get; set; }

        /// <summary>
        /// Gets or sets the script id.
        /// </summary>
        public int ScriptId { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the byte code of script.
        /// </summary>
        public string ByteCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether included.
        /// </summary>
        public bool Included { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the file type.
        /// </summary>
        public string FileType { get; set; }
    }

    /// <summary>
    /// The questionnaire, provides the porperties for Script and QuestionnaireRoute method execution success or failure information.
    /// </summary>
    public class Questionnaire
    {
        /// <summary>
        /// Gets or sets the scripts.
        /// </summary>
        public List<Script> Scripts { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is success.
        /// </summary>
        public bool IsSuccess { get; set; }
    }

    /// <summary>
    /// The scriptRoot, provides the porperties for Script, ScriptContent and QuestionnaireRoute method execution success or failure information.
    /// </summary>
    public class ScriptRoot
    {
        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        public Script Script { get; set; }

        /// <summary>
        /// Gets or sets the script content.
        /// </summary>
        public ScriptContent ScriptContent { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        public string FileName { get; set; }

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