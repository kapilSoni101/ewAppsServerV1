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
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// AppUser details
    /// </summary>
    public class TenantUserDetailsDTO:BaseDTO {

        /// <summary>
        /// User unique identifier
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        public string Phone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// Name of the User updating.
        /// </summary>
        public string UpdatedByName {
            get; set;
        }


        /// <summary>
        /// Name of the User
        /// </summary>
        public string LastName {
            get; set;
        }

        /// <summary>
        /// Name of the User
        /// </summary>
        public string FullName {
            get; set;
        }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// Count of features
        /// </summary>
        public int FeatureCount {
            get; set;
        }

        /// <summary>
        /// Invitor name
        /// </summary>
        public string InvitedBy {
            get;
            set;
        }

        /// <summary>
        /// Last active date
        /// </summary>
        public DateTime? JoinedDate {
            get; set;
        }

        /// <summary>
        /// Last active date
        /// </summary>
        public DateTime InvitedOn {
            get; set;
        }

        /// <summary>
        /// Admin bit to indicate a user is admin
        /// </summary>
        public bool Admin {
            get;
            set;
        }

        /// <summary>
        /// Admin bit to indicate a user is admin
        /// </summary>
        public new bool Deleted {
            get;
            set;
        }

        /// <summary>
        /// Business Partner TenantId.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// User Active.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Permission bit of the user.
        /// </summary>
        public long PermissionBitMask {
            get; set;
        }

        /// <summary>
        /// RoleId of the user.
        /// </summary>
        public Guid RoleId {
            get; set;
        }

        /// <summary>
        /// User Identitynumber.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

    /// <summary>
    /// Permission bit of the user.
    /// </summary>
    [NotMapped]
    public long AdminUserPermissionBitMask
    {
      get; set;
    }
  }
}
