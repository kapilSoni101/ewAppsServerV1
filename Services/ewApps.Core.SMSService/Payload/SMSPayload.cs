/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using System;
using System.Collections.Generic;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.UserSessionService;

namespace ewApps.Core.SMSService {
    /// <summary>
    /// It has all the data required to generate SMS.
    /// </summary>
    public class SMSPayload {

        public Guid AppId {
            get; set;
        }
        public int AppType {
            get; set;
        }
        public Guid TenantId {
            get; set;
        }
        public Guid RecepientUserId {
            get; set;
        }
        public string UserSMSAddress {
            get; set;
        }
        public string UserLanguage {
            get; set;
        }
        public bool IsLoggedInUser {
            get; set;
        }
        //If the deeplink generated in the Notification process has deeplink error to add in the subject line of email.
        public bool HasLinkError {
            get; set;
        }
        public string EventXMLData {
            get; set;
        }
        public string CustomData {
            get; set;
        }
        /// <summary>
        /// Deeplink payload to generate deeplink for the email body.
        /// </summary>
        public DeeplinkPayload DeeplinkPayload {
            get; set;
        }
        /// <summary>
        /// Notification information like Notificationid, LinkedNotificationId
        /// </summary>
        public Dictionary<string, string> NotificationInfo {
            get; set;
        }
        /// <summary>
        /// XSLT arguments that are defined in the template
        /// </summary>
        public Dictionary<string, string> XSLTArguments {
            get; set;
        }

        public DeeplinkResultSet DeeplinkResultSet {
            get; set;
        }

        public string XSLTemplateContent {
            get; set;
        }

        public bool InMemoryXSLTemplate {
            get; set;
        }

        public SMSUserSessionDTO SMSUserSession {
            get; set;
        }

        public int NotificationDeliveryType {
            get; set;
        }

        public Guid NotificationId {
            get; set;
        }

        public string SMSRecipient {
            get; set;
        }

    }
}
