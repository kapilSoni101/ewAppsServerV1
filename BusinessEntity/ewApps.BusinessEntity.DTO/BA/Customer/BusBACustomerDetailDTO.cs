using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class BusBACustomerDetailDTO {

        public BusBACustomerDetailDTO() {
            BillToAddressList = new List<CustomerAddressDTO>();
            ShipToAddressList = new List<CustomerAddressDTO>();
        }
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
        public List<CustomerAddressDTO> BillToAddressList {
            get; set;
        }
        public List<CustomerAddressDTO> ShipToAddressList {
            get; set;
        }

    }
}
