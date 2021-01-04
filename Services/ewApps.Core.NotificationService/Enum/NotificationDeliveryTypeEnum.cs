using System;

namespace ewApps.Core.NotificationService {
    /// <summary>
    /// This enum defines the supported notification deivery type.
    /// </summary>
    [Flags]
    public enum NotificationDeliveryType {
        None=0, 

        /// <summary>
        /// This type indicates the notification delivery as Email.
        /// </summary>
        Email = 1,

        /// <summary>
        /// This type indicates the notification delivery as SMS.
        /// </summary>
        SMS = 2,

        /// <summary>
        /// This type indicates the notification delivery as AS-Notification.
        /// </summary>
        ASNotification = 4,

        All = Email | SMS | ASNotification

    }

}
