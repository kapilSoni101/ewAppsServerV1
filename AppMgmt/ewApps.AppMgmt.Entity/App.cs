/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 07 July 2019

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
    /// App table represting all the applications.
    /// </summary>
    [Table("App", Schema ="am")]
    public class App:BaseEntity {

        /// <summary>
        /// The entity name.
        /// </summary>
        public const string EntityName = "App";

        /// <summary>
        /// Identity number.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of application.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name {
            get; set;
        }

        /// <summary>
        /// Theme Identifier.
        /// </summary>
        public Guid ThemeId {
            get; set;
        }

        /// <summary>
        /// App active status identifier.
        /// </summary>
        [Required]
        public bool Active {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// Tenant identifier.
        /// </summary>
        [NotMapped]
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }

        /// <summary>
        /// Application Inactive Comment
        /// </summary>
        [MaxLength(4000)]
        public string InactiveComment {
            get;
            set;
        }


        /// <summary>
        /// SubscriptionMode of the application.
        /// </summary>
        public int AppSubscriptionMode {
            get;
            set;
        }

        /// <summary>
        /// AppScope of the application.
        /// </summary>
        public int AppScope {
            get;
            set;
        }
        /// <summary>
        /// Constructed identifier.
        /// </summary>
        public bool Constructed {
            get; set;
        }

        #region IValidator<App> Members

        /// <inheritdoc />
        public bool Validate(out IList<EwpErrorData> brokenRules) {
            brokenRules = BrokenRules(this).ToList<EwpErrorData>();
            return brokenRules.Count > 0;
        }

        /// <inheritdoc />
        public IEnumerable<EwpErrorData> BrokenRules(App entity) {
            //Check for application name is required.
            if(string.IsNullOrEmpty(entity.Name))
                yield return new EwpErrorData() {
                    ErrorSubType = (int)ValidationErrorSubType.FieldRequired,
                    Data = "Name",
                    Message = "App name is required."
                };
        }

        #endregion

    }
}
