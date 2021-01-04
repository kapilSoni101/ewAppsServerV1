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
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This class implements standard database logic and operations for SyncTimeLog entity.
    /// </summary>
    public class SyncTimeLogRepository:BaseRepository<SyncTimeLog, BusinessEntityDbContext>, ISyncTimeLogRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public SyncTimeLogRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }


        /// <summary>
        /// Get Sync Time log details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// //TO Query @"SELECT *FROM be.SyncTimeLog WHERE be.SyncTimeLog.TenantId = @TenantId";
        public async Task<List<BASyncTimeLogDTO>> GetSyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT be.SyncTimeLog.ERPConnectorKey, be.SyncTimeLog.ERPEntityKey, be.SyncTimeLog.ReceiveFromTime, 
                                    be.SyncTimeLog.ReceiveToTime, be.SyncTimeLog.SendSyncFromTime, be.SyncTimeLog.SendSyncToTime,
                                    be.SyncTimeLog.LastReceiveRowId, be.SyncTimeLog.LastSendRowId, be.SyncTimeLog.LastSyncStatus,
                                    be.SyncTimeLog.LastSyncStatusText
                             FROM be.SyncTimeLog where TenantId = @TenantId";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<BASyncTimeLogDTO>(query, new SqlParameter[] { paramTenantId }, token);
        }



        #endregion
    }
}
