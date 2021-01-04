using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.ScheduledJobService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ewApps.Payment.DS {
    /// <summary>
    /// Implements a scheduling service to execute recurring payment process.
    /// </summary>
    /// <seealso cref="ewApps.Core.ScheduledJobService.BaseSchedulerService" />
    public class RecurringPaymentScheduler:BaseSchedulerService, IHostedService {
        private string schedulerName = "RecurringPaymentScheduler";

        IScheduledJobManager _scheduledJobManager;
        ScheduledJobDBContext _dbContext;
        static int _timerTickInterval = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecurringPaymentScheduler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="scopeFactory">The scope factory.</param>
        public RecurringPaymentScheduler(ILogger<BaseSchedulerService> logger, IServiceScopeFactory scopeFactory)
            : base(_timerTickInterval, logger, scopeFactory) {
        }

        /// <summary>
        /// Executes scheduled job based on configured time and trigger the recurring payment process.
        /// </summary>
        /// <param name="currentSchedulingTimeInUTC">The current scheduling time in UTC.</param>
        /// <param name="cancellationToken">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>A reference of async task.</returns>
        public override async Task ExecuteAsync(DateTime currentSchedulingTimeInUTC, CancellationToken cancellationToken) {
            try {
                using(IServiceScope serviceScope = _serviceScopeFactory.CreateScope()) {

                    _dbContext = serviceScope.ServiceProvider.GetRequiredService<ScheduledJobDBContext>();
                    _scheduledJobManager = serviceScope.ServiceProvider.GetRequiredService<IScheduledJobManager>();

                    DateTime currnetUTCExecutionTime = currentSchedulingTimeInUTC;// new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

                    // Get recurring payment scheduler scheduled for current tick time.
                    Scheduler scheduler = _scheduledJobManager.GetScheduler(schedulerName, currentSchedulingTimeInUTC);

                    if(scheduler != null) {

                        // Get list of recurring payment scheduled job list that are scheduled for tick time.
                        List<ScheduledJob> scheduledJobList = _scheduledJobManager.GetScheduledJobList(scheduler.NextExecutionTime, schedulerName);

                        // Loop on feched scheduled jobs to trigger recurring payment process.
                        foreach(ScheduledJob job in scheduledJobList) {
                            try {
                                ExecuteRecurringPaymentJob(job);
                            }
                            catch(Exception ex) {
                                _logger.LogError(ex, ex.Message);
                            }
                        }

                        // Calcuate scheduler next execution time.
                        scheduler = CalculateNextExecutionDateTime(scheduler);

                        // Update scheduler next execution time.
                        _dbContext.Update(scheduler);
                    }

                    // Save all database changes.
                    _dbContext.SaveChanges();
                }
                await Task.Delay(TimeSpan.FromMilliseconds(1000), cancellationToken);
            }
            catch(Exception ex) {
                _logger.LogError(ex, ex.Message);
            }
        }

        private void ExecuteRecurringPaymentJob(ScheduledJob recurringPaymentJob) {
            // Console.WriteLine("ExecuteRecurringPaymentJob is starting.");

            recurringPaymentJob.Processed = true;
            recurringPaymentJob.UpdatedOn = DateTime.UtcNow;
            recurringPaymentJob.Status = ScheduledJobStatusEnum.Unknown.ToString();
            _scheduledJobManager.UpdateScheduledJob(recurringPaymentJob);

            // Create a log entry
            ScheduledJobLog jobLog = new ScheduledJobLog();
            jobLog.InProcessQueueTime = DateTime.UtcNow;
            jobLog.InProcessQueueStatus = ScheduledJobStatusEnum.Success.ToString();
            jobLog.InProcessQueueReason = ScheduledJobStatusEnum.Success.ToString();
            jobLog.CreatedOn = DateTime.UtcNow;
            jobLog.UpdatedOn = jobLog.CreatedOn;
            jobLog.ScheduledJobId = recurringPaymentJob.ID;
            jobLog.ScheduledTime = recurringPaymentJob.ScheduledTime;
            jobLog.WorkflowStep = WorkflowStepEnum.InProcess.ToString();
            _dbContext.Add<ScheduledJobLog>(jobLog);
            _dbContext.SaveChanges();

            ScheduledJobDTO scheduledJobDTO = ScheduledJobDTO.MapFrom(recurringPaymentJob);

            // Add API call here.
            ResponseDTO response = ExecuteCallbackAction(recurringPaymentJob.SourceCallback, scheduledJobDTO);

            // Update job log record.
            jobLog.WorkflowStep = WorkflowStepEnum.CalledSource.ToString();
            jobLog.SourceCallbackTime = DateTime.UtcNow;
            jobLog.SourceCallbackStatus = response.ReponseType;
            jobLog.SourceCallbackReason = response.Response;
            jobLog.UpdatedOn = DateTime.UtcNow;
            _dbContext.Update<ScheduledJobLog>(jobLog);

            _dbContext.SaveChanges();
        }

        private ResponseDTO ExecuteCallbackAction(string callbackUri, ScheduledJobDTO scheduledJobDTO) {
            HttpClient client = new HttpClient();
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            ResponseDTO responseDTO = responseDTO = new ResponseDTO();
            try {
                responseDTO = reqProc.ExecutePUTRequest<ResponseDTO, ScheduledJobDTO>(callbackUri, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, scheduledJobDTO);
            }
            catch(Exception ex) {
                responseDTO.ReponseType = ScheduledJobStatusEnum.Failed.ToString();
                Exception flatException;
                if(ex is AggregateException) {
                    flatException = (ex as AggregateException).Flatten();
                    responseDTO.Response = flatException.Message + "\r\n" + flatException.StackTrace;
                    _logger.LogError(flatException, responseDTO.Response);
                }
                else {
                    responseDTO.Response = ex.Message + "\r\n" + ex.StackTrace;
                    _logger.LogError(ex, responseDTO.Response);
                }
            }

            return responseDTO;
        }

        private Scheduler CalculateNextExecutionDateTime(Scheduler scheduler) {
            DateTime currentExecutionDateTime = scheduler.NextExecutionTime;
            switch(scheduler.FrequencyType) {
                case (int)FrequencyType.Minute:
                    currentExecutionDateTime = scheduler.NextExecutionTime.AddMinutes(scheduler.FrequencyValue);
                    break;
                case (int)FrequencyType.Hour:
                    currentExecutionDateTime = scheduler.NextExecutionTime.AddHours(scheduler.FrequencyValue);
                    break;
                case (int)FrequencyType.Day:
                    currentExecutionDateTime = scheduler.NextExecutionTime.AddDays(scheduler.FrequencyValue);
                    break;
                case (int)FrequencyType.Month:
                    currentExecutionDateTime = scheduler.NextExecutionTime.AddMonths(scheduler.FrequencyValue);
                    break;
                case (int)FrequencyType.Year:
                    currentExecutionDateTime = scheduler.NextExecutionTime.AddYears(scheduler.FrequencyValue);
                    break;
            }
            scheduler.LastExecutionTime = scheduler.NextExecutionTime;
            scheduler.NextExecutionTime = currentExecutionDateTime;
            scheduler.UpdatedOn = DateTime.UtcNow;

            return scheduler;
        }
    }
}
