/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 31 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
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
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
   public class PubTenantUserSignUpDS :IPubTenantUserSignUpDS {

        IRoleDS _roleDS;
        IUserSessionManager _userSessionManager;
        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IPublisherNotificationHandler _publisherNotificationHandler;
        IPublisherDS _publisherDS;

        public PubTenantUserSignUpDS(IRoleDS roleDS, IPublisherDS publisherDS, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork, IPublisherNotificationHandler publisherNotificationHandler,
            IRoleLinkingDS roleLinkingDS, IOptions<AppPortalAppSettings> appPortalAppSettings, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
            _roleDS = roleDS;
            _userSessionManager = userSessionManager;
            _roleLinkingDS = roleLinkingDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _publisherNotificationHandler = publisherNotificationHandler;
            _publisherDS = publisherDS;
        }

        public async Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get user session
            UserSession userSession = _userSessionManager.GetSession();
            Guid appId = Guid.Empty;

            #region Add User On appMGMT 

            tenantUserSignUpDTO.TenantId = userSession.TenantId;

            string methodUri = "tenantuser/addpublisheruser";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions signupTenantUserRequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, methodUri, tenantUserSignUpDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);

            // Execute api to generate tenant and user data.
            ServiceExecutor serviceExecutor = new ServiceExecutor(_appPortalAppSettings.AppMgmtApiUrl);
            TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = await serviceExecutor.ExecuteAsync<TenantUserSignUpResponseDTO>(signupTenantUserRequestOptions, false);
            appId = tenantUserSignUpResponseDTO.UserAppRelationDTOs[0].AppId;

            #endregion Add User On appMGMT 

            #region RoleLinking

            // Get role/add role based on input permission mask bit.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(tenantUserSignUpDTO.PermissionBitMask, appId, (int)UserTypeEnum.Publisher, userSession.TenantUserId, cancellationToken);

            // Add Role linking entry for primary user.
            RoleLinking roleLinking = new RoleLinking();
            roleLinking.RoleId = roleId;
            roleLinking.AppId = appId;
            roleLinking.TenantId = userSession.TenantId;
            roleLinking.TenantUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
            _roleLinkingDS.UpdateSystemFields(roleLinking, SystemFieldMask.Id | SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            await _roleLinkingDS.AddAsync(roleLinking, cancellationToken);

            #endregion RoleLinking

            #region Prefrence 

            // Add tenant user preference entry.
            TenantUserAppPreference tenantUserAppPreference = new TenantUserAppPreference();
            tenantUserAppPreference.EmailPreference = (long)PublisherEmailPreferenceEnum.All;
            tenantUserAppPreference.SMSPreference = (long)PublisherSMSPreferenceEnum.All;
            tenantUserAppPreference.AppId = appId;
            tenantUserAppPreference.TenantUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
            tenantUserAppPreference.TenantId = userSession.TenantId;
            _tenantUserAppPreferenceDS.UpdateSystemFields(tenantUserAppPreference, SystemFieldMask.Id | SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);

            #endregion Prefrence 

            _unitOfWork.SaveAll();

            PublisherNotificationDTO publisherNotificationDTO = new PublisherNotificationDTO();
            Publisher  publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);

            if(tenantUserSignUpResponseDTO.NewUser != null && tenantUserSignUpResponseDTO.NewUser.Item2) {
                publisherNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                publisherNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                publisherNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                publisherNotificationDTO.InvitedUserAppKey = tenantUserSignUpResponseDTO.UserAppRelationDTOs[0].AppKey;
                publisherNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                publisherNotificationDTO.InvitedUserAppId = appId;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                publisherNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, userSession.Subdomain);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.SubDomain = userSession.Subdomain;
                publisherNotificationDTO.UserSession = userSession;
                publisherNotificationDTO.PortalName = tenantUserSignUpResponseDTO.NewUser.Item1;
                await _publisherNotificationHandler.SendPublisherUserExistingEmailInvite(publisherNotificationDTO);
            }
            else {
                publisherNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                publisherNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                publisherNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                publisherNotificationDTO.InvitedUserAppKey = tenantUserSignUpResponseDTO.UserAppRelationDTOs[0].AppKey;
                publisherNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                publisherNotificationDTO.InvitedUserAppId = appId;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                publisherNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, userSession.Subdomain);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.SubDomain = userSession.Subdomain;
                publisherNotificationDTO.UserSession = userSession;
                publisherNotificationDTO.InvitorUserFullName = userSession.UserName;
                await _publisherNotificationHandler.SendPublisherUserNewEmailInvite(publisherNotificationDTO);
            }

            return tenantUserSignUpResponseDTO;

        }
    }
}
