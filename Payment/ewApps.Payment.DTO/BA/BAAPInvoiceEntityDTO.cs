using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DTO {
    /// <summary>
    /// Invoice table storing all the invoice data.
    /// </summary>
    public class BAAPInvoiceEntityDTO:BaseDTO {

        public new Guid ID {
            get;
            set;
        }

        /// <summary>
        /// Unique key of invoice (Invoice generated from ERP).
        /// </summary>

        public string ERPAPInvoiceKey {
            get; set;
        }

        /// <summary>
        /// Connector key of invoice (Invoice generated from ERP connector name).
        /// </summary>
        
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Unique key of Vendor (Vendor generated from ERP portal).
        /// </summary>

        public string ERPVendorKey {
            get; set;
        }

        /// <summary>
        /// Vendor table unique id.
        /// </summary>        
        public Guid VendorId {
            get; set;
        }

        /// <summary>
        /// Name of customer.
        /// </summary>
        
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Name of conatct person.
        /// </summary>
        
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Local currency from where invoice generated.
        /// </summary>
        
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Status of invoice.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Currenct status test.
        /// </summary>
        
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Invoice posting date. 
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Document date.
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Sales Employees.
        /// </summary>
        
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Owner of invoice.
        /// </summary>
        
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Invoice actual amount without discount.
        /// </summary>
        
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Invoice actual amount without discount.
        /// </summary>
        
        public decimal TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Discount apply on Invoice.
        /// </summary>
        
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Discount apply on Invoice.
        /// </summary>
        
        public decimal DiscountFC {
            get; set;
        }

        /// <summary>
        /// Freight charges apply on Invoice.
        /// </summary>
        
        public decimal Freight {
            get; set;
        }
        /// <summary>
        /// Freight charges apply on Invoice.
        /// </summary>
        
        public decimal FreightFC {
            get; set;
        }

        /// <summary>
        /// Tax charges apply on Invoice.
        /// </summary>
        
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Tax charges apply on Invoice.
        /// </summary>
        
        public decimal TaxFC {
            get; set;
        }

        /// <summary>
        /// Due amount on Invoice.
        /// </summary>
        
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Due amount on Invoice.
        /// </summary>
        
        public decimal TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Appled amount.
        /// </summary>
        
        public decimal AppliedAmount {
            get; set;
        }

        /// <summary>
        /// Appled amount.
        /// </summary>
        
        public decimal AppliedAmountFC {
            get; set;
        }

        /// <summary>
        /// Balanace due on invoice.
        /// </summary>
        
        public decimal BalanceDue {
            get; set;
        }

        /// <summary>
        /// Balanace due on invoice.
        /// </summary>
        
        public decimal BalanceDueFC {
            get; set;
        }

        /// <summary>
        /// Note for invoice.
        /// </summary>
        
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Ship from address key, coming from ERP connector.
        /// </summary>
        
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Ship to address key, coming from ERP connector.
        /// </summary>
        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address key.
        /// </summary>
        
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Shipping Type.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Shipping type text.
        /// </summary>
        
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// Invoice tracking number.
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        public new Guid UpdatedBy {
            get;
            set;
        }

    }
}
