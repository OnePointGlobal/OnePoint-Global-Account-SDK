// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="OnePoint Global Ltd">
//    Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The app config class, manages the configuration keys.
//   Provides the code to get config key's value.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OnePoint.AccountSdk
{
    using System.Configuration;

    /// <summary>
    ///     The app config.
    /// </summary>
    internal class AppConfig
    {
        #region Constants

        /// <summary>
        /// The confg hostname.
        /// </summary>
        private const string CONFIG_HOSTNAME = "hostname";

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the api host name, from config key.
        /// </summary>
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