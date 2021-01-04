using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Contains bank account information.
    /// </summary>
    public class CustVCACHPayAttrDTO: BaseDTO {

        /// <summary>
        /// Name of bank.
        /// </summary>
        public string BankName {
            get; set;
        }

        /// <summary>
        /// Bank account number.
        /// </summary>
        public string AccountNo {
            get; set;
        }

        /// <summary>
        /// Name in bank acount.
        /// </summary>
        public string NameInBank {
            get; set;
        }

        /// <summary>
        /// Routing number.
        /// </summary>
        public string ABARounting {
            get; set;
        }

        /// <summary>
        /// Account type whether it is saving/checking.
        /// </summary>
        public string AccountType {
            get; set;
        }

        /// <summary>
        /// SECCode.
        /// </summary>
        public string SECCode {
            get; set;
        }

    }

}
