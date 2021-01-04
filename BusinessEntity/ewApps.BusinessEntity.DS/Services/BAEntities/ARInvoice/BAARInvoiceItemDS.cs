/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal/Amit Mundra <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// Class contains invoice item supportive methods.
  /// 1) Conatins required get invoice itemmethods.
  /// 2) Conatins add/update/delete methods.
  /// </summary>
  public class BAARInvoiceItemDS : BaseDS<BAARInvoiceItem>, IBAARInvoiceItemDS
  {

    IBAARInvoiceItemRepository _arInvoiceItemRepo;
    IBAItemMasterDS _itemMasterDS;

    #region Constructor

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="arInvoiceItemRepo">Invoiceitem data repo class.</param>
    public BAARInvoiceItemDS(IBAARInvoiceItemRepository arInvoiceItemRepo, IBAItemMasterDS itemMasterDS) : base(arInvoiceItemRepo)
    {
      _arInvoiceItemRepo = arInvoiceItemRepo;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Get

    /// <summary>
    /// Get invoice items by invoice id.
    /// </summary>
    /// <param name="invoiceId">InvoiceID</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<BAARInvoiceItemDQ>> GetInvoiceItemListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken))
    {
      return await _arInvoiceItemRepo.GetInvoiceItemListByInvoiceIdAsync(invoiceId, token);
    }

    #endregion Get

    #region Add

    ///<inheritdoc/>
    public async Task AddInvoiceItemListAsync(List<BAARInvoiceItemSyncDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < invoiceItemList.Count; i++)
      {
        //BAARInvoiceItem item = await FindAsync(im => im.ARInvoiceID == invoiceId && im.ERPItemKey == invoiceItemList[i].ERPItemKey && im.TenantId == tenantId);
        //if (item == null)
        //{
          await AddInvocieItemAsync(invoiceItemList[i], tenantId, tenantUserId, invoiceId);
        //}

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
    private async Task AddInvocieItemAsync(BAARInvoiceItemSyncDTO invoiceItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid invoiceId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == invoiceItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      invoiceItemSyncDTO.ARInvoiceID = invoiceId;
      if (itemMaster != null)
      {
        invoiceItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }
      BAARInvoiceItem invoieItem = await FindAsync(invitem => invitem.ERPItemKey == invoiceItemSyncDTO.ERPItemKey && invitem.ERPARInvoiceKey == invoiceItemSyncDTO.ERPARInvoiceKey && invitem.TenantId == tenantId);
      if (invoieItem != null)
      {
        invoieItem = BAARInvoiceItemSyncDTO.MapToEntity(invoiceItemSyncDTO);
        UpdateSystemFieldsByOpType(invoieItem, OperationType.Update);
        await UpdateAsync(invoieItem, invoieItem.ID, token);
      }
      else
      {
        invoieItem = BAARInvoiceItemSyncDTO.MapToEntity(invoiceItemSyncDTO);
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

    #region Add

    ///<inheritdoc/>
    public async Task AddBAARInvoiceItemListAsync(List<AddBAARInvoiceItemDTO> invoiceItemList, Guid tenantId, Guid tenantUserId, Guid invoiceId, string erpArInvoiceKey, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < invoiceItemList.Count; i++)
      {
        await AddBAARInvocieItemAsync(invoiceItemList[i], tenantId, tenantUserId, invoiceId, erpArInvoiceKey, token);
      }
    }

    /// <summary>
    /// add invice item .
    /// </summary>
    /// <param name="invoiceItemDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddBAARInvocieItemAsync(AddBAARInvoiceItemDTO invoiceItemDTO, Guid tenantId, Guid tenantUserId, Guid invoiceId, string erpArInvoiceKey, CancellationToken token = default(CancellationToken))
    {
      invoiceItemDTO.ARInvoiceID = invoiceId;
      BAARInvoiceItem invoieItem = AddBAARInvoiceItemDTO.MapToEntity(invoiceItemDTO, erpArInvoiceKey);
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

    #endregion Add
  }
}
