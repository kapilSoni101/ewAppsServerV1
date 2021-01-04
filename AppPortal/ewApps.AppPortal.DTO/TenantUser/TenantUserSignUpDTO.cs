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
using System.Collections.Generic;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// This class is a DTO that contains tenant user information to be use for Add and Update operations.
    /// </summary>
    public class TenantUserSignUpDTO {

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// User unique identifier.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }
        /// <summary>
        /// Portal type of the user.
        /// </summary>
        public bool IsPrimary {
            get; set;
        }
        /// <summary>
        /// USer first name.
        /// </summary>
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// User last name.
        /// </summary>
        public string LastName {
            get; set;
        }

        /// <summary>
        /// Full name of the user.
        /// </summary>
        public string FullName {
            get; set;
        }

        /// <summary>
        ///  App user email login user id of the user.
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// User phone number of the tenant user.
        /// </summary>
        public string Phone {
            get; set;
        }

        /// <summary>
        /// Portal type of the user.
        /// </summary>
        public int UserType {
            get; set;
        }

        /// <summary>
        /// Permission Bit Mask of the user.
        /// </summary>
        public long PermissionBitMask {
            get; set;
        }

        /// <summary>
        /// User details about app and its permissions.
        /// </summary>
        public List<UserAppRelationDTO> UserAppRelationDTOList {
            get; set;
        }
    }
}
