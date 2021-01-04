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
    /// with the DB for retriving the data related to BAContract entity.
    /// </summary>
    public interface IBAContractDS:IBaseDS<BAContract> {
        /// <summary>
        /// Gets the contract list by tenant id.
        /// </summary>
        /// <param name="tenantId">The tenant id to filter contract list.</param>
        /// <param name="listDateFilterDTO">The DTO to filter contract list.</param>
        /// <param name="cancellationToken">The token to cancel async operation.</param>
        /// <returns>Returns contract list that matches given tenant id.</returns>
        Task<IEnumerable<BusBAContractDTO>> GetContractListByTenantIdAsync(Guid tenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        Task<BusBAContractViewDTO> GetContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<CustBAContractDTO>> GetContractListByTenantIdAsyncForCust(Guid tenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

        Task AddContractListAsync(List<BAContractSyncDTO> contractList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));


    }
}
