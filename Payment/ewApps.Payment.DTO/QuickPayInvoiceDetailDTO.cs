using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
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
        /// Tenantid
        /// </summary>
        public Guid TenantId {
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
