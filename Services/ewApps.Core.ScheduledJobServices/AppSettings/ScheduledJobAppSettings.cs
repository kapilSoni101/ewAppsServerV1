/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This class contains all configuration information required to execute scheduler service.
    /// </summary>
    public class ScheduledJobAppSettings {
        /// <summary>
        /// Database connection string to get scheduler and scheduled job information.
        /// </summary>
        public string ConnectionString {
            get; set;
        }

        /// <summary>
        /// A value indicating whether [enable debug log].
        /// </summary>
        /// <value>
        ///   if <c>true</c> [debug log becomes enable]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableDebugLog {
            get; set;
        } = false;

    }
}