/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Configuration Get/Update
    /// </summary>
    public class ConfigurationDTO:BaseDTO {


        #region Tenant's Detail

        /// <summary>
        /// 
        /// </summary>
        public new Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SubDomainName {
            get; set;
        }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string IdentityNumber {
            get;
            set;
        }

        /// <summary>
        /// Tenant's primary user last invitation date and time.
        /// </summary>        
        public DateTime InvitedOn {
            get;
            set;
        }

        /// <summary>
        /// User name who invite tenant's primary user last.
        /// </summary>
        public string InvitedBy {
            get;
            set;
        }

        /// <summary>
        /// Date and Time when user accept invitation for current application.
        /// </summary>
        public DateTime? JoinedDate {
            get; set;
        }

        /// <summary>
        /// True if tenant is active.
        /// </summary>
        public bool Active {
            get; set;
        }

        #endregion Tenant's Detail


        #region Tenant User / Admin User Detail

        /// <summary>
        /// 
        /// </summary>
        public Guid AdminUserId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserFirstName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserLastName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AdminUserEmail {
            get; set;
        }

        #endregion Tenant User / Admin User Detail


        #region Publisher Address

        /// <summary>
        ///  Address Model
        /// </summary>
        [NotMapped]
        public List<PublisherAddressDTO> PublisherAddressDTO {
            get;
            set;
        }

        #endregion Publisher Address


        #region Publisher / Contact Person Detail

        /// <summary>
        /// 
        /// </summary>
        public Guid PublisherId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonDesignation {
            get; set;
        }

        /// <summary>
        /// Tenant's communication email.
        /// </summary>
        public string ContactPersonEmail {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContactPersonPhone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Website {
            get; set;
        }

        #endregion Publisher / Contact Person Detail



        /// <summary>
        /// Business portal URL of the application for current tenant.
        /// </summary>
        [NotMapped]
        public string BusinessPortalUrl {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public List<AppDetailInfoDTO> AppList {
            get;
            set;
        }


    }
}
