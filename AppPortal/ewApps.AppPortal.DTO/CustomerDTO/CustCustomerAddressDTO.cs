using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {


   public class CustCustomerAddressDTO {

        /// <summary>
        /// Unique id of customer.
        /// </summary>
        public new Guid ID {
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
        public string Line1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Line2 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Line3 {
            get; set;
        }

        public string AddressStreet1 {
            get; set;
        }

        public string AddressStreet2 {
            get; set;
        }

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
