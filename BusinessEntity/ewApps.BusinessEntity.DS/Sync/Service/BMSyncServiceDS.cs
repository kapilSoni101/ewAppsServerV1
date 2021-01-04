using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS
{

  public class BMSyncServiceDS : ISyncServiceDS
  {

    public Task<bool> TestConnectionAsync(ConTestConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<bool> AddConnectionAsync(ConAddConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public async Task<bool> DeleteConnectionAsync(Guid tenantId, string connectorKey, CancellationToken token = default(CancellationToken))
    {
      return true;
    }
    public Task<ConectorResDTO> GetInitBusinessDataAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetInitCustomerDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetInitVendorDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitItemMasterDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitSalesOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitAPInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitInvoiceDataByIdAsync(string ERPInvoiceKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitSalesQuotationDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetInitDeliveryDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetInitContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetInitVendorContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitASNDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> GetItemPriceAsync(PullItemPriceReqDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> GetInitPurchaseOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public Task<ConectorResDTO> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public Task<ConectorResDTO> PushSalesQuotationDataInERPAsync(BASalesQuotationSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    public async Task<ConectorResDTO> GetInitSalesOrderDataByIdAsync(string ERPSalesOrderKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }
    public async Task<ConectorResDTO> GetInitSalesQuotationDataByIdAsync(string ERPSalesOrderKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    #region Get Attachment 

    /// <summary>
    /// Get item Master Attachment by id .
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetItemMasterAttachmentAsync(string itemId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="salesOrderId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetSalesOrderAttachmentAsync(string salesOrderId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Get Sales Quotation Attachment by id .
    /// </summary>
    /// <param name="salesQuotationId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetSalesQuotationAttachmentAsync(string salesQuotationId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Get Delivery Attachment by id .
    /// </summary>
    /// <param name="deliveryId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetDeliveryAttachmentAsync(string deliveryId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Get Contract Attachment by id .
    /// </summary>
    /// <param name="contractId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetContractAttachmentAsync(string contractId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Get ASN Attachment by id .
    /// </summary>
    /// <param name="asnId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetASNAttachmentAsync(string asnId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Get invoice Attachment by id .
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ConectorResDTO> GetARInvoiceAttachmentAsync(string invoiceId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      throw new NotImplementedException();
    }

    #endregion Get Attachment 
  }
}
