using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    public class CustVCACHPayAttrDTO:BaseDTO {
        //public BankName:string;
        //public AccountNo:string;
        //public NameInBank:string;
        //public ABARounting:Number
        //public AccountType:string;
        //public SECCode:string ; 

        public string BankName {
            get; set;
        }

        public string AccountNo {
            get; set;
        }

        public string NameInBank {
            get; set;
        }

        public string ABARounting {
            get; set;
        }

        public string AccountType {
            get; set;
        }

        public string SECCode {
            get; set;
        }
        

    }
}