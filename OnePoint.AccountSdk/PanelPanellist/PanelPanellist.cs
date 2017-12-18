// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PanelPanellist.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The panelpanellist, manages panel and panellist data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace OnePoint.AccountSdk.PanelPanellist
{
    /// <summary>
    /// The panel class, provides the porperties for panel data.
    /// </summary>
    public class Panel
    {
        /// <summary>
        /// Gets or sets the panellist count of a panel.
        /// </summary>
        public int PanellistCount { get; set; }

        /// <summary>
        /// Gets or sets the media url.
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the logo url.
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// Gets or sets the panel id.
        /// </summary>
        public long PanelId { get; set; }

        /// <summary>
        /// Gets or sets the theme template id.
        /// </summary>
        public long ThemeTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the panel type.
        /// </summary>
        public int PanelType { get; set; }

        /// <summary>
        /// Gets or sets the search tag.
        /// </summary>
        public string SearchTag { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        public long MediaId { get; set; }

        /// <summary>
        /// Gets or sets the logo id.
        /// </summary>
        public long LogoId { get; set; }
    }

    /// <summary>
    /// The panellist class, provides the porperties for panellist data.
    /// </summary>
    public class Panellist
    {
        /// <summary>
        /// Gets or sets the panellist id.
        /// </summary>
        public int PanellistId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the date bf birth.
        /// </summary>
        public string DOB { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the address 1.
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address 2.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public int CountryCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether terms condition.
        /// </summary>
        public bool TermsCondition { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status { get; set; }

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
        /// Gets or sets the search tag.
        /// </summary>
        public string SearchTag { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        public int MaritalStatus { get; set; }
    }

    /// <summary>
    /// The country class, provides the porperties for country data.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the std.
        /// </summary>
        public string Std { get; set; }

        /// <summary>
        /// Gets or sets the gmt.
        /// </summary>
        public string Gmt { get; set; }

        /// <summary>
        /// Gets or sets the credit rate.
        /// </summary>
        public int CreditRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsDeleted { get; set; }
    }

    /// <summary>
    /// The profile variable class, provides the porperties for panellist profile variable data.
    /// </summary>
    public class ProfileVariable
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
    /// The panellist root object class, provides the porperties for Panellist, PanellistRoute method execution success or failure information.
    /// </summary>
    public class PanellistRootObject
    {
        /// <summary>
        /// Gets or sets the panellist.
        /// </summary>
        public List<Panellist> Panellist { get; set; }

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
    /// The panel root object class, provides the porperties for Panel, PanelRoute method execution success or failure information.
    /// </summary>
    public class PanelRootObject
    {
        /// <summary>
        /// Gets or sets the panels.
        /// </summary>
        public List<Panel> Panels { get; set; }

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
    /// The profile variable root object class, provides the porperties for ProfileVariable, ProfileElementRoute method execution success or failure information.
    /// </summary>
    public class ProfileVariableRootObject
    {
        /// <summary>
        /// Gets or sets the profile elements.
        /// </summary>
        public List<ProfileVariable> ProfileElements { get; set; }

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