using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class is responsible to manage connection and sync data from SAP ERP
  /// </summary>
  /// <seealso cref="ewApps.BusinessEntity.DS.ISyncServiceDS" />
  public class SAPSyncServiceDS : ISyncServiceDS
  {

    #region Local Member 

    BusinessEntityAppSettings _appSetting;
    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initilize constructor
    /// </summary>   
    /// <param name="appSetting"></param>
    public SAPSyncServiceDS(IServiceScopeFactory scopeFactory)
    {
      using (IServiceScope serviceScope = scopeFactory.CreateScope())
      {
        IOptions<BusinessEntityAppSettings> appSetting = serviceScope.ServiceProvider.GetRequiredService<IOptions<BusinessEntityAppSettings>>();
        _appSetting = appSetting.Value;
      }
    }

    #endregion Constructor

    #region Connection

    /// <summary>
    /// Test connection to SAP connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> TestConnectionAsync(ConTestConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {

      // Map Connector Req Model
      ConTestConnectionReqDTO conTestConnection = new ConTestConnectionReqDTO();
      conTestConnection.SAPAccessToken = request.SAPAccessToken;
      conTestConnection.SAPBaseURL = request.SAPBaseURL;
      conTestConnection.SAPDBName = request.SAPDBName;
      conTestConnection.SAPServer = request.SAPServer;

      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "connection/test";
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, conTestConnection, _appSetting.AppName, _appSetting.IdentityServerUrl);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      return true;
    }

    /// <summary>
    /// Establish connection to SAP connector.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> AddConnectionAsync(ConAddConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {

      // Map Connector Req Model
      ConAddConnectionReqDTO conAddConnection = new ConAddConnectionReqDTO();
      conAddConnection.SAPAccessToken = request.SAPAccessToken;
      conAddConnection.SAPBaseURL = request.SAPBaseURL;
      conAddConnection.SAPDBName = request.SAPDBName;
      conAddConnection.SAPServer = request.SAPServer;
      conAddConnection.TenantId = request.TenantId;
      conAddConnection.SAPRefId = "";
      conAddConnection.AppIdentityServerURL = _appSetting.IdentityServerUrl;
      conAddConnection.AppBaseURLName = _appSetting.BusinessConnectorApiUrl;

      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "connection/establish";

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, conAddConnection, _appSetting.AppName, _appSetting.IdentityServerUrl);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
      var response = await httpRequestProcessor.ExecuteAsync<ConAddConnectionResDTO>(requestOptions, false);

      return true;
    }

    /// <summary>
    /// Deletes the connection asynchronous.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="connectorKey">The connector key.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> DeleteConnectionAsync(Guid tenantId, string connectorKey, CancellationToken token = default(CancellationToken))
    {
      return true;
    }

    #endregion Connection

    #region Pull ERP Data

    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitBusinessDataAsync(Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "company";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      // Log Time
      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-Customer: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitCustomerDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "customer/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      // Log Time
      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-Customer: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;

    }

    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitVendorDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "vendor/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      // Log Time
      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-Customer: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;

    }
    /// <summary>
    /// Get itemmaster from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param> 
    /// <param name="token"></param>    
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitItemMasterDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "items/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      // Log Time
      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ItemMaster: Total time taken: {0} in ms", s.ElapsedMilliseconds));


      return response;
    }

    /// <summary>
    /// Get sales order  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitSalesOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesorder/list";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesOrder: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "invoice/ar/list";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ARInvoice: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitInvoiceDataByIdAsync(string ERPInvoiceKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "invoice/ar/" + ERPInvoiceKey;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ARInvoice: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitAPInvoiceDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "invoice/ap/list";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ARInvoice: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get salesorder  from connector and add in local database.
    /// </summary>
    /// <param name="ERPSalesOrderKey"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitSalesOrderDataByIdAsync(string ERPSalesOrderKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesorder/" + ERPSalesOrderKey;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ARInvoice: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitSalesQuotationDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesquotation/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesQuotation: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }
    /// <summary>
    /// Get salesorder  from connector and add in local database.
    /// </summary>
    /// <param name="ERPSalesOrderKey"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitSalesQuotationDataByIdAsync(string ERPSalesQuotationKey, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesorder/" + ERPSalesQuotationKey;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-ARInvoice: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Delivery  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitDeliveryDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "delivery/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-Delivery: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Contract  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>  
    public async Task<ConectorResDTO> GetInitContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "contract/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesQuotation: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get Contract  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>  
    public async Task<ConectorResDTO> GetInitVendorContractDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "vendorcontract/list";

      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesQuotation: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get ASN  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitASNDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "asn/list";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesQuotation: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get item price  from connector.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetItemPriceAsync(PullItemPriceReqDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "items/price";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesQuotation: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    /// <summary>
    /// Get purchase order  from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetInitPurchaseOrderDataAsync(ConTransRequestDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "purchaseorder/list";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesOrder: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    #endregion Pull ERP Data

    #region Get Attachment 

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetItemMasterAttachmentAsync(string itemId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "items/attachments/" + itemId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="salesOrderId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetSalesOrderAttachmentAsync(string salesOrderId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesorder/attachments/" + salesOrderId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get Sales Quotation Attachment by id .
    /// </summary>
    /// <param name="salesQuotationId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetSalesQuotationAttachmentAsync(string salesQuotationId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesquotation/attachments/" + salesQuotationId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get Delivery Attachment by id .
    /// </summary>
    /// <param name="deliveryId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetDeliveryAttachmentAsync(string deliveryId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "delivery/attachments/" + deliveryId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="contractId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetContractAttachmentAsync(string contractId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "contract/attachments/" + contractId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get Sales Order Attachment by id .
    /// </summary>
    /// <param name="asnId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetASNAttachmentAsync(string asnId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "asn/attachments/" + asnId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    /// <summary>
    /// Get invoice Attachment by id .
    /// </summary>
    /// <param name="invoiceId"></param>
    /// <param name="line"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> GetARInvoiceAttachmentAsync(string invoiceId, string line, Guid tenantId, CancellationToken token = default(CancellationToken))
    {
      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "invoice/ar/attachments/" + invoiceId + "/" + line;


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);

      return response;
    }

    #endregion Get Attachment 

    #region Push ERP Data

    /// <summary>
    /// Push sales order  to connector.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesorder";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesOrder: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }
    /// <summary>
    /// Push sales order  to connector.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<ConectorResDTO> PushSalesQuotationDataInERPAsync(BASalesQuotationSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      HttpClient client = new HttpClient();
      string baseUrl = _appSetting.BusinessConnectorApiUrl;
      string reqeustMethod = "salesquotation";


      List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
      headerParameters.Add(new KeyValuePair<string, string>("TenantId", tenantId.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                 RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);


      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);

      Stopwatch s = new Stopwatch();
      s.Restart();
      ConectorResDTO response = await httpRequestProcessor.ExecuteAsync<ConectorResDTO>(requestOptions, false);
      s.Stop();
      Log.Debug(string.Format("BE-Sync-InitDB-SAP-SalesOrder: Total time taken: {0} in ms", s.ElapsedMilliseconds));

      return response;
    }

    #endregion Push ERP Data
  }
}
