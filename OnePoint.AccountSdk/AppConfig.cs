// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="OnePoint Global Ltd">
//   Copyright 2012 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The app config.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OnePoint.AccountSdk
{
    using System;
    using System.Configuration;
    using System.Reflection;

    /// <summary>
    /// The app config.
    /// </summary>
    internal class AppConfig
    {
        #region Constants

        /// <summary>
        /// The confi g_ emai l_ medi a_ folder.
        /// </summary>
        private const string CONFIG_HOSTNAME = "hostname";


        #endregion

        #region Public Properties

        public static string HostName
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings[CONFIG_HOSTNAME];
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        #endregion
    }
}