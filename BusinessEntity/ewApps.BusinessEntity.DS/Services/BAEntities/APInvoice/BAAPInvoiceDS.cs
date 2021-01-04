using System;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.BusinessEntity.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class implements standard business logic and operations for BAAPInvoice entity.
  /// </summary>
  public class BAAPInvoiceDS : BaseDS<BAAPInvoice>, IBAAPInvoiceDS
  {

    IBAAPInvoiceRepository _apInvoiceRepo;
    IBAAPInvoiceItemDS _invoiceItemDS;
    IBAAPInvoiceAttachmentDS _invoiceAttachmentDS;
    IBAVendorDS _vendorDS;
    IUnitOfWork _unitOfWork;
    IBusinessEntityNotificationHandler _businessEntityNotificationHandler;
    IQNotificationDS _qNotificationDS;
    IUniqueIdentityGeneratorDS _identityDataService;
    IDMDocumentDS _documentDS;
    IQBAInvoiceRepository _qBAInvoiceRepository;
    ILogger<BAAPInvoiceDS> _logger;
    IUserSessionManager _sessionManager;


    /// <summary>
    /// Default constructor with APInvoice respository parameter.
    /// </summary>
    /// <param name="apInvoiceRepo"></param>
    /// <param name="invoiceItemDS"></param>
    /// <param name="identityDataService"></param>
    /// <param name="unitOfWork"></param>
    public BAAPInvoiceDS(IQNotificationDS qNotificationDS, IBAAPInvoiceRepository apInvoiceRepo, IBAAPInvoiceItemDS invoiceItemDS, IBAAPInvoiceAttachmentDS invoiceAttachmentDS, IBAVendorDS vendorDS, IUniqueIdentityGeneratorDS identityDataService, IDMDocumentDS documentDS, IQBAInvoiceRepository qBAInvoiceRepository, IUnitOfWork unitOfWork, IBusinessEntityNotificationHandler businessEntityNotificationHandler, IUserSessionManager sessionManager, ILogger<BAAPInvoiceDS> logger) : base(apInvoiceRepo)
    {
      _apInvoiceRepo = apInvoiceRepo;
      _invoiceItemDS = invoiceItemDS;
      _invoiceAttachmentDS = invoiceAttachmentDS;
      _vendorDS = vendorDS;
      _identityDataService = identityDataService;
      _qBAInvoiceRepository = qBAInvoiceRepository;
      _unitOfWork = unitOfWork;
      _documentDS = documentDS;
      _businessEntityNotificationHandler = businessEntityNotificationHandler;
      _qNotificationDS = qNotificationDS;
      _sessionManager = sessionManager;
      _logger = logger;
    }


    #region Add

    ///<inheritdoc/>
    
    public async Task AddAPInvoiceListAsync(List<BAAPInvoiceSyncDTO> invoiceList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken))
    {
      List<string> invoiceAddERPKeyList = new List<string>();
      List<string> invoiceUpdateERPKeyList = new List<string>();
      for (int i = 0; i < invoiceList.Count; i++)
      {
        if (isBulkInsert)
        {
          bool oldInvoice = await AddInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
        }
        else
        {
          if (invoiceList[i].OpType.Equals("Inserted"))
          {
            bool oldInvoice = await AddInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
            if (oldInvoice == true)
            {
              invoiceAddERPKeyList.Add(invoiceList[i].ERPAPInvoiceKey);
            }
          }
          else if (invoiceList[i].OpType.Equals("Modified"))
          {
            invoiceUpdateERPKeyList.Add(invoiceList[i].ERPAPInvoiceKey);
           // await UpdateInvoiceAsync(invoiceList[i], tenantId, tenantUserId);
          }
        }

      }
      //save data
      _unitOfWork.SaveAll();

      //if(isBulkInsert) {
      //    //   OnBulkAddAPInvoiceInIntegratedModeAsync(invoiceERPKeyList, tenantId);
      //}
      //else {
      if (invoiceAddERPKeyList.Count > 0)
      {
        //OnAddAPInvoiceInIntegratedModeAsync(invoiceAddERPKeyList, tenantId);
      }
      if (invoiceUpdateERPKeyList.Count > 0)
      {
        //OnUpdateAPInvoiceInIntegratedModeAsync(invoiceUpdateERPKeyList, tenantId);
      }

      // }
    }

    /// <summary>
    /// add invoice and its child data .
    /// </summary>
    /// <param name="invoiceSyncDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<bool> AddInvoiceAsync(BAAPInvoiceSyncDTO invoiceSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BAAPInvoice invoice = await FindAsync(inv => inv.ERPAPInvoiceKey == invoiceSyncDTO.ERPAPInvoiceKey && inv.TenantId == tenantId);
      if (invoice != null)
      {
        return false;
      }
      BAVendor vendor = await _vendorDS.FindAsync(cust => cust.ERPVendorKey == invoiceSyncDTO.ERPVendorKey && cust.TenantId == tenantId);
      invoiceSyncDTO.VendorId = vendor.ID;
      invoice = BAAPInvoiceSyncDTO.MapToEntity(invoiceSyncDTO);

      // UpdateSystemFieldsByOpType(salesOrder, OperationType.Add);
      invoice.ID = Guid.NewGuid();
      invoice.CreatedBy = tenantUserId;//;// Session
      invoice.UpdatedBy = tenantUserId;
      invoice.CreatedOn = DateTime.UtcNow;
      invoice.UpdatedOn = DateTime.UtcNow;
      invoice.Deleted = false;
      invoice.TenantId = tenantId;

      // add vendor detail
      await AddAsync(invoice, token);

      //Add vendor address detail.
      if (invoiceSyncDTO.InvoiceItemList != null && invoiceSyncDTO.InvoiceItemList.Count > 0)
      {
        await _invoiceItemDS.AddInvoiceItemListAsync(invoiceSyncDTO.InvoiceItemList, tenantId, tenantUserId, invoice.ID);
      }

      //Add vendor address detail.
      if (invoiceSyncDTO.Attachments != null && invoiceSyncDTO.Attachments.Count > 0)
      {
        await _invoiceAttachmentDS.AddAPInvoiceAttachmentListAsync(invoiceSyncDTO.Attachments, tenantId, tenantUserId, invoice.ID);
      }


      return true;

    }

    #endregion Add



  }

}