using System;
using System.Collections.Generic;
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
    public class BizNotificationHandler:IBizNotificationHandler {
        IBizNotificationService _bizNotificationService;
        AppPortalAppSettings _appPortalAppSettings;
        ITokenInfoDS _tokenInfoDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;

        public BizNotificationHandler(IBizNotificationService bizPaymentNotificationService, ITokenInfoDS tokenInfoDS, IUnitOfWork unitOfWork, IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager) {
            _bizNotificationService = bizPaymentNotificationService;
            _appPortalAppSettings = appPortalAppSettings.Value;
            _tokenInfoDS = tokenInfoDS;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
        }

        #region Payment

        /// <summary>
        /// business payment user on board notification
        /// </summary>
        /// <param name="businessPaymentUserOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SendBizPaymentUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessPaymentUserOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessPaymentUserOnBoardNotificationDTO.DateTimeFormat);

            businessPaymentUserOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(businessPaymentUserOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
            businessPaymentUserOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessPaymentUserOnBoardNotificationDTO.JoinedDate.Value, businessPaymentUserOnBoardNotificationDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessPaymentUserOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
            eventData.Add("onboardTime", formatDate);

            eventData.Add("BizSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, businessPaymentUserOnBoardNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessPaymentUserOnBoardNotificationDTO.BusinessTenantId.ToString());
            // ToDo: Replace it with some entity type enum.
            eventData.Add("asAppId", businessPaymentUserOnBoardNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.TenantUser).ToString());
            eventData.Add("asTargetEntityId", businessPaymentUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("onboardedUserName", businessPaymentUserOnBoardNotificationDTO.UserName);
            eventData.Add("onboardedUserId", businessPaymentUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("appName", businessPaymentUserOnBoardNotificationDTO.AppName);
            //eventData.Add("onboardTime", businessPaymentUserOnBoardNotificationDTO.JoinedDate.ToString());
            eventData.Add("publisherName", businessPaymentUserOnBoardNotificationDTO.PublisherName);
            eventData.Add("businessName", businessPaymentUserOnBoardNotificationDTO.BusinessName);
            eventData.Add("copyrightText", businessPaymentUserOnBoardNotificationDTO.Copyright);
            eventData.Add(NotificationConstants.SubDomainKey, businessPaymentUserOnBoardNotificationDTO.SubDomain);

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);
            
            // eventData.Add("portalURL", businessOnBoardNotificationDTO.PortalURL);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessPaymentUserOnBoardNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessPaymentUserOnBoardNotificationDTO.OnBoardedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        /// <summary>
        /// Business customer user on board notification 
        /// </summary>
        /// <param name="businessCustUserOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SendBizCustUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessCustUserOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessCustUserOnBoardNotificationDTO.DateTimeFormat);
            businessCustUserOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(businessCustUserOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
            businessCustUserOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessCustUserOnBoardNotificationDTO.JoinedDate.Value, businessCustUserOnBoardNotificationDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessCustUserOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
            eventData.Add("onboardTime", formatDate);

            eventData.Add("BizSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, businessCustUserOnBoardNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessCustUserOnBoardNotificationDTO.BusinessTenantId.ToString());
            // ToDo: Replace it with some entity type enum.
            eventData.Add("asAppId", businessCustUserOnBoardNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.TenantUser).ToString());
            eventData.Add("asTargetEntityId", businessCustUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("publisherName", businessCustUserOnBoardNotificationDTO.PublisherName);
            eventData.Add("businessName", businessCustUserOnBoardNotificationDTO.BusinessName);
            eventData.Add("appName", businessCustUserOnBoardNotificationDTO.AppName);
            eventData.Add("customerAppName", businessCustUserOnBoardNotificationDTO.CustomerName);
            eventData.Add("onboardedUserName", businessCustUserOnBoardNotificationDTO.UserName);
            eventData.Add("onboardedUserId", businessCustUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            // eventData.Add("onboardTime", businessCustUserOnBoardNotificationDTO.JoinedDate.ToString());
            eventData.Add("copyrightText", businessCustUserOnBoardNotificationDTO.Copyright);
            eventData.Add(NotificationConstants.SubDomainKey, businessCustUserOnBoardNotificationDTO.SubDomain);
            // eventData.Add("portalURL", businessOnBoardNotificationDTO.PortalURL);

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessCustUserOnBoardNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessCustUserOnBoardNotificationDTO.OnBoardedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }


        #region Business SetUp
        /// <summary>
        /// Business setup user on board notification
        /// </summary>
        /// <param name="businessSetupUserOnBoardNotificationDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SendBizSetupUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessSetupUserOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "customerCompanyName" />
            //< xsl:param name = "dateTime" />
            //< xsl:param name = "newUserName" />
            //< xsl:param name = "subDomain" />
            //< xsl:param name = "portalURL" />
            //< xsl:param name = "applicationName" />
            //< xsl:param name = "copyrightText" />


            Dictionary<string, string> eventData = new Dictionary<string, string>();


            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessSetupUserOnBoardNotificationDTO.DateTimeFormat);
            businessSetupUserOnBoardNotificationDTO.JoinedDate = DateTime.SpecifyKind(businessSetupUserOnBoardNotificationDTO.JoinedDate.Value, DateTimeKind.Utc);
            businessSetupUserOnBoardNotificationDTO.JoinedDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessSetupUserOnBoardNotificationDTO.JoinedDate.Value, businessSetupUserOnBoardNotificationDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessSetupUserOnBoardNotificationDTO.JoinedDate.Value, dtPickList.JSDateTimeFormat);
            eventData.Add("onboardTime", formatDate);
            eventData.Add("dateTime", formatDate);

            eventData.Add("BizSetupAppId", Core.BaseService.Constants.BusinessApplicationId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, businessSetupUserOnBoardNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessSetupUserOnBoardNotificationDTO.BusinessTenantId.ToString());
            // ToDo: Replace it with some entity type enum.   
            eventData.Add("asAppId", businessSetupUserOnBoardNotificationDTO.AppId.ToString());
            // eventData.Add("asTargetEntityType", "1");
            eventData.Add("asTargetEntityType", Convert.ToString((int)EntityTypeEnum.TenantUser));
            eventData.Add("asTargetEntityId", businessSetupUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("onboardedUserId", businessSetupUserOnBoardNotificationDTO.OnBoardedUserId.ToString());
            eventData.Add("onboardedUserName", businessSetupUserOnBoardNotificationDTO.UserName);
            eventData.Add("appCount", "2");
            //eventData.Add("onboardTime", businessSetupUserOnBoardNotificationDTO.JoinedDate.ToString());
            eventData.Add("publisherName", businessSetupUserOnBoardNotificationDTO.PublisherName);
            eventData.Add("publisherCompanyName", businessSetupUserOnBoardNotificationDTO.PublisherName);
            eventData.Add("businessName", businessSetupUserOnBoardNotificationDTO.BusinessName);
            eventData.Add("businessCompanyName", businessSetupUserOnBoardNotificationDTO.BusinessName);
            eventData.Add("customerCompanyName", businessSetupUserOnBoardNotificationDTO.CustomerName);
            //eventData.Add("dateTime", businessSetupUserOnBoardNotificationDTO.JoinedDate.ToString());
            eventData.Add("newUserName", businessSetupUserOnBoardNotificationDTO.UserName);
            eventData.Add(NotificationConstants.SubDomainKey, businessSetupUserOnBoardNotificationDTO.SubDomain);
            eventData.Add("appName", businessSetupUserOnBoardNotificationDTO.AppName);
            eventData.Add("copyrightText", businessSetupUserOnBoardNotificationDTO.Copyright);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessSetupUserOnBoardNotificationDTO.UserSessionInfo));

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.NewBusinessSetupUserOnboard;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessSetupUserOnBoardNotificationDTO.OnBoardedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        #endregion Business SetUp

        public async Task GenerateAppReomoveNotification(BusinessUserNotificationGeneralDTO businessUserNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            #region XML
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="applicationName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionTime"/>
            //<xsl:param name="copyrightText"/>
            //<xsl:param name="businessCompanyName"/>
            #endregion XML

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessUserNotificationGeneralDTO.DateTimeFormat);
            businessUserNotificationGeneralDTO.ActionDate = DateTime.SpecifyKind(businessUserNotificationGeneralDTO.ActionDate, DateTimeKind.Utc);
            businessUserNotificationGeneralDTO.ActionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessUserNotificationGeneralDTO.ActionDate, businessUserNotificationGeneralDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessUserNotificationGeneralDTO.ActionDate, dtPickList.JSDateTimeFormat);

            // ToDo: Replace it with some entity type enum.
            eventData.Add("publisherCompanyName", businessUserNotificationGeneralDTO.PublisherName);
            eventData.Add("applicationName", businessUserNotificationGeneralDTO.ApplicationName);
            eventData.Add("businessUserName", businessUserNotificationGeneralDTO.InvitedUserFullName);
            eventData.Add("actionTime", formatDate);
            eventData.Add("copyrightText", businessUserNotificationGeneralDTO.CopyRightText);
            eventData.Add("businessCompanyName", businessUserNotificationGeneralDTO.BusinessCompanyName);

            eventData.Add(NotificationConstants.AppIdKey, businessUserNotificationGeneralDTO.InvitedUserAppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessUserNotificationGeneralDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppUserIdKey, businessUserNotificationGeneralDTO.InvitedUserId.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessUserNotificationGeneralDTO.userSession));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.BusinessUserAppAccessRemoved;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessUserNotificationGeneralDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        public async Task GenerateAppAddAndReomoveNotification(BusinessUserNotificationAppAccessUpdateDTO businessUserNotificationGeneralDTO,List<AppShortInfoDTO> appShortInfoDTOs, CancellationToken cancellationToken = default(CancellationToken)) {

            #region XML
              //<xsl:param name="publisherCompanyName"/>
              //<xsl:param name="userName"/>
              //<xsl:param name="businessCompanyName"/>
              //<xsl:param name="updatedBy"/>
              //<xsl:param name="updatedOn"/>
              //<xsl:param name="subDomain"/>
              //<xsl:param name="portalUrl"/>
              //<xsl:param name="copyrightText"/>
            #endregion XML

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessUserNotificationGeneralDTO.DateTimeFormat);
            businessUserNotificationGeneralDTO.ActionDate = DateTime.SpecifyKind(businessUserNotificationGeneralDTO.ActionDate, DateTimeKind.Utc);
            businessUserNotificationGeneralDTO.ActionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessUserNotificationGeneralDTO.ActionDate, businessUserNotificationGeneralDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessUserNotificationGeneralDTO.ActionDate, dtPickList.JSDateTimeFormat);

            // ToDo: Replace it with some entity type enum.
            eventData.Add("publisherCompanyName", businessUserNotificationGeneralDTO.PublisherName);
            eventData.Add("updatedBy", businessUserNotificationGeneralDTO.UpdatedBy);
            eventData.Add("userName", businessUserNotificationGeneralDTO.InvitedUserFullName);
            eventData.Add("updatedOn", formatDate);
            eventData.Add("copyrightText", businessUserNotificationGeneralDTO.CopyRightText);
            eventData.Add("businessCompanyName", businessUserNotificationGeneralDTO.BusinessCompanyName);
            eventData.Add("subDomain", businessUserNotificationGeneralDTO.SubDomain);
            eventData.Add("portalUrl", string.Format(_appPortalAppSettings.BusinessPortalClientURL, businessUserNotificationGeneralDTO.userSession.Subdomain));

            eventData.Add(NotificationConstants.AppIdKey, businessUserNotificationGeneralDTO.InvitedUserAppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessUserNotificationGeneralDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppUserIdKey, businessUserNotificationGeneralDTO.InvitedUserId.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            // Generate xml data for invoice list
            string xml = XMLSerialzer.Serialize(appShortInfoDTOs);

            eventDataDict.Add(NotificationConstants.EventDataXmlKey, xml);
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessUserNotificationGeneralDTO.userSession));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.BusinessUserAppAccessAddAndRemoved;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessUserNotificationGeneralDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        public async Task GenerateAppPermissionChangeNotification(BusinessUserPermissionNotificationGeneralDTO businessUserPermissionNotificationGeneralDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            #region XML
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="applicationName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionTime"/>
            //<xsl:param name="copyrightText"/>
            //<xsl:param name="businessCompanyName"/>
            //< xsl:param name = "subDomain" />
            //< xsl:param name = "portalURL" />
            //< xsl:param name = "userName" />
            //< xsl:param name = "userNameChange" />

            #endregion XML

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessUserPermissionNotificationGeneralDTO.DateTimeFormat);
            businessUserPermissionNotificationGeneralDTO.ActionDate = DateTime.SpecifyKind(businessUserPermissionNotificationGeneralDTO.ActionDate, DateTimeKind.Utc);
            businessUserPermissionNotificationGeneralDTO.ActionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessUserPermissionNotificationGeneralDTO.ActionDate, businessUserPermissionNotificationGeneralDTO.TimeZone, false);
            string formatDate = DateTimeHelper.FormatDate(businessUserPermissionNotificationGeneralDTO.ActionDate, dtPickList.JSDateTimeFormat);

            // ToDo: Replace it with some entity type enum.
            eventData.Add("publisherCompanyName", businessUserPermissionNotificationGeneralDTO.PublisherName);
            eventData.Add("applicationName", businessUserPermissionNotificationGeneralDTO.ApplicationName);
            eventData.Add("userName", businessUserPermissionNotificationGeneralDTO.InvitedUserFullName);
            eventData.Add("userNameChange", businessUserPermissionNotificationGeneralDTO.UserFullName);
            eventData.Add("subDomain", businessUserPermissionNotificationGeneralDTO.SubDomain);
            eventData.Add("actionTime", formatDate);
            eventData.Add("copyrightText", businessUserPermissionNotificationGeneralDTO.CopyRightText);
            eventData.Add("businessCompanyName", businessUserPermissionNotificationGeneralDTO.BusinessCompanyName);
            eventData.Add("portalURL", string.Format(_appPortalAppSettings.BusinessPortalClientURL, businessUserPermissionNotificationGeneralDTO.userSession.Subdomain));
            eventData.Add("asAppId", businessUserPermissionNotificationGeneralDTO.InvitedUserAppId.ToString());
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.TenantUser).ToString());
            eventData.Add("asTargetEntityId", businessUserPermissionNotificationGeneralDTO.InvitedUserId.ToString());

            eventData.Add(NotificationConstants.AppIdKey, businessUserPermissionNotificationGeneralDTO.InvitedUserAppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessUserPermissionNotificationGeneralDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppUserIdKey, businessUserPermissionNotificationGeneralDTO.InvitedUserId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.UserDetailsUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessUserPermissionNotificationGeneralDTO.userSession));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessUserPermissionNotificationGeneralDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        public async Task SendBizPaymentUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken)) {

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

            // ToDo: Replace it with some entity type enum.          

            eventData.Add("asAppId", businessNotesNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", businessNotesNotificationDTO.EntityType.ToString());
            eventData.Add("asTargetEntityId", businessNotesNotificationDTO.EntityId.ToString());
            eventData.Add("applicationName", businessNotesNotificationDTO.AppName);
            eventData.Add("publisherCompanyName", businessNotesNotificationDTO.PublisherName);
            eventData.Add("customerCompanyName", businessNotesNotificationDTO.CustomerName);
            eventData.Add("customerCompanyID", businessNotesNotificationDTO.CustomerId);
            //eventData.Add("dateTime", Convert.ToString(DateTime.UtcNow));
            eventData.Add("businessCompanyName", businessNotesNotificationDTO.BusinessName);
            eventData.Add("ID", businessNotesNotificationDTO.ERPDocNum);
            eventData.Add("userTypeText", UserTypeEnum.Business.ToString());
            eventData.Add("subDomain", businessNotesNotificationDTO.SubDomainName);
            eventData.Add("userName", businessNotesNotificationDTO.UserName);
            eventData.Add("comment", businessNotesNotificationDTO.Comment);
            eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);
            eventData.Add("copyrightText", businessNotesNotificationDTO.Copyright);

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            switch(bizNotificationEventEnum) {
                case (long)BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsNoteUrl.ToString();
                    break;
        }
            
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotesNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = bizNotificationEventEnum;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotesNotificationDTO.UserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        public async Task SendCustUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken)) {

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

            // ToDo: Replace it with some entity type enum.          

            eventData.Add("asAppId", businessNotesNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", businessNotesNotificationDTO.EntityType.ToString());
            eventData.Add("asTargetEntityId", businessNotesNotificationDTO.EntityId.ToString());
            eventData.Add("applicationName", businessNotesNotificationDTO.AppName);
            eventData.Add("publisherCompanyName", businessNotesNotificationDTO.PublisherName);
            eventData.Add("customerCompanyName", businessNotesNotificationDTO.CustomerName);
            eventData.Add("customerCompanyID", businessNotesNotificationDTO.CustomerId);
            //eventData.Add("dateTime", Convert.ToString(DateTime.UtcNow));
            eventData.Add("businessCompanyName", businessNotesNotificationDTO.BusinessName);
            eventData.Add("ID", businessNotesNotificationDTO.ERPDocNum);
            eventData.Add("userTypeText", UserTypeEnum.Business.ToString());
            eventData.Add("subDomain", businessNotesNotificationDTO.SubDomainName);
            eventData.Add("userName", businessNotesNotificationDTO.UserName);
            eventData.Add("comment", businessNotesNotificationDTO.Comment);
            eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);
            eventData.Add("copyrightText", businessNotesNotificationDTO.Copyright);

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            switch(bizNotificationEventEnum) {
                case (long)BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.InvoiceDetailsNoteUrl.ToString();
                    break;
            }

            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotesNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = bizNotificationEventEnum;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotesNotificationDTO.UserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }



        #endregion Payment

        #region CustomerApp
        public async Task SendBizCustomertUserOnNotesAddedNotificationAsync(BusinessNotesNotificationDTO businessNotesNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken)) {

            #region XML

            //< xsl:param name = "publisherCompanyName" />
            //< xsl:param name = "businessCompanyName" />
            //< xsl:param name = "customerCompanyName" />
            //< xsl:param name = "customerCompanyID" />
            //< xsl:param name = "dateTime" />
            //< xsl:param name = "invoiceID" />
            //< xsl:param name = "userType" />
            //< xsl:param name = "userName" />
            //< xsl:param name = "comment" />
            //< xsl:param name = "subDomain" />
            //< xsl:param name = "portalURL" />
            //< xsl:param name = "applicationName" />
            //< xsl:param name = "copyrightText" />

            #endregion

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add(NotificationConstants.AppIdKey, businessNotesNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.TenantIdKey, businessNotesNotificationDTO.BusinessTenantId.ToString());
            eventData.Add(NotificationConstants.AppUserIdKey, businessNotesNotificationDTO.UserId.ToString());
            eventData.Add(NotificationConstants.EntityIdKey, businessNotesNotificationDTO.EntityId.ToString());
            // ToDo: Replace it with some entity type enum.          

            eventData.Add("asAppId", businessNotesNotificationDTO.AppId.ToString());
            eventData.Add("asTargetEntityType", businessNotesNotificationDTO.EntityType.ToString());
            eventData.Add("asTargetEntityId", businessNotesNotificationDTO.EntityId.ToString());
            eventData.Add("applicationName", businessNotesNotificationDTO.AppName);
            eventData.Add("publisherCompanyName", businessNotesNotificationDTO.PublisherName);
            eventData.Add("customerCompanyName", businessNotesNotificationDTO.CustomerName);
            eventData.Add("customerCompanyID", businessNotesNotificationDTO.CustomerId);
            eventData.Add("dateTime", Convert.ToString(DateTime.UtcNow));
            eventData.Add("businessCompanyName", businessNotesNotificationDTO.BusinessName);
            eventData.Add("ID", businessNotesNotificationDTO.ERPDocNum);
            eventData.Add("userTypeText", UserTypeEnum.Business.ToString());
            eventData.Add("subDomain", businessNotesNotificationDTO.SubDomainName);
            eventData.Add("userName", businessNotesNotificationDTO.UserName);
            eventData.Add("comment", businessNotesNotificationDTO.Comment);
            eventData.Add("portalURL", _appPortalAppSettings.BusinessPortalClientURL);
            eventData.Add("copyrightText", businessNotesNotificationDTO.Copyright);            

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessNotesNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = bizNotificationEventEnum;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotesNotificationDTO.UserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }
        #endregion

        #region Business Support

        public async Task SendBusinessSupportNotificationAsync(BusinessSupportNotificationDTO businessSupportNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {

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
            eventData.Add("customerCompanyID", businessSupportNotificationDTO.CustomerName);
            eventData.Add("userName", businessSupportNotificationDTO.UserName);
            eventData.Add("ticketID", businessSupportNotificationDTO.IdentityNumber.ToString());
            eventData.Add("title", businessSupportNotificationDTO.Title);

            eventData.Add("description", businessSupportNotificationDTO.Description);
            eventData.Add("oldPriority", businessSupportNotificationDTO.OldPriority);
            eventData.Add("newPriority", businessSupportNotificationDTO.NewPriority);

            eventData.Add("oldStatus", businessSupportNotificationDTO.OldStatus);
            eventData.Add("newStatus", businessSupportNotificationDTO.NewStatus);
           // eventData.Add("dateTime", DateTime.UtcNow.ToString());

            eventData.Add("count", businessSupportNotificationDTO.Count);
            eventData.Add("commentsText", businessSupportNotificationDTO.CommentText);


      // new changes for add datetime notification
      DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(businessSupportNotificationDTO.DateTimeFormat);    
      DateTime localDate =  DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(DateTime.UtcNow, businessSupportNotificationDTO.TimeZone, false);
      string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
      string formatDate = DateTimeHelper.FormatDate(localDate, format);
      eventData.Add("dateTime", formatDate);

     
      businessSupportNotificationDTO.ModifiedOn = DateTime.SpecifyKind(businessSupportNotificationDTO.ModifiedOn.Value, DateTimeKind.Utc);
      businessSupportNotificationDTO.ModifiedOn = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(businessSupportNotificationDTO.ModifiedOn.Value, businessSupportNotificationDTO.TimeZone, false);      
      string modifiedDate = DateTimeHelper.FormatDate(businessSupportNotificationDTO.ModifiedOn.Value, format);      
      eventData.Add("modifiedOn", modifiedDate);
     

      eventData.Add("updatedBy", businessSupportNotificationDTO.ModifiedBy);
            eventData.Add("assignedTo", businessSupportNotificationDTO.NewAssignee);
      eventData.Add("oldassignedTo", businessSupportNotificationDTO.OldAssignee);
      eventData.Add("subDomain", businessSupportNotificationDTO.Subdomain);

            eventData.Add("portalURL", businessSupportNotificationDTO.PortalUrl);
            eventData.Add("applicationName", businessSupportNotificationDTO.AppName);
            eventData.Add("copyrightText", businessSupportNotificationDTO.Copyright);

      ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
      aSNotificationAdditionalInfo.NavigationKey = CustomerNavigationKeyEnum.MyTicketDetailsUrl.ToString();
      string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
      eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);


      // Creates list of dictionary for event data.
      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(businessSupportNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessSupportNotificationDTO.UserSessionInfo.TenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        }

        #endregion Business Support

        #region Private Methods

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

        #endregion

        #region Shifted from Old code

        ///<inheritdoc/>
        public async Task GenerateBusinessNewUserInviteNotification(BusinessAccountNotificationDTO businessNotificationDTO) {

            #region xslt arguments

            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="invitedUserName"/>
            //<xsl:param name="businessInvitingUserName"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/>
            //<xsl:param name="copyrightText"/>

            #endregion xslt arguments

            #region TokeInfo

            TokenDataDTO tokenDataDTO = new TokenDataDTO();
            tokenDataDTO.Code = businessNotificationDTO.PasswordCode;
            tokenDataDTO.Email = businessNotificationDTO.InvitedUserEmail;
            tokenDataDTO.IdentityUserId = businessNotificationDTO.InvitedUserIdentityUserId;
            tokenDataDTO.TenantId = businessNotificationDTO.TenantId;
            tokenDataDTO.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenDataDTO.UserType = (int)UserTypeEnum.Business;

            TokenInfo tokenInfo = new TokenInfo();
            tokenInfo.ID = Guid.NewGuid();
            tokenInfo.AppKey = businessNotificationDTO.InvitedUserAppKey;
            tokenInfo.TenantUserId = businessNotificationDTO.InvitedUserId;
            tokenInfo.CreatedDate = DateTime.UtcNow;
            tokenInfo.TenantId = businessNotificationDTO.TenantId;
            tokenInfo.TokenData = Newtonsoft.Json.JsonConvert.SerializeObject(tokenDataDTO);
            tokenInfo.TokenType = (int)BusinessTokenTypeEnum.BusinessUserWithNewEmailIdInvite;
            tokenInfo.UserType = (int)UserTypeEnum.Business;

            _tokenInfoDS.Add(tokenInfo);
            _unitOfWork.SaveAll();

            #endregion TokeInfo

            // Create notification information
            Dictionary<string, string> eventData = new Dictionary<string, string>();

            eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
            eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
            eventData.Add("invitedUserName", businessNotificationDTO.InvitedUserFullName);
            eventData.Add("businessInvitingUserName", businessNotificationDTO.InvitorUserName);
            eventData.Add("subDomain", businessNotificationDTO.SubDomain);
            eventData.Add("portalURL", businessNotificationDTO.PortalURL);
            eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);

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
            deeplinkInfo.Add("partnerid", " 123 ");
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
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO);

            //await _businessNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite, false, eventDataDict, businessNotificationDTO.UserSession.TenantUserId);
        }

    #endregion

        ///<inheritdoc/>
        public async Task GenerateBusinessUserAccountStatusChangedNotification(BusinessAccountNotificationDTO businessNotificationDTO, long bizNotificationEventEnum, CancellationToken cancellationToken = default(CancellationToken)) {
          //#region xslt arguments
          ////<xsl:param name="publisherCompanyName"/>
          ////<xsl:param name="businessUserName"/>
          ////<xsl:param name="businessUserNameChange"/>
          ////<xsl:param name="status"/>
          ////<xsl:param name="copyrightText"/>
          //#endregion xslt arguments

          // Create notification information
          Dictionary<string, string> eventData = new Dictionary<string, string>();

          eventData.Add("publisherCompanyName", businessNotificationDTO.PublisherName);
          eventData.Add("businessCompanyName", businessNotificationDTO.BusinessCompanyName);
          eventData.Add("businessUserName", businessNotificationDTO.InvitedUserFullName);
          eventData.Add("businessUserNameChange", businessNotificationDTO.InvitorUserName);
          eventData.Add("newStatus", businessNotificationDTO.NewUserStatus);
          eventData.Add("oldStatus", businessNotificationDTO.OldUserStatus);
          eventData.Add("copyrightText", businessNotificationDTO.CopyRightText);
          eventData.Add("subDomain", businessNotificationDTO.SubDomain);
          eventData.Add("appuserid", businessNotificationDTO.InvitedUserId.ToString());
          eventData.Add("appid", businessNotificationDTO.InvitedUserAppId.ToString());
          eventData.Add("tenantid", businessNotificationDTO.UserSession.TenantId.ToString());

          // Creates list of dictionary for event data.
          Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
          eventDataDict.Add("EventData", eventData);
          eventDataDict.Add("UserSession", businessNotificationDTO.UserSession);

          GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
          generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
          generateNotificationDTO.EventId = bizNotificationEventEnum;
          generateNotificationDTO.EventInfo = eventDataDict;
          generateNotificationDTO.LoggedinUserId = businessNotificationDTO.InvitedUserId;
          generateNotificationDTO.UseCacheForTemplate = false;

          await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
          // (  int)ModuleTypeEnum.Business, (long)BizNotificationEventEnum.BusinessUserAccountStatusChanged, false, eventDataDict, businessNotificationDTO.InvitedUserId);
    }

        ///<inheritdoc/>
        public async Task SendEmailForContactUs(ContactUsDTO ContactUsDTO) {

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
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)BizNotificationEventEnum.ContactUsNotification;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = session.TenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            await _bizNotificationService.GenerateNotificationAsync(generateNotificationDTO);

        }

    }
}
