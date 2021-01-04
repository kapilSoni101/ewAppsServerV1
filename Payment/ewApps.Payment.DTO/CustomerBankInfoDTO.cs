using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// CustomerBank DTO contains bank and custom information.
    /// </summary>
    public class CustomerBankInfoDTO {

        #region Vericheck Data

        public string VCMerchantId {
            get; set;
        }

        /// <summary>
        /// Merchant Id to which this customer belongs
        /// </summary>
        public Guid MerchantId {
            get; set;
        }
        /// <summary>
        /// Customer Name
        /// </summary>
        public string Name {
            get; set;
        }

        public Guid AppServiceId {
            get; set;
        }
        /// <summary>
        /// Customer Bank Account Name
        /// </summary>
        public string BankAccountName {
            get; set;
        }

        /// <summary>
        /// Customer Bank Name
        /// </summary>
        public string BankName {
            get; set;
        }

        /// <summary>
        /// Bank Routing Number
        /// </summary>
        public string ABARoutingNumber {
            get; set;
        }
        /// <summary>
        /// Customer account number to be used for payment
        /// </summary>
        public string AccountNumber {
            get; set;
        }
        /// <summary>
        /// Account type:Saving,Checking
        /// </summary>
        public BankAccountTypeEnum AccountType {
            get; set;
        }

        #endregion

        #region ewApps Data

        /// <summary>
        /// ewapps customer ID 
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// ewapps Partner ID corresponding to this customer
        /// </summary>
        public Guid PartnerId {
            get; set;
        }

        /// <summary>
        /// Business Id of the merchant to which this Customer belongs
        /// </summary>
        public Guid BusinessId {
            get; set;
        }

        /// <summary>
        /// Active or not?
        /// </summary>
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Tenant Id 
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        [NotMapped]
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        #endregion

    }
}
