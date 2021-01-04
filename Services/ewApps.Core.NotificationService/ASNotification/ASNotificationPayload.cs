using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.NotificationService {

    public class ASNotificationPayload {

        public Guid AppId {
            get; set;
        }


        public Guid TenantId {
            get; set;
        }

        // Currently it's passed through EventData dictionary.
        public Guid PartnerTenantId {
            get; set;
        }

        // Current Recipient User Id.
        public Guid RecepientUserId {
            get; set;
        }

        public string UserLanguage {
            get; set;
        }

        public bool IsLoggedInUser {
            get; set;
        }

        public long EventId {
            get; set;
        }

        /// <summary>
        /// The event XML data.
        /// </summary>
        public string EventXMLData {
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

        public string XSLTemplateContent {
            get; set;
        }

        public bool InMemoryXSLTemplate {
            get; set;
        }

        public int NotificationDeliveryType {
            get; set;
        }


        public Guid LinkNotificationId {
            get; set;
        }

        public int EntityType {
            get; set;
        }

        public Guid EntityId {
            get; set;
        }

        public string AdditionalInfo {
            get; set;
        }

        public long ASNotificationType {
            get; set;
        }

    }
}
