using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO.PreferenceDTO {
    public class PreferenceUpdateDTO {

        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Created By.
        /// </summary>
        public Guid CreatedBy {
            get; set;
        }

        /// <summary>
        /// Created On.
        /// </summary>
        public DateTime? CreatedOn {
            get; set;
        }

        /// <summary>
        /// Updated By.
        /// </summary>
        public Guid UpdatedBy {
            get; set;
        }

        /// <summary>
        /// Updated On.
        /// </summary>
        public DateTime? UpdatedOn {
            get; set;
        }

        /// <summary>
        /// Deleted.
        /// </summary>
        public bool Deleted {
            get; set;
        }

        /// <summary>
        /// TenantId Identifier.
        /// </summary>
        public Guid TenantId {
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


        public Guid AppId {
            get; set;
        }

    }
}
