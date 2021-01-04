using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
  /// Contains sales quotation required methods to add/update/delete operation.
  /// Also contains method for getting sales quotation.
  /// </summary>
  public class BASalesQuotationItemDS : BaseDS<BASalesQuotationItem>, IBASalesQuotationItemDS
  {

    #region Local variables

    IBASalesQuotationItemRepository _baSalesQuotationRepository;
    IBAItemMasterDS _itemMasterDS;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// default constructor.
    /// </summary>
    /// <param name="baSalesQuotationRepository"></param>
    public BASalesQuotationItemDS(IBASalesQuotationItemRepository baSalesQuotationRepository, IBAItemMasterDS itemMasterDS) : base(baSalesQuotationRepository)
    {
      _baSalesQuotationRepository = baSalesQuotationRepository;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Get

    /// <summary>
    /// Get sales quotration item list by teanntid.
    /// </summary>
    /// <param name="salesQuotationId">salesQuotationId unique id.</param>
    /// <param name="includeDeleted">return all sales quotation item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    public async Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListBySalesOrderIdAsync(Guid salesQuotationId, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _baSalesQuotationRepository.GetSalesQuotationItemsListBySalesOrderIdAsync(salesQuotationId, includeDeleted, token);
    }

    /// <summary>
    /// Get sales quotation item list by ERP sales quotation key.
    /// </summary>
    /// <param name="erpSalesQuotationKey">erpSalesQuotationKey unique key of connector.</param>
    /// <param name="includeDeleted">return all sales order item with deleted items if flag is true.</param>        
    /// <returns>return list of sales order entity.</returns>
    public async Task<List<BASalesQuotationItem>> GetSalesQuotationItemsListByERPSalesOrderKeyAsync(string erpSalesQuotationKey, bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      return await _baSalesQuotationRepository.GetSalesQuotationItemsListByERPSalesOrderKeyAsync(erpSalesQuotationKey, includeDeleted, token);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<BusBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _baSalesQuotationRepository.GetSalesQuotationItemListBySalesQuotationId(salesQuotationId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CustBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationIdForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _baSalesQuotationRepository.GetSalesQuotationItemListBySalesQuotationIdForCust(salesQuotationId);
    }


    /// <inheritdoc/>
    public async Task<IEnumerable<BusBASalesQuotationItemDTO>> GetSalesQuotationItemListBySalesQuotationId(Guid salesQuotationId)
    {
      return _baSalesQuotationRepository.GetSalesQuotationItemListBySalesQuotationId(salesQuotationId);
    }

    #endregion Get

    #region Add

    ///<inheritdoc/>
    public async Task AddSalesQuotationItemListAsync(List<BASalesQuotationItemSyncDTO> salesQuotationItemList, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < salesQuotationItemList.Count; i++)
      {
        //BASalesQuotationItem item = await FindAsync(im => im.SalesQuotationId == salesQuotationId && im.ERPItemKey == salesQuotationItemList[i].ERPItemKey && im.TenantId == tenantId);
        //if (item == null)
        //{
          await AddSalesQuotationItemAsync(salesQuotationItemList[i], tenantId, tenantUserId, salesQuotationId);
       // }

      }
    }

    /// <summary>
    /// add sales quotation item data .
    /// </summary>
    /// <param name="salesQuotationItemSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddSalesQuotationItemAsync(BASalesQuotationItemSyncDTO salesQuotationItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == salesQuotationItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        salesQuotationItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }
      BASalesQuotationItem salesQuotationItem = await FindAsync(SQitem => SQitem.ERPItemKey == salesQuotationItemSyncDTO.ERPItemKey && SQitem.ERPSalesQuotationKey == salesQuotationItemSyncDTO.ERPSalesQuotationKey && SQitem.TenantId == tenantId);
      if (salesQuotationItem != null)
      {
        salesQuotationItem = BASalesQuotationItemSyncDTO.MapToEntity(salesQuotationItemSyncDTO);
        UpdateSystemFieldsByOpType(salesQuotationItem, OperationType.Update);
        await UpdateAsync(salesQuotationItem, salesQuotationItem.ID, token);
      }
      else
      {
        salesQuotationItemSyncDTO.SalesQuotationId = salesQuotationId;
        salesQuotationItem = BASalesQuotationItemSyncDTO.MapToEntity(salesQuotationItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        salesQuotationItem.ID = id;
        salesQuotationItem.CreatedBy = tenantUserId;
        salesQuotationItem.UpdatedBy = tenantUserId;
        salesQuotationItem.CreatedOn = DateTime.UtcNow;
        salesQuotationItem.UpdatedOn = DateTime.UtcNow;
        salesQuotationItem.Deleted = false;
        salesQuotationItem.TenantId = tenantId;
        await AddAsync(salesQuotationItem, token);
      }

    }

    #endregion Add

  }
}
