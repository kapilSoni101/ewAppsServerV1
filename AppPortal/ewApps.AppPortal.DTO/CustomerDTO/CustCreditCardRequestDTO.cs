using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class CustCreditCardRequestDTO {

        /// <summary>
        /// Card Number
        /// </summary>
        public string CardNumber {
            get; set;
        }

        /// <summary>
        /// Expiration Date
        /// </summary>
        public string ExpirationDate {
            get; set;
        }

        /// <summary>
        /// Card Holder Name
        /// </summary>
        public string CardHolderName {
            get; set;
        }

        /// <summary>
        /// DeveloperID
        /// </summary>
        public string DeveloperID {
            get; set;
        }

    }
}
