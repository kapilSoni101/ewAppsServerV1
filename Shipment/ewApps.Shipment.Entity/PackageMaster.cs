/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Kuldeep Singh Chauhan <kchauhan@eworkplaceapps.com>
 * Date: 08 Aug 2019
 */


using ewApps.Core.BaseService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Shipment.Entity {

    /// <summary>
    /// PackageMaster table represting all Shipment Package Master Data Information.
    /// </summary>
    [Table("PackageMaster", Schema = "ship")]
    public class PackageMaster:BaseEntity {

        /// <summary>
        /// Package Refrence Id 
        /// </summary>
        [MaxLength(100)]
        //PkgRefId
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// Name of Package 
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string PkgName {
            get; set;
        }

        /// <summary>
        /// The Weight of Package  
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,5)")]
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
        [Column(TypeName = "decimal(18,5)")]
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
        [Column(TypeName = "decimal(18,5)")]
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
        [Column(TypeName = "decimal(18,5)")]
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

        /// <summary>
        /// The Status of Package  
        /// </summary>
        [Required]
        public bool Status {
            get; set;
        }
    }
}
