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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This class contains scheduled job execution log.
    /// </summary>
    [Table("ScheduledJobLog", Schema = "core")]
    public class ScheduledJobLog {

        /// <summary>
        /// The system generated unique id.
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// The unique id of logged job.
        /// </summary>
        [Required]
        public Guid ScheduledJobId {
            get; set;
        }


        /// <summary>
        /// Scheduler execution time (in UTC).
        /// </summary>
        public DateTime ScheduledTime {
            get; set;
        }

        /// <summary>
        /// Execution step of scheduled job.
        /// </summary>
        [MaxLength(100)]
        public string WorkflowStep {
            get; set;
        }

        /// <summary>
        /// The in-process queue date-time (in UTC).
        /// </summary>
        public DateTime InProcessQueueTime {
            get; set;
        }

        /// <summary>
        /// The in-process queue status.
        /// </summary>
        [MaxLength(100)]
        public string InProcessQueueStatus {
            get; set;
        }

        /// <summary>
        /// The in-process queue reason.
        /// </summary>
        [MaxLength(4000)]
        public string InProcessQueueReason {
            get; set;
        }

        /// <summary>
        /// The date-time (in UTC) of source callback execution initiated.
        /// </summary>
        public DateTime? SourceCallbackTime {
            get; set;
        }

        /// <summary>
        /// The execution status of source callback.
        /// </summary>
        [MaxLength(100)]
        public string SourceCallbackStatus {
            get; set;
        }

        /// <summary>
        /// The source callback reason.
        /// </summary>
        [Required]
        [MaxLength(4000)]
        public string SourceCallbackReason {
            get; set;
        }

        /// <summary>
        /// Source Callback execution completion date-time (in UTC).
        /// </summary>
        public DateTime? CompletionTime {
            get; set;
        }

        /// <summary>
        /// The scheduled job completion status.
        /// </summary>
        [MaxLength(100)]
        public string CompletionStatus {
            get; set;
        }

        /// <summary>
        /// The scheduled job completion reason.
        /// </summary>
        [MaxLength(4000)]
        public string CompletionReason {
            get; set;
        }

        /// <summary>
        /// The log creation date-time (in UTC).
        /// </summary>
        public DateTime CreatedOn {
            get; set;
        }

        /// <summary>
        /// The log updation date-time (in UTC).
        /// </summary>
        public DateTime UpdatedOn {
            get; set;
        }

    }
}
