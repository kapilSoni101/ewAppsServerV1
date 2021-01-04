using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains detail information of a invoice.
    /// 1) Invoice detail with all subitems, discount, taxes etc information.
    /// 2) Payble amount information of a invoice.
    /// 3) Customer information.  
    /// </summary>
    public class QuickPayInvoiceDetailDTO {

        /// <summary>
        /// Invoice detail object.
        /// </summary>
        public BAARInvoiceViewDTO InvoiceDTO {
            get; set;
        }

        /// <summary>
        /// Customer detail object.
        /// </summary>
        public CustomerPaymentInfoDTO CustomerDTO {
            get; set;
        }

        /// <summary>
        /// Tenantid
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// TenantLogUrl contains the url.
        /// </summary>
        public string TenantLogUrl {
            get; set;
        }

        public string TenantName {
            get; set;
        }

        /// <summary>
        /// Datetime formate for the tenant.
        /// </summary>
        public string DateTimeFormat {
            get; set;
        }

        /// <summary>
        /// Timezone for the tenant.
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Language for the tenant.
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Currency for the tenant.
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// Amount to pay for invoice.
        /// </summary>
        public decimal PaybleAmount {
            get; set;
        }

    }
}
