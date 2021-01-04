/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 04 Feb 2020
 * 
 * Contributor/s: 
 * Last Updated On: 04 Feb 2020
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using ewApps.Core.Money;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public interface IPurchaseOrderDS {

        #region Get

        /// <summary>
        /// Get purchase order list by TenantId.
        /// </summary>
        /// <param name="listDateFilterDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="busTenantId">Business tenantid.</param>
        /// <param name="purchaseorderId">Unique invoice id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAPurchaseOrderViewDTO> GetPurchaseOrderByPurchaseOrderIdAsync(Guid busTenantId, Guid purchaseorderId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="purchaseOrderId">Unique purchaseOrder id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid purchaseOrderId, CancellationToken token = default(CancellationToken));

    Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get

    #region CRUD

    /// <summary>
    /// Add purchase order.
    /// </summary>
    /// <param name="httpRequest">Contains request object</param>
    /// <param name="request">Contains PruchaseOrder json.</param>
    /// <param name="token">Cancellation token for async operations</param>   
    Task<AddPurchaseOrderResponseDTO> AddPurchaseOrderAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        #endregion CRUD

    }
}
