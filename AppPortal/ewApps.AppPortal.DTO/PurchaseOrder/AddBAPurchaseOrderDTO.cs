using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DTO {

    public class AddBAPurchaseOrderDTO {

        /// <summary>
        /// Represents the ERPSalesOrderKey.
        /// </summary>
        public string ERPSalesOrderKey {
            get; set;
        }

        /// <summary>
        /// Represents the Vendor key.
        /// </summary>
        public string ERPVendorKey {
            get; set;
        }

        /// <summary>
        /// Represents the Vendor Id.
        /// </summary>
        public Guid VendorID {
            get; set;
        }

        /// <summary>
        /// Represents the customer name.
        /// </summary>
        public string VendorName {
            get; set;
        }

        /// <summary>
        /// Represents the contact person name of customer.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Represents the customer ref no.
        /// </summary>
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Represents the LocalCurrency.
        /// </summary> 
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Represents the status.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Represents the status text.
        /// </summary>
        public string StatusText {
            get; set;
        }

        /// <summary>
        /// Represents the posting date of purchase inquiry.
        /// </summary>
        public DateTime PostingDate {
            get; set;
        }

        /// <summary>
        /// Represents the delivery date of purchase inquiry.
        /// </summary>
        public DateTime DeliveryDate {
            get; set;
        }

        /// <summary>
        /// Represents the document date
        /// </summary>
        public DateTime DocumentDate {
            get; set;
        }

        /// <summary>
        /// Represents the PickAndPackRemarks
        /// </summary>
        public string PickAndPackRemarks {
            get; set;
        }

        /// <summary>
        /// Represents the SalesEmployee
        /// </summary>
        public string SalesEmployee {
            get; set;
        }
        /// <summary>
        /// Represents the owner
        /// </summary>
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Represents the TotalBeforeDiscount
        /// </summary>
        public decimal TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Represents the Discount
        /// </summary>
        public decimal Discount {
            get; set;
        }

        /// <summary>
        /// Represents the Freight
        /// </summary>
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Represents the TotalPaymentDue
        /// </summary>
        public decimal TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Represents the ShipFromAddress
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShipFromAddressKey
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERPShipToAddressKey
        /// </summary>
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ShipToAddress
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ERPBillToAddressKey
        /// </summary>
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the BillToAddress
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Purcahse order item list.
        /// </summary>
        public List<BAPurchaseOrderItemDTO> OrderItemList {
            get; set;
        }

        public List<AddUpdateDocumentModel> listAttachment {
            get; set;
        }

        public List<NotesAddDTO> NotesList {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingText
        /// </summary>
        public string ShippingText {
            get; set;
        }

    }

}
