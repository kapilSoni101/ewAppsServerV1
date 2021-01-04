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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace ewApps.Core.EmailService {
    public class EmailQueueDS:IEmailQueueDS {

        EmailDBContext _dbContext;

        #region Constructor

        public EmailQueueDS(EmailDBContext dbContext, IOptions<EmailAppSettings> appSetting) {
            _dbContext = dbContext;
        }

        #endregion

        /// <inheritdoc/>
        public EmailQueue Add(EmailQueue notificationQueue) {
            EntityEntry<EmailQueue> savedEntity = _dbContext.EmailQueues.Add(notificationQueue);
            return savedEntity.Entity;
        }

        /// <inheritdoc/>
        public List<EmailQueue> GetPendingEmailNotificationList(DateTime fromDate) {
            // return _dbContext.EmailQueues.Where(i => i.DeliveryTime <= fromDate).ToList();
            return _dbContext.EmailQueues.FromSql("EXECUTE prcDequeueEmailQueue").ToList();
        }

        /// <inheritdoc/>
        public int Save() {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void UpdateState(Guid notificationQueueId, EmailNotificationState notificationState, bool commit = true) {
            EmailQueue queuedItem = _dbContext.EmailQueues.Find(notificationQueueId);
            queuedItem.State = (int)notificationState;
            _dbContext.EmailQueues.Update(queuedItem);
            if(commit) {
                Save();
            }
        }
    }
}
