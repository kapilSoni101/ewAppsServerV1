/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// This class contains job information that are scheduled for priodical execution.
    /// </summary>
    [Table("ScheduledJob", Schema = "core")]
    public class ScheduledJob {

        /// <summary>
        /// The system generated unique job id.
        /// </summary>
        [Key]
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of Job
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string JobName {
            get; set;
        }

        /// <summary>
        /// The scheduled job next execution time (in UTC).
        /// </summary>
        public DateTime ScheduledTime {
            get; set;
        }


        /// <summary>
        /// The source name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SourceName {
            get; set;
        }

        /// <summary>
        /// The source entity id.
        /// </summary>
        public Guid SourceId {
            get; set;
        }

        /// <summary>
        /// The source event name.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string SourceEventName {
            get; set;
        }

        /// <summary>
        /// The scheduled job event payload.
        /// </summary>
        [MaxLength(4000)]
        public string SourceEventPayload {
            get; set;
        }

        /// <summary>
        /// This is REST api url to be called to trigger job source on scheduled time.
        /// </summary>
        /// <remarks>This REST api url must return a response of Type <see cref="ResponseDTO"/>. Also API type should be of HttpPut./></remarks>
        [MaxLength(200)]
        public string SourceCallback {
            get; set;
        }

        /// <summary>
        /// A value indicating whether this <see cref="ScheduledJob"/> is processed.
        /// </summary>
        public bool Processed {
            get; set;
        }

        /// <summary>
        /// The current status of scheduler job.
        /// </summary>
        [MaxLength(100)]
        public string Status {
            get; set;
        }

        /// <summary>
        /// Job record creation date-time (in UTC).
        /// </summary>
        public DateTime CreatedOn {
            get; set;
        }

        /// <summary>
        /// Job record last updated date-time (in UTC).
        /// </summary>
        public DateTime UpdatedOn {
            get; set;
        }

        /// <summary>
        /// A value indicating whether this <see cref="ScheduledJob"/> is delete.
        /// </summary>
        public bool Deleted {
            get; set;
        }

    }
}
