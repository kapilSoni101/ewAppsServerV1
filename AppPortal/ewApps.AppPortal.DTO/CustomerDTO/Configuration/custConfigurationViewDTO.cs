﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// get configuration detail
    /// </summary>
    public class CustConfigurationViewDTO {

        /// <summary>
        /// 
        /// </summary>
        public CustConfigurationViewDTO() {
            CustomerBillAddressList = new List<CustCustomerAddressDTO>();
            CustomerShipAddressList = new List<CustCustomerAddressDTO>();
            CustomerContactList = new List<CustCustomerContactDTO>();
            CustomerBankAcctDetailList = new List<BankAcctDetailDTO>();
            CustomerCreditCardDetailList = new List<CreditCardDetailDTO>();
                
        }

        /// <summary>
        /// Customer id
        /// </summary>
        public Guid CustomerID {
            get; set;
        }

        /// <summary>
        /// BusinessPartnerTenantId
        /// </summary>
        public Guid BusinessPartnerTenantId {
            get; set;
        }

        

        /// <summary>
        /// Customer name
        /// </summary>
        public string CustomerName {
            get; set;
        }

        ///// <summary>
        ///// Group 
        ///// </summary>
        //public string Group {
        //    get; set;
        //}

        /// <summary>
        /// ERPCustomerKey
        /// </summary>
        public string ERPCustomerKey {
            get;set;
        }

        /// <summary>
        /// federalTaxID
        /// </summary>
        public string FederalTaxID {
            get; set;
        }

        /// <summary>
        /// first Telephone number
        /// </summary>
        public string Tel1 {
            get; set;
        }

        /// <summary>
        /// Seconde telephone number
        /// </summary>
        public string Tel2 {
            get; set;
        }

        /// <summary>
        /// Moblie number
        /// </summary>
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// Email ID
        /// </summary>
        public string Email {
            get; set;
        }

        /// <summary>
        /// WebSite
        /// </summary>
        public string Website {
            get; set;
        }

        /// <summary>
        /// Currency
        /// </summary>
        public string CurrencyCode {
            get; set;
        }

        /// <summary>
        /// Group Value
        /// </summary>
        public string GroupValue {
            get; set;
        }

        /// <summary>
        /// Group seperator
        /// </summary>
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        /// Decimal precision
        /// </summary>
        public int DecimalPrecision {
            get; set;
        }

        /// <summary>
        /// Decimal seperator
        /// </summary>
        public string DecimalSeperator {
            get; set;
        }

        /// <summary>
        /// Language
        /// </summary>
        public string Language {
            get; set;
        }

        /// <summary>
        /// Date Time format
        /// </summary>
        public string DateTimeFormat {
            get; set;
        }

        /// <summary>
        /// Date Time zone
        /// </summary>
        public string TimeZone {
            get; set;
        }

        /// <summary>
        /// Can update currency
        /// </summary>
        public bool CanUpdateCurrency {
            get; set;
        }
       

        /// <summary>
        /// Get customer billing address
        /// </summary>
        [NotMapped]
        public List<CustCustomerAddressDTO> CustomerBillAddressList {
            get; set;
        }

        /// <summary>
        /// Get customer shipment address
        /// </summary>
        [NotMapped]
        public List<CustCustomerAddressDTO> CustomerShipAddressList {
            get; set;
        }

        /// <summary>
        /// Get customer contact detail
        /// </summary>
        [NotMapped]
        public List<CustCustomerContactDTO> CustomerContactList {
            get; set;
        }

        /// <summary>
        /// Get customer account detail
        /// </summary>
        [NotMapped]
        public List<BankAcctDetailDTO> CustomerBankAcctDetailList {
            get; set;
        }

        /// <summary>
        /// Get customer contact detail
        /// </summary>
        [NotMapped]
        public List<CreditCardDetailDTO> CustomerCreditCardDetailList {
            get; set;
        }
    }
}
