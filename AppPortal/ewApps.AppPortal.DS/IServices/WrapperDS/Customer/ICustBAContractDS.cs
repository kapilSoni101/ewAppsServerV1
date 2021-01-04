using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This interface defines contract operations for business tenant.
    /// </summary>
    public interface ICustBAContractDS {

        /// <summary>
        /// Gets the contract list by business tenant id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id to filter contract list.</param>
        /// <param name="listDateFilterDTO">The DTO to filter contract list.</param>
        /// <param name="cancellationToken">The token to cancel async operation.</param>
        /// <returns>Returns contract list that matches given tenant id.</returns>
        Task<IEnumerable<CustBAContractDTO>> GetContractListByBusinessIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken= default(CancellationToken));
    }
}
