using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BAAPInvoiceItem entity.
    /// </summary>
    public class BAAPInvoiceItemDS:BaseDS<BAAPInvoiceItem>, IBAAPInvoiceItemDS {

    IBAAPInvoiceItemRepository _apInvoiceItemRepo;
    IBAItemMasterDS _itemMasterDS;

    #region Constructor

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="arInvoiceItemRepo">Invoiceitem data repo class.</param>
    public BAAPInvoiceItemDS(IBAAPInvoiceItemRepository apInvoiceItemRepo, IBAItemMasterDS itemMasterDS) : base(apInvoiceItemRepo)
    {
      _apInvoiceItemRepo = apInvoiceItemRepo;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

   

    #region Add

    ///<inheritdoc/>
    public async Task AddInvoiceItemListAsync(List<BAAPInvoiceItemSyncDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < invoiceItemList.Count; i++)
      {
        BAAPInvoiceItem item = await FindAsync(im => im.APInvoiceID == invoiceId && im.ERPItemKey == invoiceItemList[i].ERPItemKey && im.TenantId == tenantId);
        if (item == null)
        {
          await AddInvocieItemAsync(invoiceItemList[i], tenantId, tenantUserId, invoiceId);
        }

      }
    }

    /// <summary>
    /// add invice item .
    /// </summary>
    /// <param name="invoiceItemSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddInvocieItemAsync(BAAPInvoiceItemSyncDTO invoiceItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == invoiceItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      invoiceItemSyncDTO.APInvoiceID = invoiceId;
      if (itemMaster != null)
      {
        invoiceItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }
      BAAPInvoiceItem invoieItem = await FindAsync(invitem => invitem.ERPItemKey == invoiceItemSyncDTO.ERPItemKey && invitem.ERPAPInvoiceKey == invoiceItemSyncDTO.ERPAPInvoiceKey && invitem.TenantId == tenantId);
      if (invoieItem != null)
      {
        invoieItem = BAAPInvoiceItemSyncDTO.MapToEntity(invoiceItemSyncDTO);
        UpdateSystemFieldsByOpType(invoieItem, OperationType.Update);
        await UpdateAsync(invoieItem, invoieItem.ID, token);
      }
      else
      {
        invoieItem = BAAPInvoiceItemSyncDTO.MapToEntity(invoiceItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        invoieItem.ID = id;
        invoieItem.CreatedBy = tenantUserId;
        invoieItem.UpdatedBy = tenantUserId;
        invoieItem.CreatedOn = DateTime.UtcNow;
        invoieItem.UpdatedOn = DateTime.UtcNow;
        invoieItem.Deleted = false;
        invoieItem.TenantId = tenantId;
        await AddAsync(invoieItem, token);
      }

    }

    #endregion Add

  }

}