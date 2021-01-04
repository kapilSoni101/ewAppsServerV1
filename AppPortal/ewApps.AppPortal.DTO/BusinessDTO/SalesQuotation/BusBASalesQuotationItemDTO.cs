/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 30 September 2018
 */


namespace ewApps.AppPortal.DTO {

    public class BusBASalesQuotationItemDTO {
        
        /// <summary>
        /// ERPSalesQuotationItemKey will be not null if coming from ERP connector.
        /// </summary>
        public string ERPSalesQuotationItemKey {
            get; set;
        }

        /// <summary>        
        /// ERPConnectorKey will be not null if coming from ERP connector.        
        /// </summary>        
        public string ERPConnectorKey {
            get; set;
        }

        /// <summary>
        /// ERPSalesQuotationKey will be not null if coming from ERP connector. Unique id of  ERPSalesQuotationKey.
        /// </summary>
        
        public string ERPSalesQuotationKey {
            get; set;
        }

        /// <summary>
        /// ERPItemKey will be not null if coming from ERP connector. Unique id of ERPItemKey.
        /// </summary>
        
        public string ERPItemKey {
            get; set;
        }


        /// <summary>
        /// SerialOrBatchNo
        /// </summary>        
        public string SerialOrBatchNo {
            get; set;
        }

        /// <summary>
        /// Name of item master
        /// </summary>
        
        public string ItemName {
            get; set;
        }

        public decimal Quantity {
            get; set;
        }

        /// <summary>
        /// QuantityUnit
        /// </summary>        
        public string QuantityUnit {
            get; set;
        }

        /// <summary>
        /// Unit price.
        /// </summary>
        public decimal UnitPrice {
            get; set;
        }

        /// <summary>
        /// Cost per unit.
        /// </summary>        
        public string Unit {
            get; set;
        }

        /// <summary>
        /// Discount percent.
        /// </summary>
        public decimal DiscountPercent {
            get; set;
        }

        /// <summary>
        /// Discount amount on total cost.
        /// </summary>
        public decimal DiscountAmount {
            get; set;
        }

        /// <summary>
        /// Tax code.
        /// </summary>
        
        public string TaxCode {
            get; set;
        }

        /// <summary>
        /// Tax percent apply on cost.
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

        /// <summary>
        /// GLAccount.
        /// </summary>        
        public string GLAccount {
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
        /// ShipFromAddressKey not null if coming from ERP connector.
        /// </summary>
        public string ShipFromAddressKey {
            get; set;
        }
        
        public string ERPShipToAddressKey {
            get; set;
        }

        /// <summary>
        /// Ship to address.
        /// </summary>
        public string ShipToAddress {
            get; set;
        }

        /// <summary>
        /// Bill to address key, Will be non nullable if coming from ERP.
        /// </summary>        
        public string ERPBillToAddressKey {
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
