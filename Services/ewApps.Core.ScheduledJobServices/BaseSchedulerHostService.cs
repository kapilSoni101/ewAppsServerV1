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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.ScheduledJobService {

    public abstract class BaseSchedulerService: IDisposable {
        public ILogger _logger;
        private Timer _timer;
        private CancellationTokenSource _stoppingCts = null;
        private int _timerTickInterval = 10000;

        protected IServiceScopeFactory _serviceScopeFactory;

        /// <summary>
        /// If it is true, skip schduler execution.
        /// </summary>
        protected bool _inProgress = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSchedulerService"/> class.
        /// </summary>
        /// <param name="timerTickInterval">The timer tick interval.</param>
        /// <param name="logger">The logger instace to log any error and/or information.</param>
        /// <param name="scopeFactory">The scope factory instance to create object using DI.</param>
        public BaseSchedulerService(int timerTickInterval, ILogger<BaseSchedulerService> logger, IServiceScopeFactory scopeFactory) {
            _logger = logger;
            _serviceScopeFactory = scopeFactory;
            _timerTickInterval = timerTickInterval;
        }

        /// <summary>
        /// Starts when application starts.
        /// </summary>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. 
        /// Using this token async operation duration can be control.
        /// </param>
        /// <returns>Returns async task instace.</returns>
        public Task StartAsync(CancellationToken token) {
            // Initializes timer instance with configured timer interval.
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(token);
            _timer = new Timer(OnTimerCallback, null, 0, _timerTickInterval);
            return Task.CompletedTask;
        }

        // This method will execute on fixed time interval of timer and call scheduler execution function.
        private void OnTimerCallback(object state) {
            try {
                if(_inProgress == false) {
                    _inProgress = true;
                    // Console.WriteLine("OnTimerCallback is starting.");

                    Task.Run(() => ExecuteAsync(DateTime.UtcNow, _stoppingCts.Token))
                        // Continue the async execution with execution resultion.
                        .ContinueWith((t) => {
                            // Mark flag to False to allow new execution of scheduler.
                            _inProgress = false;

                            // If async call is falted log exception detail.
                            if(t.IsFaulted) {
                                _logger.LogError(t.Exception.Message, t.Exception);
                            }

                            // Reset timer tick interval to configured value to start timer.
                            _timer.Change(0, _timerTickInterval);
                        });
                }
            }
            catch(Exception ex) {
                _logger.LogError(ex.Message, ex);
                _inProgress = false;
            }
        }

        /// <summary>
        /// Stop triggers when application is shutdown gracefully.
        /// </summary>
        public Task StopAsync(CancellationToken cancellationToken) {
            // Console.WriteLine("Timed Background Service is stopping.");
            // _logger.LogInformation("Timed Background Service is stopping.");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Dispose is called by GC.
        /// </summary>
        public void Dispose() {
            _timer?.Dispose();
        }

        /// <summary>
        /// Executes the operation on every inteval of scheduled timer.
        /// </summary>
        /// <param name="currentSchedulingTimeInUTC">The current scheduling time in UTC.</param>
        /// <param name="token">The task cancellation token to handle task cancellation.</param>
        /// <returns>Returns async task instance.</returns>
        public abstract Task ExecuteAsync(DateTime currentSchedulingTimeInUTC, CancellationToken token);

    }
}
