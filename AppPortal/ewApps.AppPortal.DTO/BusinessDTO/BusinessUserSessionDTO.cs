using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Data transfer object for the session of the business.
    /// </summary>
    public class BusinessUserSessionDTO:BaseUserSessionAndAppDTO {

        /// <summary>
        /// Configured flag. 
        /// </summary>
        public bool Configured {
            get; set;
        }

        /// <summary>
        /// Configured flag. : TODO
        /// </summary>
        public bool IntegratedMode {
            get; set;
        }

        /// <summary>
        /// Custome ref id for the custome user. 
        /// </summary>
        public string PublisherName {
            get;
            set;
        }

        /// <summary>
        /// Custome ref id for the custome user.
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
        [NotMapped]
        public UserSessionCurrencyDTO UserSessionCurrency {
            get; set;
        }
    }
}
