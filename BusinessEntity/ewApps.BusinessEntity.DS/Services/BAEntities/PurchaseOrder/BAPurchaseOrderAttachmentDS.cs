using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BAPurchaseOrderAttachment entity.
    /// </summary>
    public class BAPurchaseOrderAttachmentDS:BaseDS<BAPurchaseOrderAttachment>, IBAPurchaseOrderAttachmentDS {

        #region Local Member 

        IBAPurchaseOrderAttachmentRepository _repository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sessionmanager"></param>
        public BAPurchaseOrderAttachmentDS(IBAPurchaseOrderAttachmentRepository repository, IUserSessionManager sessionmanager) : base(repository) {
            _repository = repository;
            _sessionmanager = sessionmanager;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="pOrderId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _repository.GetPurchaseOrderAttachmentListByOrderIdAsync(pOrderId, cancellationToken);
        }

    #endregion Get

    #region Add

        ///<inheritdoc/>
    public async Task AddPurchaseOrderAttachmentListAsync(List<BAPurchaseOrderAttachmentSyncDTO> purchaseOrderAttachmentList, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < purchaseOrderAttachmentList.Count; i++)
      {
        await AddPurchaseOrderAttachmentAsync(purchaseOrderAttachmentList[i], tenantId, tenantUserId, purchaseOrderId);
      }
    }

    private async Task AddPurchaseOrderAttachmentAsync(BAPurchaseOrderAttachmentSyncDTO purchaseOrderAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken))
    {


      purchaseOrderAttachmentSyncDTO.PurchaseOrderId = purchaseOrderId;
      BAPurchaseOrderAttachment purchaseOrderAttachment = BAPurchaseOrderAttachmentSyncDTO.MapToEntity(purchaseOrderAttachmentSyncDTO);
      //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
      Guid id = Guid.NewGuid();
      purchaseOrderAttachment.ID = id;
      purchaseOrderAttachment.CreatedBy = tenantUserId;
      purchaseOrderAttachment.UpdatedBy = tenantUserId;
      purchaseOrderAttachment.CreatedOn = DateTime.UtcNow;
      purchaseOrderAttachment.UpdatedOn = DateTime.UtcNow;
      purchaseOrderAttachment.Deleted = false;
      purchaseOrderAttachment.TenantId = tenantId;
      await AddAsync(purchaseOrderAttachment, token);
    }

#endregion Add

  }

}