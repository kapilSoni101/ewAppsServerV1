using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;

namespace ewApps.Core.NotificationService {
    /// <summary>
    /// This interface is common for all ewApps notifications, from various modules.
    /// Note that this interface does not deliver notifications.
    /// It gathers all data required for the notification and stores it in a database.
    /// A separate service/process delivers notifications in appropriate channels, like email, messaging etc.
    /// </summary>
    public interface INotificationService<T> where T : NotificationRecipient, new() {

        NotificationDeliveryType GetSupportedNotificationType(long platformNotificationEvents);

                /// <summary>
                /// Generates notification data in the notification table directly or incdirectly by other service.
                /// </summary>
                /// <param name="generateNotificationDTO"></param>
                /// <param name="token">A cancellation token that help to control the execution of async notification process.</param>
        // bool GenerateNotification(DataTable recipientDT, KeyValuePair<int, long> subscribedEvent, Dictionary<string, object> eventData);
        Task GenerateNotificationAsync(GenerateNotificationDTO generateNotificationDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Create deeplinkg payload for payment.
        /// </summary>
        /// <param name="notificationPayload">The notification payload.</param>
        /// <returns>Returns updated deeplink payload.</returns>
        DeeplinkPayload GetDeeplinkPayload(NotificationPayload<T> notificationPayload);

        /// <summary>
        /// Create email payload.
        /// </summary>
        /// <param name="notificationPayload">Notification payload</param>
        /// <param name="recepientRow">Notification list</param>
        /// <param name="eventXMLData">event data</param>
        /// <param name="hasLinkError">Deeplink error flag</param>
        /// <param name="xmlArguments">xml argument.</param>
        /// <returns>Returns updated email payload.</returns>
        EmailPayload GetEmailPayload(NotificationPayload<T> notificationPayload, T recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments);


        /// <summary>
        /// Set deellink notfication payload
        /// </summary>
        /// <param name="notificationPayload">Notification payload</param>
        /// <param name="deeplinkResultSet">Deeplink result sets</param>
        Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<T> notificationPayload, DeeplinkResultSet deeplinkResultSet);

        bool AddASNotification(ASNotificationDTO aSNotificationDTO, NotificationPayload<T> notificationPayload);
    }
}
