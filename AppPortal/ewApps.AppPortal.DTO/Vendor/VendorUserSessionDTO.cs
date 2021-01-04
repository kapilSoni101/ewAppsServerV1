using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.AppPortal.DTO {

  /// <summary>
  /// Session info of the logined in vendor user on vendor portal.
  /// </summary>
  public class VendorUserSessionDTO:BaseUserSessionAndAppDTO {

        /// <summary>
        /// Configured flag.
        /// </summary>
        public bool Configured {
            get; set;
        }

        /// <summary>
        /// Configured flag.
        /// </summary>
        public bool IntegratedMode {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        public string PublisherName {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public Guid PublisherTenantId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LeftBusinessName {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LeftBusinessLogoUrl {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TenantWebsite {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid PartnerTenantId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid BAVendorId {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LeftPartnerName {
            get; set;
        }

        /// <summary>
        /// Custome ref id for the vendor user. (Check)
        /// </summary>
        public string ERPVendorKey {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public UserSessionCurrencyDTO UserSessionCurrency {
            get; set;
        }
    }
}
