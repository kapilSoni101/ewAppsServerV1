using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    public interface IBusBAASNDS {

        /// <summary>
        /// Gets the asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the asn detail by asn identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the vendor's asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        Task<IEnumerable<BusBAASNDTO>> GetVendorASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the vendor's asn detail by asn identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        Task<BusBAASNViewDTO> GetVendorASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
