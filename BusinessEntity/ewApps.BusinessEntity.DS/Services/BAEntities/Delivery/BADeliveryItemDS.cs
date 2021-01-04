using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using System.Linq;
using System.Linq.Expressions;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class manages the CRUD operations related methods and business logics for deliveryItemR entity.
  /// </summary>
  public class BADeliveryItemDS : BaseDS<BADeliveryItem>, IBADeliveryItemDS
  {

    IBADeliveryItemRepository _deliveryItemRepo;
    IBAItemMasterDS _itemMasterDS;

    #region Constructor

    /// <summary>
    /// Public constructor for DeliveryItemDS class.
    /// </summary>
    /// <param name="deliveryItemRepo">Repository class dependancy for delviery item.</param>
    public BADeliveryItemDS(IBADeliveryItemRepository deliveryItemRepo, IBAItemMasterDS itemMasterDS) : base(deliveryItemRepo)
    {
      _deliveryItemRepo = deliveryItemRepo;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Add

    ///<inheritdoc/>
    public async Task AddDeliveryItemListAsync(List<BADeliveryItemSyncDTO> deliveryItemList, Guid tenantId, Guid tenantUserId, Guid deliveryId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < deliveryItemList.Count; i++)
      {
        //BADeliveryItem item = await FindAsync(im => im.DeliveryID == deliveryId && im.ERPItemKey == deliveryItemList[i].ERPItemKey && im.TenantId == tenantId);
        //if (item == null)
        //{
          await AddDeliveryItemAsync(deliveryItemList[i], tenantId, tenantUserId, deliveryId);
        //}


      }
    }

    /// <summary>
    /// add delivery item.
    /// </summary>
    /// <param name="deliveryItemSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddDeliveryItemAsync(BADeliveryItemSyncDTO deliveryItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid deliveryId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == deliveryItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        deliveryItemSyncDTO.ItemID = itemMaster.ID;
      }
      else
      {
        return;
      }
      BADeliveryItem deliveryItem = await FindAsync(delitem => delitem.ERPItemKey == deliveryItemSyncDTO.ERPItemKey && delitem.ERPDeliveryKey == deliveryItemSyncDTO.ERPDeliveryKey && delitem.TenantId == tenantId);
      if (deliveryItem != null)
      {
        deliveryItem = BADeliveryItemSyncDTO.MapToEntity(deliveryItemSyncDTO);
        UpdateSystemFieldsByOpType(deliveryItem, OperationType.Update);
        await UpdateAsync(deliveryItem, deliveryItem.ID, token);
      }
      else
      {
        deliveryItemSyncDTO.DeliveryID = deliveryId;
        deliveryItem = BADeliveryItemSyncDTO.MapToEntity(deliveryItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        deliveryItem.ID = id;
        deliveryItem.CreatedBy = tenantUserId;
        deliveryItem.UpdatedBy = tenantUserId;
        deliveryItem.CreatedOn = DateTime.UtcNow;
        deliveryItem.UpdatedOn = DateTime.UtcNow;
        deliveryItem.Deleted = false;
        deliveryItem.TenantId = tenantId;

        await AddAsync(deliveryItem, token);
      }
    }

    #endregion Add

    #region Get

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _deliveryItemRepo.GetDeliveryItemNameListByDeliveryId(deliveryId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BusBADeliveryItemDTO>> GetDeliveryItemListByDeliveryId(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _deliveryItemRepo.GetDeliveryItemListByDeliveryId(deliveryId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CustBADeliveryItemDTO>> GetDeliveryItemListByDeliveryIdForCust(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _deliveryItemRepo.GetDeliveryItemListByDeliveryIdForCust(deliveryId);
    }

    #endregion


  }
}
