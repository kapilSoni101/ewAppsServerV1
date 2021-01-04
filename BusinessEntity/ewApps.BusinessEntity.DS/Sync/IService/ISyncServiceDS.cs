using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// Contains all required all methods which comunicate to v1 connector.
  /// </summary>
  public interface ISyncServiceDS
  {

    /// <summary>
    /// Test connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> TestConnectionAsync(ConTestConnectionReqDTO request, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Establish connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> AddConnectionAsync(ConAddConnectionReqDTO request, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Deletes the connection asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="connectorKey">The connector key.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> DeleteConnectionAsync(Guid tenantId, string connectorKey, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitBusinessDataAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitCustomerDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<ConectorResDTO> GetInitVendorDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<ConectorResDTO> GetInitInvoiceDataByIdAsync(string ERPInvoiceKey, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitItemMasterDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitSalesOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitAPInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitSalesQuotationDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitDeliveryDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitVendorContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetInitASNDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<ConectorResDTO> GetItemPriceAsync(PullItemPriceReqDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));


    Task<ConectorResDTO> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));
    Task<ConectorResDTO> PushSalesQuotationDataInERPAsync(BASalesQuotationSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));


    Task<ConectorResDTO> GetInitSalesOrderDataByIdAsync(string ERPSalesOrderKey, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<ConectorResDTO> GetInitSalesQuotationDataByIdAsync(string ERPSalesOrderKey, Guid tenantId, CancellationToken token = default(CancellationToken));


    Task<ConectorResDTO> GetInitPurchaseOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));
    #region Get Attachment 

    /// <summary>
    /// Get item Master Attachment by id .
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetItemMasterAttachmentAsync(string itemId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="salesOrderId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetSalesOrderAttachmentAsync(string salesOrderId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get Sales Quotation Attachment by id .
    /// </summary>
    /// <param name="salesQuotationId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetSalesQuotationAttachmentAsync(string salesQuotationId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get Delivery Attachment by id .
    /// </summary>
    /// <param name="deliveryId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetDeliveryAttachmentAsync(string deliveryId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get Contract Attachment by id .
    /// </summary>
    /// <param name="contractId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetContractAttachmentAsync(string contractId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get ASN Attachment by id .
    /// </summary>
    /// <param name="asnId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetASNAttachmentAsync(string asnId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get invoice Attachment by id .
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<ConectorResDTO> GetARInvoiceAttachmentAsync(string invoiceId, string line, Guid tenantId, CancellationToken token = default(CancellationToken));

    #endregion Get Attachment 
  }
}
