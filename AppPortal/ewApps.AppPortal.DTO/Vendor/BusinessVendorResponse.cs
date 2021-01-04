using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    public class BusinessVendorResponse {
        /// <summary>
        /// Unique id of vendor.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string VendorName {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ERPVendorKey {
            get; set;
        }
        /// <summary>
        /// customer tenant id .
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Currency {
            get; set;
        }


        public int CurrencyCode {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BusVendorContactDTO> VendorContactList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BusVendorAddressDTO> BillToAddressList {
            get; set;
        }

        /// <summary>
        /// Shipping address.
        /// </summary>
        public List<BusVendorAddressDTO> ShipToAddressList {
            get; set;
        }

        public List<BusinessAddressModelDTO> listBusinessAddress {
            get;set;
        }

    }

}
