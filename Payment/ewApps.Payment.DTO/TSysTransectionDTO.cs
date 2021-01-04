using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class TSysTransectionDTO {

        public Guid ID {
            get; set;
        }

        public string MID {
            get; set;
        }
        public Guid TenantId {
            get; set;
        }
        public string TransactionId {
            get; set;
        }
        public decimal TransactionAmount {
            get; set;
        }
        public decimal CapturedAmount { get; set; } = 0;
        public int CapturedTransactionCount { get; set; } = 0;
        public decimal ReturnedAmount { get; set; } = 0;
        public string InvoiceID {
            get; set;
        }
        public string PaymentID {
            get; set;
        }
        public string TransactionMode {
            get; set;
        } //"Card", "Token"
        public string Status {
            get; set;
        }

        /// <summary>
        /// Currenst generated tsys transection id.
        /// </summary>
        public string CurrentTransactionId { get; set; } 

    }
}
