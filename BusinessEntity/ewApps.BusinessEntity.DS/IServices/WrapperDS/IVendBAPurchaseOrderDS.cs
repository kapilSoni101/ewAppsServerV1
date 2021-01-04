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
    /// This interface defines <see cref="BASalesOrder"/> entity operations for business.
    /// </summary>
    public interface IVendBAPurchaseOrderDS : IBaseDS<BAPurchaseOrder>
  {

    /// <summary>
    /// Get purchase order list by tenantid.
    /// </summary>
    /// <param name="listDateFilterDTO">filter object</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get purchase order detail by purcheid and tenant id.
    /// </summary>
    /// <param name="businessTenantId">Business tenantid</param>
    /// <param name="poId">purchase order id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync(Guid businessTenantId, Guid poId, CancellationToken cancellationToken = default(CancellationToken));

    Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken));
  }
}
