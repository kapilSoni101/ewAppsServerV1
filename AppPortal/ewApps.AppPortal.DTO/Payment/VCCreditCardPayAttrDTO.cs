using System;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class VCCreditCardPayAttrDTO:BaseDTO {

        public new Guid ID {
            get; set;
        }

        public string CardNumber {
            get; set;
        }
        public string NameOnCard {
            get; set;
        }
        public short ExpiryMonth {
            get; set;
        }
        public short ExpiryYear {
            get; set;
        }
        public string CardType {
            get; set;
        }

        public string Token {
            get; set;
        }
    }
}