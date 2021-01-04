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

namespace ewApps.AppMgmt.Entity {

    /// <summary>
    /// Tenant user table storing all the user related data.
    /// </summary>
    [Table("TenantUser", Schema = "am")]
    public class TenantUser:BaseEntity {

        public const string EntityName = "TenantUser";

        /// <summary>
        /// Identity user id for user identification on identity server foreign key in ASPNetUsers.
        /// </summary>
        [Required]
        public Guid IdentityUserId {
            get; set;
        }

        /// <summary>
        /// FirstName of the user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName {
            get; set;
        }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName {
            get; set;
        }

        /// <summary>
        /// Fullname of the user.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string FullName {
            get; set;
        }

        /// <summary>
        /// Email of the user.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email {
            get; set;
        }

        /// <summary>
        /// Identity user code.
        /// </summary>
        [MaxLength(4000)]
        public string Code {
            get; set;
        }

        /// <summary>
        /// Identity number id used for UI .
        /// </summary>
        [MaxLength(20)]
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        [MaxLength(20)]
        public string Phone {
            get; set;
        }

        /// <summary>
        /// TenantId identifier - not required column in TenantUser Table.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get;
            set;
        }

        #region Validation

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        private IEnumerable<EwpErrorData> BrokenRules(TenantUser entity) {
            //Check for employee's first name is required.
            if(string.IsNullOrEmpty(entity.FirstName)) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "First Name",
                    Message = "User FirstName is required."
                };
            }
            if(string.IsNullOrEmpty(entity.Email)) {
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Email",
                    Message = "User email is required."
                };
            }
        }

        #endregion Validation

    }
}