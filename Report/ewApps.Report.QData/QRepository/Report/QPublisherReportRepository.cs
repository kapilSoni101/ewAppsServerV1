/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 5 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 February 2019
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
    /// This class implements standard database logic and operations for Publisher Report entity.
    /// </summary>
    public class QPublisherReportRepository :BaseRepository<BaseDTO, QReportDbContext>, IQPublisherReportRepository {

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sessionManager"></param>
    /// <param name="connectionManager"></param>
    public QPublisherReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }



        #endregion Constructor
        #region Get   

        #region PlatForm Reports 
        ///<inheritdoc/>
        public async Task<List<PlatPublisherReportDTO>> GetPFPublisherListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT pub.ID, au.FullName AS CreatedByName,pub.CreatedOn,pub.Deleted,pub.Name,pub.IdentityNumber, pub.Active,
                                        (SELECT COUNT(distinct a.ID) FROM am.App a
								        INNER JOIN ap.PublisherAppSetting ps on a.Id = ps.AppId
										WHERE a.AppSubscriptionMode=2 AND a.Deleted = 0 AND  ps.TenantId = pub.TenantId AND ps.Deleted = 0) AS ApplicationCount, 
                                        (SELECT COUNT(distinct t.Id) FROM am.TenantLinking tl 
                                        INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId 
                                        WHERE PublisherTenantId= pub.TenantId 
					                    AND tl.BusinessTenantId IS NOT NULL AND T.Deleted = 0 and t.TenantType = 3 
                                        AND (T.CreatedOn BETWEEN @FromDate AND @ToDate)) AS TenantCount, 
                                        (SELECT COUNT(distinct t.Id) FROM am.TenantLinking tl 
					                    INNER JOIN am.Tenant T ON T.ID= TL.BusinessTenantId 
					                    WHERE PublisherTenantId= pub.TenantId AND tl.BusinessTenantId is not null and T.Active= 1 
                                        AND T.Deleted = 0 AND (T.CreatedOn BETWEEN @FromDate AND @ToDate)) AS ActiveTenantCount, 
                                        (SELECT COUNT(distinct ap.ID) FROM am.TenantUser ap
					                    INNER JOIN am.UserTenantLinking tu ON ap.ID= tu.TenantUserId 
					                    AND tu.TenantId=pub.Tenantid AND (ap.CreatedOn BETWEEN @FromDate AND @ToDate)) AS UserCount, '' AS Status 
                                        FROM ap.publisher AS pub
                                        INNER JOIN am.TenantUser au ON au.ID= pub.CreatedBy
					                    AND (Pub.CreatedOn BETWEEN @FromDate AND @ToDate)";

            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            List<PlatPublisherReportDTO> appDTOs = await base.GetQueryEntityListAsync<PlatPublisherReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate });
            return appDTOs;
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPublisherNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            FormattableString query = $@"SELECT p.Name FROM ap.Publisher p 
								         INNER JOIN ap.PublisherAppSetting ps on p.TenantId = ps.TenantId
								         WHERE p.deleted = 0 AND p.Active = 1 AND ps.AppId = @AppId";
            SqlParameter appIdParam = new SqlParameter("@AppId", appId);
            List<NameDTO> publisherNameList = await GetQueryEntityListAsync<NameDTO>(query.ToString(), parameters: new object[] { appIdParam });
            return publisherNameList;
        }
        #endregion

        #endregion Get
    }
}
