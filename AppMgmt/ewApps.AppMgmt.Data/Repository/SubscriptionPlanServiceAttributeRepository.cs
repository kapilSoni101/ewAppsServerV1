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
    public class SubscriptionPlanServiceAttributeRepository:BaseRepository<SubscriptionPlanServiceAttribute, AppMgmtDbContext>, ISubscriptionPlanServiceAttributeRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        ///  <param name="connectionManager"></param>
        public SubscriptionPlanServiceAttributeRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion


        /// <inheritdoc/>
        public async Task<List<SubsPlanServiceAttributeInfoDTO>> GetPlanServiceAttributeListByPlanIdsAsync(List<Guid> planIdList, CancellationToken cancellationToken = default(CancellationToken)) {
            //string sql = @"SELECT [ID] AS 'SubsPlanServiceAttributeId', [SubscriptionPlanId], [SubscriptionPlanServiceId], [AppServiceAttributeId] FROM am.[SubscriptionPlanServiceAttribute]
            //                   WHERE deleted=0 and SubscriptionPlanId IN ({0})";
            string sql = @"SELECT spsa.[ID] AS 'SubsPlanServiceAttributeId', [SubscriptionPlanId], [SubscriptionPlanServiceId], [AppServiceAttributeId], asa.Name AS 'AttributeName', asa.Active FROM am.[SubscriptionPlanServiceAttribute] AS spsa
                           INNER JOIN am.AppServiceAttribute AS asa ON spsa.AppServiceAttributeId=asa.ID                                
                           WHERE spsa.deleted=0 and  SubscriptionPlanId IN ({0})";

            string planIdString = string.Format("{0}{1}{2}", "'", string.Join("','", planIdList), "'");

            sql = string.Format(sql, planIdString);

            List<SubsPlanServiceAttributeInfoDTO> subsPlanServiceAttrInfoDTOs = await GetQueryEntityListAsync<SubsPlanServiceAttributeInfoDTO>(sql, null, cancellationToken);
            return subsPlanServiceAttrInfoDTOs;
        }

    }
}
