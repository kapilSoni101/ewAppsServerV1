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
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.Data {
    // <summary>
    /// This interface provides methods to execute all database operations to get InvoiceItem and
    /// related data transfer objects.
    // </summary>
    public interface IBASalesOrderItemRepository:IBaseRepository<BASalesOrderItem> {

        #region Get

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="salesOrderId">salesOrderId unique id.</param>
        /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
        /// <returns>return list of sales order entity.</returns>
        Task<List<BASalesOrderItem>> GetSalesOrderItemsListBySalesOrderIdAsync(Guid salesOrderId, bool includeDeleted, CancellationToken token = default(CancellationToken));

        Task<List<BASalesOrderItem>> GetSalesOrderItemsListByERPSalesOrderKeyAsync(string erpSalesOrderKey, bool includeDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the sales order item list by sales order identifier.
        /// </summary>
        /// <param name="soId">The so identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of sales order item that matches provieded sales order id.</returns>
        Task<List<BusBASalesOrderItemDTO>> GetSalesOrderItemListBySOIdAsync(Guid soId, CancellationToken cancellationToken = default(CancellationToken));


            #endregion Get

        }
    }
