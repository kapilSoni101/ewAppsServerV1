using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// All necessary property required for access the deeplink data.
    /// </summary>
    public class AddDeepLinkDTO {

        /// <summary>
        /// Invoiceid.
        /// </summary>
        public Guid InvoiceId {
            get; set;
        }

        /// <summary>
        /// Customerid.
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// applicationid.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// applicationid.
        /// </summary>
        public decimal PaybleAmount {
            get; set;
        }

    }
}
