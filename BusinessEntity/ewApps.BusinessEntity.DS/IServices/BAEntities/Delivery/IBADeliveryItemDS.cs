using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This interface provides the CRUD operations related methods for delviery entity.
    /// </summary>
    public interface IBADeliveryItemDS:IBaseDS<BADeliveryItem> {

        #region Add

        /// <summary>
        /// add delivery item list .
        /// </summary>
        /// <param name="deliveryItemList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddDeliveryItemListAsync(List<BADeliveryItemSyncDTO> deliveryItemList, Guid tenantId, Guid tenantUserId, Guid deliveryId, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Get

        /// <summary>
        /// Gets the delivery item name list by delivery id.
        /// </summary>
        /// <param name="deliveryId">The delivery to find delivery items.</param>
        /// <param name="cancellationToken">The async task cancellation token.</param>
        /// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the delivery item list by delivery identifier.
        /// </summary>
        /// <param name="deliveryId">The delivery identifier to find items.</param>
        /// <param name="cancellationToken">The async task cancellation token.</param>
        /// <returns>Returns list of delivery items that matches provided delivery id.</returns>
        Task<IEnumerable<BusBADeliveryItemDTO>> GetDeliveryItemListByDeliveryId(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CustBADeliveryItemDTO>> GetDeliveryItemListByDeliveryIdForCust(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion

  }
}
