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

  public class BAASNItemDS : BaseDS<BAASNItem>, IBAASNItemDS
  {

    #region Local variables

    IBAASNItemRepositorty _asnItemRepo;
    IBAItemMasterDS _itemMasterDS;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BAContractItemDS"/> class.
    /// </summary>
    /// <param name="contractItemRepo">The contract item repo.</param>
    public BAASNItemDS(IBAASNItemRepositorty asnItemRepo, IBAItemMasterDS itemMasterDS) : base(asnItemRepo)
    {
      _asnItemRepo = asnItemRepo;
      _itemMasterDS = itemMasterDS;
    }

    #endregion Constructor

    #region Add

    ///<inheritdoc/>
    public async Task AddASNItemListAsync(List<BAASNItemSyncDTO> asnItemList, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken))
    {
      for (int i = 0; i < asnItemList.Count; i++)
      {
      //  BAASNItem item = await FindAsync(im => im.ASNId == asnId && im.ERPItemKey == asnItemList[i].ERPItemKey && im.TenantId == tenantId);
      //  if (item == null)
      //  {
          await AddASNItemAsync(asnItemList[i], tenantId, tenantUserId, asnId);
        //}

      }
    }



    /// <summary>
    /// add sales quotation item data .
    /// </summary>
    /// <param name="asnItemSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <param name="asnId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddASNItemAsync(BAASNItemSyncDTO asnItemSyncDTO, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken))
    {
      BAItemMaster itemMaster = await _itemMasterDS.FindAsync(im => im.ERPItemKey == asnItemSyncDTO.ERPItemKey && im.TenantId == tenantId);

      if (itemMaster != null)
      {
        asnItemSyncDTO.ItemID = itemMaster.ID;
      }
      else
      {
        return;
      }
      BAASNItem asnItem = await FindAsync(asnitem => asnitem.ERPItemKey == asnItemSyncDTO.ERPItemKey && asnitem.ERPASNKey == asnItemSyncDTO.ERPASNKey && asnitem.TenantId == tenantId);
      if (asnItem != null)
      {
        asnItem = BAASNItemSyncDTO.MapToEntity(asnItemSyncDTO);
        UpdateSystemFieldsByOpType(asnItem, OperationType.Update);
        await UpdateAsync(asnItem, asnItem.ID, token);
      }
      else
      {
        asnItemSyncDTO.ASNId = asnId;
         asnItem = BAASNItemSyncDTO.MapToEntity(asnItemSyncDTO);
        //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
        Guid id = Guid.NewGuid();
        asnItem.ID = id;
        asnItem.CreatedBy = tenantUserId;
        asnItem.UpdatedBy = tenantUserId;
        asnItem.CreatedOn = DateTime.UtcNow;
        asnItem.UpdatedOn = DateTime.UtcNow;
        asnItem.Deleted = false;
        asnItem.TenantId = tenantId;
        await AddAsync(asnItem, token);
      }

    }

    #endregion Add

    #region Get Methods

    public IEnumerable<BusBAASNItemDTO> GetASNItemListByASNId(Guid asnId)
    {
      return _asnItemRepo.GetASNItemListByASNId(asnId);
    }

    public IEnumerable<CustBAASNItemDTO> GetASNItemListByASNIdForCust(Guid asnId)
    {
      return _asnItemRepo.GetASNItemListByASNIdForCust(asnId);
    }

    #endregion
  }
}
