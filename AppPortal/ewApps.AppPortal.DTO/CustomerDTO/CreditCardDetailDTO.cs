
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    public class CreditCardDetailDTO {

        public new Guid ID {
            get; set;
        }
        
        public string NameOnCard {
            get; set;
        }
        public string CardType {
            get; set;
        }
        public string CardNumber {
            get; set;
        }
        public short ExpiryYear {
            get; set;
        }
        public short ExpiryMonth {
            get; set;
        }
        public int OperationType {
            get; set;
        }

        public string Token {
            get; set;
        }

        [NotMapped]
        public Guid? PreAuthPaymentID {
            get; set;
        }

    }
}
