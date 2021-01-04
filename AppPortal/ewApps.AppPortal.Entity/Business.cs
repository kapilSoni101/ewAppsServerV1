/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 08 Aug 2019

 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Entity {
    /// <summary>
    /// Theme entity represting all Business properties.
    /// </summary>
    [Table("Business", Schema = "ap")]
    public class Business: BaseEntity {

        [Required]
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        [MaxLength(100)]
        public string ContactPersonEmail {
            get; set;
        }

        [MaxLength(100)]
        public string ContactPersonName {
            get; set;
        }

        [MaxLength(100)]
        public string ContactPersonDesignation {
            get; set;
        }

        [MaxLength(100)]
        public string ContactPersonPhone {
            get; set;
        }
        /// <summary>
        ///CurrencyCode 
        /// </summary>
        [Required]
        public int CurrencyCode {
            get; set;
        }
        /// <summary>
        ///GroupValue
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string GroupValue {
            get; set;
        }

        /// <summary>
        ///Powered By  
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        ///DecimalSeperator 
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string DecimalSeperator {
            get; set;
        }


        /// <summary>
        ///DecimalPrecision
        /// </summary>
        [Required]
        public int DecimalPrecision {
            get; set;
        }

        /// <summary>
        /// CanUpdateCurrency
        /// </summary>
        [Required]
        public bool CanUpdateCurrency {
            get; set;
        }

        /// <summary>
        /// CanUpdateCurrency
        /// </summary>       
        public bool InitDB {
            get; set;
        }        

        /// <summary>
        /// website
        /// </summary>
        [MaxLength(100)]
        public string Website {
            get;
            set;
        }


        /// <summary>
        /// Tenant Language
        /// </summary>
        [MaxLength(100)]
        public string Language {
            get;
            set;
        }

        /// <summary>
        ///  Tenant TimeZone
        /// </summary>
        [MaxLength(100)]
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        ///  Tenant DateTimeFormat
        /// </summary>
        public string DateTimeFormat {
            get;
            set;
        }

        /// <summary>
        /// IdentityNumber
        /// </summary>
        [MaxLength(100)]
        public string IdentityNumber {
            get;
            set;
        }

        /// <summary>
        /// Tenant LogoThumbnailId  
        /// </summary>
        public Guid LogoThumbnailId {
            get;
            set;
        }

        /// <summary>
        ///  print lable layout
        /// </summary>        
        [MaxLength(100)]
        public string PrintLabelLayout {
            get;
            set;
        }
        /// <summary>
        /// Business default WeightUnit 
        /// </summary>     
        [MaxLength(100)]
        public string WeightUnit {
            get;
            set;
        }
        /// <summary>
        /// Business default SizeUnit
        /// </summary>      
        [MaxLength(100)]
        public string SizeUnit {
            get;
            set;
        }

        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// FederalTexId of Business 
        /// </summary>      
        [MaxLength(100)]
        public string FederalTexId {
            get;
            set;
        }

        /// <summary>
        ///Telephone no 2 .
        /// </summary>
        [MaxLength(100)]
        public string TelePhone1 {
            get; set;
        }

        /// <summary>
        /// Telephone no 2 .
        /// </summary>
        [MaxLength(100)]
        public string TelePhone2 {
            get; set;
        }


        /// <summary>
        /// Phone.
        /// </summary>
        [MaxLength(100)]
        public string MobilePhone {
            get; set;
        }

        /// <summary>
        /// Email.
        /// </summary>
        [MaxLength(100)]
        public string Email {
            get; set;
        }

        /// <summary>
        /// Status of BUsiness
        /// </summary>
        public bool Status {
            get; set;
        }

        /// <summary>
        /// IntegratedMode of Business
        /// </summary>
        public bool IntegratedMode {
            get; set;
        }

        /// <summary>
        /// Configured of Business
        /// </summary>
        public bool Configured {
            get; set;
        }

    }
}
