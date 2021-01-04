/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date:  05 March 2019
 * 
 * Contributor/s: Amit Mundra.
 * Last Updated On: 05 March 2019
 */

using System;

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// Contains the property to update ScheduleJob, Log, Status and reson.
    /// </summary>
    public class ScheduledJobUpdateDTO {

        /// <summary>
        /// The parent scheduler job id.
        /// </summary>
        public Guid ScheduleJobId {
            get; set;
        }

        /// <summary>
        /// The system generated unique id.
        /// </summary>
        public Guid LogId {
            get; set;
        }

        /// <summary>
        /// Reason for current status change.
        /// </summary>
        public string Reason {
            get; set;
        }

        /// <summary>
        /// Current status change of scheduler.
        /// </summary>
        public string Status {
            get; set;
        }

    }
}
