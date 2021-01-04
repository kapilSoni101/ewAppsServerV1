using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    public class PaymentInfoDTO {

        public Guid ID {
            get;set;
        }

        public string IdentityNumber {
            get;set;
        }

        /// <summary>
        /// Type of the Payment like for Invoice/Advanced
        /// </summary>        
        public int PaymentType {
            get; set;
        }

        public string Description {
            get; set;
        }

        public string Status {
            get; set;
        }

        public decimal AmountPaid {
            get;set;
        }

        public decimal AmountPaidFC {
            get; set;
        }

        public string BankAccountName {
            get; set;
        }

        public string CustomerAccountNumber {
            get; set;
        }

        public string PayeeName {
            get; set;
        }


        public string PayServiceName {
            get; set;
        }

        public string PayServiceAttributeName {
            get; set;
        }

        public string PaymentTransectionCurrency {
            get; set;
        }

    }
}
