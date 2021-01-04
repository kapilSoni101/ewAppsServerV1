using Microsoft.Extensions.Logging;
using System;

namespace ewApps.Core.LoggingService {
    /// <summary>
    /// Contains static methods to log diffrent type of messages.
    /// </summary>
    public class Logger {

        private readonly ILogger _logger = null;

        public Logger(ILogger<Logger> logger) {
            _logger = logger;
        }

        /// <summary>
        /// Log information message.
        /// </summary>
        public void LogInfo(string infoMessage) {
            _logger.LogInformation("Information: {0} ", infoMessage);
        }

        /// <summary>
        /// Log error detail.
        /// </summary>
        public void LogError(Exception ex, string message) {
            _logger.LogError(ex, "\r\n ========================================================================= \r\n Error: \r\n  {0} \r\n ", message);
        }

        /// <summary>
        /// Log the warning message.
        /// </summary>
        public void LogWarning(string message) {
            _logger.LogWarning("Warning: {0} ", message);
        }

        /// <summary>
        /// Set the line break
        /// </summary>        
        /// <param name="level">Level is used to define the type of error message for which we are giving the line break.</param>
        public void LineBreak(LogLevel level) {
            _logger.Log(level, "\n", null);
        }

        /// <summary>
        /// Enter seperator in log file.
        /// </summary>    
        public void Seperator(LogLevel level, string seperator = "============================================") {
            _logger.Log(level, seperator, null);
        }

    }
}
