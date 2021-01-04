using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

  public interface ICustBAItemMasterDS {

    Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemidAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));

    Task<BASyncItemPriceDTO> PullItemPriceAsync(PullItemPriceReqDTO request, CancellationToken token = default(CancellationToken));
  }
}
