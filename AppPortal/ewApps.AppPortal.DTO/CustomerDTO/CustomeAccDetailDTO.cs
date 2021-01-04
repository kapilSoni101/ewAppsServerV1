using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class CustomeAccDetailDTO {

        public CustomeAccDetailDTO() {
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
