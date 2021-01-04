using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {   

    public class BACustomerEntityDTO:BaseDTO {

        /// <summary>
        /// SAP connector key .
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// SAP customer key .
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Name of customer .
        /// </summary>
        public string CustomerName {
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
        public string Group {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Currency {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string FederalTaxID {
            get; set;
        }

        /// <summary>
        /// Customer Address 1 .
        /// </summary>
        public string AddressLine1 {
            get; set;
        }

        /// <summary>
        ///  Customer address 2 .
        /// </summary>
        public string AddressLine2 {
            get; set;
        }

        /// <summary>
        ///  Customer Address street .
        /// </summary>
        public string Street {
            get; set;
        }

        /// <summary>
        /// Customer Address street number .
        /// </summary>
        public string StreetNo {
            get; set;
        }

        /// <summary>
        /// Customer Address city .
        /// </summary>
        public string City {
            get; set;
        }

        /// <summary>
        /// Customer Address zipcode .
        /// </summary>
        public string ZipCode {
            get; set;
        }

        /// <summary>
        /// Customer Address state .
        /// </summary>
        public string State {
            get; set;
        }

        /// <summary>
        /// Customer Address country .
        /// </summary>
        public string Country {
            get; set;
        }

        /// <summary>
        /// Customer  telephone number1 .
        /// </summary>
        public string Tel1 {
            get; set;
        }

        /// <summary>
        /// Customer  telephone number2 .
        /// </summary>
        public string Tel2 {
            get; set;
        }

        /// <summary>
        /// Customer  mobile number .
        /// </summary>
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// Customer  fax number.
        /// </summary>
        public string Fax {
            get; set;
        }

        /// <summary>
        /// Customer  email .
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// Customer  website .
        /// </summary>
        public string Website {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int StatusText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks {
            get; set;
        }

    }
}
