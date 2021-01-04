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
    /// This class implements standard database logic and operations for Tenant Report entity.
    /// </summary>
    public class QTenantReportRepository : BaseRepository<BaseDTO, QReportDbContext>, IQTenantReportRepository
    {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QTenantReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager)
        {
        }

        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<List<TenantReportDTO>> GetTenantListAsync(ReportFilterDTO filter, Guid tenantId, Guid homeAppId, Guid appId, CancellationToken token = default(CancellationToken))
        {

            SqlParameter start = new SqlParameter("@startDate", filter.FromDate);
            SqlParameter end = new SqlParameter("@endDate", filter.ToDate);
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter homeAppIdParam = new SqlParameter("@homeAppId", homeAppId);
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            return this.GetQueryEntityList<TenantReportDTO>("prcQGetFilterVendorViewModelReportList @startDate, @endDate, @tenantId, @homeAppId ,@AppId", new SqlParameter[] { start, end, tenantIdParam, homeAppIdParam, appIdParam });
                        
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetServiceNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
        {

            FormattableString query = $@"SELECT a.Name FROM am.AppService a 
                                   INNER JOIN am.TenantSubscription ts ON ts.AppId = a.AppId AND 
                                   a.Deleted ='false' AND ts.TenantId = @TenantId";
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            List<NameDTO> serviceNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { tenantIdParam });
            return serviceNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetTenantNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT t.Name FROM am.TenantSubscription ts 
										INNER JOIN am.Tenant t on t.ID = ts.TenantId
								        INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = t.ID  
										WHERE AppId = @AppId and t.Deleted = 0 AND ts.Deleted = 0 
                                        AND t.TenantType = 3 and tl.PublisherTenantId = @TenantId
                                        AND tl.BusinessPartnerTenantId is null";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      List<NameDTO> tennatNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { appIdParam, tenantIdParam }, token);
            return tennatNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetServiceNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT a.Name FROM am.AppService a
								   INNER JOIN ap.PubBusinessSubsPlanAppService pbspa on pbspa.AppServiceId = a.ID and pbspa.TenantId = @TenantId 
								   WHERE a.AppId = @AppId and pbspa.TenantId = @TenantId Group by pbspa.AppServiceId,a.Name";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            List<NameDTO> serviceNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { appIdParam, tenantIdParam });
            return serviceNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT Name FROM am.AppService WHERE AppId = @AppId";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            List<NameDTO> serviceNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { appIdParam });
            return serviceNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetTenantNameListBySubscriptionPlanIdAsync(Guid subscriptionPlanId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT t.Name FROM am.Tenant t 
                                   INNER JOIN am.TenantSubscription ts ON ts.TenantId = t.ID AND 
                                   ts.Deleted = 0 AND ts.SubscriptionPlanId = @SubscriptionPlanId AND t.TenantType = 3";
            SqlParameter subscriptionPlanIdParam = new SqlParameter("@SubscriptionPlanId", subscriptionPlanId);
            List<NameDTO> TenantNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { subscriptionPlanIdParam });
            return TenantNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFTenantNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT t.Name                                  
                                  FROM am.Tenant AS t
                                  INNER JOIN am.TenantSubscription AS ts ON t.ID=ts.TenantId
                                  WHERE (ts.AppId=@AppId) AND (ts.Deleted = 0) AND t.TenantType = 3 AND t.Deleted = 0";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            List<NameDTO> tennatNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { appIdParam }, token);
            return tennatNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@"SELECT T.Name FROM am.TenantLinking tl 
								         INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId
								         INNER JOIN ap.Publisher pub on pub.TenantId = TL.PublisherTenantId 
                                         WHERE pub.Id = @PublisherId AND tl.BusinessTenantId IS NOT NULL 
								         AND T.Deleted = 0 and t.TenantType = 3
                                         Group By T.Id,T.Name";    
            SqlParameter pubIdParam = new SqlParameter("@PublisherId", publisherId);
            List<NameDTO> tennatNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { pubIdParam }, token);
            return tennatNameList;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFActiveTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken))
        {
            FormattableString query = $@" SELECT T.Name FROM am.TenantLinking tl 
								          INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId
								          INNER JOIN ap.Publisher pub on pub.TenantId = TL.PublisherTenantId 
                                         WHERE pub.Id = @PublisherId AND tl.BusinessTenantId IS NOT NULL 
								         AND T.Deleted = 0 and t.TenantType = 3 AND T.Active = 1
                                         Group By T.Id,T.Name";
            SqlParameter pubIdParam = new SqlParameter("@PublisherId", publisherId);
            List<NameDTO> tennatNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { pubIdParam }, token);
            return tennatNameList;
        }

        ///<inheritdoc/>
        public async Task<List<PlatformTenantDTO>> GetTenantReportListOnPlatformAsync(ReportFilterDTO filter, Guid homeAppId,  CancellationToken token = default(CancellationToken)) {

            SqlParameter start = new SqlParameter("@startDate", filter.FromDate);
            SqlParameter end = new SqlParameter("@endDate", filter.ToDate);            
            SqlParameter homeAppIdParam = new SqlParameter("@homeAppId", homeAppId);           
            return this.GetQueryEntityList<PlatformTenantDTO>("prcQPlatGetFilterVendorViewModelReportListForPlatform @startDate, @endDate, @homeAppId", new SqlParameter[] { start, end, homeAppIdParam});


        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFServiceNameListByPlanIdAsync(Guid planId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT aSer.Name FROM am.SubscriptionPlanService AS sps
						                INNER JOIN am.AppService AS aSer ON sps.AppServiceId=aSer.ID
						                WHERE sps.deleted=0 and sps.SubscriptionPlanId = @PlanId";
            SqlParameter subscriptionPlanIdParam = new SqlParameter("@PlanId", planId);
            List<NameDTO> serviceNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), new object[] { subscriptionPlanIdParam });
            return serviceNameList;
        }


        #endregion
    }
}
