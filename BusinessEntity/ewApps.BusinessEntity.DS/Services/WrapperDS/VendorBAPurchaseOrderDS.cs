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

namespace ewApps.BusinessEntity.DS {
  public class VendorBAPurchaseOrderDS : BaseDS<BAPurchaseOrder>, IVendBAPurchaseOrderDS
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
    public VendorBAPurchaseOrderDS(IBAPurchaseOrderRepository purchaseOrderRepo, IBAPurchaseOrderItemDS purchaseOrderItemDS,
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
    /// <inheritdoc/>
    public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsyncForVend(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _purchaseOrderRepo.GetPurchaseOrderListByBusinessTenantIdForVendAsync(businessTenantId, listDateFilterDTO, cancellationToken);
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

    #endregion 
  }
}
