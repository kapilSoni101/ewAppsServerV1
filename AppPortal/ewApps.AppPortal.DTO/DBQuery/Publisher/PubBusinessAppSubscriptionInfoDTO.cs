using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Class contains short information of application subscribe by a tenant.
    /// </summary>
    public class PubBusinessAppSubscriptionInfoDTO {
        /// <summary>
        /// Name of application.
        /// </summary>
        public string Name {
            get; set;
        }

        /// <summary>
        /// Application active state.
        /// </summary>
        public int Status {
            get; set;
        }

        /// <summary>
        /// Primary user joined date.
        /// </summary>
        public DateTime? PrimaryUserActivateDate {
            get; set;
        }

    }
}
