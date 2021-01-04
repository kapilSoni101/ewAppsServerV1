using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {

  public interface ICustBAItemMasterDS {

    /// <summary>
    /// Gets the item master list by bus tenant identifier asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets the item master list by bus tenant identifier and item identifier asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="token">The token.</param>
    /// <returns></returns>
    Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemIdAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken));
  }
}
