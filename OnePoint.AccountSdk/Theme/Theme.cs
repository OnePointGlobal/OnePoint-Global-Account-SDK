// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Theme.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.

// </copyright>
// <summary>
//   The theme, manages user app and account theming data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Theme
{
    /// <summary>
    /// The theme class, provides the porperties for user app and account theme data.
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Gets or sets the theme template id.
        /// </summary>
        public int ThemeTemplateId { get; set; }

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
    /// The themelist class, provides the porperties for theme element data.
    /// </summary>
    public class ThemeList
    {
        /// <summary>
        /// Gets or sets the media url.
        /// </summary>
        public object MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the theme id.
        /// </summary>
        public int ThemeID { get; set; }

        /// <summary>
        /// Gets or sets the theme template id.
        /// </summary>
        public int ThemeTemplateId { get; set; }

        /// <summary>
        /// Gets or sets the theme element type id.
        /// </summary>
        public int ThemeElementTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

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
    /// The theme root class, provides the porperties for Theme, ThemeRoute method execution success or failure information.
    /// </summary>
    public class ThemeRoot
    {
        /// <summary>
        /// Gets or sets the themes.
        /// </summary>
        public List<Theme> Themes { get; set; }

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
    /// The theme list root, has the fields to set Theme elements & success/failure message.
    /// </summary>
    public class ThemeListRoot
    {
        /// <summary>
        /// Gets or sets the theme list.
        /// </summary>
        public List<ThemeList> ThemeList { get; set; }

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