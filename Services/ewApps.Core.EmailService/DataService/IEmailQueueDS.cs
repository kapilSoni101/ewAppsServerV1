/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 July 2019
 */

using System;
using System.Collections.Generic;

namespace ewApps.Core.EmailService {

    /// <summary>
    /// This interface declares set of operations required for <see cref="EmailQueue"/>.
    /// </summary>
    public interface IEmailQueueDS {
        /// <summary>
        /// Gets the pending email notification list.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <returns>Returns List of pending <see cref="EmailQueue"/> items.</returns>
        List<EmailQueue> GetPendingEmailNotificationList(DateTime fromDate);

        /// <summary>
        /// Updates the state of <see cref="EmailQueue"/> entity.
        /// </summary>
        /// <param name="emailQueueId">The email queue entity identifier.</param>
        /// <param name="notificationState">State of email queue to be update.</param>
        /// <param name="commit">if set to <c>true</c> [commit] all database changes.</param>
        void UpdateState(Guid emailQueueId, EmailNotificationState notificationState, bool commit = true);

        /// <summary>
        /// Adds the specified notification queue.
        /// </summary>
        /// <param name="notificationQueue">The notification queue.</param>
        /// <returns>Returns newly added <see cref="EmailQueue"/> entity.</returns>
        EmailQueue Add(EmailQueue notificationQueue);

        /// <summary>
        /// Saves this instance.
        /// </summary>
        /// <returns>Returns total number of records get affected in this current change save.</returns>
        int Save();
    }
}