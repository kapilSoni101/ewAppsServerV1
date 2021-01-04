
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
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
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice and related information (like Data Transfer Object).
    /// </summary>
    public class BASalesQuotationItemRepository:BaseRepository<BASalesQuotationItem, BusinessEntityDbContext>, IBASalesQuotationItemRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BASalesQuotationItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BASalesQuotationItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <summary>
        /// Get sales quotration item list by teanntid.
        /// </summary>
        /// <param name="salesQuotationId">salesQuotationId unique id.</param>
        /// <param name="includeDeleted">return all sales quotation item with deleted items if flag is true.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListBySalesOrderIdAsync(Guid salesQuotationId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesQuotationItem.Where(so => so.SalesQuotationId == salesQuotationId).ToListAsync(token);
            }
            return await _context.BASalesQuotationItem.Where(so => so.SalesQuotationId == salesQuotationId && !so.Deleted).ToListAsync(token);
        }

        /// <summary>
        /// Get sales quotation item list by ERP sales quotation key.
        /// </summary>
        /// <param name="erpSalesQuotationKey">erpSalesQuotationKey unique key of connector.</param>
        /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListByERPSalesOrderKeyAsync(string erpSalesQuotationKey, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesQuotationItem.Where(so => so.ERPSalesQuotationKey == erpSalesQuotationKey).ToListAsync(token);
            }
            return await _context.BASalesQuotationItem.Where(so => so.ERPSalesQuotationKey == erpSalesQuotationKey && !so.Deleted).ToListAsync(token);
        }

        /// <inheritdoc/>
        public IQueryable<BusBASalesQuotationItemDTO> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId) {
            string sql = @"SELECT ERPSalesQuotationItemKey,ERPConnectorKey,ERPSalesQuotationKey,ERPItemKey,SerialOrBatchNo,ItemName,Quantity,QuantityUnit
							,UnitPrice,Unit,DiscountPercent,DiscountAmount,TaxCode,TaxPercent,TotalLC,GLAccount,BlanketAgreementNo,ShipFromAddress,ERPShipToAddressKey
							,ShipToAddress,ERPBillToAddressKey,BillToAddress,ShipFromAddressKey
							FROM be.BASalesQuotationItem AS sqi
                           Where sqi.SalesQuotationId=@SalesQuotationId";

            SqlParameter salesQuotationIdParam = new SqlParameter("@SalesQuotationId", salesQuotationId);
            return _context.BusBASalesQuotationItemDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam });
        }


    /// <inheritdoc/>
    public IQueryable<CustBASalesQuotationItemDTO> GetSalesQuotationItemListBySalesQuotationIdForCust(Guid salesQuotationId)
    {
      string sql = @"SELECT ERPSalesQuotationItemKey,ERPConnectorKey,ERPSalesQuotationKey,ERPItemKey,SerialOrBatchNo,ItemName,Quantity,QuantityUnit
							,UnitPrice,Unit,DiscountPercent,DiscountAmount,TaxCode,TaxPercent,TotalLC,GLAccount,BlanketAgreementNo,ShipFromAddress,ERPShipToAddressKey
							,ShipToAddress,ERPBillToAddressKey,BillToAddress,ShipFromAddressKey
							FROM be.BASalesQuotationItem AS sqi
                           Where sqi.SalesQuotationId=@SalesQuotationId";

      SqlParameter salesQuotationIdParam = new SqlParameter("@SalesQuotationId", salesQuotationId);
      return _context.CustBASalesQuotationItemDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam });
    }


    #endregion Get

  }
}
