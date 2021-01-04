/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.NotificationService;
using ewApps.Core.ServiceProcessor;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS
{

  /// <summary>
  /// Data service implements all authenticate related api's
  /// </summary>

  public class AuthenticationDS : IAuthenticationDS
  {


    #region Local Member

    IPlatformDS _platformDS;
    IPublisherDS _publisherDS;
    IBusinessDS _businessDS;
    IEntityThumbnailDS _entityThumbnailDS;
    ITokenInfoDS _tokenInfoDataService;
    NotificationAppSettings _notificationAppSetting;
    AppPortalAppSettings _appSettings;

    #endregion Local Member

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationDS"/> class.
    /// </summary>
    /// <param name="businessDS">The business ds.</param>
    /// <param name="notificationAppSettingOption">The notification application setting option.</param>
    /// <param name="platformDS">The platform ds.</param>
    /// <param name="entityThumbnailDS">The entity thumbnail ds.</param>
    /// <param name="publisherDS">The publisher ds.</param>
    /// <param name="tokenInfoDataService">The token information data service.</param>
    public AuthenticationDS(IBusinessDS businessDS, IOptions<NotificationAppSettings> notificationAppSettingOption, IPlatformDS platformDS,
        IPublisherDS publisherDS, ITokenInfoDS tokenInfoDataService, IOptions<AppPortalAppSettings> appSettings, IEntityThumbnailDS entityThumbnailDS)
    {

      _platformDS = platformDS;
      _publisherDS = publisherDS;
      _businessDS = businessDS;
      _entityThumbnailDS = entityThumbnailDS;
      _tokenInfoDataService = tokenInfoDataService;
      _notificationAppSetting = notificationAppSettingOption.Value;
      _appSettings = appSettings.Value;
    }

    #endregion Constructor

    ///<inheritdoc/>
    public async Task<LoginBrandingDTO> ValidateSubdomainAsync(string subDomain, string pKey, int uType, CancellationToken token = default(CancellationToken))
    {

      LoginBrandingDTO loginBrandingDTO = new LoginBrandingDTO();

      TenantInfoDTO tenantInfoDTO = await GetTenantInfoBySubdomain(subDomain, uType);

      switch (uType)
      {

        //for platform
        case (int)UserTypeEnum.Platform:
          ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
          break;
        // for publisher
        case (int)UserTypeEnum.Publisher:
          // ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
          loginBrandingDTO = await ValidatePublisher(tenantInfoDTO, subDomain);
         // loginBrandingDTO.SubDomainName = subDomain;
          break;
        // for business
        case (int)UserTypeEnum.Business:
          //ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
          loginBrandingDTO = await ValidateBusiness(tenantInfoDTO, subDomain);
          //loginBrandingDTO.SubDomainName = subDomain;
          break;
        // for customer
        case (int)UserTypeEnum.Customer:
          //ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
          loginBrandingDTO = await ValidateBusiness(tenantInfoDTO, subDomain);
          //loginBrandingDTO.SubDomainName = subDomain;
          break;
        // for vendor
        case (int)UserTypeEnum.Vendor:
          //ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
          loginBrandingDTO = await ValidateBusiness(tenantInfoDTO, subDomain);
          //loginBrandingDTO.SubDomainName = subDomain;
          break;
        default:
          break;
      }

      return loginBrandingDTO;
    }



    ///<inheritdoc/>
    public async Task<TokenDataDTO> GetTokenInfoByTokenIdAsync(Guid tokenId, string pKey, string IdentityAppClientId, int uType, CancellationToken token = default(CancellationToken))
    {

      TokenInfo tokenInfo = await _tokenInfoDataService.GetAsync(tokenId);
      TokenDataDTO tokenDataDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenDataDTO>(tokenInfo.TokenData);

      TenantInfoDTO tenantInfoDTO = await GetTenantInfoByTenantId(tokenDataDTO.TenantId, uType);

      switch (uType)
      {

        case (int)UserTypeEnum.Platform://for platform
          tokenDataDTO = await GetPlatformLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Publisher: // for publisher
          tokenDataDTO = await GetPublisherLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Business:// for business
          tokenDataDTO = await GetBusinessLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Customer:// for customer
          tokenDataDTO = await GetCustomerLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Vendor:// for vendor
          tokenDataDTO = await GetVendorLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        default:
          break;
      }
      return tokenDataDTO;

    }

    ///<inheritdoc/>
    public async Task<TokenDataDTO> GetTokenInfoBySubdomainAsync(string subDomain, string pKey, string IdentityAppClientId, int uType, CancellationToken token = default(CancellationToken))
    {

      TokenDataDTO tokenDataDTO = new TokenDataDTO();
      TenantInfoDTO tenantInfoDTO = await GetTenantInfoBySubdomain(subDomain, uType);

      switch (uType)
      {

        case (int)UserTypeEnum.Platform://for platform
          tokenDataDTO = await GetPlatformLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Publisher: // for publisher
          tokenDataDTO = await GetPublisherLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Business:// for business
          tokenDataDTO = await GetBusinessLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Customer:// for customer
          tokenDataDTO = await GetCustomerLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        case (int)UserTypeEnum.Vendor:// for vendor
          tokenDataDTO = await GetVendorLoginData(tenantInfoDTO, tokenDataDTO);
          break;
        default:
          break;
      }

      return tokenDataDTO;
    }

    /// <summary>
    /// Gets the tenant information by subdomain.
    /// </summary>
    /// <param name="subdomain">The subdomain.</param>
    /// <param name="uType">Type of the u.</param>
    /// <returns></returns>
    private async Task<TenantInfoDTO> GetTenantInfoBySubdomain(string subdomain, int uType)
    {

      string baseuri = _appSettings.AppMgmtApiUrl;
      string requesturl = "appmgmtauth/tenantinfo/" + subdomain + "/" + uType;

      TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Get;

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      tenantInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantInfoDTO>(requestOptions, false);

      return tenantInfoDTO;

    }


    /// <summary>
    /// Gets the tenant information by tenant identifier.
    /// </summary>
    /// <param name="tenantId">The tenant identifier.</param>
    /// <param name="uType">Type of the u.</param>
    /// <returns></returns>
    private async Task<TenantInfoDTO> GetTenantInfoByTenantId(Guid tenantId, int uType)
    {

      string baseuri = _appSettings.AppMgmtApiUrl;
      string requesturl = "appmgmtauth/tenantinfotenantid/" + tenantId + "/" + uType;

      TenantInfoDTO tenantInfoDTO = new TenantInfoDTO();

      RequestOptions requestOptions = new RequestOptions();
      requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
      requestOptions.Method = requesturl;
      requestOptions.MethodData = null;
      requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
      requestOptions.ServiceRequestType = RequestTypeEnum.Get;

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
      tenantInfoDTO = await httpRequestProcessor.ExecuteAsync<TenantInfoDTO>(requestOptions, false);

      return tenantInfoDTO;

    }

    ///<inheritdoc/>
    public async Task<bool> CheckTokenAsync(Guid tokenId)
    {
      TokenInfo tokenInfo = await _tokenInfoDataService.FindAsync(t => t.ID == tokenId && t.Deleted == false);
      if (tokenInfo == null)
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    ///<inheritdoc/>
    public async Task DeleteTokenInfoAsync(TokenInfoDTO tokenInfoDTO)
    {
      await _tokenInfoDataService.DeleteTokenAsync(tokenInfoDTO);
    }

    #region Get Login Data

    /// <summary>
    /// Gets the platform login data.
    /// </summary>
    /// <param name="tenantInfoDTO">The tenant information dto.</param>
    /// <returns></returns>
    private async Task<TokenDataDTO> GetPlatformLoginData(TenantInfoDTO tenantInfoDTO, TokenDataDTO tokenData)
    {

      ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
      //TokenDataDTO tokenData = new TokenDataDTO();

      ThumbnailAddAndUpdateDTO thumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId((Guid)tenantInfoDTO.TenantId);
      if (thumbnailAddAndUpdateDTO != null)
      {
        tokenData.PublisherLogo = thumbnailAddAndUpdateDTO.ThumbnailUrl;
      }

      tokenData.PublisherName = tenantInfoDTO.TenantName;
      tokenData.BusinessLogo = "";
      tokenData.BusinessName = "";
      tokenData.TenantId = tenantInfoDTO.TenantId;
      tokenData.SubDomain = tenantInfoDTO.SubDomainName;
      tokenData.Copyright = platform.Copyright;
      tokenData.NotificationUrl = _notificationAppSetting.NotificationServiceUrl + AppPortalConstants.PlatForgotPasswordUrl;
      tokenData.ApplicationName = platform.Name;
      tokenData.UserType = (int)UserTypeEnum.Platform;
      return tokenData;

    }


    /// <summary>
    /// Gets the publisher login data.
    /// </summary>
    /// <param name="tenantInfoDTO">The tenant information dto.</param>
    /// <returns></returns>
    private async Task<TokenDataDTO> GetPublisherLoginData(TenantInfoDTO tenantInfoDTO, TokenDataDTO tokenData)
    {

      ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
      //TokenDataDTO tokenData = new TokenDataDTO();

      ThumbnailAddAndUpdateDTO thumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId((Guid)tenantInfoDTO.TenantId);
      if (thumbnailAddAndUpdateDTO != null)
      {
        tokenData.PublisherLogo = thumbnailAddAndUpdateDTO.ThumbnailUrl;
      }

      tokenData.PublisherName = tenantInfoDTO.TenantName;
      tokenData.BusinessLogo = "";
      tokenData.BusinessName = "";
      tokenData.TenantId = tenantInfoDTO.TenantId;
      tokenData.SubDomain = tenantInfoDTO.SubDomainName;
      tokenData.Copyright = publisher.Copyright;
      tokenData.NotificationUrl = _notificationAppSetting.NotificationServiceUrl + AppPortalConstants.PubForgotPasswordUrl;
      tokenData.ApplicationName = publisher.Name;
      tokenData.UserType = (int)UserTypeEnum.Publisher;

      if (tokenData.Copyright == "" || tokenData.Copyright == null)
      {
        ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.PlatformTenantId);
        tokenData.Copyright = platform.Copyright;
      }
      // tokenData.BusinessName = tenantLogoDTOPlatform.TenantName;

      return tokenData;
    }

    /// <summary>
    /// Gets the business login data.
    /// </summary>
    /// <param name="tenantInfoDTO">The tenant information dto.</param>
    /// <returns></returns>
    private async Task<TokenDataDTO> GetBusinessLoginData(TenantInfoDTO tenantInfoDTO, TokenDataDTO tokenData)
    {

      //Get Business
      ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
      // TokenDataDTO tokenData = new TokenDataDTO();

      ThumbnailAddAndUpdateDTO thumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId((Guid)tenantInfoDTO.TenantId);
      if (thumbnailAddAndUpdateDTO != null)
      {
        tokenData.BusinessLogo = thumbnailAddAndUpdateDTO.ThumbnailUrl;
      }

      tokenData.TenantId = tenantInfoDTO.TenantId;
      tokenData.BusinessName = business.Name;

      // Get  Publisher, copyright, website from publisher
      ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.PublisherTenantId);
      tokenData.Copyright = publisher.Copyright;
      tokenData.PublisherWebsite = publisher.Website;
      tokenData.PublisherName = publisher.Name;
      tokenData.PublisherLogo = "";

      // Get copyright from platform
      if (tokenData.Copyright == "" || tokenData.Copyright == null)
      {
        ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.PlatformTenantId);
        tokenData.Copyright = platform.Copyright;
      }

      tokenData.NotificationUrl = _notificationAppSetting.NotificationServiceUrl + AppPortalConstants.BizForgotPasswordUrl;
      tokenData.UserType = (int)UserTypeEnum.Business;
      //tokenData.AppKey = Core.Common.AppKeyEnum.biz.ToString();
      //tokenData.ApplicationName = "Temp";// tenantLogoDTO.ApplicationName;

      return tokenData;
    }
    /// <returns></returns>
    private async Task<TokenDataDTO> GetCustomerLoginData(TenantInfoDTO tenantInfoDTO, TokenDataDTO tokenData)
    {

      //Get Business
      ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
      // TokenDataDTO tokenData = new TokenDataDTO();

      ThumbnailAddAndUpdateDTO thumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId((Guid)tenantInfoDTO.TenantId);
      if (thumbnailAddAndUpdateDTO != null)
      {
        tokenData.BusinessLogo = thumbnailAddAndUpdateDTO.ThumbnailUrl;
      }

      tokenData.TenantId = tenantInfoDTO.TenantId;
      tokenData.BusinessName = business.Name;

      // Get  Publisher, copyright, website from publisher
      ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.PublisherTenantId);
      tokenData.Copyright = publisher.Copyright;
      tokenData.PublisherWebsite = publisher.Website;
      tokenData.PublisherName = publisher.Name;
      tokenData.PublisherLogo = "";

      // Get copyright from platform
      if (tokenData.Copyright == "" || tokenData.Copyright == null)
      {
        ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.PlatformTenantId);
        tokenData.Copyright = platform.Copyright;
      }

      tokenData.NotificationUrl = _notificationAppSetting.NotificationServiceUrl + AppPortalConstants.CustForgotPasswordUrl;
      tokenData.UserType = (int)UserTypeEnum.Customer;
      //tokenData.AppKey = Core.Common.AppKeyEnum.biz.ToString();
      //tokenData.ApplicationName = "Temp";// tenantLogoDTO.ApplicationName;

      return tokenData;
    }

    private async Task<TokenDataDTO> GetVendorLoginData(TenantInfoDTO tenantInfoDTO, TokenDataDTO tokenData) {

      //Get Business
      ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
      // TokenDataDTO tokenData = new TokenDataDTO();

      ThumbnailAddAndUpdateDTO thumbnailAddAndUpdateDTO = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId((Guid)tenantInfoDTO.TenantId);
      if(thumbnailAddAndUpdateDTO != null) {
        tokenData.BusinessLogo = thumbnailAddAndUpdateDTO.ThumbnailUrl;
      }

      tokenData.TenantId = tenantInfoDTO.TenantId;
      tokenData.BusinessName = business.Name;

      // Get  Publisher, copyright, website from publisher
      ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.PublisherTenantId);
      tokenData.Copyright = publisher.Copyright;
      tokenData.PublisherWebsite = publisher.Website;
      tokenData.PublisherName = publisher.Name;
      tokenData.PublisherLogo = "";

      // Get copyright from platform
      if(tokenData.Copyright == "" || tokenData.Copyright == null) {
        ewApps.AppPortal.Entity.Platform platform = await _platformDS.FindAsync(t => t.TenantId == tenantInfoDTO.PlatformTenantId);
        tokenData.Copyright = platform.Copyright;
      }

      tokenData.NotificationUrl = _notificationAppSetting.NotificationServiceUrl + AppPortalConstants.VendForgotPasswordUrl;
      tokenData.UserType = (int)UserTypeEnum.Vendor;
      //tokenData.AppKey = Core.Common.AppKeyEnum.biz.ToString();
      //tokenData.ApplicationName = "Temp";// tenantLogoDTO.ApplicationName;

      return tokenData;
    }
    #endregion Get Login Data

    #region  Check Validation

    private async Task<LoginBrandingDTO> ValidatePublisher(TenantInfoDTO tenantInfoDTO, string subDomain)
    {

      if (tenantInfoDTO != null)
      {
        ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
        LoginBrandingDTO loginBrandingDTO = new LoginBrandingDTO();
        loginBrandingDTO.SubDomainName = subDomain;

        if (publisher != null)
        {
          if (publisher.Active && !publisher.Deleted)
          {
            loginBrandingDTO.Active = true;
          }
          else
          {
            loginBrandingDTO.Active = false;
            loginBrandingDTO.ValidationType = AppPortalConstants.Inactive;
            loginBrandingDTO.ValidationTypeData = AppPortalConstants.PublisherPortalName;
          }
        }
        return loginBrandingDTO;
      }
      return null;
    }

    // this function will work for business and customer subdomain validation
    private async Task<LoginBrandingDTO> ValidateBusiness(TenantInfoDTO tenantInfoDTO, string subDomain)
    {

      if (tenantInfoDTO != null)
      {
        bool validSubdomain = false;

        ewApps.AppPortal.Entity.Business business = await _businessDS.FindAsync(t => t.TenantId == tenantInfoDTO.TenantId);
        LoginBrandingDTO loginBrandingDTO = new LoginBrandingDTO();
        loginBrandingDTO.SubDomainName = subDomain;
        loginBrandingDTO.Active = tenantInfoDTO.Active;
        loginBrandingDTO.Deleted = tenantInfoDTO.Deleted;


        if (!validSubdomain && tenantInfoDTO.Deleted)
        { // if business tenant is deleted
          loginBrandingDTO.Deleted = true;
          loginBrandingDTO.ValidationType = AppPortalConstants.Delete;
          validSubdomain = true;
        }
        if (!validSubdomain && !tenantInfoDTO.Active)
        { // if business tenant is Inactive
          loginBrandingDTO.Active = false;
          loginBrandingDTO.ValidationType = AppPortalConstants.Inactive;
          validSubdomain = true;
        }
        if (!validSubdomain)
        {
          ewApps.AppPortal.Entity.Publisher publisher = await _publisherDS.FindAsync(t => t.TenantId == tenantInfoDTO.PublisherTenantId);
          if (publisher != null)
          {
            if (publisher.Active)
            {  // if publisher is Inactive
              loginBrandingDTO.Active = true;
            }
            else
            {
              loginBrandingDTO.Active = false;
              loginBrandingDTO.ValidationType = AppPortalConstants.Inactive;
            }
          }
        }
        loginBrandingDTO.ValidationTypeData = AppPortalConstants.BusinessPortalName;

        return loginBrandingDTO;
      }
      return null;
    }

    #endregion Check Validation
  }
}
