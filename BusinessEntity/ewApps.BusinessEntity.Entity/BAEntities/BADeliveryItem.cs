using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Represents the delivery item related properties.
    /// </summary>
    [Table("BADeliveryItem", Schema = "be")]
    public class BADeliveryItem:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BADeliveryItem";

        /// <summary>
        /// Represents the ERP delivery item key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPDeliveryKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP contract key
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery id
        /// </summary>
        [Required]
        public Guid DeliveryID {
            get; set;
        }

        /// <summary>
        /// Represents the ERP   item key.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// Represents the lot number.
        /// </summary>
        [MaxLength(20)]
        public string LotNo {
            get; set;
        }

        /// <summary>
        /// Represents the  item id.
        /// </summary>
        [Required]
        public Guid ItemID {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item type.
        /// </summary>
        public int ItemType {
            get; set;
        }

        /// <summary>
        /// Represents the  Item Type Text
        /// </summary>
        [MaxLength(20)]
        public string ItemTypeText {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item Quantity.
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal UnitPriceFC {
            get; set;
        }


        /// <summary>
        /// Represents the   delivery item DiscountPercent.
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery   TaxCode.
        /// </summary>
        [MaxLength(20)]
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TaxPercent
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal TaxPercent {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TotalLC
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal TotalLC {
            get; set;
        }

        /// <summary>
        /// Represents the   delivery TotalLC
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal TotalLCFC {
            get; set;
        }

        /// <summary>
        /// Represents the Blanket Agreement No
        /// </summary>
        [MaxLength(100)]
        public string BlanketAgreementNo {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery Freight.
        /// </summary>
        [Column(TypeName = "decimal (18,9)")]
        public decimal Freight {
            get; set;
        }

        /// <summary>
        /// Represents the ERP delivery Freight.
        /// </summary>
        [Column(TypeName = "decimal (18,9)")]
        public decimal FreightFC {
            get; set;
        }

        ///// <summary>
        ///// Represents the ERP Delivery ItemKey 
        ///// </summary>
        //[Required]
        //[MaxLength(100)]
        //public string ERPDeliveryItemKey {
        //  get; set;
        //}

        /// <summary>
        /// Represents the Serial Or BatchNo
        /// </summary>
        [MaxLength(100)]
        public string SerialOrBatchNo {
            get; set;
        }

    }
}


//ID
//ERPDeliveryItemKey
//ERPContractKey
//DeliveryID
//ERPDeliveryKey
//ERPItemKey
//SerialOrBatchNo
//ItemID
//ItemName
//* ItemType
//Quantity
//UnitPrice
//DiscountPercent
//TaxCode
//TaxPercent
//TotalLC
//BlanketAgreementNo
//Freight
