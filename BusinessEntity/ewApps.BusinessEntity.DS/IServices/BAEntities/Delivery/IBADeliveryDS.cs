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
  /// This interface provides the CRUD operations related methods for delviery entity.
  /// </summary>
  public interface IBADeliveryDS:IBaseDS<BADelivery> {

        #region Add

        /// <summary>
        /// Add delivery list .
        /// </summary>
        /// <param name="deliveryList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddDeliveryListAsync(List<BADeliverySyncDTO> deliveryList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        Task<IEnumerable<BusBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);



        #endregion Add

        #region Get   
        
        /// <summary>
        /// Gets the delivery detail by delivery identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="deliveryId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail that matches provided delivery id.</returns>
        Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId,Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<CustBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken);
    Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));


        #endregion
  }
}
