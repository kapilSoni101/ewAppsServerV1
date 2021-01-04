/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for Subscription Report entity.
    /// </summary>
    public class QSubcriptionReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQSubcriptionReportRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        ///  <param name="connectionManager"></param>
        public QSubcriptionReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<List<SubscriptionReportDTO>> GetSubscriptionListAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT sp.Id,sp.IdentityNumber,sp.Deleted,sp.Active,'' as Status,
                                    sp.PlanName as SubscriptionName,sp.Term,sp.TermUnit,sp.PaymentCycle,sp.PriceInDollar,
                                    app.Name as Application, 
                                    (SELECT COUNT(Distinct(ts.TenantId)) FROM am.TenantSubscription ts 
									INNER JOIN am.Tenant t on t.Id = ts.TenantId AND t.TenantType = 3
                                    WHERE ts.SubscriptionPlanId = sp.Id AND ts.Deleted = 0 AND (t.CreatedOn BETWEEN @FromDate AND @ToDate)) AS Tenants 
									FROM am.[SubscriptionPlan] AS sp                                
                                    INNER JOIN am.App AS app ON app.ID=sp.[AppId] 
                                    INNER JOIN ap.pubbusinesssubsplan  pb
                                    on sp.id= pb.SubscriptionPlanId 
                                    Where pb.TenantId=@TenantId AND sp.Deleted=0  AND (sp.CreatedOn BETWEEN @FromDate AND @ToDate)  ORDER BY sp.CreatedOn DESC ";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantIdparam = new SqlParameter("@TenantId", tenantId);
            List<SubscriptionReportDTO> subcriptionList = await GetQueryEntityListAsync<SubscriptionReportDTO>(query.ToString(), new object[] { fromDate, toDate, tenantIdparam });
            return subcriptionList;

        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetSubscriptionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT pbsp.PlanName AS Name FROM ap.PubBusinessSubsPlan pbsp 
                                   INNER JOIN am.TenantSubscription ts ON ts.SubscriptionPlanId = pbsp.Id 
								   INNER JOIN am.App a ON a.ID = ts.AppId WHERE AppSubscriptionMode = 2
								   AND ts.TenantId = @TenantId";
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            List<NameDTO> subcriptionNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { tenantIdParam });
            return subcriptionNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetSubscriptionNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT s.PlanName AS Name FROM am.SubscriptionPlan s 
                                   WHERE s.AppId = @AppId AND TenantId = @TenantId";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            List<NameDTO> subcriptionNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { appIdParam, tenantIdParam });
            return subcriptionNameList;
        }

        #region Platform Subscrition Report
        public async Task<List<PlatSubscriptionReportDTO>> GetPlatSubscriptionListAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT sp.ID, sp.IdentityNumber, sp.PlanName,sp.PriceInDollar, 
                                sp.Active, sp.BusinessUserCount, sp.CustomerUserCount, sp.EndDate, 
								AppServiceCount = (select count(id) from am.SubscriptionPlanService where subscriptionPlanId =sp.id and Deleted=0 ),  
								app.Name AS 'AppName'  
                                ,sp.CreatedOn,sp.Term,sp.TermUnit,0.1 as TotalBilling  
                                ,sp.AutoRenewal, sp.UserPerCustomerCount ,sp.AllowUnlimitedShipment,sp.Deleted,p.Name as PublisherName,t.Name as BusinessName
                                FROM am.SubscriptionPlan AS sp
                                INNER JOIN am.App AS app ON app.ID=sp.AppId
								LEFT JOIN ap.PubBusinessSubsPlan pbsp ON pbsp.SubscriptionPlanId = sp.ID AND sp.AppId = pbsp.AppId
								LEFT JOIN ap.Publisher p ON p.TenantId = pbsp.TenantId
								LEFT JOIN am.TenantSubscription ts ON ts.SubscriptionPlanId = pbsp.ID AND ts.AppId = pbsp.AppId
								LEFT JOIN am.Tenant t ON t.Id = ts.TenantId  
								Where sp.TenantId=@TenantId AND (sp.CreatedOn BETWEEN @FromDate AND @ToDate) Order BY sp.CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantIdparam = new SqlParameter("@TenantId", tenantId);
            List<PlatSubscriptionReportDTO> subcriptionList = await GetQueryEntityListAsync<PlatSubscriptionReportDTO>(query.ToString(), new object[] { fromDate, toDate, tenantIdparam });
            return subcriptionList;

        } 
        #endregion
        #endregion
    }
}
