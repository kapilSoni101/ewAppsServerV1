﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Class contains detail model of tenant.
    /// </summary>
//ToDo: nitin- model name is not correct. and basedto is not required.
//ToDo: nitin-New keyword is not required.
    public class UpdateTenantModelDTO:BaseDTO {
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

        #region Currency Localization Value

        /// <summary>
        ///CurrencyCode 
        /// </summary>
        [NotMapped]
        public int CurrencyCode {
            get; set;
        }

        /// <summary>
        ///GroupValue
        /// </summary>
        [NotMapped]
        public string GroupValue {
            get; set;
        }

        /// <summary>
        ///Powered By  
        /// </summary>
        [NotMapped]
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        ///DecimalSeperator 
        /// </summary>
        [NotMapped]
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        ///DecimalPrecision
        /// </summary>
        [NotMapped]
        public int DecimalPrecision {
            get; set;
        }

        #endregion Currency Localization Value


        /// <summary>
        /// Class contains detail subscription information.
        /// </summary>
        [NotMapped]
        public List<UpdateBusinessAppSubscriptionDTO> Subscriptions {
            get; set;
        }

        /// <summary>
        /// Entity type.
        /// </summary>
        public int EntityType {
            get; set;
        }

    }
}
