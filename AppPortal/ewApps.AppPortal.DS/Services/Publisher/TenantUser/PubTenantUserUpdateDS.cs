﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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
    public class PubTenantUserUpdateDS:IPubTenantUserUpdateDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IQPublisherAndUserDS _qPublisherAndUserDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        IUnitOfWork _unitOfWork;
        IPublisherNotificationHandler _publisherNotificationHandler;
        

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public PubTenantUserUpdateDS(IUserSessionManager userSessionManager, IRoleLinkingDS roleLinkingDS,
            IQPublisherAndUserDS qPublisherAndUserDS, IRoleDS roleDS, IOptions<AppPortalAppSettings> appSettings, IUnitOfWork unitOfWork, IPublisherNotificationHandler publisherNotificationHandler) {
            _userSessionManager = userSessionManager;
            _appSettings = appSettings.Value;
            _qPublisherAndUserDS = qPublisherAndUserDS;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _unitOfWork = unitOfWork;
            _publisherNotificationHandler = publisherNotificationHandler;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<UpdateTenantUserResponseDTO> UpdateTenantUserAsync(TenantUserDTO tenantUserDTO, Guid appId) {

            UserSession userSession = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updatepublisheruser/" + userSession.TenantId + "/" + appId;

            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);

            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = await httpRequestProcessor.ExecuteAsync<UpdateTenantUserResponseDTO>(requestOptions, false);

            // Get role/add role and add a entry in rolelinking table  // Add/Update  rolelinking for the user.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(tenantUserDTO.PermissionBitMask, appId, (int)UserTypeEnum.Publisher, tenantUserDTO.TenantUserId);

            RoleLinking roleLinking = await _roleLinkingDS.FindAsync(a => a.TenantUserId == tenantUserDTO.TenantUserId && a.AppId == appId && a.Deleted == false && a.TenantId == userSession.TenantId);
            if(roleLinking != null && (roleLinking.RoleId != roleId)) {

                updateTenantUserResponseDTO.PermissionsChanged = true;
                // get role
                Role role = await _roleDS.GetAsync(roleId);

                // Delete user session if permissions are changed
                await ValidationOnUpdateUser(tenantUserDTO.TenantUserId, userSession.TenantId, (int)UserTypeEnum.Publisher, appId, role);
                await _userSessionManager.DeletedByAppUserAndAppId(tenantUserDTO.TenantUserId, appId);

                roleLinking.RoleId = roleId;
                _roleLinkingDS.UpdateSystemFieldsByOpType(roleLinking, OperationType.Update);
                await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
                _unitOfWork.SaveAll();

                await GeneratePermissionChangeNotification(tenantUserDTO.TenantUserId, userSession.TenantUserId, userSession.TenantId);
            }
            return updateTenantUserResponseDTO;
        }

        private async Task GeneratePermissionChangeNotification(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId) {
            UserSession userSession = _userSessionManager.GetSession();
            PublisherPermissionNotificationDTO publisherPermissionNotificationDTO = new PublisherPermissionNotificationDTO();
            publisherPermissionNotificationDTO = await _qPublisherAndUserDS.GetPublisherUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId);
            if(publisherPermissionNotificationDTO != null) {
                //publisherPermissionNotificationDTO.ActionDate = DateTime.Now;
                publisherPermissionNotificationDTO.UserSession = userSession;
                await _publisherNotificationHandler.PublisherUserPermissionChanged(publisherPermissionNotificationDTO);
            }
        }

        #region Validation 

        private async Task ValidationOnUpdateUser(Guid tenantUserID, Guid tenantId, int userType, Guid appId, Role role) {

            Tuple<bool, Guid> lastAdmin = await _qPublisherAndUserDS.CheckUserIsLastAdminUserAsync(tenantId, userType, appId);

            if(lastAdmin.Item1 && lastAdmin.Item2 == tenantUserID && (role.RoleKey.ToLower() != ewApps.AppPortal.Common.AppPortalConstants.AdminRoleKey.ToLower())) {
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

    }
}
