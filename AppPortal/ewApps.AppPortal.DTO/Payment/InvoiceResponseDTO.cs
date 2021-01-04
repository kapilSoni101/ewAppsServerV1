using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// A response model object of a invoice.
    /// </summary>
    public class InvoiceResponseDTO {

        public Guid InvoiceId {
            get; set;
        }

        public int InvoiceEntityType {
            get; set;
        }

        public string ERPInvoiceKey {
            get; set;
        }

        public string ERPInvoiceDocNum {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

    }
}
