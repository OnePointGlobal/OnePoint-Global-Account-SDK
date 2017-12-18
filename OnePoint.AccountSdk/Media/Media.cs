// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Media.cs" company="OnePoint Global Ltd">
//   Copyright (c) 2017 OnePoint Global Ltd. All rights reserved. 
// </copyright>
// <summary>
//   The media, manages media data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace OnePoint.AccountSdk.Media
{
    /// <summary>
    /// The media class, provides the porperties for media data.
    /// </summary>
    public class Media
    {
        /// <summary>
        /// Gets or sets the media id.
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// Gets or sets the media name.
        /// </summary>
        public string MediaName { get; set; }

        /// <summary>
        /// Gets or sets the media description.
        /// </summary>
        public string MediaDescription { get; set; }

        /// <summary>
        /// Gets or sets the media type id.
        /// </summary>
        public int MediaTypeId { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last updated date.
        /// </summary>
        public string LastUpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is video.
        /// </summary>
        public bool IsVideo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is sound.
        /// </summary>
        public bool IsSound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is image.
        /// </summary>
        public bool IsImage { get; set; }

        /// <summary>
        /// Gets or sets the media url.
        /// </summary>
        public string MediaUrl { get; set; }
    }

    /// <summary>
    /// The media root class, provides the porperties for Media, MediaRoute method execution success or failure information.
    /// </summary>
    public class MediaRoot
    {
        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        public List<Media> Media { get; set; }

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