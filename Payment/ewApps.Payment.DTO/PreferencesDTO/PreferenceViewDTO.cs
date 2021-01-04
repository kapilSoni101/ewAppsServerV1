using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DTO {

    /// <summary>
    /// Preference view 
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

        public static PreferenceViewDTO MapFromTenantUserPreference(TenantUserAppPreference tenantUserPreference) {
            PreferenceViewDTO preferenceViewDTO = new PreferenceViewDTO();
            preferenceViewDTO.ASPreference = tenantUserPreference.ASPreference;
            preferenceViewDTO.CreatedBy = tenantUserPreference.CreatedBy;
            preferenceViewDTO.CreatedOn = tenantUserPreference.CreatedOn;
            preferenceViewDTO.EmailPreference = tenantUserPreference.EmailPreference;
            preferenceViewDTO.ID = tenantUserPreference.ID;
            preferenceViewDTO.SMSPreference = tenantUserPreference.SMSPreference;
            preferenceViewDTO.TenantId = tenantUserPreference.TenantId;
            preferenceViewDTO.TenantUserId = tenantUserPreference.TenantUserId;
            preferenceViewDTO.UpdatedBy = tenantUserPreference.UpdatedBy;
            preferenceViewDTO.UpdatedOn = tenantUserPreference.UpdatedOn;
            return preferenceViewDTO;
        }

    }
}
