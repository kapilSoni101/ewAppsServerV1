using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusCustomerSetUpAppDTO {

        /// <summary>
        /// ewapps customer ID 
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary> CustomerId
        /// ewapps Partner ID corresponding to this customer
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }
        /// <summary>
        /// ewapps Partner ID corresponding to this customer
        /// </summary>
        public string CustomerName {
            get; set;
        }
       
        /// <summary>
        /// Tenant Id 
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// Tenant Id 
        /// </summary>
        public int ApplicationCount {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

    }
}
