/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
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
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice item and related information (like Data Transfer Object).
    /// </summary>
    public class BASalesQuotationRepository:BaseRepository<BASalesQuotation, BusinessEntityDbContext>, IBASalesQuotationRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BASalesQuotationRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CoreDbContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BASalesQuotationRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get for Business

        /// <summary>
        /// Get BASales Quotation list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return BASalesQuotation with deleted items.</param>        
        /// <returns>return list of BASalesQuotation entity.</returns>
        public async Task<List<BASalesQuotation>> GetSalesQuotationListByTenantIdAsync(Guid tenantId, ListDateFilterDTO listDateFilterDTO, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesQuotation.Where(so => so.TenantId == tenantId && so.DocumentDate >= listDateFilterDTO.FromDate && so.DocumentDate <= listDateFilterDTO.ToDate).ToListAsync(token);
            }
            return await _context.BASalesQuotation.Where(so => so.TenantId == tenantId && !so.Deleted && so.DocumentDate >= listDateFilterDTO.FromDate && so.DocumentDate <= listDateFilterDTO.ToDate).ToListAsync(token);
        }

        /// <summary>
        /// Get BASales Quotation by ERP unique key.
        /// </summary>
        /// <param name="erpSalesQuotationKey">SalesQuotationKey is a ERP unique key.</param>
        /// <param name="token"></param>
        /// <returns>return BASalesQuotation entity.</returns>
        public async Task<BASalesQuotation> GetSalesQuotationByERPKeyAsync(string erpSalesQuotationKey, CancellationToken token = default(CancellationToken)) {
            return await _context.BASalesQuotation.FirstOrDefaultAsync(sq => sq.ERPSalesQuotationKey == erpSalesQuotationKey, token);
        }

        /// <inheritdoc/>
        public IQueryable<BusBASalesQuotationDTO> GetSalesQuotationListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO) {
            string sql = @"SELECT ID, ERPSalesQuotationKey, ERPConnectorKey, ERPCustomerKey, CustomerId, CustomerName, ContactPerson, CustomerRefNo
                            , LocalCurrency, Status, StatusText, PostingDate, ValidUntil,  DocumentDate,  SalesEmployee, Owner, TotalBeforeDiscount, Discount
							, Freight, Tax, TotalPaymentDue, Remarks, ERPShipToAddressKey, ShipFromAddress, ShipFromAddressKey,							
							ShipToAddress, ERPBillToAddressKey, BillToAddress, ShippingType, ShippingTypeText, ERPDocNum
                            FROM be.BASalesQuotation
                            WHERE TenantId=@BusinessTenantId AND Deleted=0 AND DocumentDate Between @FromDate AND @ToDate";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);

            return _context.BusBASalesQuotationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam });
        }

        /// <inheritdoc/>
        public async Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken) {
            string sql = @"SELECT sq.ID, ERPSalesQuotationKey, ERPCustomerKey, CustomerID, CustomerName, ContactPerson, CustomerRefNo, 
                            LocalCurrency, PostingDate,DocumentDate, sq.CreatedOn, tuCreatedBy.FullName AS 'CreatedByName', sq.UpdatedOn, tuUpdateBy.FullName AS 'UpdatedByName', 
                            BillToAddress, Status, StatusText, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue,
                            ShipFromAddressKey, ShipFromAddress,ShippingType, ShippingTypeText, SalesEmployee, [Owner], Remarks
							,ERPBillToAddressKey,ERPConnectorKey,ERPShipToAddressKey,ShipToAddress,ValidUntil, ERPDocNum
                            FROM be.BASalesQuotation AS sq
                            INNER JOIN am.TenantUser as tuCreatedBy ON sq.CreatedBy = tuCreatedBy.ID
                            INNER JOIN am.TenantUser as tuUpdateBy ON sq.UpdatedBy = tuUpdateBy.ID
                            where sq.ID= @SalesQuotationId AND sq.Deleted= 0";

            SqlParameter salesQuotationIdParam = new SqlParameter("@SalesQuotationId", salesQuotationId);
            return await _context.BusBASalesQuotationViewDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion Get for Business

        #region Get for customer

        /// <inheritdoc/>
        public IQueryable<CustBASalesQuotationDTO> GetSalesQuotationListByPartnerTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO) {
            string sql = @"SELECT sq.ID, ERPSalesQuotationKey, sq.ERPConnectorKey, sq.ERPCustomerKey, sq.CustomerName, ContactPerson, CustomerRefNo
                      , LocalCurrency, sq.Status, sq.StatusText, PostingDate, ValidUntil,  DocumentDate,  SalesEmployee, Owner, TotalBeforeDiscount, Discount
                      , Freight, Tax, TotalPaymentDue, sq.Remarks, ERPShipToAddressKey, ShipFromAddress, ShipFromAddressKey,							
                      ShipToAddress, ERPBillToAddressKey, BillToAddress, sq.ShippingType, sq.ShippingTypeText, sq.CustomerId, sq.ERPDocNum  
                      FROM be.BASalesQuotation as sq
                      INNER JOIN be.BACustomer as c on sq.CustomerId=c.ID
                      where c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND sq.DocumentDate BETWEEN @FromDate AND @ToDate";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            return _context.CustBASalesQuotationDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam });
        }

        /// <inheritdoc/>
        public async Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken) {
            string sql = @"SELECT sq.ID, ERPSalesQuotationKey, ERPCustomerKey, CustomerID, CustomerName, ContactPerson, CustomerRefNo, 
                                LocalCurrency, PostingDate,DocumentDate, sq.CreatedOn, tuCreatedBy.FullName AS 'CreatedByName', sq.UpdatedOn, tuUpdateBy.FullName AS 'UpdatedByName', 
                                BillToAddress, Status, StatusText, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue,
                                ShipFromAddressKey, ShipFromAddress,ShippingType, ShippingTypeText, SalesEmployee, [Owner], Remarks
							    ,ERPBillToAddressKey,ERPConnectorKey,ERPShipToAddressKey,ShipToAddress,ValidUntil, ERPDocNum
                                FROM be.BASalesQuotation AS sq
                                INNER JOIN am.TenantUser as tuCreatedBy ON sq.CreatedBy = tuCreatedBy.ID
                                INNER JOIN am.TenantUser as tuUpdateBy ON sq.UpdatedBy = tuUpdateBy.ID
                                where sq.ID= @SalesQuotationId AND sq.Deleted= 0";

            SqlParameter salesQuotationIdParam = new SqlParameter("@SalesQuotationId", salesQuotationId);
            return await _context.CustBASalesQuotationViewDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        #endregion Get for Customer

    }
}
