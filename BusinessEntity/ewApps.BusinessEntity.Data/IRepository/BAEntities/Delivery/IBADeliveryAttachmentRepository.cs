/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:21 October 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This interface responsible for exposing all the methods that are 
    /// intrecting with the DB for retriving the data related to BADeliveryAttachment entity.
    /// </summary>
    public interface IBADeliveryAttachmentRepository:IBaseRepository<BADeliveryAttachment> {

        /// <summary>
        /// Gets the delivery item list by delivery identifier.
        /// </summary>
        /// <param name="deliveryId">The delivery identifier to find items.</param>
        /// <param name="cancellationToken">The async task cancellation token.</param>
        /// <returns>Returns list of delivery items that matches provided delivery id.</returns>
        Task<IEnumerable<BusBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<CustBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdForCustAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken));
    }
}

