/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData{

    /// <summary>
    /// This is the repository responsible for filtering data realted to Vendor ASN Report and services related to it
    /// </summary>
    public class QVendorASNReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQVendorASNReportRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public QVendorASNReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<VendorASNReportDTO>> GetBizVendorASNListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPASNKey,ShipDate AS VendorShipmentDate,TrackingNo AS SoTrackingNo FROM be.BAASN
							    WHERE TenantId = @TenantId AND (DocumentDate BETWEEN @FromDate AND @ToDate)
                                ORDER BY CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<VendorASNReportDTO> vendorASNReportDTO = await GetQueryEntityListAsync<VendorASNReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return vendorASNReportDTO;

        }
        #endregion
    }
}
