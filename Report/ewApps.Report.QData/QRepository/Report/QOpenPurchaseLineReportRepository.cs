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

namespace ewApps.Report.QData {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Open Purchase Line Report and services related to it
    /// </summary>
    public class QOpenPurchaseLineReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQOpenPurchaseLineReportRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public QOpenPurchaseLineReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<VendOpenPurchaseLineReportDTO>> GetBizVendOpenPurchaseLineListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT poi.ERPPurchaseOrderKey,poi.ERPPurchaseOrderItemKey,poi.ItemName,poi.ItemID,po.PostingDate,po.DeliveryDate,
							po.DocumentDate,po.ERPVendorKey AS VendorId,po.VendorName,poi.Quantity AS OrderQuantity,'' AS OpenQuantity,po.ID 
							FROM be.BAPurchaseOrder po
                            INNER JOIN be.BAPurchaseOrderItem poi ON poi.PurchaseOrderId = po.ID 
							WHERE po.TenantId=@TenantId  AND po.DocumentDate BETWEEN @FromDate AND @ToDate
                            ORDER BY po.CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<VendOpenPurchaseLineReportDTO> vendOpenPurchaseLineReportDTO = await GetQueryEntityListAsync<VendOpenPurchaseLineReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return vendOpenPurchaseLineReportDTO;

        }
        #endregion
    }
}
