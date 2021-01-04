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
    public interface IBADeliveryRepository:IBaseRepository<BADelivery> {

        #region Business Methods

        #region Get

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        IQueryable<BusBADeliveryDTO> GetDeliveryListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO);

        /// <summary>
        /// Gets the delivery detail with items by delivery identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="deliveryId">The delivery identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns delivery detail with items that matches provided delivery id.</returns>
        Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken);

        IQueryable<CustBADeliveryDTO> GetDeliveryListByBusinessTenantIdForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO);

        Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken);

        #endregion

        #endregion
    }
}
