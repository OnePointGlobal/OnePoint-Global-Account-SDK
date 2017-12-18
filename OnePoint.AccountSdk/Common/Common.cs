// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Common.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The common. Manages common operation performed in account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Common
{
    /// <summary>
    /// The common.
    /// </summary>
    class Common
    {
    }

    /// <summary>
    /// The country class, provides properties for country data.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the std.
        /// </summary>
        public string Std { get; set; }
    }

    /// <summary>
    /// The country root class, provides the porperties for Country, ComminRoute method execution success or failure information..
    /// </summary>
    public class CountryRoot
    {
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public List<Country> Country { get; set; }

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