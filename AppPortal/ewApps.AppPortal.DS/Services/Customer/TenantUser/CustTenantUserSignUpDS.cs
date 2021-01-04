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
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class CustTenantUserSignUpDS:ICustTenantUserSignUpDS {

        #region Local Variables

        IRoleDS _roleDS;
        IUserSessionManager _userSessionManager;
        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IBusinessNotificationHandler _businessNotificationHandler;
        IPublisherDS _publisherDS;

        #endregion Local Variables

        #region Constructor

        public CustTenantUserSignUpDS(IRoleDS roleDS, IPublisherDS publisherDS, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork, IBusinessNotificationHandler businessNotificationHandler,
            IRoleLinkingDS roleLinkingDS, IOptions<AppPortalAppSettings> appPortalAppSettings, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
            _roleDS = roleDS;
            _userSessionManager = userSessionManager;
            _roleLinkingDS = roleLinkingDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _businessNotificationHandler = businessNotificationHandler;
            _publisherDS = publisherDS;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get user session
            UserSession userSession = _userSessionManager.GetSession();

            #region Add User On appMGMT 

            tenantUserSignUpDTO.TenantId = userSession.TenantId;

            string methodUri = "tenantuser/custsetupaddcustomeruser";
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
                BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
                // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
                Guid appId = tenantUserSignUpDTO.UserAppRelationDTOList.Where(u => u.AppKey == AppKeyEnum.custsetup.ToString()).FirstOrDefault().AppId;

                if(tenantUserSignUpResponseDTO.NewUser != null && tenantUserSignUpResponseDTO.NewUser.Item2) {
                    businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.custsetup.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = " ";
                    businessAccountNotificationDTO.BusinessCompanyName = " ";
                    businessAccountNotificationDTO.PartnerType = "Customer";
                    businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                    businessAccountNotificationDTO.ApplicationName = "Payment";
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.CustomerPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = " ";
                    businessAccountNotificationDTO.UserSession = userSession;
                    await _businessNotificationHandler.GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(businessAccountNotificationDTO);
                }
                else {
                    businessAccountNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                    businessAccountNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                    businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                    businessAccountNotificationDTO.TenantId = userSession.TenantId;
                    businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.custsetup.ToString();
                    businessAccountNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                    businessAccountNotificationDTO.InvitedUserAppId = appId;
                    businessAccountNotificationDTO.PublisherName = " ";
                    businessAccountNotificationDTO.BusinessCompanyName = " ";
                    businessAccountNotificationDTO.PartnerType = "Customer";
                    businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
                    businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
                    businessAccountNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                    businessAccountNotificationDTO.ApplicationName = "Payment";
                    businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
                    businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.CustomerPortalClientURL, userSession.Subdomain);
                    businessAccountNotificationDTO.CopyRightText = " ";
                    businessAccountNotificationDTO.UserSession = userSession;
                    await _businessNotificationHandler.GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(businessAccountNotificationDTO);
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

                if(item.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    TenantUserAppManagmentDTO tenantUserAppManagmentDTO = new TenantUserAppManagmentDTO();
                    tenantUserAppManagmentDTO.AppId = item.AppId;
                    tenantUserAppManagmentDTO.CreatedBy = createdBy;
                    tenantUserAppManagmentDTO.TenantId = tenantId;
                    tenantUserAppManagmentDTO.TenantUserId = tenantUserId;
                    tenantUserAppManagmentDTO.UserType = (int)UserTypeEnum.Customer;
                    tenantUserAppManagmentDTO.PermissionBitMask = item.PermissionBitMask;
                    tenantUserAppManagmentDTO.EmailPreference = (int)CustomerUserPaymentAppPreferenceEnum.AllEmail;
                    tenantUserAppManagmentDTO.ASPreference = (int)CustomerUserPaymentAppPreferenceEnum.AllAS;
                    tenantUserAppManagmentDTO.SMSPreference = (int)CustomerUserPaymentAppPreferenceEnum.AllSMS;

                    RequestOptions requestOptions = new RequestOptions();
                    requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
                    requestOptions.Method = "customeruser/appassign";
                    requestOptions.MethodData = tenantUserAppManagmentDTO;
                    requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
                    requestOptions.ServiceRequestType = RequestTypeEnum.Post;
                    requestOptions.BearerTokenInfo = new BearerTokenOption();
                    requestOptions.BearerTokenInfo.AppClientName = _appPortalAppSettings.AppName;
                    requestOptions.BearerTokenInfo.AuthServiceUrl = _appPortalAppSettings.IdentityServerUrl;
                    List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
                    headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
                    requestOptions.HeaderParameters = headerParams;
                    ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.PaymentApiUrl);
                    await httpRequestProcessor.ExecuteAsync<BusinessTenantSignUpResponseDTO>(requestOptions, false);
                }
                else if(item.AppKey.Equals(AppKeyEnum.custsetup.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, item.PermissionBitMask, cancellationToken);
                    await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, 0, 0, 0, cancellationToken);
                }
                else if(item.AppKey.Equals(AppKeyEnum.cust.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    await AddUserRoleAndRoleLinkingAsync(tenantId, tenantUserId, item.AppId, createdBy, item.PermissionBitMask, cancellationToken);
                    await AddTenantUserAppPrefernces(tenantId, tenantUserId, item.AppId, createdBy, (long)CustomerUserCustomerAppPreferenceEnum.AllEmail, (long)CustomerUserCustomerAppPreferenceEnum.AllSMS, (long)CustomerUserCustomerAppPreferenceEnum.AllAS, cancellationToken);
                }
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

    }
}
