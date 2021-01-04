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
    /// <summary>
    /// Contains method to update business tenant and supportive entities.
    /// </summary>
    public class TenantUpdateForBusinessDS: ITenantUpdateForBusinessDS {

        #region Local Variables

        ITenantDS _tenantDS;
        ILogger<TenantUpdateForBusinessDS> _loggerService;
        ITenantUserDS _tenantUserDS;
        ITenantSubscriptionDS _tenantSubscriptionDS;
        ITenantAppServiceLinkingDS _tenantAppServiceLinkingDS;
        IUserSessionManager _userSessionManager;
        ITenantLinkingDS _tenantLinkingDS;
        ISubscriptionPlanDS _subscriptionPlanDataService;
        IAppDS _appDS;
        IServiceAccountDetailDS _serviceAccountDetailDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        IUnitOfWork _unitOfWork;
        IIdentityServerDS _identityServerDS;

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
        public TenantUpdateForBusinessDS(ITenantDS tenantDS, ITenantSubscriptionDS tenantSubscriptionDS, ITenantAppServiceLinkingDS tenantAppServiceLinkingDS,
                               ITenantUserDS tenantUserDS, IIdentityServerDS identityServerDS,
                               IUserSessionManager userSessionManager, ITenantLinkingDS tenantLinkingDS,
                               ISubscriptionPlanDS subscriptionPlanDS, IAppDS appDS, IServiceAccountDetailDS serviceAccountDetailDS,
                               IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS,                               
                               ILogger<TenantUpdateForBusinessDS> loggerService, IUnitOfWork unitOfWork) {
            _tenantDS = tenantDS;
            _tenantSubscriptionDS = tenantSubscriptionDS;
            _tenantAppServiceLinkingDS = tenantAppServiceLinkingDS;
            _tenantUserDS = tenantUserDS;
            _userSessionManager = userSessionManager;
            _tenantLinkingDS = tenantLinkingDS;
            _subscriptionPlanDataService = subscriptionPlanDS;
            _appDS = appDS;
            _serviceAccountDetailDS = serviceAccountDetailDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _loggerService = loggerService;
            _unitOfWork = unitOfWork;
            _identityServerDS = identityServerDS;
        }

        #endregion Constructor

        #region Validation

        /// <summary>
        /// Validating tenant entity with all broken rules, If any define rules faildd then entity raising exception.
        /// </summary>
        /// <param name="tenant"></param>
        private async Task ValidateTenantAsync(Tenant tenant, CancellationToken token = default(CancellationToken)) {
            IList<EwpErrorData> listErrData;
            // validating tenant entity.
            if(tenant.Validate(out listErrData)) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Validation;
                error.EwpErrorDataList = listErrData;
                EwpValidationException exc = new EwpValidationException("Tenant validation error.", error.EwpErrorDataList);
                throw exc;
            }
            if(await _tenantDS.IsSubdomainExistAsync(tenant.SubDomainName, tenant.ID, token)) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Duplicate;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)DuplicateErrorSubType.None;
                errorData.Message = "Duplicate AdminEmail";
                error.EwpErrorDataList.Add(errorData);
                EwpDuplicateNameException exc = new EwpDuplicateNameException(AppMgmt.Common.Constants.DomainNameDuplicateMessage, error.EwpErrorDataList);
                throw exc;
            }
        }

        private bool ValidateTenantAdd(BusinessAppSubscriptionDTO tenantRegistrtionDTO) {
            // Validating tenant some common data in add/update operation. 
            ValidateTenantCommonData(tenantRegistrtionDTO);

            return true;
        }

        /// <summary>
        /// Validating tenant model object.
        /// </summary>
        /// <param name="tenantRegistrtionDTO"></param>
        /// <returns></returns>
        private bool ValidateTenantCommonData(BusinessAppSubscriptionDTO tenantRegistrtionDTO) {
            EwpError error = new EwpError();
            error.ErrorType = ErrorType.Success;
            if(string.IsNullOrEmpty(tenantRegistrtionDTO.AdminFirstName)) {
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.Data = "First name";
                errorData.ErrorSubType = (int)ValidationErrorSubType.FieldRequired;
                errorData.Message = "First name is required.";
                error.EwpErrorDataList.Add(errorData);
                //return false;
            }
            if(string.IsNullOrEmpty(tenantRegistrtionDTO.AdminLastName)) {
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)ValidationErrorSubType.FieldRequired;
                errorData.Data = "Last name";
                errorData.Message = "Last name is required.";
                error.EwpErrorDataList.Add(errorData);
                //return false;
            }
            if(string.IsNullOrEmpty(tenantRegistrtionDTO.AdminEmail)) {
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.Data = "Admin email";
                errorData.ErrorSubType = (int)ValidationErrorSubType.FieldRequired;
                errorData.Message = "Admin email is required.";
                error.EwpErrorDataList.Add(errorData);
                //return false;
            }
            if(error.ErrorType != ErrorType.Success) {
                EwpValidationException exc = new EwpValidationException("Required field error.", error.EwpErrorDataList);
                throw exc;
            }

            return true;
        }

        /// <summary>
        /// Validatiing application user required fields.
        /// If Entity validation fail then raising exception. 
        /// </summary>
        /// <param name="updateBusinessDTO"></param>
        /// <returns></returns>
        private bool ValidateTenantUpdateSubscription(UpdateBusinessAppSubscriptionDTO updateBusinessDTO) {
            EwpError error = new EwpError();
            error.ErrorType = ErrorType.Success;
            if(updateBusinessDTO.SubscriptionPlanId == Guid.Empty) {
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)ValidationErrorSubType.FieldRequired;
                errorData.Message = "Select valid subscription plan.";
                error.EwpErrorDataList.Add(errorData);
                //return false;
            }

            if(error.ErrorType != ErrorType.Success) {
                EwpValidationException exc = new EwpValidationException("Required field error.", error.EwpErrorDataList);
                throw exc;
            }


            return true;
        }

        #endregion Validation

        #region Map Properties    

        private void mapTenantProperties(Tenant t, BusinessSignUpDTO tenantRegistrtionDTO) {
            t.Name = tenantRegistrtionDTO.Name;
            t.SubDomainName = tenantRegistrtionDTO.SubDomainName;
            t.VarId = tenantRegistrtionDTO.VarId;
            t.Language = tenantRegistrtionDTO.Language;
            t.Currency = tenantRegistrtionDTO.Currency;
            t.TimeZone = tenantRegistrtionDTO.TimeZone;
        }

        private Tenant mapUpdateTenantBusinessProperties(Tenant t, UpdateTenantModelDTO tenantRegistrtionDTO) {
            t.Name = tenantRegistrtionDTO.Name;
            t.SubDomainName = tenantRegistrtionDTO.SubDomainName;
            t.VarId = tenantRegistrtionDTO.VarId;
            t.Language = tenantRegistrtionDTO.Language;
            t.Currency = tenantRegistrtionDTO.Currency;
            t.TimeZone = tenantRegistrtionDTO.TimeZone;
            t.Active = tenantRegistrtionDTO.Active;

            return t;
        }

        #endregion Map Properties 

        #region Update Business 

        ///<inheritdoc/>
        public async Task<BusinessResponseModelDTO> UpdateBusinessTenantAsync(UpdateTenantModelDTO tenantRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
            // Passing business tenant id and business type to get Tenantlinking.            
            bool isAdd = false;
            BusinessResponseModelDTO response = new BusinessResponseModelDTO();
            response.userAppRelationDTOs = new List<UserAppRelationDTO>();

            // Updating tenant.
            Tenant tenant = await UpdateTenantAsync(tenantRegistrtionDTO, token);
            //response.IsActive = tenant.Active;
            
            //ToDo: nitin-Tenant Subscription logic should be based on operation type.
                        // Adding/updating application subscription for vendor.
            List<UpdateBusinessAppSubscriptionDTO> listSubscription = tenantRegistrtionDTO.Subscriptions;
            // Geeting all subscribed application list.
            List<TenantSubscription> existingSubscriptionLis = await _tenantSubscriptionDS.GetTenantSubscriptionListByTenantIdAsync(tenantRegistrtionDTO.ID, token);
            // Deleting unsubscribed applications. 
            // Method will check if application doesn't come in client request then delete all those subscribed application.
            await DeleteSubscriptionAsync(listSubscription, existingSubscriptionLis);

            Guid appPublisherId = new Guid(Core.BaseService.Constants.PublisherApplicationId);

            Dictionary<Guid, Dictionary<Guid, Guid>> dictAppSubPlanChange = new Dictionary<Guid, Dictionary<Guid, Guid>>();
            // conatins list of apps are changing the active/inactive state.
            List<TenantSubscription> listSubsActiveInactive = new List<TenantSubscription>();

            #region Subscription add/update

            if(listSubscription != null) {
                for(int i = 0; i < listSubscription.Count; i++) {
                    /// Finding whether list exist.
                    TenantSubscription exist = null;
                    exist = existingSubscriptionLis.Find(ex => ex.ID == listSubscription[i].ID);

                    isAdd = exist == null;
                    UpdateBusinessAppSubscriptionDTO businessDTO = tenantRegistrtionDTO.Subscriptions[i];
                    ValidateTenantUpdateSubscription(businessDTO);
                    // checking whethere subscription plan has been changed for existing app subscription.
                    if(!isAdd && exist.SubscriptionPlanId != listSubscription[i].SubscriptionPlanId) {
                        Dictionary<Guid, Guid> dictPlanCHange = new Dictionary<Guid, Guid>();
                        dictPlanCHange.Add(exist.SubscriptionPlanId, listSubscription[i].SubscriptionPlanId);
                        dictAppSubPlanChange.Add(listSubscription[i].AppId, dictPlanCHange);
                    }
                    // Mapping subscription entity.
                    TenantSubscription subscription = MapSubscriptionFromBusiness(businessDTO, exist);

                    if(isAdd) {
                        // If app was not subscribed previously then subscibing for that application.
                        _tenantSubscriptionDS.UpdateSystemFieldsByOpType(subscription, OperationType.Add);
                        subscription.TenantId = tenant.ID;
                        subscription.Status = 1;
                        await _tenantSubscriptionDS.AddAsync(subscription, token);

                        App app = await _appDS.GetAsync(subscription.AppId);
                        UserAppRelationDTO userAppRelationDTO = new UserAppRelationDTO();
                        userAppRelationDTO.AppId = app.ID;
                        userAppRelationDTO.AppKey = app.AppKey;
                        response.userAppRelationDTOs.Add(userAppRelationDTO);
                        // Asign application to primary user.
                        await AssignApplicationBusinessPrimaryUser(tenantRegistrtionDTO.PrimaryUserId, subscription.TenantId, subscription.AppId, tenantRegistrtionDTO.CreatedBy);
                    }
                    else {
                        _tenantSubscriptionDS.UpdateSystemFieldsByOpType(subscription, OperationType.Update);
                        subscription.TenantId = tenant.ID;
                        await _tenantSubscriptionDS.UpdateAsync(subscription, subscription.ID, token);
                        if(subscription.Status != exist.Status) {
                            listSubsActiveInactive.Add(subscription);
                        }
                    }
                    /*                   
                    List<TenantAppServiceLinking> listAppExistList = await _tenantAppServiceLinkingDS.GetAppServiceByTenantIdAndAppIdAsync(tenant.ID, subscription.AppId, token);
                    // Adding/Updating subservice selected for a tenant.
                    await UpdateTenantAppSubServicesAsync(tenantRegistrtionDTO, businessDTO, tenant, listAppExistList, isAdd, token);
                    */
                }
            }

            #endregion Subscription add/update

            _unitOfWork.SaveAll();

            response.Id = tenant.ID;
            response.IsSuccess = true;

            return response;
        }

        /// <summary>
        /// Add tenant/tenantlinking/business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>    
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<Tenant> UpdateTenantAsync(UpdateTenantModelDTO businessRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
            Tenant tenant = await _tenantDS.GetAsync(businessRegistrtionDTO.TenantId, token);
            tenant = mapUpdateTenantBusinessProperties(tenant, businessRegistrtionDTO);
            // Setting all system fields property default value.
            _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Update);
            tenant.TenantType = (int)TenantType.Buisness;
            // Validating tenant.
            await ValidateTenantAsync(tenant, token);
            // TODO: Rajesh
            //CheckSecurityOnUpdate();
            // Updating tenant.
            tenant = await _tenantDS.UpdateAsync(tenant, businessRegistrtionDTO.ID, token);

            return tenant;
        }

        #endregion Update Business 

        #region Update Tenant child entities

        /// <summary>
        /// Adding configuration for tenant application.
        /// </summary>
        /// <param name="businessDTO">Model of client object.</param>
        /// <param name="tenant">Business tenant</param>
        private async Task UpdateTenantAppSubServicesAsync(UpdateTenantModelDTO tenaneModel, UpdateBusinessAppSubscriptionDTO businessDTO, Tenant tenant, List<TenantAppServiceLinking> existingAppServiceList, bool isNewAppSubscription, CancellationToken token) {

            List<AppServiceRequestDTO> appServiceDTOList = businessDTO.AppSubServices;
            //List<TenantAppServiceDTO> existingAppServiceList = isNewAppSubscription ? null : _tenantAppServiceLinkingDS.GetAppServiceByBusinessId(tenant.ID);
            TenantAppServiceLinking tenantAppService;
            for(int j = 0; j < appServiceDTOList.Count; j++) {
                TenantAppServiceLinking exist = null;
                List<AppServiceAttributeRequestDTO> listAttrDTO = businessDTO.AppSubServices[j].AppServiceAttributeDTO;
                for(int i = 0; i < listAttrDTO.Count; i++) {

                    if(existingAppServiceList != null)
                        exist = existingAppServiceList.Find(ex => ex.AppId == businessDTO.AppId && ex.ServiceId == appServiceDTOList[j].ID && ex.ServiceAttributeId == listAttrDTO[i].ID);

                    if(exist == null) {
                        tenantAppService = new TenantAppServiceLinking();
                        _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(tenantAppService, OperationType.Add);
                        tenantAppService.TenantId = tenant.ID;
                        tenantAppService.AppId = businessDTO.AppId;
                        tenantAppService.ServiceId = businessDTO.AppSubServices[j].ID;
                        tenantAppService.ServiceAttributeId = listAttrDTO[i].ID;
                        await _tenantAppServiceLinkingDS.AddAsync(tenantAppService, token);
                    }
                    if(businessDTO.AppSubServices[j].Name == "VeriCheck" && businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].Name == "ACH Payments") {
                        AppServiceAccountDetail srvcActDtl = new AppServiceAccountDetail();
                        if(exist != null) {
                            _serviceAccountDetailDS.UpdateSystemFieldsByOpType(srvcActDtl, OperationType.Update);
                            srvcActDtl.ID = exist.ID;
                            //await UpdateVCMerchantAsync(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].buinessVCACHPayAttrDTO, tenant);
                        }
                        else {
                            _serviceAccountDetailDS.UpdateSystemFieldsByOpType(srvcActDtl, OperationType.Add);
                            //await AddVCMerchantAsync(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].buinessVCACHPayAttrDTO, tenant);
                        }
                        srvcActDtl.AppId = businessDTO.AppId;
                        srvcActDtl.TenantId = tenant.ID;
                        srvcActDtl.ServiceAttributeId = listAttrDTO[i].ID;
                        srvcActDtl.ServiceId = businessDTO.AppSubServices[j].ID;
                        srvcActDtl.EntityId = tenant.ID;
                        srvcActDtl.EntityType = tenaneModel.EntityType;  //(int)CoreEntityTypeEnum.Tenant;
                        if(listAttrDTO[i] == null) { // || string.IsNullOrEmpty(listAttrDTO[i].buinessVCACHPayAttrDTO.VeriCheckID)
                            //srvcActDtl.AccountJson = JsonConvert.SerializeObject(new BusVCACHPayAttrDTO());
                        }
                        else {
                            srvcActDtl.AccountJson = JsonConvert.SerializeObject(listAttrDTO[i].buinessVCACHPayAttrDTO);
                        }
                        srvcActDtl.AccountJson = GetEncryptValue(srvcActDtl.AccountJson);
                        if(exist != null) {
                            //await _serviceAccountDetailDS.UpdateAsync(srvcActDtl, srvcActDtl.ID);
                            await _serviceAccountDetailDS.UpdateAccountDetailJsonAsync(srvcActDtl.AppId, srvcActDtl.ServiceId, srvcActDtl.ServiceAttributeId, srvcActDtl.EntityId, srvcActDtl.AccountJson, token);
                        }
                        else
                            await _serviceAccountDetailDS.AddAsync(srvcActDtl, token);
                    }
                }

            }
            // Deleting the app service.
            if(!isNewAppSubscription) {
                for(int j = 0; j < existingAppServiceList.Count; j++) {

                    // appServiceDTOList, Its the list coming in Updated request model.
                    if(appServiceDTOList == null || appServiceDTOList.Count == 0) {
                        //await _tenantAppServiceLinkingDS.DeleteAppServiceLinkingByAppServiceAndAttributeAsync(existingAppServiceList[j].AppId, existingAppServiceList[j].AppServiceId, tenant.ID, token);
                        await _tenantAppServiceLinkingDS.DeleteAsync(existingAppServiceList[j].ID, token);
                    }
                    else {
                        AppServiceRequestDTO reqDelete = appServiceDTOList.Find(exist => exist.ID == existingAppServiceList[j].ServiceId);
                        if(reqDelete == null) {
                            await _tenantAppServiceLinkingDS.DeleteAsync(existingAppServiceList[j].ID, token);
                            continue;
                        }
                        for(int i = 0; i < appServiceDTOList.Count; i++) {
                            int valueJ = j;
                            int valuei = i;
                            List<AppServiceAttributeRequestDTO> listAttrDTO = appServiceDTOList[i].AppServiceAttributeDTO;
                            AppServiceAttributeRequestDTO delete = listAttrDTO.Find(data => existingAppServiceList[j].ServiceId == appServiceDTOList[i].ID && data.ID == existingAppServiceList[j].ServiceAttributeId);
                            if(delete == null && existingAppServiceList[j].AppId == businessDTO.AppId && existingAppServiceList[j].ServiceId == appServiceDTOList[i].ID) {
                                await _tenantAppServiceLinkingDS.DeleteAsync(existingAppServiceList[j].ID, token);
                                //await _tenantAppServiceLinkingDS.DeleteAppServiceLinkingByAppServiceAndAttributeAsync(existingAppServiceList[j].AppId, existingAppServiceList[j].AppServiceId, existingAppServiceList[j].AttributeId, tenant.ID, token);
                            }

                        }
                    }

                }
            }
        }

        /// <summary>
        /// Delete the subscription list if didn't get from client.
        /// </summary>
        /// <param name="clientSubscriptionList"></param>
        /// <param name="existingSubscriptionLis"></param>
        private async Task DeleteSubscriptionAsync(List<UpdateBusinessAppSubscriptionDTO> clientSubscriptionList, List<TenantSubscription> existingSubscriptionLis) {
            TenantSubscription subs = new TenantSubscription();
            Guid homeAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            for(int j = 0; j < existingSubscriptionLis.Count; j++) {
                UpdateBusinessAppSubscriptionDTO delete = null;

                if(clientSubscriptionList != null) {
                    delete = clientSubscriptionList.Find(data => data.ID == existingSubscriptionLis[j].ID);
                }
                // Do not delete home application.
                if(delete == null && existingSubscriptionLis[j].AppId != homeAppId) {
                    await _tenantSubscriptionDS.DeleteApplicationAsync(existingSubscriptionLis[j].TenantId, existingSubscriptionLis[j].AppId);

                }
            }
        }

        #endregion Update Tenant child entities    

        #region Support

        /// <summary>
        /// Manually mapping subscibe application object fields from model object.
        /// </summary>
        /// <param name="businessSubscription">Subscription model object to map in to entity object.</param>
        /// <param name="sub">subscription object to map value from model object and return it.</param>
        /// <returns>return TenantSubscription object.</returns>
        private TenantSubscription MapSubscriptionFromBusiness(UpdateBusinessAppSubscriptionDTO businessSubscription, TenantSubscription sub) {
            if(sub == null) {
                sub = new TenantSubscription();
                sub.ThemeId = businessSubscription.ThemeId;
            }
            sub.Term = businessSubscription.Term;
            sub.AppId = businessSubscription.AppId;
            sub.AutoRenewal = businessSubscription.AutoRenewal;
            sub.PriceInDollar = businessSubscription.PriceInDollar;
            sub.SubscriptionStartDate = businessSubscription.SubscriptionStartDate;
            sub.SubscriptionStartEnd = businessSubscription.SubscriptionStartEnd;
            sub.SubscriptionPlanId = businessSubscription.SubscriptionPlanId;
            sub.PaymentCycle = businessSubscription.PaymentCycle;
            sub.BusinessUserCount = businessSubscription.BusinessUserCount;
            sub.ID = businessSubscription.ID;
            sub.CustomizeSubscription = businessSubscription.CustomizeSubscription;
            sub.Status = businessSubscription.Status;
            sub.InactiveComment = businessSubscription.InactiveComment;

            sub.OneTimePlan = businessSubscription.OneTimePlan;
            sub.ShipmentCount = businessSubscription.ShipmentCount;
            sub.ShipmentUnit = businessSubscription.ShipmentUnit;
            sub.UserPerCustomerCount = businessSubscription.UserPerCustomerCount;
            sub.CustomerUserCount = businessSubscription.CustomerUserCount;

            return sub;
        }

        private string GetEncryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Encrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        private string GetDecryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Decrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        #endregion Support

        private async Task AssignApplicationBusinessPrimaryUser(Guid tenantUserId, Guid tenantId, Guid appId, Guid createdBy) {

            #region Asign application (TenantUserAppLinking)
            //ToDo: nitin-This is case of update tenant so tenant user can't be null.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserId);
            if(tenantUser != null) {
                TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(tu => tu.TenantUserId == tenantUserId && tu.AppId == appId && tu.TenantId == tenantId && tu.Deleted == false);
                if(tenantUserAppLinking == null) {

                    await _identityServerDS.AssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, UserTypeEnum.Business.ToString());

                    tenantUserAppLinking = new TenantUserAppLinking();
                    tenantUserAppLinking.ID = Guid.NewGuid();
                    tenantUserAppLinking.Deleted = false;
                    tenantUserAppLinking.TenantId = tenantId;
                    tenantUserAppLinking.AppId = appId;
                    tenantUserAppLinking.TenantUserId = tenantUserId;
                    tenantUserAppLinking.UserType = (int)UserTypeEnum.Business;
                    tenantUserAppLinking.Active = true;
                    tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                    //ToDo: nitin- Use  system field update method.
                    tenantUserAppLinking.CreatedBy = createdBy;
                    tenantUserAppLinking.CreatedOn = DateTime.UtcNow;
                    tenantUserAppLinking.UpdatedBy = tenantUserAppLinking.CreatedBy;
                    tenantUserAppLinking.UpdatedOn = tenantUserAppLinking.CreatedOn;
                    tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                    tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                    tenantUserAppLinking.BusinessPartnerTenantId = null;
                    await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking);
                }

                #endregion Asign application (TenantUserAppLinking)
            }
        }

    }
}
