using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class BusTenantUserSignUpDS:IBusTenantUserSignUpDS {

        #region Local Variables

        IRoleDS _roleDS;
        IUserSessionManager _userSessionManager;
        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IBusinessNotificationHandler _businessNotificationHandler;
        IPublisherDS _publisherDS;
        IBizNotificationHandler _bizNotificationHandler;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IBusUserPreferenceDS _busUserPreferenceDS;

        #endregion Local Variables

        #region Constructor

        public BusTenantUserSignUpDS(IQBusinessAndUserDS qBusinessAndUserDS, IBizNotificationHandler bizNotificationHandler, IRoleDS roleDS, IPublisherDS publisherDS, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork, IBusinessNotificationHandler businessNotificationHandler,
            IRoleLinkingDS roleLinkingDS, IOptions<AppPortalAppSettings> appPortalAppSettings, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IBusUserPreferenceDS busUserPreferenceDS) {
            _roleDS = roleDS;
            _userSessionManager = userSessionManager;
            _roleLinkingDS = roleLinkingDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _businessNotificationHandler = businessNotificationHandler;
            _publisherDS = publisherDS;
            _bizNotificationHandler = bizNotificationHandler;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _busUserPreferenceDS = busUserPreferenceDS;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get user session
            UserSession userSession = _userSessionManager.GetSession();
            //Guid appId = Guid.Empty;

            #region Add User On appMGMT 

            tenantUserSignUpDTO.TenantId = userSession.TenantId;

            string methodUri = "tenantuser/addbusinessuser";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions signupTenantUserRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, methodUri, tenantUserSignUpDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await serviceExecutor.ExecuteAsync<TenantUserSignUpResponseDTO>(signupTenantUserRequestOptions, false);

            #endregion Add User On appMGMT 

            #region Assign Role/UserAppPreferences and other dependency

            await AddRoleLinkingAndAppPrefrencesForUser(tenantUserSignUpDTO.UserAppRelationDTOList, userSession.TenantId, tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId, userSession.TenantUserId, cancellationToken);

            #endregion Assign Role/UserAppPreferences and other dependency

            _unitOfWork.SaveAll();

            try {
                BusinessUserNotificationGeneralDTO businessUserNotificationGeneralDTO = await _qBusinessAndUserDS.GetBusinessUserNotificationGeneralDataAsync(userSession.TenantUserId, userSession.TenantId, string.Empty);

                BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
                // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
                Guid appId = tenantUserSignUpDTO.UserAppRelationDTOList.Where(u => u.AppKey == AppKeyEnum.biz.ToString()).FirstOrDefault().AppId;

                if(tenantUserSignUpResponseDTO.NewUser != null && tenantUserSignUpResponseDTO.NewUser.Item2) {
                    businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = businessUserNotificationGeneralDTO.PublisherName;
                    businessAccountNotificationDTO.BusinessCompanyName = businessUserNotificationGeneralDTO.BusinessCompanyName;
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.BusinessPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = businessUserNotificationGeneralDTO.CopyRightText;
                    businessAccountNotificationDTO.UserSession = userSession;
                    businessAccountNotificationDTO.PortalName = tenantUserSignUpResponseDTO.NewUser.Item1;
                    await _businessNotificationHandler.GenerateBusinessUserWithExistingEmailIdInviteNotification(businessAccountNotificationDTO);
                }
                else {
                    businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = businessUserNotificationGeneralDTO.PublisherName;
                    businessAccountNotificationDTO.BusinessCompanyName = businessUserNotificationGeneralDTO.BusinessCompanyName;
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.BusinessPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = businessUserNotificationGeneralDTO.CopyRightText;
                    businessAccountNotificationDTO.UserSession = userSession;
                    //await _businessNotificationHandler.GenerateBusinessNewUserInviteNotification(businessAccountNotificationDTO);
                    await _bizNotificationHandler.GenerateBusinessNewUserInviteNotification(businessAccountNotificationDTO);

                }
            }
            catch(Exception) {
                throw;
            }
            return tenantUserSignUpResponseDTO;
        }

        #endregion Public Methods

        #region Private Methods

        private async Task AddRoleLinkingAndAppPrefrencesForUser(List<UserAppRelationDTO> userAppRelationDTOs, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            // User session.
            UserSession userSession = _userSessionManager.GetSession();

            foreach(UserAppRelationDTO item in userAppRelationDTOs) {
                // Business Portal - Business Setup App
                if(item.AppKey.Equals(AppKeyEnum.biz.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, item.PermissionBitMask, cancellationToken);
                    // ToDo: Review code to create tenant user preference entry.

                    //old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, (long)BusinessUserBusinessSetupAppPreferenceEnum.All, cancellationToken);

                    ////--new code
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, (long)BusinessUserBusinessSetupAppPreferenceEnum.AllAPPreference, cancellationToken);
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
                    tenantUserAppManagmentDTO.PermissionBitMask = item.PermissionBitMask;

                    //////New code
                    //tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
                    //tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;
                    //tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllPayPreference;

                    tenantUserAppManagmentDTO.EmailPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllEmail;
                    tenantUserAppManagmentDTO.SMSPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllSMS;
                    tenantUserAppManagmentDTO.ASPreference = (long)BusinessUserPaymentAppPreferenceEnum.AllAS;

                    // ToDo: Review code to create tenant user preference entry.
                    RequestOptions requestOptions = new RequestOptions();
                    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    requestOptions.Method = "businessuser/appassign";
                    requestOptions.MethodData = tenantUserAppManagmentDTO;
                    requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                    requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                    requestOptions.BearerTokenInfo = new BearerTokenOption();
                    requestOptions.BearerTokenInfo.AppClientName = _appPortalAppSettings.AppName;
                    requestOptions.BearerTokenInfo.AuthServiceUrl = _appPortalAppSettings.IdentityServerUrl;

                    requestOptions.HeaderParameters = new List<KeyValuePair<string, string>>();
                    requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);

                    await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);

                    ////New code
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, (long)BusinessUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
                }
                // Business Portal - Ship App
                else if(item.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                    roleLinkingAndPreferneceDTO.AppId = item.AppId;
                    roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                    roleLinkingAndPreferneceDTO.TenantId = tenantId;
                    roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                    roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Business;
                    roleLinkingAndPreferneceDTO.PermissionBitMask = item.PermissionBitMask;

                    RequestOptions requestOptions = new RequestOptions();
                    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    requestOptions.Method = "businessuser/appassign";
                    requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                    requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                    requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                    requestOptions.BearerTokenInfo = new BearerTokenOption();
                    requestOptions.BearerTokenInfo.AppClientName = _appPortalAppSettings.AppName;
                    requestOptions.BearerTokenInfo.AuthServiceUrl = _appPortalAppSettings.IdentityServerUrl;
                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.ShipmentApiUrl);
                    await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
                }
                // Business Portal - Customers App
                else if(item.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, item.PermissionBitMask, cancellationToken);
                    // ToDo: Review code to create tenant user preference entry.

                    //--old code
                    //await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, 0, 0, cancellationToken);

                    ////--new code
                    //await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllAPPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                    //await _busUserPreferenceDS.AddPaymentPrefValue(tenantId, tenantUserId, createdBy, item.AppId, userSession.ID.ToString(), (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, (long)BusinessUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);

                    await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserCustomerAppPreferenceEnum.AllEmail, (long)BusinessUserCustomerAppPreferenceEnum.AllSMS, (long)BusinessUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
                }
        // Business Portal - Vendor App
        else if(item.AppKey.Equals(AppKeyEnum.vend.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, item.PermissionBitMask, cancellationToken);
                   // await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, 0, 0, cancellationToken);
          await _busUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, item.AppId, (long)BusinessUserVendorAppPreferenceEnum.AllEmail, (long)BusinessUserVendorAppPreferenceEnum.AllSMS, (long)BusinessUserVendorAppPreferenceEnum.AllAS, cancellationToken);
        }
            }
        }

        private async Task AddUserRoleAndRoleLinkingAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long permissionbitMask, CancellationToken cancellationToken) {
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

        private async Task AddTenantUserAppPrefernces(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long emailPrefrence, long smsPreference, CancellationToken cancellationToken) {
            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.ID = Guid.NewGuid();
            tenantUserAppPreference.AppId = appId;
            tenantUserAppPreference.CreatedBy = createdBy;
            tenantUserAppPreference.CreatedOn = DateTime.UtcNow;
            tenantUserAppPreference.Deleted = false;
            tenantUserAppPreference.EmailPreference = emailPrefrence;
            tenantUserAppPreference.SMSPreference = smsPreference;
            tenantUserAppPreference.TenantId = tenantId;
            tenantUserAppPreference.TenantUserId = tenantUserId;
            tenantUserAppPreference.UpdatedBy = tenantUserAppPreference.CreatedBy;
            tenantUserAppPreference.UpdatedOn = tenantUserAppPreference.CreatedOn;
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);
        }

        #endregion Private Methods
    }
}
