/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 29 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// A wrapper class contains method to add business with all its related entities.
  /// </summary>
  public class BusinessUpdateDS:IBusinessUpdateDS {

    #region Local Variables

    IBusinessDS _businessDS;
    IBusinessAddressDS _addressDS;
    IUserSessionManager _userSessionManager;
    AppPortalAppSettings _appSetting;
    IQPublisherAndUserDS _qPublisherDS;
    ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
    IUnitOfWork _unitOfWork;
    ITenantAppLinkingDS _tenantAppLinkingDS;
    IRoleDS _roleDS;
    IRoleLinkingDS _roleLinkingDS;
    IBusUserPreferenceDS _busUserPreferenceDS;

    #endregion Local Variables

    #region Constructor

    /// <summary>
    /// Default constructor.
    /// </summary>
    public BusinessUpdateDS(IBusinessDS businessDS,
        IBusinessAddressDS addressDS, IRoleDS roleDS, IRoleLinkingDS roleLinkingDS,
    ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IUserSessionManager userSessionManager,
        IQPublisherAndUserDS qPublisherDS, ITenantAppLinkingDS tenantAppLinkingDS, IUnitOfWork unitOfWork,
        IOptions<AppPortalAppSettings> appSetting, IBusUserPreferenceDS busUserPreferenceDS) {
      _businessDS = businessDS;
      _addressDS = addressDS;
      _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
      _userSessionManager = userSessionManager;
      _qPublisherDS = qPublisherDS;
      _tenantAppLinkingDS = tenantAppLinkingDS;
      _unitOfWork = unitOfWork;
      _appSetting = appSetting.Value;
      _roleDS = roleDS;
      _roleLinkingDS = roleLinkingDS;
      _busUserPreferenceDS = busUserPreferenceDS;
    }

    #endregion Constructor     

    #region Update Business 

    ///<inheritdoc/>
    public async Task<BusinessResponseModelDTO> UpdateBusinessAsync(UpdateBusinessTenantModelDQ tenantRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
      // Passing business tenant id and business type to get Tenantlinking.            
      // bool isAdd = false;
      //ToDo: nitin-review model name.
      BusinessResponseModelDTO response = new BusinessResponseModelDTO();

      // Updating tenant.
      Business tenant = await UpdateTenantAndAddressAndBusinessAsync(tenantRegistrtionDTO, token);

      // Adding/updating application subscription for vendor.
      List<UpdateBusinessAppSubscriptionDTO> listSubscription = tenantRegistrtionDTO.Subscriptions;
      bool isApplicationAdd = false;
      if(listSubscription != null) {
        Guid busAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);

        //ToDo: we are using CQRS for update operation is it correct?
        AppDetailDQ busAppDtlDTO = await _qPublisherDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantIDAsync(busAppId, tenantRegistrtionDTO.PublisherTenantId, token);

        Guid busThemeId = busAppDtlDTO != null ? busAppDtlDTO.ThemeId : Guid.Empty;

        // getting publisher app setting                
        // AppDetailDQ pubAppSetting = await _pubAppSettingDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(appPublisherId, parentTenantLinking.PublisherTenantId.Value, token);
        for(int i = 0; i < listSubscription.Count; i++) {
          // ToDo: Review this code.
          AppDetailDQ appDtlDTO = await _qPublisherDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantIDAsync(listSubscription[i].AppId, tenantRegistrtionDTO.PublisherTenantId, token);
          if(appDtlDTO != null) {
            listSubscription[i].ThemeId = appDtlDTO.ThemeId;
          }
          else if(busAppDtlDTO != null) {
            listSubscription[i].ThemeId = busAppDtlDTO.ThemeId;
          }

          TenantAppLinking tl = await _tenantAppLinkingDS.FindAsync(item => item.AppId == listSubscription[i].AppId && item.TenantId == tenantRegistrtionDTO.TenantId, token);
          // if not exist then add TenantAppLinking.
          if(tl == null) {
            await AddTenantAppLinkingAsync(tenantRegistrtionDTO.TenantId, listSubscription[i].ThemeId, listSubscription[i].AppId, token);
            isApplicationAdd = true;
          }
        }
      }

      // filtering the connector setting save for business specific.
      List<ConnectorConfigDTO> busExistingConnectorConfig = tenantRegistrtionDTO.ConnectorConfigList;
      // method will add/update/delete connector configuration setting. 
      // if connector already exist then update, if not coming in incoming request then delete. otherwise add.
      await UpdateBusinessConnectorConfigsAsync(tenantRegistrtionDTO.TenantId, busExistingConnectorConfig, token);

      //ToDo: nitin- model name is TenantUpdateForBusinessDTO as decided. Also only editable fields should be passed to update.
      UpdateAppMgmtTenantModelDTO appMgmtTenantModel = tenantRegistrtionDTO.MapBusinessModelToUpdateAppMgmtTenantModel();
      appMgmtTenantModel.EntityType = (int)TenantType.Buisness;
      // Update tenant and subscription.
      //ToDo: nitin- model name is not correct.
      BusinessUpdateResponseModelDTO busUpdateResModel = await UpdateTenantAndAppSubscriptionApiAsync(appMgmtTenantModel, token);


      // Asing role and prefrences for th eprimary user for new applications
      if(busUpdateResModel.userAppRelationDTOs != null && busUpdateResModel.userAppRelationDTOs.Count > 0) {
        //ToDo: nitin- this method is calling multiple web api so in-case of any error it's difficutl to rollback any operation.
        await AddRoleLinkingAndAppPrefrencesForUser(busUpdateResModel.userAppRelationDTOs, tenantRegistrtionDTO.TenantId, tenantRegistrtionDTO.PrimaryUserId, tenantRegistrtionDTO.CreatedBy, token);
      }

      _unitOfWork.SaveAll();

      //if (isApplicationAdd && tenantRegistrtionDTO.IntegratedMode)
      //{
      //  await UpdateERPDataForNewApplicationAsync(tenantRegistrtionDTO.TenantId, token);
      //}

      response.Id = tenant.ID;
      response.IsSuccess = true;

      return response;
      //ToDo: nitin- add note on how to rollback web api call operation as we are not maintaining any response or other variable..
    }

    private async Task AddTenantAppLinkingAsync(Guid tenantId, Guid themeId, Guid appId, CancellationToken token = default(CancellationToken)) {
      TenantAppLinking taLinking = new TenantAppLinking();
      taLinking.ThemeId = themeId;
      taLinking.AppId = appId;
      // AddTenantAppLinkingAsync
      _tenantAppLinkingDS.UpdateSystemFieldsByOpType(taLinking, OperationType.Add);
      taLinking.TenantId = tenantId;
      await _tenantAppLinkingDS.AddAsync(taLinking, token);
    }

    private async Task<BusinessUpdateResponseModelDTO> UpdateTenantAndAppSubscriptionApiAsync(UpdateAppMgmtTenantModelDTO dto, CancellationToken token = default(CancellationToken)) {
      //RequestOptions requestOptions = new RequestOptions();
      //requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      //requestOptions.Method = "tenant/updatebusiness";
      //requestOptions.MethodData = dto;
      //requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      //requestOptions.ServiceRequestType = RequestTypeEnum.Post;
      //requestOptions.BearerTokenInfo = new BearerTokenOption();
      //requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
      //requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
      //ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
      //return await httpRequestProcessor.ExecuteAsync<BusinessUpdateResponseModelDTO>(requestOptions, false);
      string Method = "tenant/updatebusiness";
      List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      listHeader.Add(hdr);
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, dto, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
      return await httpRequestProcessor.ExecuteAsync<BusinessUpdateResponseModelDTO>(requestOptions, false);
    }

    /// <summary>
    /// Adding/updating/deleting configuration for business.
    /// </summary>        
    /// <param name="tenantId">Business tenant</param>        
    /// <param name="connectorConfigDTO">Incoming connetor request is coming for add/update/delete.</param>
    /// <param name="token"></param>
    private async Task UpdateBusinessConnectorConfigsAsync(Guid tenantId, List<ConnectorConfigDTO> connectorConfigDTO, CancellationToken token) {
      // Preparing api calling process model.
      //RequestOptions requestOptions = new RequestOptions();
      //requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      //requestOptions.Method = "ERPConnectorConfig/addupdateconfig/" + tenantId.ToString();
      //requestOptions.MethodData = connectorConfigDTO;
      //requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      //requestOptions.ServiceRequestType = RequestTypeEnum.Post;
      //requestOptions.BearerTokenInfo = new BearerTokenOption();
      //requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
      //requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
      //List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      //string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      //KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      //listHeader.Add(hdr);
      //requestOptions.HeaderParameters = listHeader;
      //ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
      //await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);

      // Preparing api calling process model.   

      string Method = "basync/manageconnection/" + tenantId.ToString();
      List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      listHeader.Add(hdr);
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, connectorConfigDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
      await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
    }

    /// <summary>
    /// pull erp Data
    /// </summary>        
    /// <param name="tenantId">Business tenant</param>       
    /// <param name="token"></param>
    private async Task UpdateERPDataForNewApplicationAsync(Guid tenantId, CancellationToken token) {

      // Preparing api calling process model.   
      PullERPDataReqDTO request = new PullERPDataReqDTO();
      request.SelectedBAEntityTypes = new List<int>() { };
      request.TenantId = tenantId;
      string Method = "BASync/pullerpdata";
      List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
      string clientsessionid = _userSessionManager.GetSession().ID.ToString();
      KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
      listHeader.Add(hdr);
      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, request, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
      await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
    }

    #endregion Update Business 

    #region Update Tenant/Address/Business

    /// <summary>
    /// Add tenant/tenantlinking/business.
    /// </summary>
    /// <param name="businessRegistrtionDTO"></param>    
    /// <param name="token"></param>
    /// <returns></returns>
    // ToDo: nitin-call indivisual method from main method instead of clubbing two operations in single private method.
    private async Task<Business> UpdateTenantAndAddressAndBusinessAsync(UpdateBusinessTenantModelDQ businessRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
      //ToDo: nitin-this method will spilit into two method.

      // Update business.  
      Business bus = await UpdateBusinessInfoAsync(businessRegistrtionDTO, token);
      // Adding address.
      //ToDo: nitin- Address should be add/update/delete based on operation type.
      await UpdateBusinessAddressListAsync(businessRegistrtionDTO.AddressList, bus, businessRegistrtionDTO.TenantId, token);

      return bus;
    }

    /// <summary>
    /// Update business address.
    /// </summary>
    /// <param name="addressDTOList"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task UpdateBusinessAddressListAsync(List<BusinessAddressModelDTO> addressDTOList, Business bus, Guid tenantId, CancellationToken token = default(CancellationToken)) {
      List<BusinessAddress> listAddress = await _addressDS.GetAddressEntityListByParentEntityIdAndAddressTypeAsync(tenantId, bus.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
      bool find = false;
      for(int i = 0; i < listAddress.Count; i++) {
        find = false;
        for(int dtoIndex = 0; dtoIndex < addressDTOList.Count; dtoIndex++) {
          if(listAddress[i].ID == addressDTOList[dtoIndex].ID) {
            find = true;
            break;
          }
        }
        if(!find) {
          listAddress[i].Deleted = true;
          await _addressDS.UpdateAsync(listAddress[i], listAddress[i].ID, token);
        }
      }

      for(int i = 0; i < addressDTOList.Count; i++) {
        BusinessAddress add = listAddress.Find(addrs => addrs.ID == addressDTOList[i].ID);
        if(add != null) {
          await UpdateBusinessAddressAsync(addressDTOList[i], tenantId, add, token);
        }
        else {
          await AddAddressAsync(addressDTOList[i], bus, tenantId, token);
        }
      }

    }

    /// <summary>
    /// Add business address.
    /// </summary>
    /// <param name="addressDTO"></param>
    /// <param name="bus">Business entity.</param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task AddAddressAsync(BusinessAddressModelDTO addressDTO, Business bus, Guid tenantId, CancellationToken token = default(CancellationToken)) {
      BusinessAddress address = BusinessAddressModelDTO.MapToEntity(addressDTO);
      address.ID = Guid.NewGuid();
      address.Label = addressDTO.Label;
      _addressDS.UpdateSystemFieldsByOpType(address, OperationType.Add);
      address.TenantId = tenantId;
      address.BusinessId = bus.ID;
      address.AddressType = (int)BusinessAddressTypeEnum.DefaultBusinessAddress;
      await _addressDS.AddAsync(address, token);
    }

    /// <summary>
    /// Update business address.
    /// </summary>
    /// <param name="addressDTO"></param>
    /// <param name="tenantId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task UpdateBusinessAddressAsync(BusinessAddressModelDTO addressDTO, Guid tenantId, BusinessAddress businessAddress, CancellationToken token = default(CancellationToken)) {
      businessAddress.Label = addressDTO.Label;
      businessAddress.AddressStreet1 = addressDTO.AddressStreet1;
      businessAddress.AddressStreet2 = addressDTO.AddressStreet2;
      businessAddress.AddressStreet3 = addressDTO.AddressStreet3;
      businessAddress.City = addressDTO.City;
      businessAddress.Country = addressDTO.Country;
      businessAddress.State = addressDTO.State;
      businessAddress.Phone = addressDTO.Phone;
      businessAddress.ZipCode = addressDTO.ZipCode;

      businessAddress.AddressType = addressDTO.AddressType;
      businessAddress.FaxNumber = addressDTO.FaxNumber;
      businessAddress.ZipCode = addressDTO.ZipCode;
      businessAddress.ZipCode = addressDTO.ZipCode;
      businessAddress.ZipCode = addressDTO.ZipCode;

      _addressDS.UpdateSystemFieldsByOpType(businessAddress, OperationType.Update);

      await _addressDS.UpdateAsync(businessAddress, businessAddress.ID, token);
    }


    /// <summary>
    /// Update business entity from business model.
    /// </summary>
    /// <param name="businessRegistrtionDTO"></param>
    /// <param name="token"></param>
    private async Task<Business> UpdateBusinessInfoAsync(UpdateBusinessTenantModelDQ businessRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
      Business business = await _businessDS.GetBusinessByTenantIdAsync(businessRegistrtionDTO.TenantId, token);
      business.Name = businessRegistrtionDTO.Name;
      business.ContactPersonDesignation = businessRegistrtionDTO.ContactPersonDesignation;
      business.ContactPersonEmail = businessRegistrtionDTO.ContactPersonEmail;
      business.ContactPersonName = businessRegistrtionDTO.ContactPersonName;
      business.ContactPersonPhone = businessRegistrtionDTO.ContactPersonPhone;

      business.Language = businessRegistrtionDTO.Language;
      business.TimeZone = businessRegistrtionDTO.TimeZone;
      business.DateTimeFormat = businessRegistrtionDTO.DateTimeFormat;
      business.Website = businessRegistrtionDTO.Website;

      _businessDS.UpdateSystemFieldsByOpType(business, OperationType.Update);
      business.TenantId = businessRegistrtionDTO.TenantId;

      await _businessDS.UpdateAsync(business, business.ID, token);

      return business;
    }

    #endregion Update Tenant/Address/Business

    #region Support

    private string GetEncryptValue(string value) {
      return new CryptoHelper().Encrypt(value, CryptoHelper.EncryptionAlgorithm.AES);
    }

    private string GetDecryptValue(string value) {
      return new CryptoHelper().Decrypt(value, CryptoHelper.EncryptionAlgorithm.AES);
    }

    #endregion Support

    #region Private Methods
    //ToDo: nitin-Method name
    private async Task AddRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {
      UserSession userSession = _userSessionManager.GetSession();

      foreach(UserAppRelationDTO item in userAppRelationDTOs) {
        // Business Portal - Setup App
        if(item.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
          await AddPrimaryUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserBusinessSetupAppPermissionEnum.All, cancellationToken);

          // ToDo: Review code to create tenant user preference entry.
          //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserEmailPrefrenceEnum.All, (long)BusinessUserSMSPrefrenceEnum.All, cancellationToken);

          //--old code
          //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, cancellationToken);

          ////--new code
          //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, cancellationToken);
          //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllBEPreference, cancellationToken);

          await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllEmail, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllSMS, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAS, cancellationToken);
        }
        // Business Portal - Payment App
        else if(item.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
          TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
          tenantUserAppManagmentDTO.AppId = item.AppId;
          tenantUserAppManagmentDTO.CreatedBy = createdBy;
          tenantUserAppManagmentDTO.TenantId = tenantId;
          tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
          tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Business;
          tenantUserAppManagmentDTO.Admin = true;

          ////New code
          //tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
          //tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
          //tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;

          tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllEmail;
          tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllSMS;
          tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllAS;

          RequestOptions requestOptions = new RequestOptions();
          requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
          //ToDo: nitin-url is close to signup operation.
          requestOptions.Method = "businessuser/appassign";
          requestOptions.MethodData = tenantUserAppManagmentDTO;
          requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
          requestOptions.ServiceRequestType = RequestTypeEnum.Post;
          requestOptions.BearerTokenInfo = new BearerTokenOption();
          requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
          requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
          ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.PaymentApiUrl);
          await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);

          ////New code
          //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
          //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
        }
        else if(item.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
          TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
          tenantUserAppManagmentDTO.AppId = item.AppId;
          tenantUserAppManagmentDTO.CreatedBy = createdBy;
          tenantUserAppManagmentDTO.TenantId = tenantId;
          tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
          tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Business;
          tenantUserAppManagmentDTO.Admin = true;

          RequestOptions requestOptions = new RequestOptions();
          requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
          //ToDo: nitin-url is close to signup operation.
          requestOptions.Method = "businessuser/appassign";
          requestOptions.MethodData = tenantUserAppManagmentDTO;
          requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
          requestOptions.ServiceRequestType = RequestTypeEnum.Post;
          requestOptions.BearerTokenInfo = new BearerTokenOption();
          requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
          requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
          ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.ShipmentApiUrl);
          await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
        }
        // Business Portal - Customer App
        else if(item.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
          await AddPrimaryUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserCustomerAppPermissionEnum.All, cancellationToken);
          await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserCustomerAppPreferenceEnum.AllEmail, (long)BusinessUserCustomerAppPreferenceEnum.AllSMS, (long)BusinessUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
        }
        // Business Portal - Vendor App
        else if(item.AppKey.Equals(AppKeyEnum.vend.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
          await AddPrimaryUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserCustomerAppPermissionEnum.All, cancellationToken);
          await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserVendorAppPreferenceEnum.AllEmail, (long)BusinessUserVendorAppPreferenceEnum.AllSMS, (long)BusinessUserVendorAppPreferenceEnum.AllAS, cancellationToken);
        }
      }
    }


    private async Task AddPrimaryUserRoleAndRoleLinkingAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long permissionbitMask, CancellationToken cancellationToken) {
      // Get role/add role based on input permission mask bit.
      Guid roleId = await _roleDS.GetOrCreateRoleAsync(permissionbitMask, appId, (int)UserTypeEnum.Business, createdBy, cancellationToken);

      // Add Role linking entry for primary user.
      RoleLinking roleLinking = new RoleLinking();
      roleLinking.RoleId = roleId;
      roleLinking.AppId = appId;
      roleLinking.TenantId = tenantId;
      roleLinking.TenantUserId = tenantUserId;
      //ToDo: nitin-use system field update method.
      roleLinking.CreatedBy = createdBy;
      roleLinking.CreatedOn = DateTime.UtcNow;
      roleLinking.UpdatedBy = roleLinking.CreatedBy;
      roleLinking.UpdatedOn = roleLinking.CreatedOn;
      await _roleLinkingDS.AddAsync(roleLinking, cancellationToken);
    }

    private async Task AddTenantUserAppPrefernces(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long emailPrefrence, long smsPreference, long asPreference, CancellationToken cancellationToken) {
      // Add tenant user preference entry.
      TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
      tenantUserAppPreference.ID = Guid.NewGuid();
      tenantUserAppPreference.AppId = appId;
      tenantUserAppPreference.CreatedBy = createdBy;
      tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
      tenantUserAppPreference.Deleted = false;
      tenantUserAppPreference.EmailPreference = emailPrefrence;
      tenantUserAppPreference.SMSPreference = smsPreference;
      tenantUserAppPreference.ASPreference = asPreference;
      //ToDo: nitin-use system field update method.
      tenantUserAppPreference.TenantId = tenantId;
      tenantUserAppPreference.TenantUserId = tenantUserId;
      tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
      tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
      await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
    }

    // private async Task GenerateBusinessUserInvitation

    #endregion Private Methods
  }
}
