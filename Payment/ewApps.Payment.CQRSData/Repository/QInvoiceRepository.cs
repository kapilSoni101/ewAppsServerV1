/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 29 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public class QInvoiceRepository:QBaseRepository<QPaymentDBContext>, IQInvoiceRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QInvoiceRepository(QPaymentDBContext context): base(context) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, 
                            inv.CustomerRefNo, inv.CustomerName, inv.TrackingNo, 
                            inv.TotalBeforeDiscount, inv.TotalBeforeDiscountFC,inv.TotalPaymentDue, inv.TotalPaymentDueFC,
							inv.BalanceDue, inv.BalanceDueFC,
                            inv.Discount, inv.DiscountFC, inv.Tax, inv.TaxFC, inv.Freight, 
                            inv.FreightFC, inv.StatusText, inv.Status, 
							inv.ERPCustomerKey, inv.ERPARInvoiceKey, inv.Remarks,
							inv.LocalCurrency, cu.Currency AS CustomerCurrency , 
                            inv.PostingDate, inv.DueDate, inv.DocumentDate,inv.UpdatedOn,
							inv.AppliedAmount AS AmountPaid, inv.AppliedAmountFC AS AmountPaidFC,
                            inv.BillToAddress, inv.ShipFromAddress, inv.ShipToAddress,inv.ContactPerson  AS ContactName,
                            inv.ShippingTypeText, inv.SalesEmployee, inv.Owner,
                            tuCreatedtBy.FullName AS CreatedByName, tuUpdatedBy.FullName AS UpdatedByName, inv.TenantId, inv.ERPDocNum  
                            FROM BE.BAARInvoice inv                             
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND inv.ID = @invoiceId
                            INNER JOIN AM.TenantUser tuCreatedtBy on tuCreatedtBy.ID = inv.CreatedBy
                            INNER JOIN AM.TenantUser tuUpdatedBy on tuUpdatedBy.ID = inv.UpdatedBy";

            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            BAARInvoiceViewDTO invoiceDetailDTOs = await GetQueryEntityAsync<BAARInvoiceViewDTO>(query, new object[] { parameterS });

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT * FROM BE.BAARInvoice WHERE ID = @invoiceId ";

            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            BAARInvoiceEntityDTO invoiceDetailDTOs = await GetQueryEntityAsync<BAARInvoiceEntityDTO>(query, new object[] { parameterS });

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {

            //string query = string.Format(@"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, cu.CustomerRefId, cu.Name as CustomerName, inv.InvoiceName,inv.UpdatedOn, inv.InvoiceNo,au.FullName AS UpdatedByFullName, 
            //         inv.DueDate, inv.Amount, inv.AmountPaid, inv.Status, CASE WHEN inv.AmountPaid = 0 THEN 1 WHEN inv.Amount - inv.AmountPaid > 0 THEN 2 ELSE 3 END as PaymentStatus, inv.Amount - inv.AmountPaid AS OutstandingAmount, 
            //ISNULL(dc.DocumentCurrencyCode, 840) AS DocumentCurrencyCode, ISNULL(dc.FinalConversionRate, 1) AS FinalConversionRate, ISNULL(dc.AgentCurrencyCode, 840) AS AgentCurrencyCode
            //FROM Invoice inv  inner join Customer cu on cu.ID = inv.CustomerId 
            //INNER JOIN TenantUser au ON au.ID = inv.UpdatedBy 
            //LEFT JOIN DocumentCurrency dc ON dc.DocumentID = inv.ID 
            //WHERE inv.TenantId=@tenatId And inv.Deleted=@deleted And  (inv.CreatedOn BETWEEN @FromDate AND @ToDate) ORDER BY inv.CreatedOn DESC ");
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            inv.TenantId=@tenatId AND inv.Deleted=@deleted AND  (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy ";


            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter parameterS = new SqlParameter("@tenatId", tenatId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", filter.Deleted);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { parameterS, fromDate, toDate, parameterDeleted },token);

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get invoice list by invoiceIds.
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            inv.ID IN (@invoiceId) AND inv.Deleted=@deleted 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy WHERE inv.ID IN (@invoiceId) ";

            string invIds = string.Join("','", invoiceId);
            SqlParameter parameterS = new SqlParameter("@invoiceId", invIds);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", false);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { parameterS, parameterDeleted }, token);

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            string cancelText = ewApps.Core.CommonService.Constants.CanceledInvoiceText;
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            inv.TenantId=@tenatId AND inv.Deleted=@deleted AND inv.StatusText =@cancelText AND  (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy ";


            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter parameterS = new SqlParameter("@tenatId", tenatId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", filter.Deleted);
            SqlParameter parameterCancelText = new SqlParameter("@cancelText", cancelText);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { parameterS, fromDate, toDate, parameterDeleted, parameterCancelText }, token);

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            //string query = string.Format(@"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, cu.CustomerRefId, cu.Name as CustomerName, inv.InvoiceName, inv.InvoiceNo, 
            //         inv.DueDate,inv.UpdatedOn,au.FullName AS UpdatedByFullName, inv.Amount, inv.AmountPaid, inv.Status, 
            //          CASE WHEN inv.AmountPaid = 0 THEN 1 WHEN inv.Amount - inv.AmountPaid > 0 THEN 2 ELSE 3 END as PaymentStatus, inv.Amount - inv.AmountPaid AS OutstandingAmount,
            //         ISNULL(dc.DocumentCurrencyCode, 840) AS DocumentCurrencyCode, ISNULL(dc.FinalConversionRate, 1) AS FinalConversionRate, ISNULL(dc.AgentCurrencyCode, 840) AS AgentCurrencyCode
            //FROM Invoice inv  
            //inner join Customer cu on cu.ID = inv.CustomerId And cu.BusinessPartnerTenantId=@customerId And inv.Deleted=@deleted And (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
            //INNER JOIN TenantUser au ON au.ID = inv.UpdatedBy 
            //LEFT JOIN DocumentCurrency dc ON dc.DocumentID = inv.ID   ORDER BY inv.CreatedOn DESC
            // "); 
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND 
                            cu.BusinessPartnerTenantId=@customerId AND inv.Deleted=@deleted AND  (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy ";

            SqlParameter parameterS = new SqlParameter("@customerId", filter.ID);
            SqlParameter parameterdeleted = new SqlParameter("@deleted", filter.Deleted);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { parameterS, parameterdeleted, fromDate, toDate });

            return invoiceDetailDTOs;

        }


        ///<inheritdoc/>
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {           
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate  FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND  
                            inv.TotalPaymentDue > 0 AND 
                            inv.TenantId=@tenatId AND inv.StatusText =@status AND inv.Deleted=@deleted AND  (inv.CreatedOn BETWEEN @FromDate AND @ToDate) 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy ";

            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter parameterS = new SqlParameter("@tenatId", tenatId);
            SqlParameter parameterDeleted = new SqlParameter("@deleted", filter.Deleted);
            SqlParameter parameterStatus = new SqlParameter("@status", ewApps.Payment.Common.Constants.OpenStatus);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { parameterS, fromDate, toDate, parameterDeleted, parameterStatus }, token);

            return invoiceDetailDTOs;

        }

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.PostingDate, cu.CustomerName as CustomerName, cu.ERPCustomerKey,
                            inv.ERPARInvoiceKey,inv.UpdatedOn, inv.TrackingNo,au.FullName AS UpdatedByFullName, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, inv.TotalPaymentDue - inv.BalanceDue AS AmountPaid, inv.TotalPaymentDueFC - inv.BalanceDueFC AS AmountPaidFC,
                            inv.Status, inv.StatusText, inv.BalanceDue, inv.BalanceDueFC, inv.LocalCurrency, cu.Currency AS CustomerCurrency, inv.Remarks, inv.ShippingTypeText, inv.DocumentDate, inv.ERPDocNum 
                            ,inv.DueDate FROM BE.BAARInvoice inv 
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId AND cu.BusinessPartnerTenantId = @customerId AND  
                            inv.BalanceDue > 0  AND inv.StatusText =@status AND inv.Deleted=0 
                            INNER JOIN AM.TenantUser au ON au.ID = inv.UpdatedBy ";
            

            SqlParameter paramCustomerId = new SqlParameter("@customerId", customerId);
            SqlParameter parameterStatus = new SqlParameter("@status", ewApps.Payment.Common.Constants.OpenStatus);
            List<BAARInvoiceDTO> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceDTO>(query, new object[] { paramCustomerId, parameterStatus }, token);

            return invoiceDetailDTOs;

        }

        public async Task<TenantInfo> GetBusinessInfoByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT bus.Name, bus.Id, bus.IntegratedMode, t.SubdomainName as Subdomain  FROM AP.Business bus INNER JOIN AM.Tenant t ON t.ID = bus.TenantId AND bus.TenantId = @tenantId  WHERE bus.TenantId = @tenantId ";

            SqlParameter paramtenantId = new SqlParameter("@tenantId", tenantId);
            TenantInfo tenantInfo = await GetQueryEntityAsync<TenantInfo>(query, new object[] { paramtenantId }, token);

            return tenantInfo;
        }

        /// <summary>
        /// Get primary user of a customer.
        /// </summary>
        /// <param name="businessPartnerTenantId">Business Partner TenantId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfCustomerAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT * from AM.UserTenantLinking WHERE IsPrimary = 1 AND BusinessPartnerTenantId = @tenantId ";

            SqlParameter paramtenantId = new SqlParameter("@tenantId", businessPartnerTenantId);
            UserTenantLinkingEntityDTO tenantInfo = await GetQueryEntityAsync<UserTenantLinkingEntityDTO>(query, new object[] { paramtenantId }, token);

            return tenantInfo;
        }

        #endregion Get

    }
}
