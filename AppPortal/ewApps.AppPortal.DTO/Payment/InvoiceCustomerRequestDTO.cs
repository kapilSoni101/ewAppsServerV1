using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Invoice customer request model.
    /// </summary>
    public class InvoiceCustomerRequestDTO {

        /// <summary>
        /// List of invoice ids.
        /// </summary>
        public List<Guid> listInvoiceId {
            get;set;
        }

        /// <summary>
        /// List of invoice ids.
        /// </summary>
        public List<string> erpARInvoiceKeys {
            get; set;
        }

        /// <summary>
        /// CustomerIds
        /// </summary>
        public Guid CustomerId {
            get;set;
        }

        /// <summary>
        /// ApplicationId.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// TenantId.
        /// </summary>
        public Guid TenantId {
            get; set;
        }

    }
}
