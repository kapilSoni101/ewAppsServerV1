/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace ewApps.Core.SMSService {
    public class SMSQueueDS:ISMSQueueDS {
        SMSDbContext _dbContext;

        #region Constructor

        public SMSQueueDS(SMSDbContext dbContext, IOptions<SMSAppSettings> appSetting) {
            _dbContext = dbContext;
        }

        #endregion

        public SMSQueue Add(SMSQueue notificationQueue) {
            EntityEntry<SMSQueue> savedEntity = _dbContext.SMSQueues.Add(notificationQueue);
            return savedEntity.Entity;
        }

        public List<SMSQueue> GetPendingSMSNotificationList(DateTime fromDate) {
            //return _dbContext.SMSQueues.Where(i => i.DeliveryTime <= fromDate).ToList();
            return _dbContext.SMSQueues.FromSql("EXECUTE prcDequeueSMSQueue").ToList();
        }

        public int Save() {
            return _dbContext.SaveChanges();
        }

        public void UpdateState(Guid notificationQueueId, SMSNotificationState notificationState, bool commit = true) {
            SMSQueue queuedItem = _dbContext.SMSQueues.Find(notificationQueueId);
            queuedItem.State = (int)notificationState;
            _dbContext.SMSQueues.Update(queuedItem);
            if(commit) {
                Save();
            }
        }
    }
}
