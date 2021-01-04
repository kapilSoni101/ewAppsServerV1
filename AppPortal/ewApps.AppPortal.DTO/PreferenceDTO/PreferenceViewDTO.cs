using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// 
    /// </summary>
    public class PreferenceViewDTO:BaseDTO {

        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public new Guid ID {
            get; set;
        }

        /// <summary>
        /// Created By.
        /// </summary>
        public new Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// Created On.
        /// </summary>
        public new DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// Updated By.
        /// </summary>
        public new Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// Updated On.
        /// </summary>
        public new DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// Deleted.
        /// </summary>
        public new bool Deleted {
            get; set;
        }

        /// <summary>
        /// TenantId Identifier.
        /// </summary>
        public new Guid TenantId {
            get; set;
        }

        /// <summary>
        /// AppUser Identifier.
        /// </summary>
        public Guid TenantUserId {
            get; set;
        }

        /// <summary>
        /// App Identifier.
        /// </summary>
        public long EmailPreference {
            get; set;
        }

        /// <summary>
        /// App Identifier.
        /// </summary>
        public long ASPreference {
            get; set;
        }

        /// <summary>
        /// App Identifier.
        /// </summary>
        public long SMSPreference {
            get; set;
        }

    }
}
