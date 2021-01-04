using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// Contains publisher primary user info and contains a user info exist with a email.
    /// </summary>
    public class PublisherTenantInfoDTO {

        /// <summary>
        /// Publisher Admin user
        /// </summary>
        public UserShortInfoDQ PublisherAdmin {
            get;
            set;
        }

        /// <summary>
        /// User conatins by a email.
        /// </summary>
        public Guid UserId {
            get;
            set;
        }

    }
}
