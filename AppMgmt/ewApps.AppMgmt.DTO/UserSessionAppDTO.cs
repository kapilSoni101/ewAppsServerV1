/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 20 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 Aug 2019
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.AppMgmt.DTO {

    /// <summary>
    /// User session app DTO to get all the application belowing to the portal user loginin into.
    /// </summary>
    public class UserSessionAppDTO {
        
        /// <summary>
        /// Application Id of the application of the portal.
        /// </summary>
        public Guid AppId {
            get;
            set;
        }

        /// <summary> 
        /// Applciation name from publisher app setting.
        /// </summary>
        public string ApplicationName {
            get;
            set;
        }

        /// <summary> 
        /// Applciation name from publisher app setting.
        /// </summary>
        public string AppKey {
            get;
            set;
        }

        /// <summary>
        /// Application title for the branding.
        /// </summary>
        public string AppTitle {
            get;
            set;
        }

        /// <summary>
        /// Theme key of the user tenant.
        /// </summary>
        public string ThemeKey {
            get;
            set;
        }

        /// <summary>
        /// Top Header LeftLogoUrl.
        /// </summary>
        public string ApplicationLogoUrl {
            get;
            set;
        }

        /// <summary>
        /// Top Header LeftLogoUrl.
        /// </summary>
        public string TopHeaderLeftLogoUrl {
            get;
            set;
        }

        /// <summary>
        /// Top Header LeftName.
        /// </summary>
        public string TopHeaderLeftName {
            get;
            set;
        }

        /// <summary>
        /// Top Header Left PortalName.
        /// </summary>
        public string TopHeaderLeftPortalName {
            get;
            set;
        }

        /// <summary>
        /// User permission bitmask for the user application wise.
        /// </summary>
        public long PermissionBitMask {
            get;
            set;
        }

        /// <summary>
        /// Admin flag of the user for the application.
        /// </summary>
        public bool IsAdmin {
            get;
            set;
        }

        /// <summary>
        /// If application is given to the business by the publisher.
        /// </summary>
        public bool Subscribed {
            get;
            set;
        }

        /// <summary>
        /// App table flat flag.(App is developed or not)
        /// </summary>
        public bool Constructed {
            get; set;
        }

        /// <summary>
        /// User app linking.
        /// </summary>
        public bool Access {
            get; set;
        }

    }
}
