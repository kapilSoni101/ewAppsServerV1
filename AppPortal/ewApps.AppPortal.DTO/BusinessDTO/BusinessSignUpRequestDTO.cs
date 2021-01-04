using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Class contains porperties to registered tenant.
    /// </summary>
    public class BusinessSignUpRequestDTO {

        public string Name {
            get; set;
        }

        /// <summary>
        ///  Unique name of subdomain.
        /// </summary>
        public string SubDomainName {
            get; set;
        }

        /// <summary>
        /// Tenant id of a publisher, which is adding tenant.
        /// </summary>
        public Guid PublisherTenantId {
            get;set;
        }

        /// <summary>
        /// It help to get creator of tenant.
        /// </summary>
        public string SourceTenantSubDomainName {
            get; set;
        }

        public string VarId {
            get; set;
        }

        public bool Active {
            get; set;
        }

        // It will be useful later.
        public List<BusinessAddressModelDTO> AddressList {
            get; set;
        }

        public string CapabilityConfig {
            get; set;
        }

        public string Language {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        public string Currency {
            get; set;
        }

        public string ContactPersonEmail {
            get; set;
        }

        public string ContactPersonName {
            get; set;
        }

        public string ContactPersonDesignation {
            get; set;
        }

        public string ContactPersonPhone {
            get; set;
        }

        public string Website {
            get; set;
        }

        /// <summary>
        /// Contains list of subscribe application.
        /// </summary>
        public List<BusinessAppSubscriptionDTO> Subscriptions {
            get; set;
        }

        /// <summary>
        /// Connector setting.
        /// </summary>
        public List<ConnectorConfigDTO> ConnectorConfigList {
            get; set;
        }

        #region Currency Localization Value

        /// <summary>
        ///CurrencyCode 
        /// </summary>
        public int CurrencyCode {
            get; set;
        }
        /// <summary>
        ///GroupValue
        /// </summary>
        public string GroupValue {
            get; set;
        }

        /// <summary>
        ///Powered By  
        /// </summary>
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        ///DecimalSeperator 
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        ///DecimalPrecision
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }

        #endregion Currency Localization Value

        #region Tenant Primary User 

        public string AdminEmail {
            get; set;
        }

        public string AdminFirstName {
            get; set;
        }

        public string AdminLastName {
            get; set;
        }

        #endregion Tenant Primary User

        /// <summary>
        /// Map Business registration model to Tenant Registration model and return it.
        /// </summary>
        /// <returns></returns>
        public BusinessTenantSignUpDTO MapBusinessModelToAddTenantModel() {
            BusinessTenantSignUpDTO addTenant = new BusinessTenantSignUpDTO();
            addTenant.Active = this.Active;
            addTenant.AdminEmail = this.AdminEmail;
            addTenant.AdminFirstName = this.AdminFirstName;
            addTenant.AdminLastName= this.AdminLastName;
            addTenant.CapabilityConfig = this.CapabilityConfig;
            addTenant.Currency = this.Currency;
            addTenant.CurrencyCode = this.CurrencyCode;
            addTenant.DateTimeFormat = this.DateTimeFormat;
            addTenant.DecimalPrecision = this.DecimalPrecision;
            addTenant.DecimalSeperator = this.DecimalSeperator;
            addTenant.GroupSeperator = this.GroupSeperator;
            addTenant.GroupValue = this.GroupValue;
            addTenant.Language = this.Language;
            addTenant.Name = this.Name;
            addTenant.SourceTenantSubDomainName = this.SourceTenantSubDomainName;
            addTenant.SubDomainName = this.SubDomainName;
            addTenant.Subscriptions = this.Subscriptions;
            addTenant.TimeZone = this.TimeZone;
            addTenant.VarId = this.VarId;
            addTenant.Website = this.Website;            

            return addTenant;
        }
    }

}
