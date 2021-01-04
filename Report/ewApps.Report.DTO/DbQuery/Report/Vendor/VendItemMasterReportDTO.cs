/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Report.DTO {

    /// <summary>
    /// This class is a DTO with consolidate information of <see cref="VendItemMasterReportDTO"/> .
    /// </summary>
    public class VendItemMasterReportDTO {       
             

        /// <summary>
        /// The unique identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ERPItemKey {
            get; set;
        }

        /// <summary>
        /// Name of Item.
        /// </summary>
        public string ItemName {
            get; set;
        }

        /// <summary>
        ///Name of The person who managed item.
        /// </summary>
        public string ManagedItem {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string PurchaseLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string PurchaseWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string PurchaseHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string PurchaseWeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string SalesLengthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string SalesWidthUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string SalesHeightUnitText {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
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
        public string SalesWeightUnitText {
            get; set;
        }

        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks {
            get; set;
        }

        /// <summary>
        /// Application active flag.
        /// </summary>
        public string Active {
            get; set;
        }

        /// <summary>
        /// Application delete flag.
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// Preferred Vendor.
        /// </summary>
        public string PreferredVendor {
            get; set;
        }

        /// <summary>
        /// Size of all dimensions
        /// </summary>
        [NotMapped]
        public string Size {
            get; set;
        }
    }
}
