using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
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
        /// <summary>
        /// List of applciation assigned to the user.
        /// </summary>
        [NotMapped]
        public List<AppInfoDTO> AssignedAppInfo {
            get; set;
        }
    }

}
