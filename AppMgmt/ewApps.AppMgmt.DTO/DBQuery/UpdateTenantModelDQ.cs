/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 January 2019.
 * 
 * Contributor/s: 
 * Last Updated On: 31 January 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {
    /// <summary>
    /// Class contains detail model of tenant.
    /// </summary>
    public class UpdateTenantModelDQ:BaseDQ {
        /// <summary>
        /// TenantId.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        public string VarId {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string SubDomainName {
            get; set;
        }

        public bool Active {
            get; set;
        }

        public new DateTime CreatedOn {
            get; set;
        }

        public string Language {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        [NotMapped]
        public string DateTimeFormat {
            get; set;
        }

        public string Currency {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string IdentityNumber {
            get; set;
        }

        public new Guid CreatedBy {
            get; set;
        }

        public new Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// Tenant activation (joined date).
        /// </summary>
        public DateTime? JoinedOn {
            get; set;
        }

        [NotMapped]
        /// <summary>
        /// business website.
        /// </summary>
        public string Website {
            get; set;
        }        

        #region Tenant Primary User 

        [NotMapped]
        public string PrimaryUserEmail {
            get; set;
        }

        [NotMapped]
        public string PrimaryUserFirstName {
            get; set;
        }

        [NotMapped]
        public string PrimaryUserLastName {
            get; set;
        }

        /// <summary>
        /// Tenant home application Primary user joined date.
        /// </summary>
        [NotMapped]
        public DateTime? UserActivationDate {
            get; set;
        }

        /// <summary>
        /// Tenant primary user Id
        /// </summary>
        [NotMapped]
        public Guid PrimaryUserId {
            get; set;
        }

        #endregion Tenant Primary User

        /// <summary>
        /// Class contains detail subscription information.
        /// </summary>
        [NotMapped]
        public List<UpdateBusinessAppSubscriptionDTO> Subscriptions {
            get; set;
        }

    }
}
