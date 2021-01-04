using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UniqueIdentityGeneratorService;

namespace ewApps.BusinessEntity.DS
{

  public class BAPurchaseOrderItemDS : BaseDS<BAPurchaseOrderItem>, IBAPurchaseOrderItemDS
  {

    IBAPurchaseOrderItemRepository _bAPurchaseOrderItemRepository;
    IUniqueIdentityGeneratorDS _identityDataService;
    IBAItemMasterDS _itemMasterDS;
    IUnitOfWork _unitOfWork;

    /// <summary>
    /// BAPurchaseOrderItemDS
    /// </summary>
    /// <param name="bAPurchaseOrderItemRepository"></param>
    /// <param name="identityDataService"></param>
    /// <param name="unitOfWork"></param>
    public BAPurchaseOrderItemDS(IBAPurchaseOrderItemRepository bAPurchaseOrderItemRepository, IBAItemMasterDS itemMasterDS, IUniqueIdentityGeneratorDS identityDataService, IUnitOfWork unitOfWork) : base(bAPurchaseOrderItemRepository)
    {
      _bAPurchaseOrderItemRepository = bAPurchaseOrderItemRepository;
      _identityDataService = identityDataService;
      _itemMasterDS = itemMasterDS;
      _unitOfWork = unitOfWork;
    }

    #region CRUD

    #region Add



    ///<inheritdoc/>
    public async Task AddPurchaseOrderItemListAsync(List<BAPurchaseOrderItemSyncDTO> purchaseOrderItemList, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < purchaseOrderItemList.Count; i++)
      {
        BAPurchaseOrderItem item = await FindAsync(im => im.PurchaseOrderId == purchaseOrderId && im.ERPItemKey == purchaseOrderItemList[i].ERPItemKey && im.TenantId == tenantId);
        if (item == null)
        {
          await AddPurchaseOrderItemAsync(purchaseOrderItemList[i], tenantId, tenantUserId, purchaseOrderId);
        }
      }
    }

    private async Task AddPurchaseOrderItemAsync(BAPurchaseOrderItemSyncDTO purchaseOrderItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid purchaseOrderId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == purchaseOrderItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        purchaseOrderItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }

      purchaseOrderItemSyncDTO.PurchaseOrderId = purchaseOrderId;
      BAPurchaseOrderItem purchaseOrderItem = BAPurchaseOrderItemSyncDTO.MapToEntity(purchaseOrderItemSyncDTO);
      //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
      Guid id = Guid.NewGuid();
      purchaseOrderItem.ID = id;
      purchaseOrderItem.CreatedBy = tenantUserId;
      purchaseOrderItem.UpdatedBy = tenantUserId;
      purchaseOrderItem.CreatedOn = DateTime.UtcNow;
      purchaseOrderItem.UpdatedOn = DateTime.UtcNow;
      purchaseOrderItem.Deleted = false;
      purchaseOrderItem.TenantId = tenantId;
      await AddAsync(purchaseOrderItem, token);
    }


    #endregion Add

    /// <summary>
    /// Add purchaseorder item list.
    /// </summary>
    /// <param name="OrderItemList">Purchase OrderItemList</param>
    /// <param name="pOrder"></param>
    /// <param name="commit">Commit changes.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns></returns>
    public async Task AddPurchaseOrderItemLiast(List<BAPurchaseOrderItemDTO> OrderItemList, BAPurchaseOrder pOrder, bool commit, CancellationToken token = default(CancellationToken))
    {
      BAPurchaseOrderItem purchaseOrderItem;
      int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAPurchaseOrderItem, "POI", 1000);

      for (int i = 0; i < OrderItemList.Count; i++)
      {
        purchaseOrderItem = new BAPurchaseOrderItem();
        OrderItemList[i].MapPurchaseOrderToEntity(purchaseOrderItem);
        purchaseOrderItem.ERPPurchaseOrderItemKey = BusinessEntityConstants.PurchaseOrderItemPrefix + identity;
        purchaseOrderItem.ERPPurchaseOrderKey = pOrder.ERPPurchaseOrderKey;
        purchaseOrderItem.PurchaseOrderId = pOrder.ID;
        UpdateSystemFieldsByOpType(purchaseOrderItem, OperationType.Add);
        await _data.AddAsync(purchaseOrderItem, token);
      }
      if (commit)
      {
        _unitOfWork.Save();
      }
    }

    #endregion CRUD

  }
}
