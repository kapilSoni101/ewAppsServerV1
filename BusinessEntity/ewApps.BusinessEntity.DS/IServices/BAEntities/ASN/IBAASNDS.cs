using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
   public  interface IBAASNDS:IBaseDS<BAASN> {
        Task AddASNListAsync(List<BAASNSyncDTO> asnList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <param name="asnType">ASN type</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, string asnType, CancellationToken cancellationToken=default(CancellationToken));

        /// <summary>
        /// Gets the asn detail by asn identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="asnType">ASN Type</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, string asnType, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Gets the asn detail by asn identifier.
    /// </summary>
    /// <param name="asnId">The asn identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
    Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));
  }
}
