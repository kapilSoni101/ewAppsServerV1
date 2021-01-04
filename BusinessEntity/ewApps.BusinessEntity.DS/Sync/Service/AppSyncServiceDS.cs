/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 20 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DMService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace ewApps.BusinessEntity.DS
{

  /// <summary>
  /// This class implements standard business logic and operations for Sync BA entity data from connectors to local database.
  /// </summary>
  public class AppSyncServiceDS : IAppSyncServiceDS
  {

    #region Local Member 

    IBAItemMasterDS _itemMasterDS;
    ICustomerSignupDS _customerSignupDS;
    IBAVendorDS _vendorDS;
    IBASalesOrderDS _salesOrderDS;
    IBAPurchaseOrderDS _purchaseOrderDS;
    IBAARInvoiceDS _invoiceDS;
    IBAAPInvoiceDS _apInvoiceDS;
    IBASalesQuotationDS _salesQuotationDS;
    IBADeliveryDS _deliveryDS;
    IBAContractDS _contractDS;
    IBAVendorContractDS _vendContractDS;
    IBAASNDS _asnDS;
    IAppMgmtDS _appMgmtDS;
    IAppPortalDS _appPortalDS;
    IERPConfigDS _eRPConfigDS;
    BusinessEntityAppSettings _appSetting;
    ISyncServiceFactory _syncServiceFactory;
    string _connectorKey = "SAP";
    IUnitOfWork _unitOfWork;
    ISyncTimeLogDS _syncTimeLogDS;
    IUserSessionManager _userSessionManager;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initilize constructor
    /// </summary>
    /// <param name="itemMasterDS"></param>
    /// <param name="customerSignupDS"></param>
    /// <param name="salesOrderDS"></param>
    /// <param name="invoiceDS"></param>
    /// <param name="salesQuotationDS"></param>
    /// <param name="deliveryDS"></param>
    /// <param name="contractDS"></param>
    /// <param name="eRPConfigDS"></param>
    /// <param name="appMgmtDS"></param> 
    /// <param name="appSetting"></param>
    /// <param name="syncServiceFactory"></param>
    public AppSyncServiceDS(IBAItemMasterDS itemMasterDS, ICustomerSignupDS customerSignupDS, IBAVendorDS vendorDS, IBASalesOrderDS salesOrderDS, IBAARInvoiceDS invoiceDS, IBAAPInvoiceDS apInvoiceDS,
                     IBASalesQuotationDS salesQuotationDS, IBADeliveryDS deliveryDS, IBAContractDS contractDS, IBAASNDS asnDS, IBAPurchaseOrderDS purchaseOrderDS, IBAVendorContractDS vendContractDS,IAppMgmtDS appMgmtDS, IAppPortalDS appPortalDS, IERPConfigDS eRPConfigDS,
                    IOptions<BusinessEntityAppSettings> appSetting, ISyncServiceFactory syncServiceFactory, IUnitOfWork unitOfWork, ISyncTimeLogDS syncTimeLogDS, IUserSessionManager userSessionManager)
    {

      _itemMasterDS = itemMasterDS;
      _customerSignupDS = customerSignupDS;
      _vendorDS = vendorDS;
            _salesOrderDS = salesOrderDS;
      _invoiceDS = invoiceDS;
      _apInvoiceDS = apInvoiceDS;
            _salesQuotationDS = salesQuotationDS;
      _deliveryDS = deliveryDS;
      _contractDS = contractDS;
      _asnDS = asnDS;
      _purchaseOrderDS = purchaseOrderDS;
      _vendContractDS = vendContractDS;
                  _appMgmtDS = appMgmtDS;
      _appPortalDS = appPortalDS;
      _eRPConfigDS = eRPConfigDS;
      _appSetting = appSetting.Value;
      _syncServiceFactory = syncServiceFactory;
      _unitOfWork = unitOfWork;
      _syncTimeLogDS = syncTimeLogDS;
      _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Manage Connection Config
    //ToDo: nitin-Logic should be based on operation type.
    public async Task ManageConnectorConfigsAsync(List<ERPConnectorConfigDTO> connectorConfigDTO, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      //Getting exiting connector config for tenant.
      List<ERPConnectorConfigDQ> existingAppConnectorConfig = await _eRPConfigDS.GetBusinessAppConnectorConfigByBusinessIdAsync(tenantId, token);

      // Add/Update config
      if (connectorConfigDTO != null)
      {
        ERPConnectorConfig connectorConfig;
        for (int i = 0; i < connectorConfigDTO.Count; i++)
        {
          ERPConnectorConfigDQ exist = null;

          if (existingAppConnectorConfig != null)
            exist = existingAppConnectorConfig.Find(ex => ex.ConnectorKey == connectorConfigDTO[i].ConnectorKey);

          bool isAdd = exist == null || string.IsNullOrEmpty(exist.ConnectorKey);

          connectorConfig = new ERPConnectorConfig();
          connectorConfig.SettingJson = connectorConfigDTO[i].Json;
          connectorConfig.TenantId = tenantId;
          connectorConfig.ConnectorKey = connectorConfigDTO[i].ConnectorKey;
          connectorConfig.Active = true;
          connectorConfig.Status = connectorConfigDTO[i].Status;
          connectorConfig.Message = connectorConfigDTO[i].Message;

          // Adding the config.
          if (isAdd)
          {
            _eRPConfigDS.UpdateSystemFieldsByOpType(connectorConfig, OperationType.Add);
            await _eRPConfigDS.AddAsync(connectorConfig, token);
          }
          // updating config.
          else
          {
            connectorConfig.ID = exist.ID;
            _eRPConfigDS.UpdateSystemFieldsByOpType(connectorConfig, OperationType.Update);
            await _eRPConfigDS.UpdateAsync(connectorConfig, connectorConfig.ID, token);
          }

          // Add/Update Connection In Connector
          BAAddConnectionReqDTO baAddConnectionReqDTO = new BAAddConnectionReqDTO();
          if (connectorConfigDTO[i].ConnectorKey == "SAP")
          {
            baAddConnectionReqDTO = JsonConvert.DeserializeObject<BAAddConnectionReqDTO>(connectorConfigDTO[i].Json);
            baAddConnectionReqDTO.TenantId = tenantId.ToString();
            await AddUpdateConnectionAsync(baAddConnectionReqDTO);
          }
        }
      }

      // Deleting the config.
      if (existingAppConnectorConfig != null)
      {

        for (int j = 0; j < existingAppConnectorConfig.Count; j++)
        {
          ERPConnectorConfigDTO delete = null;
          if (connectorConfigDTO != null)
          {
            delete = connectorConfigDTO.Find(data => data.ConnectorKey == existingAppConnectorConfig[j].ConnectorKey);
          }

          if (delete == null)
          {
            ERPConnectorConfig config = new ERPConnectorConfig();
            config.ID = existingAppConnectorConfig[j].ID;
            await _eRPConfigDS.DeleteAsync(config.ID, token);

            // Delete Connector
            await DeleteConnectionAsync(tenantId, existingAppConnectorConfig[j].ConnectorKey);
          }
        }
      }
      //ToDo: nitin- review saveall method.
      _unitOfWork.SaveAll();
    }

    #endregion Manage Connection Config


    #region Test/Add Connection

    /// <summary>
    /// Test connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> TestConnectionAsync(BATestConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {
      // TODO: Get connetion key from ERPConnectorConf by tenantId          
      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(request.ConnectorKey);
      ConTestConnectionReqDTO conRequest = new ConTestConnectionReqDTO();
      conRequest.SAPAccessToken = request.SAPAccessToken;
      conRequest.SAPServer = request.SAPServer;
      conRequest.SAPBaseURL = request.SAPBaseURL;
      conRequest.SAPDBName = request.SAPDBName;
      var response = await syncServiceDS.TestConnectionAsync(conRequest);
      // TODO: Add Entry in SyncHistory
      return true;
    }

    /// <summary>
    /// Establish connection to V1 connector.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> AddUpdateConnectionAsync(BAAddConnectionReqDTO request, CancellationToken token = default(CancellationToken))
    {

      // Get Sync Service DS Object by Connector Key
      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(request.ConnectorKey);

      // Create Request Model
      ConAddConnectionReqDTO conRequest = new ConAddConnectionReqDTO();
      conRequest.SAPRefId = "";
      conRequest.SAPAccessToken = request.SAPAccessToken;
      conRequest.SAPServer = request.SAPServer;
      conRequest.SAPBaseURL = request.SAPBaseURL;
      conRequest.SAPDBName = request.SAPDBName;
      conRequest.AppIdentityServerURL = _appSetting.IdentityServerUrl;
      conRequest.TenantId = request.TenantId;
      conRequest.Active = true;

      // Call Connecotr AddConnection Method 
      var response = await syncServiceDS.AddConnectionAsync(conRequest);

      // TODO: Add Entry in SyncHistory
      return true;
    }


    public async Task<bool> DeleteConnectionAsync(Guid tenantId, string connectorKey, CancellationToken token = default(CancellationToken))
    {
      return true;
    }

    #endregion Test/Add Connection


    #region Pull ERP Data

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> PullERPDataAsync(PullERPDataReqDTO request, CancellationToken token = default(CancellationToken))
    {


      // Validate tenant is register for ERP    
      List<string> subscribedAppKeys = await _appPortalDS.GetTenantSuscribedApplicationKeyAsync(request.TenantId, token);
      //List<string> subscribedAppKeys = new List<string>() { AppKeyEnum.pay.ToString(), AppKeyEnum.cust.ToString(), AppKeyEnum.ship.ToString() };

      // Get primary tenant user 
      Guid tenantUserId = await _appMgmtDS.GetTenantPrimaryUserAsync(request.TenantId, token);

      // Init Customer Data
      bool pullCustomer = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.Customer))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.pay.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));

      if (pullCustomer)
      {
        //await PullERPAPInvoiceDataAsync(request, tenantUserId, token);
        await PullERPCustomerDataAsync(request, tenantUserId, token);
      }

      // Init Customer Data
      bool pullVendor = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.Vendor))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.vend.ToString()));

      if (pullVendor)
      {
        await PullERPVendorDataAsync(request, tenantUserId, token);
      }

      // Init ItemMaster Data
      bool pullItemMaster = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.ItemMaster))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.pay.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));
      if (pullItemMaster)
      {
        await PullERPItemMasterDataAsync(request, tenantUserId, token);
      }

      // Init SalesOrder Data
      bool pullSalesOrder = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.SalesOrder))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));
      if (pullSalesOrder)
      {
        //Task t3 = InitSalesOrderDataAsync(tenantId, tenantUserId, token);
        await PullERPSalesOrderDataAsync(request, tenantUserId, token);
      }

      //Init PurchaseOrder Data
      bool pullPurchaseOrder = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.PurchaseOrder))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.vend.ToString()));
      if (pullPurchaseOrder)
      {
        //Task t3 = InitSalesOrderDataAsync(tenantId, tenantUserId, token);
        await PullERPPurchaseOrderDataAsync(request, tenantUserId, token);
      }


      // Init ARInvoice Data
      bool pullARInvoice = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.ARInvoice))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.pay.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString()));
      if (pullARInvoice)
      {
        //Task t4 = InitInvoiceDataAsync(tenantId, tenantUserId, token);
        await PullERPARInvoiceDataAsync(request, tenantUserId, token);
      }

      // Init ARInvoice Data
      bool pullAPInvoice = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.APInvoice))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.vend.ToString()));
      if (pullAPInvoice)
      {
        //Task t4 = InitInvoiceDataAsync(tenantId, tenantUserId, token);
        await PullERPAPInvoiceDataAsync(request, tenantUserId, token);
      }

      // Init SalesQuotation Data
      bool pullSalesQuotation = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.SalesQuotation))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));
      if (pullSalesQuotation)
      {
        //Task t5 = InitSalesQuotationDataAsync(tenantId, tenantUserId, token);
        await PullERPSalesQuotationDataAsync(request, tenantUserId, token);
      }

      // Init Delivery Data
      bool pullDelivery = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.Delivery))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));

      if (pullDelivery)
      {
        //Task t6 = InitDeliveryDataAsync(tenantId, tenantUserId, token);
        await PullERPDeliveryDataAsync(request, tenantUserId, token);
      }
      // Init Delivery Data
      bool pullContract = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.Contract))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));

      if (pullContract)
      {
        //Task t6 = InitDeliveryDataAsync(tenantId, tenantUserId, token);
        await PullERPContractDataAsync(request, tenantUserId, token);
      }
      // Init VendorContract Data
      bool pullVendorContract = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.VendorContract))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.vend.ToString()));
      if (pullVendorContract)
      {
        //Task t4 = InitInvoiceDataAsync(tenantId, tenantUserId, token);
        await PullERPAPInvoiceDataAsync(request, tenantUserId, token);
      }

      // Init Delivery Data
      bool pullASN = (request.SelectedBAEntityTypes.Count == 0 || request.SelectedBAEntityTypes.Exists(s => s == (int)BAEntityEnum.ASN))
                          && (subscribedAppKeys.Exists(s => s == AppKeyEnum.cust.ToString())
                               || subscribedAppKeys.Exists(s => s == AppKeyEnum.ship.ToString()));

      if (pullASN)
      {
        //Task t6 = InitDeliveryDataAsync(tenantId, tenantUserId, token);
        await PullERPASNDataAsync(request, tenantUserId, token);
      }

      //Task[] tasks = { t3, t4,t5,t6 };
      //Task.WaitAll(tasks);

      // Init Purchase Order Data 
      return true;
    }

    /// <summary>
    /// Get Busines from connector and add in local database.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullBusinessDataAsync(Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      BusinessSyncDTO business = null;
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == tenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);
      ConectorResDTO response = await syncServiceDS.GetInitBusinessDataAsync(tenantId, token);
      business = JsonConvert.DeserializeObject<BusinessSyncDTO>(response.ResponsePayload.ToString());
      //int lastValue = customerList.Count - 1;
      if (response.ResponseStatus == "Success")
      {
        // SingUp Customer On App Portal
        try
        {
          string baseUrl = _appSetting.AppPortalApiUrl;
          string relativeUrl = "business/syncerp";

          UserSession session = _userSessionManager.GetSession();

          List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
          headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

          RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, relativeUrl, business, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParameters);
          ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
          var response1 = await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }
        catch (Exception ex)
        {
          StringBuilder exceptionDetail = new StringBuilder();
          exceptionDetail.Append("Exception occurred in InitDB-CustomerSignUpDS:-");
          exceptionDetail.AppendLine();
          exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
          Log.Error(ex, exceptionDetail.ToString());
          // TODO: Logo Error
          // TODO: Rollback transaction
          throw;
        }


        //TODO: Add In SyncTimeLog (1, Success)
      }
      return true;
      //TODO: Add In SyncHistory
    }

    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPCustomerDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BACustomerSyncDTO> customerList = null;
      bool isBulkInsert = false;

      // Get SyncTieLog for SalesQuotation
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.Customer.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId  
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitCustomerDataAsync(conTransRequest, request.TenantId, token);

      customerList = JsonConvert.DeserializeObject<List<BACustomerSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = customerList.Count - 1;
      if (response.ResponseStatus == "Success")
      {
        await _customerSignupDS.AddCustomerListAsync(customerList, request.TenantId, tenantUserId, isBulkInsert, token);

        if (customerList != null && customerList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, customerList[lastValue].ERPConnectorKey, "Customer", customerList[lastValue].ERPCustomerKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;

        //TODO: Add In SyncTimeLog (1, Success)
      }
      else
      {
        //TODO: Add In SyncTimeLog (2, Failed)
        if (customerList != null && customerList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, customerList[lastValue].ERPConnectorKey, "Customer", customerList[lastValue].ERPCustomerKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        // Log Error in SyncHistory
        return false;
      }
      //TODO: Add In SyncHistory
    }
    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPVendorDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAVendorSyncDTO> vendorList = null;
      bool isBulkInsert = false;

      // Get SyncTieLog for SalesQuotation
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.Vendor.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId  
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitVendorDataAsync (conTransRequest, request.TenantId, token);

      vendorList = JsonConvert.DeserializeObject<List<BAVendorSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = vendorList.Count - 1;
      if (response.ResponseStatus == "Success")
      {
        await _vendorDS.AddVendorListAsync(vendorList, request.TenantId, tenantUserId, isBulkInsert, token);

        if (vendorList != null && vendorList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, vendorList[lastValue].ERPConnectorKey, "Vendor", vendorList[lastValue].ERPVendorKey, "", (int)SyncResponseEnum.Success, token);


          //TODO: Add In SyncTimeLog (1, Success)
        }
        return true;
      }
      else
      {
        //TODO: Add In SyncTimeLog (2, Failed)
        if (vendorList != null && vendorList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, vendorList[lastValue].ERPConnectorKey, "Vendor", vendorList[lastValue].ERPVendorKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        // Log Error in SyncHistory
        return false;
      }
        //TODO: Add In SyncHistory
      }
    
    
    /// <summary>
    /// Get itemmaster from connector and add in local database.
    /// </summary>
    /// <param name="request"></param> 
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPItemMasterDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAItemMasterSyncDTO> itemMasterList = null;
      bool isBulkInsert = false;

      // Get SyncTieLog for SalesQuotation
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.ItemMaster.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitItemMasterDataAsync(conTransRequest, request.TenantId, token);
      itemMasterList = JsonConvert.DeserializeObject<List<BAItemMasterSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = itemMasterList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _itemMasterDS.AdditemMasterListAsync(itemMasterList, request.TenantId, tenantUserId, isBulkInsert, token);

        if (itemMasterList != null && itemMasterList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, itemMasterList[lastValue].ERPConnectorKey, "ItemMaster", itemMasterList[lastValue].ERPItemKey, "", (int)SyncResponseEnum.Success, token);
        }

        return true;
        //TODO: Add In SyncTimeLog
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (itemMasterList != null && itemMasterList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, itemMasterList[lastValue].ERPConnectorKey, "ItemMaster", itemMasterList[lastValue].ERPItemKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
      //TODO: Add In SyncHistory     
    }

    /// <summary>
    /// Get sales order  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPSalesOrderDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BASalesOrderSyncDTO> salesOrderList = null;
      bool isBulkInsert = false;
      ConectorResDTO response = null;
      // Get SyncTieLog for Salesorder
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.SalesOrder.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }


      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      if (request.ERPEntityKey != null && request.ERPEntityKey.Count > 0)
      {
        salesOrderList = new List<BASalesOrderSyncDTO>();
        for (int i = 0; i < request.ERPEntityKey.Count; i++)
        {
          response = await syncServiceDS.GetInitSalesOrderDataByIdAsync(request.ERPEntityKey[i], request.TenantId, token);
          BASalesOrderSyncDTO salesOrder = JsonConvert.DeserializeObject<BASalesOrderSyncDTO>(response.ResponsePayload.ToString());
          salesOrderList.Add(salesOrder);
        }

      }
      else
      {
        response = await syncServiceDS.GetInitSalesOrderDataAsync(conTransRequest, request.TenantId, token);

        salesOrderList = JsonConvert.DeserializeObject<List<BASalesOrderSyncDTO>>(response.ResponsePayload.ToString());
      }

      int lastValue = salesOrderList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _salesOrderDS.AddSalesOrderListAsync(salesOrderList, request.TenantId, tenantUserId, isBulkInsert, token);

        //TODO: Add In SyncTimeLog
        if (salesOrderList != null && salesOrderList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, salesOrderList[lastValue].ERPConnectorKey, "SalesOrder", salesOrderList[lastValue].ERPSalesOrderKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (salesOrderList != null && salesOrderList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, salesOrderList[lastValue].ERPConnectorKey, "SalesOrder", salesOrderList[lastValue].ERPSalesOrderKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get sales order  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPPurchaseOrderDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAPurchaseOrderSyncDTO> purchaseOrderList = null;
      bool isBulkInsert = false;
      ConectorResDTO response = null;
      // Get SyncTieLog for Salesorder
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.PurchaseOrder.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }


      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      if (request.ERPEntityKey != null && request.ERPEntityKey.Count > 0)
      {
        return true;
      }
      else
      {
        response = await syncServiceDS.GetInitPurchaseOrderDataAsync(conTransRequest, request.TenantId, token);

        purchaseOrderList = JsonConvert.DeserializeObject<List<BAPurchaseOrderSyncDTO>>(response.ResponsePayload.ToString());
      }

      int lastValue = purchaseOrderList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _purchaseOrderDS.AddPurchaseOrderListAsync(purchaseOrderList, request.TenantId, tenantUserId, isBulkInsert, token);

        //TODO: Add In SyncTimeLog
        if (purchaseOrderList != null && purchaseOrderList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, purchaseOrderList[lastValue].ERPConnectorKey, "PurchaseOrder", purchaseOrderList[lastValue].ERPPurchaseOrderKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (purchaseOrderList != null && purchaseOrderList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, purchaseOrderList[lastValue].ERPConnectorKey, "PurchaseOrder", purchaseOrderList[lastValue].ERPPurchaseOrderKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPARInvoiceDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAARInvoiceSyncDTO> invoiceList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for ARInvoice
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.ARInvoice.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);
      ConectorResDTO response = null;
      if (request.ERPEntityKey != null && request.ERPEntityKey.Count > 0)
      {
        invoiceList = new List<BAARInvoiceSyncDTO>();
        for (int i = 0; i < request.ERPEntityKey.Count; i++)
        {
          response = await syncServiceDS.GetInitInvoiceDataByIdAsync(request.ERPEntityKey[i], request.TenantId, token);
          BAARInvoiceSyncDTO invoice = JsonConvert.DeserializeObject<BAARInvoiceSyncDTO>(response.ResponsePayload.ToString());
          invoiceList.Add(invoice);
        }
       
      }
      else
      {
        response = await syncServiceDS.GetInitInvoiceDataAsync(conTransRequest, request.TenantId, token);
        invoiceList = JsonConvert.DeserializeObject<List<BAARInvoiceSyncDTO>>(response.ResponsePayload.ToString());
      }



      int lastValue = invoiceList.Count - 1;

      if (response.ResponseStatus == "Success")
      {

        await _invoiceDS.AddInvoiceListAsync(invoiceList, request.TenantId, tenantUserId, isBulkInsert, token);

        //TODO: Add In SyncTimeLog
        if (invoiceList != null && invoiceList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, invoiceList[lastValue].ERPConnectorKey, "ARInvoice", invoiceList[lastValue].ERPARInvoiceKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (invoiceList != null && invoiceList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, invoiceList[lastValue].ERPConnectorKey, "ARInvoice", invoiceList[lastValue].ERPARInvoiceKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get Invoice  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPAPInvoiceDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAAPInvoiceSyncDTO> invoiceList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for ARInvoice
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.APInvoice.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);
      ConectorResDTO response = null;
      if (request.ERPEntityKey != null && request.ERPEntityKey.Count > 0)
      {
        //invoiceList = new List<BAAPInvoiceSyncDTO>();
        //for (int i = 0; i < request.ERPEntityKey.Count; i++)
        //{
        //  response = await syncServiceDS.GetInitAPInvoiceDataByIdAsync(request.ERPEntityKey[i], request.TenantId, token);
        //  BAAPInvoiceSyncDTO invoice = JsonConvert.DeserializeObject<BAAPInvoiceSyncDTO>(response.ResponsePayload.ToString());
        //  invoiceList.Add(invoice);
        //}
        return true;

      }
      else
      {
        response = await syncServiceDS.GetInitAPInvoiceDataAsync(conTransRequest, request.TenantId, token);
        invoiceList = JsonConvert.DeserializeObject<List<BAAPInvoiceSyncDTO>>(response.ResponsePayload.ToString());
      }



      int lastValue = invoiceList.Count - 1;

      if (response.ResponseStatus == "Success")
      {

        await _apInvoiceDS.AddAPInvoiceListAsync(invoiceList, request.TenantId, tenantUserId, isBulkInsert, token);

        //TODO: Add In SyncTimeLog
        if (invoiceList != null && invoiceList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, invoiceList[lastValue].ERPConnectorKey, "APInvoice", invoiceList[lastValue].ERPAPInvoiceKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (invoiceList != null && invoiceList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, invoiceList[lastValue].ERPConnectorKey, "APInvoice", invoiceList[lastValue].ERPAPInvoiceKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get sales Quotation  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPSalesQuotationDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BASalesQuotationSyncDTO> salesQuotationList = null;
      ConectorResDTO response = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for SalesQuotation
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.SalesQuotation.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);
      if (request.ERPEntityKey != null && request.ERPEntityKey.Count > 0)
      {
        salesQuotationList = new List<BASalesQuotationSyncDTO>();
        for (int i = 0; i < request.ERPEntityKey.Count; i++)
        {
          response = await syncServiceDS.GetInitSalesQuotationDataByIdAsync(request.ERPEntityKey[i], request.TenantId, token);
          BASalesQuotationSyncDTO salesQuotation = JsonConvert.DeserializeObject<BASalesQuotationSyncDTO>(response.ResponsePayload.ToString());
          salesQuotationList.Add(salesQuotation);
        }

      }
      else
      {
        response = await syncServiceDS.GetInitSalesQuotationDataAsync(conTransRequest, request.TenantId, token);

        salesQuotationList = JsonConvert.DeserializeObject<List<BASalesQuotationSyncDTO>>(response.ResponsePayload.ToString());
      }

      int lastValue = salesQuotationList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _salesQuotationDS.AddSalesQuotationListAsync(salesQuotationList, request.TenantId, tenantUserId, isBulkInsert, token);
        //TODO: Add In SyncTimeLog
        if (salesQuotationList != null && salesQuotationList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, salesQuotationList[lastValue].ERPConnectorKey, "SalesQuotation", salesQuotationList[lastValue].ERPSalesQuotationKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (salesQuotationList != null && salesQuotationList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, salesQuotationList[lastValue].ERPConnectorKey, "SalesQuotation", salesQuotationList[lastValue].ERPSalesQuotationKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get Delivery  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPDeliveryDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BADeliverySyncDTO> deliveryList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for Delivery
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.Delivery.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitDeliveryDataAsync(conTransRequest, request.TenantId, token);
      deliveryList = JsonConvert.DeserializeObject<List<BADeliverySyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = deliveryList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _deliveryDS.AddDeliveryListAsync(deliveryList, request.TenantId, tenantUserId, isBulkInsert, token);
        //TODO: Add In SyncTimeLog
        if (deliveryList != null && deliveryList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, deliveryList[lastValue].ERPConnectorKey, "Delivery", deliveryList[lastValue].ERPDeliveryKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (deliveryList != null && deliveryList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, deliveryList[lastValue].ERPConnectorKey, "Delivery", deliveryList[lastValue].ERPDeliveryKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }
    /// <summary>
    /// Get Contract  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPContractDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAContractSyncDTO> contractList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for Contract
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.Contract.ToString()
       && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitContractDataAsync(conTransRequest, request.TenantId, token);

      contractList = JsonConvert.DeserializeObject<List<BAContractSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = contractList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _contractDS.AddContractListAsync(contractList, request.TenantId, tenantUserId, isBulkInsert, token);
        //TODO: Add In SyncTimeLog
        if (contractList != null && contractList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, contractList[lastValue].ERPConnectorKey, "Contract", contractList[lastValue].ERPContractKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (contractList != null && contractList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, contractList[lastValue].ERPConnectorKey, "Contract", contractList[lastValue].ERPContractKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }
    /// <summary>
    /// Get Contract  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPVendorContractDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAVendorContractSyncDTO> contractList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for Contract
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.VendorContract.ToString()
       && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitContractDataAsync(conTransRequest, request.TenantId, token);

      contractList = JsonConvert.DeserializeObject<List<BAVendorContractSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = contractList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _vendContractDS.AddContractListAsync(contractList, request.TenantId, tenantUserId, isBulkInsert, token);
        //TODO: Add In SyncTimeLog
        if (contractList != null && contractList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, contractList[lastValue].ERPConnectorKey, "Contract", contractList[lastValue].ERPContractKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (contractList != null && contractList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, contractList[lastValue].ERPConnectorKey, "Contract", contractList[lastValue].ERPContractKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }
    /// <summary>
    /// Get ASN  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PullERPASNDataAsync(PullERPDataReqDTO request, Guid tenantUserId, CancellationToken token = default(CancellationToken))
    {
      List<BAASNSyncDTO> asnList = null;
      bool isBulkInsert = false;
      // Get SyncTieLog for ASN
      SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.ASN.ToString()
      && stl.TenantId == request.TenantId);

      ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      if (syncTimeLog == null)
      {
        isBulkInsert = true;
        conTransRequest.FromDate = null;
        conTransRequest.ToDate = DateTime.UtcNow;
      }
      else
      {
        isBulkInsert = false;
        conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
        conTransRequest.ToDate = DateTime.UtcNow;
      }

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetInitASNDataAsync(conTransRequest, request.TenantId, token);

      asnList = JsonConvert.DeserializeObject<List<BAASNSyncDTO>>(response.ResponsePayload.ToString());
      int lastValue = asnList.Count - 1;
      if (response.ResponseStatus == "Success")
      {

        await _asnDS.AddASNListAsync(asnList, request.TenantId, tenantUserId, isBulkInsert, token);
        //TODO: Add In SyncTimeLog
        if (asnList != null && asnList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, asnList[lastValue].ERPConnectorKey, "ASN", asnList[lastValue].ERPASNKey, "", (int)SyncResponseEnum.Success, token);
        }
        return true;
      }
      else
      {
        //TODO: Log Error in SyncHistory
        if (asnList != null && asnList.Count > 0)
        {
          await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, asnList[lastValue].ERPConnectorKey, "ASN", asnList[lastValue].ERPASNKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        }
        return false;
      }
    }

    /// <summary>
    /// Get item  from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<BASyncItemPriceDTO> PullItemPriceAsync(PullItemPriceReqDTO request,  CancellationToken token = default(CancellationToken))
    {
      UserSession session = _userSessionManager.GetSession();

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == session.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.GetItemPriceAsync(request, session.TenantId, token);

      BASyncItemPriceDTO  itemPriceDTO = JsonConvert.DeserializeObject<BASyncItemPriceDTO>(response.ResponsePayload.ToString());

      return itemPriceDTO;


    }

    #endregion Pull ERP Data


    #region PUSH ERP Data

    /// <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      // Get primary tenant user 
      Guid tenantUserId = await _appMgmtDS.GetTenantPrimaryUserAsync(tenantId, token);

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == tenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.PushSalesOrderDataInERPAsync(request, tenantId, token);


      return true;
    }
    // <summary>
    /// Sync data from V1 connector to application
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<bool> PushSalesQuotationDataInERPAsync(BASalesQuotationSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
    {

      // Get primary tenant user 
      Guid tenantUserId = await _appMgmtDS.GetTenantPrimaryUserAsync(tenantId, token);

      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == tenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      ConectorResDTO response = await syncServiceDS.PushSalesQuotationDataInERPAsync(request, tenantId, token);


      return true;
    }

    /// <summary>
    /// Get customer from connector and add in local database.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    public async Task<bool> PostERPCustomerDataAsync(List<BACustomerSyncDTO> request, Guid tenantId,  CancellationToken token = default(CancellationToken))
    {
      List<BACustomerSyncDTO> customerList = request;
      bool isBulkInsert = false;
      // Get primary tenant user 
      Guid tenantUserId = await _appMgmtDS.GetTenantPrimaryUserAsync(tenantId, token);
      // Get SyncTieLog for SalesQuotation
      //SyncTimeLog syncTimeLog = await _syncTimeLogDS.FindAsync(stl => stl.ERPEntityKey == BAEntityEnum.Customer.ToString()
      //&& stl.TenantId == request.TenantId);

      //ConTransRequestDTO conTransRequest = new ConTransRequestDTO();
      //if (syncTimeLog == null)
      //{
      //  isBulkInsert = true;
      //  conTransRequest.FromDate = null;
      //  conTransRequest.ToDate = DateTime.UtcNow;
      //}
      //else
      //{
      //  isBulkInsert = false;
      //  conTransRequest.FromDate = syncTimeLog.ReceiveFromTime;
      //  conTransRequest.ToDate = DateTime.UtcNow;
      //}

      // TODO: Get connetion key from ERPConnectorConf by tenantId  
      //ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      //ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);

      //ConectorResDTO response = await syncServiceDS.GetInitCustomerDataAsync(conTransRequest, request.TenantId, token);

      //customerList = JsonConvert.DeserializeObject<List<BACustomerSyncDTO>>(response.ResponsePayload.ToString());
      //int lastValue = customerList.Count - 1;
      if (customerList != null && customerList.Count > 0)
      {
        await _customerSignupDS.AddCustomerListAsync(customerList, tenantId, tenantUserId, isBulkInsert, token);

        //if (customerList != null && customerList.Count > 0)
        //{
        //  await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, customerList[lastValue].ERPConnectorKey, "Customer", customerList[lastValue].ERPCustomerKey, "", (int)SyncResponseEnum.Success, token);
        //}
        return true;

        //TODO: Add In SyncTimeLog (1, Success)
      }
      else
      {
        //TODO: Add In SyncTimeLog (2, Failed)
        //if (customerList != null && customerList.Count > 0)
        //{
        //  await _syncTimeLogDS.AddSyncTimeLogByStatus(request.TenantId, customerList[lastValue].ERPConnectorKey, "Customer", customerList[lastValue].ERPCustomerKey, response.Error.ToString(), (int)SyncResponseEnum.Failed, token);
        //}
        // Log Error in SyncHistory
        return false;
      }
      //TODO: Add In SyncHistory
    }
    #endregion PUSH ERP Data


    #region Notify

    public async Task<bool> NotifyApplicationAsync(NotifyAppDTO notifyDTO, CancellationToken token = default(CancellationToken))
    {
      //ResponseDTO responseDTO = new ResponseDTO();
      //_logger.Debug("[{Method}] - Processing starts with request - {@notifyDTO}", "NotifyApplicationAsync", notifyDTO);

      //(string baseURL, List<KeyValuePair<string, string>> headers, HttpRequestProcessor httpRequestProcessor) connectionDetails = await _utils.GetAppHttpClient(token);

      ////Operation Specific URL
      //string requestURI = "/api/company/notify";

      //_logger.Verbose("[{Method}] - Processing HTTP call with {baseurl} {requestURI} and headers {@header}", "NotifyApplicationAsync", connectionDetails.baseURL, requestURI, connectionDetails.headers);
      //try
      //{
      //  // List<ResponseDTO> response = await connectionDetails.httpRequestProcessor.ExecutePOSTRequestAsync<>(connectionDetails.baseURL, requestURI, AcceptMediaType.JSON, connectionDetails.headers, null, null);
      //  responseDTO.ResponseStatus = SAPB1Const.Success;
      //}
      //catch (Exception ex)
      //{
      //  _logger.Error("[{Method}] -has error {@response}", "NotifyApplicationAsync", ex.Message.ToString());
      //  responseDTO.ResponseStatus = SAPB1Const.Failure;
      //  responseDTO.Error = ex.Message;
      //}
      //_logger.Debug("[{Method}] - Processing ends", "NotifyApplicationAsync");
      //return responseDTO;
      return true;
    }
    #endregion Notify

    public async Task<AttachmentResDTO> GetAttachmentFromERPAsync(AttachmentReqDTO request, CancellationToken token = default(CancellationToken))
    {
      string mimetype = "";
      AttachmentResDTO resDTO = new AttachmentResDTO();
      // TODO: Get connetion key from ERPConnectorConf by tenantId 
      ERPConnectorConfig eRPConnectorConfig = await _eRPConfigDS.FindAsync(ec => ec.TenantId == request.TenantId);

      ISyncServiceDS syncServiceDS = _syncServiceFactory.Resolve(eRPConnectorConfig.ConnectorKey);
      ConectorResDTO response = null;
      switch ((int)request.BAEntityType)
      {
        case (int)BAEntityEnum.ItemMaster:
          response = await syncServiceDS.GetItemMasterAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.ARInvoice:
          response = await syncServiceDS.GetARInvoiceAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.SalesOrder:
          response = await syncServiceDS.GetSalesOrderAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.SalesQuotation:
          response = await syncServiceDS.GetSalesQuotationAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.Delivery:
          response = await syncServiceDS.GetDeliveryAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.ASN:
          response = await syncServiceDS.GetASNAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        case (int)BAEntityEnum.Contract:
          response = await syncServiceDS.GetSalesOrderAttachmentAsync(request.ERPEntityKey, request.ERPEntityAttachmentKey, request.TenantId, token);
          break;
        default:
          return resDTO;
      }

      List<Body> contentList = JsonConvert.DeserializeObject<List<Body>>(response.ResponsePayload.ToString());

      if (response.ResponseStatus == "Success")
      {

        //string bs64 =contentList[0].Content;
        //File.WriteAllBytes(@"c:\files\aa.docx", Convert.FromBase64String(bs64));
        mimetype = FileSupport.GetFileMimeType(request.FileName);
        resDTO.MimeType = mimetype;
        resDTO.Content = contentList[0].Content;
      }
      return resDTO;
    }
  }

  public class Body
  {
    public string Content
    {
      get; set;
    }
  }
}
