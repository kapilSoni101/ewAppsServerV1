using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// Invoice table storing all the invoice data.
    /// </summary>
    public class BAARInvoiceEntityDTO {

        public new Guid ID {
            get;
            set;
        }
        /// <summary>
        /// Customer table unique id.
        /// </summary>        
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Appled amount.
        /// </summary>

        public decimal AppliedAmount {
            get; set;
        }

        /// <summary>
        /// Appled amount.
        /// </summary>

        public decimal AppliedAmountFC {
            get; set;
        }

        /// <summary>
        /// Balanace due on invoice.
        /// </summary>

        public decimal BalanceDue {
            get; set;
        }

        /// <summary>
        /// Balanace due on invoice.
        /// </summary>

        public decimal BalanceDueFC {
            get; set;
        }

        public Guid UpdatedBy {
            get;set;
        }

    }
}
