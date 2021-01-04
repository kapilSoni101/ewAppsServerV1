using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.BusinessEntity.DTO {

    public class CustBACustomerDetailDTO {

        /// <summary>
        /// Initialize loal variables in constructor
        /// </summary>
        public CustBACustomerDetailDTO() {
            BillToAddressList = new List<CustomerAddressDTO>();
            ShipToAddressList = new List<CustomerAddressDTO>();
        }

        /// <summary>
        /// Cust DTo
        /// </summary>
        public BACustomerDTO Customer {
            get; set;
        }

        /// <summary>
        /// cust pay detail dto list
        /// </summary>
        public List<BACustomerPaymentDetailDTO> CustomerPaymentDetailList {
            get; set;
        }

        /// <summary>
        /// Cust contact dto list
        /// </summary>
        public List<CustomerContactDTO > CustomerContactList {
            get; set;
        }
        
        /// <summary>
        ///  Bill to address list
        /// </summary>
        public List<CustomerAddressDTO> BillToAddressList {
            get; set;
        }

        /// <summary>
        /// Ship to address list
        /// </summary>
        public List<CustomerAddressDTO> ShipToAddressList {
            get; set;
        }

    }
}
