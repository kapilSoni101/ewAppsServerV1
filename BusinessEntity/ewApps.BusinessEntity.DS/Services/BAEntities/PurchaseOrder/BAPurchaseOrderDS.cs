/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Amit Mundra<amundra@eworkplaceapps.com>
 * Date:08 july 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// Contains purchase Order required methods to add/update/delete operation.
  /// Also contains method for getting purchase Order.
  /// </summary>
  public class BAPurchaseOrderDS : BaseDS<BAPurchaseOrder>, IBAPurchaseOrderDS
  {

    #region Local variables

    IBAPurchaseOrderRepository _purchaseOrderRepo;
    IBAPurchaseOrderItemDS _purchaseOrderItemDS;
    IBAVendorDS _vendorDS;
    IUniqueIdentityGeneratorDS _identityDataService;
    IDMDocumentDS _documentDS;
    IBAPurchaseOrderAttachmentDS _purchaseOrderAttachmentDS;
    IUnitOfWork _unitOfWork;

    #endregion Local variables

    #region Constructor

    /// <summary>
    /// Pucrahse order.
    /// </summary>
    /// <param name="purchaseOrderRepo">Purchorder repository class to interact with database.</param>
    /// <param name="identityDataService"></param>
    /// <param name="purchaseOrderItemDS">PurchaseOrderItemService.</param>
    /// <param name="purchaseOrderAttachmentDS">PurchaseOrderAttachmentDS</param>
    /// <param name="documentDS"></param>
    /// <param name="unitOfWork"></param>
    public BAPurchaseOrderDS(IBAPurchaseOrderRepository purchaseOrderRepo, IBAPurchaseOrderItemDS purchaseOrderItemDS,
IUniqueIdentityGeneratorDS identityDataService, IBAPurchaseOrderAttachmentDS purchaseOrderAttachmentDS,
IDMDocumentDS documentDS, IBAVendorDS vendorDS, IUnitOfWork unitOfWork) : base(purchaseOrderRepo)
    {
      _purchaseOrderRepo = purchaseOrderRepo;
      _purchaseOrderItemDS = purchaseOrderItemDS;
      _identityDataService = identityDataService;
      _purchaseOrderAttachmentDS = purchaseOrderAttachmentDS;
      _documentDS = documentDS;
      _unitOfWork = unitOfWork;
      _vendorDS = vendorDS;
    }

    #endregion Constructor

    #region Public Methods

    /// <summary>
    /// Get purchase order list by tenantid.
    /// </summary>
    /// <param name="listDateFilterDTO">filter object</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken))
    {
      return await _purchaseOrderRepo.GetPurchaseOrderListByBusinessTenantIdAsync(listDateFilterDTO.ID, listDateFilterDTO);
    }

    /// <summary>
    /// Get purchase order detail by purcheid and tenant id.
    /// </summary>
    /// <param name="businessTenantId">Business tenantid</param>
    /// <param name="poId">purchase order id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync(Guid businessTenantId, Guid poId, CancellationToken cancellationToken = default(CancellationToken))
    {
      BAPurchaseOrderViewDTO poOrderDTO = await _purchaseOrderRepo.GetPurcahseDetailByPOIdAsync(businessTenantId, poId, cancellationToken);
      if (poOrderDTO != null)
      {
        poOrderDTO.PurchaseOrderItemList = await _purchaseOrderRepo.GetPurchaseOrderItemListByPOIdAsync(poId, cancellationToken);
      }
      return poOrderDTO;
    }

    /// <summary>
    /// Get purchase order attachment list.
    /// </summary>
    /// <param name="pOrderId">Purchase orderid.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _purchaseOrderAttachmentDS.GetPurchaseOrderAttachmentListByOrderIdAsync(pOrderId, cancellationToken);
    }

    #endregion Public Methods

    #region CRUD

    #region Add

    ///<inheritdoc/>
    public async Task AddPurchaseOrderListAsync(List<BAPurchaseOrderSyncDTO> purchaseOrderList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken))
    {
      List<string> purchaseAddERPKeyList = new List<string>();
      List<string> purchaseUpdateERPKeyList = new List<string>();
      for (int i = 0; i < purchaseOrderList.Count; i++)
      {
        if (isBulkInsert)
        {
          await AddPurchaseOrderAsync(purchaseOrderList[i], tenantId, tenantUserId);
        }
        else
        {

          if (purchaseOrderList[i].OpType.Equals("Inserted"))
          {
            bool oldData = await AddPurchaseOrderAsync(purchaseOrderList[i], tenantId, tenantUserId);
            if (oldData)
            {
              purchaseAddERPKeyList.Add(purchaseOrderList[i].ERPPurchaseOrderKey);
            }
          }
          else if (purchaseOrderList[i].OpType.Equals("Modified"))
          {
            purchaseUpdateERPKeyList.Add(purchaseOrderList[i].ERPPurchaseOrderKey);
            //await UpdatePurchaseOrderAsync(purchaseOrderList[i], tenantId, tenantUserId);
          }
        }
      }
      //save data
      _unitOfWork.SaveAll();

      if (purchaseAddERPKeyList.Count > 0)
      {
        //await OnAddSOInIntegratedModeAsync(purchaseAddERPKeyList, tenantId, token);
      }
      if (purchaseUpdateERPKeyList.Count > 0)
      {
        // Add Update notification method
      }


    }

    /// <summary>
    /// add Purchase order and its child data .
    /// </summary>
    /// <param name="PurchaseOrderSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> AddPurchaseOrderAsync(BAPurchaseOrderSyncDTO PurchaseOrderSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAPurchaseOrder PurchaseOrder = await FindAsync(so => so.ERPPurchaseOrderKey == PurchaseOrderSyncDTO.ERPPurchaseOrderKey && so.TenantId == tenantId);
      if (PurchaseOrder != null)
      {
        return false;
      }
      BAVendor Vendor = await _vendorDS.FindAsync(vend => vend.ERPVendorKey == PurchaseOrderSyncDTO.ERPVendorKey && vend.TenantId == tenantId);
      PurchaseOrderSyncDTO.VendorId = Vendor.ID;
      PurchaseOrder = BAPurchaseOrderSyncDTO.MapToEntity(PurchaseOrderSyncDTO);

      // UpdateSystemFieldsByOpType(PurchaseOrder, OperationType.Add);

      PurchaseOrder.ID = Guid.NewGuid();
      PurchaseOrder.CreatedBy = tenantUserId;//;// Session
      PurchaseOrder.UpdatedBy = tenantUserId;
      PurchaseOrder.CreatedOn = DateTime.UtcNow;
      PurchaseOrder.UpdatedOn = DateTime.UtcNow;
      PurchaseOrder.Deleted = false;
      PurchaseOrder.TenantId = tenantId;

      // add Vendor detail
      await AddAsync(PurchaseOrder, token);

      //Add Vendor address detail.
      if (PurchaseOrderSyncDTO.ItemList != null && PurchaseOrderSyncDTO.ItemList.Count > 0)
      {
        await _purchaseOrderItemDS.AddPurchaseOrderItemListAsync(PurchaseOrderSyncDTO.ItemList, tenantId, tenantUserId, PurchaseOrder.ID);
      }

      //Add Vendor address detail.
      if (PurchaseOrderSyncDTO.Attachments != null && PurchaseOrderSyncDTO.Attachments.Count > 0)
      {
        await _purchaseOrderAttachmentDS.AddPurchaseOrderAttachmentListAsync(PurchaseOrderSyncDTO.Attachments, tenantId, tenantUserId, PurchaseOrder.ID);
      }


      return true;

    }

    /// <summary>
    /// add Purchase order and its child data .
    /// </summary>
    /// <param name="PurchaseOrderSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> UpdatePurchaseOrderAsync(BAPurchaseOrderSyncDTO PurchaseOrderSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAPurchaseOrder PurchaseOrder = await FindAsync(so => so.ERPPurchaseOrderKey == PurchaseOrderSyncDTO.ERPPurchaseOrderKey && so.TenantId == tenantId);

      if (PurchaseOrder != null)
      {
        PurchaseOrder = BAPurchaseOrderSyncDTO.MapToEntity(PurchaseOrderSyncDTO, PurchaseOrder);


        UpdateSystemFieldsByOpType(PurchaseOrder, OperationType.Update);

        // add Vendor detail
        await UpdateAsync(PurchaseOrder, PurchaseOrder.ID, token);

        //Add Vendor address detail.
        if (PurchaseOrderSyncDTO.ItemList != null && PurchaseOrderSyncDTO.ItemList.Count > 0)
        {
          await _purchaseOrderItemDS.AddPurchaseOrderItemListAsync(PurchaseOrderSyncDTO.ItemList, tenantId, tenantUserId, PurchaseOrder.ID);
        }

        //Add Vendor address detail.
        if (PurchaseOrderSyncDTO.Attachments != null && PurchaseOrderSyncDTO.Attachments.Count > 0)
        {
          await _purchaseOrderAttachmentDS.AddPurchaseOrderAttachmentListAsync(PurchaseOrderSyncDTO.Attachments, tenantId, tenantUserId, PurchaseOrder.ID);
        }

      }
      return true;

    }

    #endregion Add

    /// <summary>
    /// Add purchaseOrder, order items and attachments.
    /// </summary>
    /// <param name="httpRequest">Contains purchase order attachment</param>
    /// <param name="request">AddBAPurchaseOrderDTO model in json string.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<AddPurchaseOrderResponseDTO> AddPurchaseOrder(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken))
    {
      AddBAPurchaseOrderDTO addPurchaseOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<AddBAPurchaseOrderDTO>(request);
      BAPurchaseOrder purchaseOrder = new BAPurchaseOrder();
      addPurchaseOrder.MapPropertiesToEntity(purchaseOrder);
      int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAPurchaseOrder, "PO", 1000);
      purchaseOrder.ERPPurchaseOrderKey = BusinessEntityConstants.PurchaseOrderPrefix + identity;


      UpdateSystemFieldsByOpType(purchaseOrder, OperationType.Add);
      await _purchaseOrderRepo.AddAsync(purchaseOrder, token);
      // Add purchase order items.
      await _purchaseOrderItemDS.AddPurchaseOrderItemLiast(addPurchaseOrder.OrderItemList, purchaseOrder, false, token);

      // Add attachment.
      if (addPurchaseOrder.listAttachment != null && addPurchaseOrder.listAttachment.Count > 0)
      {
        int i = 0;
        Guid storageId = Guid.Empty;
        foreach (IFormFile file in httpRequest.Form.Files)
        {
          addPurchaseOrder.listAttachment[i].DocumentId = Guid.NewGuid();
          _documentDS.UploadDocumentFileToStorage(file.OpenReadStream(), addPurchaseOrder.listAttachment[i], true);
          await AddPurchaseOrderAttachmentAsync(addPurchaseOrder.listAttachment[i].DocumentId, purchaseOrder, file, token);
          i = i + 1;
        }
      }

      _unitOfWork.Save();

      return GeneratePurchaseOrderResponseDTO(purchaseOrder);
    }

    private AddPurchaseOrderResponseDTO GeneratePurchaseOrderResponseDTO(BAPurchaseOrder entity)
    {
      AddPurchaseOrderResponseDTO resDto = new AddPurchaseOrderResponseDTO();
      resDto.PurchaseOrderId = entity.ID;
      resDto.VendorId = entity.VendorId;
      resDto.ERPPurchaseOrderKey = entity.ERPPurchaseOrderKey;
      resDto.PurchaseOrderEntityType = (int)EntityTypeEnum.BAPurchaseOrder;

      return resDto;
    }

    /// <summary>
    /// Add purchase ordr attachment.
    /// </summary>
    /// <param name="attachmentId"></param>
    /// <param name="purchaseOrder"></param>
    /// <param name="file"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddPurchaseOrderAttachmentAsync(Guid attachmentId, BAPurchaseOrder purchaseOrder, IFormFile file, CancellationToken token)
    {
      int identity = _identityDataService.GetIdentityNo(Guid.Empty, (int)EntityTypeEnum.BAPurchaseOrder, "POATT", 1000);

      BAPurchaseOrderAttachment attachment = new BAPurchaseOrderAttachment();
      attachment.ID = attachmentId;
      attachment.PurchaseOrderId = purchaseOrder.ID;
      attachment.Name = file.FileName;
      attachment.ERPPurchaseOrderAttachmentKey = "POATT" + identity;
      attachment.ERPPurchaseOrderKey = purchaseOrder.ERPPurchaseOrderKey;
      attachment.TenantId = purchaseOrder.TenantId;
      attachment.CreatedBy = purchaseOrder.CreatedBy;
      attachment.UpdatedBy = purchaseOrder.UpdatedBy;
      attachment.CreatedOn = purchaseOrder.UpdatedOn;
      attachment.UpdatedOn = purchaseOrder.UpdatedOn;
      attachment.AttachmentDate = DateTime.UtcNow;
      await _purchaseOrderAttachmentDS.AddAsync(attachment, token);
    }

    #endregion CRUD
  }
}

