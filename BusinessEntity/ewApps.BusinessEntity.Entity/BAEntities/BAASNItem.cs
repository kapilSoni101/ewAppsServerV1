using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {
    /// <summary>
    /// Represents properties for ASN Item entity.
    /// </summary>
    [Table("BAASNItem", Schema = "be")]
    public class BAASNItem :BaseEntity {

        /// <summary>
        /// Represents the ERP delivery item key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPASNKey {
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
        public Guid ASNId {
            get; set;
        }

        /// <summary>
        /// Represents the ERP   item key.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string ERPItemKey {
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
        /// Represents the   delivery item Quantity.
        /// </summary>
        [Column(TypeName = "decimal (18,5)")]
        public decimal Quantity {
            get; set;
        }
        /// <summary>
        /// Unit of item.
        /// </summary>    
        [MaxLength(20)]
        public string QuantityUnit {
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
        /// Represents the   delivery item UnitPrice.
        /// </summary>
        [MaxLength(20)]
        public string Unit {
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
        /// Discount amount after applied discount on actual amount.
        /// </summary>
       [Column(TypeName = "decimal (18,5)")]
        public decimal DiscountAmount {
            get; set;
        }
        /// <summary>
        /// Represents the ERP delivery   TaxCode.
        /// </summary>
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

        

        /// <summary>
        /// Represents the Serial Or BatchNo
        /// </summary>
        public string SerialOrBatchNo {
            get; set;
        }
        /// <summary>
        /// TotalLC
        /// </summary>
        [Column(TypeName = "decimal (18,9)")]
        public decimal? GLAmount {
            get; set;
        }

        public string Whse {
            get; set;
        }
        /// <summary>
        /// Ship from address.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }


        /// <summary>
        /// ShipToAddress
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
    }
    }
