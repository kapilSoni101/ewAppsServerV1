using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    public class BusBASalesQuotationViewDTO {

        /// <summary>        
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>        
        public Guid ID {
            get; set;
        }


        /// <summary>
        /// ERPSalesQuotationKey will be not null if comingfrom ERP connector.
        /// It unique id of SalesQuotation table of ERP connector table.
        /// </summary>        
        public string ERPSalesQuotationKey {
            get; set;
        }

        /// <summary>
        /// ERPCustomerKey will be not null if coming from ERP connector.
        /// It unique id of Customer table of ERP connector table.
        /// </summary>

        public string ERPCustomerKey {
            get; set;
        }

        public Guid CustomerId {
            get; set;
        }

        /// <summary>
        /// Customer name.
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Name of Contact person.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Reference id of customer.
        /// </summary>
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Local currency.
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Posting date.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// DocumentDate
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        public DateTime CreatedOn {
            get; set;
        }

        public string CreatedByName {
            get; set;
        }

        public DateTime UpdatedOn {
            get; set;
        }

        public string UpdatedByName {
            get; set;
        }

        /// <summary>
        /// bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Current status of SalesQuotation.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Status display text.
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Total amount before discount.
        /// </summary>
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Discount on  quote.
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Frright charges.
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Tax on quote.
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Total payment due.
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// ShipFromAddressKey will be not null if coming from ERP connector.
        /// </summary>        
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// ShipFromAddress 
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }


        /// <summary>
        /// TYpe of shipping.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// shipping text.
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }


        /// <summary>
        /// Sales Employees.
        /// </summary>
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Comment on notes.
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// ERPBillToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address.
        /// </summary>
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// ERP connector key.        
        /// </summary>        
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// ERPShipToAddressKey will be not null if coming from ERP connector.
        /// It unique id of address table of ERP connector table.
        /// </summary>        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// SalesQuote Valid untill.
        /// </summary>
        public DateTime ValidUntil {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// sales order item list.
        /// </summary>
        [NotMapped]
        public List<BusBASalesQuotationItemDTO> ItemList {
            get; set;
        }

        [NotMapped]
        public IEnumerable<BusBASalesQuotationAttachmentDTO> AttachmentList {
            get; set;
        }

    }
}
