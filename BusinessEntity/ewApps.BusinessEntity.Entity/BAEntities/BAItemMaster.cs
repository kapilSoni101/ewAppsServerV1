/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 08 August 2019
 */

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    /// <summary>
    /// Represents properties for ItemMaster entity.
    /// </summary>
    [Table("BAItemMaster", Schema = "be")]
    public class BAItemMaster:BaseEntity {

        /// <summary>
        /// SAP connector key .
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// SAP item key .
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string ItemType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string BarCode {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal Price {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PriceFC {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PriceUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PriceUniText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ShippingType {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string ShippingTypeText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string ManagedItem {
            get; set;
        }

        /// <summary>
        /// Application active flag.
        /// </summary>
        [MaxLength(20)]
        public string Active {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PurchaseLength {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseLengthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PurchaseLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PurchaseWidth {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseWidthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PurchaseWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PurchaseHeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseHeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PurchaseHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PurchaseVolume {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseVolumeUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PurchaseVolumeUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal PurchaseWeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PurchaseWeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string PurchaseWeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal SalesLength {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesLengthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string SalesLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal SalesWidth {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesWidthUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string SalesWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal SalesHeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesHeightUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string SalesHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal SalesVolume {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesVolumeUnit {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(20)]
        public string SalesVolumeUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(18,5)")]
        public decimal SalesWeight {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SalesWeightUnit {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        [MaxLength(20)]
        public string SalesWeightUnitText {
            get; set;
        }

        /// <summary>
        ///
        /// </summary>
        [MaxLength(100)]
        public string Remarks {
            get; set;
        }

    }
}
