// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved.
// </copyright>
// <summary>
//   The account user manages user profile data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OnePoint.AccountSdk.User
{
    /// <summary>
    /// The user profile class, provides the porperties for user profile data.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gmt.
        /// </summary>
        public double Gmt { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the credit.
        /// </summary>
        public double Credit { get; set; }

        /// <summary>
        /// Gets or sets the shared key.
        /// </summary>
        public string SharedKey { get; set; }
    }

    /// <summary>
    /// The user root class, provides properties for UserProfile, UserRoute method execution success/failure information. 
    /// </summary>
    public class UserRoot
    {
        /// <summary>
        /// Gets or sets the user profile.
        /// </summary>
        public UserProfile UserProfile { get; set; }

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