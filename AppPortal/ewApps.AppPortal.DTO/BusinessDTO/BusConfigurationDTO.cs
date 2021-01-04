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
    public class BusConfigurationDTO:BaseDTO {

        #region Tenant's Detail

        /// <summary>
        /// Name of the application.
        /// </summary>
        public new Guid TenantId {
            get;
            set;
        }

        /// <summary>
        /// Name of the application.
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// SubDomainName of Tenant
        /// </summary>
        public string SubDomainName {
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
        //[Required]
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

        /// <summary>
        /// 
        /// </summary>
        public string Website {
            get; set;
        }

        /// <summary>
        /// FederalTexId of Business 
        /// </summary>
        public string FederalTexId {
            get;
            set;
        }

        /// <summary>
        ///Telephone no 2 .
        /// </summary>        
        public string TelePhone1 {
            get; set;
        }

        /// <summary>
        /// Telephone no 2 .
        /// </summary>       
        public string TelePhone2 {
            get; set;
        }


        /// <summary>
        /// Phone.
        /// </summary>        
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// Email.
        /// </summary>        
        public string Email {
            get; set;
        }

        /// <summary>
        /// Status of BUsiness
        /// </summary>
        public bool Status {
            get; set;
        }

        public bool InitDB {
            get;set;
        }

        #endregion

        #region Admin User Detail

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

        #endregion

        #region Address

        /// <summary>
        ///  Address Model
        /// </summary>
        [NotMapped]
        public List<BusinessAddressModelDTO> AddressDTO {
            get;
            set;
        }

        #endregion Address

        #region Contact Person Detail

        /// <summary>
        /// 
        /// </summary>
        public Guid BusinessId {
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

        #endregion

        #region Currency


        /// <summary>
        /// Business selected currency for example $ etc.
        /// </summary>
        public int CurrencyCode {
            get; set;
        }

        /// <summary>
        /// Group seperator use for currency seperator after number of degits like 10000 then Groupvalue is 3 then it should be like 10,000
        /// </summary>
        public string GroupValue {
            get; set;
        }

        /// <summary>
        /// Group seperator charecter, for example comma(,) is a seperator then 10000 will display like 10,000
        /// </summary>
        public string GroupSeperator {
            get; set;
        }
        /// <summary>
        /// Group seperator charecter, for example comma(,) is a seperator and decimal seperator is dot(.) then 10000.101 will display like 10,000.00
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }

        /// <summary>
        /// Number of deigits use after decimal places.
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }

        /// <summary>
        /// Whether currency related fields can update or not.
        /// </summary>
        public bool CanUpdateCurrency {
            get; set;
        }

        #endregion Currency

        #region Portal Localization

        /// <summary>
        /// Language Detail
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// TimeZone Detail
        /// </summary>
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// DateFormat Detail
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }
        #endregion Portal Localization     

        #region Connector

        [NotMapped]
        public List<ConnectorConfigDTO> ConnectorConfigList {
            get; set;
        }

        #endregion Connector

        #region AppService
        [NotMapped]
        // Payment Detail
        public List<AppServiceDTO> ServiceList {
            get;
            set;
        }

        #region PayAppService
        [NotMapped]
        // Payment Detail
        public List<PayAppServiceDetailDTO> PayServiceList {
            get;set;
        } 
        #endregion

        #endregion


    }
}
