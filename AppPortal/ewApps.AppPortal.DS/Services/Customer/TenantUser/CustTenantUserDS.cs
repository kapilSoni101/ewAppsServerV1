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
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS
{

  /// <summary>
  /// Cust Tenant User class contains all add/update/delete/get methods for Tenant User On Customer.    
  /// </summary>
  public class CustTenantUserDS : ICustTenantUserDS
  {

    #region Local Variable 

    IUserSessionManager _userSessionManager;
    AppPortalAppSettings _appPortalAppSettings;
    IQCustomerAndUserDS _qCustomerAndUserDS;
    IQCustomerAppDS _qCustomerAppDS;
    IBusinessNotificationHandler _businessNotificationHandler;
    ICustNotificationHandler _custNotificationHandler;
    IVendorNotificationHandler _vendorNotificationHandler;
    ITokenInfoDS _tokenInfoDS;
    IPublisherDS _pubDS;
    IBusinessDS _businessDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initialize Local Variable 
    /// </summary>
    public CustTenantUserDS(IBusinessDS businessDS, IPublisherDS pubDS, ICustNotificationHandler custNotificationHandler, IUserSessionManager userSessionManager, IQCustomerAndUserDS qCustomerAndUserDS, ITokenInfoDS tokenInfoDS,
        IOptions<AppPortalAppSettings> appSettings, IQCustomerAppDS qCustomerAppDS, IBusinessNotificationHandler businessNotificationHandler
        , IVendorNotificationHandler vendorNotificationHandler)
    {
      _userSessionManager = userSessionManager;
      _appPortalAppSettings = appSettings.Value;
      _qCustomerAndUserDS = qCustomerAndUserDS;
      _qCustomerAppDS = qCustomerAppDS;
      _businessNotificationHandler = businessNotificationHandler;
      _tokenInfoDS = tokenInfoDS;
      _custNotificationHandler = custNotificationHandler;
      _pubDS = pubDS; _businessDS = businessDS; _vendorNotificationHandler = vendorNotificationHandler;
    }

    #endregion

    #region Get User Info

    public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid tenantUserId)
    {

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
    public async Task<ResponseModelDTO> UpdateTenantUserProfile(TenantUserProfileDTO tenantUserProfileDTO)
    {

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
    public async Task<List<TenantUserSetupListDTO>> GetAllCustSetupCustomerUsersAsync(Guid businesspartnertenantid, bool deleted)
    {
      // Get user session.
      UserSession userSession = _userSessionManager.GetSession();
      Guid custAppId = new Guid(AppPortal.Common.AppPortalConstants.CustomerApplicationId);
      // Get user list.
      List<TenantUserSetupListDTO> tenantUserDetailsDTOs = await _qCustomerAndUserDS.GetAllCustSetupCustomerUsersAsync((int)UserTypeEnum.Customer, userSession.TenantId, businesspartnertenantid, deleted);
      TenantUserDetailsDTO tenantUserDetails = await _qCustomerAndUserDS.GetAdminCustomerUsersDetailsAsync((int)UserTypeEnum.Customer, userSession.TenantId, businesspartnertenantid, custAppId, deleted);

      foreach (var item in tenantUserDetailsDTOs)
      {
        if (tenantUserDetails != null)
        {
          item.AdminUserPermissionBitMask = tenantUserDetails.PermissionBitMask;

        }
        else
        {
          item.AdminUserPermissionBitMask = 0;
        }
      }


      // Filter for deleted users.
      if (deleted)
      {
        return tenantUserDetailsDTOs.GroupBy(i => i.TenantUserId).Select(a => a.First()).ToList();
      }
      else
      {
        return tenantUserDetailsDTOs;
      }
    }

    ///<inheritdoc/>
    public async Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, Guid businessPartnerTenantId, bool deleted)
    {
      UserSession userSession = _userSessionManager.GetSession();
      TenantUserAndAppViewDTO tenantUserAndAppViewDTO = await _qCustomerAndUserDS.GetTenantUserDetails(tenantUserId, userSession.TenantId, businessPartnerTenantId, AppKeyEnum.custsetup.ToString());
      if (tenantUserAndAppViewDTO != null)
      {
        tenantUserAndAppViewDTO.TenantUserAppPermissionDTOs = await _qCustomerAppDS.GetTenantUserAppAndPermissionDetailsAsync(tenantUserId, userSession.TenantId, businessPartnerTenantId, AppKeyEnum.custsetup.ToString(), deleted);
      }
      return tenantUserAndAppViewDTO;
    }

    ///<inheritdoc/>
    public async Task<List<AppShortInfoDTO>> GetApplicationForCustomerUserAsync(Guid tenantUserId)
    {
      // Get user session.
      UserSession userSession = _userSessionManager.GetSession();
      return await _qCustomerAndUserDS.GetApplicationForCustomerUserAsync(tenantUserId, userSession.TenantId);
    }

    ///<inheritdoc/>
    public async Task<List<TenantUserDetailsDTO>> GetPaymentAppCustomerUsers(Guid appId, Guid businesspartnertenantid, bool deleted)
    {
      // Get user session.
      UserSession userSession = _userSessionManager.GetSession();
      List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qCustomerAndUserDS.GetPaymentAppCustomerUsers((int)UserTypeEnum.Customer, userSession.TenantId, businesspartnertenantid, appId, deleted);

      // Logic for deleted users.
      if (deleted)
      {
        return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
      }
      else
      {
        return tenantUserDetailsDTOs;
      }
    }

    ///<inheritdoc/>
    public async Task<List<TenantUserDetailsDTO>> GetCustomerAppCustomerUsers(Guid appId, Guid businesspartnertenantid, bool deleted)
    {
      // Get user session.
      UserSession userSession = _userSessionManager.GetSession();
      List<TenantUserDetailsDTO> tenantUserDetailsDTOs = await _qCustomerAndUserDS.GetCustomertAppCustomerUsers((int)UserTypeEnum.Customer, userSession.TenantId, businesspartnertenantid, appId, deleted);
      TenantUserDetailsDTO tenantUserDetails = await _qCustomerAndUserDS.GetAdminCustomerUsersDetailsAsync((int)UserTypeEnum.Customer, userSession.TenantId, businesspartnertenantid, appId, false);
      foreach (var item in tenantUserDetailsDTOs)
      {
        item.AdminUserPermissionBitMask = tenantUserDetails.PermissionBitMask;
      }
      // Logic for deleted users.
      if (deleted)
      {
        return tenantUserDetailsDTOs.GroupBy(i => i.ID).Select(a => a.First()).ToList();
      }
      else
      {
        return tenantUserDetailsDTOs;
      }
    }

    ///<inheritdoc/>
    public async Task<List<AppInfoDTO>> GetAllCustomerApplicationsAsync(Guid businessPartnerTenantId)
    {
      return await _qCustomerAppDS.GetAllCustomerApplicationsAsync(businessPartnerTenantId);
    }

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

      businessNotificationDTO.UserType = (int)UserTypeEnum.Customer;

      UserSession userSession = new UserSession();
      userSession.TenantUserId = tenantUserInfoDTO.TenantUserId;
      userSession.TenantId = forgotPasswordDTO.TenantId;

      // Dummy user session.
      businessNotificationDTO.UserSession = userSession;

      await _custNotificationHandler.GenerateCustomerForgotPasswordAsync(businessNotificationDTO);
    }

    public async Task ReInviteTenantUserAsync(Guid tenantUserId, Guid appId)
    {



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

      if (appId == Guid.Empty)
      {
        List<string> appKeyList = new List<string>();
        appKeyList.Add(AppKeyEnum.custsetup.ToString().ToLower());

        // Get App ID by calling th API.
        string requesturlappkey = "App/getappinfobykey";

        RequestOptions appkeyrequestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, requesturlappkey, appKeyList, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
        ServiceExecutor appkeyhttpRequestProcessor = new ServiceExecutor(baseuri);
        List<AppInfoDTO> appInfoDTO = await appkeyhttpRequestProcessor.ExecuteAsync<List<AppInfoDTO>>(appkeyrequestOptions, false);
        if (appInfoDTO.Count > 0)
          appId = appInfoDTO[0].Id;
      }

      try
      {
        BusinessAccountNotificationDTO businessAccountNotificationDTO = new BusinessAccountNotificationDTO();
        // Publisher publisher = await _publisherDS.FindAsync(p => p.TenantId == userSession.TenantId);
        // Guid appId = tenantUserSignUpDTO.UserAppRelationDTOList.Where(u => u.AppKey == AppKeyEnum.custsetup.ToString()).FirstOrDefault().AppId;

        if (tenantUserInfoDTO.NewUser != null && tenantUserInfoDTO.NewUser.Item2)
        {
          businessAccountNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
          businessAccountNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
          businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
          businessAccountNotificationDTO.TenantId = userSession.TenantId;
          businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.custsetup.ToString();
          businessAccountNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
          businessAccountNotificationDTO.InvitedUserAppId = appId;
          businessAccountNotificationDTO.PublisherName = " ";
          businessAccountNotificationDTO.BusinessCompanyName = " ";
          businessAccountNotificationDTO.PartnerType = "Customer";
          businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
          businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
          businessAccountNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
          businessAccountNotificationDTO.ApplicationName = "Payment";
          businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
          businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.CustomerPortalClientURL, userSession.Subdomain);
          businessAccountNotificationDTO.CopyRightText = " ";
          businessAccountNotificationDTO.UserSession = userSession;
          await _businessNotificationHandler.GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(businessAccountNotificationDTO);
        }
        else
        {
          businessAccountNotificationDTO.PasswordCode = tenantUserInfoDTO.Code;
          businessAccountNotificationDTO.InvitedUserEmail = tenantUserInfoDTO.Email;
          businessAccountNotificationDTO.InvitedUserIdentityUserId = tenantUserInfoDTO.IdentityUserId;
          businessAccountNotificationDTO.TenantId = userSession.TenantId;
          businessAccountNotificationDTO.InvitedUserAppKey = AppKeyEnum.custsetup.ToString();
          businessAccountNotificationDTO.InvitedUserId = tenantUserInfoDTO.TenantUserId;
          businessAccountNotificationDTO.InvitedUserAppId = appId;
          businessAccountNotificationDTO.PublisherName = " ";
          businessAccountNotificationDTO.BusinessCompanyName = " ";
          businessAccountNotificationDTO.PartnerType = "Customer";
          businessAccountNotificationDTO.BusinessPartnerCompanyName = " ";
          businessAccountNotificationDTO.InvitorUserName = userSession.UserName;
          businessAccountNotificationDTO.InvitedUserFullName = tenantUserInfoDTO.FullName;
          businessAccountNotificationDTO.ApplicationName = "Payment";
          businessAccountNotificationDTO.SubDomain = userSession.Subdomain;
          businessAccountNotificationDTO.PortalURL = string.Format(_appPortalAppSettings.CustomerPortalClientURL, userSession.Subdomain);
          businessAccountNotificationDTO.CopyRightText = " ";
          businessAccountNotificationDTO.UserSession = userSession;
          await _businessNotificationHandler.GenerateBusinessPartnerPrimaryUserNewEmailIdInvitedNotification(businessAccountNotificationDTO);
        }
      }
      catch (Exception)
      {
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

    public async Task CancelTenantUserInvitation(TenantUserIdentificationDTO tenantUserIdentificationDTO)
    {

      // Get the current user session.
      UserSession userSession = _userSessionManager.GetSession();

      // Deleted tokens
      await _tokenInfoDS.DeleteTokenByTenantUserIdAndTokenType(tenantUserIdentificationDTO.TenantUserId, userSession.TenantId, (int)BusinessTokenTypeEnum.BusinessPartnerPrimaryNewUserInvite);

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

  }
}
