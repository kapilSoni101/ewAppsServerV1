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
using ewApps.Payment.Common;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public class QPaymentRepository:QBaseRepository<QPaymentDBContext>, IQPaymentRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QBusinessRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QPaymentRepository(QPaymentDBContext context): base(context) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT inv.Id, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, 
                            inv.CustomerRefNo, inv.CustomerName, inv.TrackingNo, 
                            inv.TotalPaymentDue, inv.TotalPaymentDueFC, 
                            SUM(pay.Amount) AS AmountPaid, SUM(pay.AmountFC) AS AmountPaidFC, 
                            inv.Discount, inv.DiscountFC, inv.Tax, inv.TaxFC, inv.Freight, 
                            inv.FreightFC, inv.LocalCurrency, inv.StatusText, inv.Status, inv.ERPDocNum  
                            FROM BE.BAARInvoice inv 
                            INNER JOIN PAY.PaymentInvoiceLinking invL on inv.ID = invL.InvoiceId AND inv.ID=@invoiceId 
                            INNER JOIN PAY.Payment pay on pay.ID = invL.PaymentId
                            INNER JOIN BE.BACustomer cu on cu.ID = inv.CustomerId 

                            GROUP BY pay.ID, inv.ID, inv.CreatedBy, inv.CreatedOn,inv.CustomerId, inv.CustomerRefNo, inv.CustomerName, 
                            inv.TrackingNo,inv.UpdatedOn, inv.CreatedOn, inv.TotalPaymentDue, inv.TotalPaymentDueFC, 
                            pay.Amount, inv.Discount, inv.DiscountFC, inv.Tax, inv.TaxFC, inv.Freight, inv.FreightFC,
                            inv.LocalCurrency, inv.StatusText, inv.Status  ";

            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            BAARInvoiceViewDTO invoiceDetailDTOs = await GetQueryEntityAsync<BAARInvoiceViewDTO>(query, new object[] { parameterS });

            return invoiceDetailDTOs;
        }

        /// <summary>
        /// Get payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentDetailDQ> GetPaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT pay.ID,pay.Amount  AS AmountPaid, pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                        "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                        "pay.IdentityNumber AS PaymentIdentityNo, count(*) AS TotalInvoice,inv.CustomerName AS CustomerName, '' AS CustomerId,  840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                        "inv.LocalCurrency, cust.Currency AS CustomerCurrency, AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType,pay.PayeeName, pay.CustomerAccountNumber  " +
                        "FROM BE.BACustomer cust  " +
                        "INNER JOIN BE.BAARInvoice inv ON inv.CustomerId = cust.ID " +
                        "INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = inv.ID AND payLink.PaymentId = @paymentId " +
                        "INNER JOIN PAY.Payment pay ON pay.ID = payLink.PaymentId AND pay.ID = @paymentId " +
                        "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                        "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                        //"LEFT JOIN DocumentCurrency dc ON dc.DocumentID = inv.ID " +

                        "Group By pay.ID,pay.Amount, pay.ID, pay.CreatedOn, " +
                        "pay.CreatedOn , pay.Status, pay.Description , pay.IdentityNumber, pay.AmountFC, inv.CustomerName, inv.LocalCurrency, cust.Currency,AppSrvc.Name , SrvcAttr.Name, pay.PaymentTransectionCurrency, pay.PaymentType,pay.PayeeName,pay.CustomerAccountNumber  ";
            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);

            return await GetQueryEntityAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId });
        }

        /// <summary>
        /// Get advance payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentDetailDQ> GetAdvancePaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT pay.ID,pay.Amount  AS AmountPaid, pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                        "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                        "pay.IdentityNumber AS PaymentIdentityNo, 1 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId,  bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                        "'' AS LocalCurrency, cust.Currency AS CustomerCurrency, AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber  " +
                        "FROM AP.Business bus INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId " +
                        //"INNER JOIN BE.BAARInvoice inv ON inv.CustomerId = cust.ID " +
                        //"INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = inv.ID AND payLink.PaymentId = @paymentId " +
                        "INNER JOIN PAY.Payment pay ON pay.PartnerId = cust.ID AND pay.ID = @paymentId " +
                        "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                        "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  ";
            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);

            return await GetQueryEntityAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId });
        }

        /// <summary>
        /// Get payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int invoicePaymentType = (int)PaymentTypeEnum.Invoice;
            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string sql = "SELECT pay.ID,payLink.Amount  AS AmountPaid,payLink.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                        "inv.DueDate AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                        "pay.IdentityNumber AS PaymentIdentityNo, 1 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                        "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName , pay.CustomerAccountNumber  " +
                        "FROM BE.BACustomer cust  " +
                        "INNER JOIN BE.BAARInvoice inv ON inv.CustomerId = cust.ID And cust.BusinessPartnerTenantId = @customerId " +
                        "INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = inv.ID " +
                        "INNER JOIN PAY.Payment pay ON pay.ID = payLink.PaymentId AND pay.PaymentType = " + invoicePaymentType + "  AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                        "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                        "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  ";                                                

            string advanceSql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                       "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                       "pay.IdentityNumber AS PaymentIdentityNo, 0 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                       "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName , pay.CustomerAccountNumber  " +
                       "FROM AP.Business bus " +
                       "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId AND cust.BusinessPartnerTenantId = @customerId " +
                       "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                       "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                       "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +                      
                       "";

            sql = string.Format("( {0} UNION {1} ) ORDER BY PaymentDate DESC ", sql, advanceSql);

            SqlParameter paramPaymentId = new SqlParameter("@customerId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string sql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                       "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                       "pay.IdentityNumber AS PaymentIdentityNo, 0 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                       "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, PayeeName , pay.CustomerAccountNumber  " +
                       "FROM AP.Business bus " +
                       "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId AND cust.BusinessPartnerTenantId = @customerId " +
                       "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                       "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                       "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                        "ORDER BY pay.CreatedOn DESC, pay.ID ";
            SqlParameter paramPaymentId = new SqlParameter("@customerId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int invoicePaymentType = (int)PaymentTypeEnum.Invoice;
            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string sql = "SELECT DISTINCT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, count(*) AS TotalInvoice,cust.CustomerName, cust.ERPCustomerKey AS CustomerId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType,pay.PayeeName , pay.CustomerAccountNumber " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink on payLink.TenantId = @tenantId And pay.ID = payLink.PaymentId AND pay.PaymentType = " + invoicePaymentType + " AND pay.TenantId = @tenantId " +
                      " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN BE.BAARInvoice inv on inv.TenantId = @tenantId And inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BACustomer cust on cust.TenantId = @tenantId And cust.ID = inv.CustomerId " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "Group By pay.ID,pay.Amount, pay.AmountFC, pay.ID, pay.CreatedOn, " +
                      "pay.CreatedOn , pay.Status, pay.Description , pay.IdentityNumber,cust.CustomerName, cust.ERPCustomerKey ,inv.LocalCurrency, cust.Currency, " +
                      "AppSrvc.Name , SrvcAttr.Name,pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber  ";

            string advanceSql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                       "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                       "pay.IdentityNumber AS PaymentIdentityNo, 0 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                       "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, PayeeName , pay.CustomerAccountNumber  " +
                       "FROM AP.Business bus " +
                       "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId " +
                       "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND pay.TenantId = @tenantId AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                       "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                       "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                       "";

            sql = string.Format("( {0} UNION {1} ) ORDER BY PaymentDate DESC ", sql, advanceSql);

            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            //SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDateMoment);
            //SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDateMoment);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get vendor payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<VendorPaymentDetailDQ>> GetFilterVendorTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int vendorinvoicePaymentType = (int)PaymentTypeEnum.Vendor;
            string sql = "SELECT DISTINCT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, count(*) AS TotalInvoice,cust.VendorName, cust.ERPVendorKey AS VendorId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType,pay.PayeeName , pay.CustomerAccountNumber " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink on payLink.TenantId = @tenantId And pay.ID = payLink.PaymentId AND pay.PaymentType = " + vendorinvoicePaymentType + " AND pay.TenantId = @tenantId " +
                      " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN BE.BAAPInvoice inv on inv.TenantId = @tenantId And inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BAVendor cust on cust.TenantId = @tenantId And cust.ID = inv.CustomerId " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "Group By pay.ID,pay.Amount, pay.AmountFC, pay.ID, pay.CreatedOn, " +
                      "pay.CreatedOn , pay.Status, pay.Description , pay.IdentityNumber,cust.CustomerName, cust.ERPCustomerKey ,inv.LocalCurrency, cust.Currency, " +
                      "AppSrvc.Name , SrvcAttr.Name,pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber  ORDER BY PaymentDate DESC ";


            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            //SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDateMoment);
            //SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDateMoment);

            return await GetQueryEntityListAsync<VendorPaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string sql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                       "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                       "pay.IdentityNumber AS PaymentIdentityNo, 1 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                       "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, PayeeName , pay.CustomerAccountNumber  " +
                       "FROM AP.Business bus " +
                       "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId " +
                       "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND pay.TenantId = @tenantId AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                       "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                       "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +                       
                       "ORDER BY pay.CreatedOn DESC, pay.ID ";
            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetVoidFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int invoicePaymentType = (int)PaymentTypeEnum.Invoice;
            string statusPending = Payment.Common.Constants.PaymentStatusPending;
            string sql = "SELECT DISTINCT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, count(*) AS TotalInvoice,cust.CustomerName, cust.ERPCustomerKey AS CustomerId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber  " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink on payLink.TenantId = @tenantId And pay.ID = payLink.PaymentId AND pay.Status = '" + statusPending + "' AND pay.PaymentType = " + invoicePaymentType + " And pay.TenantId = @tenantId " +
                      " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN BE.BAARInvoice inv on inv.TenantId = @tenantId  And inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BACustomer cust on cust.TenantId = @tenantId And cust.ID = inv.CustomerId " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "Group By pay.ID,pay.Amount, pay.AmountFC, pay.ID, pay.CreatedOn, " +
                      "pay.CreatedOn , pay.Status, pay.Description , pay.IdentityNumber,cust.CustomerName, cust.ERPCustomerKey ,inv.LocalCurrency, cust.Currency, " +
                      "AppSrvc.Name , SrvcAttr.Name, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber ";

            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string advanceSql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, 0 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, PayeeName , pay.CustomerAccountNumber  " +
                      "FROM AP.Business bus " +
                      "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId " +
                      "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND pay.Status = '" + statusPending + "' AND pay.TenantId = @tenantId AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "";

            sql = string.Format("( {0} UNION {1} ) ORDER BY PaymentDate DESC ", sql, advanceSql);

            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }


        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            int invoicePaymentType = (int)PaymentTypeEnum.Invoice;
            //string statusSettled = Payment.Common.Constants.PaymentStatusSettled;
            //string partialSettled = Payment.Common.Constants.PaymentStatusPartialSettled;
            string statusSettled = string.Format("'{0}', '{1}' ", Common.Constants.PaymentStatusSettled, Common.Constants.PaymentStatusPartialSettled);
            string sql = "SELECT DISTINCT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, count(*) AS TotalInvoice,cust.CustomerName, cust.ERPCustomerKey AS CustomerId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber  " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.TenantId = @tenantId And pay.ID = payLink.PaymentId  AND pay.Status IN ("+ statusSettled + ") AND pay.PaymentType = " + invoicePaymentType + " And pay.TenantId = @tenantId " +
                      " AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN BE.BAARInvoice inv on inv.TenantId = @tenantId  And inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BACustomer cust on cust.TenantId = @tenantId And cust.ID = inv.CustomerId " +
                        "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "Group By pay.ID,pay.Amount, pay.AmountFC, pay.ID, pay.CreatedOn, " +
                      "pay.CreatedOn , pay.Status, pay.Description , pay.IdentityNumber,cust.CustomerName, cust.ERPCustomerKey ,inv.LocalCurrency, cust.Currency, " +
                      "AppSrvc.Name , SrvcAttr.Name, pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName, pay.CustomerAccountNumber ";

            int advancePaymentType = (int)PaymentTypeEnum.Advance;
            string advanceSql = "SELECT pay.ID,pay.Amount  AS AmountPaid,pay.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, 0 AS TotalInvoice,cust.CustomerName AS CustomerName, cust.ERPCustomerKey AS CustomerId, bus.CurrencyCode AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "'' AS LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency, pay.PaymentType, PayeeName , pay.CustomerAccountNumber  " +
                      "FROM AP.Business bus " +
                      "INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId " +
                      "INNER JOIN PAY.Payment pay ON pay.PartnerID = cust.Id AND pay.PaymentType = " + advancePaymentType + " AND pay.Status IN (" + statusSettled + ") AND pay.TenantId = @tenantId AND (pay.CreatedOn BETWEEN @FromDate AND @ToDate) " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "";

            sql = string.Format("( {0} UNION {1} ) ORDER BY PaymentDate DESC ", sql, advanceSql);

            SqlParameter paramPaymentId = new SqlParameter("@tenantId", filter.ID);
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paramPaymentId, fromDate, toDate });
        }

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {            
            string sql = "SELECT DISTINCT pay.ID,payLink.Amount  AS AmountPaid,payLink.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, 1 AS TotalInvoice,cust.CustomerName, cust.ERPCustomerKey AS CustomerId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName , pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName , pay.CustomerAccountNumber  " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = @invoiceId And pay.ID = payLink.PaymentId  " +                      
                      "INNER JOIN BE.BAARInvoice inv on inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BACustomer cust on cust.ID = inv.CustomerId " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +                     
                      "ORDER BY pay.CreatedOn DESC ";
            SqlParameter paraminvoiceId = new SqlParameter("@invoiceId", invoiceId);

            return await GetQueryEntityListAsync<PaymentDetailDQ>(sql, new object[] { paraminvoiceId });
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT pay.ID, pay.Amount as AmountPaid, pay.AmountFC, inv.TotalPaymentDue, inv.TotalPaymentDueFC,
                            pay.CreatedBy, pay.CreatedOn, pay.OriginationDate, pay.Description as Note, 
                            pay.IdentityNumber,  pay.PartnerId, pay.Type as PaymentType, pay.CustomerAccountNumber,pay.Status,  
                            pay.BusinessId, pay.CheckImageBack, pay.CheckImageFront, pay.CheckNumber, cust.CustomerName, 
                            pay.CustomerName AS BankAccountName, cust.ERPCustomerKey AS CustomerRefId, payLink.InvoiceId, pay.TenantId,   
                            inv.ShipToAddress, inv.BillToAddress, inv.ShipFromAddress, pay.PayeeName AS PayeeName,
                            ISNULL(tu.IdentityNumber, '') AS PayeeId, inv.LocalCurrency, cust.Currency AS CustomerCurrency,
                            AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.AuthCode, pay.TransactionId  
                            FROM PAY.Payment pay
                            INNER JOIN BE.BACustomer cust ON pay.PartnerId = cust.ID AND pay.ID = @paymentId
                            INNER JOIN PAY.PaymentInvoiceLinking payLink ON pay.ID = payLink.PaymentId AND pay.ID = @paymentId
                            INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  
                            INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  
                            AND payLink.InvoiceId = @invoiceId
                            INNER JOIN BE.BAARInvoice inv ON payLink.InvoiceId = inv.ID And inv.ID = @invoiceId
                            LEFT JOIN AM.TenantUser tu ON tu.Id = pay.CreatedBy";

            SqlParameter paramPaymentId = new SqlParameter("@paymentId", id);
            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            return await GetQueryEntityAsync<PaymentDetailModelDTO>(sql, new object[] { paramPaymentId, parameterS });
        }


        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<List<PaymentDetailModelDTO>> GetPaymentDTOListAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT pay.ID, payLink.Amount as AmountPaid, payLink.AmountFC as AmountPaidFC, inv.TotalPaymentDue, inv.TotalPaymentDueFC,
                            pay.CreatedBy, pay.CreatedOn, pay.OriginationDate, pay.Description as Note, 
                            pay.IdentityNumber,  pay.PartnerId, pay.Type as PaymentType, pay.CustomerAccountNumber,pay.Status,  
                            pay.BusinessId, pay.CheckImageBack, pay.CheckImageFront, pay.CheckNumber, cust.CustomerName, 
                            pay.CustomerName AS BankAccountName, cust.ERPCustomerKey AS CustomerRefId, payLink.InvoiceId, pay.TenantId,   
                            inv.ShipToAddress, inv.BillToAddress, inv.ShipFromAddress, pay.PayeeName AS PayeeName,
                            ISNULL(tu.IdentityNumber, '') AS PayeeId, inv.LocalCurrency, cust.Currency AS CustomerCurrency,
                            AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.AuthCode, pay.TransactionId 
                            FROM PAY.Payment pay
                            INNER JOIN BE.BACustomer cust ON pay.PartnerId = cust.ID AND pay.ID = @paymentId
                            INNER JOIN PAY.PaymentInvoiceLinking payLink ON pay.ID = payLink.PaymentId AND pay.ID = @paymentId                            
                            INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  
                            INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  
                            INNER JOIN BE.BAARInvoice inv ON payLink.InvoiceId = inv.ID 
                            LEFT JOIN AM.TenantUser tu ON tu.Id = pay.CreatedBy";

            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);
            return await GetQueryEntityListAsync<PaymentDetailModelDTO>(sql, new object[] { paramPaymentId });
        }

        /// <summary>
        /// Gets Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentInfoDTO> GetPaymentInfoDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT pay.ID, pay.Amount as AmountPaid, pay.AmountFC as AmountPaidFC, 
                            pay.CreatedBy, pay.CreatedOn, pay.OriginationDate, pay.Description, 
                            pay.IdentityNumber, pay.PaymentType, pay.CustomerAccountNumber,pay.Status,                              
                            pay.CustomerName AS BankAccountName, 
                            pay.PayeeName AS PayeeName,
                            AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency 
                            FROM  PAY.Payment pay 
                            INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId AND pay.ID = @paymentId 
                            INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId                              
                            ";

            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);
            return await GetQueryEntityAsync<PaymentInfoDTO>(sql, new object[] { paramPaymentId });
        }

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT pay.ID, pay.Amount as AmountPaid, pay.AmountFC as AmountPaidFC, 0 AS TotalPaymentDue, 
                            pay.CreatedBy, pay.CreatedOn, pay.OriginationDate, pay.Description as Note, 
                            pay.IdentityNumber,  pay.PartnerId, pay.Type as PaymentType, pay.CustomerAccountNumber,pay.Status,  
                            pay.BusinessId, pay.CheckImageBack, pay.CheckImageFront, pay.CheckNumber, cust.CustomerName, 
                            pay.CustomerName AS BankAccountName, cust.ERPCustomerKey AS CustomerRefId, pay.TenantId,   
                            '' AS ShipToAddress, '' AS BillToAddress, '' AS ShipFromAddress, pay.PayeeName AS PayeeName,
                            ISNULL(tu.IdentityNumber, '') AS PayeeId, bus.CurrencyCode, '' AS LocalCurrency, cust.Currency AS CustomerCurrency,
                            AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName, pay.PaymentTransectionCurrency,  pay.AuthCode, pay.TransactionId  
                            FROM  AP.Business bus 
                            INNER JOIN BE.BACustomer cust ON cust.TenantId = bus.TenantId 
                            INNER JOIN  PAY.Payment pay ON pay.PartnerId = cust.ID AND pay.ID = @paymentId                                                        
                            INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  
                            INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId                              
                            LEFT JOIN AM.TenantUser tu ON tu.Id = pay.CreatedBy";

            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);
            return await GetQueryEntityAsync<PaymentAdvanceDetailModelDTO>(sql, new object[] { paramPaymentId });
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>        
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT inv.TrackingNo, inv.ERPARInvoiceKey, inv.ERPDocNum, inv.DueDate,inv.BalanceDue,inv.BalanceDueFC, 
                            inv.TotalPaymentDue,inv.TotalPaymentDueFC, inv.TotalBeforeDiscount, inv.TotalBeforeDiscountFC,
                            inv.AppliedAmount AS AmountPaid, inv.AppliedAmountFC AS AmountPaidFC,
                            payLink.Amount AS ReceiptPaidAmount, payLink.AmountFC AS ReceiptPaidAmountFC  FROM 
                            BE.BAARInvoice inv 
                            INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = inv.ID AND payLink.PaymentId = @paymentId ";
            SqlParameter paramPaymentId = new SqlParameter("@paymentId", paymentId);

            return await GetQueryEntityListAsync<InvoiceInfoDTO>(sql, new object[] { paramPaymentId }, token);
        }

        #endregion Get

        #region Vendor

        /// <summary>
        /// Get vendor payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<VendorPaymentDetailDQ>> GetVendorPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            string sql = "SELECT DISTINCT pay.ID,payLink.Amount  AS AmountPaid,payLink.AmountFC  AS AmountPaidFC, pay.ID  as PaymentId, pay.CreatedOn AS PaymentDate, " +
                      "pay.CreatedOn AS InvoiceDueDate, 0.0 AS OutstandingAmount, 0.0 AS TotalAmount, pay.Status, pay.Description AS Note, " +
                      "pay.IdentityNumber AS PaymentIdentityNo, 1 AS TotalInvoice,cust.VendorName, cust.ERPVendorKey AS VendorId, 840 AS DocumentCurrencyCode, 1.0 AS FinalConversionRate, " +
                      "inv.LocalCurrency, cust.Currency as CustomerCurrency,AppSrvc.Name AS PayServiceName, SrvcAttr.Name AS PayServiceAttributeName , pay.PaymentTransectionCurrency, pay.PaymentType, pay.PayeeName , pay.CustomerAccountNumber  " +
                      "FROM PAY.Payment pay " +
                      "INNER JOIN PAY.PaymentInvoiceLinking payLink ON payLink.InvoiceId = @invoiceId And pay.ID = payLink.PaymentId  " +
                      "INNER JOIN BE.BAAPInvoice inv on inv.ID = payLink.InvoiceId " +
                      "INNER JOIN BE.BAVendor cust on cust.ID = inv.VendorId " +
                      "INNER JOIN AM.AppService AppSrvc ON AppSrvc.ID = pay.AppServiceId  " +
                      "INNER JOIN AM.AppServiceAttribute SrvcAttr ON SrvcAttr.ID = pay.AppServiceAttributeId  " +
                      "ORDER BY pay.CreatedOn DESC ";
            SqlParameter paraminvoiceId = new SqlParameter("@invoiceId", invoiceId);

            return await GetQueryEntityListAsync<VendorPaymentDetailDQ>(sql, new object[] { paraminvoiceId });
        }

        #endregion Vendor

    }
}
