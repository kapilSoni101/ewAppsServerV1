/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 07 July 2019
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BASalesQuotationItem", Schema = "be")]
    public class BASalesQuotationItem:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BASalesQuotationItem";

        [MaxLength(100)]
        /// <summary>
        /// ERPSalesQuotationItemKey will be not null if coming from ERP connector.
        /// </summary>
        public string ERPSalesQuotationItemKey {
            get; set;
        }

        /// <summary>        
        /// ERPConnectorKey will be not null if coming from ERP connector.        
        /// </summary>
        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        [MaxLength(20)]
        public string LotNo {
            get; set;
        }

        /// <summary>
        /// Unique id of SalesQuotationId.
        /// </summary>
        [Required]
        public Guid SalesQuotationId {
            get; set;
        }

        /// <summary>
        /// ERPSalesQuotationKey will be not null if coming from ERP connector. Unique id of  ERPSalesQuotationKey.
        /// </summary>
        [MaxLength(100)]
        public string ERPSalesQuotationKey {
            get; set;
        }

        /// <summary>
        /// ERPItemKey will be not null if coming from ERP connector. Unique id of ERPItemKey.
        /// </summary>
        [MaxLength(100)]
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// Unique id of item master.
        /// </summary>
        [Required]
        public Guid ItemId {
            get; set;
        }

        /// <summary>
        /// SerialOrBatchNo
        /// </summary>
        [MaxLength(100)]
        public string SerialOrBatchNo {
            get; set;
        }

        /// <summary>
        /// Name of item master
        /// </summary>
        [MaxLength(100)]
        public string ItemName {
            get; set;
        }

        [Column(TypeName = "decimal(18, 5)")]
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// QuantityUnit
        /// </summary>
        [MaxLength(100)]
        public string QuantityUnit {
            get; set;
        }

        /// <summary>
        /// Unit price.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Unit price.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal UnitPriceFC {
            get; set;
        }

        /// <summary>
        /// Cost per unit.
        /// </summary>
        [MaxLength(100)]
        public string Unit {
            get; set;
        }

        /// <summary>
        /// Discount percent.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Discount amount on total cost.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountAmount {
            get; set;
        }

        /// <summary>
        /// Discount amount on total cost.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountAmountFC {
            get; set;
        }

        /// <summary>
        /// Tax code.
        /// </summary>
        [MaxLength(100)]
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Tax percent apply on cost.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TaxPercent {
            get; set;
        }

        /// <summary>
        /// TotalLC
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalLC {
            get; set;
        }

        /// <summary>
        /// TotalLC
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal TotalLCFC {
            get; set;
        }

        /// <summary>
        /// GLAccount.
        /// </summary>
        [MaxLength(100)]
        public string GLAccount {
            get; set;
        }

        /// <summary>
        /// BlanketAgreementNo
        /// </summary>
        [MaxLength(100)]
        public string BlanketAgreementNo {
            get; set;
        }

        /// <summary>
        /// Ship from address.
        /// </summary>
        [MaxLength(4000)]
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// ShipFromAddressKey not null if coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ShipFromAddressKey {
            get; set;
        }

        [MaxLength(100)]
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to address key, Will be non nullable if coming from ERP.
        /// </summary>
        [MaxLength(100)]
        public string ERPBillToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        [MaxLength(4000)]
        public string BillToAddress {
            get; set;
        }


    }

}
