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
using System.Linq;
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
    public interface IBADeliveryItemRepository:IBaseRepository<BADeliveryItem> {

        /// <summary>
        /// Gets the delivery item name list by delivery id.
        /// </summary>
        /// <param name="deliveryId">The delivery to find delivery items.</param>
        /// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        IQueryable<string> GetDeliveryItemNameListByDeliveryId(Guid deliveryId);

        /// <summary>
        /// Gets the delivery item list by delivery identifier.
        /// </summary>
        /// <param name="deliveryId">The delivery identifier to find items.</param>
        /// <returns>Returns list of delivery items that matches provided delivery id.</returns>
        IQueryable<BusBADeliveryItemDTO> GetDeliveryItemListByDeliveryId(Guid deliveryId);

        IQueryable<CustBADeliveryItemDTO> GetDeliveryItemListByDeliveryIdForCust(Guid deliveryId);
    }
}
