using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Credit card payment information.
    /// </summary>
    public class CreditCardPayAttrModel {

        public Guid ID {
            get; set;
        }

        public string CardNumber {
            get; set;
        }

        public string NameOnCard {
            get; set;
        }

        public string ExpiryMonth {
            get; set;
        }

        public string ExpiryYear {
            get; set;
        }

        public string CardType {
            get; set;
        }

        public string CVV {
            get; set;
        }

        public string Token {
            get; set;
        }

    }
}
