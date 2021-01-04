using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// 
    /// </summary>
    public class SignUpBACustomerDTO {

        /// <summary>
        /// 
        /// </summary>
        public string ERPConnectorKey {
            get; set;
        }

        public string FederalTaxID {
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
        public string AddressLine1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string AddressLine2 {
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
        /// <summary>
        /// 
        /// </summary>
        public string Tel1 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel2 {
            get; set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Fax {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Website {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ShippingType {
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
        public int? Status {
            get; set;
        }


        /// <summary>
        /// 
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? OpType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<object> CustomerPaymentDetailList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SignUpBACustomerContactDTO> CustomerContactList {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<SignUpBACustomerAddressDTO> CustomerAddressList {
            get; set;
        }

        public string Language {
            get; set;
        }

        public string TimeZone {
            get; set;
        }

        public string DateTimeFormat {
            get; set;
        }

        #region Currency Localization Value

        /// <summary>
        ///CurrencyCode 
        /// </summary>
        public int CurrencyCode {
            get; set;
        }
        /// <summary>
        ///GroupValue
        /// </summary>
        public string GroupValue {
            get; set;
        }

        /// <summary>
        ///Powered By  
        /// </summary>
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        ///DecimalSeperator 
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        ///DecimalPrecision
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }

        #endregion Currency Localization Value

    }
}

