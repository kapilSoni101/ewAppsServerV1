using ewApps.Core.DeeplinkServices;
using ewApps.Core.UserSessionService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.EmailService {
    /// <summary>
    /// It has all the data required to generate email.
    /// </summary>

    public class EmailPayload {
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
        public string UserEmailAddress {
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

        /// <summary>
        /// The event XML data.
        /// </summary>
        public string EventXMLData {
            get; set;
        }


        public string CustomData {
            get; set;
        }

        // Note: This property is not required. Please remove it.
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

        public EmailUserSessionDTO EmailUserSession {
            get; set;
        }

        public int NotificationDeliveryType {
            get; set;
        }

        public int EmailDeliverySubType {
            get; set;
        }

    }
}
