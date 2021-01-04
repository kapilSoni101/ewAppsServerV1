using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BAAPinvoiceAttachment entity.
    /// </summary>
    public class BAAPInvoiceAttachmentDS:BaseDS<BAAPInvoiceAttachment>, IBAAPInvoiceAttachmentDS {

    #region Local variables

    IBAAPInvoiceAttachmentRepository _invoiceAttachmentRepo;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invoiceAttachmentRepo"></param>
    public BAAPInvoiceAttachmentDS(IBAAPInvoiceAttachmentRepository invoiceAttachmentRepo) : base(invoiceAttachmentRepo)
    {
      _invoiceAttachmentRepo = invoiceAttachmentRepo;
    }

    #endregion Constructor

    #region Add

    ///<inheritdoc/>
    public async Task AddAPInvoiceAttachmentListAsync(List<BAAPInvoiceAttachmentSyncDTO> aPInvoiceAttachmentList, Guid tenantId, Guid tenantUserId, Guid apInvoiceId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < aPInvoiceAttachmentList.Count; i++)
      {
        await AddAPInvoiceAttachmentAsync(aPInvoiceAttachmentList[i], tenantId, tenantUserId, apInvoiceId);
      }
    }

    private async Task AddAPInvoiceAttachmentAsync(BAAPInvoiceAttachmentSyncDTO aPInvoiceAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid apInvoiceId, CancellationToken token = default(CancellationToken))
    {

      BAAPInvoiceAttachment aPInvoiceAttachment = await FindAsync(invattach => invattach.ERPAPInvoiceAttachmentKey == aPInvoiceAttachmentSyncDTO.ERPAPInvoiceAttachmentKey
      && invattach.ERPAPInvoiceKey == aPInvoiceAttachmentSyncDTO.ERPAPInvoiceKey && invattach.TenantId == tenantId);
      if (aPInvoiceAttachment != null)
      {
        aPInvoiceAttachment = BAAPInvoiceAttachmentSyncDTO.MapToEntity(aPInvoiceAttachmentSyncDTO);
        UpdateSystemFieldsByOpType(aPInvoiceAttachment, OperationType.Update);
        await UpdateAsync(aPInvoiceAttachment, aPInvoiceAttachment.ID, token);
      }
      else
      {
        aPInvoiceAttachmentSyncDTO.APInvoiceId = apInvoiceId;
        aPInvoiceAttachment = BAAPInvoiceAttachmentSyncDTO.MapToEntity(aPInvoiceAttachmentSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        aPInvoiceAttachment.ID = id;
        aPInvoiceAttachment.CreatedBy = tenantUserId;
        aPInvoiceAttachment.UpdatedBy = tenantUserId;
        aPInvoiceAttachment.CreatedOn = DateTime.UtcNow;
        aPInvoiceAttachment.UpdatedOn = DateTime.UtcNow;
        aPInvoiceAttachment.Deleted = false;
        aPInvoiceAttachment.TenantId = tenantId;
        await AddAsync(aPInvoiceAttachment, token);
      }
    }

    #endregion Add

  }

}