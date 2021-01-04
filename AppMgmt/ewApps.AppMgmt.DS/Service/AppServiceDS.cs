/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AppManagement.DTO;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS {

  /// <summary>
  /// Manages the app services related CRUD methods.
  /// </summary>
  public class AppServiceDS:BaseDS<AppService>, IAppServiceDS {

        #region Local Variable
        IAppServiceRepository _appServiceRep;
        IAppServiceAttributeDS _appServiceAttributeDS;
        ITenantAppServiceLinkingDS _tenantAppServiceLinkingDS;
        ICustomerAppServiceLinkingDS _customerAppServiceLinkingDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;
        ILogger<AppServiceDS> _loggerService;
        IServiceAccountDetailDS _appServiceAcctDetailDS;
        AppMgmtAppSettings _appMgmtAppSettings;
        #endregion

        #region Constructor 



        public AppServiceDS(ILogger<AppServiceDS> loggerService, IAppServiceRepository appServiceRep, IUserSessionManager userSessionManager, IAppServiceAttributeDS appServiceAttributeDS, ITenantAppServiceLinkingDS tenantAppServiceLinkingDS, ICustomerAppServiceLinkingDS customerAppServiceLinkingDS, IUnitOfWork unitOfWork, IServiceAccountDetailDS appServiceAcctDetailDS,IOptions<AppMgmtAppSettings> appMgmtAppSettings) : base(appServiceRep) {
      _appServiceRep = appServiceRep;
      _appServiceAttributeDS = appServiceAttributeDS;
      _tenantAppServiceLinkingDS = tenantAppServiceLinkingDS;
      _customerAppServiceLinkingDS = customerAppServiceLinkingDS;
      _unitOfWork = unitOfWork;
      _userSessionManager = userSessionManager;
      _loggerService = loggerService;
      _appServiceAcctDetailDS = appServiceAcctDetailDS;
            _appMgmtAppSettings = appMgmtAppSettings.Value;
    }

    #endregion

    #region Get

    /// <summary>
    /// Get appservices by application ID.
    /// </summary>
    /// <param name="appId">Application id.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<AppServiceDTO>> GetAppServicesDetailWithAttributesAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
      List<AppServiceDTO> appServDTO = new List<AppServiceDTO>();

      // Get services from DB.
      List<AppService> appServiceList = await _appServiceRep.GetAppServicesDetailsAsync(appId, token);
      
      foreach(AppService service in appServiceList) {

        // map service entity to DTO.
        AppServiceDTO mappedDTOService = AppServiceDTO.MapAppServiceToDTO(service);

        // Now get corrosponding attribute for service Id.
        mappedDTOService.AppServiceAttributeList = await _appServiceAttributeDS.GetAppServiceAttributeByAppServiceAsync(mappedDTOService.ID, token);

        // Add to list.
        appServDTO.Add(mappedDTOService);
      }

      return appServDTO;
    }


        public async Task<List<AppService>> GetAppServicesDetailsAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<AppService> appservices;
            appservices = await _appServiceRep.GetAppServicesDetailsAsync(appId);
            return appservices;
        }

        public async Task<List<AppServiceDTO>> GetAppServiceListByAppIdAsync(Guid appId, bool onlyActive, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            List<AppServiceDTO> appServiceList = await _appServiceRep.GetAppServiceListByAppIdAsync(appId, onlyActive, includeDeleted);
            return appServiceList;
        }

        ///<inheritdoc/>
        public async Task<List<AppService>> GetAppServiceAsync(List<PubBusinessSubsPlanAppServiceDTO> pubBusinessSubsPlanAppServiceDTO, CancellationToken token = default(CancellationToken)) {
           
            List<AppService> appservices = await _appServiceRep.GetAppServiceAsync(pubBusinessSubsPlanAppServiceDTO, token);
            return appservices;
        }



        #endregion Get

        public async Task<List<AppServiceAccDetailIdDTO>> UpdateBusinessAppServiceWithServiceAccountDetailInfo(List<AppServiceDTO> updatedAppServiceList, Guid appId, Guid tenantId,int entityTypeEnumValue) {
            
            // Get all already saved business's AppService linking list.
            List<AppServiceAccDetailIdDTO> appServiceAccDetailIdList = new List<AppServiceAccDetailIdDTO>();
            List<TenantAppServiceLinking> savedTenantAppServiceLinkingList = await _tenantAppServiceLinkingDS.GetEntityByAppAndTenantId(appId, tenantId, false);
            List<AppServiceAccountDetail> savedAccountDetailList = null;
            List<TenantAppServiceLinking> deletedTenantAppServiceLinkingList = new List<TenantAppServiceLinking>();

            // Encrypted 13-06-2019
            CryptoHelper cryptoHelper = new CryptoHelper();

            // If zero app service and attributes received in input delete all linking and account details.
            if(updatedAppServiceList == null || updatedAppServiceList.Count == 0) {
                foreach(TenantAppServiceLinking savedTenantAppServiceLining in savedTenantAppServiceLinkingList) {
                    savedTenantAppServiceLining.Deleted = true;
                    _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(savedTenantAppServiceLining, OperationType.Update);
                    _tenantAppServiceLinkingDS.Update(savedTenantAppServiceLining, savedTenantAppServiceLining.ID);

                    // Deletes all account details linked with this service id.
                    //_appServiceAcctDetailDS.DeleteByEntityTypeAndEntityAndServiceAndAttributeId((int)Core.Common.CoreEntityTypeEnum.Tenant, tenantId, savedTenantAppServiceLining.ServiceId, Guid.Empty, Common.OperationType.Update);
                    List<AppServiceAccDetailIdDTO> IdList = await _appServiceAcctDetailDS.DeleteByEntityTypeAndEntityAndServiceId((int)AppMgmtEntityTypeEnum.Tenant, tenantId, savedTenantAppServiceLining.ServiceId, Guid.Empty, OperationType.Update);
                    appServiceAccDetailIdList.AddRange(IdList);
                }
            }
            else {
                foreach(TenantAppServiceLinking savedTenantAppServiceLining in savedTenantAppServiceLinkingList) {
                    
                    // if updated input collection doesn't have any record in saved Service and Attribute linking, delete all linking of current service.
                    if(updatedAppServiceList.Where(i => i.ID.Equals(Guid.Empty) == false)
                        .Any(j => j.ID.Equals(savedTenantAppServiceLining.ServiceId)) == false) {
                    
                        // Delete current service and attribute linking.
                        savedTenantAppServiceLining.Deleted = true;
                        _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(savedTenantAppServiceLining, OperationType.Update);
                        _tenantAppServiceLinkingDS.Update(savedTenantAppServiceLining, savedTenantAppServiceLining.ID);

                        // Deletes all account details linked with this service id.
                        //_appServiceAcctDetailDS.DeleteByEntityTypeAndEntityAndServiceAndAttributeId((int)Core.Common.CoreEntityTypeEnum.Tenant, tenantId, savedTenantAppServiceLining.ServiceId, Guid.Empty, Common.OperationType.Update);
                        List<AppServiceAccDetailIdDTO> IdList = await _appServiceAcctDetailDS.DeleteByEntityTypeAndEntityAndServiceId((int)AppMgmtEntityTypeEnum.Tenant, tenantId, savedTenantAppServiceLining.ServiceId, Guid.Empty, OperationType.Update);
                        appServiceAccDetailIdList.AddRange(IdList);
                    }
                }
            }

            // Refresh latest app service and attribute linking.
            savedTenantAppServiceLinkingList = await _tenantAppServiceLinkingDS.GetEntityByAppAndTenantId(appId, tenantId, false);

            // Loop on updated app service
            foreach(AppServiceDTO updatedAppService in updatedAppServiceList) {
                // Handles all attributes for given services.
                deletedTenantAppServiceLinkingList = new List<TenantAppServiceLinking>();

                foreach(TenantAppServiceLinking savedTenantAppServiceLinking in savedTenantAppServiceLinkingList) {
                    //if(savedTenantAppServiceLinking.ServiceId.Equals(updatedAppService.ID)
                    //    && (updatedAppService.AppServiceAttributeList == null
                    //        || updatedAppService.AppServiceAttributeList.Count == 0
                    //        || updatedAppService.AppServiceAttributeList.Where(i => i.ID.Equals(Guid.Empty) == false)
                    //        .Any(j => j.ID.Equals(savedTenantAppServiceLinking.ServiceAttributeId) == false))) {
                    if(savedTenantAppServiceLinking.ServiceId.Equals(updatedAppService.ID)
                       && (updatedAppService.AppServiceAttributeList == null
                           || updatedAppService.AppServiceAttributeList.Count == 0
                           || updatedAppService.AppServiceAttributeList.Where(i => i.ID.Equals(Guid.Empty) == false)
                           .All(j => j.ID.Equals(savedTenantAppServiceLinking.ServiceAttributeId) == false))) {

                        savedTenantAppServiceLinking.Deleted = true;
                        _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(savedTenantAppServiceLinking, OperationType.Update);
                        _tenantAppServiceLinkingDS.Update(savedTenantAppServiceLinking, savedTenantAppServiceLinking.ID);

                        // Updates all account details to delete=true;
                        //_appServiceAcctDetailDS.DeleteByEntityTypeAndEntityAndServiceAndAttributeId((int)Core.Common.CoreEntityTypeEnum.Tenant, tenantId, savedTenantAppServiceLinking.ServiceId, savedTenantAppServiceLinking.ServiceAttributeId, Common.OperationType.Update);
                    }
                }

                // Refresh latest app service and attribute linking.
                savedTenantAppServiceLinkingList = await _tenantAppServiceLinkingDS.GetEntityByAppAndTenantId(appId, tenantId, false);
                savedAccountDetailList = await _appServiceAcctDetailDS.GetEntityByEntityTypeAndEntityAndServiceAndAttributeId(entityTypeEnumValue, tenantId, updatedAppService.ID, Guid.Empty);
                foreach(AppServiceAttributeDTO updatedAppServiceAttribute in updatedAppService.AppServiceAttributeList) {
                    // Get All Deleted Services that are not passed in input parameters.
                    TenantAppServiceLinking tenantAppServiceLinking = savedTenantAppServiceLinkingList.FirstOrDefault(i => i.ServiceId.Equals(updatedAppService.ID) && i.ServiceAttributeId.Equals(updatedAppServiceAttribute.ID));

                    // Add New business App Service Linking
                    if(tenantAppServiceLinking == null) {
                        TenantAppServiceLinking newAppServiceLinking = new TenantAppServiceLinking();
                        newAppServiceLinking.AppId = appId;
                        newAppServiceLinking.ServiceAttributeId = updatedAppServiceAttribute.ID;
                        newAppServiceLinking.ServiceId = updatedAppService.ID;
                        _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(newAppServiceLinking, OperationType.Add);
                        _tenantAppServiceLinkingDS.Add(newAppServiceLinking);

                    }

                }

                // Update existing business app service linking.
                if(savedAccountDetailList == null) {
                    // Add Account Detail
                    foreach(AppServiceAcctDetailDTO updateAcctDetailDTO in updatedAppService.CarrierServiceAccountDetailList) {
                        AppServiceAccountDetail appServiceAccDetail = new AppServiceAccountDetail();
                        appServiceAccDetail.AppId = appId;
                        appServiceAccDetail.EntityType = entityTypeEnumValue;
                        appServiceAccDetail.EntityId = tenantId;

                        appServiceAccDetail.ServiceId = updatedAppService.ID;
                        appServiceAccDetail.ServiceAttributeId = Guid.Empty;
                        // ToDo: Add Encryption code
                        appServiceAccDetail.AccountJson = cryptoHelper.Encrypt(updateAcctDetailDTO.AccountJson, Common.Constants.DefaultEncryptionAlgo);
                        _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(appServiceAccDetail, OperationType.Add);
                        _appServiceAcctDetailDS.Add(appServiceAccDetail);
                        //create model 
                        AppServiceAccDetailIdDTO appServiceAccDetailId = new AppServiceAccDetailIdDTO();
                        appServiceAccDetailId.Id = appServiceAccDetail.ID;
                        appServiceAccDetailId.SourceEntityId = tenantId;
                        appServiceAccDetailId.operationType = OperationType.Add;
                        appServiceAccDetailId.AccountJson = updateAcctDetailDTO.AccountJson;
                        appServiceAccDetailId.ServiceId = updatedAppService.ID;
                        appServiceAccDetailIdList.Add(appServiceAccDetailId);
                    }
                }

                else {
                    // Deleted all saved account details that are not found in input collection.
                    foreach(AppServiceAccountDetail savedAccountDetail in savedAccountDetailList) {

                        if(updatedAppService.CarrierServiceAccountDetailList == null
                         || updatedAppService.CarrierServiceAccountDetailList.Count == 0
                         || updatedAppService.CarrierServiceAccountDetailList.Where(i => i.ID.Equals(Guid.Empty) == false)
                         .All(j => j.ID.Equals(savedAccountDetail.ID)) == false) {
                            // Updates all account details to delete=true;
                            savedAccountDetail.Deleted = true;
                            // Updates all account details to delete=true;
                            _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(savedAccountDetail, OperationType.Update);
                            _appServiceAcctDetailDS.Update(savedAccountDetail, savedAccountDetail.ID);
                            //create model 
                            AppServiceAccDetailIdDTO appServiceAccDetailId = new AppServiceAccDetailIdDTO();
                            appServiceAccDetailId.Id = savedAccountDetail.ID;
                            appServiceAccDetailId.SourceEntityId = tenantId;
                            appServiceAccDetailId.operationType = OperationType.Delete;
                            appServiceAccDetailId.AccountJson = savedAccountDetail.AccountJson;
                            appServiceAccDetailId.ServiceId = savedAccountDetail.ID;
                            appServiceAccDetailIdList.Add(appServiceAccDetailId);
                        }
                    }
                    foreach(AppServiceAcctDetailDTO updatedAcctDetailDTO in updatedAppService.CarrierServiceAccountDetailList) {
                        // Add new account detail.
                        if(updatedAcctDetailDTO.ID.Equals(Guid.Empty)) {
                            AppServiceAccountDetail appServiceAccDetail = new AppServiceAccountDetail();
                            appServiceAccDetail.AppId = appId;
                            appServiceAccDetail.EntityType = entityTypeEnumValue;
                            appServiceAccDetail.EntityId = tenantId;

                            appServiceAccDetail.ServiceId = updatedAppService.ID;
                            appServiceAccDetail.ServiceAttributeId = Guid.Empty;
                            // ToDo: Add account json encryption code here.
                            appServiceAccDetail.AccountJson = cryptoHelper.Encrypt(updatedAcctDetailDTO.AccountJson, Common.Constants.DefaultEncryptionAlgo);

                            _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(appServiceAccDetail, OperationType.Add);
                            _appServiceAcctDetailDS.Add(appServiceAccDetail);
                            //create model 
                            AppServiceAccDetailIdDTO appServiceAccDetailId = new AppServiceAccDetailIdDTO();
                            appServiceAccDetailId.Id = appServiceAccDetail.ID;
                            appServiceAccDetailId.SourceEntityId = tenantId;
                            appServiceAccDetailId.operationType = OperationType.Add;
                            appServiceAccDetailId.AccountJson = updatedAcctDetailDTO.AccountJson;
                            appServiceAccDetailId.ServiceId = updatedAcctDetailDTO.ID;
                            appServiceAccDetailIdList.Add(appServiceAccDetailId);
                        }
                        else {
                            // Update Account Detail
                            AppServiceAccountDetail appServiceAccDetail = _appServiceAcctDetailDS.Get(updatedAcctDetailDTO.ID);
                            appServiceAccDetail.EntityType = entityTypeEnumValue;
                            appServiceAccDetail.EntityId = tenantId;
                            // ToDo: Add account json encryption code here.
                            appServiceAccDetail.AccountJson = cryptoHelper.Encrypt(updatedAcctDetailDTO.AccountJson, Common.Constants.DefaultEncryptionAlgo);
                            _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(appServiceAccDetail, OperationType.Update);
                            _appServiceAcctDetailDS.Update(appServiceAccDetail, appServiceAccDetail.ID);
                            //create model 
                            AppServiceAccDetailIdDTO appServiceAccDetailId = new AppServiceAccDetailIdDTO();
                            appServiceAccDetailId.Id = appServiceAccDetail.ID;
                            appServiceAccDetailId.SourceEntityId = tenantId;
                            appServiceAccDetailId.operationType = OperationType.Update;
                            appServiceAccDetailId.AccountJson = updatedAcctDetailDTO.AccountJson;
                            appServiceAccDetailId.ServiceId = updatedAcctDetailDTO.ID;
                            appServiceAccDetailIdList.Add(appServiceAccDetailId);
                        }
                    }
                }
            }


            return appServiceAccDetailIdList;
        }

        public async Task UpdateBusinessAppServiceInfoAsync(List<PayAppServiceDetailDTO> updatedAppServiceList, Guid appId, Guid tenantId) {
            // Encrypted 18-09-2019
            CryptoHelper cryptoHelper = new CryptoHelper();

            //Get All Saved Account Detail 
            List<AppServiceAccountDetail> saveAppServiceAccDetailList = new List<AppServiceAccountDetail>();
            saveAppServiceAccDetailList = (await _appServiceAcctDetailDS.FindAllAsync(i => i.TenantId == tenantId && i.AppId == appId)).ToList();

            //Delete Account Detail 
            foreach(AppServiceAccountDetail appServiceAccountDetail in saveAppServiceAccDetailList) {
                if(!updatedAppServiceList.Any(i => i.ID == appServiceAccountDetail.ServiceId)) {
                    await _appServiceAcctDetailDS.DeleteAsync(appServiceAccountDetail.ID);
                }
                }

            foreach(PayAppServiceDetailDTO servicedto in updatedAppServiceList) {
                if(servicedto.AppServiceAttributeList != null) {
                    foreach(PayAppServiceAttributeDetailDTO attributedto in servicedto.AppServiceAttributeList) {
                        if(attributedto.AppServiceAccountList != null && attributedto.AppServiceAccountList.Count > 0) {
                            if(attributedto.AppServiceAccountList[0].AccountJson != null) {

                            AppServiceAccountDetail appServiceAccDetail = new AppServiceAccountDetail();
                            appServiceAccDetail = await _appServiceAcctDetailDS.GetAsync(attributedto.AppServiceAccountList[0].ID);
                            if(appServiceAccDetail == null) {

                                // Add Account Detail
                                appServiceAccDetail = new AppServiceAccountDetail();
                                appServiceAccDetail.ServiceId = servicedto.ID;
                                appServiceAccDetail.ServiceAttributeId = attributedto.ID;
                                appServiceAccDetail.AccountJson = attributedto.AppServiceAccountList[0].AccountJson;
                                //appServiceAccDetail.AccountJson = cryptoHelper.Encrypt(attributedto.AppServiceAccountList[0].AccountJson, Common.Constants.DefaultEncryptionAlgo);
                                appServiceAccDetail.AppId = appId;
                                appServiceAccDetail.EntityType = (int)AppMgmtEntityTypeEnum.Tenant;
                                appServiceAccDetail.EntityId = tenantId;
                                appServiceAccDetail.ID = attributedto.AppServiceAccountList[0].ID;

                                _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(appServiceAccDetail, OperationType.Add);
                                    appServiceAccDetail.ID = attributedto.AppServiceAccountList[0].ID;
                                    appServiceAccDetail.TenantId = tenantId;

                                    await _appServiceAcctDetailDS.AddAsync(appServiceAccDetail);
                            }
                            else {

                                // Add Account Detail

                                appServiceAccDetail.ServiceId = servicedto.ID;
                                appServiceAccDetail.ServiceAttributeId = attributedto.ID;
                                appServiceAccDetail.AccountJson = attributedto.AppServiceAccountList[0].AccountJson;
                                //appServiceAccDetail.AccountJson = cryptoHelper.Encrypt(attributedto.AppServiceAccountList[0].AccountJson, Common.Constants.DefaultEncryptionAlgo);
                                appServiceAccDetail.AppId = appId;
                                appServiceAccDetail.EntityType = (int)AppMgmtEntityTypeEnum.Tenant;
                                appServiceAccDetail.EntityId = tenantId;


                                _appServiceAcctDetailDS.UpdateSystemFieldsByOpType(appServiceAccDetail, OperationType.Update);

                                await _appServiceAcctDetailDS.UpdateAsync(appServiceAccDetail, appServiceAccDetail.ID);


                            }
                            //Save AppService Account Detail 
                            await _appServiceAcctDetailDS.SaveAsync();

                                if(servicedto.ServiceKey == "VeriCheck" && attributedto.AttributeKey == "ACHPayments") {
                                    BusVCACHPayAttrDTO BusVCACHPayAttrDTO = ParsingHelper.ParseJSONToObject<BusVCACHPayAttrDTO>(appServiceAccDetail.AccountJson);
                                    if(BusVCACHPayAttrDTO != null) {
                                        await UpdateVCMerchantAsync(BusVCACHPayAttrDTO, tenantId, appId);
                                    }
                                }
                                if(servicedto.ServiceKey == "TSYS" && attributedto.AttributeKey == "CreditCardPayments") {
                                    BusTSYSCreditCardPayAttrDTO BusTSYSCreditCardPayAttrDTO = ParsingHelper.ParseJSONToObject<BusTSYSCreditCardPayAttrDTO>(appServiceAccDetail.AccountJson);
                                    object ob =  await GetAuthenticationMerchantKeyAsync(BusTSYSCreditCardPayAttrDTO, tenantId, appId);
                                    //string transactionkey = "Transaction";
                                    //string transactionKeyID = ob.GetType().GetProperty(transactionkey).GetValue(ob, null).ToString();
                                    if(BusTSYSCreditCardPayAttrDTO != null) {
                                        await UpdateTSYSMerchantAsync(BusTSYSCreditCardPayAttrDTO, tenantId, appId);
                                    }
                                }
                            }
                    }
                    }
                }                
            }
        
        }

       

        /// <summary>
        /// Adds VC Merchant.
        /// </summary>
        /// <param name="e"> entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task UpdateVCMerchantAsync(BusVCACHPayAttrDTO e, Guid tenantId, Guid appId) {
           
            
            HttpClient client = new HttpClient();
            string url = _appMgmtAppSettings.VeriCheckApiUrl + "Merchant/addupdate";
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);
            VCMerchantDTO merchant = new VCMerchantDTO();
            merchant.BusinessId = tenantId;
            merchant.TenantId = tenantId;
            merchant.VCId = e.VeriCheckID;
            UserSession session = _userSessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", appId.ToString());
            headerParameters.Add(appidKey);


            //string bearerToken = await reqProc.GetBearerTokenAsync(_appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl);

            //client.SetBearerToken(bearerToken);
            object paymentDTO = null;
            try {
                paymentDTO = await reqProc.ExecutePUTRequestAsync<object, VCMerchantDTO>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, merchant);
            }
            catch(Exception ex) {
                StringBuilder errorBuilder = new StringBuilder();
                errorBuilder.Append("Error occurred in payment connector api call");
                errorBuilder.AppendLine("Connector Url: " + url);
                errorBuilder.AppendLine("Business Id: " + merchant.BusinessId.ToString());
                errorBuilder.AppendLine("Tenant Id: " + merchant.TenantId.ToString());
                errorBuilder.AppendLine("VC Id: " + merchant.VCId.ToString());
                errorBuilder.AppendLine(ex.Message);
                errorBuilder.AppendLine(ex.StackTrace);
                _loggerService.LogError(errorBuilder.ToString());
                EwpError ewp = JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
            }
        }

        /// <summary>
        /// Adds VC Merchant.
        /// </summary>
        /// <param name="e"> entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task UpdateTSYSMerchantAsync(BusTSYSCreditCardPayAttrDTO e, Guid tenantId, Guid appId) {


            HttpClient client = new HttpClient();
            string url = _appMgmtAppSettings.TSYSApiUrl + "Merchant";
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);
            MerchantDTO merchant = new MerchantDTO();
            merchant.MID = e.MID;
            merchant.UserId = e.UserId;
            merchant.Password = e.Password;
            merchant.TransactionKey = "TAV9LJOWVIS352PURZOKA6NACOYMJZF2";
            merchant.Active = false;
            merchant.DeveloperId = null;
            merchant.DeviceId = "88700000317201";
            UserSession session = _userSessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", appId.ToString());
            headerParameters.Add(appidKey);


            //string bearerToken = await reqProc.GetBearerTokenAsync(_appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl);

            //client.SetBearerToken(bearerToken);
            object paymentDTO = null;
            try {
                paymentDTO = await reqProc.ExecutePOSTRequestAsync<object, MerchantDTO>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, merchant);
            }
            catch(Exception ex) {
                StringBuilder errorBuilder = new StringBuilder();
                errorBuilder.Append("Error occurred in payment connector api call");
                errorBuilder.AppendLine("Connector Url: " + url);
                errorBuilder.AppendLine("MID: " + merchant.MID.ToString());
                errorBuilder.AppendLine("UserID: " + merchant.UserId.ToString());               
                errorBuilder.AppendLine(ex.Message);
                errorBuilder.AppendLine(ex.StackTrace);
                _loggerService.LogError(errorBuilder.ToString());
                EwpError ewp = JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
            }
        }

        /// <summary>
        /// Adds VC Merchant.
        /// </summary>
        /// <param name="e"> entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<object> GetAuthenticationMerchantKeyAsync(BusTSYSCreditCardPayAttrDTO e, Guid tenantId, Guid appId) {


            HttpClient client = new HttpClient();
            string url = _appMgmtAppSettings.TSYSApiUrl + "Authentication/key/detail";
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);
            AuthenticationRequestDTO authenticationRequestDTO = new AuthenticationRequestDTO();
            GenerateTokenDTO generateKey = new GenerateTokenDTO();
            generateKey.mid = e.MID;
            generateKey.userID = e.UserId;
            generateKey.password = e.Password;
            authenticationRequestDTO.GenerateKey = generateKey;
            //merchant.TransactionKey = null;
            //merchant.Active = false;
            UserSession session = _userSessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
            headerParameters.Add(tenantKey);

            KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
            headerParameters.Add(appUserIdKey);

            KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", appId.ToString());
            headerParameters.Add(appidKey);


            //string bearerToken = await reqProc.GetBearerTokenAsync(_appMgmtAppSettings.AppName, _appMgmtAppSettings.IdentityServerUrl);

            //client.SetBearerToken(bearerToken);
            object paymentDTO = null;
            try {
               return paymentDTO = await reqProc.ExecutePOSTRequestAsync<object, AuthenticationRequestDTO>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, authenticationRequestDTO);
            }
            catch(Exception ex) {
                StringBuilder errorBuilder = new StringBuilder();
                errorBuilder.Append("Error occurred in payment connector api call");
                errorBuilder.AppendLine("Connector Url: " + url);
                errorBuilder.AppendLine("MID: " + generateKey.mid.ToString());
                errorBuilder.AppendLine("UserID: " + generateKey.userID.ToString());
                errorBuilder.AppendLine(ex.Message);
                errorBuilder.AppendLine(ex.StackTrace);
                _loggerService.LogError(errorBuilder.ToString());
                EwpError ewp = JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return null;
            }
        }


        private void RaiseException(EwpError ewp, Exception ex) {
            switch(ewp.ErrorType) {
                case ErrorType.Validation:
                    string message = ewp.MessageList.Count > 0 ? ewp.MessageList[0] : "";
                    EwpValidationException valid = new EwpValidationException(message, ewp.EwpErrorDataList);
                    throw valid;
                default:
                    throw ex;
            }
        }

    }
}
