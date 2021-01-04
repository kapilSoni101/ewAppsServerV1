using System;
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DTO {
    /// <summary>
    /// This class is a DTO for adding user data.
    /// </summary>
    public class UserAppRelationDTO {

        /// <summary>
        /// User appp identifier.
        /// </summary>
        public Guid AppId {
            get; set;
        }

        /// <summary>
        /// User appp identifier.
        /// </summary>
        public long PermissionBitMask {
            get; set;
        }

        /// <summary>
        /// ClientAppType.
        /// </summary>
        public string AppKey {
            get; set;
        }

        public OperationType OperationType {
            get; set;
        }

        public bool Active {
            get;set;
        }
    }
}
