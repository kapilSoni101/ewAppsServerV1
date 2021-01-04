/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */


namespace ewApps.Core.EmailService {
    /// <summary>
    /// Enum represents notification process status.
    /// </summary>
    public enum EmailNotificationState {
        /// <summary>
        /// The Queued
        /// </summary>
        Queued = 1,
        /// <summary>
        /// The error
        /// </summary>
        Error = 2,
        /// <summary>
        /// The sent
        /// </summary>
        Sent = 3,
    }
}
