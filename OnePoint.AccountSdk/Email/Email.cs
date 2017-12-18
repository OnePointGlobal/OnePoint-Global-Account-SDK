// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Email.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The email template.Manages email template data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Email
{
    /// <summary>
    /// The email template class, provides the porperties for email templates data.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Gets or sets the email template id.
        /// </summary>
        public int EmailTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email content.
        /// </summary>
        public string EmailContent { get; set; }

        /// <summary>
        /// Gets or sets the email type.
        /// </summary>
        public int EmailType { get; set; }

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
    /// The email rootcclass, provides the porperties for EmailTemplate, EmailRoute method execution success or failure information..
    /// </summary>
    public class EmailRoot
    {
        /// <summary>
        /// Gets or sets the email templates.
        /// </summary>
        public List<EmailTemplate> EmailTemplates { get; set; }

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
    /// The panellist profile fields class,provides the porperties for PanellistProfileFields used in email and method execution success or failure information. .
    /// </summary>
    public class PanellistProfileFields
    {
        /// <summary>
        /// Gets or sets the special fields.
        /// </summary>
        public List<string> SpecialFields { get; set; }

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