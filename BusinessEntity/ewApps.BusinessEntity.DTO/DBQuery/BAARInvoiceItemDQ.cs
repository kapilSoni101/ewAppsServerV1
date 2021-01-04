/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: 
 * Last Updated On: 26 December 2018
 */

using System;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DTO {

    /// <summary>
    /// Invoice  details Data trasnfer object.
    /// </summary>
    public class BAARInvoiceItemDQ {

        public Guid ID {
            get; set;
        }

        public string LotNo {
            get; set;
        }

        public Guid ARInvoiceID {
            get; set;
        }

        public string SerialOrBatchNo {
            get; set;
        }

        public Guid ItemId {
            get; set;
        }

        public string ItemName {
            get; set;
        }

        public string ItemType {
            get; set;
        }

        public decimal Quantity {
            get; set;
        }

        public string QuantityUnit {
            get; set;
        }

        public decimal UnitPrice {
            get; set;
        }

        public decimal UnitPriceFC {
            get; set;
        }

        public string Unit {
            get; set;
        }

        public decimal DiscountPercent {
            get; set;
        }

        public decimal DiscountAmount {
            get; set;
        }

        public decimal DiscountAmountFC {
            get; set;
        }

        public string TaxCode {
            get; set;
        }
        public decimal TaxPercent {
            get; set;
        }
        public decimal TotalLC {
            get; set;
        }
        public decimal TotalLCFC {
            get; set;
        }

        /// <summary>
        /// Price calculate as per exchange rate.
        /// </summary>
        [NotMapped]
        public decimal CalculateItemPrice {
            get; set;
        }

        /// <summary>
        /// ItemTotalPrice calculate as per exchange rate.
        /// </summary>
        [NotMapped]
        public decimal CalculateItemTotatPrice {
            get; set;
        }

        #region Address

        /// <summary>
        /// Ship from address
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// Ship to address.
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

        #endregion Address

        /// <summary>
        /// Map entity property to dto object.
        /// </summary>
        /// <param name="item"></param>
        public void MapPropertiesFromEntity(BAARInvoiceItem item) {
            this.ID = item.ID;
            this.ARInvoiceID = item.ARInvoiceID;
            this.UnitPrice = item.UnitPrice;
            this.UnitPriceFC = item.UnitPriceFC;
            this.Quantity = item.Quantity;            
            this.TaxPercent = item.TaxPercent;
            this.DiscountAmount = item.DiscountAmount;
            this.DiscountAmountFC = item.DiscountAmountFC;
            this.DiscountAmountFC = item.DiscountAmountFC;
        }

    }
}
