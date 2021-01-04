using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class SignUpBACustomerAddressDTO {
        /// <summary>
        /// 
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Label {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ObjectType {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ObjectTypeText {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddressStreet1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressStreet2 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressStreet3 {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Street {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string StreetNo {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string City {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string ZipCode {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string State {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Country {
            get; set;
        }

    }
}
