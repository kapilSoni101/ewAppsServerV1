/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ewApps.AppMgmt.Entity {

    [Table("Tenant", Schema = "am")]
    public class Tenant:BaseEntity {

        [Required]
        public string IdentityNumber {
            get; set;
        }

        [MaxLength(4000)]
        [Required]
        public string Name {
            get; set;
        }

        [MaxLength(100)]
        public string VarId {
            get; set;
        }

        [MaxLength(50)]
        [Required]
        public string SubDomainName {
            get; set;
        }

        public string LogoUrl {
            get; set;
        }

        [MaxLength(100)]
        [Required]
        public string Language {
            get; set;
        }

        [MaxLength(100)]
        [Required]
        public string TimeZone {
            get; set;
        }

        [MaxLength(100)]
        [Required]
        public string Currency {
            get; set;
        }

        [Required]
        public bool Active {
            get; set;
        }

        [Required]
        public int TenantType {
            get; set;
        }

        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }
        /// <summary>
        /// Customer created date.
        /// </summary>
        public DateTime? InvitedOn {
            get; set;
        }
        /// <summary>
        /// null if any application is not assigned.
        /// </summary>
        public DateTime? JoinedOn {
            get; set;
        }
        /// <summary>
        /// Customer invited by name .
        /// </summary>
        public Guid? InvitedBy {
            get; set;
        }
        /*
        public string ContactPersonName { get; set; }

        public string ContactPersonDesignation { get; set; }

        public string ContactPersonPhone { get; set; } 
    */

        #region IValidator<Tenant> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(Tenant entity) {
            //Check for employee's first name is required.
            if(string.IsNullOrEmpty(entity.Name)) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Name",
                    Message = "Tenant name is required."
                };
            }
            if(string.IsNullOrEmpty(entity.SubDomainName)) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Name",
                    Message = "Domain name is required."
                };
            }
        }

        #endregion

    }
}
