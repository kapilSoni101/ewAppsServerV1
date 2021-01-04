/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for Business TenantUser entity.
    /// </summary>   
    public class BusTenantUserDS:IBusTenantUserDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;
        IQBusinessAndUserDS _qBusinessAndUserDS;
        IQBusinessAppDS _qBusinessAppDS;
        IBusinessNotificationHandler _businessNotificationHandler;
        ITokenInfoDS _tokenInfoDS;

        IPublisherDS _pubDS;
        IBusinessDS _businessDS;
        IBizNotificationHandler _bizNotificationHandler;




        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public BusTenantUserDS(IBizNotificationHandler bizNotificationHandler, IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettings, ITokenInfoDS tokenInfoDS,
            IPublisherDS pubDS, IBusinessDS businessDS,
            IQBusinessAndUserDS qBusinessAndUserDS, IQBusinessAppDS qBusinessAppDS, IBusinessNotificationHandler businessNotificationHandler) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appSettings.Value;
            _qBusinessAndUserDS = qBusinessAndUserDS;
            _qBusinessAppDS = qBusinessAppDS;
            _businessNotificationHandler = businessNotificationHandler;
            _tokenInfoDS = tokenInfoDS;
            _pubDS = pubDS;
            _businessDS = businessDS;
            _bizNotificationHandler = bizNotificationHandler;
        }

        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<List<TenantUserSetupListDTO>> GetAllBusinessUsersAsync(bool deleted) {

            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();

            // Get user list
            List<TenantUserSetupListDTO> tenantUserDetailsDTOs = await _qBusinessAndUserDS.GetAllBusinessUsersAsync((int)UserTypeEnum.Business, userSession.TenantId, deleted);

            // Remove duplicated deleted logic
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.TenantUserId).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetPaymentAllBusinessUsersAsync(Guid appId, bool deleted) {

            // Get user session and user list.
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qBusinessAndUserDS.GetPaymentAllBusinessUsersAsync((int)UserTypeEnum.Business, userSession.TenantId, appId, deleted);

            // Logic for deleted users.
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetShipmentAllBusinessUsersAsync(Guid appId, bool deleted) {

            // Get user session and user list.
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qBusinessAndUserDS.GetShipmenttAllBusinessUsersAsync((int)UserTypeEnum.Business, userSession.TenantId, appId, deleted);

            // Logic for deleted users.
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetCustomerAppAllBusinessUsersAsync(Guid appId, bool deleted) {

            // Get user session and user list.
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qBusinessAndUserDS.GetCustomerAppAllBusinessUsersAsync((int)UserTypeEnum.Business, userSession.TenantId, appId, deleted);

            // Logic for deleted users.
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetVendorAppAllBusinessUsersAsync(Guid appId, bool deleted) {

            // Get user session and user list.
            UserSession userSession = _userSessionManager.GetSession();
            List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qBusinessAndUserDS.GetVendorAppAllBusinessUsersAsync((int)UserTypeEnum.Business, userSession.TenantId, appId, deleted);

            // Logic for deleted users.
            if(deleted) {
                return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
            }
            else {
                return tenantUserDetailsDTOs;
            }
        }

        ///<inheritdoc/>
        public async Task<List<AppInfoDTO>> GetAllApplicationForTenantAsync(Guid tenantId) {
            return await _qBusinessAppDS.GetAllApplicationForTenantAsync(tenantId);
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, bool deleted) {
            UserSession userSession = _userSessionManager.GetSession();
            TenantUserAndAppViewDTO tenantUserAndAppViewDTO = await _qBusinessAndUserDS.GetTenantUserDetails(tenantUserId, userSession.TenantId, AppKeyEnum.biz.ToString());
            if(tenantUserAndAppViewDTO != null) {
                tenantUserAndAppViewDTO.TenantUserAppPermissionDTOs = await _qBusinessAppDS.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, userSession.TenantId, AppKeyEnum.biz.ToString(), deleted);
            }
            return tenantUserAndAppViewDTO;
        }

        ///<inheritdoc/>
        public async Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId) {
            // Get user session.
            UserSession userSession = _userSessionManager.GetSession();
            return await _qBusinessAndUserDS.GetApplicationForUserAsync(tenantUserId, userSession.TenantId);
        }

        #endregion Get

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

        public async Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO) {

            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/infobyemail/" + forgotPasswordDTO.Email.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            TenantUserInfoDTO tenantUserInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantUserInfoDTO>(requestOptions, false);

            //Get TenantLinking Details 
            TenantInfoDTO tenantInfoDTO = await GetTenantLinkingDetails(forgotPasswordDTO.TenantId, TenantType.Buisness);

            // Now get pub name and copyright
            Entity.Publisher pubEntity = await _pubDS.GetPublisherByPublisherTenantIdAsync(new Guid(tenantInfoDTO.PublisherTenantId.Value.ToString()));

            // Now get business comp name
            Entity.Business busEntity = await _businessDS.GetBusinessByTenantIdAsync(new Guid(tenantInfoDTO.BusinessTenantId.Value.ToString()));

            BusinessAccountNotificationDTO businessNotificationDTO = new BusinessAccountNotificationDTO();
            businessNotificationDTO.PasswordCode = forgotPasswordDTO.Code;
            businessNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
            businessNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
            businessNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
            businessNotificationDTO.TenantId = forgotPasswordDTO.TenantId;
            businessNotificationDTO.InvitedUserAppKey = AppKeyEnum.biz.ToString();
            businessNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
            businessNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
            businessNotificationDTO.PublisherName = pubEntity.Name;    //  "eWapps";
            businessNotificationDTO.CopyRightText = pubEntity.Copyright;
            businessNotificationDTO.BusinessCompanyName = busEntity.Name;
            businessNotificationDTO.UserType = (int)UserTypeEnum.Business;
            businessNotificationDTO.SubDomain = tenantInfoDTO.SubDomainName;

            UserSession userSession = new UserSession();
            userSession.TenantUserId = tenantUserInfoDTO.TenantUserId;
            userSession.TenantId = forgotPasswordDTO.TenantId;

            // Dummy user session.
            businessNotificationDTO.UserSession = userSession;

            await _businessNotificationHandler.GenerateBusinessForgotPasswordAsync(businessNotificationDTO);
        }

        public async Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "tenantuser/info/" + tenantUserId.ToString();

            // Get user detials by calling th API.
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", userSession.ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, requesturl, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
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
                    businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.BusinessPortalClientURL, userSession.Subdomain);
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

            // APi calling to cahnge the status again
            TenantUserIdentificationDTO tenantUserIdentificationDTO = new TenantUserIdentificationDTO();
            tenantUserIdentificationDTO.AppId = appId;
            tenantUserIdentificationDTO.TenantId = userSession.TenantId;
            tenantUserIdentificationDTO.TenantUserId = tenantUserId;

            requesturl = "tenantuser/updateinvitationstatus/" + (int)TenantUserInvitaionStatusEnum.Invited;
            requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturl, tenantUserIdentificationDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            await httpRequestProcessor.ExecuteAsync<bool>(requestOptions, false);
        }

        public async Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Get the current user session.
            UserSession userSession = _userSessionManager.GetSession();

            // Deleted tokens
            await _tokenInfoDS.DeleteTokenByTenantUserIdAndTokenType(tenantUserIdentificationDTO.TenantUserId, userSession.TenantId, (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite);

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

        /// <summary>
        /// Method returns the feature count.
        /// </summary>
        /// <param name="permissionbitmask"></param>
        /// <returns></returns>
        private int GetBusinessPermissionCountByBitMask(long permissionbitmask) {
            int enumCount = Enum.GetNames(typeof(BusinessUserBusinessSetupAppPermissionEnum)).Length;
            List<BusinessUserBusinessSetupAppPermissionEnum> list = new List<BusinessUserBusinessSetupAppPermissionEnum>();
            BusinessUserBusinessSetupAppPermissionEnum bitmask = (BusinessUserBusinessSetupAppPermissionEnum)permissionbitmask;
            foreach(BusinessUserBusinessSetupAppPermissionEnum item in Enum.GetValues(typeof(BusinessUserBusinessSetupAppPermissionEnum))) {
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

        private async Task<TenantInfoDTO> GetTenantLinkingDetails(Guid tenantId, TenantType tenantType) {

            TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

            // Preparing api calling process model.
            #region Get Tenantlinking Information

            string methodUri = "tenant/gettenantlinkinginfo/" + tenantId + "/" + tenantType;
            // List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            // headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl);
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            tenantInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantInfoDTO>(requestOptions, false);

            #endregion

            return tenantInfoDTO;
        }

    }
}
