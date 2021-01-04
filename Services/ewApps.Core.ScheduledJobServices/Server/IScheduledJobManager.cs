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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// This interface provides method definations to manage <see cref="ScheduledJob"/> and <see cref="ScheduledJobLog"/> data.
    /// </summary>
    public interface IScheduledJobManager {

        /// <summary>
        /// Gets the Scheduler entity that matches given scheduler name and scheduled time.
        /// </summary>
        /// <param name="schedulerName">Name of the scheduler.</param>
        /// <param name="scheduleTimeInUTC">The schedule time in UTC.</param>
        /// <returns>Returns Scheduler entity that matches given scheduler name and scheduled time.</returns>
        Scheduler GetScheduler(string schedulerName, DateTime scheduleTimeInUTC);

        /// <summary>
        /// Gets the ScheduledJob based on input source entity id.
        /// </summary>
        /// <param name="sourceId">Source entity id to getch ScheduledJob record.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>Returns ScheduledJob entity based on input SQL and parameters.</returns>
        Task<ScheduledJob> GetScheduledJobListAsync(Guid sourceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the scheduled job list that matches given scheduled date time (in UTC).
        /// </summary>
        /// <param name="scheduledTimeInUTC">The scheduled time in UTC to find all scheduled jobs.</param>
        /// <param name="jobName">Name of the job.</param>
        /// <returns>Returns list of ScheduledJob that matches given scheduled time and job name.</returns>
        List<ScheduledJob> GetScheduledJobList(DateTime scheduledTimeInUTC, string jobName);

        /// <summary>
        /// Adds the scheduled job.
        /// </summary>
        /// <param name="scheduledJobDTO">The scheduled job dto.</param>
        /// <returns>Returns created scheduled job ID.</returns>
        Guid AddScheduledJob (ScheduledJobDTO scheduledJobDTO);

        /// <summary>
        /// Adds the scheduled job list.
        /// </summary>
        /// <param name="scheduledJobList">The scheduled job list to be added in database.</param>
        /// <returns>Returns record count to be added.</returns>
        int AddScheduledJobList(List<ScheduledJobDTO> scheduledJobList);

        /// <summary>
        /// Updates the scheduled job record.
        /// </summary>
        /// <param name="scheduledJob">The scheduled job entity instance with updated information.</param>
        /// <returns>Returns true if entity get updated without any error.</returns>
        bool UpdateScheduledJob(ScheduledJob scheduledJob);

        /// <summary>
        /// Update status in <see cref="ScheduledJob"/> and creats a <see cref="ScheduledJobLog"/> for current status change.
        /// </summary>
        /// <param name="scheduleJobDTO">A DTO instance that contains status and other related information.</param>
        /// <returns>Returns number of records get affected in current update operation.</returns>
        int UpdateScheduledJobAndLog(ScheduledJobUpdateDTO scheduleJobDTO);

        /// <summary>
        /// Deletes the un processed scheduled job that maches the given source entity id.
        /// </summary>
        /// <param name="sourceEntityId">The source entity id to find all scheduled jobs.</param>
        /// <returns>Returns true if all scheduled jobs are deleted based on provided source entity id.</returns>
        bool DeleteUnProcessedScheduledJobBySourceEntityId(Guid sourceEntityId);

        /// <summary>
        /// Deletes the un processed scheduled job that maches the given scheduled job id.
        /// </summary>
        /// <param name="scheduledJobId">The source entity id to find all scheduled jobs.</param>
        /// <returns>Returns true if all scheduled jobs are deleted based on provided scheduled job id.</returns>
        bool DeleteUnProcessedScheduledJobByScheduleJobId(Guid scheduledJobId);

    }
}