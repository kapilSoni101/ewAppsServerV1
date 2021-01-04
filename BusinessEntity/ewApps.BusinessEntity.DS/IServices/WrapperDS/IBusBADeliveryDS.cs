using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// This interface defines <see cref="BADelivery"/> entity operations for business.
    /// </summary>
    public interface IBusBADeliveryDS {
        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the delivery item name list by delivery id.
        /// </summary>
        /// <param name="deliveryId">The delivery to find delivery items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the delivery detail with items by delivery identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="deliveryId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail with items that matches provided delivery id.</returns>
        Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId,Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));


    }
}