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
    public interface IBAPurchaseOrderRepository:IBaseRepository<BAPurchaseOrder> {

        /// <summary>
        /// Get purchase order list by tenantid.
        /// </summary>
        /// <param name="businessTenantId">Tenant id</param>
        /// <param name="listDateFilterDTO">filter object</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get purchase order detail by purcheid and tenant id.
        /// </summary>
        /// <param name="businessTenantId">Business tenantid</param>
        /// <param name="poId">purchase order id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync(Guid businessTenantId, Guid poId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get purchase order item by purchaseid.
        /// </summary>
        /// <param name="poId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BAPurchaseOrderItemDTO>> GetPurchaseOrderItemListByPOIdAsync(Guid poId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdForVendAsync(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken));
  }
}
