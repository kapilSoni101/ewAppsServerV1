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
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>
    /// This class implements standard database logic and operations for SubscriptionPlan entity.
    /// </summary>
    public class SubscriptionPlanRepository:BaseRepository<SubscriptionPlan, AppMgmtDbContext>, ISubscriptionPlanRepository {

        #region Constructor
        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        ///  <param name="connectionManager"></param>
        public SubscriptionPlanRepository(AppMgmtDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }
        #endregion

        #region Get

        /// <summary>
        /// Get subscrption plan by appId.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<SubscriptionPlan> GetSubscriptionPlansByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            string query = "SELECT TOP 1 * FROM am.SubscriptionPlan WHERE AppId = @appId AND Deleted=0 ";
            SqlParameter param = new SqlParameter("@appId", appId);
            return await GetEntityAsync(query, new SqlParameter[] { param }, token);
        }

        ///<inheritdoc/>
        public async Task<List<SubscriptionPlan>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            return await _context.SubscriptionPlan.Where(sp => sp.AppId == appId && (sp.TenantId == Guid.Empty || sp.TenantId == publisherTenantId) && sp.Deleted == false).AsNoTracking().ToListAsync(token);
        }

        ///<inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppIdAsync(Guid appId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT DISTINCT  sp.[ID], sp.[IdentityNumber], [PlanName], sp.[AppId], [Term], [TermUnit], [PriceInDollar], 
                            sp.[Active], [PaymentCycle], [AllowUnlimitedTransaction], [BusinessUserCount], [CustomerUserCount], [EndDate], [OtherFeatures], 
                            [StartDate], [TransactionCount], [AppServiceCount] = Count(sps.AppServiceId) Over (Partition By sps.[SubscriptionPlanId]), app.Name AS 'AppName'
                            ,GETDATE() as CreatedOn,convert(uniqueidentifier, '00000000-0000-0000-0000-000000000000') as CreatedBy, '' as CreatedByName,
                            [AutoRenewal], [OneTimePlan], [UserPerCustomerCount], [ShipmentCount], [ShipmentUnit],[AllowUnlimitedShipment]
                            ,sp.UpdatedBy, sp.UpdatedOn,  Convert(bit, 0)  as CanDelete , '' as UpdatedByName , app.AppKey  
                            FROM am.[SubscriptionPlan] AS sp
                            LEFT JOIN am.SubscriptionPlanService AS sps ON sp.ID=sps.[SubscriptionPlanId] 
                            INNER JOIN am.App AS app ON app.ID=sp.[AppId]  
                            Where sp.AppId=@AppId AND sp.Active=@PlanState AND sp.Deleted=0";

            SqlParameter appIdParam = new SqlParameter("AppId", appId);
            SqlParameter planStateParam = new SqlParameter("PlanState", planState);
            SqlParameter[] paramList = new SqlParameter[] { appIdParam, planStateParam };
            //return await _context.SubscriptionPlanInfoDTOQuery.FromSql(query, paramList).ToListAsync();
            return await GetQueryEntityListAsync<SubscriptionPlanInfoDTO>(query, paramList, cancellationToken);
        }

        //public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanServiceAndAttributeByPlanIdsAsync(List<Guid> planIdList) {
        //    return null;
        //}

        ///<inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByTenantIdAsync(Guid tenantId, bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT sp.[ID], sp.[IdentityNumber], sp.[PlanName], sp.[AppId], sp.[Term], sp.[TransactionCount], sp.[TermUnit], sp.[PriceInDollar], 
                                sp.[Active], sp.[PaymentCycle], sp.[AllowUnlimitedTransaction], sp.[BusinessUserCount], sp.[CustomerUserCount], sp.[EndDate], sp.[OtherFeatures], 
                                sp.[StartDate],[AppServiceCount] = (select count(id) from am.SubscriptionPlanService where subscriptionPlanId =sp.id and Deleted=0 ),  app.Name AS 'AppName'  
                                ,sp.CreatedOn , sp.CreatedBy,  tu.FullName as CreatedByName
                                ,sp.[AutoRenewal], sp.[OneTimePlan], sp.[UserPerCustomerCount], sp.[ShipmentCount], sp.[ShipmentUnit],sp.[AllowUnlimitedShipment]
                                ,sp.UpdatedBy, sp.UpdatedOn, Convert(bit, 0) as CanDelete  , '' as UpdatedByName  , app.AppKey 
                                FROM am.[SubscriptionPlan] AS sp
                                
                                INNER JOIN am.App AS app ON app.ID=sp.[AppId] 
                                INNER JOIN am.Tenantuser tu on tu.Id = sp.CreatedBy

                                Where sp.TenantId=@TenantId AND sp.Deleted=0 Order by sp.CreatedOn desc";
            //and sp.Active = @PlanState
            SqlParameter appIdParam = new SqlParameter("TenantId", tenantId);
            SqlParameter planStateParam = new SqlParameter("PlanState", planState);
            SqlParameter[] paramList = new SqlParameter[] { appIdParam, planStateParam };
            return await _context.SubscriptionPlanInfoDTOQuery.FromSql(query, paramList).ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlaninfoByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken)) {
            string query = @"SELECT sp.[ID], sp.[IdentityNumber], [PlanName], sp.[AppId], [Term], [TransactionCount], [TermUnit], [PriceInDollar], 
                                      sp.[Active], [PaymentCycle], [AllowUnlimitedTransaction], [BusinessUserCount], [CustomerUserCount], [EndDate], [OtherFeatures], 
                                      [StartDate], 0 as [AppServiceCount], app.Name AS 'AppName', sp.CreatedOn, sp.CreatedBy, tu.FullName as CreatedByName,
                                      [AutoRenewal], [OneTimePlan], [UserPerCustomerCount], [ShipmentCount], [ShipmentUnit],[AllowUnlimitedShipment]
                                       ,sp.UpdatedBy, sp.UpdatedOn, tuUpdatedBy.FullName as UpdatedByName  , app.AppKey ,
                                      CanDelete= Convert(bit,Isnull((select top 1 1 FROM [am].[SubscriptionPlan] sp 
                                      WHERE Exists (
                                      SELECT TOP 1 SubscriptionPlanId FROM ap.pubbusinesssubsplan where subscriptionplanId=@planId and deleted = 0
                                      UNION
                                      SELECT top 1 SubscriptionPlanId  FROM am.tenantsubscription where subscriptionplanId=@planId and deleted = 0
                                      )),0))
                                      FROM am.[SubscriptionPlan] AS sp
                                      LEFT JOIN am.SubscriptionPlanService AS sps ON sp.ID=sps.[SubscriptionPlanId] 
                                      INNER JOIN am.App AS app ON app.ID=sp.[AppId]  
						                          INNER JOIN am.TenantUser tu on tu.ID = sp.CreatedBy
                                      INNER JOIN am.TenantUser tuUpdatedBy on tuUpdatedBy.ID = sp.UpdatedBy
                                      Where sp.Id = @planId";

            SqlParameter appIdParam = new SqlParameter("planId", id);
            //return await GetQueryEntityListAsync<SubscriptionPlanInfoDTO>(query, paramList, cancellationToken);
            return await GetQueryEntityAsync<SubscriptionPlanInfoDTO>(query, new SqlParameter[] { appIdParam }, cancellationToken);
        }

        public IEnumerable<string> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT aService.Name FROM am.SubscriptionPlanService AS spService
                INNER JOIN am.AppService as aService ON spService.AppServiceId=aService.ID
                WHERE spService.SubscriptionPlanId=@SubsPlanId";

            SqlParameter subsPlanIdParam = new SqlParameter("SubsPlanId", subsPlanId);
            return _context.AppService.FromSql(sql, new object[] { subsPlanIdParam }).Select(k=>k.Name);
        }


        #endregion Get

    }
}
