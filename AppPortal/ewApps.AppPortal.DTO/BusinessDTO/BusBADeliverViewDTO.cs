using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.AppPortal.DTO {
    public class BusBADeliveryViewDTO {

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery key.
        /// </summary>
        public string ERPDeliveryKey {
            get; set;
        }


        //Missing
        [NotMapped]
        public string ERPConnectorKey {
            get; set;
        }



        /// <summary>
        /// Represents the ERP customer key.
        /// </summary>
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Represents the cust id
        /// </summary>
        public Guid CustomerID {
            get; set;
        }

        /// <summary>
        /// Represents the customer name.
        /// </summary>
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Represents the contact person name of customer.
        /// </summary>
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Represents the  Customer Referenvce No.
        /// </summary>
        public string CustomerRefNo {
            get; set;
        }

        // UpdatedOn, UpdatedBy, BillToAddress, Status, StatusText
        /// <summary>
        /// Represents the LocalCurrency
        /// </summary>
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Represents the posting date of delivery.
        /// </summary>
        public DateTime? PostingDate {
            get; set;
        }

        /// <summary>
        /// Represents the DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate {
            get; set;
        }

        /// <summary>
        /// Represents the DocumentDate
        /// </summary>
        public DateTime? DocumentDate {
            get; set;
        }

        /// <summary>
        /// The created Date-Time (in UTC).
        /// </summary>
        public DateTime CreatedOn {
            get; set;
        }

        /// <summary>
        /// The name of the created by.
        /// </summary>
        public string CreatedByName {
            get; set;
        }

        /// <summary>
        /// The updated on date-time (in UTC).
        /// </summary>
        public DateTime UpdatedOn {
            get; set;
        }

        /// <summary>
        /// The name of the updated by user.
        /// </summary>
        public string UpdatedByName {
            get; set;
        }

        /// <summary>
        /// The bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the delivery status.
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
        /// Represents the  Total amount Before Discount
        /// </summary>
        public decimal? TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Represents the Discount amount
        /// </summary>
        public decimal? Discount {
            get; set;
        }

        /// <summary>
        /// Represents the Freight
        /// </summary>
        public decimal? Freight {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Represents the Total Paymen tDue
        /// </summary>
        public decimal? TotalPaymentDue {
            get; set;
        }


        /// <summary>
        /// The ship from address key.
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// The ship from address.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Represents the TrackingNo.
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType text.
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// Represents the SalesEmployee
        /// </summary>
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Represents the Owner
        /// </summary>
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// The ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        public string ERPShipToAddressKey {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// Delivery item list.
        /// </summary>
        [NotMapped]
        public List<BusBADeliveryItemDTO> DeliveryItemList {
            get; set;
        }

        [NotMapped]
        public List<NotesViewDTO> NotesList {
            get; set;
        }

        [NotMapped]
        public List<BusBADeliveryAttachmentDTO> DeliveryAttachmentList {
            get; set;
        }

    }
}
