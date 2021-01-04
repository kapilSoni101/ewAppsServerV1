/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:  Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 10 February 2020
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for Vendor TenantUser entity.
    /// </summary>   
    public class VendTenantUserDS : IVendTenantUserDS {

        #region Local Member 

        IUserSessionManager _userSessionManager;
        AppPortalAppSettings _appPortalAppSettings;
        IPublisherDS _pubDS;
        IBusinessDS _businessDS;
    IVendorNotificationHandler _vendorNotificationHandler;
    IQVendorAndUserDS _qVendorAndUserDS;
    IQVendorAppDS _qVendorAppDS;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initialinzing local variables
    /// </summary>
    public VendTenantUserDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettings, IPublisherDS pubDS, IBusinessDS businessDS,
 IQVendorAndUserDS qVendorAndUserDS , IVendorNotificationHandler vendorNotificationHandler, IQVendorAppDS qVendorAppDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appSettings.Value;
            _pubDS = pubDS;
            _businessDS = businessDS;
            _vendorNotificationHandler = vendorNotificationHandler;
      _qVendorAndUserDS = qVendorAndUserDS;
      _qVendorAppDS = qVendorAppDS;
          }

        #endregion Constructor

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

        ///<inheritdoc/>
        public async Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO)
        {

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
          businessNotificationDTO.PublisherName = pubEntity.Name;    //  "eWapps";
          businessNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
          businessNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
          businessNotificationDTO.CopyRightText = pubEntity.Copyright;
          businessNotificationDTO.BusinessCompanyName = busEntity.Name;

          businessNotificationDTO.SubDomain = tenantInfoDTO.SubDomainName;

          businessNotificationDTO.UserType = (int)UserTypeEnum.Vendor;

          UserSession userSession = new UserSession();
          userSession.TenantUserId = tenantUserInfoDTO.TenantUserId;
          userSession.TenantId = forgotPasswordDTO.TenantId;

          // Dummy user session.
          businessNotificationDTO.UserSession = userSession;

          await _vendorNotificationHandler.GenerateVendorUserForgotPasswordAsync(businessNotificationDTO);
        }

        //Get TenantLinking Details 
        private async Task<TenantInfoDTO> GetTenantLinkingDetails(Guid tenantId, TenantType tenantType)
        {

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
   

    #region VendorSetUP App 

    #region Get
    ///<inheritdoc/>
    public async Task<List<TenantUserSetupListDTO>> GetAllVendSetupVendorUsersAsync(Guid businesspartnertenantid, bool deleted) {
      // Get user session.
      UserSession userSession = _userSessionManager.GetSession();

      // Get user list.
      List<TenantUserSetupListDTO> tenantUserDetailsDTOs = await _qVendorAndUserDS.GetAllVendSetupVendorUsersAsync((int)UserTypeEnum.Vendor, userSession.TenantId, businesspartnertenantid, deleted);

      // Filter for deleted users.
      if(deleted) {
        return tenantUserDetailsDTOs.GroupBy(i => i.TenantUserId).Select(a => a.First()).ToList();
      }
      else {
        return tenantUserDetailsDTOs;
      }
    }

    ///<inheritdoc/>
    public async Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, Guid businessPartnerTenantId, bool deleted) {
      UserSession userSession = _userSessionManager.GetSession();
      TenantUserAndAppViewDTO tenantUserAndAppViewDTO = await _qVendorAndUserDS.GetTenantUserDetails(tenantUserId, userSession.TenantId, businessPartnerTenantId, AppKeyEnum.vendsetup.ToString());
      if(tenantUserAndAppViewDTO != null) {
        tenantUserAndAppViewDTO.TenantUserAppPermissionDTOs = await _qVendorAppDS.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, userSession.TenantId, businessPartnerTenantId, AppKeyEnum.vendsetup.ToString(), deleted);
      }
      return tenantUserAndAppViewDTO;
    }
    #endregion Get

    #endregion VendorSetUP App  
  }
}
