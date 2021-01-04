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
  public interface IAppSyncServiceDS
  {

    /// <summary>
    /// Test connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> TestConnectionAsync(BATestConnectionReqDTO request, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Deletes the connection asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="connectorKey">The connector key.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> DeleteConnectionAsync(Guid tenantId, string connectorKey, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Establish connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> AddUpdateConnectionAsync(BAAddConnectionReqDTO request, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<bool> PullERPDataAsync(PullERPDataReqDTO request, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Adding/updating/deleting configuration for business.
    /// </summary>                
    /// <param name="connectorConfigDTO">Incoming connetor request is coming for add/update/delete.</param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    Task ManageConnectorConfigsAsync(List<ERPConnectorConfigDTO> connectorConfigDTO, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<AttachmentResDTO> GetAttachmentFromERPAsync(AttachmentReqDTO request, CancellationToken token = default(CancellationToken));

    Task<BASyncItemPriceDTO> PullItemPriceAsync(PullItemPriceReqDTO request, CancellationToken token = default(CancellationToken));

    Task<bool> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));

    Task<bool> PushSalesQuotationDataInERPAsync(BASalesQuotationSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken));
    Task<bool> PostERPCustomerDataAsync(List<BACustomerSyncDTO> request, Guid tenantId, CancellationToken token = default(CancellationToken));


    Task<bool> NotifyApplicationAsync(NotifyAppDTO notifyDTO, CancellationToken token = default(CancellationToken));

    Task<bool> PullERPVendorDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken));
    Task<bool> PullERPPurchaseOrderDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken));


  }
}
