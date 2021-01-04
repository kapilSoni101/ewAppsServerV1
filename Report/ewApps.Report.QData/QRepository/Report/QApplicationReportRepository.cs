// Hari sir comment
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
using Microsoft.EntityFrameworkCore;

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for Application Report entity.
    /// </summary>
    public class QApplicationReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQApplicationReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QApplicationReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion Constructor

    #region Get

    #region Publisher Reports
    ///<inheritdoc/>
    public async Task<List<ApplicationReportDTO>> GetApplicationListAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT a.ID,IdentityNumber,pas.Name AS ApplicationName
                                  ,(SELECT COUNT(1) FROM am.SubscriptionPlan WHERE AppId = a.id AND TenantId = @TenantId AND (CreatedOn Between @FromDate AND @ToDate)) AS Subscriptions
                                  ,(SELECT COUNT(DISTINCT appserviceid) FROM ap.PubBusinessSubsPlanAppService where AppId= a.id and TenantId = @TenantId AND (CreatedOn Between @FromDate AND @ToDate)) AS Services
                                  ,(SELECT  COUNT(1) FROM am.TenantSubscription ts INNER JOIN am.Tenant t on t.ID = ts.TenantId
								  INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = t.ID  
                                  WHERE AppId = a.id and t.Deleted = 0 and ts.Deleted = 0 and t.TenantType = 3 and 
								  tl.PublisherTenantId = @TenantId and tl.BusinessPartnerTenantId is null AND (t.CreatedOn Between @FromDate AND @ToDate)) AS Tenants
                                  ,a.Active,a.Deleted,'' AS Status
                                  FROM am.App a
								  INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.Id 
                                  WHERE a.AppSubscriptionMode = 2 AND a.Deleted = 0 AND pas.TenantId = @TenantId";
      SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

      List<ApplicationReportDTO> appDTOs = await base.GetQueryEntityListAsync<ApplicationReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantIdParam });
      return appDTOs;
    }


    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetApplicaitionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT pas.Name FROM am.App a 
                                INNER JOIN am.TenantSubscription ts ON ts.AppId = a.Id
                                INNER JOIN am.TenantLinking tl ON tl.BusinessTenantId = ts.TenantId and BusinessPartnerTenantId is null 
                                INNER JOIN ap.PublisherAppSetting pas ON pas.AppId = a.Id AND tl.PublisherTenantId = pas.TenantId AND  
                                ts.TenantId = @TenantId GROUP BY pas.Name";
      SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
      List<NameDTO> applicationNameList = await _context.Query<NameDTO>().FromSql(query.ToString(), new object[] { tenantIdParam }).ToListAsync();
      return applicationNameList;
    }
    #endregion

    #region PlatForm
    ///<inheritdoc/>
    public async Task<List<PlatApplicationReportDTO>> GetPFApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      FormattableString query = $@"SELECT a.ID,a.Deleted,Name as ApplicationName,a.Active,		
					                        (SELECT COUNT(id) FROM am.AppService WHERE AppId= a.id ) Servicecount ,
					                        (SELECT COUNT(distinct p.id) FROM ap.Publisher p 
								            INNER JOIN ap.PublisherAppSetting ps on p.TenantId = ps.TenantId
								            WHERE p.deleted = 0 and ps.AppId = a.Id  AND (p.CreatedOn Between @FromDate AND @ToDate)) Publishercount, 
					                        (select COUNT(distinct t.Id) FROM am.TenantSubscription ts 
					                        INNER JOIN am.Tenant t ON t.ID=ts.TenantId WHERE AppId= a.id AND ts.Deleted=0 AND t.TenantType = 3 
                                            AND t.Deleted = 0 AND (t.CreatedOn Between @FromDate AND @ToDate)) TenantCount, 
					                        a.IdentityNumber,'' Status  FROM am.App a WHERE a.deleted=0 AND a.AppSubscriptionMode = 2";

      SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
      SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

      List<PlatApplicationReportDTO> appDTOs = await base.GetQueryEntityListAsync<PlatApplicationReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate });
      return appDTOs;
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFApplicaitionNameListAsync(Guid publisherId , CancellationToken token = default(CancellationToken)) {
     
        FormattableString query = $@"SELECT a.Name FROM am.App a
									 INNER JOIN ap.PublisherAppSetting ps on a.Id = ps.AppId
									 INNER JOIN ap.Publisher pub on ps.TenantId = pub.TenantId 
									 WHERE a.AppSubscriptionMode=2 AND a.Deleted = 0 and pub.ID = @PublisherId AND ps.Deleted = 0";
      SqlParameter publisherIdParam = new SqlParameter("@PublisherId", publisherId);
      List<NameDTO> applicationNameList = await _context.Query<NameDTO>().FromSql(query.ToString(), new object[] { publisherIdParam }).ToListAsync();
        return applicationNameList;    
     
    }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetBusinessAppSubscriptionInfoDTOByBusinessId(Guid tenantId,Guid homeAppId) {

            FormattableString query = $@"SELECT a.Name FROM am.App a
                                         INNER JOIN am.TenantSubscription ts ON ts.AppId = a.ID 
                                         WHERE ts.TenantId = @TenantId and AppId <> @homeAppId";
            SqlParameter tenantIdParam = new SqlParameter("@tenantId", tenantId);
            SqlParameter homeAppIdParam = new SqlParameter("@homeAppId", homeAppId);
            List<NameDTO> applicationNameList = await _context.Query<NameDTO>().FromSql(query.ToString(), new object[] { tenantIdParam, homeAppIdParam }).ToListAsync();
            return applicationNameList;

        }
        #endregion

        #endregion Get
    }
}
