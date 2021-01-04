
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
    public class BASalesOrderItemRepository:BaseRepository<BASalesOrderItem, BusinessEntityDbContext>, IBASalesOrderItemRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BAARInvoiceRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BASalesOrderItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="salesOrderId">salesOrderId unique id.</param>
        /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesOrderItem>> GetSalesOrderItemsListBySalesOrderIdAsync(Guid salesOrderId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesOrderItem.Where(so => so.SalesOrderId == salesOrderId).ToListAsync(token);
            }
            return await _context.BASalesOrderItem.Where(so => so.SalesOrderId == salesOrderId && !so.Deleted).ToListAsync(token);
        }

        /// <summary>
        /// Get sales order item list by ERP sales order key.
        /// </summary>
        /// <param name="erpSalesOrderKey">salesOrderKey unique key of connector.</param>
        /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesOrderItem>> GetSalesOrderItemsListByERPSalesOrderKeyAsync(string erpSalesOrderKey, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesOrderItem.Where(so => so.ERPSalesOrderKey == erpSalesOrderKey).ToListAsync(token);
            }
            return await _context.BASalesOrderItem.Where(so => so.ERPSalesOrderKey == erpSalesOrderKey && !so.Deleted).ToListAsync(token);
        }


        public async Task<List<BusBASalesOrderItemDTO>> GetSalesOrderItemListBySOIdAsync(Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ERPSalesOrderItemKey, ERPConnectorKey, ERPSalesOrderKey, ERPItemKey, SerialOrBatchNo, ItemName, Quantity, QuantityUnit, UnitPrice, Unit,
                        DiscountPercent, DiscountAmount, TaxCode, TaxPercent, TotalLC, Whse, BlanketAgreementNo, ShipFromAddress, ShipFromAddressKey, ShipToAddress, 
                        ERPShipToAddressKey, BillToAddress, ERPBillToAddressKey
                        FROM be.BASalesOrderItem WHERE SalesOrderId=@SOID";

            SqlParameter soIDParam = new SqlParameter("SOID", soId);
            return await _context.BusBASalesOrderItemDTOQuery.FromSql(sql, new object[] { soIDParam }).ToListAsync(cancellationToken);
        }

        #endregion Get

    }
}
