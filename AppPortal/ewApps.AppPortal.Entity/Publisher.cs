/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;

namespace ewApps.AppPortal.Entity {

    /// <summary>
    /// Publisher table represting all the Publisher.
    /// </summary>
    [Table("Publisher", Schema = "ap")]
    public class Publisher:BaseEntity {

        /// <summary>
        /// The entity name publisher.
        /// </summary>
        public const string EntityName = "Publisher";

        /// <summary>
        /// The name  of the Publisher.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Identity number for ui of the publisher.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// Publisher active status identifier.
        /// </summary>
        [Required]
        public bool Active {
            get; set;
        }

        /// <summary>
        /// Publisher Inactive Comment when publisher is inactive.
        /// </summary>
        [MaxLength(4000)]
        public string InactiveComment {
            get;
            set;
        }

        /// <summary>
        /// Contact PersonName of the publisher admin user.
        /// </summary>
        [MaxLength(100)]
        public string ContactPersonName {
            get;
            set;
        }

        /// <summary>
        /// Contact Person Designation of the publisher admin user.
        /// </summary>
        [MaxLength(100)]
        public string ContactPersonDesignation {
            get;
            set;
        }

        /// <summary>
        /// Contact Person Email of the publisher admin user.
        /// </summary>
        [MaxLength(100)]
        public string ContactPersonEmail {
            get;
            set;
        }

        /// <summary>
        /// Contact Person Phone of the publisher admin user.
        /// </summary>
        [MaxLength(20)]
        public string ContactPersonPhone {
            get;
            set;
        }

        /// <summary>
        /// Website of the publisher.
        /// </summary>
        [MaxLength(100)]
        public string Website {
            get;
            set;
        }


        /// <summary>
        /// Currency Code related to currency of publisher.
        /// </summary>
        public int? CurrencyCode {
            get; set;
        }

        /// <summary>
        /// Group Value related to currency of publisher.
        /// </summary>
        [MaxLength(20)]
        public string GroupValue {
            get; set;
        }

        /// <summary>
        /// Group seperator related to currency of publisher.
        /// </summary>
        [MaxLength(20)]
        public string GroupSeperator {
            get; set;
        }

        /// <summary>
        /// Decimal Seperator related to currency of publisher.
        /// </summary>
        [MaxLength(20)]
        public string DecimalSeperator {
            get; set;
        }

        /// <summary>
        /// Decimal Precision related to currency of publisher.
        /// </summary>
        public int? DecimalPrecision {
            get; set;
        }

        /// <summary>
        /// Can Update Currency related to currency of publisher.
        /// </summary>
        public bool? CanUpdateCurrency {
            get; set;
        }

        /// <summary>
        /// Tenant LogoThumbnailId  
        /// </summary>
        public Guid LogoThumbnailId {
            get;
            set;
        }

        /// <summary>
        /// Tenant language of publisher.
        /// </summary>
        [MaxLength(20)]
        public string Language {
            get;
            set;
        }

        /// <summary>
        /// Tenant Time Zone of the publisher.
        /// </summary>
        [MaxLength(20)]
        public string TimeZone {
            get;
            set;
        }

        /// <summary>
        /// Tenant DateTime format of the publisher.
        /// </summary>
        [MaxLength(20)]
        public string DateTimeFormat {
            get;
            set;
        }

        /// <summary>
        /// Powered By of the publisher.
        /// </summary>
        [MaxLength(100)]
        public string PoweredBy {
            get; set;
        }

        /// <summary>
        /// Tenant Copyright of the publisher.
        /// </summary>
        [MaxLength(100)]
        public string Copyright {
            get;
            set;
        }

        /// <summary>
        /// Publisher Customerzied Logo Thumbnail Flag.
        /// </summary>       
        public bool CustomizedLogoThumbnail {
            get; set;
        }

        /// <summary>
        /// Publisher Apply Powered By Flag.
        /// </summary>        
        public bool ApplyPoweredBy {
            get; set;
        }

        /// <summary>
        /// CanUpdateCopyRight Flag Indicate The Right Of Publisher To change CopyRight Tag.
        /// </summary>        
        public bool CanUpdateCopyright {
            get; set;
        }

        /// <summary>
        /// CustomizedCopyright flag is use to check Copyright is shown to its own publisher Copyright or platform copyright
        /// </summary>
        [Required]
        public bool CustomizedCopyright {
            get; set;
        }

        #region IValidator<Publisher> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(Publisher entity) {
            if(string.IsNullOrEmpty(entity.Name))
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Name",
                    Message = "Publisher name is required."
                };
        }

        #endregion

    }
}
