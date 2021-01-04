/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Amit Mundra<amundra@eworkplaceapps.com>
 * Date:08 july 2019
 * 
 */


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS {
    public interface IBAPurchaseOrderDS:IBaseDS<BAPurchaseOrder> {

        /// <summary>
        /// Get purchase order list by tenantid.
        /// </summary>
        /// <param name="listDateFilterDTO">filter object</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get purchase order detail by purcheid and tenant id.
        /// </summary>
        /// <param name="businessTenantId">Business tenantid</param>
        /// <param name="poId">purchase order id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync(Guid businessTenantId, Guid poId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="pOrderId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Add purchaseOrder, order items and attachment.
        /// </summary>
        /// <param name="httpRequest">Contains purchase order attachment</param>
        /// <param name="request">AddBAPurchaseOrderDTO model in json string.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AddPurchaseOrderResponseDTO> AddPurchaseOrder(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        Task AddPurchaseOrderListAsync(List<BAPurchaseOrderSyncDTO> purchaseOrderList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));
    }
}
