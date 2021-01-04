using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.Money;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// Contains invoice detail.
    /// </summary>
    public class AddBAARInvoiceDTO:BaseDQ {

        public new Guid ID {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        public string CustomerRefNo {
            get; set;
        }

        public string CustomerName {
            get; set;
        }

        public string ShippingTypeText {
            get; set;
        }

        public string SalesEmployee {
            get; set;
        }

        public string Owner {
            get; set;
        }

        /// <summary>
        /// Invoice No
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        #region Amount

        public decimal ExchangeRate {
            get; set;
        }

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
        public List<AddBAARInvoiceItemDTO> invoiceItems {
            get; set;
        }

        public string ContactName {
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

        public string ERPDocNum {
            get; set;
        }

        [NotMapped]
        public List<AddUpdateDocumentModel> listAttachment {
            get; set;
        }


        public static BAARInvoice MapToEntity(AddBAARInvoiceDTO invDTO) {
            BAARInvoice inv = new BAARInvoice();
            inv.AppliedAmount = 0;
            inv.AppliedAmountFC = 0;
            inv.BalanceDue = invDTO.BalanceDue;
            if(invDTO.ExchangeRate <= 0 || invDTO.LocalCurrency == invDTO.CustomerCurrency) {
                invDTO.ExchangeRate = 1;
            }
            inv.BalanceDueFC = invDTO.BalanceDue * invDTO.ExchangeRate;
            inv.TotalPaymentDue = invDTO.BalanceDue;
            inv.TotalPaymentDueFC = invDTO.BalanceDueFC;
            inv.TotalBeforeDiscount = invDTO.TotalBeforeDiscount;
            inv.TotalBeforeDiscountFC = invDTO.TotalBeforeDiscount * invDTO.ExchangeRate;
            inv.TotalPaymentDue = invDTO.TotalPaymentDue;
            inv.TotalPaymentDueFC = invDTO.TotalPaymentDue * invDTO.ExchangeRate;
            inv.Discount = invDTO.Discount;
            inv.DiscountFC = invDTO.Discount * invDTO.ExchangeRate;
            inv.Tax = invDTO.Tax;
            inv.TaxFC = invDTO.Tax * invDTO.ExchangeRate;
            inv.Freight = invDTO.Freight;
            inv.FreightFC = invDTO.Freight * invDTO.ExchangeRate;
            inv.CustomerId = invDTO.CustomerId;
            inv.CustomerName = invDTO.CustomerName;
            inv.ERPCustomerKey = invDTO.ERPCustomerKey;
            if(invDTO.DocumentDate != null) {
                inv.DocumentDate = invDTO.DocumentDate.Value;
            }
            inv.DueDate = invDTO.DueDate;
            inv.PostingDate = DateTime.UtcNow;
            inv.ShipFromAddress = invDTO.ShipFromAddress;
            inv.ShipToAddress = invDTO.ShipToAddress;
            inv.BillToAddress = invDTO.BillToAddress;
            inv.ContactPerson = invDTO.ContactName;
            //inv.LocalCurrency = invDTO.LocalCurrency;
            int currency = Convert.ToInt32(invDTO.LocalCurrency);
            var invcc = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)currency);
            if(invcc != null) {
                inv.LocalCurrency = invcc.Symbol;
            }
            
            inv.Owner = invDTO.Owner;
            inv.SalesEmployee = invDTO.SalesEmployee;
            inv.ShippingTypeText = invDTO.ShippingTypeText;
            inv.StatusText = invDTO.StatusText;
            inv.Remarks = invDTO.Remarks;

            return inv;
        }

    }
}
