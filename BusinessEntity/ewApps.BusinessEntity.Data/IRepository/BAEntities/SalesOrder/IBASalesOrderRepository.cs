// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

    // <summary>
    /// This interface provides methods to execute all database operations to get Invoice and
    /// related data transfer objects.
    // </summary>
    public interface IBASalesOrderRepository:IBaseRepository<BASalesOrder> {

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return sales order with deleted items.</param>        
        /// <returns>return list of sales order entity.</returns>
        Task<List<BASalesOrder>> GetSalesOrderListByTenantIdAsync(Guid tenantId, bool includeDeleted, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the sales order list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <returns>Returns sales order list that matches provided business tenant id.</returns>
        IQueryable<BusBASalesOrderDTO> GetSalesOrderListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO);


        /// <summary>
        /// Gets sales order detail that matches the provided business tenant id and sales order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="soId">The sales order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="BusBASalesOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));

        IQueryable<CustBASalesOrderDTO> GetSalesOrderListByBusinessTenantIdForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO);

        Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
