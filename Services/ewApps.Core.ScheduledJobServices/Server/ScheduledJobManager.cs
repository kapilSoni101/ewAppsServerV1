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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// This class provides methods to manage <see cref="ScheduledJob"/> and <see cref="ScheduledJobLog"/> data.
    /// </summary>
    /// <seealso cref="ewApps.Core.ScheduledJobService.IScheduledJobManager" />
    public class ScheduledJobManager:IScheduledJobManager {

        #region Constructor and property

        private ScheduledJobDBContext _scheduledJobDBContext;
        private ILogger<ScheduledJobManager> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledJobManager"/> class and dependecies.
        /// </summary>
        /// <param name="dbContext">The database context to query on database.</param>
        /// <param name="logger">The logger instance to log information.</param>
        public ScheduledJobManager(ScheduledJobDBContext dbContext, ILogger<ScheduledJobManager> logger) {
            _scheduledJobDBContext = dbContext;
            _logger = logger;
        }

        #endregion

        #region Data Methods

        /// <inheritdoc />
        public async Task<ScheduledJob> GetScheduledJobListAsync(Guid sourceId, CancellationToken token = default(CancellationToken)) {
            return await _scheduledJobDBContext.ScheduledJobs.Where(scheduledJob => scheduledJob.SourceId == sourceId).SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public Scheduler GetScheduler(string schedulerName, DateTime scheduleTimeInUTC) {
            return _scheduledJobDBContext.Scheduler.Where(i => i.Active == true && i.NextExecutionTime <= scheduleTimeInUTC && i.SchedulerName == schedulerName).FirstOrDefault();
        }

        /// <inheritdoc />
        public List<ScheduledJob> GetScheduledJobList(DateTime scheduledTimeInUTC, string jobName) {
            return _scheduledJobDBContext.ScheduledJobs.Where(i => i.Processed == false && i.ScheduledTime <= scheduledTimeInUTC && i.JobName == jobName).ToList();
        }

        /// <inheritdoc />
        public Guid AddScheduledJob(ScheduledJobDTO scheduledJobDTO) {
            ScheduledJob scheduledJob = ScheduledJobDTO.MapTo(scheduledJobDTO);
            scheduledJob.Status = ScheduledJobStatusEnum.NotProcessed.ToString();
            scheduledJob.CreatedOn = DateTime.UtcNow;
            scheduledJob.UpdatedOn = scheduledJob.CreatedOn;
            _scheduledJobDBContext.Add(scheduledJob);
            _scheduledJobDBContext.SaveChanges();
            return scheduledJob.ID;
        }

        /// <inheritdoc />
        public int AddScheduledJobList(List<ScheduledJobDTO> scheduledJobList) {
            if(scheduledJobList != null) {
                foreach(ScheduledJobDTO job in scheduledJobList) {
                    ScheduledJob scheduledJob = ScheduledJobDTO.MapTo(job);
                    scheduledJob.Status = ScheduledJobStatusEnum.NotProcessed.ToString();
                    scheduledJob.CreatedOn = DateTime.UtcNow;
                    scheduledJob.UpdatedOn = scheduledJob.CreatedOn;
                    _scheduledJobDBContext.Add(scheduledJob);
                }
                return _scheduledJobDBContext.SaveChanges();
            }
            return 0;
        }

        /// <inheritdoc />
        public int UpdateScheduledJobAndLog(ScheduledJobUpdateDTO scheduleJobDTO) {
            // Updates parent scheduled job records with current status.
            ScheduledJob job = _scheduledJobDBContext.ScheduledJobs.FirstOrDefault(i => i.ID == scheduleJobDTO.ScheduleJobId);
            if(job != null) {
                job.Status = scheduleJobDTO.Status;
                job.UpdatedOn = DateTime.UtcNow;
                _scheduledJobDBContext.ScheduledJobs.Update(job);
            }

            // Updates job log record with completion status and its's reason of completion. Status can be either Sucess or failed or something else.
            ScheduledJobLog jobLog = _scheduledJobDBContext.ScheduledJobLogs.FirstOrDefault(i => i.ScheduledJobId == scheduleJobDTO.ScheduleJobId);
            if(jobLog != null) {
                jobLog.CompletionStatus = WorkflowStepEnum.Completed.ToString();
                jobLog.CompletionReason = scheduleJobDTO.Reason;
                jobLog.CompletionTime = DateTime.UtcNow;
                jobLog.UpdatedOn = jobLog.CompletionTime.Value;
                _scheduledJobDBContext.ScheduledJobLogs.Update(jobLog);
            }

            return _scheduledJobDBContext.SaveChanges();
        }

        /// <inheritdoc />
        public bool UpdateScheduledJob(ScheduledJob scheduledJob) {
            _scheduledJobDBContext.Update(scheduledJob);
            return true;
        }

        /// <inheritdoc />
        public bool DeleteUnProcessedScheduledJobBySourceEntityId(Guid sourceEntityId) {
            List<ScheduledJob> unProcessedJobList = _scheduledJobDBContext.ScheduledJobs.Where(i => i.SourceId.Equals(sourceEntityId) && i.Processed == false).ToList();

            foreach(ScheduledJob job in unProcessedJobList) {
                job.Deleted = true;
                job.UpdatedOn = DateTime.UtcNow;
                _scheduledJobDBContext.Update(job);
            }
            _scheduledJobDBContext.SaveChanges();
            return true;
        }

        /// <inheritdoc />
        public bool DeleteUnProcessedScheduledJobByScheduleJobId(Guid scheduledJobId) {
            ScheduledJob unProcessedJob = _scheduledJobDBContext.ScheduledJobs.FirstOrDefault(i => i.ID.Equals(scheduledJobId) && i.Processed == false);
            if(unProcessedJob != null) {
                unProcessedJob.Deleted = true;
                unProcessedJob.UpdatedOn = DateTime.UtcNow;

                _scheduledJobDBContext.Update(unProcessedJob);
                _scheduledJobDBContext.SaveChanges();
            }
            else {
                throw new ewApps.Core.ExceptionService.EwpInvalidOperationException("Scheduled Job is not found.");
            }
            return true;
        }

        #endregion Data Methods

    }
}
