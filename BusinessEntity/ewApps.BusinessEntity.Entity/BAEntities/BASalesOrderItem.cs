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
using System.Linq;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.BusinessEntity.Entity {

    [Table("BASalesOrderItem", Schema = "be")]
    public class BASalesOrderItem:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "BASalesOrderItem";

        /// <summary>
        /// Unique key of sales order item key coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ERPSalesOrderItemKey {
            get;set;
        }

        /// <summary>
        /// Unique key of ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// Unique key of sales order table.
        /// </summary>
        [Required]
        public Guid SalesOrderId {
            get; set;
        }

        /// <summary>
        /// Unique key of sales order table coming from ERP connector.
        /// </summary>
        public string ERPSalesOrderKey {
            get; set;
        }

        /// <summary>
        /// Unique key of item coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ERPItemKey {
            get; set;
        }

        [MaxLength(20)]
        public string LotNo {
            get; set;
        }

        /// <summary>
        /// Unique key of ItemMaster table.
        /// </summary>
        [Required]
        public Guid ItemId {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string SerialOrBatchNo {
            get; set;
        }        

        /// <summary>
        /// Item name.
        /// </summary>
        [MaxLength(100)]
        public string ItemName {
            get; set;
        }

        /// <summary>
        /// # quanity of item.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Unit of item.
        /// </summary>
        [MaxLength(100)]
        public string QuantityUnit {
            get; set;
        }

        /// <summary>
        /// Price of item.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Price of item.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal UnitPriceFC {
            get; set;
        }

        /// <summary>
        /// Price of unit. It may be like per item/dozen/gram etc.
        /// </summary>
        [MaxLength(100)]
        public string Unit {
            get; set;
        }

        /// <summary>
        /// Discount percent in invoice.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Discount amount after applied discount on actual amount.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountAmount {
            get; set;
        }

        /// <summary>
        /// Discount amount after applied discount on actual amount.
        /// </summary>
        [Column(TypeName = "decimal(18, 5)")]
        public decimal DiscountAmountFC {
            get; set;
        }

        /// <summary>
        ///  Tax code.
        /// </summary>
        [MaxLength(100)]
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Tax persent appled on item.
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

        [MaxLength(100)]
        public string Whse {
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
        /// ShipFromAddressKey will be not null if coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// ShipToAddress
        /// </summary>
        [MaxLength(4000)]
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// ShipTOAddressKey will be not null if coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        [MaxLength(4000)]
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to addressKey will be not null if coming from ERP connector.
        /// </summary>
        [MaxLength(100)]
        public string ERPBillToAddressKey {
            get; set;
        }

        #region IValidator<App> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(BASalesOrderItem entity) {
            //Check for application name is required.
            if(entity.ItemId == Guid.Empty) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Item",
                    Message = "Item is required."
                };
            }

        }

        #endregion

    }
}
