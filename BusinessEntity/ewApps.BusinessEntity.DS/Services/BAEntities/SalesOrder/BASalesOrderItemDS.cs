using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// Contains sales order items required methods to add/update/delete operation.
  /// Also contains method for getting sales order items.
  /// </summary>
  public class BASalesOrderItemDS : BaseDS<BASalesOrderItem>, IBASalesOrderItemDS
  {

    #region Local variables

    IBASalesOrderItemRepository _baSalesOrderItemRepository;
    IBAItemMasterDS _itemMasterDS;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// default constructor.
    /// </summary>
    /// <param name="baSalesOrderItemRepository"></param>
    public BASalesOrderItemDS(IBASalesOrderItemRepository baSalesOrderItemRepository, IBAItemMasterDS itemMasterDS) : base(baSalesOrderItemRepository)
    {
      _baSalesOrderItemRepository = baSalesOrderItemRepository;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Get

    /// <summary>
    /// Get sales order item list by teanntid.
    /// </summary>
    /// <param name="salesOrderId">salesOrderId unique id.</param>
    /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    public async Task<List<BASalesOrderItem>> GetSalesOrderItemsListBySalesOrderIdAsync(Guid salesOrderId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _baSalesOrderItemRepository.GetSalesOrderItemsListBySalesOrderIdAsync(salesOrderId, includeDeleted, token);
    }

    /// <summary>
    /// Get sales order item list by ERP sales order key.
    /// </summary>
    /// <param name="erpSalesOrderKey">salesOrderKey unique key of connector.</param>
    /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    public async Task<List<BASalesOrderItem>> GetSalesOrderItemsListByERPSalesOrderKeyAsync(string erpSalesOrderKey, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _baSalesOrderItemRepository.GetSalesOrderItemsListByERPSalesOrderKeyAsync(erpSalesOrderKey, includeDeleted, token);
    }

    /// <inheritdoc/>
    public async Task<List<BusBASalesOrderItemDTO>> GetSalesOrderItemListBySOIdAsync(Guid soId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _baSalesOrderItemRepository.GetSalesOrderItemListBySOIdAsync(soId, cancellationToken);
    }


    #endregion Get

    #region Add

    ///<inheritdoc/>
    public async Task AddSalesOrderItemListAsync(List<BASalesOrderItemSyncDTO> salesOrderItemList, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < salesOrderItemList.Count; i++)
      {
          await AddSalesOrderItemAsync(salesOrderItemList[i], tenantId, tenantUserId, salesOrderId);
        
      }
    }

    private async Task AddSalesOrderItemAsync(BASalesOrderItemSyncDTO salesOrderItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == salesOrderItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        salesOrderItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }
      BASalesOrderItem salesOrderItem = await FindAsync(SOitem => SOitem.ERPItemKey == salesOrderItemSyncDTO.ERPItemKey && SOitem.ERPSalesOrderKey == salesOrderItemSyncDTO.ERPSalesOrderKey && SOitem.TenantId == tenantId);
      if (salesOrderItem != null)
      {
        salesOrderItem = BASalesOrderItemSyncDTO.MapToEntity(salesOrderItemSyncDTO);
        UpdateSystemFieldsByOpType(salesOrderItem, OperationType.Update);
        await UpdateAsync(salesOrderItem, salesOrderItem.ID, token);
      }
      else
      {
        salesOrderItemSyncDTO.SalesOrderId = salesOrderId;
        salesOrderItem = BASalesOrderItemSyncDTO.MapToEntity(salesOrderItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        salesOrderItem.ID = id;
        salesOrderItem.CreatedBy = tenantUserId;
        salesOrderItem.UpdatedBy = tenantUserId;
        salesOrderItem.CreatedOn = DateTime.UtcNow;
        salesOrderItem.UpdatedOn = DateTime.UtcNow;
        salesOrderItem.Deleted = false;
        salesOrderItem.TenantId = tenantId;
        await AddAsync(salesOrderItem, token);
      }
    }
    ///<inheritdoc/>
    public async Task AddSalesOrderItemListAsyncForCust(List<CustBASalesOrderItemDTO> salesOrderItemList, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < salesOrderItemList.Count; i++)
      {
        salesOrderItemList[i].SalesOrderId = salesOrderId;
        await AddSalesOrderItemAsyncForCust(salesOrderItemList[i], tenantId, tenantUserId, salesOrderId);
      }
    }


    ///<inheritdoc/>
    private async Task AddSalesOrderItemAsyncForCust(CustBASalesOrderItemDTO dto, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      BASalesOrderItem salesOrderItem = new BASalesOrderItem();

      salesOrderItem = CustBASalesOrderItemDTO.MapToEntity(dto, salesOrderItem);

      UpdateSystemFieldsByOpType(salesOrderItem, OperationType.Add);
      //Guid id = Guid.NewGuid();
      //salesOrderItem.ID = id;
      //salesOrderItem.CreatedBy = tenantUserId;
      //salesOrderItem.UpdatedBy = tenantUserId;
      //salesOrderItem.CreatedOn = DateTime.UtcNow;
      //salesOrderItem.UpdatedOn = DateTime.UtcNow;
      //salesOrderItem.Deleted = false;
      //salesOrderItem.TenantId = tenantId;
      await AddAsync(salesOrderItem, token);
    }

    #endregion Add


    ///<inheritdoc/>
    public async Task UpdateSalesOrderItemListAsyncForCust(List<CustBASalesOrderItemDTO> salesOrderItemList, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      // first delete all existing items and add again the new.
      await DeleteSalesOrderItemsByOrderIdAsyncForCust(salesOrderId, token);

      // Now insert all items again.
      for (int i = 0; i < salesOrderItemList.Count; i++)
      {
        salesOrderItemList[i].SalesOrderId = salesOrderId;
        await AddSalesOrderItemAsyncForCust(salesOrderItemList[i], tenantId, tenantUserId, salesOrderId);
      }

    }

    ///<inheritdoc/>
    public async Task DeleteSalesOrderItemsByOrderIdAsyncForCust(Guid salesOrderId, CancellationToken token = default(CancellationToken))
    {
      // get all items first.
      IEnumerable<BASalesOrderItem> salesOrderItemList = await FindAllAsync(i => i.SalesOrderId == salesOrderId);

      foreach (BASalesOrderItem item in salesOrderItemList)
      {
        await DeleteAsync(item.ID, token);
      }
    }

  }
}
