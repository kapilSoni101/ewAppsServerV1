using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppPortal.DTO {

    /// <summary>
    /// Data transfer object for the session of the platform.
    /// </summary>
    public class PlatfromUserSessionDTO:BaseUserSessionAndAppDTO {

        /// <summary>
        /// Platform thumbnail id of the platfrom if user changes the thumbnail of the platform.
        /// </summary>
        public Guid? PlatformThumbnailId {
            get;
            set;
        }
    }
}
