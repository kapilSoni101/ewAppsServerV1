using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.DS {

    public class VendorNotificationHandler:IVendorNotificationHandler {

        IVendorNotificationService _vendorNotificationService;
        AppPortalAppSettings _appPortalAppSettings;
        ITokenInfoDS _tokenInfoDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;

        public VendorNotificationHandler(IOptions<AppPortalAppSettings> appPortalAppSettings, IVendorNotificationService vendorNotificationService, ITokenInfoDS tokenInfoDS, IUnitOfWork unitOfWork, IUserSessionManager userSessionManager) {
            _tokenInfoDS = tokenInfoDS;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
            _vendorNotificationService = vendorNotificationService;
            _appPortalAppSettings = appPortalAppSettings.Value;
        }

        ///<inheritdoc/>
        public async Task GenerateNewVendorNewEmailIdInvitedNotification(BusinessAccountNotificationDTO notificationDTO) {

            #region xslt arguments
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="businessPartnerType"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="invitedUserName"/>
            //<xsl:param name="applicationName"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="invitedUserEmailId"/>
            //<xsl:param name="portalURL"/>
            //<xsl:param name="copyrightText"/>
            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = notificationDTO.PasswordCode;
            tokenDataDTO.Email = notificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = notificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = notificationDTO.TenantId;
            tokenDataDTO.AppKey = notificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Vendor;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = notificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = notificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = notificationDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)VendorTokenTypeEnum.VendorInviteWithNewEmail;
            tokenInfo.UserType = (int)UserTypeEnum.Vendor;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.Save();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", notificationDTO.PublisherName);
            eventData.Add("businessCompanyName", notificationDTO.BusinessCompanyName);
            eventData.Add("businessPartnerType", notificationDTO.PartnerType);
            eventData.Add("businessPartnerCompanyName", notificationDTO.BusinessPartnerCompanyName);
            eventData.Add("invitedUserName", notificationDTO.InvitedUserFullName);
            eventData.Add("applicationName", notificationDTO.ApplicationName);
            eventData.Add("subDomain", notificationDTO.SubDomain);
            eventData.Add("invitedUserEmailId", notificationDTO.InvitedUserEmail);
            eventData.Add("portalURL", notificationDTO.PortalURL);
            eventData.Add("copyrightText", notificationDTO.CopyRightText);

            eventData.Add("appuserid", notificationDTO.InvitedUserId.ToString());
            eventData.Add("appid", notificationDTO.InvitedUserAppId.ToString());
            eventData.Add("tenantid", notificationDTO.TenantId.ToString());

            eventData.Add("appkey", notificationDTO.InvitedUserAppKey);

            Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
            deeplinkInfo.Add("tenantid", notificationDTO.TenantId.ToString());
            deeplinkInfo.Add("appuserid", notificationDTO.InvitedUserId.ToString());
            deeplinkInfo.Add("identityserveruserid", notificationDTO.InvitedUserIdentityUserId.ToString());
            deeplinkInfo.Add("loginEmail", notificationDTO.InvitedUserEmail);
            deeplinkInfo.Add("usertype", UserTypeEnum.Vendor.ToString());
            deeplinkInfo.Add("code", notificationDTO.PasswordCode);
            deeplinkInfo.Add("partnerid", " 123 ");
            deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
            deeplinkInfo.Add("subDomain", notificationDTO.SubDomain);
            deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(notificationDTO.UserSession));

            // Send notification.
            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)VendorNotificationEventEnum.VendorInviteWithNewEmail;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = notificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _vendorNotificationService.GenerateNotificationAsync(generateNotificationDTO);

            // await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite, false, eventDataDict, notificationDTO.TenantUser.ID);
        }

        #region Onboard Notification

        /// <summary>
        /// Vendor User Vendor Portal-Setup OnBoard.
        /// </summary>
        /// <param name="vendorOnBoardNotificationDTO"></param>
        /// <returns></returns>
        public async Task SendVendorSetupUserOnBoardNotificationAsync(VendorOnBoardNotificationDTO vendorOnBoardNotificationDTO) {
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(vendorOnBoardNotificationDTO.DateTimeFormat);
            vendorOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(vendorOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
            vendorOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(vendorOnBoardNotificationDTO.JoinedDate.Value, vendorOnBoardNotificationDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(vendorOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
            eventData.Add("onboardTime", formatDate);

            eventData.Add("VendSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, vendorOnBoardNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, vendorOnBoardNotificationDTO.BusinessTenantId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, vendorOnBoardNotificationDTO.BusinessPartnerTenantId.ToString());
            // ToDo: Replace it with some entity type enum.
            eventData.Add("asAppId", vendorOnBoardNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
            eventData.Add("asTargetEntityId", vendorOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("onboardedUserId", vendorOnBoardNotificationDTO.OnBoardedUserId.ToString());

            eventData.Add("publisherName", vendorOnBoardNotificationDTO.PublisherName);
            eventData.Add("appName", vendorOnBoardNotificationDTO.AppName);
            eventData.Add("vendorName", vendorOnBoardNotificationDTO.VendorName);
            eventData.Add("onboardedUserName", vendorOnBoardNotificationDTO.UserName);
            eventData.Add("copyrightText", vendorOnBoardNotificationDTO.Copyright);
            eventData.Add("subDomain", vendorOnBoardNotificationDTO.SubDomain);
            eventData.Add("portalURL", vendorOnBoardNotificationDTO.PortalURL);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(vendorOnBoardNotificationDTO.UserSessionInfo));

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Vendor;
            generateNotificationDTO.EventId = (long)VendorNotificationEventEnum.VendorUserSetupAppOnboard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = vendorOnBoardNotificationDTO.OnBoardedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _vendorNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        /// <summary>
        /// Customer Portal customer Setup user on board notification
        /// </summary>
        /// <param name="vendorOnBoardNotificationDTO"></param>
        /// <returns></returns>
        public async Task SendVendorSetupUserOnBoardNotificationAsync(VendorOnBoardNotificationDTO vendorOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(vendorOnBoardNotificationDTO.DateTimeFormat);
            vendorOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(vendorOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
            vendorOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(vendorOnBoardNotificationDTO.JoinedDate.Value, vendorOnBoardNotificationDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(vendorOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
            eventData.Add("onboardTime", formatDate);

            // TODO: Nitin: Use app id from database.
            eventData.Add("CustSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, vendorOnBoardNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, vendorOnBoardNotificationDTO.BusinessTenantId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, vendorOnBoardNotificationDTO.BusinessPartnerTenantId.ToString());
            // ToDo: Replace it with some entity type enum.
            eventData.Add("asAppId", vendorOnBoardNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
            eventData.Add("asTargetEntityId", vendorOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("onboardedUserId", vendorOnBoardNotificationDTO.OnBoardedUserId.ToString());

            eventData.Add("publisherName", vendorOnBoardNotificationDTO.PublisherName);
            eventData.Add("appName", vendorOnBoardNotificationDTO.AppName);
            eventData.Add("vendorName", vendorOnBoardNotificationDTO.VendorName);
            eventData.Add("onboardedUserName", vendorOnBoardNotificationDTO.UserName);
            //eventData.Add("onboardTime", customerOnBoardNotificationDTO.JoinedDate.ToString());
            eventData.Add("copyrightText", vendorOnBoardNotificationDTO.Copyright);
            eventData.Add("subDomain", vendorOnBoardNotificationDTO.SubDomain);
            eventData.Add("portalURL", vendorOnBoardNotificationDTO.PortalURL);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(vendorOnBoardNotificationDTO.UserSessionInfo));

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Vendor;
            generateNotificationDTO.EventId = (long)VendorNotificationEventEnum.VendorUserSetupAppOnboard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = vendorOnBoardNotificationDTO.OnBoardedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _vendorNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }
        #endregion

        #region Private Method

        private NotificationUserSessionDTO MapNotitificationUserSession(UserSession userSession) {
            NotificationUserSessionDTO NotificationUserSessionDTO = new NotificationUserSessionDTO();
            NotificationUserSessionDTO.AppId = userSession.AppId;
            NotificationUserSessionDTO.ID = userSession.ID;
            NotificationUserSessionDTO.IdentityServerId = userSession.IdentityServerId;
            NotificationUserSessionDTO.IdentityToken = userSession.IdentityToken;
            NotificationUserSessionDTO.Subdomain = userSession.Subdomain;
            NotificationUserSessionDTO.TenantId = userSession.TenantId;
            NotificationUserSessionDTO.TenantName = userSession.TenantName;
            NotificationUserSessionDTO.TenantUserId = userSession.TenantUserId;
            NotificationUserSessionDTO.UserName = userSession.UserName;
            NotificationUserSessionDTO.UserType = userSession.UserType;
            return NotificationUserSessionDTO;
        }

        #endregion Private Method

    //23
    ///<inheritdoc/>
    public async Task GenerateVendorUserForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO)
    {

      #region xslt arguments
      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="businessUserName"/>
      //<xsl:param name="businessUserID"/>
      //<xsl:param name="copyrightText"/>
      #endregion xslt arguments

      #region TokeInfo

      TokenDataDTO tokenDataDTO = new TokenDataDTO();
      tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
      tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
      tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
      tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
      tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
      tokenDataDTO.UserType = businessNotificationDTO.UserType;

      TokenInfo tokenInfo = new TokenInfo();
      tokenInfo.ID = Guid.NewGuid();
      tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
      tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
      tokenInfo.CreatedDate = DateTime.UtcNow;
      tokenInfo.TenantId = businessNotificationDTO.TenantId;
      tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
      tokenInfo.TokenType = (int)VendorTokenTypeEnum.VendorUserForgotPassword;
      tokenInfo.UserType = businessNotificationDTO.UserType;

      _tokenInfoDS.Add(tokenInfo);
      _unitOfWork.Save();

      #endregion TokeInfo

      // Create notification information
      Dictionary<string, string> eventData = new Dictionary<string, string>();

      eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
      eventData.Add("businessUserName", businessNotificationDTO.InvitedUserFullName);
      eventData.Add("businessUserID", businessNotificationDTO.InvitedUserEmail);
      eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);
      eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);

      eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
      eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
      eventData.Add("tenantid", businessNotificationDTO.TenantId.ToString());

      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
      deeplinkInfo.Add("tenantid", businessNotificationDTO.TenantId.ToString());
      deeplinkInfo.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
      deeplinkInfo.Add("identityserveruserid", businessNotificationDTO.InvitedUserIdentityUserId.ToString());
      deeplinkInfo.Add("loginEmail", businessNotificationDTO.InvitedUserEmail);
      deeplinkInfo.Add("usertype", UserTypeEnum.Business.ToString());
      deeplinkInfo.Add("code", businessNotificationDTO.PasswordCode);
      deeplinkInfo.Add("partnerid", "123");
      deeplinkInfo.Add("tokeninfoid", tokenInfo.ID.ToString());
      deeplinkInfo.Add("subDomain", businessNotificationDTO.SubDomain);
      deeplinkInfo.Add("tokentype", tokenInfo.TokenType.ToString());

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotificationDTO.UserSession));

      // Send notification.
      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)VendorNotificationEventEnum.VendorUserForgotPassword ;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _vendorNotificationService.GenerateNotificationAsync(generateNotificationDTO);

      //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
    }


  }

}
