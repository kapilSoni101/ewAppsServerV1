using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Class contains porperties to registered tenant.
    /// Class will used to send data to App Management API to registered tenant.
    /// </summary>
    public class BusinessTenantSignUpDTO {

        public Guid GeneratedTenantId {
            get; set;
        }

        /// <summary>
        /// Userid will be a unique id. Add user with this Id.
        /// </summary>
        public Guid GeneratedUserId {
            get; set;
        }

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

        public string Website {
            get; set;
        }

        /// <summary>
        /// Contains list of subscribe application.
        /// </summary>
        public List<BusinessAppSubscriptionDTO> Subscriptions {
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
        /// A entity type enum value.
        /// </summary>
        public int EntityType {
            get; set;
        }

        /// <summary>
        /// Its business permission.
        /// </summary>
        public long PermissionEnum {
            get; set;
        }        

    }
}
