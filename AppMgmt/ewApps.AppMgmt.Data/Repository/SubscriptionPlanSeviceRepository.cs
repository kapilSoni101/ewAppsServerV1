/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This class implements standard database logic and operations for SubscriptionPlan entity.
    /// </summary>
    public class SubscriptionPlanServiceRepository:BaseRepository<SubscriptionPlanService, AppMgmtDbContext>, ISubscriptionPlanServiceRepository {

        #region Constructor
        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        ///  <param name="connectionManager"></param>
        public SubscriptionPlanServiceRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        public async Task<List<SubsPlanServiceInfoDTO>> GetPlanServiceListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            //string sql = @"SELECT [ID] AS 'SubscriptionPlanServiceId', [SubscriptionPlanId], [AppServiceId] FROM am.[SubscriptionPlanService]
            //               WHERE deleted=0 and SubscriptionPlanId IN ({0})";
            string sql = @"SELECT sps.[ID] AS 'SubscriptionPlanServiceId', [SubscriptionPlanId], [AppServiceId], aSer.Name AS [ServiceName], aSer.Active FROM am.[SubscriptionPlanService] AS sps
                           INNER JOIN am.AppService AS aSer ON sps.AppServiceId=aSer.ID
                           WHERE sps.deleted=0 and  SubscriptionPlanId IN ({0})";

            string planIdString = string.Format("{0}{1}{2}", "'", string.Join("','", planIdList), "'");

            sql = string.Format(sql, planIdString);

            List<SubsPlanServiceInfoDTO> subsPlanServiceInfoDTOs = await GetQueryEntityListAsync<SubsPlanServiceInfoDTO>(sql, null, cancellationToken);
            return subsPlanServiceInfoDTOs;
        }


        //#region Get

        ///// <summary>
        ///// Get subscrption plan by appId.
        ///// </summary>
        ///// <param name="appId"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public async Task<SubscriptionPlan> GetSubscriptionPlansByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

        //    string query = "SELECT TOP 1 * FROM am.SubscriptionPlan WHERE AppId = @appId AND Deleted=0 ";
        //    SqlParameter param = new SqlParameter("@appId", appId);
        //    return await GetEntityAsync(query, new SqlParameter[] { param }, token);
        //}

        /////<inheritdoc/>
        //public async Task<List<SubscriptionPlan>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {            
        //    return await _context.SubscriptionPlan.Where(sp => sp.AppId == appId && (sp.TenantId == Guid.Empty || sp.TenantId == publisherTenantId) && sp.Deleted == false).AsNoTracking().ToListAsync(token);
        //}

        //#endregion Get

    }
}
