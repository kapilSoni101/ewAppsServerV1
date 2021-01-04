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
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for TenantUser entity.
    /// </summary>
    public class PubTenantUserDS:IPubTenantUserDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;
        IQPublisherAndUserDS _qPublisherAndUserDS;
        IPublisherDS _publisherDS;
        IPublisherNotificationHandler _publisherNotificationHandler;
        ITokenInfoDS _tokenInfoDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public PubTenantUserDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettings, ITokenInfoDS tokenInfoDS,
            IQPublisherAndUserDS qPublisherAndUserDS, IPublisherDS publisherDS, IPublisherNotificationHandler publisherNotificationHandler) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appSettings.Value;
            _qPublisherAndUserDS = qPublisherAndUserDS;
            _publisherDS = publisherDS;
            _publisherNotificationHandler = publisherNotificationHandler;
            _tokenInfoDS = tokenInfoDS;
        }

        #endregion Constructor

        #region Public Methods

        public async Task<List<TenantUserDetailsDTO>> GetAllPublisherUsersAsync(Guid appId, bool deleted) {

            // get user sesion of the current user.
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qPublisherAndUserDS.GetAllPublisherUsersAsync((int)UserTypeEnum.Publisher, userSession.TenantId, appId, deleted);

            foreach(TenantUserDetailsDTO item in tenantUserDetailsDTOs) {
                // Get feature count.        
                item.FeatureCount = GetPublihserPermissionCountByBitMask(item.PermissionBitMask);
            }
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserAndPermissionDetails(Guid tenantUserId, bool deleted) {
            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAndPermissionViewDTO tenantUserAndPermissionViewDTO = await _qPublisherAndUserDS.GetTenantUserDetails(tenantUserId, userSession.TenantId, AppKeyEnum.pub.ToString(), deleted);
            return tenantUserAndPermissionViewDTO;
        }

        public async Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            #region Get user data

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            #endregion Get user data

            #region Notification

            PublisherNotificationDTO publisherNotificationDTO = new PublisherNotificationDTO();
            Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);

            if(tenantUserInfoDTO.NewUser != null && tenantUserInfoDTO.NewUser.Item2) {
                publisherNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                publisherNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                publisherNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                publisherNotificationDTO.InvitedUserAppKey = AppKeyEnum.pub.ToString();
                publisherNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                publisherNotificationDTO.InvitedUserAppId = appId;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                publisherNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, userSession.Subdomain);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.SubDomain = userSession.Subdomain;
                publisherNotificationDTO.UserSession = userSession;
                publisherNotificationDTO.PortalName = tenantUserInfoDTO.NewUser.Item1;
                await _publisherNotificationHandler.SendPublisherUserExistingEmailInvite(publisherNotificationDTO);
            }
            else {
                publisherNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
                publisherNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
                publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
                publisherNotificationDTO.InvitedUserTenantId = userSession.TenantId;
                publisherNotificationDTO.InvitedUserAppKey = AppKeyEnum.pub.ToString();
                publisherNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
                publisherNotificationDTO.InvitedUserAppId = appId;
                publisherNotificationDTO.PublisherCompanyName = publisher.Name;
                publisherNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
                publisherNotificationDTO.PublisherPortalURL = string.Format(_appPortalAppSettings.PublisherPortalClientURL, userSession.Subdomain);
                publisherNotificationDTO.CopyRigthText = publisher.Copyright;
                publisherNotificationDTO.TenantLanguage = publisher.Language;
                publisherNotificationDTO.SubDomain = userSession.Subdomain;
                publisherNotificationDTO.UserSession = userSession;
                publisherNotificationDTO.InvitorUserFullName = userSession.UserName;
                await _publisherNotificationHandler.SendPublisherUserNewEmailInvite(publisherNotificationDTO);
            }

            #endregion Notification

            #region Update invitaion status

            // APi calling to cahnge the status again
            TenantUserIdentificationDTO tenantUserIdentificationDTO = new TenantUserIdentificationDTO();
            tenantUserIdentificationDTO.AppId = appId;
            tenantUserIdentificationDTO.TenantId = userSession.TenantId;
            tenantUserIdentificationDTO.TenantUserId = tenantUserId;

            requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.Invited;
            requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);

            #endregion Update invitaion status
        }

        public async Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            // Deleted tokens
            await _tokenInfoDS.DeleteTokenByTenantUserIdAndTokenType(tenantUserIdentificationDTO.TenantUserId, userSession.TenantId, (int)PublisherTokenTypeEnum.PublisherUserWithNewEmailInvite);

            // APi calling
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.InvitationCanceled;

            // Change user status in .
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO) {

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/infobyemail/" + forgotPasswordDTO.Email.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            PublisherNotificationDTO publisherNotificationDTO = new PublisherNotificationDTO();
            Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == forgotPasswordDTO.TenantId);

            publisherNotificationDTO.PasswordCode = forgotPasswordDTO.Code;
            publisherNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
            publisherNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
            publisherNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
            publisherNotificationDTO.InvitedUserTenantId = forgotPasswordDTO.TenantId;
            publisherNotificationDTO.InvitedUserAppKey = AppKeyEnum.pub.ToString();
            publisherNotificationDTO.PublisherCompanyName = publisher.Name;
            publisherNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
            publisherNotificationDTO.CopyRigthText = publisher.Copyright;
            publisherNotificationDTO.TenantLanguage = publisher.Language;
            publisherNotificationDTO.SubDomain = "";

            UserSession userSession = new UserSession();
            userSession.TenantUserId = tenantUserInfoDTO.TenantUserId;
            userSession.TenantId = forgotPasswordDTO.TenantId;

            publisherNotificationDTO.UserSession = userSession;

            await _publisherNotificationHandler.SendPublisherForgotPasswordEmail(publisherNotificationDTO);

        }

        #endregion Public Methods

        #region Get User Info
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid tenantUserId) {

            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string methodUri = "TenantUser/getuserinfo/" + tenantUserId;
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            TenantUserProfileDTO tenantUserProfileDTO = new TenantUserProfileDTO();
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            tenantUserProfileDTO = await httpRequestProcessor.ExecuteAsync<TenantUserProfileDTO>(requestOptions, false);

            return tenantUserProfileDTO;



        }
        #endregion

        #region Update User Profile
        public async Task<ResponseModelDTO> UpdateTenantUserProfile(TenantUserProfileDTO tenantUserProfileDTO) {

            UserSession session = _userSessionManager.GetSession();

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/profileupdate";

            ResponseModelDTO responseModelDTO = new ResponseModelDTO();
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserProfileDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);

            responseModelDTO = await httpRequestProcessor.ExecuteAsync<ResponseModelDTO>(requestOptions, false);

            return responseModelDTO;

        }

        #endregion Update User Profile

        #region Private Methods

        /// <summary>
        /// Method returns the feature count.
        /// </summary>
        /// <param name="permissionbitmask"></param>
        /// <returns></returns>
        private int GetPublihserPermissionCountByBitMask(long permissionbitmask) {
            int enumCount = Enum.GetNames(typeof(PublisherUserPublisherAppPermissionEnum)).Length;
            List<PublisherUserPublisherAppPermissionEnum> list = new List<PublisherUserPublisherAppPermissionEnum>();
            PublisherUserPublisherAppPermissionEnum bitmask = (PublisherUserPublisherAppPermissionEnum)permissionbitmask;
            foreach(PublisherUserPublisherAppPermissionEnum item in Enum.GetValues(typeof(PublisherUserPublisherAppPermissionEnum))) {
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
