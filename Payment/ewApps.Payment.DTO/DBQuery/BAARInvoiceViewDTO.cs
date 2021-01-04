/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Contains invoice detail.
    /// </summary>
    public class BAARInvoiceViewDTO:BaseDQ {

        public new Guid ID {
            get; set;
        }

        public new Guid TenantId {
            get;
            set;
        }

        public Guid CustomerId {
            get; set;
        }

        public string CustomerRefNo {
            get; set;
        }

        public string ShippingTypeText {
            get;set;
        }

        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        public string SalesEmployee {
            get;set;
        }

        public string Owner {
            get;set;
        }

        #region Amount

        public decimal TotalBeforeDiscount {
            get; set;
        }

        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Total  amount of invoice
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Total amount(Foreign currency) of invoice.
        /// </summary>
        public decimal TotalPaymentDueFC {
            get; set;
        }

        public decimal BalanceDue {
            get; set;
        }

        public decimal BalanceDueFC {
            get; set;
        }

        /// <summary>
        /// Amount paid for invoice.
        /// </summary>
        public decimal AmountPaid {
            get; set;
        }

        /// <summary>
        /// Amount paid for invoice.
        /// </summary>
        public decimal AmountPaidFC {
            get; set;
        }

        /// <summary>
        /// Discount.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Discount in foreign currency.
        /// </summary>
        public decimal DiscountFC {
            get; set;
        }

        /// <summary>
        /// Tax 
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// tax in foreign currency.
        /// </summary>
        public decimal TaxFC {
            get; set;
        }

        /// <summary>
        /// Freight
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Freight in forrign currency.
        /// </summary>
        public decimal FreightFC {
            get; set;
        }

        /// <summary>
        /// LocalCurrency
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        public string CustomerCurrency {
            get; set;
        }

        public string Remarks {
            get; set;
        }

        #endregion Amount

        public string ERPCustomerKey {
            get; set;
        }

        public string ERPARInvoiceKey {
            get; set;
        }

        public int Status {
            get; set;
        }

        public string StatusText {
            get; set;
        }

        public new DateTime? CreatedOn {
            get; set;
        }

        public new DateTime? UpdatedOn {
            get; set;
        }

        public DateTime? DueDate {
            get; set;
        }

        public DateTime? DocumentDate {
            get; set;
        }

        public DateTime? PostingDate {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        /// <summary>
        /// List of invoice items.
        /// </summary>
        [NotMapped]
        public List<BAARInvoiceItemViewDTO> invoiceItems {
            get; set;
        }

        //[NotMapped]
        //public Address Address {
        //    get; set;
        //}        

        public string ContactName {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }
        
        #region Address

        /// <summary>
        /// Ship from address
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        #endregion Address      

    }
}

/*
        Currency exchange rate code.
 
        /// <summary>
        /// Business currency code.
        /// </summary>    
        public int FromCurrencyCode {
            get; set;
        }

        /// <summary>
        /// customer currency code.
        /// </summary>    
        public int ToCurrencyCode {
            get; set;
        }

        /// <summary>
        /// Currency conversion rate FromCurrency to ToCurrency.
        /// </summary>    
        public decimal ExchangeRate {
            get; set;
        }

        /// <summary>
        /// calulated excahge rate Amount.
        /// </summary>
        [NotMapped]
        public decimal CalculateAmount {
            get; set;
        }

        /// <summary>
        /// Calulated Amountpaid as per exchangerate.
        /// </summary>
        [NotMapped]
        public decimal CalculateAmountPaid {
            get; set;
        }     

        [NotMapped]
        public decimal CalculateTax {
            get; set;
        }

        [NotMapped]
        public decimal CalculateDiscount {
            get; set;
        }
*/
