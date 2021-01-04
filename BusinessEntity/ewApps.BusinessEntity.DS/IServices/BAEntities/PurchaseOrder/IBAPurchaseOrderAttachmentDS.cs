using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
  
  /// <summary>
  /// Contains purchase order methods.
  /// </summary>
  public interface IBAPurchaseOrderAttachmentDS:IBaseDS<BAPurchaseOrderAttachment> {

        #region Get

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="pOrderId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken));

    #endregion Get

    #region Add/Update/Delete
    Task AddPurchaseOrderAttachmentListAsync(List<BAPurchaseOrderAttachmentSyncDTO> purchaseOrderAttachmentList, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken));
        #endregion Add/Update/Delete

  }

}