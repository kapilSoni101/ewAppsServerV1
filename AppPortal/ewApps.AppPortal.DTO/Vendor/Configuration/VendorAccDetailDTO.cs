using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class VendorAccDetailDTO {

        public VendorAccDetailDTO() {
            BankAcctDetailList = new List<BankAcctDetailDTO>();
            CreditCardDetailList = new List<CreditCardDetailDTO>();
        }

        public List<BankAcctDetailDTO> BankAcctDetailList {
            get; set;
        }

        public List<CreditCardDetailDTO> CreditCardDetailList {
            get; set;
        }
    }
}
