/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Anil Nigam<anigam @eworkplaceapps.com>
 * Date:08 july 2019
 * 
 */


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
  /// <summary>
  /// Responsible for exposing all the methods that are intrecting with 
  /// the DB for retriving the data related to SyncTimeLog entity.
  /// </summary>
  public interface ISyncTimeLogDS:IBaseDS<SyncTimeLog> {

    /// <summary>
    /// Add Sync Time log 
    /// </summary>
    /// <param name="ErpConnectorKey"></param>
    /// <param name="ErpCustomerKey"></param>
    /// <param name="Error"></param>
    /// <param name="Status"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task AddSyncTimeLogByStatus(Guid tenantId, string ErpConnectorKey, string ErpEntityKey, string lastSyncRowId, string Error, int Status, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get SyncTimeLog
        /// </summary>
        /// <returns></returns>
        Task<List<BASyncTimeLogDTO>> GetSyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
