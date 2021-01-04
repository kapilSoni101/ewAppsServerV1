/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 02 July 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 July 2019
 */

using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ewApps.Core.EmailService {
    /// <summary>
    /// This class defines the set of operations required for <see cref="EmailDeliveryLogDS"/>.
    /// </summary>
    /// <seealso cref="IEmailDeliveryLogDS" />
    public class EmailDeliveryLogDS:IEmailDeliveryLogDS {
        private EmailDBContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailDeliveryLogDS"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EmailDeliveryLogDS(EmailDBContext dbContext) {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public EmailDeliveryLog Add(EmailDeliveryLog emailDeliveryLog) {
            EntityEntry<EmailDeliveryLog> logEntry = _dbContext.EmailDeliveryLogs.Add(emailDeliveryLog);
            return logEntry.Entity;
        }

        /// <inheritdoc/>
        public EmailDeliveryLog Update(EmailDeliveryLog emailDeliveryLog) {
            EntityEntry<EmailDeliveryLog> logEntry = _dbContext.EmailDeliveryLogs.Update(emailDeliveryLog);
            return logEntry.Entity;
        }

        /// <inheritdoc/>
        public int Save() {
            return _dbContext.SaveChanges();
        }
    }
}
