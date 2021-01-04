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
using System.Linq;
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

    /// <summary>
    /// This class implements standard business logic and operations for appUser entity.
    /// </summary>
    public class PlatTenantUserDS:IPlatTenantUserDS {

        #region Local Member 

        IPlatformAppUserAccess _entityAccess;
        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appSettings;
        IQPlatformAndUserDS _qPlatformAndUserDS;
        IRoleDS _roleDS;
        IRoleLinkingDS _roleLinkingDS;
        IUnitOfWork _unitOfWork;
        IPlatformDS _platformDS;
        IPlatformNotificationHandler _platformNotificationHandler;
        ITokenInfoDS _tokenInfoDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public PlatTenantUserDS(IPlatformAppUserAccess entityAccess, IUserSessionManager userSessionManager, IRoleLinkingDS roleLinkingDS, IPlatformNotificationHandler platformNotificationHandler,
            IQPlatformAndUserDS qPlatformAndUserDS, IRoleDS roleDS, ITokenInfoDS tokenInfoDS, IOptions<AppPortalAppSettings> appSettings, IUnitOfWork unitOfWork, IPlatformDS platformDS) {
            _entityAccess = entityAccess;
            _userSessionManager = userSessionManager;
            _appSettings = appSettings.Value;
            _qPlatformAndUserDS = qPlatformAndUserDS;
            _roleDS = roleDS;
            _roleLinkingDS = roleLinkingDS;
            _unitOfWork = unitOfWork;
            _platformNotificationHandler = platformNotificationHandler;
            _platformDS = platformDS;
            _tokenInfoDS = tokenInfoDS;
        }

        #endregion Constructor

        #region Public Methods

        #region Get 

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetPlatformTenantUsers(Guid appId, bool deleted) {
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qPlatformAndUserDS.GetAllPlatfromUsersAsync((int)UserTypeEnum.Platform, userSession.TenantId, appId, deleted);

            foreach(TenantUserDetailsDTO item in tenantUserDetailsDTOs) {
                // Get feature count.        
                item.FeatureCount = GetPlatformPermissionCountByBitMask(item.PermissionBitMask);
            }
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid tenantUserId) {

            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appSettings.AppMgmtApiUrl;
            string methodUri = "TenantUser/getuserinfo/" + tenantUserId;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            TenantUserProfileDTO tenantUserProfileDTO = new TenantUserProfileDTO();
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            tenantUserProfileDTO = await httpRequestProcessor.ExecuteAsync<TenantUserProfileDTO>(requestOptions, false);

            return tenantUserProfileDTO;
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserAndPermissionDetails(Guid tenantUserId, bool deleted) {
            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAndPermissionViewDTO tenantUserAndPermissionViewDTO = await _qPlatformAndUserDS.GetTenantUserDetails(tenantUserId, userSession.TenantId, AppKeyEnum.plat.ToString(), deleted);
            return tenantUserAndPermissionViewDTO;
        }

        #endregion

        #region Update 

        ///<inheritdoc/>
        public async Task<ResponseModelDTO> UpdateTenantUserProfile(TenantUserProfileDTO tenantUserProfileDTO) {

            UserSession session = _userSessionManager.GetSession();

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/profileupdate";

            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserProfileDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);

            responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);

            return responseModelDTO;
        }

        ///<inheritdoc/>
        public async Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            #region Get user data

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            #endregion Get USer data

            #region Notification

            PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
            Platform platform = await _platformDS.FindAsync(p => p.TenantId == userSession.TenantId);

            if(tenantUserInfoDTO.NewUser != null && tenantUserInfoDTO.NewUser.Item2) {
                platformNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                platformNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                platformNotificationDTO.InvitedUserAppKey = AppKeyEnum.plat.ToString();
                platformNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = appId;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                platformNotificationDTO.PlatformPortalURL = _appSettings.PlatformPortalClientURL;
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = platform.Language;
                platformNotificationDTO.UserSession = userSession;
                platformNotificationDTO.PortalName = tenantUserInfoDTO.NewUser.Item1;
                await _platformNotificationHandler.SendPlatformUserWithExistingEmailInviteAsync(platformNotificationDTO);
            }
            else {
                platformNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                platformNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                platformNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                platformNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                platformNotificationDTO.InvitedUserAppKey = AppKeyEnum.plat.ToString();
                platformNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                platformNotificationDTO.InvitedUserAppId = appId;
                platformNotificationDTO.PlatformCompanyName = platform.Name;
                platformNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                platformNotificationDTO.PlatformPortalURL = _appSettings.PlatformPortalClientURL;
                platformNotificationDTO.CopyRigthText = platform.Copyright;
                platformNotificationDTO.TenantLanguage = platform.Language;
                platformNotificationDTO.UserSession = userSession;
                platformNotificationDTO.InvitorUserFullName = userSession.UserName;
                await _platformNotificationHandler.SendPlatformUserWithNewEmailInviteAsync(platformNotificationDTO);
            }

            #endregion Notification

            #region Update invitaion status

            // APi calling to cahnge the status again
            TenantUserIdentificationDTO tenantUserIdentificationDTO = new TenantUserIdentificationDTO();
            tenantUserIdentificationDTO.AppId = appId;
            tenantUserIdentificationDTO.TenantId = userSession.TenantId;
            tenantUserIdentificationDTO.TenantUserId = tenantUserId;

            requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.Invited;
            requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            #endregion Update invitaion status
        }

        ///<inheritdoc/>
        public async Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            // Deleted tokens
            await _tokenInfoDS.DeleteTokenByTenantUserIdAndTokenType(tenantUserIdentificationDTO.TenantUserId, userSession.TenantId, (int)PlatformTokenTypeEnum.PlatformUserWithNewEmailInvite);

            // APi calling
            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updateinvitationstatus/"  + (int)TenantUserInvitaionStatusEnum.InvitationCanceled;

            // Change user status in .
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        ///<inheritdoc/>
        public async Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO) {

            string baseuri = _appSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/infobyemail/" + forgotPasswordDTO.Email.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            PlatformNotificationDTO platformNotificationDTO = new PlatformNotificationDTO();
            Platform platform = await _platformDS.FindAsync(p => p.TenantId == forgotPasswordDTO.TenantId);

            platformNotificationDTO.PasswordCode = forgotPasswordDTO.Code;
            platformNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
            platformNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
            platformNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
            platformNotificationDTO.InvitedUserTenantId = forgotPasswordDTO.TenantId;
            platformNotificationDTO.InvitedUserAppKey = AppKeyEnum.plat.ToString();
            platformNotificationDTO.PlatformCompanyName = platform.Name;
            platformNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
            platformNotificationDTO.CopyRigthText = platform.Copyright;
            platformNotificationDTO.TenantLanguage = platform.Language;

            UserSession userSession = new UserSession();
            userSession.TenantUserId = tenantUserInfoDTO.TenantUserId;
            userSession.TenantId = forgotPasswordDTO.TenantId;

            platformNotificationDTO.UserSession = userSession;

            await _platformNotificationHandler.SendPlatformUserForgotPasswordEmailAsync(platformNotificationDTO);

        }

        #endregion Update 

        #region Security

        ///<inheritdoc/>
        public IEnumerable<bool> GetLoginUsersAppPermission() {
            return _entityAccess.AccessList(Guid.Empty);
        }

        ///<inheritdoc/>
        private void CheckSecurityOnAdding() {

            if(!_entityAccess.CheckAccess((int)OperationType.Add, Guid.Empty)) {

                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Add,
              Message = string.Format("", "AppUser")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        ///<inheritdoc/>
        private void CheckSecurityOnUpdating() {
            if(!_entityAccess.CheckAccess((int)OperationType.Update, Guid.Empty)) {

                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Update,
              Message = string.Format("", "AppUser")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        ///<inheritdoc/>
        private void CheckSecurityOnDelete() {
            if(!_entityAccess.CheckAccess((int)OperationType.Delete, Guid.Empty)) {
                // Raise security exception
                List<EwpErrorData> errorDataList = new List<EwpErrorData>() {
            new EwpErrorData() {
              ErrorSubType = (int)SecurityErrorSubType.Delete,
              Message = string.Format("", "AppUser")
              }
            };

                throw new EwpSecurityException("", errorDataList);
            }
        }

        #endregion Security

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Method returns the feature count.
        /// </summary>
        /// <param name="permissionbitmask"></param>
        /// <returns></returns>
        private int GetPlatformPermissionCountByBitMask(long permissionbitmask) {
            int enumCount = Enum.GetNames(typeof(PlatformUserPlatformAppPermissionEnum)).Length;
            List<PlatformUserPlatformAppPermissionEnum> list = new List<PlatformUserPlatformAppPermissionEnum>();
            PlatformUserPlatformAppPermissionEnum bitmask = (PlatformUserPlatformAppPermissionEnum)permissionbitmask;
            foreach(PlatformUserPlatformAppPermissionEnum item in Enum.GetValues(typeof(PlatformUserPlatformAppPermissionEnum))) {
                if((bitmask & item) == item) {
                    list.Add(item);
                }
            }
            int count = 0;
            if(list.Count == enumCount) {
                count = enumCount - 2;
            }
            else {
                count = list.Count - 1;
            }
            return count;
        }

       

        #endregion Private Methods

    }
}
