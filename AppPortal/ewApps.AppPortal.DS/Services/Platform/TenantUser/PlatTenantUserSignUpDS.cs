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
    public class PlatTenantUserSignUpDS :IPlatTenantUserSignUpDS {

        IRoleDS _roleDS;
        IUserSessionManager _userSessionManager;
        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        AppPortalAppSettings _appPortalAppSettings;
        IUnitOfWork _unitOfWork;
        IPlatformNotificationHandler _platformNotificationHandler;
        IPlatformDS _platformDS;

        public PlatTenantUserSignUpDS(IRoleDS roleDS, IPlatformDS platformDS, IUserSessionManager userSessionManager, IUnitOfWork unitOfWork, IPlatformNotificationHandler platformNotificationHandler,
            IRoleLinkingDS roleLinkingDS,IOptions< AppPortalAppSettings> appPortalAppSettings , ITenantUserAppPreferenceDS tenantUserAppPreferenceDS) {
            _roleDS = roleDS;
            _userSessionManager = userSessionManager;
            _roleLinkingDS = roleLinkingDS;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _unitOfWork = unitOfWork;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _platformNotificationHandler = platformNotificationHandler;
            _platformDS = platformDS;
        }

        public async Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get user session
            UserSession userSession = _userSessionManager.GetSession();
            Guid appId = Guid.Empty;

            #region Add User On appMGMT 

            tenantUserSignUpDTO.TenantId = userSession.TenantId;

            string methodUri = "tenantuser/addplatformuser";
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
            Guid roleId = await _roleDS.GetOrCreateRoleAsync((long)tenantUserSignUpDTO.PermissionBitMask, appId, (int)UserTypeEnum.Platform, userSession.TenantUserId, cancellationToken);

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
            tenantUserAppPreference.EmailPreference = (long)PlatformEmailPreferenceEnum.All;
            tenantUserAppPreference.SMSPreference = (long)PlatformSMSPreferenceEnum.All;
            tenantUserAppPreference.AppId = appId;
            tenantUserAppPreference.TenantUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
            tenantUserAppPreference.TenantId = userSession.TenantId;
            _tenantUserAppPreferenceDS.UpdateSystemFields(tenantUserAppPreference, SystemFieldMask.Id | SystemFieldMask.CreatedBy | SystemFieldMask.CreatedOn | SystemFieldMask.UpdatedBy | SystemFieldMask.UpdatedOn);
            await _tenantUserAppPreferenceDS.AddAsync(tenantUserAppPreference, cancellationToken);

            #endregion Prefrence 

            _unitOfWork.SaveAll();

            PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
            Platform platform = await _platformDS.FindAsync(p => p.TenantId == userSession.TenantId);

            if(tenantUserSignUpResponseDTO.NewUser != null && tenantUserSignUpResponseDTO.NewUser.Item2) {
                platformNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                platformNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                platformNotificationDTO.InvitedUserAppKey = tenantUserSignUpResponseDTO.UserAppRelationDTOs[0].AppKey;
                platformNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = appId;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                platformNotificationDTO.PlatformPortalURL = _appPortalAppSettings.PlatformPortalClientURL;
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = platform.Language;
                platformNotificationDTO.UserSession = userSession;
                platformNotificationDTO.PortalName = tenantUserSignUpResponseDTO.NewUser.Item1;
                await _platformNotificationHandler.SendPlatformUserWithExistingEmailInviteAsync(platformNotificationDTO);
            }
            else {
                platformNotificationDTO.PasswordCode = tenantUserSignUpResponseDTO.TenantUserDTO.Code;
                platformNotificationDTO.InvitedUserEmail = tenantUserSignUpResponseDTO.TenantUserDTO.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = tenantUserSignUpResponseDTO.TenantUserDTO.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                platformNotificationDTO.InvitedUserAppKey = tenantUserSignUpResponseDTO.UserAppRelationDTOs[0].AppKey;
                platformNotificationDTO.InvitedUserId = tenantUserSignUpResponseDTO.TenantUserDTO.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = appId;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = tenantUserSignUpResponseDTO.TenantUserDTO.FullName;
                platformNotificationDTO.PlatformPortalURL = _appPortalAppSettings.PlatformPortalClientURL;
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = platform.Language;
                platformNotificationDTO.UserSession = userSession;
                platformNotificationDTO.InvitorUserFullName = userSession.UserName;
                await _platformNotificationHandler.SendPlatformUserWithNewEmailInviteAsync(platformNotificationDTO);
            }

            return tenantUserSignUpResponseDTO;

        }

    }
}
