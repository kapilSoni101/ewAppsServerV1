using System;
using System.Collections.Generic;
using System.Linq;
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
    /// A wrapper class provide support method for tenant registration.
    /// </summary>
    public class TenantSignUpForBusinessDS:ITenantSignUpForBusinessDS {

        #region Local Variables

        ITenantDS _tenantDS;
        ILogger<TenantSignUpForBusinessDS> _loggerService;
        ITenantUserDS _tenantUserDS;
        ITenantSubscriptionDS _tenantSubscriptionDS;
        ITenantAppServiceLinkingDS _tenantAppServiceLinkingDS;
        IUniqueIdentityGeneratorDS _identityNumbersDS;
        IUserSessionManager _userSessionManager;
        ITenantLinkingDS _tenantLinkingDS;
        ISubscriptionPlanDS _subscriptionPlanDataService;
        IAppDS _appDS;
        IServiceAccountDetailDS _serviceAccountDetailDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        IIdentityServerDS _identityServerDS;
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
        public TenantSignUpForBusinessDS(ITenantDS tenantDS, ITenantSubscriptionDS tenantSubscriptionDS, ITenantAppServiceLinkingDS tenantAppServiceLinkingDS,
                               ITenantUserDS tenantUserDS, IUniqueIdentityGeneratorDS identityNumbersDS,
                               IUserSessionManager userSessionManager, ITenantLinkingDS tenantLinkingDS,
                               ISubscriptionPlanDS subscriptionPlanDS, IAppDS appDS, IServiceAccountDetailDS serviceAccountDetailDS,
                               IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS,
                               IIdentityServerDS identityServerDS,
                               ILogger<TenantSignUpForBusinessDS> loggerService, IUnitOfWork unitOfWork) {
            _tenantDS = tenantDS;
            _tenantSubscriptionDS = tenantSubscriptionDS;
            _tenantAppServiceLinkingDS = tenantAppServiceLinkingDS;
            _tenantUserDS = tenantUserDS;
            _identityNumbersDS = identityNumbersDS;
            _userSessionManager = userSessionManager;
            _tenantLinkingDS = tenantLinkingDS;
            _subscriptionPlanDataService = subscriptionPlanDS;
            _appDS = appDS;
            _serviceAccountDetailDS = serviceAccountDetailDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _identityServerDS = identityServerDS;
            _loggerService = loggerService;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor        

        #region Business SignUp

        #region Add

        /// <summary>
        /// Method is used to singup business as well as subscribe the application for business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="token"></param>
        /// <returns>return response model.</returns>
        public async Task<TenantSignUpForBusinessResDTO> BusinessSignupAsync(BusinessSignUpDTO businessRegistrtionDTO, CancellationToken token = default(CancellationToken)) {
            TenantSignUpForBusinessResDTO tenantAddResponseDTO = new TenantSignUpForBusinessResDTO();

            // Add Tenant
            // Add Primary User
            // Add Users
            // Add Subscription
            // Add Role Permission
            // Add Identity User            
            // Add System Configuration            
            // Mapping all property.

            TenantLinking parentTenantLinking = await _tenantLinkingDS.GetTenantLinkingBySubdomainAndTenantTypeAsync(businessRegistrtionDTO.SourceTenantSubDomainName, TenantType.Publisher, token);
            if(parentTenantLinking == null) {
                // Raise exception.
                return null;
            }

            // Getting pub application by key.
            App publisherApp = await _appDS.GetAppByAppKeyAsync(AppKeyEnum.pub.ToString(), token);

            Guid pubAppId = publisherApp.ID;

            Guid publisherTenantId = parentTenantLinking.PublisherTenantId.Value;

            //AppDetailDQ pubAppSetting = await _pubAppSettingDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(pubAppId, publisherTenantId, token);

            UserShortInfoDQ pubPrimaryUserInfo = null;

            UserSession session = _userSessionManager.GetSession();

            if(session == null) {
               // Guid pubHomeAppId = pubAppId; // new Guid(Common.Constants.PublisherApplicationId);
                // Get parent publisher's primary user.
                pubPrimaryUserInfo = await _tenantSubscriptionDS.GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(publisherTenantId, pubAppId, UserTypeEnum.Publisher, token);

                //if(pubPrimaryUserInfo != null) {
                //    pubPrimaryUserInfo.TenantId = publisherTenantId;
                //}
                pubPrimaryUserInfo.TenantId = publisherTenantId;
                businessRegistrtionDTO.CreatedBy = pubPrimaryUserInfo.ID;
            }
            else {
                businessRegistrtionDTO.CreatedBy = session.TenantUserId;
            }


            #region Adding Tenant/Business/Business Subscription Data

            // Adding tenant and its linked  entity.
            Tenant tenant = await AddTenantAndSupportEntity(businessRegistrtionDTO, parentTenantLinking, pubPrimaryUserInfo, token);
            tenantAddResponseDTO.TenantId = tenant.ID;
            tenantAddResponseDTO.PublisherTenantId = publisherTenantId;
            tenantAddResponseDTO.BusinessApplicationId = pubAppId;
            tenantAddResponseDTO.CreatedBy = tenant.CreatedBy;
            tenantAddResponseDTO.CreatedOn = tenant.CreatedOn.Value;
            ResponseModelDTO response = new ResponseModelDTO();


            #endregion Adding Tenant/Business Application Subscription Data


            #region Adding Tenant Subscription/related Data

            Guid busAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            // Adding a business application subscription entity.
            TenantSubscription adminBusSubscriptionEntity = await AddAdminTenantSubscription(businessRegistrtionDTO, tenant, busAppId, pubPrimaryUserInfo, token);

            List<TenantSubscription> listSubscription = new List<TenantSubscription>();
            if(businessRegistrtionDTO.Subscriptions != null) {
                // Adding subscribe application and its user.
                TenantSubscription[] subscription = BusinessAppSubscriptionDTO.MapModelArrayToEntityArray(businessRegistrtionDTO.Subscriptions);// _mapper.Map<TenantSubscription[]>(businessRegistrtionDTO.Subscriptions);
                for(int i = 0; i < subscription.Length; i++) {
                    subscription[i].Status = 1;
                    //userReponse = null;
                    BusinessAppSubscriptionDTO businessDTO = businessRegistrtionDTO.Subscriptions[i];
                    // Validating the application with all required fields.
                    ValidateTenantAdd(businessDTO);



                    if(session == null && pubPrimaryUserInfo != null) {
                        subscription[i].CreatedBy = pubPrimaryUserInfo.ID;
                        subscription[i].UpdatedBy = pubPrimaryUserInfo.ID;
                        subscription[i].CreatedOn = DateTime.UtcNow;
                        subscription[i].UpdatedOn = DateTime.UtcNow;
                    }
                    else if(session != null) {
                        _tenantSubscriptionDS.UpdateSystemFieldsByOpType(subscription[i], OperationType.Add);
                    }
                    subscription[i].TenantId = tenant.ID;
                    if(pubPrimaryUserInfo != null) {
                        subscription[i].CreatedBy = pubPrimaryUserInfo.ID;
                        subscription[i].UpdatedBy = pubPrimaryUserInfo.ID;
                    }

                    _tenantSubscriptionDS.Add(subscription[i]);
                    // Adding subservice selected for a tenant.
                    //await AddTenantAppSubServicesAsync(businessDTO, tenant, UserShortInfoCreator, token);
                }
                listSubscription.Add(adminBusSubscriptionEntity);
                listSubscription.AddRange(subscription);
            }
            else {
                listSubscription.Add(adminBusSubscriptionEntity);
            }


            #endregion Add Tenant Subscription/related data


            response.IsSuccess = true;

            //TODO : Ishwar
            #region Add User And Assign application to user

            List<UserAppRelationDTO> userAppRelationDTOList = new List<UserAppRelationDTO>();

            foreach(TenantSubscription item in listSubscription) {
                UserAppRelationDTO dto = new UserAppRelationDTO();
                dto.AppId = item.AppId;
                dto.AppKey = (await _appDS.GetAsync(item.AppId)).AppKey;
                userAppRelationDTOList.Add(dto);
            }

            TenantUser tenantUser = await SignUpBusinessPrimaryUserAsync(businessRegistrtionDTO, userAppRelationDTOList, token);

            #endregion Add User And Assign application to user

            // Commiting all changes in database.
            _unitOfWork.SaveAll();

            tenantAddResponseDTO.UserAppRelationDTOList = new List<UserAppRelationDTO>();
            tenantAddResponseDTO.UserAppRelationDTOList = userAppRelationDTOList;
            tenantAddResponseDTO.TenantUserInfo = TenantUserDTO.MapFromTenantUser(tenantUser);
            return tenantAddResponseDTO;
        }

        #region User

        private async Task<TenantUser> SignUpBusinessPrimaryUserAsync(BusinessSignUpDTO businessRegistrtionDTO, List<UserAppRelationDTO> userAppRelationDTOList, CancellationToken token = default(CancellationToken)) {
            IdentityServerAddUserResponseDTO identityServerAddUserResponseDTO = null;
            TenantUser tenantUser = null;
            UserSession userSession = _userSessionManager.GetSession();

            try {
                #region TenantUser

                // Add TenantUser entity.

                // Check if user with same email id is already exist.
                tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == businessRegistrtionDTO.AdminEmail && tu.Deleted == false);
                if(tenantUser == null) {
                    tenantUser = new TenantUser();
                    tenantUser.ID = businessRegistrtionDTO.GeneratedUserId;
                    tenantUser.Email = businessRegistrtionDTO.AdminEmail;
                    tenantUser.FirstName = businessRegistrtionDTO.AdminFirstName;
                    tenantUser.LastName = businessRegistrtionDTO.AdminLastName;
                    tenantUser.FullName = businessRegistrtionDTO.AdminFirstName + " " + businessRegistrtionDTO.AdminLastName;
                    tenantUser.CreatedBy = businessRegistrtionDTO.CreatedBy;
                    tenantUser.CreatedOn = DateTime.UtcNow;
                    tenantUser.UpdatedBy = tenantUser.CreatedBy;
                    tenantUser.UpdatedOn = tenantUser.CreatedOn;
                    tenantUser = await _tenantUserDS.AddTenantUserAsync(tenantUser);
                }

                #endregion TenantUser

                #region TenantUserLinking

                UserTenantLinking userTenantLinking = new UserTenantLinking();
                userTenantLinking.ID = Guid.NewGuid();
                userTenantLinking.TenantId = businessRegistrtionDTO.GeneratedTenantId;
                userTenantLinking.TenantUserId = tenantUser.ID;
                userTenantLinking.UserType = (int)UserTypeEnum.Business;
                userTenantLinking.IsPrimary = true;
                userTenantLinking.Deleted = false;
                userTenantLinking.PartnerType = null;
                userTenantLinking.BusinessPartnerTenantId = null;
                userTenantLinking.CreatedBy = businessRegistrtionDTO.CreatedBy;
                userTenantLinking.CreatedOn = DateTime.UtcNow;
                userTenantLinking.UpdatedBy = userTenantLinking.CreatedBy;
                userTenantLinking.UpdatedOn = userTenantLinking.CreatedOn;
                await _userTenantLinkingDS.AddAsync(userTenantLinking);

                #endregion TenantUserLinking

                #region Asign application (TenantUserAppLinking)

                foreach(UserAppRelationDTO item in userAppRelationDTOList) {
                    TenantUserAppLinking tenantUserAppLinking = new TenantUserAppLinking();
                    tenantUserAppLinking.ID = Guid.NewGuid();
                    tenantUserAppLinking.Deleted = false;
                    tenantUserAppLinking.TenantId = userTenantLinking.TenantId;
                    tenantUserAppLinking.AppId = item.AppId;
                    tenantUserAppLinking.TenantUserId = tenantUser.ID;
                    tenantUserAppLinking.UserType = userTenantLinking.UserType;
                    tenantUserAppLinking.Active = true;
                    tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                    tenantUserAppLinking.CreatedBy = businessRegistrtionDTO.CreatedBy;
                    tenantUserAppLinking.CreatedOn = DateTime.UtcNow;
                    tenantUserAppLinking.UpdatedBy = tenantUserAppLinking.CreatedBy;
                    tenantUserAppLinking.UpdatedOn = tenantUserAppLinking.CreatedOn;
                    tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                    tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                    tenantUserAppLinking.BusinessPartnerTenantId = null;
                    await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking);
                }

                #endregion Asign application (TenantUserAppLinking)

                #region Add User In Identity Server

                // Add user on identity server.
                string appKeys = string.Join(',', userAppRelationDTOList.Select(u => u.AppKey));

                // Create identityUserDTO for adding user on identity server.
                IdentityUserDTO identityUserDTO = new IdentityUserDTO {
                    FirstName = tenantUser.FirstName,
                    LastName = tenantUser.LastName,
                    ClientAppType = appKeys,
                    Email = tenantUser.Email,
                    IsActive = true,
                    TenantId = userTenantLinking.TenantId,
                    UserType = (int)UserTypeEnum.Business
                };

                identityServerAddUserResponseDTO = await _identityServerDS.AddUserOnIdentityServerAsync(identityUserDTO);

                // Assign identityUseID.
                if(identityServerAddUserResponseDTO.Code != null) {
                    tenantUser.IdentityUserId = identityServerAddUserResponseDTO.UserId;
                    tenantUser.Code = identityServerAddUserResponseDTO.Code;
                    await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);
                }

                #endregion

                _unitOfWork.SaveAll();

                return tenantUser;
            }
            catch(Exception ex) {
                // Log error
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in SignUpTenantAndTenantUserDs.SigupPublisherPrimaryUser:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());

                // To delete the user created on identity server incase of any exception after creating the user.
                if(identityServerAddUserResponseDTO != null) {
                    await _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, businessRegistrtionDTO.GeneratedTenantId);
                }
                throw;
            }
        }

        #endregion User

        #endregion Add

        #region Support Tenant Entity Add

        /// <summary>
        /// Adding subservices for a application subscribed by a tenant(Business).
        /// </summary>
        /// <param name="businessDTO">Business application subscription object.</param>
        /// <param name="tenant">Tenant object.</param>
        /// <param name="UserShortInfoCreator">Business creator user object.</param>
        /// <param name="token"></param>
        private async Task AddTenantAppSubServicesAsync(BusinessAppSubscriptionDTO businessDTO, Tenant tenant, UserShortInfoDQ UserShortInfoCreator, CancellationToken token = default(CancellationToken)) {
            if(businessDTO.AppSubServices != null) {
                TenantAppServiceLinking tenantAppService;
                UserSession session = _userSessionManager.GetSession();
                // Adding subservice selected for a tenant.
                for(int j = 0; j < businessDTO.AppSubServices.Count; j++) {
                    if(businessDTO.AppSubServices[j].AppServiceAttributeDTO == null) {
                        continue;
                    }
                    for(int i = 0; i < businessDTO.AppSubServices[j].AppServiceAttributeDTO.Count; i++) {
                        tenantAppService = new TenantAppServiceLinking();
                        if(session == null && UserShortInfoCreator != null) {
                            tenantAppService.CreatedBy = UserShortInfoCreator.ID;
                            tenantAppService.UpdatedBy = UserShortInfoCreator.ID;
                            tenantAppService.CreatedOn = DateTime.UtcNow;
                            tenantAppService.UpdatedOn = DateTime.UtcNow;
                        }
                        else if(session != null)
                            _tenantAppServiceLinkingDS.UpdateSystemFieldsByOpType(tenantAppService, OperationType.Add);

                        tenantAppService.TenantId = tenant.ID;
                        tenantAppService.AppId = businessDTO.AppId;
                        tenantAppService.ServiceId = businessDTO.AppSubServices[j].ID;
                        tenantAppService.ServiceAttributeId = businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].ID;
                        await _tenantAppServiceLinkingDS.AddAsync(tenantAppService);

                        AppServiceAccountDetail srvcActDtl = new AppServiceAccountDetail();
                        //_serviceAccountDetailDS.UpdateSystemFieldsByOpType(srvcActDtl, OperationType.Add);
                        srvcActDtl.CreatedBy = tenantAppService.CreatedBy;
                        srvcActDtl.UpdatedBy = tenantAppService.UpdatedBy;
                        srvcActDtl.CreatedOn = tenantAppService.CreatedOn;
                        srvcActDtl.UpdatedOn = tenantAppService.UpdatedOn;
                        srvcActDtl.AppId = businessDTO.AppId;
                        srvcActDtl.TenantId = tenant.ID;
                        srvcActDtl.ServiceAttributeId = tenantAppService.ServiceAttributeId;
                        srvcActDtl.ServiceId = tenantAppService.ServiceId;
                        srvcActDtl.EntityId = tenantAppService.TenantId;

                        srvcActDtl.EntityType = (int)AppMgmtEntityTypeEnum.Tenant; //(int)CoreEntityTypeEnum.Tenant;
                        if(businessDTO.AppSubServices[j].Name == "VeriCheck" && businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].Name == "ACH Payments") {
                            if(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i] == null) {
                                /// Always send object as created.                                
                                /// TODO: Amit 
                                //await AddVCMerchantAsync(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].buinessVCACHPayAttrDTO, tenant);
                            }
                            else {
                                srvcActDtl.AccountJson = JsonConvert.SerializeObject(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].buinessVCACHPayAttrDTO);
                                /// TODO: Amit set entity type.
                                //await AddVCMerchantAsync(businessDTO.AppSubServices[j].AppServiceAttributeDTO[i].buinessVCACHPayAttrDTO, tenant);
                            }
                            await _serviceAccountDetailDS.AddAsync(srvcActDtl, token);
                        }
                    }
                }
            }
        }

        #endregion Support Tenant Entity Add

        #region Admin

        /// <summary>
        ///  Add admin user subscription.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="tenant"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<TenantSubscription> AddAdminTenantSubscription(BusinessSignUpDTO businessRegistrtionDTO, Tenant tenant, Guid appId, UserShortInfoDQ userShortInfoCreator, CancellationToken token = default(CancellationToken)) {
            SubscriptionPlan subsPlan = await _subscriptionPlanDataService.GetSubscriptionPlansByAppIdAsync(appId, token);
            TenantSubscription tenantSubscription = new TenantSubscription();
            if(subsPlan != null) {
                tenantSubscription.SubscriptionPlanId = subsPlan.ID;
                //tenantSubscription.AlertFrequency = subsPlan.AlertFrequency;
                //tenantSubscription.AutoRenew = subsPlan.AutoRenewal;
                tenantSubscription.PriceInDollar = subsPlan.PriceInDollar;
                // TODO: Amit
                tenantSubscription.ThemeId = new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560");
            }
            else {
                tenantSubscription.SubscriptionPlanId = Guid.Empty;
            }

            tenantSubscription.AppId = appId;
            tenantSubscription.SubscriptionStartDate = DateTime.UtcNow;
            tenantSubscription.SubscriptionStartEnd = DateTime.UtcNow;
            tenantSubscription.Status = 1;
            if(userShortInfoCreator != null) {
                tenantSubscription.CreatedBy = userShortInfoCreator.ID;
                tenantSubscription.UpdatedBy = userShortInfoCreator.ID;
                tenantSubscription.CreatedOn = DateTime.UtcNow;
                tenantSubscription.UpdatedOn = DateTime.UtcNow;
            }
            else
                _tenantSubscriptionDS.UpdateSystemFieldsByOpType(tenantSubscription, OperationType.Add);
            tenantSubscription.TenantId = tenant.ID;
            await _tenantSubscriptionDS.AddAsync(tenantSubscription, token);

            return tenantSubscription;
        }

        #endregion Add Admin

        #region Add Tenant/TenantLinking

        /// <summary>
        /// Add tenant/tenantlinking/business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="parentTenantLinking"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<Tenant> AddTenantAndSupportEntity(BusinessSignUpDTO businessRegistrtionDTO, TenantLinking parentTenantLinking, UserShortInfoDQ UserShortInfoCreator, CancellationToken token = default(CancellationToken)) {
            Tenant tenant = new Tenant();
            mapTenantProperties(tenant, businessRegistrtionDTO);
            // Setting all system fields property default value.

            tenant.TenantType = (int)TenantType.Buisness;
            if(_userSessionManager.GetSession() == null && UserShortInfoCreator != null) {
                tenant.CreatedBy = UserShortInfoCreator.ID;
                tenant.UpdatedBy = UserShortInfoCreator.ID;
                tenant.CreatedOn = DateTime.UtcNow;
                tenant.UpdatedOn = DateTime.UtcNow;
            }
            else {
                _tenantDS.UpdateSystemFieldsByOpType(tenant, OperationType.Add);
            }
            tenant.InvitedBy = tenant.CreatedBy;
            tenant.InvitedOn = tenant.UpdatedOn;
            // Validating tenant.
            await ValidateTenantAsync(tenant, token);
            tenant.IdentityNumber = "T023842435";
            // setting identity number value.            
            //int identityNumber = _identityNumbersDS.GetIdentityNo(Guid.Empty, businessRegistrtionDTO.EntityType, AppMgmt.Common.Constants.TenantIdPrefix, 100001);
            //tenant.IdentityNumber = AppMgmt.Common.Constants.TenantIdPrefix + identityNumber;
            tenant.ID = businessRegistrtionDTO.GeneratedTenantId;
            // Adding tenant.
            tenant = await _tenantDS.AddAsync(tenant, token);

            // Add tenantlinking
            TenantLinking tl = new TenantLinking();
            tl.BusinessTenantId = tenant.ID;
            tl.PublisherTenantId = parentTenantLinking.PublisherTenantId;
            tl.PlatformTenantId = parentTenantLinking.PlatformTenantId;
            tl.CreatedBy = tenant.CreatedBy;
            tl.UpdatedBy = tenant.UpdatedBy;
            tl.CreatedOn = tenant.CreatedOn;
            tl.UpdatedOn = tenant.UpdatedOn;
            //_tenantLinkingDS.UpdateSystemFields(tl);
            await _tenantLinkingDS.AddAsync(tl, token);
            return tenant;
        }

        #endregion Add Tenant/TenantLinking        

        #endregion Business SignUp            

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

        #endregion Map Properties    

        #region Support

        private string GetEncryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Encrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        private string GetDecryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Decrypt(value, Core.CommonService.CryptoHelper.EncryptionAlgorithm.AES);
        }

        #endregion Support

    }
}
