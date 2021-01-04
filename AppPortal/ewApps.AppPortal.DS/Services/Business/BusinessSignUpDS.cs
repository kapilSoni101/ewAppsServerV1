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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.PreferenceDTO;
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
    public class BusinessSignUpDS:IBusinessSignUpDS {

        #region Local Variables

        AppPortalAppSettings _appSettings;
        IBusinessDS _businessDS;
        IBusinessAddressDS _addressDS;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSetting;
        IUniqueIdentityGeneratorDS _identityDataService;
        IQPublisherAndUserDS _qPublisherDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        IUnitOfWork _unitOfWork;
        IPublisherDS _publisherDS;
        ITenantAppLinkingDS _tenantAppLinkingDS;
        IPublisherNotificationHandler _publisherNotificationHandler;
        IBusUserPreferenceDS _busUserPreferenceDS;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BusinessSignUpDS(IBusinessDS businessDS,
            IBusinessAddressDS addressDS, IUniqueIdentityGeneratorDS identityDataService,
            ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IUserSessionManager userSessionManager, IRoleDS roleDS,
            IRoleLinkingDS roleLinkingDS, IPublisherDS publisherDS, IPublisherNotificationHandler publisherNotificationHandler,
            IQPublisherAndUserDS qPublisherDS, ITenantAppLinkingDS tenantAppLinkingDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appSetting, IBusUserPreferenceDS busUserPreferenceDS) {
            _businessDS = businessDS;
            _addressDS = addressDS;
            _identityDataService = identityDataService;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _userSessionManager = userSessionManager;
            _qPublisherDS = qPublisherDS;
            _unitOfWork = unitOfWork;
            _appSetting = appSetting.Value;
            _roleLinkingDS = roleLinkingDS;
            _roleDS = roleDS;
            _tenantAppLinkingDS = tenantAppLinkingDS;
            _publisherDS = publisherDS;
            _publisherNotificationHandler = publisherNotificationHandler;
            _busUserPreferenceDS = busUserPreferenceDS;
        }

        #endregion Constructor                

        #region Add/Update/Delete

        /// <summary>
        /// Method is used to singup business as well as subscribe the application for business.
        /// </summary>
        /// <param name="businessSignUpDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> BusinessSignUpAsync(BusinessSignUpRequestDTO businessSignUpDTO, CancellationToken token = default(CancellationToken)) {
            //Dictionary<Guid, AddUserResponseDTO> userListToNotified = new Dictionary<Guid, AddUserResponseDTO>();
            // Add Tenant
            // Add Primary User
            // Add Users
            // Add Subscription
            // Add Role Permission
            // Add Identity User            
            // Add System Configuration     

            Guid newBusinessTenantId = Guid.NewGuid();

            BusinessTenantSignUpResponseDTO businessTenantSignUpResponseDTO = new BusinessTenantSignUpResponseDTO();

            // Getting pub application by key.
            Guid pubAppId = new Guid(AppPortal.Common.AppPortalConstants.PublisherApplicationId);

            Guid publisherTenantId = businessSignUpDTO.PublisherTenantId;

            UserSession session = _userSessionManager.GetSession();
            UserShortInfoDQ pubAdminUserInfo = null;

            // Get publisher and it's primary user information.
            PublisherPrimaryUserAndUserDTO pubPrimaryUserDTO = await GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(businessSignUpDTO, pubAppId, (int)UserTypeEnum.Publisher, token);

            // If user with the business email already exist then add all data with that user otherwise generate new userid and add user with that userid.
            if(pubPrimaryUserDTO.UserId == Guid.Empty) {
                pubPrimaryUserDTO.UserId = Guid.NewGuid();
            }

            if(session == null) {
                //  Guid pubHomeAppId = pubAppId; // new Guid(Common.Constants.PublisherApplicationId);
                pubAdminUserInfo = pubPrimaryUserDTO.PublisherAdmin;

                //if(pubAdminUserInfo != null) {
                //    pubAdminUserInfo.TenantId = publisherTenantId;
                //}
                pubAdminUserInfo.TenantId = publisherTenantId;
                businessTenantSignUpResponseDTO.CreatedBy = pubAdminUserInfo.ID;
            }
            else {
                businessTenantSignUpResponseDTO.CreatedBy = session.TenantUserId;
            }

            businessTenantSignUpResponseDTO.TenantId = newBusinessTenantId;
            businessTenantSignUpResponseDTO.CreatedOn = DateTime.UtcNow;
            businessTenantSignUpResponseDTO.PublisherTenantId = publisherTenantId;

            #region Adding Tenant/Business/Business Subscription Data

            // Adding tenant and its linked  entity.
            Business business = await AddBusinessAndAddress(businessSignUpDTO, businessTenantSignUpResponseDTO, token);

            ResponseModelDTO response = new ResponseModelDTO();

            #endregion Adding Tenant/Business/Business Subscription Data

            #region Tenant Subscription/related Data

            Guid busAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            AppDetailDQ busAppDtlDTO = await _qPublisherDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantIDAsync(busAppId, publisherTenantId, token);
            // TODO: Amit.
            Guid busThemeId = busAppDtlDTO != null ? busAppDtlDTO.ThemeId : new Guid("46E3CF6C-6CD6-7C12-ED0E-1E906DFE5560");
            // Add Tenant app linking.
            await AddTenantAppLinkingAsync(businessTenantSignUpResponseDTO, busThemeId, busAppId, token);

            List<BusinessAppSubscriptionDTO> listSubscription = new List<BusinessAppSubscriptionDTO>();
            if(businessSignUpDTO.Subscriptions != null) {
                // Adding subscribe application and its user.
                //TenantSubscription[] subscription = BusinessAppSubscriptionDTO.MapModelArrayToEntityArray(businessRegistrtionDTO.Subscriptions);// _mapper.Map<TenantSubscription[]>(businessRegistrtionDTO.Subscriptions);
                List<BusinessAppSubscriptionDTO> subscription = businessSignUpDTO.Subscriptions;
                for(int i = 0; i < subscription.Count; i++) {
                    BusinessAppSubscriptionDTO businessDTO = businessSignUpDTO.Subscriptions[i];
                    AppDetailDQ appDtlDTO = await _qPublisherDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantIDAsync(businessDTO.AppId, publisherTenantId, token);
                    if(appDtlDTO != null) {
                        subscription[i].ThemeId = appDtlDTO.ThemeId;
                    }
                    else if(busAppDtlDTO != null) {
                        subscription[i].ThemeId = busAppDtlDTO.ThemeId;
                    }
                    // Add tenant app linking.
                    await AddTenantAppLinkingAsync(businessTenantSignUpResponseDTO, subscription[i].ThemeId, businessDTO.AppId, token);
                    /* AppDetailDQ appDtlDTO = await _pubAppSettingDS.GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(businessDTO.AppId, publisherTenantId, token);

                    if(appDtlDTO != null) {
                        subscription[i].ThemeId = appDtlDTO.ThemeId;
                    }
                    else if(pubAppSetting != null) {
                        subscription[i].ThemeId = pubAppSetting.ThemeId;
                    } */
                }
                //listSubscription.Add(tuple.Item2);
                listSubscription.AddRange(subscription);
            }
            else {
                //listSubscription.Add(tuple.Item2);
            }


            #endregion Tenant Subscription/related data



            //await this.AddBusinessConnectorConfigsAsync(businessSignUpDTO.ConnectorConfigList, businessTenantSignUpResponseDTO, token);

            BusinessTenantSignUpDTO addTenantDTO = businessSignUpDTO.MapBusinessModelToAddTenantModel();
            // Its userid generated for business email.
            addTenantDTO.GeneratedUserId = pubPrimaryUserDTO.UserId;
            addTenantDTO.GeneratedTenantId = businessTenantSignUpResponseDTO.TenantId;
            addTenantDTO.EntityType = (int)TenantType.Buisness;
            // Call API to add tenant and related data.
            businessTenantSignUpResponseDTO = await AddTenantAndAppSubscriptionAsync(addTenantDTO, token);

            #region Assign Role/UserAppPreferences and other dependency

            await AddRoleLinkingAndAppPrefrencesForUser(businessTenantSignUpResponseDTO.UserAppRelationDTOList, addTenantDTO.GeneratedTenantId, addTenantDTO.GeneratedUserId, businessTenantSignUpResponseDTO.CreatedBy, token);

            #endregion Assign Role/UserAppPreferences and other dependency

            _unitOfWork.Save();

            #region Notification

            try {

                Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == publisherTenantId);

                PublisherNotificationDTO publisherNotificationDTO = new PublisherNotificationDTO();
                publisherNotificationDTO.PasswordCode = businessTenantSignUpResponseDTO.TenantUserInfo.Code;
                publisherNotificationDTO.InvitedUserEmail = businessTenantSignUpResponseDTO.TenantUserInfo.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = businessTenantSignUpResponseDTO.TenantUserInfo.IdentityUserId;
                publisherNotificationDTO.InvitedUserId = businessTenantSignUpResponseDTO.TenantUserInfo.TenantUserId;
                publisherNotificationDTO.InvitedUserTenantId = businessTenantSignUpResponseDTO.TenantId;
                publisherNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                publisherNotificationDTO.InvitedUserAppId = businessTenantSignUpResponseDTO.UserAppRelationDTOList.Find(ur => ur.AppKey.ToLower() == AppKeyEnum.biz.ToString().ToLower()).AppId;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.BusinessCompanyName = businessSignUpDTO.Name;
                publisherNotificationDTO.InvitedUserFullName = businessTenantSignUpResponseDTO.TenantUserInfo.FullName;
                publisherNotificationDTO.SubDomain = businessSignUpDTO.SubDomainName;
                publisherNotificationDTO.BusinessPortalURL = string.Format(_appSetting.BusinessPortalClientURL, businessSignUpDTO.SubDomainName);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.UserSession = new UserSession {
                    TenantUserId = businessTenantSignUpResponseDTO.CreatedBy
                };
                await _publisherNotificationHandler.SendBusinessPrimaryUserWithNewEmailInvite(publisherNotificationDTO);
            }
            catch(Exception ex) {
                // throw;
            }

            #endregion Notification

            response.Id = businessTenantSignUpResponseDTO.TenantId;
            return response;
        }

        private async Task AddTenantAppLinkingAsync(BusinessTenantSignUpResponseDTO businessTenantSignUpResponseDTO, Guid themeId, Guid appId, CancellationToken token = default(CancellationToken)) {
            TenantAppLinking taLinking = new TenantAppLinking();
            taLinking.ThemeId = themeId;
            taLinking.TenantId = businessTenantSignUpResponseDTO.TenantId;
            taLinking.AppId = appId;
            taLinking.CreatedBy = businessTenantSignUpResponseDTO.CreatedBy;
            taLinking.UpdatedBy = businessTenantSignUpResponseDTO.CreatedBy;
            taLinking.CreatedOn = businessTenantSignUpResponseDTO.CreatedOn;
            taLinking.UpdatedOn = businessTenantSignUpResponseDTO.CreatedOn;
            await _tenantAppLinkingDS.AddAsync(taLinking, token);
        }

        private async Task<BusinessTenantSignUpResponseDTO> AddTenantAndAppSubscriptionAsync(BusinessTenantSignUpDTO dto, CancellationToken token = default(CancellationToken)) {
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "tenant/businesssignup";
            requestOptions.MethodData = dto;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
            return await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
        }

        /// <summary>
        /// Adding connector configuration for tenant application.
        /// </summary>
        /// <param name="connectorConfigDTO">Client connector configuration setting for a tenant.</param>
        /// <param name="tenant">Business tenant</param>
        private async Task AddBusinessConnectorConfigsAsync(List<ConnectorConfigDTO> connectorConfigDTO, BusinessTenantSignUpResponseDTO tenant, CancellationToken token = default(CancellationToken)) {
            if(connectorConfigDTO == null) {
                return;
            }
            /// set tenant properties.
            for(int i = 0; i < connectorConfigDTO.Count; i++) {
                connectorConfigDTO[i].CreatedBy = tenant.CreatedBy;
                connectorConfigDTO[i].UpdatedBy = tenant.CreatedBy;
                connectorConfigDTO[i].TenantId = tenant.TenantId;
            }
            // Preparing api calling process model.
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "ERPConnectorConfig/addconnectorconfig";
            requestOptions.MethodData = connectorConfigDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);

            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        #endregion Add/Update/Delete        

        #region Add Tenant/TenantLinking/Business

        /// <summary>
        /// Add tenant/tenantlinking/business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="tenant"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<Business> AddBusinessAndAddress(BusinessSignUpRequestDTO businessRegistrtionDTO, BusinessTenantSignUpResponseDTO tenant, CancellationToken token = default(CancellationToken)) {
            // Adding business.
            Business business = await AddBusiness(businessRegistrtionDTO, tenant, token);

            // Adding address.
            await AddBusinessAddressListAsync(businessRegistrtionDTO.AddressList, business, tenant, token);

            return business;
        }

        // Add business address.
        private async Task AddBusinessAddressListAsync(List<BusinessAddressModelDTO> addressDTOList, Business business, BusinessTenantSignUpResponseDTO tenant, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < addressDTOList.Count; i++) {
                //await AddBusinessAddressAsync(addressDTOList[i], business.ID, tenant, token);
                BusinessAddress address = BusinessAddressModelDTO.MapToEntity(addressDTOList[i]);
                address.ID = Guid.NewGuid();
                address.Label = addressDTOList[i].Label;
                address.BusinessId = business.ID;
                address.AddressType = (int)BusinessAddressTypeEnum.DefaultBusinessAddress;

                address.CreatedOn = tenant.CreatedOn;
                address.UpdatedOn = tenant.CreatedOn;
                address.CreatedBy = tenant.CreatedBy;
                address.UpdatedBy = tenant.CreatedBy;
                address.TenantId = tenant.TenantId;

                await _addressDS.AddAsync(address, token);
            }
        }

        //// Add business address.
        //private async Task AddBusinessAddressAsync(BusinessAddressModelDTO addressDTO, Guid businessId, BusinessTenantSignUpResponseDTO tenant, CancellationToken token = default(CancellationToken)) {
        //    BusinessAddress address = BusinessAddressModelDTO.MapToEntity(addressDTO);
        //    address.ID = Guid.NewGuid();
        //    address.Label = addressDTO.Label;
        //    address.BusinessId = businessId;
        //    address.CreatedOn = tenant.CreatedOn;
        //    address.UpdatedOn = tenant.CreatedOn;
        //    address.CreatedBy = tenant.CreatedBy;
        //    address.UpdatedBy = tenant.CreatedBy;
        //    address.TenantId = tenant.TenantId;
        //    address.AddressType = (int)BusinessAddressTypeEnum.DefaultBusinessAddress;

        //    await _addressDS.AddAsync(address, token);
        //}

        /// <summary>
        /// Generate business entity from business model.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="tenant"></param>
        /// <param name="token"></param>
        private async Task<Business> AddBusiness(BusinessSignUpRequestDTO businessRegistrtionDTO, BusinessTenantSignUpResponseDTO tenant, CancellationToken token = default(CancellationToken)) {
            Business business = new Business();
            business.Name = businessRegistrtionDTO.Name;
            business.ContactPersonDesignation = businessRegistrtionDTO.ContactPersonDesignation;
            business.ContactPersonEmail = businessRegistrtionDTO.ContactPersonEmail;
            business.ContactPersonName = businessRegistrtionDTO.ContactPersonName;
            business.ContactPersonPhone = businessRegistrtionDTO.ContactPersonPhone;
            // setting currency
            business.CanUpdateCurrency = true;
            business.CurrencyCode = businessRegistrtionDTO.CurrencyCode;
            business.GroupValue = businessRegistrtionDTO.GroupValue;
            business.GroupSeperator = businessRegistrtionDTO.GroupSeperator;
            business.DecimalSeperator = businessRegistrtionDTO.DecimalSeperator;
            business.DecimalPrecision = businessRegistrtionDTO.DecimalPrecision;
            business.Website = businessRegistrtionDTO.Website;
            business.Language = businessRegistrtionDTO.Language;
            business.TimeZone = businessRegistrtionDTO.TimeZone;
            business.DateTimeFormat = businessRegistrtionDTO.DateTimeFormat;
            // TODO:
            business.IntegratedMode = true;

            int identityNumber = _identityDataService.GetIdentityNo(Guid.Empty, (int)AppPortalEntityTypeEnum.Business, AppPortal.Common.AppPortalConstants.BusinessIdPrefix, 100001);
            business.IdentityNumber = AppPortal.Common.AppPortalConstants.BusinessIdPrefix + identityNumber;

            UserSession session = _userSessionManager.GetSession();

            if(session == null) {
                business.CreatedBy = tenant.CreatedBy;
                business.UpdatedBy = tenant.CreatedBy;
                business.CreatedOn = DateTime.UtcNow;
                business.UpdatedOn = business.CreatedOn;
            }
            else {
                _businessDS.UpdateSystemFieldsByOpType(business, OperationType.Add);
                //business.TenantId = tenant.TenantId;
            }

            business.TenantId = tenant.TenantId;

            await _businessDS.AddAsync(business, token);
            return business;
        }

        #endregion Add Tenant/TenantLinking/Business        

        #region Support

        /// <summary>
        /// Get tenant primary user detail.
        /// </summary>
        /// <param name="busRegDto"></param>
        /// <param name="pubHomeAppId"></param>
        /// <param name="uType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<PublisherPrimaryUserAndUserDTO> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(BusinessSignUpRequestDTO busRegDto, Guid pubHomeAppId, int uType, CancellationToken token = default(CancellationToken)) {
            // Initilize request object.
            PublisherRequestDTO puReqDTO = new PublisherRequestDTO();
            puReqDTO.PubHomeAppId = pubHomeAppId;
            puReqDTO.PublisherTenantId = busRegDto.PublisherTenantId;
            puReqDTO.UserType = uType;
            puReqDTO.UserEmail = busRegDto.AdminEmail;

            // Preparing api calling process model.
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "tenantuser/getpublisherprimaryuser";
            requestOptions.MethodData = puReqDTO;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Put;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.AppMgmtApiUrl);
            return await httpRequestProcessor.ExecuteAsync<PublisherPrimaryUserAndUserDTO>(requestOptions, false);
        }

        #endregion Support

        #region Private Methods

        private async Task AddRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            foreach (UserAppRelationDTO item in userAppRelationDTOs) {
                // Business Portal - Business Setup
                if(item.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddPrimaryUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserBusinessSetupAppPermissionEnum.All, cancellationToken);
                    // ToDo: Review code to create tenant user preference entry.
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserEmailPrefrenceEnum.All, (long)BusinessUserSMSPrefrenceEnum.All, cancellationToken);

                    //--old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, cancellationToken);

                    ////--new code
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId,  createdBy, item.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, cancellationToken);
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
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId,createdBy, item.AppId,(long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
                }
                // Business Portal - Ship App
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

                    //--old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserCustomerAppPreferenceEnum.All, (long)BusinessUserCustomerAppPreferenceEnum.All, (long)BusinessUserCustomerAppPreferenceEnum.All, cancellationToken);

                    ////--new code
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy,item.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddPaymentPrefValue(tenantId, tenantUserId, createdBy,item.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);

                    await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllEmail, (long)BusinessUserCustomerAppPreferenceEnum.AllSMS, (long)BusinessUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
                }
        // Business Portal - Vendor App
        else if(item.AppKey.Equals(AppKeyEnum.vend.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddPrimaryUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserVendorAppPermissionEnum.All, cancellationToken);
                    await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserVendorAppPreferenceEnum.All, (long)BusinessUserVendorAppPreferenceEnum.All, (long)BusinessUserVendorAppPreferenceEnum.All, cancellationToken);
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
            tenantUserAppPreference.TenantId = tenantId;
            tenantUserAppPreference.TenantUserId = tenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
        }

        #endregion Private Methods

        public async Task ReInvitePrimaryBusinessUserAsync(Guid tenantUserId, Guid bizTenantId, string subDomain, CancellationToken token = default(CancellationToken)) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();
            string baseuri = _appSetting.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            List<string> appKeyList = new List<string>();
            appKeyList.Add(AppKeyEnum.biz.ToString().ToLower());

            // Get App ID by calling th API.
            string requesturlappkey = "App/getappinfobykey";

            RequestOptions appkeyrequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturlappkey, appKeyList, _appSetting.AppName, _appSetting.IdentityServerUrl, headerParams);
            ServiceExecutor appkeyhttpRequestProcessor = new ServiceExecutor(baseuri);
            List<AppInfoDTO> appInfoDTO = await appkeyhttpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(appkeyrequestOptions, false);


            try {
                Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
                Business business = await _businessDS.FindAsync(b => b.TenantId == bizTenantId);


                PublisherNotificationDTO publisherNotificationDTO = new PublisherNotificationDTO();
                publisherNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                publisherNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                publisherNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                publisherNotificationDTO.InvitedUserTenantId = bizTenantId;
                publisherNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                publisherNotificationDTO.InvitedUserAppId = appInfoDTO[0].Id;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.BusinessCompanyName = business.Name;
                publisherNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                publisherNotificationDTO.SubDomain = subDomain;
                publisherNotificationDTO.BusinessPortalURL = string.Format(_appSetting.BusinessPortalClientURL, subDomain);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.UserSession = new UserSession {
                    TenantUserId = tenantUserId
                };
                await _publisherNotificationHandler.SendBusinessPrimaryUserWithNewEmailInvite(publisherNotificationDTO);
            }
            catch(Exception) {
                throw;
            }

        }

    }
}
