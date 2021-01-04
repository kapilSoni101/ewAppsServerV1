/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 8 august  2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 8 august  2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    //ToDo: Nitin: Review: All properties comments are not proper.
    // like Name= Platform company name.
    // CurrencyCode=> Platform company currency to be use to display amount.
    /// <summary>
    /// Platform table is plaform extenstion table.
    /// //ToDo: Nitin: Review: LIke This entity contains info extented information of Platform tenant.
    /// </summary>
    [Table("Platform", Schema = "ap")]
    public class Platform:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "Platform";

        /// <summary>
        /// Name of the platform.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// CurrencyCode  for the platform
        /// </summary>
        [Required]
        public int CurrencyCode {
            get; set;
        }

        /// <summary>
        /// Group Value related to currency.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string GroupValue {
            get; set;
        }

        /// <summary>
        /// Group Seperator value related to currency.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        /// Decimal Seperator related to currency.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        /// Decimal Precision related to currency.
        /// </summary>
        [Required]
        public int DecimalPrecision {
            get; set;
        }

        /// <summary>
        /// Can Update Currency flag for the currency.
        /// </summary>
        [Required]
        public bool CanUpdateCurrency {
            get; set;
        }


        /// <summary>
        /// Tenant Logo ThumbnailId.
        /// </summary>
        public Guid LogoThumbnailId {
            get;
            set;
        }


        /// <summary>
        /// Tenant language for the platform.
        /// </summary>
        [MaxLength(20)]
        public string Language {
            get;
            set;
        }

        /// <summary>
        /// Tenant Time Zone  for the platform.
        /// </summary>
        [MaxLength(20)]
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// Tenant DateTime format for the platform.
        /// </summary>
        [MaxLength(100)]
        public string DateTimeFormat {
            get;
            set;
        }

        /// <summary>
        /// Powered By for the platform.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string PoweredBy {
            get; set;
        }

        /// <summary>
        /// Tenant Copyright for the platform.
        /// </summary>
        [MaxLength(100)]
        public string Copyright {
            get;
            set;
        }

    }
}
