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

namespace ewApps.AppPortal.DTO {

    public class BASalesOrderItemSyncDTO {

        /// <summary>
        /// Unique key of sales order item key coming from ERP connector.
        /// </summary>        
        public string ERPSalesOrderItemKey {
            get; set;
        }

        /// <summary>
        /// Unique key of ERP connector.
        /// </summary>

        public string ERPConnectorKey {
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
        public string ERPItemKey {
            get; set;
        }

        public string SerialOrBatchNo {
            get; set;
        }

        public string ItemName {
            get; set;
        }

        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// Unit of item.
        /// </summary>        
        public string QuantityUnit {
            get; set;
        }

        /// <summary>
        /// Price of item.
        /// </summary>
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Price of unit. It may be like per item/dozen/gram etc.
        /// </summary>        
        public string Unit {
            get; set;
        }

        /// <summary>
        /// Discount percent in invoice.
        /// </summary>
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Discount amount after applied discount on actual amount.
        /// </summary>
        public decimal DiscountAmount {
            get; set;
        }

        /// <summary>
        ///  Tax code.
        /// </summary>        
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Tax persent appled on item.
        /// </summary>
        public decimal TaxPercent {
            get; set;
        }

        /// <summary>
        /// TotalLC
        /// </summary>
        public decimal TotalLC {
            get; set;
        }

        public string Whse {
            get; set;
        }

        /// <summary>
        /// BlanketAgreementNo
        /// </summary>

        public string BlanketAgreementNo {
            get; set;
        }

        /// <summary>
        /// Ship from address.
        /// </summary>
        public string ShipFromAddress {
            get; set;
        }

        /// <summary>
        /// ShipFromAddressKey will be not null if coming from ERP connector.
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }

        /// <summary>
        /// ShipToAddress
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// ShipTOAddressKey will be not null if coming from ERP connector.
        /// </summary>        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Bill to address.
        /// </summary>
        public string BillToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to addressKey will be not null if coming from ERP connector.
        /// </summary>

        public string ERPBillToAddressKey {
            get; set;
        }

        

    }
}
