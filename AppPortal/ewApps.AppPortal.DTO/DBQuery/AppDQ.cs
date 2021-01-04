using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// App entity resamble.
    /// </summary>
    public class AppDQ: BaseDQ {

        /// <summary>
        /// Application ID.
        /// </summary>
        public new Guid ID {
            get;
            set;
        }

        /// <summary>
        /// Identity number.
        /// </summary>
        public string IdentityNumber {
            get; set;
        }

        /// <summary>
        /// The name  of applications.
        /// </summary>
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
        public bool Active {
            get; set;
        }

        /// <summary>
        /// App unique identification key.
        /// </summary>
        public string AppKey {
            get; set;
        }

        /// <summary>
        /// Tenant identifier.
        /// </summary>
        public override Guid TenantId {
            get => base.TenantId;
            set => base.TenantId = value;
        }

        /// <summary>
        /// Application Inactive Comment
        /// </summary>
        public string InactiveComment {
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

        public int AppSubscriptionMode {
            get;set;
        }

    }
}
