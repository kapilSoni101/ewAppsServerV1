using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ewApps.Core.SMSService {
    /// <summary>
    /// This class defines the set of operations required for <see cref="SMSDeliveryLogDS"/>.
    /// </summary>
    /// <seealso cref="ISMSDeliveryLogDS" />
    public class SMSDeliveryLogDS:ISMSDeliveryLogDS {
        private SMSDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SMSDeliveryLogDS"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public SMSDeliveryLogDS(SMSDbContext dbContext) {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public SMSDeliveryLog Add(SMSDeliveryLog emailDeliveryLog) {
            EntityEntry<SMSDeliveryLog> logEntry = _dbContext.SMSDeliveryLogs.Add(emailDeliveryLog);
            return logEntry.Entity;
        }

        /// <inheritdoc/>
        public int Save() {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public SMSDeliveryLog Update(SMSDeliveryLog emailDeliveryLog) {
            EntityEntry<SMSDeliveryLog> logEntry = _dbContext.SMSDeliveryLogs.Update(emailDeliveryLog);
            return logEntry.Entity;
        }
    }
}
