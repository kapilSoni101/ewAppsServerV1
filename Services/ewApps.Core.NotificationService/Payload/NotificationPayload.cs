using System;
using System.Collections.Generic;
using ewApps.Core.DeeplinkServices;

namespace ewApps.Core.NotificationService {

    // Basically a data class with fields required to  generate database row/rows for a notification
    // During the course of Notification flow in NotificationService, this data is computed and stored 
    // in this object.
    // At the end, in Save method, it is stored in the database

    /// <summary>
    /// This class contains all information related to an actions that triggers notification process.
    /// </summary>
    public class NotificationPayload<T> where T : NotificationRecipient, new() {

        /// <summary>ModuleId of the module from where the service is used.</summary>
        public int ModuleId {
            get;
            set;
        }

        /// <summary>All events in a module are defined with event identification by eventId</summary>
        /// <value>The event identifier.</value>
        public long EventId {
            get;
            set;
        }

        public NotificationDeliveryType SupportedDeliveryType {
            get; set;
        }


        /// <summary>Unique identification of logged in user, who triggered the event that causes the notification</summary>
        public Guid LoggedinUserId {
            get;
            set;
        }

        /// <summary>Unique identification of the notification recepient (User)</summary>
        /// <value>The recipient list.</value>
        public List<T> RecipientList {
            get;
            set;
        }

        /// <summary>
        /// It's unique id and different for each type of notification generated for current event.
        /// </summary>
        public Guid NotificationId {
            get;
            set;
        }

        /// <summary>
        /// It's unique id and same for each type of notification generated for current event.
        /// </summary>
        public Guid LinkedNotificationId {
            get;
            set;
        }

        /// <summary>
        /// A unique id that is use to track the notification delivery at client end.
        /// </summary>
        public Guid TrackingId {
            get;
            set;
        }

        /// <summary>
        /// Set of information to be use to generate notification.
        /// 
        /// </summary>
        public Dictionary<string, object> EventInfo {
            get;
            set;
        }

        /// <summary>
        /// Any other info that will be gather during the notification process.
        /// </summary>
        public Dictionary<string, object> OtherInfo {
            get;
            set;
        }

        /// <summary>
        /// List of universal links and it's payload that will use in generated notification.
        /// </summary>
        public DeeplinkResultSet DeeplinkResultSet {
            get;
            set;
        }

        /// <summary>
        /// The user session information.
        /// </summary>
        public Dictionary<string, string> UserSessionInformation {
            get;
            set;
        }

    }
}
