/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;


namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Publisher details Data trasnfer object.
    /// </summary>
    public class PublisherDetailsDTO {

        /// <summary>
        /// Publisher's unique id.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// The publisher's creator unique id.
        /// </summary>
        public Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// The publisher's creator name.
        /// </summary>
        public string CreatedByName {
            get; set;
        }

        /// <summary>
        /// The publisher's creation date time (in UTC).
        /// </summary>
        public DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// The last updated by user name.
        /// </summary>
        public Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// The last updated date time (in UTC).
        /// </summary>
        public DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PublisherDetailsDTO"/> is deleted. <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Publisher's Identity number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of Publisher.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Publisher active status identifier.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Publisher Inactive Comment
        /// </summary>
        public string InactiveComment {
            get;
            set;
        }

        /// <summary>
        /// Publisher's business subscribed application count.
        /// </summary>
        public int ApplicationCount {
            get;
            set;
        }

        /// <summary>
        /// Publisher's business tenant count.
        /// </summary>
        public int TenantCount {
            get;
            set;
        }

        /// <summary>
        /// Publisher's inactive business tenant count
        /// </summary>
        public int ActiveTenantCount {
            get;
            set;
        }

        /// <summary>
        /// Publisher's business user count.
        /// </summary>
        public int UserCount {
            get;
            set;
        }

    }
}
