/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 February 2020
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 06 February 2020
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
    /// This class implements standard database logic and operations for Purchase Orders Report entity.
    /// </summary>
    public class QPurchaseOrdersReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQPurchaseOrdersReportRepository  {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>            
        public QPurchaseOrdersReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<List<VendPurchaseOrdersReportDTO>> GetBizVendPurchaseOrdersListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT po.ID,po.StatusText,po.PostingDate,po.DeliveryDate,po.DocumentDate,po.ShippingType,
							po.ShippingTypeText,po.TotalPaymentDue,po.Remarks,po.ERPVendorKey AS VendorId,po.VendorName,po.ERPPurchaseOrderKey 
							FROM be.BAPurchaseOrder po
							WHERE po.TenantId=@TenantId  AND po.DocumentDate BETWEEN @FromDate AND @ToDate
                            ORDER BY po.CreatedOn DESC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<VendPurchaseOrdersReportDTO> vendPurchaseOrdersReportDTO = await GetQueryEntityListAsync<VendPurchaseOrdersReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return vendPurchaseOrdersReportDTO;

        }
    }
}
