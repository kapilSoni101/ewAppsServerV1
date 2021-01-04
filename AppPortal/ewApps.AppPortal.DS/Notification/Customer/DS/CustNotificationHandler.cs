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
using ewApps.Core.CommonService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
  public class CustNotificationHandler : ICustNotificationHandler
  {
    ICustNotificationService _custNotificationService;
    IUnitOfWork _unitOfWork;
    AppPortalAppSettings _appPortalAppSettings;
    ITokenInfoDS _tokenInfoDataService;
    IUserSessionManager _userSessionManager;

    public CustNotificationHandler(IUnitOfWork unitOfWork, ITokenInfoDS tokenInfoDataService, ICustNotificationService custNotificationService, IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager)
    {
      _custNotificationService = custNotificationService;
      _appPortalAppSettings = appPortalAppSettings.Value;
      _unitOfWork = unitOfWork;
      _tokenInfoDataService = tokenInfoDataService;
      _userSessionManager = userSessionManager;
    }

    /// <summary>
    ///  Customer Portal Payment App User On Board
    /// </summary>
    /// <param name="customerOnBoardNotificationDTO"></param>
    /// <returns></returns>
    public async Task SendCustPaymentUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO)
    {

      #region XML Email
      //<xsl:param name="publisherName"/>
      //<xsl:param name="paymentAppName"/>
      //<xsl:param name="customerName"/>
      //<xsl:param name="onboardedUserName"/>
      //<xsl:param name="onboardTime"/>
      //<xsl:param name="copyrightText"/>
      //<xsl:param name="subDomain"/>
      //<xsl:param name="portalURL"/>
      #endregion XML Email

      #region XML AS/SMS
      //<xsl:param name="onboardedUserName"/>
      //<xsl:param name="paymentAppName"/>
      //<xsl:param name="onboardTime"/>
      #endregion XML AS/SMS

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerOnBoardNotificationDTO.DateTimeFormat);
      customerOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(customerOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
      customerOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerOnBoardNotificationDTO.JoinedDate.Value, customerOnBoardNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(customerOnBoardNotificationDTO.JoinedDate.Value, format);
      eventData.Add("onboardTime", formatDate);

      eventData.Add("CustSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString()); // TO DO
      eventData.Add(NotificationConstants.AppIdKey, customerOnBoardNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerOnBoardNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, customerOnBoardNotificationDTO.BusinessPartnerTenantId.ToString());
      // ToDo: Replace it with some entity type enum.
      eventData.Add("asAppId", customerOnBoardNotificationDTO.AppId.ToString());
      // eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
      eventData.Add("asTargetEntityId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());
      eventData.Add("onboardedUserId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());

      eventData.Add("publisherName", customerOnBoardNotificationDTO.PublisherName);
      eventData.Add("appName", customerOnBoardNotificationDTO.AppName);
      eventData.Add("customerName", customerOnBoardNotificationDTO.CustomerName);
      eventData.Add("onboardedUserName", customerOnBoardNotificationDTO.UserName);
      //eventData.Add("onboardTime", customerOnBoardNotificationDTO.JoinedDate.ToString());
      eventData.Add("copyrightText", customerOnBoardNotificationDTO.Copyright);
      eventData.Add("subDomain", customerOnBoardNotificationDTO.SubDomain);
      eventData.Add("portalURL", customerOnBoardNotificationDTO.PortalURL);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerOnBoardNotificationDTO.UserSessionInfo));

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.UserDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.NewCustomerPaymentUserOnboard;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerOnBoardNotificationDTO.OnBoardedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }

    /// <summary>
    /// Customer Portal customer app user on board notification
    /// </summary>
    /// <param name="customerOnBoardNotificationDTO"></param>
    /// <returns></returns>
    public async Task SendCustCustomerUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO)
    {

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerOnBoardNotificationDTO.DateTimeFormat);
      customerOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(customerOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
      customerOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerOnBoardNotificationDTO.JoinedDate.Value, customerOnBoardNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(customerOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
      eventData.Add("onboardTime", formatDate);


      eventData.Add("CustSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString()); // TO DO
      eventData.Add(NotificationConstants.AppIdKey, customerOnBoardNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerOnBoardNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, customerOnBoardNotificationDTO.BusinessPartnerTenantId.ToString());
      // ToDo: Replace it with some entity type enum.
      eventData.Add("asAppId", customerOnBoardNotificationDTO.AppId.ToString());
      // eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
      eventData.Add("asTargetEntityId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());
      eventData.Add("onboardedUserId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());

      eventData.Add("publisherName", customerOnBoardNotificationDTO.PublisherName);
      eventData.Add("appName", customerOnBoardNotificationDTO.AppName);
      eventData.Add("customerName", customerOnBoardNotificationDTO.CustomerName);
      eventData.Add("onboardedUserName", customerOnBoardNotificationDTO.UserName);
      //eventData.Add("onboardTime", customerOnBoardNotificationDTO.JoinedDate.ToString());
      eventData.Add("copyrightText", customerOnBoardNotificationDTO.Copyright);
      eventData.Add("subDomain", customerOnBoardNotificationDTO.SubDomain);
      eventData.Add("portalURL", customerOnBoardNotificationDTO.PortalURL);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerOnBoardNotificationDTO.UserSessionInfo));

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.UserDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.NewCustomerCustomerUserOnboard;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerOnBoardNotificationDTO.OnBoardedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }


    /// <summary>
    /// Customer Portal customer Setup user on board notification
    /// </summary>
    /// <param name="customerOnBoardNotificationDTO"></param>
    /// <returns></returns>
    public async Task SendCustSetupUserOnBoardNotificationAsync(CustomerOnBoardNotificationDTO customerOnBoardNotificationDTO)
    {

      Dictionary<string, string> eventData = new Dictionary<string, string>();
      if (String.IsNullOrEmpty(customerOnBoardNotificationDTO.DateTimeFormat))
      {
        customerOnBoardNotificationDTO.DateTimeFormat = "DD/MM/YYYY";
      }
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerOnBoardNotificationDTO.DateTimeFormat);
      customerOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(customerOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
      customerOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerOnBoardNotificationDTO.JoinedDate.Value, customerOnBoardNotificationDTO.TimeZone, false);
      string formatDate = DateTimeHelper.FormatDate(customerOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
      eventData.Add("onboardTime", formatDate);

      eventData.Add("CustSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString()); // TO DO            
      eventData.Add(NotificationConstants.AppIdKey, customerOnBoardNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerOnBoardNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, customerOnBoardNotificationDTO.BusinessPartnerTenantId.ToString());
      // ToDo: Replace it with some entity type enum.
      eventData.Add("asAppId", customerOnBoardNotificationDTO.AppId.ToString());
      eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
      eventData.Add("asTargetEntityId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());
      eventData.Add("onboardedUserId", customerOnBoardNotificationDTO.OnBoardedUserId.ToString());

      eventData.Add("publisherName", customerOnBoardNotificationDTO.PublisherName);
      eventData.Add("appName", customerOnBoardNotificationDTO.AppName);
      eventData.Add("customerName", customerOnBoardNotificationDTO.CustomerName);
      eventData.Add("onboardedUserName", customerOnBoardNotificationDTO.UserName);
      //eventData.Add("onboardTime", customerOnBoardNotificationDTO.JoinedDate.ToString());
      eventData.Add("copyrightText", customerOnBoardNotificationDTO.Copyright);
      eventData.Add("subDomain", customerOnBoardNotificationDTO.SubDomain);
      eventData.Add("portalURL", customerOnBoardNotificationDTO.PortalURL);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerOnBoardNotificationDTO.UserSessionInfo));

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.UserDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.NewCustomerCustomerUserOnboard;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerOnBoardNotificationDTO.OnBoardedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO);
    }


    public async Task SendCustUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO customerNotesNotificationDTO, long custNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken))
    {

      #region XML

      //< xsl:param name = "publisherCompanyName" />
      //< xsl:param name = "businessCompanyName" />
      //< xsl:param name = "customerCompanyName" />
      //< xsl:param name = "customerCompanyID" />
      //< xsl:param name = "dateTime" />
      //< xsl:param name = "ID" />
      //< xsl:param name = "userType" />
      //< xsl:param name = "userName" />
      //< xsl:param name = "comment" />
      //< xsl:param name = "subDomain" />
      //< xsl:param name = "portalURL" />
      //< xsl:param name = "applicationName" />
      //< xsl:param name = "copyrightText" />

      #endregion

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(customerNotesNotificationDTO.DateTimeFormat);
      customerNotesNotificationDTO.CreatedOn = DateTime.SpecifyKind(customerNotesNotificationDTO.CreatedOn, DateTimeKind.Utc);
      customerNotesNotificationDTO.CreatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(customerNotesNotificationDTO.CreatedOn, customerNotesNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(customerNotesNotificationDTO.CreatedOn, format);
      eventData.Add("dateTime", formatDate);

      eventData.Add(NotificationConstants.AppIdKey, customerNotesNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerNotesNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, customerNotesNotificationDTO.UserId.ToString());
      //eventData.Add(NotificationConstants.EntityIdKey, customerNotesNotificationDTO.EntityId.ToString());
      eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, customerNotesNotificationDTO.BusinessTenantId.ToString());

      // ToDo: Replace it with some entity type enum.          

      eventData.Add("asAppId", customerNotesNotificationDTO.AppId.ToString());
      eventData.Add("asTargetEntityType", customerNotesNotificationDTO.EntityType.ToString());
      eventData.Add("asTargetEntityId", customerNotesNotificationDTO.EntityId.ToString());
      eventData.Add("applicationName", customerNotesNotificationDTO.AppName);
      eventData.Add("publisherCompanyName", customerNotesNotificationDTO.PublisherName);
      eventData.Add("customerCompanyName", customerNotesNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", customerNotesNotificationDTO.CustomerId);
      eventData.Add("businessCompanyName", customerNotesNotificationDTO.BusinessName);
      eventData.Add("ID", customerNotesNotificationDTO.ERPDocNum);
      eventData.Add("userTypeText", UserTypeEnum.Customer.ToString());
      eventData.Add("subDomain", customerNotesNotificationDTO.SubDomainName);
      eventData.Add("userName", customerNotesNotificationDTO.UserName);
      eventData.Add("comment", customerNotesNotificationDTO.Comment);
      eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);
      eventData.Add("copyrightText", customerNotesNotificationDTO.Copyright);

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      switch (custNotificationEventEnum)
      {
        case (long)CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
          aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.InvoiceDetailsNoteUrl.ToString();
          break;
      }

      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerNotesNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = custNotificationEventEnum;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerNotesNotificationDTO.UserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    public async Task SendBizUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken))
    {

      #region XML

      //< xsl:param name = "publisherCompanyName" />
      //< xsl:param name = "businessCompanyName" />
      //< xsl:param name = "customerCompanyName" />
      //< xsl:param name = "customerCompanyID" />
      //< xsl:param name = "dateTime" />
      //< xsl:param name = "ID" />
      //< xsl:param name = "userType" />
      //< xsl:param name = "userName" />
      //< xsl:param name = "comment" />
      //< xsl:param name = "subDomain" />
      //< xsl:param name = "portalURL" />
      //< xsl:param name = "applicationName" />
      //< xsl:param name = "copyrightText" />

      #endregion

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessNotesNotificationDTO.DateTimeFormat);
      businessNotesNotificationDTO.CreatedOn = DateTime.SpecifyKind(businessNotesNotificationDTO.CreatedOn, DateTimeKind.Utc);
      businessNotesNotificationDTO.CreatedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessNotesNotificationDTO.CreatedOn, businessNotesNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(businessNotesNotificationDTO.CreatedOn, format);
      eventData.Add("dateTime", formatDate);


      eventData.Add(NotificationConstants.AppIdKey, businessNotesNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, businessNotesNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, businessNotesNotificationDTO.UserId.ToString());
      eventData.Add(NotificationConstants.EntityIdKey, businessNotesNotificationDTO.EntityId.ToString());
      //eventData.Add(NotificationConstants.EntityIdKey, customerNotesNotificationDTO.EntityId.ToString());
      eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, businessNotesNotificationDTO.BusinessTenantId.ToString());

      // ToDo: Replace it with some entity type enum.          

      eventData.Add("asAppId", businessNotesNotificationDTO.AppId.ToString());
      eventData.Add("asTargetEntityType", businessNotesNotificationDTO.EntityType.ToString());
      eventData.Add("asTargetEntityId", businessNotesNotificationDTO.EntityId.ToString());
      eventData.Add("applicationName", businessNotesNotificationDTO.AppName);
      eventData.Add("publisherCompanyName", businessNotesNotificationDTO.PublisherName);
      eventData.Add("customerCompanyName", businessNotesNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", businessNotesNotificationDTO.CustomerId);

      eventData.Add("businessCompanyName", businessNotesNotificationDTO.BusinessName);
      eventData.Add("ID", businessNotesNotificationDTO.ERPDocNum);
      eventData.Add("userTypeText", UserTypeEnum.Business.ToString());
      eventData.Add("subDomain", businessNotesNotificationDTO.SubDomainName);
      eventData.Add("userName", businessNotesNotificationDTO.UserName);
      eventData.Add("comment", businessNotesNotificationDTO.Comment);
      eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);
      eventData.Add("copyrightText", businessNotesNotificationDTO.Copyright);

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      switch (bizNotificationEventEnum)
      {
        case (long)CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
          aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.InvoiceDetailsNoteUrl.ToString();
          break;
      }

      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);



      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotesNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = bizNotificationEventEnum;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = businessNotesNotificationDTO.UserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    //23
    ///<inheritdoc/>
    public async Task GenerateCustomerForgotPasswordAsync(BusinessAccountNotificationDTO businessNotificationDTO)
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
      tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessPartnerUserForgotPassword;
      tokenInfo.UserType = businessNotificationDTO.UserType;

      _tokenInfoDataService.Add(tokenInfo);
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
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.ForgotPasswordBusinessPartner;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO);

      //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword, false, eventDataDict, businessNotificationDTO.TenantUser.ID);
    }

    public async Task GenerateAppReomoveNotification(CustomerUserNotificationGeneralDTO customerUserNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken))
    {

      #region XML
      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="applicationName"/>           
      //< xsl:param name = "customerUserName" />
      //<xsl:param name="actionTime"/>
      //<xsl:param name="copyrightText"/>
      //<xsl:param name="businessCompanyName"/>
      #endregion XML

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      // ToDo: Replace it with some entity type enum.
      eventData.Add("publisherCompanyName", customerUserNotificationGeneralDTO.PublisherName);
      eventData.Add("applicationName", customerUserNotificationGeneralDTO.ApplicationName);
      eventData.Add("customerUserName", customerUserNotificationGeneralDTO.InvitedUserFullName);
      eventData.Add("dateTime", customerUserNotificationGeneralDTO.ActionDate.ToString());
      eventData.Add("copyrightText", customerUserNotificationGeneralDTO.CopyRightText);
      eventData.Add("businessCompanyName", customerUserNotificationGeneralDTO.BusinessCompanyName);

      eventData.Add(NotificationConstants.AppIdKey, customerUserNotificationGeneralDTO.InvitedUserAppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerUserNotificationGeneralDTO.TenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, customerUserNotificationGeneralDTO.InvitedUserId.ToString());

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerUserNotificationGeneralDTO.userSession));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.CustomerUserAppAccessRemoved;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerUserNotificationGeneralDTO.InvitedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    public async Task GenerateAppPermissionChangeNotification(CustomerUserPermissionChangeNotificationGeneralDTO customerUserPermissionChangeNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken))
    {

      #region XML
      //<xsl:param name="publisherCompanyName"/>
      //<xsl:param name="applicationName"/>
      //<xsl:param name="copyrightText"/>
      //<xsl:param name="businessCompanyName"/>
      //< xsl:param name = "subDomain" />
      //< xsl:param name = "portalURL" />
      //< xsl:param name = "customerUserName" />
      //< xsl:param name = "customerUserNameChange" />
      #endregion XML

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      // ToDo: Replace it with some entity type enum.
      eventData.Add("publisherCompanyName", customerUserPermissionChangeNotificationGeneralDTO.PublisherName);
      eventData.Add("applicationName", customerUserPermissionChangeNotificationGeneralDTO.ApplicationName);
      eventData.Add("customerUserName", customerUserPermissionChangeNotificationGeneralDTO.InvitedUserFullName);
      eventData.Add("customerUserNameChange", customerUserPermissionChangeNotificationGeneralDTO.UserFullName);
      eventData.Add("subDomain", customerUserPermissionChangeNotificationGeneralDTO.SubDomain);
      eventData.Add("actionTime", DateTime.UtcNow.ToString());
      eventData.Add("copyrightText", customerUserPermissionChangeNotificationGeneralDTO.CopyRightText);
      eventData.Add("businessCompanyName", customerUserPermissionChangeNotificationGeneralDTO.BusinessCompanyName);
      eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);

      eventData.Add("asAppId", customerUserPermissionChangeNotificationGeneralDTO.InvitedUserAppId.ToString());
      eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.TenantUser).ToString());
      eventData.Add("asTargetEntityId", customerUserPermissionChangeNotificationGeneralDTO.InvitedUserId.ToString());

      eventData.Add(NotificationConstants.AppIdKey, customerUserPermissionChangeNotificationGeneralDTO.InvitedUserAppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, customerUserPermissionChangeNotificationGeneralDTO.TenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, customerUserPermissionChangeNotificationGeneralDTO.InvitedUserId.ToString());

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.UserDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(customerUserPermissionChangeNotificationGeneralDTO.userSession));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = customerUserPermissionChangeNotificationGeneralDTO.InvitedUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    #region Add Customer Ticket
    public async Task AddSupportTicketFromCustomerNotificationAsync(BusinessSupportNotificationDTO businessSupportNotificationDTO, CancellationToken cancellationToken = default(CancellationToken))
    {

      /*
       < xsl:param name = "publisherCompanyName" />
       < xsl:param name = "businessCompanyName" />
       < xsl:param name = "customerCompanyName" />
       < xsl:param name = "customerCompanyID" />
       < xsl:param name = "userName" />
       < xsl:param name = "ticketID" />
       < xsl:param name = "title" />
       < xsl:param name = "description" />
       < xsl:param name = "oldPriority" />
       < xsl:param name = "newPriority" />
       < xsl:param name = "oldStatus" />
       < xsl:param name = "newStatus" />
       < xsl:param name = "dateTime" />
       < xsl:param name = "count" />
       < xsl:param name = "commentsText" />
       < xsl:param name = "modifiedOn" />
       < xsl:param name = "updatedBy" />
       < xsl:param name = "assignedTo" />
       < xsl:param name = "subDomain" />
       < xsl:param name = "portalURL" />
       < xsl:param name = "applicationName" />
       < xsl:param name = "copyrightText" />*/

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessSupportNotificationDTO.DateTimeFormat);
      DateTime localDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(DateTime.UtcNow, businessSupportNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(localDate, format);
      eventData.Add("dateTime", formatDate);

      eventData.Add(NotificationConstants.AppIdKey, businessSupportNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, businessSupportNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, businessSupportNotificationDTO.CreatedBy.ToString());
      // ToDo: Replace it with some entity type enum.
      eventData.Add("asAppId", businessSupportNotificationDTO.AppId.ToString());
      eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.SupportTicket));
      eventData.Add("asTargetEntityId", businessSupportNotificationDTO.TicketId.ToString());


      eventData.Add("publisherCompanyName", businessSupportNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", businessSupportNotificationDTO.BusinessName);
      eventData.Add("customerName", businessSupportNotificationDTO.CustomerName);
      eventData.Add("customerCompanyID", businessSupportNotificationDTO.CustIdentityNumber);
      eventData.Add("userName", businessSupportNotificationDTO.UserName);
      eventData.Add("ticketID", businessSupportNotificationDTO.IdentityNumber.ToString());
      eventData.Add("title", businessSupportNotificationDTO.Title);
      eventData.Add("description", businessSupportNotificationDTO.Description);
      eventData.Add("contactEmail", businessSupportNotificationDTO.ContactEmail);
      eventData.Add("newPriority", businessSupportNotificationDTO.NewPriority);

      // eventData.Add("oldStatus", businessSupportNotificationDTO.OldStatus);
      eventData.Add("newStatus", businessSupportNotificationDTO.NewStatus);
      // eventData.Add("dateTime", DateTime.UtcNow.ToString());

      eventData.Add("count", businessSupportNotificationDTO.Count);
      eventData.Add("commentsText", businessSupportNotificationDTO.CommentText);
      eventData.Add("createdOn", businessSupportNotificationDTO.CreatedOn.ToString());
      eventData.Add("userID", businessSupportNotificationDTO.UserIdentityNumber);
      // eventData.Add("custIdentityNumber", businessSupportNotificationDTO.CustIdentityNumber);


      eventData.Add("createdBy", businessSupportNotificationDTO.ModifiedBy);

      eventData.Add("subDomain", businessSupportNotificationDTO.Subdomain);

      eventData.Add("portalURL", businessSupportNotificationDTO.PortalUrl);
      eventData.Add("applicationName", businessSupportNotificationDTO.AppName);
      eventData.Add("copyrightText", businessSupportNotificationDTO.Copyright);

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.CustomerTicketDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(businessSupportNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.AddCustomerTicketForBusinessUser;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = businessSupportNotificationDTO.UserSessionInfo.TenantUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    #endregion Add Customer Ticket

    #region Update Customer Ticket
    public async Task SendCustomerSupportNotificationAsync(BusinessSupportNotificationDTO businessSupportNotificationDTO, CancellationToken cancellationToken = default(CancellationToken))
    {

      /*
        <xsl:param name="publisherCompanyName"/>
        <xsl:param name="businessCompanyName"/>              
        <xsl:param name="customerCompanyName"/>
        <xsl:param name="customerCompanyID"/>
        <xsl:param name="applicationName"/>
        <xsl:param name="ticketID"/>
        <xsl:param name="title"/>
        <xsl:param name="modifiedBy"/>
        <xsl:param name="modifiedOn"/>
        <xsl:param name="newAssignedTo"/>
        <xsl:param name="oldStatus"/>
        <xsl:param name="newStatus"/>
        <xsl:param name="oldPriority"/>
        <xsl:param name="newPriority"/>
        <xsl:param name="dateTime"/>
        <xsl:param name="count"/>
        <xsl:param name="createdBy"/>
        <xsl:param name="createdOn"/>

        <xsl:param name="description"/>
        <xsl:param name="commentsText"/>
        <xsl:param name="subDomain"/>
        <xsl:param name="portalURL"/>
        <xsl:param name="copyrightText"/>*/

      Dictionary<string, string> eventData = new Dictionary<string, string>();

      // new changes for add datetime notification
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessSupportNotificationDTO.DateTimeFormat);
      DateTime localDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(DateTime.UtcNow, businessSupportNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(localDate, format);
      eventData.Add("dateTime", formatDate);


      eventData.Add(NotificationConstants.AppIdKey, businessSupportNotificationDTO.AppId.ToString());
      eventData.Add(NotificationConstants.TenantIdKey, businessSupportNotificationDTO.BusinessTenantId.ToString());
      eventData.Add(NotificationConstants.AppUserIdKey, businessSupportNotificationDTO.CreatedBy.ToString());
      // ToDo: Replace it with some entity type enum.
      eventData.Add("asAppId", businessSupportNotificationDTO.AppId.ToString());
      eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.SupportTicket));
      // eventData.Add("asTargetEntityType", "1");
      eventData.Add("asTargetEntityId", businessSupportNotificationDTO.TicketId.ToString());


      eventData.Add("publisherCompanyName", businessSupportNotificationDTO.PublisherName);
      eventData.Add("businessCompanyName", businessSupportNotificationDTO.BusinessName);
      eventData.Add("customerCompanyName", businessSupportNotificationDTO.CustomerCompanyName);
      eventData.Add("customerCompanyID", businessSupportNotificationDTO.CustIdentityNumber);
      eventData.Add("customerName", businessSupportNotificationDTO.CustomerName);
      eventData.Add("applicationName", businessSupportNotificationDTO.AppName);
      eventData.Add("ticketID", businessSupportNotificationDTO.IdentityNumber.ToString());
      eventData.Add("title", businessSupportNotificationDTO.Title);
      eventData.Add("modifiedBy", businessSupportNotificationDTO.ModifiedBy.ToString());
      eventData.Add("modifiedOn", businessSupportNotificationDTO.ModifiedOn.ToString());
      eventData.Add("newAssignedTo", businessSupportNotificationDTO.NewAssignee);
      eventData.Add("oldPriority", businessSupportNotificationDTO.OldPriority);
      eventData.Add("newPriority", businessSupportNotificationDTO.NewPriority);

      eventData.Add("oldStatus", businessSupportNotificationDTO.OldStatus);
      eventData.Add("newStatus", businessSupportNotificationDTO.NewStatus);

      eventData.Add("description", businessSupportNotificationDTO.Description);

      //eventData.Add("dateTime", DateTime.UtcNow.ToString());

      eventData.Add("count", businessSupportNotificationDTO.Count);
      eventData.Add("createdBy", businessSupportNotificationDTO.CustomerName);
      eventData.Add("createdOn", businessSupportNotificationDTO.CreatedOn.ToString());
      eventData.Add("commentsText", businessSupportNotificationDTO.CommentText);

      eventData.Add("subDomain", businessSupportNotificationDTO.Subdomain);

      eventData.Add("portalURL", businessSupportNotificationDTO.PortalUrl);

      eventData.Add("copyrightText", businessSupportNotificationDTO.Copyright);

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.CustomerTicketDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(businessSupportNotificationDTO.UserSessionInfo));

      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = businessSupportNotificationDTO.UserSessionInfo.TenantUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
    }

    ///<inheritdoc/>
    public async Task SendEmailForContactUs(ContactUsDTO ContactUsDTO)
    {

      UserSession session = _userSessionManager.GetSession();

      // Parameters needed in xslt
      #region XSlt Parameters Commented

      //<xsl:param name="osName"/>
      //<xsl:param name="browserName"/>
      //<xsl:param name="appVersion"/>
      //<xsl:param name="userName"/>
      //<xsl:param name="userEmail"/>
      //<xsl:param name="phoneNumber"/>
      //<xsl:param name="portal"/>
      //<xsl:param name="accountName"/>
      //<xsl:param name="application"/>
      //<xsl:param name="timeOfAction"/>
      //<xsl:param name="comments"/>
      //<xsl:param name="copyrightText"/>

      #endregion XSlt Parameters Commented

      // Generated notification data
      #region EventData

      Dictionary<string, string> eventData = new Dictionary<string, string>();
      // Data fro getting the receipent list.
      eventData.Add("osName", ContactUsDTO.OS.ToString());
      eventData.Add("browserName", ContactUsDTO.Browser.ToString());
      eventData.Add("appVersion", ContactUsDTO.AppVersion);

      eventData.Add("userName", ContactUsDTO.Name);
      eventData.Add("userEmail", ContactUsDTO.Email);
      eventData.Add("phoneNumber", ContactUsDTO.Phone.ToString());
      eventData.Add("portal", ContactUsDTO.PortalName);
      eventData.Add("timeOfAction", DateTime.Now.ToString());
      eventData.Add("copyrightText", ContactUsDTO.CopyRightText);
      eventData.Add("comments", ContactUsDTO.Message);
      eventData.Add("accountName", ContactUsDTO.CompanyName);
      eventData.Add("application", ContactUsDTO.ApplicationName);

      eventData.Add(NotificationConstants.TenantIdKey, session.TenantId.ToString());

      #endregion EventData

      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
      eventDataDict.Add("EventData", eventData);
      eventDataDict.Add("UserSession", MapNotitificationUserSession(session));

      // Send notification
      GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
      generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Customer;
      generateNotificationDTO.EventId = (long)CustNotificationEventEnum.ContactUsNotification;
      generateNotificationDTO.EventInfo = eventDataDict;
      generateNotificationDTO.LoggedinUserId = session.TenantUserId;
      generateNotificationDTO.UseCacheForTemplate = false;
      await _custNotificationService.GenerateNotificationAsync(generateNotificationDTO);

    }
    #endregion



    #region Private Methods

    private NotificationUserSessionDTO MapNotitificationUserSession(UserSession userSession)
    {
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

    #endregion
  } }

