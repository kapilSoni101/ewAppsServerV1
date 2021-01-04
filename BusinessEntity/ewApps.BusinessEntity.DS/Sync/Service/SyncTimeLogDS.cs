/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:09 september 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

  /// <summary>
  ///  This class implements standard business logic and operations for SyncTimeLog entity.
  /// </summary>
  public class SyncTimeLogDS:BaseDS<SyncTimeLog>, ISyncTimeLogDS {

    #region Local variables

    ISyncTimeLogRepository _syncTimeLogRepo;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// 
    /// </summary>
    /// <param name="syncTimeLogRepo"></param>
    public SyncTimeLogDS(ISyncTimeLogRepository syncTimeLogRepo) : base(syncTimeLogRepo) {
      _syncTimeLogRepo = syncTimeLogRepo;
    }

    #endregion Constructor

    public async Task AddSyncTimeLogByStatus(Guid tenantId, string ErpConnectorKey, string ErpEntityKey,string lastSyncRowId, string Error, int Status, CancellationToken token = default(CancellationToken)) {
      
      SyncTimeLog  syncTimeLog = await FindAsync(i => i.TenantId == tenantId  && i.ERPEntityKey == ErpEntityKey);

      if(syncTimeLog == null) {
        syncTimeLog = new SyncTimeLog();
      }
      syncTimeLog.ERPConnectorKey = ErpConnectorKey;
      syncTimeLog.ERPEntityKey = ErpEntityKey;
      syncTimeLog.ReceiveFromTime = DateTime.UtcNow;
      syncTimeLog.ReceiveToTime = DateTime.UtcNow;
      syncTimeLog.SendSyncFromTime = DateTime.UtcNow;
      syncTimeLog.SendSyncToTime = DateTime.UtcNow;
      syncTimeLog.SendSyncToTime = DateTime.UtcNow;
      syncTimeLog.LastReceiveRowId = ErpEntityKey;
      syncTimeLog.LastSendRowId = null;
      syncTimeLog.LastSyncStatus = Status;
      syncTimeLog.LastSyncStatusText = Error;

      if(syncTimeLog.ID !=  Guid.Empty) {
        UpdateSystemFieldsByOpType(syncTimeLog, OperationType.Update);
        await _syncTimeLogRepo.UpdateAsync(syncTimeLog, syncTimeLog.ID, token);
      }
      else {
        UpdateSystemFieldsByOpType(syncTimeLog, OperationType.Add);
        await _syncTimeLogRepo.AddAsync(syncTimeLog, token);
      }

       _syncTimeLogRepo.Save();

    }

        /// <summary>
        /// Get Sync Time Log details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BASyncTimeLogDTO>> GetSyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            List<BASyncTimeLogDTO> bASyncTimeLogDTO = await _syncTimeLogRepo.GetSyncTimeLogAsync(tenantId, token);
            return bASyncTimeLogDTO;

        }
    }
}

