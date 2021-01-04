/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
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
  ///  This class implements standard business logic and operations for BAContractItem entity.
  /// </summary>
  public class BAContractItemDS : BaseDS<BAContractItem>, IBAContractItemDS
  {

    #region Local variables

    IBAContractItemRepository _contractItemRepo;
    IBAItemMasterDS _itemMasterDS;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BAContractItemDS"/> class.
    /// </summary>
    /// <param name="contractItemRepo">The contract item repo.</param>
    public BAContractItemDS(IBAContractItemRepository contractItemRepo, IBAItemMasterDS itemMasterDS) : base(contractItemRepo)
    {
      _contractItemRepo = contractItemRepo;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Get

    /// <inheritdoc/>
    public async Task<IEnumerable<BusBAContractItemDTO>> GetContractItemListByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _contractItemRepo.GetContractItemListByContractId(businessTenantId, contractId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CustBAContractItemDTO>> GetContractItemListByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return _contractItemRepo.GetContractItemListByContractIdForCust(businessTenantId, contractId);
    }

    #endregion

    #region Add

    ///<inheritdoc/>
    public async Task AddContractItemListAsync(List<BAContractItemSyncDTO> ContractItemList, Guid tenantId, Guid tenantUserId, Guid cotractId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < ContractItemList.Count; i++)
      {
        //BAContractItem item = await FindAsync(im => im.ContractId == cotractId && im.ERPItemKey == ContractItemList[i].ERPItemKey && im.TenantId == tenantId);
        //if (item == null)
        //{
          await AddContractItemAsync(ContractItemList[i], tenantId, tenantUserId, cotractId);
        //}

      }
    }

    /// <summary>
    /// add sales quotation item data .
    /// </summary>
    /// <param name="contractItemSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="contractId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddContractItemAsync(BAContractItemSyncDTO contractItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid contractId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == contractItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        contractItemSyncDTO.ItemId = itemMaster.ID;
      }
      else
      {
        return;
      }
      BAContractItem contractItem = await FindAsync(contItem => contItem.ERPItemKey == contractItemSyncDTO.ERPItemKey && contItem.ERPContractKey == contractItemSyncDTO.ERPContractKey && contItem.TenantId == tenantId);
      if (contractItem != null)
      {
        contractItem = BAContractItemSyncDTO.MapToEntity(contractItemSyncDTO);
        UpdateSystemFieldsByOpType(contractItem, OperationType.Update);
        await UpdateAsync(contractItem, contractItem.ID, token);
      }
      else
      {
        contractItemSyncDTO.ContractId = contractId;
        contractItem = BAContractItemSyncDTO.MapToEntity(contractItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        contractItem.ID = id;
        contractItem.CreatedBy = tenantUserId;
        contractItem.UpdatedBy = tenantUserId;
        contractItem.CreatedOn = DateTime.UtcNow;
        contractItem.UpdatedOn = DateTime.UtcNow;
        contractItem.Deleted = false;
        contractItem.TenantId = tenantId;
        await AddAsync(contractItem, token);
      }
    }



    #endregion Add
  }
}
