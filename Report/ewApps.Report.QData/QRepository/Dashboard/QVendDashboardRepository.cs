/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 4 February 2020
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 4 February 2020
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData{

    /// <summary>
    /// This is the repository responsible for filtering data realted to Vendor Dashboard and services related to it
    /// </summary>   
        public class QVendDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQVendDashboardRepository {

            #region Constructor 

            /// <summary>
            ///  Constructor initializing the base variables
            /// </summary>
            /// <param name="context"></param>
            /// <param name="sessionManager"></param>
            /// <param name="connectionManager"></param>
            public QVendDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
            }

            #endregion Constructor  


            ///<inheritdoc/>
            public async Task<VendApInvoiceStatusDTO> GetVendInvoicesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT COUNT(CASE WHEN (StatusText = @OpenStatus)   THEN 0 ELSE NULL END) AS TotalOpenInvoices, 
                                 COUNT(CASE WHEN (StatusText = @ClosedStatus) THEN 0 ELSE NULL END) AS ClosedInvoices,
                                 (SELECT SUM(TotalPaymentDue) FROM be.BAAPInvoice WHERE (StatusText = @OpenStatus)) AS TotalOpenInvoicesAmount,
                                 (SELECT SUM(TotalPaymentDue) FROM be.BAAPInvoice WHERE (StatusText = @ClosedStatus)) AS TotalClosedInvoicesAmount
                                 FROM be.BAAPInvoice WHERE TenantId = @TenantId AND Deleted = 0";
                SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
                SqlParameter openstatus = new SqlParameter("@OpenStatus", Constants.Open);
                SqlParameter closedstatus = new SqlParameter("@ClosedStatus", Constants.Closed);

            return GetQueryEntityList<VendApInvoiceStatusDTO>(sql.ToString(), parameters: new object[] { tenantIdParam, openstatus, closedstatus }).FirstOrDefault<VendApInvoiceStatusDTO>();
            }

            ///<inheritdoc/>
            public async Task<VendOrdersLineStatusDTO> GetVendOrderLineStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT COUNT(DISTINCT po.ID) AS OpenOrders,
												(SELECT COUNT(DISTINCT POI.ID) AS Orders FROM be.BAPurchaseOrder bpo
												INNER JOIN 
												be.BAPurchaseOrderItem poi ON poi.PurchaseOrderId = bpo.ID) AS OpenLines
												FROM be.BAPurchaseOrder po												
												Where po.Deleted = 0 AND po.TenantId = @TenantId AND po.StatusText = @OpenStatus";
                SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);

                return GetQueryEntityList<VendOrdersLineStatusDTO>(sql.ToString(), parameters: new object[] { tenantIdParam }).FirstOrDefault<VendOrdersLineStatusDTO>();
            }

            ///<inheritdoc/>
            public async Task<VendOrderStatusDTO> GetVendOrderStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT COUNT(DISTINCT ID) AS Orders,
												SUM(po.TotalPaymentDue) AS OrdersAmount
												FROM be.BAPurchaseOrder po
												Where po.DocumentDate BETWEEN DATEADD(Month,-1, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                                AND SysUTCDATETIME()
							                    AND po.Deleted = 0 AND po.TenantId = @TenantId AND po.StatusText = @OpenStatus";
                SqlParameter tenantIdParam = new SqlParameter("@TenantId", tenantId);
            SqlParameter openstatus = new SqlParameter("@OpenStatus", Constants.Open);

            return GetQueryEntityList<VendOrderStatusDTO>(sql.ToString(), parameters: new object[] { tenantIdParam, openstatus }).FirstOrDefault<VendOrderStatusDTO>();
            }



            ///<inheritdoc/>
            public async Task<List<VendOpenLinesListDTO>> GetAllVendOpenLineListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT po.ID,po.ERPPurchaseOrderKey,po.VendorName,po.DocumentDate,poi.ItemName,poi.Quantity FROM be.BAPurchaseOrder po
								 INNER JOIN be.BAPurchaseOrderItem poi ON poi.PurchaseOrderId = po.ID
								 Where po.Deleted = 0 AND po.TenantId = @TenantId and po.Createdon 
								 BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() order by po.CreatedOn Desc";

                SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
                return await GetQueryEntityListAsync<VendOpenLinesListDTO>(sql.ToString(), parameters: new object[] { comtenantId });
            }

            ///<inheritdoc/>
            public async Task<List<VendRecentAPInvoices>> GetAllVendRecentAPInvoicesListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT ID,ERPApInvoiceKey,Status,VendorName,PostingDate,TotalPaymentDue FROM be.BAAPInvoice po 
                                    Where po.Deleted = 0 AND po.TenantId = @TenantId and po.Createdon 
                                    BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                    AND SysUTCDATETIME() order by CreatedOn Desc";

                SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
                return await GetQueryEntityListAsync<VendRecentAPInvoices>(sql.ToString(), parameters: new object[] { comtenantId });
            }

            ///<inheritdoc/>
            public async Task<List<VendRecentPurchaseOrder>> GetAllVendRecentPurchaseOrderListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
                FormattableString sql = $@"SELECT ID,ERPPurchaseOrderKey,Status,VendorName,PostingDate,DocumentDate,TotalPaymentDue FROM be.BAPurchaseOrder po 
                                    Where po.Deleted = 0 AND po.TenantId = @TenantId and po.Createdon 
                                    BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                    AND SysUTCDATETIME() order by CreatedOn Desc";

                SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
                return await GetQueryEntityListAsync<VendRecentPurchaseOrder>(sql.ToString(), parameters: new object[] { comtenantId });
            }
        }
}
