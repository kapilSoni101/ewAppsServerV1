// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra<amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Amit
 * Last Updated On: 26 December 2018
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice and related information (like Data Transfer Object).
    /// </summary>
    public class BAARInvoiceRepository:BaseRepository<BAARInvoice, BusinessEntityDbContext>, IBAARInvoiceRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BADeliveryRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BAARInvoiceRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Whether invoice exists.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<bool> IsInvoiceExistAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken)) {
            BAARInvoice arInv = await _context.BAARInvoice.FirstOrDefaultAsync(inv => inv.ERPARInvoiceKey == erpARInvoiceKey, token);
            return arInv != null;
        }

        /// <summary>
        /// Invoice exist for customer.
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> IsInvoiceExistsAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            IQueryable<BAARInvoice> records = _context.BAARInvoice;
            return records.Where(ar => ar.CustomerId == customerId).Count() > 0;
        }

        #endregion Public Methods

        #region Get

        /// <summary>
        /// Get invoice.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return invoice entity.</returns>
        public async Task<BAARInvoice> GetInvoiceByERPInvoiceKeyAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken)) {
            BAARInvoice arInv = await _context.BAARInvoice.FirstOrDefaultAsync(inv => inv.ERPARInvoiceKey == erpARInvoiceKey, token);
            return arInv;
        }

        ///<inheritdoc/>
        public async Task<List<BAARInvoiceDQ>> GetInvoiceByTenant(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {

            string query = string.Format(@"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, cu.CustomerName, 
                            inv.TrackingNo AS InvoiceName,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.CreatedOn, inv.TotalPaymentDue, inv.TotalPaymentDueFC, SUM(pay.Amount) AS AmountPaid, inv.Status, 
                            CASE WHEN SUM(pay.Amount) = 0 THEN 1 WHEN inv.TotalPaymentDue - SUM(pay.Amount) > 0 THEN 2 ELSE 3 END as PaymentStatus, 
                            inv.TotalPaymentDue - SUM(pay.Amount) AS OutstandingAmount, ISNULL(dc.DocumentCurrencyCode, 840) AS DocumentCurrencyCode, 
                            ISNULL(dc.FinalConversionRate, 1) AS FinalConversionRate, ISNULL(dc.AgentCurrencyCode, 840) AS AgentCurrencyCode 
                            FROM BAARInvoice inv  
                            INNER JOIN PaymentInvoiceLinking invL on inv.ID = invL.InvoiceId AND 
                            inv.TenantId=@tenatId And inv.Deleted=@deleted AND 
                            (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
                            INNER JOIN Payment pay on pay.ID = invL.PaymentId  
                            INNER join BACustomer cu on cu.ID = inv.CustomerId 
                            INNER JOIN TenantUser au ON au.ID = inv.UpdatedBy 
                            LEFT JOIN DocumentCurrency dc ON dc.DocumentID = inv.ID                             
                            GROUP BY pay.ID, inv.ID, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, cu.CustomerName, 
                            inv.TrackingNo,inv.UpdatedOn,  au.FullName, inv.CreatedOn, inv.TotalPaymentDue, inv.TotalPaymentDueFC, 
                            pay.Amount, inv.Status, inv.TotalPaymentDue,  dc.DocumentCurrencyCode, dc.FinalConversionRate, dc.AgentCurrencyCode
                            ORDER BY inv.CreatedOn DESC ");


            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter parameterS = new SqlParameter("@tenatId", tenatId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", filter.Deleted);
            List<BAARInvoiceDQ> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDQ>(query, new object[] { parameterS, fromDate, toDate, parameterDeleted });

            return invoiceDetailDTOs;
        }

        #endregion Get

    }
}
