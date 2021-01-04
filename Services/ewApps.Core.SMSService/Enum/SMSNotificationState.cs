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

namespace ewApps.Core.SMSService {
    public enum SMSNotificationState:ushort {

        /// <summary>
        /// The pending
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
