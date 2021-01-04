using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Contains customer and its payment services.
    /// </summary>
    public class CustomerPaymentInfoDTO {

        #region ewApps Data

        /// <summary>
        /// ewapps customer ID 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        public string Currency {
            get;set;
        }

        /// <summary>
        /// Business entity customerid.
        /// </summary>
        public Guid BACustomerId {
            get; set;
        }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Tenant Id 
        /// </summary>
        public Guid TenantId {
            get; set;
        }

        public Guid BusinessPartnerTenantId {
            get; set;
        }

        #endregion

        /// <summary>
        /// List of subscribed payment services by a business.
        /// </summary>
        public List<BusAppServiceDTO> listAppServiceDTO;

    }
}
