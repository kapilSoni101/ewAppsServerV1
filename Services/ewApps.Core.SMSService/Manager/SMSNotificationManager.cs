using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.ScheduledJobService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.SMSService {

    public class SMSNotificationManager:BaseSchedulerService, IHostedService {
        //private ISMSQueueDS _smsQueueDS;
        //private ISMSDeliveryLogDS _smsDeliveryLogDS;
        //private ISMSDispatcher _smsDispatcher;
        private ConcurrentDictionary<string, BlockingCollection<SMSQueue>> _inMemoryQueue = null;
        private ConcurrentDictionary<string, Task> _taskList = null;
        private ConcurrentDictionary<string, CancellationTokenSource> _cancelationTokenList = null;
        private IServiceScopeFactory _scopeFactory;

        public SMSNotificationManager(IOptions<SMSAppSettings> appSetting, ILogger<SMSNotificationManager> logger, IServiceScopeFactory scopeFactory)
            : base(1000, logger, scopeFactory) {
            //_smsQueueDS = smsQueueDS;
            //_smsDeliveryLogDS = smsDeliveryLog;
            InitializeMembers();
            //_smsDispatcher = smsDispatcher;
            _scopeFactory = scopeFactory;
        }

        private void InitializeMembers() {
            _taskList = new ConcurrentDictionary<string, Task>();
            _cancelationTokenList = new ConcurrentDictionary<string, CancellationTokenSource>();
            _inMemoryQueue = new ConcurrentDictionary<string, BlockingCollection<SMSQueue>>();
        }

        public override async Task ExecuteAsync(DateTime currentSchedulingTimeInUTC, CancellationToken token) {
            using(IServiceScope scope = _scopeFactory.CreateScope()) {
                ISMSQueueDS smsQueueDS = scope.ServiceProvider.GetRequiredService<ISMSQueueDS>();

                try {
                    // Remove Seconds and Milli-Second part from date-time.
                    DateTime fetchTime = new DateTime(currentSchedulingTimeInUTC.Year, currentSchedulingTimeInUTC.Month, currentSchedulingTimeInUTC.Day, currentSchedulingTimeInUTC.Hour, currentSchedulingTimeInUTC.Minute, 0, DateTimeKind.Utc);

                    // Get all pending push that queued in DB.
                    // By DeliveryTime<=Current Timer Signal Time.
                    List<SMSQueue> pendingSMSNotification = smsQueueDS.GetPendingSMSNotificationList(fetchTime);
                    smsQueueDS.Save();

                    for(int i = 0; i < pendingSMSNotification.Count; i++) {

                        try {
                            string tagKey = pendingSMSNotification[i].Recipient;

                            #region Add/Update user queue and new item

                            _inMemoryQueue.AddOrUpdate(tagKey,
                                (DictKey) => {
                                    BlockingCollection<SMSQueue> newQueue = new BlockingCollection<SMSQueue>();
                                    try {
                                        newQueue.Add(pendingSMSNotification[i]);
                                    }
                                    catch(Exception exAdd) {
                                        // Update state to Error in DB.
                                        smsQueueDS.UpdateState(pendingSMSNotification[i].ID, SMSNotificationState.Error);
                                        smsQueueDS.Save();

                                        StringBuilder errorMessage = new StringBuilder();
                                        errorMessage.AppendFormat("Error occurred during add in In-Memory queue. SMSQueueId: {0}", pendingSMSNotification[i].ID);
                                        errorMessage.AppendLine();
                                        errorMessage.AppendFormat("Error Message:{0}", exAdd.Message);
                                        errorMessage.AppendLine();
                                        errorMessage.AppendFormat("Error Stacktrace:{0}", exAdd.StackTrace);
                                        _logger.LogError(errorMessage.ToString());
                                    }
                                    return newQueue;
                                },
                                  (DictKey, OldQueue) => {
                                      try {
                                          OldQueue.TryAdd(pendingSMSNotification[i]);
                                      }
                                      catch(Exception exUpdate) {
                                          // Update state to Error in DB.
                                          smsQueueDS.UpdateState(pendingSMSNotification[i].ID, SMSNotificationState.Error);
                                          smsQueueDS.Save();

                                          StringBuilder errorMessage = new StringBuilder();
                                          errorMessage.AppendFormat("Error occurred during update in In-Memory queue. ID: {0}", pendingSMSNotification[i].ID);
                                          errorMessage.AppendLine();
                                          errorMessage.AppendFormat("Error Message:{0}", exUpdate.Message);
                                          errorMessage.AppendLine();
                                          errorMessage.AppendFormat("Error Stacktrace:{0}", exUpdate.StackTrace);
                                          _logger.LogError(errorMessage.ToString());
                                      }
                                      return OldQueue;
                                  }
                                  );
                            #endregion

                            if(_taskList.ContainsKey(tagKey) == false) {
                                CancellationTokenSource tokenSource = new CancellationTokenSource();
                                _cancelationTokenList.TryAdd(tagKey, tokenSource);
                                bool added = _taskList.TryAdd(tagKey, Task.Factory.StartNew(() => ProcessSMSNotification(tagKey), tokenSource.Token).ContinueWith(TaskCancelled, TaskContinuationOptions.OnlyOnCanceled));
                            }
                            else if(_taskList[tagKey].IsFaulted || _taskList[tagKey].IsCompleted || _taskList[tagKey].IsCanceled) {
                                CancellationTokenSource tokenSource = new CancellationTokenSource();
                                _cancelationTokenList[tagKey] = tokenSource;
                                _taskList[tagKey] = Task.Factory.StartNew(() => ProcessSMSNotification(tagKey), tokenSource.Token).ContinueWith(TaskCancelled, TaskContinuationOptions.OnlyOnCanceled);
                            }
                        }
                        catch(Exception ex) {
                            // Update error state.
                            smsQueueDS.UpdateState(pendingSMSNotification[i].ID, SMSNotificationState.Error);
                            smsQueueDS.Save();

                            // Build error log string and log it.
                            StringBuilder errorMessage = new StringBuilder();
                            errorMessage.AppendFormat("Error occurred during add in In-Memory queue of ID: {0}", pendingSMSNotification[i].ID);
                            errorMessage.AppendLine();
                            if(ex is AggregateException && ex.InnerException != null) {
                                AggregateException aggrEx = ex.InnerException as AggregateException;
                                for(int k = 0; k < aggrEx.InnerExceptions.Count; k++) {
                                    errorMessage.AppendFormat("Inner Exception(s) {0}:", i);
                                    errorMessage.AppendLine();
                                    errorMessage.AppendFormat("Error Message: {0}", aggrEx.InnerExceptions[k].Message);
                                    errorMessage.AppendLine();
                                    errorMessage.AppendFormat("Error StackTrace: {0}", aggrEx.InnerExceptions[k].StackTrace);
                                }
                            }
                            else {
                                errorMessage.AppendFormat("Error Message:{0}", ex.Message);
                                errorMessage.AppendLine();
                                errorMessage.AppendFormat("Error Stacktrace:{0}", ex.StackTrace);
                            }
                            _logger.LogError(errorMessage.ToString());
                        }
                    }
                    pendingSMSNotification = null;
                    //// ToDo: Review it.
                    //_inProgress = false;
                    //EnableTimer(true);
                    //StartTimer(true);
                    //}
                }
                catch(Exception ex) {
                    //// ToDo: Review it.
                    //_inProgress = false;
                    //EnableTimer(true);
                    //StartTimer(true);

                    StringBuilder exception = new StringBuilder();
                    exception.AppendFormat("Error occurred for iteration time {0}:", currentSchedulingTimeInUTC.ToString());
                    exception.AppendLine(ex.Message);
                    exception.AppendLine(ex.StackTrace);
                    _logger.LogError(exception.ToString());

                }
                finally {
                    //if(_inProgress == true)
                    //    _inProgress = false;
                    ////EnableTimer(true);
                    ////StartTimer(true);
                }
            }
        }

        private void ProcessSMSNotification(string key) {
            using(IServiceScope scope = _serviceScopeFactory.CreateScope()) {
                ISMSDeliveryLogDS smsDeliveryLogDS = scope.ServiceProvider.GetRequiredService<ISMSDeliveryLogDS>();
                ISMSDispatcher smsDispatcher = scope.ServiceProvider.GetRequiredService<ISMSDispatcher>();
                ISMSQueueDS smsQueueDS = scope.ServiceProvider.GetRequiredService<ISMSQueueDS>();

                try {
                    if(_inMemoryQueue.ContainsKey(key) == false) {
                        Thread.Sleep(2000);
                    }

                    foreach(SMSQueue itemToProcess in _inMemoryQueue[key].GetConsumingEnumerable(_cancelationTokenList[key].Token)) {
                        // This try block handle all types of errors. Also update record state in DB and log in file. 
                        try {

                            #region Send SMS

                            // Add NotificationQueue record to 'DeliveredNotificationLog' table.
                            SMSDeliveryLog smsDeliveryLog = AddToDeliveredNotificationLog(itemToProcess, SMSNotificationState.Sent);
                            smsDeliveryLogDS.Add(smsDeliveryLog);

                            // Dispatch NotificationQueue item.
                            //_dispatcher.DispatchNotification(itemToProcess, attachmentList);
                            smsDispatcher.SendSMS(itemToProcess.Recipient, itemToProcess.MessagePart2);

                            int savedRecord = smsDeliveryLogDS.Save();

                            #endregion

                            // If this is last item in queue, cancel task.
                            if(_inMemoryQueue.ContainsKey(key) && _inMemoryQueue[key].Count == 0 || (_inMemoryQueue.ContainsKey(key) == false && _cancelationTokenList.ContainsKey(key))) {
                                _cancelationTokenList[key].Cancel();
                            }

                        }
                        // This block handle any type of error occurred during send. It will never rethrow error.
                        catch(Exception exDuringSend) {
                            // Update error state.
                            smsQueueDS.UpdateState(itemToProcess.ID, SMSNotificationState.Error);
                            smsQueueDS.Save();

                            // Build error log string.
                            StringBuilder errorMessage = new StringBuilder();
                            errorMessage.AppendFormat("Error occurred during send process of SMS Queue Id: {0}", itemToProcess.ID);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Error Message:{0}", exDuringSend.Message);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Error Stacktrace:{0}", exDuringSend.StackTrace);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("SMS MessagePart2 :{0}", itemToProcess.MessagePart2);
                            errorMessage.AppendLine();

                            Exception innerEx = exDuringSend.InnerException;
                            while(innerEx != null) {
                                errorMessage.AppendFormat("Inner Exception Type:{0}", exDuringSend.InnerException.GetType().FullName);
                                errorMessage.AppendLine();
                                errorMessage.AppendFormat("Inner Exception Error Message:{0}", exDuringSend.InnerException.Message);
                                errorMessage.AppendLine();
                                errorMessage.AppendFormat("Inner Exception Error Stacktrace:{0}", exDuringSend.InnerException.StackTrace);
                                innerEx = innerEx.InnerException;
                            }

                            // Task cancelled here to handle one case. During last item send process, an error occurred before task cancel.
                            // In this case task is cancelled here.
                            if(_inMemoryQueue[key].Count == 0 && _cancelationTokenList[key].IsCancellationRequested == false) {
                                _cancelationTokenList[key].Cancel();
                            }

                        }
                        //finally {
                        //    // attachmentList = null;
                        //}
                    }
                }
                // Handles task cancelled error.
                catch(OperationCanceledException) {
                    BlockingCollection<SMSQueue> queueList = null;
                    if(_inMemoryQueue.TryRemove(key, out queueList)) {
                        queueList.Dispose();
                    }

                    Task canceledTask;
                    _taskList.TryRemove(key, out canceledTask);

                    CancellationTokenSource canceledTOken;
                    if(_cancelationTokenList.TryRemove(key, out canceledTOken)) {
                        canceledTOken.Dispose();
                    }
                }
                catch(Exception ex) {
                    _logger.LogError(ex.Message + "\r\n" + ex.StackTrace);
                }
            }
        }

        private SMSDeliveryLog AddToDeliveredNotificationLog(SMSQueue itemToProcess, SMSNotificationState currentState) {
            SMSDeliveryLog log = new SMSDeliveryLog();
            log.ID = Guid.NewGuid();
            log.DeliveryType = itemToProcess.DeliveryType;
            log.DeliverySubType = itemToProcess.DeliverySubType;
            log.Recipient = itemToProcess.Recipient;
            log.MessagePart1 = itemToProcess.MessagePart1;
            log.MessagePart2 = itemToProcess.MessagePart2;
            log.ScheduledDeliveryTime = itemToProcess.DeliveryTime;
            log.ActualDeliveryTime = DateTime.UtcNow;
            log.ApplicationId = itemToProcess.ApplicationId;
            log.NotificationId = itemToProcess.NotificationId;
            log.CreatedBy = itemToProcess.CreatedBy;
            log.CreatedOn = log.ActualDeliveryTime;
            log.Deleted = false;
            return log;
        }

        static void TaskCancelled(Task completedTask) {
            int? taskState = (completedTask.AsyncState as int?);
            if(taskState.HasValue) {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Task " + taskState.Value + "cancelled for task " + completedTask.Id);
                Console.ResetColor();
            }
        }

    }
}
