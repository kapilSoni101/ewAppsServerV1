using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.BusinessEntity.DTO {
    /// <summary>
    /// This class contains business application delivery entity information for business.
    /// </summary>
    public class CustBADeliveryDTO
  {
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

        /// <summary>
        /// Represents the ERP connctor key.
        /// </summary>
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

        /// <summary>
        /// Represents the LocalCurrency
        /// </summary>
        public string LocalCurrency {
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
        /// Represents the  Total amount Before Discount
        /// </summary>
        public decimal? TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Represents the  Total amount Before Discount fc
        /// </summary>
        public decimal? TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Represents the Discount amount
        /// </summary>
        public decimal? Discount {
            get; set;
        }
        /// <summary>
        /// Represents the Discount amount
        /// </summary>
        public decimal? DiscountFC {
            get; set;
        }

        /// <summary>
        /// Represents the Freight
        /// </summary>
        public decimal? Freight {
            get; set;
        }

        // <summary>
        /// Represents the Freight
        /// </summary>
        public decimal? FreightFC {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        public decimal? TaxFC {
            get; set;
        }

        /// <summary>
        /// Represents the Total Paymen tDue
        /// </summary>
        public decimal? TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Represents the Total PaymentDue
        /// </summary>
        public decimal? TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Represents the ShipToAddress
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the   ERP Bill To AddressKey
        /// </summary>
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the Bill To Address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType.
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType text.
        /// </summary>
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// Represents the TrackingNo.
        /// </summary>
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Represents the StampNo.
        /// </summary>
        public string StampNo {
            get; set;
        }

        /// <summary>
        /// Represents the PickAndPackRemarks
        /// </summary>
        public string PickAndPackRemarks {
            get; set;
        }

        /// <summary>
        /// Represents the ERP connctor key.
        /// </summary>
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ship from addrses.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ship from address key.
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }

        public string ERPDocNum {
            get; set;
        }


        /// <summary>
        /// sales order item list.
        /// </summary>
        [NotMapped]
        public List<BADeliveryItemSyncDTO> DeliveryItemList {
            get; set;
        }

    }
}
