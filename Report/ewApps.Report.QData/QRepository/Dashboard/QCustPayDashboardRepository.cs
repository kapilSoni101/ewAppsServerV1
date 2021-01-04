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
    /// This class implements standard database logic and operations for Customer payment dashboard entity.
    /// </summary>
    public class QCustPayDashboardRepository:BaseRepository<BaseDTO, QReportDbContext>, IQCustPayDashboardRepository {
       
        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        /// <param name="connectionManager"></param>
        public QCustPayDashboardRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor  

        #region PartPay Dashboard Repository
        ///<inheritdoc/>
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT COUNT(CASE WHEN (i.StatusText = @OpenStatus) THEN 0 ELSE NULL END) AS TotalOpenInvoices, 
                                                 COUNT(CASE WHEN (i.StatusText = @ClosedStatus) THEN 0 ELSE NULL END) AS TotalPaidInvoices, 
                                                 COUNT(CASE WHEN (i.StatusText = @PartialStatus) THEN 0 ELSE NULL END) AS PartialPaid, 
                                                 (SELECT COUNT(DISTINCT i.ID) FROM Be.BAARInvoice i
								                 INNER JOIN be.BACustomer c on c.Id = i.CustomerId
												 INNER JOIN pay.Payment p on p.PartnerId = c.ID  
								                 WHERE c.BusinessPartnerTenantId = @CustomerId AND (p.Amount> 0)  AND i.StatusText = @ClosedStatus AND i.createdon 
                                                 BETWEEN DATEADD(dd,-7,getdate()) and getDate()+1) AS PaidLastWeek, 
                                                 isnull((SELECT SUM(p.Amount) FROM be.BAARInvoice i
								                 INNER JOIN be.BACustomer c on c.Id = i.CustomerId 
												 INNER JOIN pay.Payment p on p.PartnerId = c.ID  
								                 WHERE c.BusinessPartnerTenantId = @CustomerId
								                 and i.Createdon BETWEEN DATEADD(day,-7,getdate()) and getDate()+1),0) as AmountPaidLaskWeek 
                                                 FROM be.BAARInvoice i
								                 INNER JOIN be.BACustomer c on c.Id = i.CustomerId  
								                 WHERE c.BusinessPartnerTenantId = @CustomerId and i.Deleted = 0";
            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            SqlParameter openstatus = new SqlParameter("@OpenStatus", Constants.Open);
            SqlParameter closedstatus = new SqlParameter("@ClosedStatus", Constants.Closed);
            SqlParameter partialstatus = new SqlParameter("@PartialStatus", Constants.Partial);
            return GetQueryEntityList<BACInvoiceStatusCountDTO>(sql.ToString(), parameters: new object[] { comappId, openstatus, closedstatus, partialstatus }).FirstOrDefault<BACInvoiceStatusCountDTO>();
        }

        ///<inheritdoc/>
        public async Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string sql = "EXEC prcQGetBusinessNameAndSumOfInvoiceByCustomer @CustomerId";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<InoviceAndMonthNameDTO>(sql, parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@" SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey, inv.ERPDocNum as InvoiceNo,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, 
                            cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DueDate,
                            inv.DocumentDate, (SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = inv.ID) as TotalLineItems,
							inv.TotalPaymentDue AS OriginalAmount,
                            inv.BalanceDue AS OutStanding 
                            FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId                            
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy 
							WHERE (inv.BalanceDue > 0) and inv.Deleted = 0 and cu.BusinessPartnerTenantId = @CustomerId
                            AND inv.DueDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                            AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
							ORDER BY CreatedOn Desc";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<UpComingPaymentDTO>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingRecurringPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT DISTINCT rp.Id,rp.OrderId as InvoiceNo,rp.StartDate AS CreatedOn,
							                    sj.scheduledTime as DueDate, 
							                    rp.TermAmount AS Amount,c.Name AS CustomerName,(rp.TermAmount + rp.InvoiceTax) As OutStanding, 
							                    ISNULL(dc.DocumentCurrencyCode, 840) AS DocumentCurrencyCode, ISNULL(dc.FinalConversionRate, 1) AS FinalConversionRate,Convert(bit,0) as InvoiceFlag from RecurringPayment rp 
							                    INNER JOIN Customer c ON c.Id = rp.CustomerId
							                    INNER JOIN DocumentCurrency dc ON dc.DocumentID =rp.ID
							                    INNER JOIN ScheduledJob as sj on rp.Id=sj.SourceId
							                    Where scheduledTime BETWEEN DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0) 
                                  AND DateAdd(Month,3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) 
							                    AND rp.Deleted = 0 AND c.BusinessPartnerTenantId = @CustomerId";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<UpComingPaymentDTO>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            FormattableString sql = $@"SELECT p.ID,p.IdentityNumber as PaymentNo,p.Status,pl.Amount as CreditAmount,pl.CreatedOn,i.CustomerName,i.ERPARInvoiceKey
                                 FROM be.BAARInvoice i 
                                 INNER JOIN pay.PaymentInvoiceLinking pl on pl.InvoiceId = i.Id
                                 INNER JOIN pay.Payment p on pl.PaymentId = p.Id
                                 INNER JOIN be.BACustomer c on c.Id = i.CustomerId AND c.BusinessPartnerTenantId = @CustomerId 
                                
                                 where c.BusinessPartnerTenantId = @CustomerId and pl.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
                                 AND SysUTCDATETIME() order by CreatedOn Desc";

            SqlParameter comappId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<RecentPaymentDTO>(sql.ToString(), parameters: new object[] { comappId });
        }

        ///<inheritdoc/>
        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            //    FormattableString sql = $@"SELECT i.ID,i.ERPARInvoiceKey as InvoiceNo,i.PostingDate,i.CustomerName,i.TotalPaymentDue,i.StatusText,i.DueDate,i.CustomerId,
            //(SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = i.ID) as TotalLineItems
            //                         FROM BE.BAARInvoice i 
            // INNER JOIN be.BACustomer c ON c.Id = i.CustomerId                            
            //                         where c.BusinessPartnerTenantId = @CustomerId and i.Createdon BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0))
            //                         AND SysUTCDATETIME() order by i.CreatedOn Desc";
            FormattableString sql = $@"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey, inv.ERPDocNum as InvoiceNo,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, 
                            cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DueDate,
                            inv.DocumentDate, (SELECT COUNT(1) FROM be.BAARInvoiceItem WHERE ARInvoiceID = inv.ID) as TotalLineItems 
                            FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            cu.BusinessPartnerTenantId = @CustomerId  AND  (inv.CreatedOn BETWEEN DATEADD(Month,-3, DATEADD(dd, DATEDIFF(dd, 0, SysUTCDATETIME()), 0)) AND SysUTCDATETIME() ) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy order by CreatedOn Desc ";

            SqlParameter comcustomerId = new SqlParameter("@CustomerId", customerId);
            return await GetQueryEntityListAsync<RecentInvoicesDTO>(sql.ToString(), parameters: new object[] { comcustomerId });
        }
        #endregion

    }
}

