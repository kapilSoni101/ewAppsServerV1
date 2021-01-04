/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 19 June 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 June 2019
 */

using System;

namespace ewApps.Notification.Common {
    public class NotificationAppSettings {

        public string AppName {
            get;
            set;
        }

        public string AppVersion {
            get; set;
        }

        public string IdentityServerUrl {
            get;
            set;
        }

        public string Deployment {
            get; set;
        }

        public string MinimumLoggingLevel {
            get; set;
        }

        public string ConnectionString {
            get; set;
        }

        public string LogPortalUrl {
            get; set;
        }
        
        public string[] CrossOriginsUrls {
            get;
            set;
        }
      
    }
}
