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

namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This class is DTO (Data Transfer Object) contains scheduled job information.
    /// </summary>
    public class ScheduledJobDTO {

        /// <summary>
        /// The system generated unique id.
        /// </summary>   
        public Guid ID {
            get; set;
        }

        /// <summary>
        /// Name of Job
        /// </summary>
        public string JobName {
            get; set;
        }

        /// <summary>
        /// Scheduler's next schedule time (in UTC).
        /// </summary>
        public DateTime ScheduledTime {
            get; set;
        }

        /// <summary>
        /// The parent source entity name.
        /// </summary>
        public string SourceName {
            get; set;
        }

        /// <summary>
        /// The parent source entity id.
        /// </summary>
        public Guid SourceId {
            get; set;
        }

        /// <summary>
        /// The parent entity's scheduler event name.
        /// </summary>
        public string SourceEventName {
            get; set;
        }


        /// <summary>
        /// The parent source entity event payload.
        /// </summary>
        public string SourceEventPayload {
            get; set;
        }

        /// <summary>
        /// The source module call back API url.
        /// </summary>
        public string SourceCallback {
            get; set;
        }


        /// <summary>
        /// Map the ScheduledJob DTO model to ScheduledJob database entity.
        /// </summary>
        /// <param name="scheduledJobDTO">ScheduledJob DTO to be map ScheduledJob entity.</param>   
        /// <returns>Return ScheduledJob database entity mapped from .</returns>
        public static ScheduledJob MapTo(ScheduledJobDTO scheduledJobDTO) {
            ScheduledJob scheduledJob = new ScheduledJob();
            scheduledJob.ID = scheduledJobDTO.ID;
            scheduledJob.JobName = scheduledJobDTO.JobName;
            scheduledJob.ScheduledTime = scheduledJobDTO.ScheduledTime;
            scheduledJob.SourceCallback = scheduledJobDTO.SourceCallback;
            scheduledJob.SourceEventName = scheduledJobDTO.SourceEventName;
            scheduledJob.SourceEventPayload = scheduledJobDTO.SourceEventPayload;
            scheduledJob.SourceId = scheduledJobDTO.SourceId;
            scheduledJob.SourceName = scheduledJobDTO.SourceName;

            return scheduledJob;
        }

        /// <summary>
        /// Map the ScheduledJob database entity to ScheduledJob DTO.
        /// </summary>
        /// <param name="scheduledJob">ScheduledJob entity to be map to ScheduledJob DTO.</param>   
        /// <returns>Return ScheduledJob DTO entity mapped from ScheduledJob entity.</returns>
        public static ScheduledJobDTO MapFrom(ScheduledJob scheduledJob) {
            ScheduledJobDTO scheduledJobDTO = new ScheduledJobDTO();
            scheduledJobDTO.ID = scheduledJob.ID;
            scheduledJobDTO.JobName = scheduledJob.JobName;
            scheduledJobDTO.ScheduledTime = scheduledJob.ScheduledTime;
            scheduledJobDTO.SourceCallback = scheduledJob.SourceCallback;
            scheduledJobDTO.SourceEventName = scheduledJob.SourceEventName;
            scheduledJobDTO.SourceEventPayload = scheduledJob.SourceEventPayload;
            scheduledJobDTO.SourceId = scheduledJob.SourceId;
            scheduledJobDTO.SourceName = scheduledJob.SourceName;

            return scheduledJobDTO;
        }

    }
}
