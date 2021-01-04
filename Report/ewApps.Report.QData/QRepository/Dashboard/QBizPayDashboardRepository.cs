/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
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

namespace ewApps.Report.QData {

    /// <summary>
    /// This class implements standard database logic and operations for business payment dashboard entity.
    /// </summary>
    public class QBizPayDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQBizPayDashboardRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public QBizPayDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor  

        #region BizPay Dashboard Repository
        ///<inheritdoc/>
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(CASE WHEN (StatusText = @OpenStatus)   THEN 0 ELSE NULL END) AS TotalOpenInvoices, 
                                 COUNT(CASE WHEN (StatusText = @ClosedStatus) THEN 0 ELSE NULL END) AS TotalPaidInvoices,
                                 COUNT(CASE WHEN (StatusText = @OpenStatus AND AppliedAmount  != TotalPaymentDue) THEN 0 ELSE NULL END) AS PartialPaid,
                                 (SELECT COUNT(DISTINCT pil.InvoiceId) FROM be.BAARInvoice bi
								 INNER JOIN pay.PaymentInvoiceLinking pil on pil.InvoiceId = bi.ID and pil.TenantId = bi.TenantId
                                 WHERE bi.TenantId = @TenantId AND (pil.Amount > 0)  AND bi.StatusText = @ClosedStatus AND pil.Createdon BETWEEN DATEADD(dd, -7, getdate()+1) and getDate()+1) AS PaidLastWeek,
                                 ISNull((SELECT SUM(pil.Amount) FROM be.BAARInvoice bi
								 INNER JOIN pay.PaymentInvoiceLinking pil on pil.InvoiceId = bi.ID and pil.TenantId = bi.TenantId
								 WHERE bi.TenantId = @TenantId and pil.Createdon 
                                 BETWEEN DATEADD(day, -7, getdate()) and getDate() + 1),0) as AmountPaidLaskWeek
                                 FROM be.BAARInvoice WHERE TenantId = @TenantId AND Deleted = 0";
            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter openstatus = new SqlParameter("@OpenStatus", Constants.Open);
            SqlParameter closedstatus = new SqlParameter("@ClosedStatus", Constants.Closed);
            SqlParameter partialstatus = new SqlParameter("@PartialStatus", Constants.Partial);
            return GetQueryEntityList<BACInvoiceStatusCountDTO>(sql.ToString(), parameters: new object[] { comtenantId, openstatus, closedstatus, partialstatus }).FirstOrDefault<BACInvoiceStatusCountDTO>();
        }

        ///<inheritdoc/>
        public async Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prcQGetMonthNameAndSumOfInvoiceByTenant @TenantId";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<InoviceAndMonthNameDTO>(sql, parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@" SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey, inv.ERPDocNum as InvoiceNo,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, 
                            cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DueDate,
                            inv.DocumentDate, (SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = inv.ID AND TenantId = @TenantId) as TotalLineItems,
							inv.TotalPaymentDue AS OriginalAmount,
                            inv.BalanceDue AS OutStanding 
                            FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId                            
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy 
							WHERE inv.Deleted = 0 AND (inv.BalanceDue > 0) AND  inv.TenantId = @TenantId 
                            AND inv.DueDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                            AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
							ORDER BY CreatedOn Desc";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<UpComingPaymentDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingRecurringPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT DISTINCT rp.Id,rp.OrderId as InvoiceNo,rp.StartDate AS PostingDate,
							                    sj.scheduledTime as DueDate, 
							                    rp.TermAmount AS OriginalAmount,c.CustomerName,(rp.TermAmount + rp.InvoiceTax) As OutStanding
							                    from pay.RecurringPayment rp 
							                    INNER JOIN be.BACustomer c ON c.Id = rp.CustomerId							                    
							                    INNER JOIN core.ScheduledJob as sj on rp.Id=sj.SourceId
							                    Where scheduledTime BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                                                AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) 
							                    AND rp.Deleted = 0 AND rp.TenantId = @TenantId";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<UpComingPaymentDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT p.ID,p.IdentityNumber as PaymentNo,p.Status,pl.Amount as CreditAmount,pl.CreatedOn,i.CustomerName,i.ErparInvoiceKey
                                 FROM BE.BAARInvoice i 
                                 INNER JOIN pay.PaymentInvoiceLinking pl on pl.InvoiceId = i.Id and pl.TenantId = @TenantId AND i.TenantId = @TenantId 
                                 INNER JOIN pay.Payment p on pl.PaymentId = p.Id and p.TenantId = @TenantId                                                                  
                                 where i.TenantId = @TenantId and pl.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() order by CreatedOn Desc";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentPaymentDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            //FormattableString sql = $@"SELECT i.ID,i.ERPARInvoiceKey as InvoiceNo,i.PostingDate,i.CustomerName,i.TotalPaymentDue,i.StatusText,i.DueDate,i.CustomerId,
            //                     (SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = i.ID AND TenantId = @TenantId) as TotalLineItems
            //                     FROM BE.BAARInvoice i                            
            //                     where i.TenantId = @TenantId and i.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
            //                     AND SysUTCDATETIME() order by CreatedOn Desc";
            FormattableString sql = $@"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey, inv.ERPDocNum as InvoiceNo,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, 
                            cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DueDate,
                            inv.DocumentDate, (SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = inv.ID AND TenantId = @TenantId) as TotalLineItems 
                            FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            inv.TenantId = @TenantId AND  (inv.CreatedOn BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) AND SysUTCDATETIME() ) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy order by CreatedOn Desc ";

            SqlParameter comtenantId = new SqlParameter("@TenantId", tenantId);
            return await GetQueryEntityListAsync<RecentInvoicesDTO>(sql.ToString(), parameters: new object[] { comtenantId });
        }


        #endregion
    }
}
