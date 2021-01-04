/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
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

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// Responsible for exposing all the methods that are intrecting 
    /// with the DB for retriving the data related to BAContractItem entity.
    /// </summary>
    public interface IBAContractItemDS:IBaseDS<BAContractItem> {

        /// <summary>
        /// Gets the contract item list by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contarct item list that matches the provided business tenant id and contract  id.</returns>
        Task<IEnumerable<BusBAContractItemDTO>> GetContractItemListByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CustBAContractItemDTO>> GetContractItemListByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddContractItemListAsync(List<BAContractItemSyncDTO> ContractItemList, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken));
  }
}
