using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Contains customer and payment detail to make payment.
    /// </summary>
    public class UserPaymentInfoModel {

        /// <summary>
        /// Customer Name
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// CustomerId
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Business entity customerid.
        /// </summary>
        public Guid BACustomerId {
            get; set;
        }

        /// <summary>
        /// BusinessPartnerTenantId.
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// Tenant Id
        /// </summary>        
        public Guid TenantId {
            get; set;
        }

        /// <summary>
        /// Selected payment service.
        /// </summary>        
        public Guid AppServiceId {
            get; set;
        }

        /// <summary>
        /// Selected payment serviceattribute.
        /// </summary>
        public Guid AppServiceAttributeId {
            get; set;
        }

        /// <summary>
        /// Payment done by PreAuth or not. 
        /// </summary>
        public Guid? PreAuthPaymentID {
            get; set;
        }

        /// <summary>
        /// Payment is doing as partial "Y"/"N". 
        /// </summary>
        public string PartialPayment {
            get; set;
        }

        /// <summary>
        /// In case of vericheck payment, Model should not be null.
        /// </summary>
        public CustVCACHPayAttrDTO SelectedCustVCACHPayAttr {
            get;set;
        }

        /// <summary>
        /// In case of CreditCard payment.
        /// </summary>
        public CreditCardPayAttrModel selectedVCCreditCardPayAttr {
            get;set;
        }

    }
}
