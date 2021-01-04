using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS {
    public class TenantForBusinessDS:ITenantForBusinessDS {

        #region Local Variables

        ITenantDS _tenantDS;
        ILogger<TenantForBusinessDS> _loggerService;
        ITenantUserDS _tenantUserDS;
        ITenantSubscriptionDS _tenantSubscriptionDS;
        ITenantAppServiceLinkingDS _tenantAppServiceLinkingDS;
        IUserSessionManager _userSessionManager;
        ITenantLinkingDS _tenantLinkingDS;
        IServiceAccountDetailDS _serviceAccountDetailDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        IAppDS _appDS;
        IUnitOfWork _unitOfWork;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Constructor with services classes.
        /// </summary>
        /// <param name="tenantDS"></param>
        /// <param name="tenantUserDS"></param>
        /// <param name="tenantSubscriptionDS"></param>
        /// <param name="tenantAppServiceLinkingDS"></param>
        /// <param name="identityNumbersDS"></param>
        /// <param name="roleDS"></param>
        /// <param name="userSessionManager"></param>
        /// <param name="tenantLinkingDS"></param>
        /// <param name="subscriptionPlanDS"></param>
        /// <param name="appDS"></param>
        /// <param name="userTenantLinkingDS"></param>
        /// <param name="roleLinkingDS"></param>
        /// <param name="tenantUserAppLinkingDS"></param>
        public TenantForBusinessDS(ITenantDS tenantDS, ITenantSubscriptionDS tenantSubscriptionDS, ITenantAppServiceLinkingDS tenantAppServiceLinkingDS,
                               ITenantUserDS tenantUserDS, IAppDS appDS,
                               IUserSessionManager userSessionManager, ITenantLinkingDS tenantLinkingDS,
                               IServiceAccountDetailDS serviceAccountDetailDS,
                               IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS,
                               ILogger<TenantForBusinessDS> loggerService, IUnitOfWork unitOfWork) {
            _tenantDS = tenantDS;
            _tenantSubscriptionDS = tenantSubscriptionDS;
            _tenantAppServiceLinkingDS = tenantAppServiceLinkingDS;
            _tenantUserDS = tenantUserDS;
            _appDS = appDS;
            _userSessionManager = userSessionManager;
            _tenantLinkingDS = tenantLinkingDS;
            _serviceAccountDetailDS = serviceAccountDetailDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _loggerService = loggerService;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Get Business Method

        ///<inheritdoc/>       
        public async Task<UpdateTenantModelDQ> GetBusinessUpdateModelAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            try {

                UpdateTenantModelDQ dto = await _tenantDS.GetBusinessTenantDetailModelDTOAsync(tenantId, token);
                dto.TenantId = tenantId;

                // Getting application primary user. 
                List<UserShortInfoDQ> usersList = await _tenantSubscriptionDS.GetBusinessPrimaryUsersByBusinessIdAsync(tenantId, token);
                UserShortInfoDQ usInfo = GetBusinessPrimaryUser(usersList);
                if(usInfo != null) {
                    dto.PrimaryUserEmail = usInfo.Email;
                    dto.PrimaryUserFirstName = usInfo.FirstName;
                    dto.PrimaryUserLastName = usInfo.LastName;
                    dto.PrimaryUserId = usInfo.ID;
                }

                UserSession userSession = _userSessionManager.GetSession();

                #region Getting Tenant Subscription

                // Getting the application subscription list.
                List<TenantAppSubscriptionDQ> list = await _tenantSubscriptionDS.GetBusinessSubscriptionByBusinessIdAsync(tenantId, userSession.TenantId);

                // Getting Business home application.
                App appBus = await _appDS.GetAppByAppKeyAsync("biz", token);
                Guid homeAppId = appBus.ID;


                dto.Subscriptions = new List<UpdateBusinessAppSubscriptionDTO>();
                // return all subscription exluding home application.
                //List<TenantAppSubscriptionDQ> filterSubscribtionList = list.FindAll(app => app.AppId != homeAppId); // dto.Subscriptions
                for(int i = 0; i < list.Count; i++) {
                    if(list[i].AppId != homeAppId) {
                        UpdateBusinessAppSubscriptionDTO udModel = UpdateBusinessAppSubscriptionDTO.MapProperties(list[i]);
                        dto.Subscriptions.Add(udModel);
                    }
                }

                #endregion Getting Tenant Subscription Data

                dto.UserActivationDate = dto.JoinedOn;
                // Getting config setting.
                //List<ConnectorConfigDQ> config = await _systemConfigDS.GetBusinessAppConnectorConfigByBusinessIdAsync(tenantId, token);
                // Getting all subscription related child entities like user, config setting etc.
                if(list != null) {
                    // Getting services.
                    /*
                    // TODO: Amit set entity type. 2nd parameter.
                    List<TenantAppServiceDQ> listAppService = await _tenantAppServiceLinkingDS.GetAppServiceByBusinessIdAsync(tenantId, (int)AppMgmtEntityTypeEnum.Tenant, token);

                    #region Mapping TenantSubscription services Data

                    for(int i = 0; i < dto.Subscriptions.Count; i++) {
                        dto.Subscriptions[i].AppSubServices = new List<AppServiceRequestDTO>();
                        Dictionary<Guid, AppServiceRequestDTO> dictAppServiceRequestDTO = new Dictionary<Guid, AppServiceRequestDTO>();
                        // Getting the list of subscribe subservice by a Application.
                        for(int appIndex = 0; appIndex < listAppService.Count; appIndex++) {
                            if(listAppService[appIndex].AppId == dto.Subscriptions[i].AppId) {
                                AppServiceRequestDTO appReq;
                                dictAppServiceRequestDTO.TryGetValue(listAppService[appIndex].AppServiceId, out appReq);
                                if(appReq == null) {
                                    appReq = GenerateAppServiceRequestModel(listAppService[appIndex]);
                                    dictAppServiceRequestDTO.Add(listAppService[appIndex].AppServiceId, appReq);
                                }
                                else {
                                    if(listAppService[appIndex].AttributeId != Guid.Empty) {
                                        AppServiceAttributeRequestDTO attr = new AppServiceAttributeRequestDTO();
                                        attr.Name = listAppService[appIndex].AttributeName;
                                        attr.ID = listAppService[appIndex].AttributeId;

                                        if(!string.IsNullOrEmpty(listAppService[appIndex].AccountJson)) {
                                            listAppService[appIndex].AccountJson = GetDecryptValue(listAppService[appIndex].AccountJson);
                                            /// TODO: Amit. Uncomment code.
                                            //attr.buinessVCACHPayAttrDTO = JsonConvert.DeserializeObject<BusVCACHPayAttrDTO>(listAppService[appIndex].AccountJson);
                                        }
                                        /// TODO: Amit. Uncomment code.
                                        //if(attr.buinessVCACHPayAttrDTO == null)
                                        //    attr.buinessVCACHPayAttrDTO = new BusVCACHPayAttrDTO();
                                        //attr.buinessVCACHPayAttrDTO = new BusVCACHPayAttrDTO();
                                        appReq.AppServiceAttributeDTO.Add(attr);
                                    }
                                }
                            }
                            // Adding all services for that app in subscription.
                            foreach(Guid key in dictAppServiceRequestDTO.Keys) {
                                dto.Subscriptions[i].AppSubServices.Add(dictAppServiceRequestDTO[key]);
                            }

                        }

                        // setting the user.
                        for(int appIndex = 0; appIndex < usersList.Count; appIndex++) {
                            // Adding the admin user set for a application.
                            if(usersList[appIndex].AppId == dto.Subscriptions[i].AppId) {
                                dto.Subscriptions[i].UserId = usersList[appIndex].ID;
                                dto.Subscriptions[i].UserActivationDate = dto.UserActivationDate; //usersList[appIndex].UserActivationDate;
                                break;
                            }

                        }
                    }

                    #endregion Mapping TenantSubscription Services Data
                    */
                    for(int i = 0; i < dto.Subscriptions.Count; i++) {
                        for(int appIndex = 0; appIndex < usersList.Count; appIndex++) {
                            // Adding the admin user set for a application.
                            if(usersList[appIndex].AppId == dto.Subscriptions[i].AppId) {
                                dto.Subscriptions[i].UserId = usersList[appIndex].ID;
                                dto.Subscriptions[i].UserActivationDate = dto.UserActivationDate; //usersList[appIndex].UserActivationDate;
                                break;
                            }

                        }
                    }
                }
                else {
                    dto.Subscriptions = new List<UpdateBusinessAppSubscriptionDTO>();
                }

                return dto;
            }
            catch(Exception ex) {
                throw ex;
            }

            return null;
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
                    //attr.buinessVCACHPayAttrDTO = JsonConvert.DeserializeObject<BusVCACHPayAttrDTO>(appServiceDTO.AccountJson);
                }
                //if(attr.buinessVCACHPayAttrDTO == null)
                //    attr.buinessVCACHPayAttrDTO = new BusVCACHPayAttrDTO();

                reqDTO.AppServiceAttributeDTO.Add(attr);
            }

            return reqDTO;
        }

        #endregion Get Business Method

        #region Support

        private string GetEncryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Encrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        private string GetDecryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Decrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        #endregion Support

        #region Delete

        /// <summary>
        /// Delete business by tenantid.
        /// </summary>
        /// <param name="tenantId">Tenant id of business.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteBusinessTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            Tenant tenant = await _tenantDS.GetAsync(tenantId, token);
            if(tenant != null) {
                tenant.Deleted = true;
                _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
                await _tenantDS.UpdateAsync(tenant, tenant.ID, token);

                List<TenantSubscription> listTS = await _tenantSubscriptionDS.GetTenantSubscriptionListByTenantIdAsync(tenantId, token);
                if(listTS != null) {
                    for(int i = 0; i < listTS.Count; i++) {
                        listTS[i].Deleted = true;
                        _tenantSubscriptionDS.UpdateSystemFieldsByOpType(listTS[i], OperationType.Update);
                        await _tenantSubscriptionDS.UpdateAsync(listTS[i], listTS[i].ID, token);
                    }
                }

                //TenantLinking tenantLinking = await _tenantLinkingDS.GetTenantLinkingByTenantIdAndTypeAsync(tenantId, TenantType.Buisness, token);
                //if(tenantLinking != null) {
                //    tenantLinking.Deleted = true;
                //    _tenantLinkingDS.UpdateSystemFieldsByOpType(tenantLinking, OperationType.Update);
                //    await _tenantLinkingDS.UpdateAsync(tenantLinking, tenantLinking.ID, token);
                //}
                _unitOfWork.SaveAll();
            }
        }

        #endregion Delete

    }
}
