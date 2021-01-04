using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Represents the delivery related properties.
    /// </summary>
    [Table("BADelivery", Schema = "be")]
    public class BADelivery:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BADelivery";

        /// <summary>
        /// Represents the ERP delivery key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPDeliveryKey {
            get; set;
        }

        /// <summary>
        /// Unique number of Deliver.
        /// </summary>
        [MaxLength(100)]
        public string ERPDocNum {
            get; set;
        }

        /// <summary>
        /// Represents the ERP connctor key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP customer key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPCustomerKey {
            get; set;
        }

        /// <summary>
        /// Represents the cust id
        /// </summary>
        [Required]
        [MaxLength(100)]
        public Guid CustomerID {
            get; set;
        }

        /// <summary>
        /// Represents the customer name.
        /// </summry>
        [Required]
        [MaxLength(100)]
        public string CustomerName {
            get; set;
        }

        /// <summary>
        /// Represents the contact person name of customer.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ContactPerson {
            get; set;
        }

        /// <summary>
        /// Represents the  Customer Referenvce No.
        /// </summary>
        [MaxLength(100)]
        public string CustomerRefNo {
            get; set;
        }

        /// <summary>
        /// Represents the LocalCurrency
        /// </summary>
        [MaxLength(3)]
        public string LocalCurrency {
            get; set;
        }

        /// <summary>
        /// Represents the delivery status.
        /// </summary>
        [Required]
        public int Status {
            get; set;
        }

        /// <summary>
        /// Represents the status text.
        /// </summary>
        [Required]
        [MaxLength(20)]
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
        [MaxLength(100)]
        public string SalesEmployee {
            get; set;
        }

        /// <summary>
        /// Represents the Owner
        /// </summary>
        [MaxLength(100)]
        public string Owner {
            get; set;
        }

        /// <summary>
        /// Represents the  Total amount Before Discount
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? TotalBeforeDiscount {
            get; set;
        }

        /// <summary>
        /// Represents the  Total amount Before Discount fc
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? TotalBeforeDiscountFC {
            get; set;
        }

        /// <summary>
        /// Represents the Discount amount
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? Discount {
            get; set;
        }
        /// <summary>
        /// Represents the Discount amount
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? DiscountFC {
            get; set;
        }

        /// <summary>
        /// Represents the Freight
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? Freight {
            get; set;
        }

        // <summary>
        /// Represents the Freight
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? FreightFC {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal Tax {
            get; set;
        }

        /// <summary>
        /// Represents the Tax
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? TaxFC {
            get; set;
        }

        /// <summary>
        /// Represents the Total Paymen tDue
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? TotalPaymentDue {
            get; set;
        }

        /// <summary>
        /// Represents the Total PaymentDue
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal? TotalPaymentDueFC {
            get; set;
        }

        /// <summary>
        /// Represents the Remarks
        /// </summary>
        [MaxLength(4000)]
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Represents the ShipToAddress
        /// </summary>
        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the   ERP Bill To AddressKey
        /// </summary>
        [MaxLength(20)]
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the Bill To Address.
        /// </summary>
        [MaxLength(4000)]
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType.
        /// </summary>
        [Required]
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// Represents the ShippingType text.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// Represents the TrackingNo.
        /// </summary>
        [MaxLength(100)]
        public string TrackingNo {
            get; set;
        }

        /// <summary>
        /// Represents the StampNo.
        /// </summary>
        [MaxLength(100)]
        public string StampNo {
            get; set;
        }

        /// <summary>
        /// Represents the PickAndPackRemarks
        /// </summary>
        [MaxLength(4000)]
        public string PickAndPackRemarks {
            get; set;
        }

        /// <summary>
        /// Represents the ERP connctor key.
        /// </summary>
        [MaxLength(20)]
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Represents the ship from addrses.
        /// </summary>
        [MaxLength(4000)]
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Represents the ship from address key.
        /// </summary>
        [MaxLength(100)]
        public string ShipFromAddressKey {
            get; set;
        }

    }
}
