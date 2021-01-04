using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    public interface IBAPurchaseOrderItemDS:IBaseDS<BAPurchaseOrderItem> {

    Task AddPurchaseOrderItemListAsync(List<BAPurchaseOrderItemSyncDTO> purchaseOrderItemList, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add purchaseorder item list.
        /// </summary>
        /// <param name="OrderItemList">Purchase OrderItemList</param>
        /// <param name="pOrder">Added purchase order entity.</param>
        /// <param name="commit">Commit changes.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns></returns>
    Task AddPurchaseOrderItemLiast(List<BAPurchaseOrderItemDTO> OrderItemList, BAPurchaseOrder pOrder, bool commit, CancellationToken token = default(CancellationToken));

    }
}
