//Response

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

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// This class is a DTO for requesting identity server to create a user.
    /// </summary>
    public class IdentityUserDTO {

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName {
            get;
            set;
        }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName {
            get;
            set;
        }

        /// <summary>
        /// Tenant identifier.
        /// </summary>
        public Guid? TenantId {
            get;
            set;
        }

        /// <summary>
        /// Client app type for user portal identifier.
        /// </summary>
        public string ClientAppType {
            get;
            set;
        }

        /// <summary>
        /// User login email.
        /// </summary>
        public string Email {
            get;
            set;
        }

        /// <summary>
        /// Active flag of the user.
        /// </summary>
        public bool IsActive {
            get;
            set;
        }

        /// <summary>
        /// User type of the user to be added.
        /// </summary>
        public int UserType {
            get;
            set;
        }
    }
}
