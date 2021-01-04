using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
    public interface IBAASNItemDS:IBaseDS<BAASNItem> {
        Task AddASNItemListAsync(List<BAASNItemSyncDTO> asnItemList, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the asn item list by asn id.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <returns>Returns ASN Items list that matches provided ASN id.</returns>
        IEnumerable<BusBAASNItemDTO> GetASNItemListByASNId(Guid asnId);

    IEnumerable<CustBAASNItemDTO> GetASNItemListByASNIdForCust(Guid asnId);

  }
}
