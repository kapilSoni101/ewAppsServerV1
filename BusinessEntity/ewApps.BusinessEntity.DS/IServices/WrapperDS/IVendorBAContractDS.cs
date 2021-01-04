using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
    public interface IVendorBAContractDS {

        Task<IEnumerable<VendorBAContractDTO>> GetContractListByBusinessIdAsyncForVendorAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        Task<VendorBAContractViewDTO> GetContractDetailByContractIdForVendorAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

    }
}
