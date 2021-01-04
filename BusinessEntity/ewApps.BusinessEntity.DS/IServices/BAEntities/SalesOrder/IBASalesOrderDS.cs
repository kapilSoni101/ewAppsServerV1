using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Contains all required sales order methods.
    /// </summary>
    public interface IBASalesOrderDS:IBaseDS<BASalesOrder> {

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return sales order with deleted items.</param>        
        /// <returns>return list of sales order entity.</returns>
        Task<List<BASalesOrder>> GetSalesOrderListByTenantIdAsync(Guid tenantId, bool includeDeleted, CancellationToken token = default(CancellationToken));

       Task AddSalesOrderListAsync(List<BASalesOrderSyncDTO> salesOrderList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the sales order list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns sales order list that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets sales order detail that matches the provided business tenant id and sales order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="soId">The sales order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="BusBASalesOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);

        Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));

        //Task AddSalesOrderListAsyncForCust(List<CustBASalesOrderAddDTO> salesOrderList, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken));

        Task AddSalesOrderAsyncForCust(CustBASalesOrderAddDTO dto, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken));

        Task UpdateSalesOrderAsyncForCust(CustBASalesOrderUpdateDTO dto, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken));
    }
}
