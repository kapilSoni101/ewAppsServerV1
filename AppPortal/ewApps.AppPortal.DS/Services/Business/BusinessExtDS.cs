/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 September 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
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
    ///  A business wrapper class to get busines and business tenant and related child entities.
    /// </summary>
    public class BusinessExtDS:IBusinessExtDS {

        #region Local Variables

        IBusinessDS _businessDS;
        IBusinessAddressDS _addressDS;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSetting;
        IUniqueIdentityGeneratorDS _identityDataService;
        IQPublisherAndUserDS _qPublisherDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IUnitOfWork _unitOfWork;
        IBusinessAddressDS _businessAddressDS;
        IQBusinessAndUserDS _qBusinessDS;
        IPublisherAppSettingDS _publisherAppSettingDS;


        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusinessExtDS(IBusinessDS businessDS,
            IBusinessAddressDS addressDS, IUniqueIdentityGeneratorDS identityDataService, IPublisherAppSettingDS publisherAppSettingDS,
            ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IUserSessionManager userSessionManager,
            IQPublisherAndUserDS qPublisherDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appSetting,
            IBusinessAddressDS businessAddressDS,
        IQBusinessAndUserDS qBusinessDS) {
            _businessDS = businessDS;
            _addressDS = addressDS;
            _identityDataService = identityDataService;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _userSessionManager = userSessionManager;
            _qPublisherDS = qPublisherDS;
            _unitOfWork = unitOfWork;
            _appSetting = appSetting.Value;
            _qBusinessDS = qBusinessDS;
            _businessAddressDS = businessAddressDS;
            _publisherAppSettingDS = publisherAppSettingDS;
        }

        #endregion Constructor

        #region Get Business Method

        ///<inheritdoc/>       
        public async Task<UpdateBusinessTenantModelDQ> GetBusinessUpdateModelAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            UpdateBusinessTenantModelDQ dto = await _businessDS.GetBusinessTenantDetailModelDTOAsync(tenantId, token);
            UpdateTenantModelDQ tenantModelDq = await GetBusinessTenantByTenantIdAsync(tenantId, token);
            tenantModelDq.MapProperties(dto);

            dto.TenantId = tenantId;
            // Getting the tenant address.
            dto.AddressList = await _addressDS.GetAddressListByParentEntityIdAndAddressTypeAsync(tenantId, dto.BusinessId, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
            dto.PublisherTenantId = userSession.TenantId;

            dto.UserActivationDate = dto.JoinedOn;
            // Getting config setting.
            List<ConnectorConfigDQ> config = await GetBusinessAppConnectorConfigByBusinessIdAsync(tenantId, token);

            #region Getiing Connector Data

            dto.ConnectorConfigList = new List<ConnectorConfigDTO>();
            if(config != null) {
                ConnectorConfigDTO conectorConfig;
                // Mapping connector setting.
                for(int i = 0; i < config.Count; i++) {
                    if(config[i].ConnectorKey == AppPortal.Common.AppPortalConstants.SAPConnectorKey || config[i].ConnectorKey == AppPortal.Common.AppPortalConstants.BMEConnectorKey) {
                        conectorConfig = new ConnectorConfigDTO();
                        conectorConfig.ID = config[i].ID;
                        conectorConfig.Json = config[i].SettingJson;
                        conectorConfig.ConnectorKey = config[i].ConnectorKey;
                        conectorConfig.Status = config[i].Status;
                        conectorConfig.Message = config[i].Message;
                        dto.ConnectorConfigList.Add(conectorConfig);
                    }
                }
            }

            #endregion Getiing Connector Data

            if(dto != null && dto.Subscriptions != null) {
                for(int i = 0; i < dto.Subscriptions.Count; i++) {
                    PublisherAppSetting publisherAppSetting = await _publisherAppSettingDS.FindAsync(pa => pa.TenantId == userSession.TenantId && pa.AppId == dto.Subscriptions[i].AppId && pa.Deleted == false);
                    if(publisherAppSetting != null) {
                        dto.Subscriptions[i].Name = publisherAppSetting.Name;
                    }
                }
            }

            return dto;
        }

        /// <summary>
        /// Get business primary user.
        /// </summary>
        /// <param name="usersList"></param>
        private UserShortInfoDQ GetBusinessPrimaryUser(List<UserShortInfoDQ> usersList) {
            Guid appId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            for(int i = 0; i < usersList.Count; i++) {
                if(usersList[i].AppId == appId) {
                    return usersList[i];
                }
            }

            return null;
        }

        private AppServiceRequestDTO GenerateAppServiceRequestModel(TenantAppServiceDQ appServiceDTO) {
            AppServiceRequestDTO reqDTO = new AppServiceRequestDTO();
            reqDTO.Name = appServiceDTO.Name;
            reqDTO.ID = appServiceDTO.AppServiceId;
            reqDTO.AppServiceAttributeDTO = new List<AppServiceAttributeRequestDTO>();
            if(appServiceDTO.AttributeId != Guid.Empty) {
                AppServiceAttributeRequestDTO attr = new AppServiceAttributeRequestDTO();
                attr.Name = appServiceDTO.AttributeName;
                attr.ID = appServiceDTO.AttributeId;
                if(!string.IsNullOrEmpty(appServiceDTO.AccountJson)) {
                    appServiceDTO.AccountJson = GetDecryptValue(appServiceDTO.AccountJson);
                    attr.buinessVCACHPayAttrDTO = JsonConvert.DeserializeObject<BusVCACHPayAttrDTO>(appServiceDTO.AccountJson);
                }
                if(attr.buinessVCACHPayAttrDTO == null)
                    attr.buinessVCACHPayAttrDTO = new BusVCACHPayAttrDTO();

                reqDTO.AppServiceAttributeDTO.Add(attr);
            }

            return reqDTO;
        }



        #endregion Get Business Method

        #region Delete

        /// <summary>
        /// Delete business and its subscription.
        /// </summary>
        /// <param name="tenantId">Business tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteBusinessAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            Business bus = await _businessDS.GetBusinessByTenantIdAsync(tenantId, token);
            if(bus != null) {
                bus.Deleted = true;
                _businessDS.UpdateSystemFieldsByOpType(bus, OperationType.Update);
                await _businessDS.UpdateAsync(bus, bus.ID, token);
                List<BusinessAddress> listAddress = await _businessAddressDS.GetAddressEntityListByParentEntityIdAndAddressTypeAsync(tenantId, bus.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress, token);
                for(int i = 0; i < listAddress.Count; i++) {
                    listAddress[i].Deleted = true;
                    _businessAddressDS.UpdateSystemFieldsByOpType(listAddress[i], OperationType.Update);
                    await _businessAddressDS.UpdateAsync(listAddress[i], listAddress[i].ID, token);
                }

                // delete tenant and related data.
                await DeletreTenantAndAppSubscriptionApiAsync(tenantId, token);

                _unitOfWork.SaveAll();
            }
        }

        private async Task DeletreTenantAndAppSubscriptionApiAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string Method = "tenant/deletebusiness/" + tenantId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Delete, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
            await httpRequestProcessor.ExecuteAsync<BusinessUpdateResponseModelDTO>(requestOptions, false);
        }

        #endregion Delete

        #region Support

        private async Task<UpdateTenantModelDQ> GetBusinessTenantByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "tenant/gettenantforbusiness/" + tenantId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
            return await httpRequestProcessor.ExecuteAsync<UpdateTenantModelDQ>(requestOptions, false);
        }

        private async Task<List<TenantAppSubscriptionDQ>> GetBusinessSubscriptionByBusinessIdAsync(Guid tenantId, Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
            // List<TenantAppSubscriptionDQ> list = await _tenantSubscriptionDS.GetBusinessSubscriptionByBusinessIdAsync(tenantId, userSession.TenantId);

            return null;
        }

        private async Task<List<UserShortInfoDQ>> GetBusinessPrimaryUsersByBusinessIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            // List<UserShortInfoDQ> usersList = await _tenantSubscriptionDS.GetBusinessPrimaryUsersByBusinessIdAsync(tenantId, token);
            return null;
        }

        private async Task<List<ConnectorConfigDQ>> GetBusinessAppConnectorConfigByBusinessIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.            
            string Method = "ERPConnectorConfig/getconnectorconfig/" + tenantId.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<ConnectorConfigDQ>>(requestOptions, false);
        }

        private async Task<List<TenantAppServiceDQ>> GetAppServiceByBusinessIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return null;
        }

        private string GetEncryptValue(string value) {
            return new CryptoHelper().Encrypt(value, CryptoHelper.EncryptionAlgorithm.AES);
        }

        private string GetDecryptValue(string value) {
            return new CryptoHelper().Decrypt(value, CryptoHelper.EncryptionAlgorithm.AES);
        }

        #endregion Support

        #region ERP Mgmt

        /// <summary>
        /// Method is used to validate ERP connector connection.
        /// </summary>
        /// <param name="request">Request model</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> TestConnectionAsync(object request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/testconnection";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, request, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }


        public async Task<bool> PullERPDataAsync(PullERPDataReqDTO pullERPDataAsync, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/pullerpdata";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, pullERPDataAsync, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        public async Task<bool> PushSalesOrderDataInERPAsync(Guid tenantid,  BusBASalesOrderDTO request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/pushsalesorderdata/"+ tenantid;
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, request, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        public async Task<AttachmentResDTO> GetAttachmentFromERP(AttachmentReqDTO request, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/erpattachmentdata";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, Method, request, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<AttachmentResDTO>(requestOptions, false);
        }
        /// <summary>
        /// Get Data from Sync time log 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusBASyncTimeLogDTO>> SyncTimeLogAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "BASync/getsynctimelog/" + tenantId;
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            string clientsessionid = _userSessionManager.GetSession().ID.ToString();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<List<BusBASyncTimeLogDTO>>(requestOptions, false);
        }

        #endregion ERP Mgmt

        #region Business Configuration
        ///<inheritdoc/>
        public async Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(CancellationToken token = default(CancellationToken)) {

            //Get Session
            UserSession session = _userSessionManager.GetSession();

            Guid busAppId = new Guid();
            Guid payAppId = new Guid();

            //Get AppInfo
            Task<List<AppInfoDTO>> appInfoDTOsTask = GetAppInfoByAppKey(session.ID);

            // Get Publisher Entity Information
            Business business = await _businessDS.FindAsync(b => b.TenantId == session.TenantId);

            //Wait Until The Above appinfo method does not excuted
            appInfoDTOsTask.Wait();
            List<AppInfoDTO> appInfoDTOs = appInfoDTOsTask.Result;
            if(appInfoDTOs != null) {
                foreach(AppInfoDTO appInfoDTO in appInfoDTOs) {
                    if(appInfoDTO.AppKey.Equals(AppKeyEnum.pay.ToString()))
                        payAppId = appInfoDTO.Id;
                    if(appInfoDTO.AppKey.Equals(AppKeyEnum.biz.ToString()))
                        busAppId = appInfoDTO.Id;
                }
            }

            //Get Configuration for Business
            BusConfigurationDTO businessConfigurationDTO = await _qBusinessDS.GetBusinessConfigurationDetailAsync(session.TenantId, busAppId);

            // Get Business Address Entity Information
            List<BusinessAddressModelDTO> addressDTO = (await _businessAddressDS.GetAddressListByParentEntityIdAndAddressTypeAsync(session.TenantId, business.ID, (int)BusinessAddressTypeEnum.DefaultBusinessAddress)).ToList();
            if(addressDTO != null || addressDTO.Count > 0) {
                businessConfigurationDTO.AddressDTO = addressDTO;
            }

            // Getting config setting.
            List<ConnectorConfigDQ> config = await GetBusinessAppConnectorConfigByBusinessIdAsync(session.TenantId, token);

            #region Getiing Connector Data

            businessConfigurationDTO.ConnectorConfigList = new List<ConnectorConfigDTO>();

            if(config != null) {
                ConnectorConfigDTO conectorConfig;
                // Mapping connector setting.
                for(int i = 0; i < config.Count; i++) {
                    if(config[i].ConnectorKey == AppPortal.Common.AppPortalConstants.SAPConnectorKey || config[i].ConnectorKey == AppPortal.Common.AppPortalConstants.BMEConnectorKey) {
                        conectorConfig = new ConnectorConfigDTO();
                        conectorConfig.ID = config[i].ID;
                        conectorConfig.Json = config[i].SettingJson;
                        conectorConfig.ConnectorKey = config[i].ConnectorKey;
                        conectorConfig.Status = config[i].Status;
                        conectorConfig.Message = config[i].Message;
                        businessConfigurationDTO.ConnectorConfigList.Add(conectorConfig);
                    }
                }
            }

            #endregion Getiing Connector Data

            //Get Service List For Payment On Business Configuration
            businessConfigurationDTO.PayServiceList = await GetBusinessAppServiceListByAppIdsAndTenantIdAsync(session.ID, payAppId, session.TenantId);

            return businessConfigurationDTO;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        public async Task UpdateBusinessConfigurationDetailAsync(BusConfigurationDTO dto, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();


            //Get AppInfo
            Guid payAppId = new Guid();
            List<AppInfoDTO> appInfoDTOs = new List<AppInfoDTO>();
            appInfoDTOs = await GetAppInfoByAppKey(session.ID);
            if(appInfoDTOs != null) {

                foreach(AppInfoDTO appInfoDTO in appInfoDTOs) {
                    if(appInfoDTO.AppKey.Equals(AppKeyEnum.pay.ToString()))
                        payAppId = appInfoDTO.Id;
                }
            }

            // API Call for Upadate other values
            await UpdateConfigurationAsync(dto, session.ID);

            //Get Bissness
            Business business = _businessDS.Get(dto.BusinessId);

            // Add and Update Address Data
            await _businessAddressDS.AddUpdateBusinessAddressListAsync(dto.AddressDTO, session.TenantId, business.ID);

            // Contact Person Detail
            _businessDS.UpdateSystemFieldsByOpType(business, OperationType.Update);
            business.Name = dto.Name; // Business Name Update
            business.ContactPersonName = dto.ContactPersonName;
            business.ContactPersonDesignation = dto.ContactPersonDesignation;
            business.ContactPersonEmail = dto.ContactPersonEmail;
            business.Website = dto.Website;
            business.Language = dto.Language;
            business.TimeZone = dto.TimeZone;
            business.DateTimeFormat = dto.DateTimeFormat;
            business.ContactPersonPhone = dto.ContactPersonPhone;
            business.FederalTexId = dto.FederalTexId;
            business.TelePhone1 = dto.TelePhone1;
            business.TelePhone2 = dto.TelePhone2;
            business.MobilePhone = dto.MobilePhone;
            business.Email = dto.Email;
            business.Status = dto.Status;
            business.InitDB = dto.InitDB;
            business.Configured = true;

            // Setting currency.
            if(business.CanUpdateCurrency) {
                business.CanUpdateCurrency = false;
                business.CurrencyCode = dto.CurrencyCode;                              
                business.DecimalPrecision = dto.DecimalPrecision;
            }
            business.GroupValue = dto.GroupValue;
            business.GroupSeperator = dto.GroupSeperator;
            business.DecimalSeperator = dto.DecimalSeperator;

            // Update Connector
            if(dto.ConnectorConfigList != null && dto.ConnectorConfigList.Count > 0)
                await UpdateBusinessConnectorConfigsAsync(business.TenantId, dto.ConnectorConfigList, token);
            else
                

            //Business Data Update 
            _businessDS.Update(business, dto.BusinessId);

            //UPdate Business Payment AppService
            if(dto.PayServiceList != null && dto.PayServiceList.Count > 0)
                await UpdatePayAppServiceInfo(dto.PayServiceList, session.ID, payAppId, business.TenantId);

            // App Service and It Account Detail Update By This Method 
            //Call To AppManagement
            //List<AppServiceAccDetailIdDTO> listAddUpdateAccount = new List<AppServiceAccDetailIdDTO>();
            //if(dto.ServiceList != null && dto.ServiceList.Count > 0) {
            //    listAddUpdateAccount = UpdateBusinessAppServiceWithServiceAccountDetailInfo(dto.ServiceList, tenantSubscription.AppId, tenant.ID);
            //}
            ////Call To Shipment
            //await AddUpdateCarrierAccountInfoAsync(listAddUpdateAccount, token);

            // Save All
            _unitOfWork.SaveAll();

        }

        private async Task UpdateConfigurationAsync(BusConfigurationDTO dto, Guid Id) {

            string baseuri = _appSetting.AppMgmtApiUrl;
            string requesturl = "configuration/updatepublisherconfiguration";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", Id.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, dto, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        /// <summary>
        /// Adding/updating/deleting configuration for business.
        /// </summary>        
        /// <param name="tenantId">Business tenant</param>        
        /// <param name="connectorConfigDTO">Incoming connetor request is coming for add/update/delete.</param>
        /// <param name="token"></param>
        public async Task UpdateBusinessConnectorConfigsAsync(Guid tenantId, List<ConnectorConfigDTO> connectorConfigDTO, CancellationToken token) {
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

        //Get TenantLinking Details 
        private async Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(Guid ID, Guid appId, Guid tenantId) {

            List<PayAppServiceDetailDTO> payAppServiceDetailDTO = new List<PayAppServiceDetailDTO>();

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "PayAppService/getbuspayappservice/" + tenantId + "/" + appId;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            string baseuri = _appSetting.PaymentApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            payAppServiceDetailDTO = await httpRequestProcessor.ExecuteAsync<List<PayAppServiceDetailDTO>>(requestOptions, false);
            #endregion

            return payAppServiceDetailDTO;
        }

        //Get TenantLinking Details 
        private async Task<List<AppInfoDTO>> GetAppInfoByAppKey(Guid ID) {

            List<AppInfoDTO> appInfoDTO = new List<AppInfoDTO>();
            List<string> appkey = new List<string>();
            appkey.Add(AppKeyEnum.pay.ToString().ToLower());
            appkey.Add(AppKeyEnum.biz.ToString().ToLower());

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "App/getappinfobykey";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, appkey, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            string baseuri = _appSetting.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            appInfoDTO = await httpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(requestOptions, false);
            #endregion

            return appInfoDTO;
        }

        //Get TenantLinking Details 
        private async Task<ResponseModelDTO> UpdatePayAppServiceInfo(List<PayAppServiceDetailDTO> payAppServiceDetailDTo, Guid ID, Guid appId, Guid tenantId) {

            ResponseModelDTO appInfoDTO = new ResponseModelDTO();

            // Preparing api calling process model.
            #region Update AppSerivce 
            string methodUri = "AppService/updateappservice/" + appId + "/" + tenantId;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, methodUri, payAppServiceDetailDTo, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            string baseuri = _appSetting.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            appInfoDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);
            #endregion

            return appInfoDTO;
        }
        #endregion

    }
}
