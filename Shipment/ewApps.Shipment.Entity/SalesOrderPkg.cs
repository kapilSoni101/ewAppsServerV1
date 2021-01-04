/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 09 April 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 09 April 2019
 */

using ewApps.Core.BaseService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// SalesOrderPkgDetail table represting all Sales Order Package Detail Information.
    /// </summary>
    [Table("SalesOrderPkg", Schema = "ship")]
    public class SalesOrderPkg:BaseEntity {

        /// <summary>
        /// Identity number to Identify Sales Order Detail.
        /// </summary>
        [Required]
        public Guid SalesOrderId {
            get; set;
        }

        /// <summary>
        /// Identity number to Identify Package Detail.
        /// </summary>
        [Required]
        public Guid PackageId {
            get; set;
        }

        public int TotalItems {
            get; set;
        }

        /// <summary>
        /// Name of Package 
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string PkgName {
            get; set;
        }

        /// <summary>
        /// The Weight of Package  
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Weight {
            get; set;
        }


        /// <summary>
        /// The Weight of Unit  
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string WeightUnit {
            get; set;
        }

        /// <summary>
        /// The Height of Package  
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Height {
            get; set;
        }

        /// <summary>
        /// The Height of Unit  
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string HeightUnit {
            get; set;
        }

        /// <summary>
        /// The Width of Package  
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Width {
            get; set;
        }

        /// <summary>
        /// The Width of Unit  
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string WidthUnit {
            get; set;
        }

        /// <summary>
        /// The Length of Package  
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18, 5)")]
        public decimal Length {
            get; set;
        }

        /// <summary>
        /// The Length of Unit  
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LengthUnit {
            get; set;
        }

        [MaxLength(100)]
        public string CarrierPackageCode {
            get;set;
        }

        //public int TotalQuantity {
        //  get; set;
        //}

    }
}
