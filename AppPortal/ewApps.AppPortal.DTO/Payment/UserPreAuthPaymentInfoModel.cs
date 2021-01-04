using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// Contains customer and payment detail to make payment.
    /// </summary>
    public class UserPreAuthPaymentInfoModel {

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
        /// CreditCard payment.
        /// </summary>
        public CreditCardPayDTO selectedVCCreditCardPayAttr {
            get; set;
        }

    }
}
