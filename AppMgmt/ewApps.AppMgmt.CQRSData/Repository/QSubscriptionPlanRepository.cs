using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.QData {

    public class QSubscriptionPlanRepository: QBaseRepository<QAppMgmtDbContext> , IQSubscriptionPlanRepository{

        #region Constructor

        /// <summary>
        /// Initlize local variables
        /// </summary>
        /// <param name="qAppMgmtDbContext"></param>
        public QSubscriptionPlanRepository(QAppMgmtDbContext qAppMgmtDbContext): base(qAppMgmtDbContext) {
            
        }

    #endregion Constructor        

    #region Get

    ///<inheritdoc/>
    public async Task<SubscriptionPlanInfoDTO> GetSubscriptionPlanReferece(Guid id, CancellationToken cancellationToken = default(CancellationToken)) {
      string query = @" select sp.[ID], sp.[IdentityNumber], sp.[PlanName], sp.[AppId], sp.[Term], sp.[TransactionCount], sp.[TermUnit], sp.[PriceInDollar], 
                sp.[Active], sp.[PaymentCycle], sp.[AllowUnlimitedTransaction], sp.[BusinessUserCount], sp.[CustomerUserCount], sp.[EndDate], sp.[OtherFeatures], 
                sp.[StartDate],0 as AppServiceCount, '' as Appkey ,
                '' as AppName  ,GETDATE() as CreatedOn, convert(uniqueidentifier, '00000000-0000-0000-0000-000000000000') as CreatedBy, '' as CreatedByName
                , sp.[AutoRenewal],  sp.[OneTimePlan],  sp.[UserPerCustomerCount],  sp.[ShipmentCount],  sp.[ShipmentUnit] ,sp.[AllowUnlimitedShipment]
                ,sp.UpdatedBy, sp.UpdatedOn,  Convert(bit, 0)  as CanDelete  , '' as UpdatedByName 
                FROM [am].[SubscriptionPlan] sp 
                WHERE sp.Id in (
                SELECT TOP 1 SubscriptionPlanId FROM ap.pubbusinesssubsplan where subscriptionplanId=@planId and deleted = 0
                UNION
                SELECT top 1 SubscriptionPlanId  FROM am.tenantsubscription where subscriptionplanId=@planId and deleted = 0
                )";

      SqlParameter appIdParam = new SqlParameter("planId", id);
      return await GetQueryEntityAsync<SubscriptionPlanInfoDTO>(query, new SqlParameter[] { appIdParam }, cancellationToken);
    }

    ///<inheritdoc/>
    public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(Guid tenantId, bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
      string query = @"SELECT sp.[ID], sp.[IdentityNumber], sp.[PlanName], sp.[AppId], sp.[Term], sp.[TransactionCount], sp.[TermUnit], sp.[PriceInDollar], 
        sp.[Active], sp.[PaymentCycle], sp.[AllowUnlimitedTransaction], sp.[BusinessUserCount], sp.[CustomerUserCount], sp.[EndDate], sp.[OtherFeatures], 
        sp.[StartDate],[AppServiceCount] = (select count(id) from am.SubscriptionPlanService where subscriptionPlanId =sp.id and Deleted=0 ),  app.Name AS 'AppName'  
        ,GETDATE() as CreatedOn, convert(uniqueidentifier, '00000000-0000-0000-0000-000000000000') as CreatedBy, '' as CreatedByName
        ,sp.[AutoRenewal], sp.[OneTimePlan], sp.[UserPerCustomerCount], sp.[ShipmentCount], sp.[ShipmentUnit],sp.[AllowUnlimitedShipment]
        ,sp.UpdatedBy, sp.UpdatedOn,  Convert(bit, 0)  as CanDelete  , '' as UpdatedByName , app.AppKey 
        FROM am.[SubscriptionPlan] AS sp
                                
        INNER JOIN am.App AS app ON app.ID=sp.[AppId] 
        inner join ap.pubbusinesssubsplan  pb
        on sp.id= pb.SubscriptionPlanId 
        Where pb.TenantId=@TenantId AND sp.Deleted=0  Order by sp.CreatedOn desc";
      //and sp.Active = @PlanState
      SqlParameter appIdParam = new SqlParameter("TenantId", tenantId);
      SqlParameter planStateParam = new SqlParameter("PlanState", planState);
      SqlParameter[] paramList = new SqlParameter[] { appIdParam, planStateParam };
      //return await _context.SubscriptionPlanInfoDTOQuery.FromSql(query, paramList).ToListAsync();

      return await _context.SubscriptionPlanInfoDTOQuery.FromSql(query, paramList).ToListAsync();

  //    return await GetQueryEntityAsync<SubscriptionPlanInfoDTO>(query, new SqlParameter[] { appIdParam }, cancellationToken);

    }

    #endregion Get      
  }
}
