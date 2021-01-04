using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// This interface defines <see cref="BASalesOrder"/> entity operations for business.
    /// </summary>
    public interface ICustBASalesOrderDS {

        /// <summary>
        /// Gets the sales order list by business tenant identifier asynchronous.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns sales order list that matches the provided business tenatn id.</returns>
        Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets sales order detail that matches the provided business tenant id and sales order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="soId">The sales order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="CustBASalesOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddSalesOrderWithItem(CustBASalesOrderAddDTO addDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateSalesOrderWithItem(CustBASalesOrderUpdateDTO updateDTO, CancellationToken cancellationToken = default(CancellationToken));
    }
}
