using System;
using System.Collections.Generic;

namespace ewApps.Core.SMSService {
    /// <summary>
    /// This interface declares set of operations required for <see cref="SMSQueue"/>.
    /// </summary>
    public interface ISMSQueueDS {
        /// <summary>
        /// Gets the pending SMS notification list.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns>Returns List of pending <see cref="SMSQueue"/> items.</returns>
        List<SMSQueue> GetPendingSMSNotificationList(DateTime fromDate);

        /// <summary>
        /// Updates the state of <see cref="SMSQueue"/> entity.
        /// </summary>
        /// <param name="smsQueueId">The SMS queue entity identifier.</param>
        /// <param name="notificationState">State of sms queue to be update.</param>
        /// <param name="commit">if set to <c>true</c> [commit] all database changes.</param
        void UpdateState(Guid smsQueueId, SMSNotificationState notificationState, bool commit = true);

        /// <summary>
        /// Adds the specified sms queue.
        /// </summary>
        /// <param name="smsQueue">The sms queue.</param>
        /// <returns>Returns newly added <see cref="SMSQueue"/> entity.</returns>
        SMSQueue Add(SMSQueue smsQueue);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Returns total number of records get affected in this current change save.</returns>
        int Save();
    }
}