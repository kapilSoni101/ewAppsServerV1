/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
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
using ewApps.Core.ExceptionService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class CustTenantUserUpdateDS:ICustTenantUserUpdateDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IQPlatformAndUserDS _qPlatformAndUserDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        IUnitOfWork _unitOfWork;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IBusinessNotificationHandler _businessNotificationHandler;
        ITokenInfoDS _tokenInfoDS;
        IBizNotificationHandler _bizNotificationHandler;
        ICustNotificationHandler _custNotificationHandler;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IQCustomerAndUserDS _qCustomerAndUserDS;
        ICustUserPreferenceDS _custUserPreferenceDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public CustTenantUserUpdateDS(IQBusinessAndUserDS qBusinessAndUserDS, IBizNotificationHandler bizNotificationHandler, IUserSessionManager userSessionManager, IBusinessNotificationHandler businessNotificationHandler,
            IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS,
            IQPlatformAndUserDS qPlatformAndUserDS, IRoleDS roleDS, ITokenInfoDS tokenInfoDS, ICustNotificationHandler custNotificationHandler, IQCustomerAndUserDS qCustomerAndUserDS,
            IOptions<AppPortalAppSettings> appSettings, IUnitOfWork unitOfWork, ICustUserPreferenceDS custUserPreferenceDS) {
            _userSessionManager = userSessionManager;
            _appSettings = appSettings.Value;
            _qPlatformAndUserDS = qPlatformAndUserDS;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _unitOfWork = unitOfWork;
            _businessNotificationHandler = businessNotificationHandler;
            _tokenInfoDS = tokenInfoDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _bizNotificationHandler = bizNotificationHandler;
            _custNotificationHandler = custNotificationHandler;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _qCustomerAndUserDS = qCustomerAndUserDS;
            _custUserPreferenceDS = custUserPreferenceDS;
        }

        #endregion Constructor

        #region Public Method

        ///<inheritdoc/>
        public async Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync(TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updatecustomersetupuser";

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserUpdateRequestDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);

            //API calling.
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await httpRequestProcessor.ExecuteAsync<UpdateTenantUserResponseDTO>(requestOptions, false);

            // Get the list of application to be added update and delete.
            List<UserAppRelationDTO> addAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Add).ToList();
            List<UserAppRelationDTO> deleteAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Delete).ToList();
            List<UserAppRelationDTO> updateAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Update).ToList();

            #region Add

            foreach(UserAppRelationDTO addApp in addAppList) {
                await AddRoleLinkingAndAppPrefrencesForUserAsync(addApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);
            }

            #endregion Add

            #region Update        


      bool permissionChange = false;
      foreach(UserAppRelationDTO updateApp in updateAppList) {
        if(!permissionChange) {
          permissionChange = await UpdateRoleForTenantUser(updateApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.BusinessPartnerTenantId, userSession.TenantUserId, cancellationToken);
          await GenerateAppPermissionChangeNotification(userSession.TenantUserId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, updateApp.AppKey);
        }
        else {
          bool permissioNotChange = await UpdateRoleForTenantUser(updateApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.BusinessPartnerTenantId, userSession.TenantUserId, cancellationToken);
          await GenerateAppPermissionChangeNotification(userSession.TenantUserId, tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId, updateApp.AppKey);
        }

      }
      updateTenantUserResponseDTO.PermissionsChanged = permissionChange;

      #endregion Update

      #region Delete

      foreach(UserAppRelationDTO deleteApp in deleteAppList) {
                await GenerateAppReomoveNotification(tenantUserUpdateRequestDTO.TenantUserId, (tenantUserUpdateRequestDTO.BusinessPartnerTenantId).Value, tenantUserUpdateRequestDTO.TenantId, deleteApp.AppKey);
                await DeleteRoleLinkingAndAppPrefrencesForUserAsync(deleteApp, tenantUserUpdateRequestDTO.TenantId, tenantUserUpdateRequestDTO.TenantUserId, userSession.TenantUserId, cancellationToken);

            }

            #endregion Delete

            _unitOfWork.SaveAll();

      // new changes for delete session if permision change
      if(permissionChange)
        await _userSessionManager.DeletedByUserIdAndTenantId(tenantUserUpdateRequestDTO.TenantUserId, tenantUserUpdateRequestDTO.TenantId);

      return updateTenantUserResponseDTO;
        }

        public async Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();
            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            try {
                BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
                // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
                //Guid appId = tenantUserSignUpDTO.UserAppRelationDTOList.Where(u => u.AppKey == AppKeyEnum.biz.ToString()).FirstOrDefault().AppId;
                BusinessUserNotificationGeneralDTO businessUserNotificationGeneralDTO = await _qBusinessAndUserDS.GetBusinessUserNotificationGeneralDataAsync(userSession.TenantUserId, userSession.TenantId, string.Empty);
                if(tenantUserInfoDTO.NewUser != null && tenantUserInfoDTO.NewUser.Item2) {
                    businessAccountNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = businessUserNotificationGeneralDTO.PublisherName;
                    businessAccountNotificationDTO.BusinessCompanyName = businessUserNotificationGeneralDTO.BusinessCompanyName;
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appSettings.BusinessPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = businessUserNotificationGeneralDTO.CopyRightText;
                    businessAccountNotificationDTO.UserSession = userSession;
                    businessAccountNotificationDTO.PortalName = tenantUserInfoDTO.NewUser.Item1;
                    await _businessNotificationHandler.GenerateBusinessUserWithExistingEmailIdInviteNotification(businessAccountNotificationDTO);
                }
                else {
                    businessAccountNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = businessUserNotificationGeneralDTO.PublisherName;
                    businessAccountNotificationDTO.BusinessCompanyName = businessUserNotificationGeneralDTO.BusinessCompanyName;
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appSettings.BusinessPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = businessUserNotificationGeneralDTO.CopyRightText;
                    ;
                    businessAccountNotificationDTO.UserSession = userSession;
                    // await _businessNotificationHandler.GenerateBusinessNewUserInviteNotification(businessAccountNotificationDTO);
                    await _bizNotificationHandler.GenerateBusinessNewUserInviteNotification(businessAccountNotificationDTO);

                }
            }
            catch(Exception) {
                throw;
            }

            // APi calling to cahnge the status again
            TenantUserIdentificationDTO tenantUserIdentificationDTO = new TenantUserIdentificationDTO();
            tenantUserIdentificationDTO.AppId = appId;
            tenantUserIdentificationDTO.TenantId = userSession.TenantId;
            tenantUserIdentificationDTO.TenantUserId = tenantUserId;

            requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.Invited;
            requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        public async Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            // Deleted tokens
            await _tokenInfoDS.DeleteTokenByTenantUserIdAndTokenType(tenantUserIdentificationDTO.TenantUserId, userSession.TenantId, (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite);

            // APi calling
            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.InvitationCanceled;

            // Change user status in .
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        #endregion Public Method

        #region Private Method

        private async Task AddRoleLinkingAndAppPrefrencesForUserAsync(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            // Customer Portal - Setup App
            if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.custsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, userAppRelationDTO.PermissionBitMask, cancellationToken);
                // ToDo: Review code to create tenant user preference entry.
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)BusinessUserEmailPrefrenceEnum.All, (long)BusinessUserSMSPrefrenceEnum.All, cancellationToken);
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)CustomerUserCustomerSetupPreferenceEnum.None, (long)CustomerUserCustomerSetupPreferenceEnum.None, (long)CustomerUserCustomerSetupPreferenceEnum.None, cancellationToken);

            }
            // Customer Portal - Payment App
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                tenantUserAppManagmentDTO.AppId = userAppRelationDTO.AppId;
                tenantUserAppManagmentDTO.CreatedBy = createdBy;
                tenantUserAppManagmentDTO.TenantId = tenantId;
                tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Customer;
                tenantUserAppManagmentDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                ////New code
                //tenantUserAppManagmentDTO.EmailPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;
                //tenantUserAppManagmentDTO.SMSPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;
                //tenantUserAppManagmentDTO.ASPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllPayPreference;

                tenantUserAppManagmentDTO.EmailPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllEmail;
                tenantUserAppManagmentDTO.SMSPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllSMS;
                tenantUserAppManagmentDTO.ASPreference = (long)CustomerUserPaymentAppPreferenceEnum.AllAS;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "customeruser/appassign";
                requestOptions.MethodData = tenantUserAppManagmentDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);

                ////New code
                //await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllAPPreference, cancellationToken);
                //await _custUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, (long)CustomerUserPaymentAppPreferenceEnum.AllBEPreference, cancellationToken);
            }
            // Customer Portal - Customer App
            else if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, userAppRelationDTO.PermissionBitMask, cancellationToken);
                //old code
                //await AddTenantUserAppPreferncesAsync(tenantId, tenantUserId, userAppRelationDTO.AppId, createdBy, (long)CustomerUserCustomerAppPreferenceEnum.All, (long)CustomerUserCustomerAppPreferenceEnum.All, (long)CustomerUserCustomerAppPreferenceEnum.All, cancellationToken);

                ////New code
                //await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllAPPreference, cancellationToken);
                //await _custUserPreferenceDS.AddBEPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllBEPreference, cancellationToken);
                //await _custUserPreferenceDS.AddPaymentPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, userSession.ID.ToString(), (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, (long)CustomerUserCustomerAppPreferenceEnum.AllPayPreference, cancellationToken);

                await _custUserPreferenceDS.AddAPPrefValue(tenantId, tenantUserId, createdBy, userAppRelationDTO.AppId, (long)CustomerUserCustomerAppPreferenceEnum.AllEmail, (long)CustomerUserCustomerAppPreferenceEnum.AllSMS, (long)CustomerUserCustomerAppPreferenceEnum.AllAS, cancellationToken);

            }
        }

        private async Task AddUserRoleAndRoleLinkingAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long permissionbitMask, CancellationToken cancellationToken) {
            // Get role/add role based on input permission mask bit.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(permissionbitMask, appId, (int)UserTypeEnum.Customer, createdBy, cancellationToken);

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

        private async Task AddTenantUserAppPreferncesAsync(Guid tenantId, Guid tenantUserId, Guid appId, Guid createdBy, long emailPrefrence, long smsPreference, long asPreference, CancellationToken cancellationToken) {
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

        private async Task<bool> UpdateRoleForTenantUser(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid? businessPartnerTenantId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();
      bool permissionUpdate = false;
      if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Customer;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "customeruser/updaterole";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                RoleUpdateResponseDTO roleUpdateResponseDTO = await httpRequestProcessor.ExecuteAsync<RoleUpdateResponseDTO>(requestOptions, false);
        if(roleUpdateResponseDTO != null) {
          permissionUpdate = roleUpdateResponseDTO.RoleUpdated;
        }
      }
            else {
                // Get role/add role and add a entry in rolelinking table  // Add/Update  rolelinking for the user.
                Guid roleId = await _roleDS.GetOrCreateRoleAsync(userAppRelationDTO.PermissionBitMask, userAppRelationDTO.AppId, (int)UserTypeEnum.Customer, tenantUserId);

                RoleLinking roleLinking = await _roleLinkingDS.FindAsync(a => a.TenantUserId == tenantUserId && a.AppId == userAppRelationDTO.AppId && a.TenantId == tenantId && a.Deleted == false);
                if(roleLinking != null && (roleLinking.RoleId != roleId)) {

                    // get role
                    Role role = await _roleDS.GetAsync(roleId);

                    // Delete user session if permissions are changed
                    await ValidationOnUpdateUser(tenantUserId, tenantId, (int)UserTypeEnum.Customer, userAppRelationDTO.AppId, businessPartnerTenantId, role.RoleKey);
                    await _userSessionManager.DeletedByAppUserAndAppId(tenantUserId, userAppRelationDTO.AppId);
          permissionUpdate = true;
          roleLinking.RoleId = roleId;
                    _roleLinkingDS.UpdateSystemFieldsByOpType(roleLinking, OperationType.Update);
                    await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                    _unitOfWork.SaveAll();
                }
            }
      return permissionUpdate;
    }

        private async Task DeleteRoleLinkingAndAppPrefrencesForUserAsync(UserAppRelationDTO userAppRelationDTO, Guid tenantId, Guid tenantUserId, Guid createdBy, CancellationToken cancellationToken = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();

            if(userAppRelationDTO.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                // Call payment api to deassign 
                TenantUserAppManagmentDTO roleLinkingAndPreferneceDTO = new TenantUserAppManagmentDTO();
                roleLinkingAndPreferneceDTO.AppId = userAppRelationDTO.AppId;
                roleLinkingAndPreferneceDTO.CreatedBy = createdBy;
                roleLinkingAndPreferneceDTO.TenantId = tenantId;
                roleLinkingAndPreferneceDTO.TenantUserId = tenantUserId;
                roleLinkingAndPreferneceDTO.UserType = (int)UserTypeEnum.Customer;
                roleLinkingAndPreferneceDTO.PermissionBitMask = userAppRelationDTO.PermissionBitMask;

                RequestOptions requestOptions = new RequestOptions();
                requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                requestOptions.Method = "customeruser/appdeassign";
                requestOptions.MethodData = roleLinkingAndPreferneceDTO;
                requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                requestOptions.ServiceRequestType = RequestTypeEnum.Put;
                requestOptions.BearerTokenInfo = new BearerTokenOption();
                requestOptions.BearerTokenInfo.AppClientName = _appSettings.AppName;
                requestOptions.BearerTokenInfo.AuthServiceUrl = _appSettings.IdentityServerUrl;
                List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                requestOptions.HeaderParameters = headerParams;
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSettings.PaymentApiUrl);
                await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
            }
            else {
                RoleLinking roleLinking = await _roleLinkingDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(roleLinking != null) {
                    roleLinking.Deleted = true;
                    await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                }
                TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(RL => RL.TenantUserId == tenantUserId && RL.TenantId == tenantId && RL.AppId == userAppRelationDTO.AppId && RL.Deleted == false);
                if(tenantUserAppPreference != null) {
                    tenantUserAppPreference.Deleted = true;
                    await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
                }
            }
        }

        #region Validation 

        private async Task ValidationOnUpdateUser(Guid tenantUserID, Guid tenantId, int userType, Guid appId, Guid? businessPartnerTenantId, string roleKey) {

            Tuple<bool, Guid> lastAdmin = await _qPlatformAndUserDS.CheckUserIsLastAdminUserAsync(tenantId, userType, appId, businessPartnerTenantId);

            if(lastAdmin.Item1 && lastAdmin.Item2 == tenantUserID && (roleKey.ToLower() != ewApps.AppPortal.Common.AppPortalConstants.AdminRoleKey.ToLower())) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Security;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)SecurityErrorSubType.Update;
                errorData.Message = "Can not change the last users admin rights";
                error.EwpErrorDataList.Add(errorData);
                EwpDuplicateNameException exc = new EwpDuplicateNameException("User permission can not be changed", error.EwpErrorDataList);
                throw exc;
            }
        }

        #endregion Validation 

        #region Notification

        private async Task GenerateAppReomoveNotification(Guid tenantUserId, Guid businessPartnerTenantId, Guid tenantId, string appkey) {
            UserSession userSession = _userSessionManager.GetSession();
            CustomerUserNotificationGeneralDTO customerUserNotificationGeneralDTO = new CustomerUserNotificationGeneralDTO();
            customerUserNotificationGeneralDTO = await _qCustomerAndUserDS.GetCustomerUserNotificationGeneralDataAsync(tenantUserId, businessPartnerTenantId, tenantId, appkey);
            if(customerUserNotificationGeneralDTO != null) {
                customerUserNotificationGeneralDTO.ActionDate = DateTime.Now;
                customerUserNotificationGeneralDTO.userSession = userSession;
                await _custNotificationHandler.GenerateAppReomoveNotification(customerUserNotificationGeneralDTO);
            }
        }

        private async Task GenerateAppPermissionChangeNotification(Guid tenantUserId, Guid businessPartnerTenantId, Guid tenantId, string appkey) {
            UserSession userSession = _userSessionManager.GetSession();
            CustomerUserPermissionChangeNotificationGeneralDTO customerUserPermissionChangeNotificationGeneralDTO = new CustomerUserPermissionChangeNotificationGeneralDTO();
            customerUserPermissionChangeNotificationGeneralDTO = await _qCustomerAndUserDS.GetCustomerUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, businessPartnerTenantId, tenantId, appkey);
            if(customerUserPermissionChangeNotificationGeneralDTO != null) {
                //customerUserNotificationGeneralDTO.ActionDate = DateTime.Now;
                customerUserPermissionChangeNotificationGeneralDTO.userSession = userSession;
                await _custNotificationHandler.GenerateAppPermissionChangeNotification(customerUserPermissionChangeNotificationGeneralDTO);
            }
        }

        #endregion Notification

        #endregion Private Method
    }
}
