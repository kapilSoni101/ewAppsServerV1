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

namespace ewApps.AppMgmt.Entity {

    /// <summary>
    /// As a user may have multiple application in multiple tenants this table is user to store that relationship.
    /// </summary>
    [Table("TenantUserAppLinking", Schema = "am")]
    public class TenantUserAppLinking:BaseEntity {

        public const string EntityName = "TenantUserAppLinking";

        /// <summary>
        /// App identifier of the user.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// User to which this linking blowngs to.
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// The Id of BusinessPartner Tenant.
        /// </summary>
        public Guid? BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// Joining Date of the user.
        /// </summary>
        public DateTime? JoinedDate {
            get; set;
        }

        /// <summary>
        /// Invitation date of the user.
        /// </summary>
        public DateTime? InvitedOn {
            get; set;
        }

        /// <summary>
        /// Invitee.
        /// </summary>
        public Guid? InvitedBy {
            get; set;
        }

        /// <summary>
        /// User type of the user.
        /// </summary>
        public int UserType {
            get; set;
        }

        /// <summary>
        /// User active incative teanant and app wise.
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Users invitation status (Values of the enum TenantUserInvitaionStatusEnum).
        /// </summary>
        public int Status {
            get; set;
        }

    }
}
