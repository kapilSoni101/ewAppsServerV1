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

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Class contains detail model of tenant.
    /// </summary>
    public class UpdateBusinessTenantModelDQ:BaseDQ {

        /// <summary>
        /// Publisher tenantid.
        /// </summary>
        public Guid PublisherTenantId {
            get; set;
        }

        /// <summary>
        /// TenantId.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        public Guid BusinessId {
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

        /// <summary>
        /// whether database initilize or not.
        /// </summary>
        public bool InitDB {
            get;set;
        }

        [NotMapped]
        /// <summary>
        /// business website.
        /// </summary>
        public string Website {
            get; set;
        }

        #region Contact Info user

        [NotMapped]
        public string ContactPersonEmail {
            get; set;
        }

        [NotMapped]
        public string ContactPersonName {
            get; set;
        }

        [NotMapped]
        public string ContactPersonDesignation {
            get; set;
        }

        [NotMapped]
        public string ContactPersonPhone {
            get; set;
        }

        #endregion Contact Info user

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

        public bool IntegratedMode {
            get;set;
        }


        /// </summary>
        [NotMapped]
        public List<BusinessAddressModelDTO> AddressList {
            get; set;
        }


        /// <summary>
        /// Class contains detail subscription information.
        /// </summary>
        [NotMapped]
        public List<UpdateBusinessAppSubscriptionDTO> Subscriptions {
            get; set;
        }

        /// <summary>
        /// Connector setting.
        /// </summary>
        [NotMapped]
        public List<ConnectorConfigDTO> ConnectorConfigList {
            get; set;
        }

        /// <summary>
        /// Map Business registration model to Tenant Registration model and return it.
        /// </summary>
        /// <returns></returns>
        public UpdateAppMgmtTenantModelDTO MapBusinessModelToUpdateAppMgmtTenantModel() {
            UpdateAppMgmtTenantModelDTO upModel = new UpdateAppMgmtTenantModelDTO();
            upModel.Active = this.Active;
            upModel.Currency = this.Currency;
            upModel.CurrencyCode = this.CurrencyCode;
            upModel.DateTimeFormat = this.DateTimeFormat;
            upModel.DecimalPrecision = this.DecimalPrecision;
            upModel.DecimalSeperator = this.DecimalSeperator;
            upModel.GroupSeperator = this.GroupSeperator;
            upModel.GroupValue = this.GroupValue;
            upModel.ID = this.ID;
            upModel.IdentityNumber = this.IdentityNumber;
            upModel.JoinedOn = this.JoinedOn;
            upModel.Language = this.Language;
            upModel.Name = this.Name;
            upModel.PrimaryUserEmail = this.PrimaryUserEmail;
            upModel.PrimaryUserFirstName = this.PrimaryUserFirstName;
            upModel.PrimaryUserId = this.PrimaryUserId;
            upModel.PrimaryUserLastName = this.PrimaryUserLastName;
            upModel.SubDomainName = this.SubDomainName;
            upModel.Subscriptions = this.Subscriptions;
            upModel.TenantId = this.TenantId;
            upModel.TimeZone = this.TimeZone;
            upModel.UpdatedBy = this.UpdatedBy;
            upModel.UserActivationDate = this.UserActivationDate;
            upModel.VarId = this.VarId;
            upModel.Website = this.Website;
            upModel.CreatedBy = this.CreatedBy;
            return upModel;
        }

    }
}
