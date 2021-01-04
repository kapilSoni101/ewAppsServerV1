using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.ScheduledJobService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.EmailService {

    public class EmailNotificationManager:BaseSchedulerService, IHostedService {

        // private IEmailQueueDS _emailQueueDS;
        // private IEmailDeliveryLogDS _emailDeliveryLogDS;
        // private IEmailDispatcher _emailDispatcher;
        private ConcurrentDictionary<string, BlockingCollection<EmailQueue>> _inMemoryQueue = null;
        private ConcurrentDictionary<string, Task> _taskList = null;
        private ConcurrentDictionary<string, CancellationTokenSource> _cancelationTokenList = null;
        private IServiceScopeFactory _scopeFactory;

        public EmailNotificationManager(IOptions<EmailAppSettings> appSetting, ILogger<EmailNotificationManager> logger, IServiceScopeFactory scopeFactory)
            : base(1000, logger, scopeFactory) {
            InitializeMembers();
            _scopeFactory = scopeFactory;
        }

        private void InitializeMembers() {
            _taskList = new ConcurrentDictionary<string, Task>();
            _cancelationTokenList = new ConcurrentDictionary<string, CancellationTokenSource>();
            _inMemoryQueue = new ConcurrentDictionary<string, BlockingCollection<EmailQueue>>();
        }

        /// <inheritdoc/>
        public override async Task ExecuteAsync(DateTime currentSchedulingTimeInUTC, CancellationToken token) {
            using(IServiceScope scope = _scopeFactory.CreateScope()) {
                IEmailQueueDS emailQueueDS = scope.ServiceProvider.GetRequiredService<IEmailQueueDS>();
                try {
                    //  if(_inProgress == false) {
                    //  _inProgress = true;

                    // Remove Seconds and Milli-Second part from date-time.
                    DateTime fetchTime = new DateTime(currentSchedulingTimeInUTC.Year, currentSchedulingTimeInUTC.Month, currentSchedulingTimeInUTC.Day, currentSchedulingTimeInUTC.Hour, currentSchedulingTimeInUTC.Minute, 0, DateTimeKind.Utc);

                    // Get all pending push that queued in DB.
                    // By DeliveryTime<=Current Timer Signal Time.
                    List<EmailQueue> pendingEmailNotification = emailQueueDS.GetPendingEmailNotificationList(fetchTime);

                    emailQueueDS.Save();

                    for(int i = 0; i < pendingEmailNotification.Count; i++) {

                        try {
                            string tagKey = pendingEmailNotification[i].Recipient;

                            #region Add/Update user queue and new item

                            _inMemoryQueue.AddOrUpdate(tagKey,
                                (DictKey) => {
                                    BlockingCollection<EmailQueue> newQueue = new BlockingCollection<EmailQueue>();
                                    try {
                                        newQueue.Add(pendingEmailNotification[i]);
                                    }
                                    catch(Exception exAdd) {
                                        // Update state to Error in DB.
                                        emailQueueDS.UpdateState(pendingEmailNotification[i].ID, EmailNotificationState.Error);
                                        emailQueueDS.Save();

                                        StringBuilder errorMessage = new StringBuilder();
                                        errorMessage.AppendFormat("Error occurred during add in In-Memory queue. PushNotificationQueueId: {0}", pendingEmailNotification[i].ID);
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
                                          OldQueue.TryAdd(pendingEmailNotification[i]);
                                      }
                                      catch(Exception exUpdate) {
                                          // Update state to Error in DB.
                                          emailQueueDS.UpdateState(pendingEmailNotification[i].ID, EmailNotificationState.Error);
                                          emailQueueDS.Save();

                                          StringBuilder errorMessage = new StringBuilder();
                                          errorMessage.AppendFormat("Error occurred during update in In-Memory queue. ID: {0}", pendingEmailNotification[i].ID);
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
                                bool added = _taskList.TryAdd(tagKey, Task.Factory.StartNew(() => ProcessEmailNotification(tagKey), tokenSource.Token).ContinueWith(TaskCancelled, TaskContinuationOptions.OnlyOnCanceled));
                            }
                            else if(_taskList[tagKey].IsFaulted || _taskList[tagKey].IsCompleted || _taskList[tagKey].IsCanceled) {
                                CancellationTokenSource tokenSource = new CancellationTokenSource();
                                _cancelationTokenList[tagKey] = tokenSource;
                                _taskList[tagKey] = Task.Factory.StartNew(() => ProcessEmailNotification(tagKey), tokenSource.Token).ContinueWith(TaskCancelled, TaskContinuationOptions.OnlyOnCanceled);
                            }
                        }
                        catch(Exception ex) {
                            // Update error state.
                            emailQueueDS.UpdateState(pendingEmailNotification[i].ID, EmailNotificationState.Error);
                            emailQueueDS.Save();
                            // Build error log string and log it.
                            StringBuilder errorMessage = new StringBuilder();
                            errorMessage.AppendFormat("Error occurred during add in In-Memory queue of ID: {0}", pendingEmailNotification[i].ID);
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
                    pendingEmailNotification = null;
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
                //finally {
                //    //if(_inProgress == true)
                //    //    _inProgress = false;
                //    ////EnableTimer(true);
                //    ////StartTimer(true);
                //}
            }
        }

        private void ProcessEmailNotification(string key) {
            using(IServiceScope scope = _serviceScopeFactory.CreateScope()) {
                IEmailDeliveryLogDS emailDeliveryLogDS = scope.ServiceProvider.GetRequiredService<IEmailDeliveryLogDS>();
                IEmailDispatcher emailDispatcher = scope.ServiceProvider.GetRequiredService<IEmailDispatcher>();
                IEmailQueueDS emailQueueDS = scope.ServiceProvider.GetRequiredService<IEmailQueueDS>();
                try {

                    if(_inMemoryQueue.ContainsKey(key) == false) {
                        Thread.Sleep(2000);
                    }

                    foreach(EmailQueue itemToProcess in _inMemoryQueue[key].GetConsumingEnumerable(_cancelationTokenList[key].Token)) {
                        // This try block handle all types of errors. Also update record state in DB and log in file. 
                        //List<NotificationAttachment> attachmentList = null;
                        try {

                            #region Truncate Subject String

                            int charLength = GetSubjectMaxLength() - itemToProcess.MessagePart1.Length;
                            // This StringBuilder holds the output results.
                            StringBuilder sb = new StringBuilder();

                            StringInfo str = new StringInfo(itemToProcess.MessagePart1);
                            // Use the ParseCombiningCharacters method to 
                            // get the index of each real character in the string.
                            Int32[] textElemIndex = StringInfo.ParseCombiningCharacters(itemToProcess.MessagePart1);

                            // Iterate through each real character showing the character and the index where it was found.
                            for(int i = 0; i < textElemIndex.Length; i++) {
                                if((sb.Length + str.SubstringByTextElements(i, 1).Length) < charLength) {
                                    sb.Append(str.SubstringByTextElements(i, 1));
                                }
                                else {
                                    if(string.IsNullOrEmpty(sb.ToString()) == false) {
                                        itemToProcess.MessagePart1 = sb.ToString();
                                    }
                                    break;
                                }

                            }
                            #endregion

                            #region Send Email
                            //// Gets all attchment list.
                            //attachmentList = _notificationAttachmentDS.GetListByEntityTypeAndId(_notificationQueueEntityType, itemToProcess.NotificationQueueId);

                            //using(TransactionScope transaction = TransactionHelper.CreateTransaction(0, IsolationLevel.ReadCommitted)) {

                            // Add NotificationQueue record to 'DeliveredNotificationLog' table.
                            EmailDeliveryLog log = AddToDeliveredNotificationLog(itemToProcess, EmailNotificationState.Sent);
                            emailDeliveryLogDS.Add(log);

                            //// Dispatch NotificationQueue item.
                            ////_dispatcher.DispatchNotification(itemToProcess, attachmentList);
                            emailDispatcher.SendEmail(itemToProcess.MessagePart2, itemToProcess.Recipient, itemToProcess.MessagePart1, true);

                            int savedRecord = emailDeliveryLogDS.Save();
                            //    transaction.Complete();
                            //}
                            #endregion

                            // If this is last item in queue, cancel task.
                            if(_inMemoryQueue.ContainsKey(key) && _inMemoryQueue[key].Count == 0 || (_inMemoryQueue.ContainsKey(key) == false && _cancelationTokenList.ContainsKey(key))) {
                                _cancelationTokenList[key].Cancel();
                            }

                        }
                        // This block handle any type of error occurred during send. It will never rethrow error.
                        catch(Exception exDuringSend) {
                            // Update error state.
                            emailQueueDS.UpdateState(itemToProcess.NotificationId, EmailNotificationState.Error);
                            emailQueueDS.Save();
                            // Build error log string.
                            StringBuilder errorMessage = new StringBuilder();
                            errorMessage.AppendFormat("Error occurred during send process of NotificationQueueId: {0}", itemToProcess.ID);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Error Message:{0}", exDuringSend.Message);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Error Stacktrace:{0}", exDuringSend.StackTrace);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Email MessagePart1 :{0}", itemToProcess.MessagePart1);
                            errorMessage.AppendLine();
                            errorMessage.AppendFormat("Email MessagePart2 :{0}", itemToProcess.MessagePart2);
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
                        finally {
                            // attachmentList = null;
                        }
                    }
                }
                // Handles task cancelled error.
                catch(OperationCanceledException) {
                    BlockingCollection<EmailQueue> queueList = null;
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

        private int GetSubjectMaxLength() {
            return 250;
        }

        private EmailDeliveryLog AddToDeliveredNotificationLog(EmailQueue itemToProcess, EmailNotificationState currentState) {
            EmailDeliveryLog log = new EmailDeliveryLog();
            log.ID = Guid.NewGuid();
            log.DeliveryType = itemToProcess.DeliveryType;
            log.DeliverySubType = itemToProcess.DeliverySubType;
            log.Recipient = itemToProcess.Recipient;
            log.CC = itemToProcess.CC;
            log.BCC = itemToProcess.BCC;
            log.ReplyTo = itemToProcess.ReplyTo;
            log.MessagePart1 = itemToProcess.MessagePart1;
            log.MessagePart2 = itemToProcess.MessagePart2;
            log.Sender = itemToProcess.Sender;
            log.SenderName = itemToProcess.SenderName;
            log.SenderKey = itemToProcess.SenderKey;
            log.ScheduledDeliveryTime = itemToProcess.DeliveryTime;
            log.ActualDeliveryTime = DateTime.UtcNow;
            //log.State = itemToProcess.State;
            log.ApplicationId = itemToProcess.ApplicationId;
            log.NotificationId = itemToProcess.ID;
            log.CreatedBy = log.CreatedBy;
            log.CreatedOn = DateTime.UtcNow;
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


