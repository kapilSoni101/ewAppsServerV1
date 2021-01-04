using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Contains all required sales order methods.
    /// </summary>
    public interface IBusBASalesOrderDS {
        /// <summary>
        /// Gets the sales order list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns sales order list that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsync(Guid businessTenantId,ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets sales order detail that matches the provided business tenant id and sales order id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="soId">The sales order identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="BusBASalesOrderViewDTO"/> that matches provided business tenant and sales order id.</returns>
        Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
