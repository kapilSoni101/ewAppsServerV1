using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BACustomerDetailSyncDTO {
        /// <summary>
        /// 
        /// </summary>
        public BACustomerDTO Customer {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<BACustomerPaymentDetailDTO> CustomerPaymentDetailList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CustomerContactDTO > CustomerContactList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CustomerAddressDTO> CustomerAddressList {
            get; set;
        }

      

    }
}
