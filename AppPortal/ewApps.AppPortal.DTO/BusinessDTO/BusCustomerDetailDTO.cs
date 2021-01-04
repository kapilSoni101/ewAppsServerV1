using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusCustomerDetailDTO {
        /// <summary>
        /// 
        /// </summary>
        public BusCustomerDTO Customer {
            get; set;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public List<BusCustomerContactDTO> CustomerContactList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BusCustomerAddressDTO> BillToAddressList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BusCustomerAddressDTO> ShipToAddressList {
            get; set;
        }

        public List<BusinessAddressModelDTO> listBusinessAddress {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public CustomeAccDetailDTO CustomerAcctDetail {
            get; set;
        }
    }
}
