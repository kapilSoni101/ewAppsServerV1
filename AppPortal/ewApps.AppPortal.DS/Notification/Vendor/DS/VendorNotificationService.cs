using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class VendorNotificationService:NotificationService<NotificationRecipient>, IVendorNotificationService {

        #region Member Variables

        IASNotificationDS _asNotificationDS;
        IVendorNotificationRecipientDS _vendorNotificationRecipientDS;
        AppPortalAppSettings _appPortalAppSettings;
        IOptions<NotificationAppSettings> _notificationAppSettingOptions;
        IBusinessNotificationReceipentDataService _businessNotificationReceipentDataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VendorNotificationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="asNotificationService">As notification service.</param>
        /// <param name="asNotificationDS">As notification DataService.</param>
        public VendorNotificationService(ILogger<VendorNotificationService> logger, IEmailService emailService, ISMSService smsService,
              IASNotificationService asNotificationService, IASNotificationDS asNotificationDS, IVendorNotificationRecipientDS vendorNotificationRecipientDS,
              IOptions<AppPortalAppSettings> appPortalAppSettingOptions, IOptions<NotificationAppSettings> notificationAppSettingOptions,
              IBusinessNotificationReceipentDataService businessNotificationReceipentDataService)
                : base(logger, emailService, smsService, asNotificationService, notificationAppSettingOptions) {
            _asNotificationDS = asNotificationDS;
            _vendorNotificationRecipientDS = vendorNotificationRecipientDS;
            _appPortalAppSettings = appPortalAppSettingOptions.Value;
            _notificationAppSettingOptions = notificationAppSettingOptions;
            _businessNotificationReceipentDataService = businessNotificationReceipentDataService;
        }

        #endregion

        #region Override Methods

        /// <inheritdoc/>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                BizNotificationEventEnum parsedEvent;

                if(moduleId <= 0) {
                    valid = false;
                }

                if(Enum.TryParse<BizNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
                    valid = false;
                }

                if(eventInfo.Count <= 0) {
                    valid = false;
                }

                if(valid == false) {
                    throw new InvalidOperationException("Invalid notification arguments.");
                }
                else {
                    return valid;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.ValidateNotificationData:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <inheritdoc/>
        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {
            List<NotificationRecipient> recipients = new List<NotificationRecipient>();
            Dictionary<string, string> eventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;
            Guid appId = Guid.Empty;
            Guid tenantUserId = Guid.Empty;
            Guid businessTenantId = Guid.Empty;
            Guid businessPartnerTenantId = Guid.Empty;
            if(eventInfo.ContainsKey(NotificationConstants.AppIdKey)) {
                appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.TenantIdKey)) {
                businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.BusinessPartnerTenantIdKey)) {
                businessPartnerTenantId = Guid.Parse(eventInfo[NotificationConstants.BusinessPartnerTenantIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.AppUserIdKey)) {
                tenantUserId = Guid.Parse(eventInfo[NotificationConstants.AppUserIdKey]);
            }

            appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);
            businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);
            businessPartnerTenantId = Guid.Empty;

            if(eventInfo.ContainsKey(NotificationConstants.BusinessPartnerTenantIdKey)) {
                businessPartnerTenantId = Guid.Parse(eventInfo[NotificationConstants.BusinessPartnerTenantIdKey]);
            }

            tenantUserId = Guid.Empty;

            if(eventInfo.ContainsKey("appuserid")) {
                tenantUserId = Guid.Parse(eventInfo["appuserid"]);
            }


            switch((VendorNotificationEventEnum)payload.EventId) {

                case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                    recipients = _businessNotificationReceipentDataService.GetInvitedBusinessUser(businessTenantId, tenantUserId, appId);
                    break;

                case VendorNotificationEventEnum.VendorUserInviteWithNewEmail:
                    recipients = _businessNotificationReceipentDataService.GetInvitedBusinessUser(businessTenantId, tenantUserId, appId);
                    break;

                case VendorNotificationEventEnum.VendorUserForgotPassword:
                  return _businessNotificationReceipentDataService.GetForgotPasswordBusinessUser(businessTenantId, tenantUserId);

                case VendorNotificationEventEnum.VendorUserSetupAppOnboard:
                    recipients = _vendorNotificationRecipientDS.GetVendorUserAppOnboardRecipients(appId, businessTenantId, businessPartnerTenantId, tenantUserId);
                    break;
                default:
                    break;
            }
            return recipients;
        }

        #endregion

        #region Deeplink Methods

        /// <inheritdoc/>
        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {
            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();
            try {
                if(notificationPayload != null) {
                    // Map properties from notification payload to deeplink payload.
                    deeplinkPayload.ModuleId = notificationPayload.ModuleId;
                    deeplinkPayload.EventId = notificationPayload.EventId;
                    deeplinkPayload.LoggedinUserId = notificationPayload.LoggedinUserId;
                    deeplinkPayload.NotificationId = notificationPayload.NotificationId;
                    deeplinkPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;

                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                        //Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                        // As each action is bound to single event so decision parameter should be event id.
                        //                        string branchKey = Convert.ToString(GetBranchKey(notificationPayload.EventId));

                        // Get deeplink actions for particular notification.
                        // If any email contains more than one deeplink, returned list should contains each deeplink information.
                        switch((VendorNotificationEventEnum)notificationPayload.EventId) {

                            case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                                deeplinkPayload.DeeplinkInfo = GetVendorNewUserInviteDeeplinkPayload(notificationPayload, (long)VendorNotificationEventEnum.VendorInviteWithNewEmail);
                                break;

                            case VendorNotificationEventEnum.VendorUserInviteWithNewEmail:
                                deeplinkPayload.DeeplinkInfo = GetVendorNewUserInviteDeeplinkPayload(notificationPayload, (long)VendorNotificationEventEnum.VendorUserInviteWithNewEmail);
                                break;

                            case VendorNotificationEventEnum.VendorUserForgotPassword:
                                //deeplinkPayload.DeeplinkInfo = GetCustUserForgotPasswordDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword);
                                deeplinkPayload.DeeplinkInfo = GetVendorUserForgotPasswordDeeplinkPayload(notificationPayload, (long)VendorNotificationEventEnum.VendorUserForgotPassword);
                                break;

                            default:
                                return null;
                        }
                    }
                }

                return deeplinkPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary>Get branch key on the basis of event type</summary>
        /// <param name="eventData">Event identitfier</param>
        private object GetBranchKeyTemp(object eventData) {
            List<string> decisionData = eventData as List<string>;
            string appKey = string.Empty;
            long eventLong = long.Parse(decisionData[0]);
            if(decisionData.Count > 1) {
                appKey = decisionData[1];
            }
            switch(eventLong) {
                  case (long)VendorNotificationEventEnum.VendorUserForgotPassword:
                  case (long)VendorNotificationEventEnum.VendorInviteWithNewEmail:
                    return _notificationAppSettingOptions.Value.VendorBranchApiKey;

                 default:
                    return "";
            }
        }

        /// <inheritdoc/>
        protected override object GetBranchKey(object decisionParameters) {
            Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = new Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>();

            try {
                // Get decision paramerts from object.
                // Dictionary<string, object> decisionDictionary = decisionParameters as Dictionary<string, object>;
                //long eventId = long.Parse(decisionDictionary["eventid"].ToString());
                //string appKey = decisionDictionary["appkey"].ToString();

                switch((VendorNotificationEventEnum)decisionParameters) {

                    case VendorNotificationEventEnum.VendorUserForgotPassword:
                      DeeplinkActionParameterAndBranchKeyDTO modelDL = new DeeplinkActionParameterAndBranchKeyDTO();
                      modelDL.DeepLinkAction = "businessuser,forgotpassword";
                      List<string> parameterListDL = new List<string>();
                      parameterListDL.Add("tenantid");
                      parameterListDL.Add("appuserid");
                      parameterListDL.Add("identityserveruserid");
                      parameterListDL.Add("loginEmail");
                      parameterListDL.Add("usertype");
                      parameterListDL.Add("code");
                      parameterListDL.Add("tokeninfoid");
                      parameterListDL.Add("subDomain");
                      modelDL.DeeplinkParametersList = parameterListDL;
                      //if (appKey == AppKeyEnum.biz.ToString()) {
                      //  model.BranchKey = _appSettingsOption.Value.Notification.BusinessBranchApiKey;
                      //}
                      //else if (appKey == AppKeyEnum.pay.ToString()) {
                      //  model.BranchKey = _appSettingsOption.Value.Notification.PaymentBranchApiKey;
                      //}
                      modelDL.BranchKey = _notificationAppSettingOptions.Value.VendorBranchApiKey;
                      branchkeyList.Add((int)UserTypeEnum.Vendor, modelDL);
                      break;

                    case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                    case VendorNotificationEventEnum.VendorUserInviteWithNewEmail:
                        //return _appSettings.VendorBranchApiKey;
                        DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                        model.DeepLinkAction = "vendorprimaryuser,invite";
                        List<string> parameterList = new List<string>();
                        parameterList.Add("tenantid");
                        parameterList.Add("appuserid");
                        parameterList.Add("identityserveruserid");
                        parameterList.Add("loginEmail");
                        parameterList.Add("usertype");
                        parameterList.Add("code");
                        parameterList.Add("tokeninfoid");
                        parameterList.Add("subDomain");
                        model.DeeplinkParametersList = parameterList;
                        //if (appKey == AppKeyEnum.biz.ToString()) {
                        //  model.BranchKey = _appSettingsOption.Value.Notification.BusinessBranchApiKey;
                        //}
                        //else if (appKey == AppKeyEnum.pay.ToString()) {
                        //  model.BranchKey = _appSettingsOption.Value.Notification.PaymentBranchApiKey;
                        //}
                        model.BranchKey = _notificationAppSettingOptions.Value.VendorBranchApiKey;
                        branchkeyList.Add((int)UserTypeEnum.Vendor, model);
                        break;

                    default:
                        branchkeyList.Add(0, new DeeplinkActionParameterAndBranchKeyDTO { });
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }

            return branchkeyList;

        }

        /// <inheritdoc/>
        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {
            // Note: This method is not required.
            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
            return deeplinkresult;
        }

        public override NotificationDeliveryType GetSupportedNotificationType(long platformNotificationEvents) {
            switch((VendorNotificationEventEnum)platformNotificationEvents) {
                case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                case VendorNotificationEventEnum.VendorInviteWithExistingEmail:
                case VendorNotificationEventEnum.VendorUserInviteWithNewEmail:
                case VendorNotificationEventEnum.VendorUserInviteWithExistingEmail:
                case VendorNotificationEventEnum.VendorUserForgotPassword:
                    return NotificationDeliveryType.Email;
                case VendorNotificationEventEnum.VendorUserOnboardOnVendorApp:
                case VendorNotificationEventEnum.VendorUserSetupAppOnboard:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                default:
                    return NotificationDeliveryType.None;
            }
        }



        #endregion

        #region Email Methods

        /// <inheritdoc/>
        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xslArguments) {
            EmailPayload emailPayload = new EmailPayload();
            try {
                emailPayload.EmailDeliverySubType = (int)EmailSubType.HTML;
                emailPayload.NotificationDeliveryType = (int)NotificationDeliveryType.Email;
                emailPayload.EventXMLData = eventXMLData;
                emailPayload.HasLinkError = hasLinkError;
                emailPayload.RecepientUserId = recepientDataRow.TenantUserId;
                emailPayload.TenantId = recepientDataRow.TenantId;
                emailPayload.UserEmailAddress = Convert.ToString(recepientDataRow.Email);

                if(notificationPayload.LoggedinUserId == emailPayload.RecepientUserId) {
                    emailPayload.IsLoggedInUser = true;
                }
                else {
                    emailPayload.IsLoggedInUser = false;
                }

                // Get user's local region language.
                if(string.IsNullOrEmpty(recepientDataRow.RegionLanguage)) {
                    emailPayload.UserLanguage = AppPortalConstants.DefaultLanguage;
                }
                else {
                    emailPayload.UserLanguage = Convert.ToString(recepientDataRow.RegionLanguage);
                }

                emailPayload.NotificationInfo = new Dictionary<string, string>();
                emailPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                emailPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());

                emailPayload.XSLTArguments = xslArguments;

                #region XSL Email Template

                // Read xsl template file content as string.
                emailPayload.XSLTemplateContent = GetEmailXsltFile((VendorNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
                emailPayload.InMemoryXSLTemplate = true;

                #endregion

                #region Email Deeplink

                ////ToDo:Nitin-Notification-This statement is not clear. Review this line.
                //emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);

                // If Condition: If email has url targetting to different client portal url.
                // Like invoice url target to 'Business Portal' for business user and target to 'Customer Portal' for customer user.
                if(notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists != null && notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Count > 0) {

                    // Get deeplink result set for current user type.
                    DeepLinkResultSetList deepLinkResultSetList = notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Where(i => i.DeeplingResultKey == recepientDataRow.UserType.ToString()).First();
                    notificationPayload.DeeplinkResultSet.DeeplinkResults = deepLinkResultSetList.DeeplinkResults;

                    // Assign deeplink result i.e. deeplink url to email payload.
                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                }
                // Else Condition: If email has url targetting to single client portal url.
                else {

                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                }

                #endregion

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            // Create list of values required to generate required notification from xsl template.
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();

            // Map common xsl parameters
            xslArguments.Add(NotificationConstants.RecipientFullNameKey, userDataRow.FullName);
            xslArguments.Add(NotificationConstants.EventNumberKey, Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));

            Dictionary<string, string> eventInfo = new Dictionary<string, string>();
            if(payload.EventInfo.ContainsKey(NotificationConstants.EventDataKey)) {
                try {
                    eventInfo = payload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>;

                    switch((VendorNotificationEventEnum)payload.EventId) {

                        case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;
                        case VendorNotificationEventEnum.VendorUserSetupAppOnboard:
                            xslArguments.Add("publisherName", eventInfo["publisherName"]);
                            xslArguments.Add("appName", eventInfo["appName"]);
                            xslArguments.Add("vendorName", eventInfo["vendorName"]);
                            xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                            xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;

                        case VendorNotificationEventEnum.VendorUserForgotPassword:
                          xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                          xslArguments.Add("customerUserName", eventInfo["businessUserName"]);
                          xslArguments.Add("customerUserID", eventInfo["businessUserID"]);
                          xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                          xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                          break;

                        default:
                          break;
                    }
                }
                catch(Exception ex) {
                    StringBuilder exceptionDetail = new StringBuilder();
                    exceptionDetail.Append("Exception occurred in VendorNotificationService.GetXMLArgumentsForEmail:-");
                    exceptionDetail.AppendLine();
                    exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                    _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                    throw;
                }
            }
            return xslArguments;

        }

        // Read email notification xsl template file content as string.
        private string GetEmailXsltFile(VendorNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                string templatePath = Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage, "Vendor", templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(templatePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetEmailXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            // CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
            return xsltTextFile;
        }

        // This method returns xsl file name that contains template name.
        private string GetXSLTemplateFileName(VendorNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                    return "VendorInviteWithNewEmail.xslt";
                case VendorNotificationEventEnum.VendorUserSetupAppOnboard:
                    return "VendorUserSetupAppOnBoard.xslt";
                case VendorNotificationEventEnum.VendorUserForgotPassword:
                    return "VendorUserForgotPassword.xslt";

                default:
                    return "";
            }
        }

        #endregion

        #region SMS Methods

        /// <inheritdoc/>
        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #endregion

        #region Push Methods

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #endregion

        #region AS Methods

        /// <inheritdoc/>
        public override ASNotificationPayload GetASNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, Dictionary<string, string> xslArguments) {
            ASNotificationPayload asNotificationPayload = new ASNotificationPayload();

            try {
                if(notificationPayload != null && (notificationPayload.SupportedDeliveryType & NotificationDeliveryType.ASNotification) == NotificationDeliveryType.ASNotification) {

                    Dictionary<string, string> eventData = (notificationPayload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>);

                    asNotificationPayload.TenantId = recepientDataRow.TenantId;
                    asNotificationPayload.RecepientUserId = recepientDataRow.TenantUserId;
                    asNotificationPayload.EventId = notificationPayload.EventId;
                    asNotificationPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
                    asNotificationPayload.EventXMLData = eventXMLData;
                    asNotificationPayload.XSLTArguments = xslArguments;
                    asNotificationPayload.EntityType = Convert.ToInt32(eventData["asTargetEntityType"]);
                    asNotificationPayload.EntityId = Guid.Parse(eventData["asTargetEntityId"]);
                    asNotificationPayload.AppId = Guid.Parse(eventData["asAppId"]);
                    asNotificationPayload.ASNotificationType = notificationPayload.EventId;
                    if(eventData.ContainsKey(NotificationConstants.ASAdditionalInfoKey)) {
                        asNotificationPayload.AdditionalInfo = eventData[NotificationConstants.ASAdditionalInfoKey];
                    }

                    if(notificationPayload.LoggedinUserId == asNotificationPayload.RecepientUserId) {
                        asNotificationPayload.IsLoggedInUser = true;
                    }
                    else {
                        asNotificationPayload.IsLoggedInUser = false;
                    }

                    string language = string.Empty;

                    // Get user's local region language.
                    if(string.IsNullOrEmpty(recepientDataRow.RegionLanguage)) {
                        language = Core.BaseService.Constants.DefaultLanguage;
                    }
                    else {
                        language = Convert.ToString(recepientDataRow.RegionLanguage);
                    }

                    asNotificationPayload.UserLanguage = language;

                    asNotificationPayload.NotificationInfo = new Dictionary<string, string>();
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());
                    asNotificationPayload.NotificationDeliveryType = (int)Core.NotificationService.NotificationDeliveryType.ASNotification;

                    #region XSL AS-Notification Template

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((VendorNotificationEventEnum)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetASNotificationPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return asNotificationPayload;
        }

        protected override Dictionary<string, string> GetXSLArgumentsForASNotification(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();
            // Map common xsl parameters
            xslArguments.Add(NotificationConstants.RecipientFullNameKey, userDataRow.FullName);
            xslArguments.Add(NotificationConstants.EventNumberKey, Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));
            Dictionary<string, string> eventInfo = payload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>;
            switch((VendorNotificationEventEnum)payload.EventId) {

                case VendorNotificationEventEnum.VendorUserOnboardOnVendorApp:
                    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    xslArguments.Add("appName", eventInfo["appName"]);
                    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    break;
                default:
                    break;
            }
            return xslArguments;
        }

        private string GetASNotificationXsltFile(VendorNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, "Vendor", templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in VendorNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(VendorNotificationEventEnum eventId) {

            switch(eventId) {
                case VendorNotificationEventEnum.VendorInviteWithNewEmail:
                    return "VendorInviteWithNewEmail.xslt";
                case VendorNotificationEventEnum.VendorUserSetupAppOnboard:
                    return "VendorUserSetupAppOnBoard.xslt";

                default:
                    return GetXSLTemplateFileName(eventId);
            }
        }

        public override bool AddASNotification(ASNotificationDTO aSNotificationDTO, NotificationPayload<NotificationRecipient> notificationPayload) {
            ASNotification aSNotification = new ASNotification();
            aSNotification.AppId = aSNotificationDTO.AppId;
            aSNotification.EntityId = aSNotificationDTO.SourceEntityId;
            aSNotification.EntityType = aSNotificationDTO.SourceEntityType;
            aSNotification.HtmlContent = aSNotificationDTO.HtmlContent;
            aSNotification.LinkNotificationId = aSNotificationDTO.LinkNotificationId;
            aSNotification.LogType = aSNotificationDTO.ASNotificationType;
            aSNotification.MetaData = aSNotificationDTO.MetaData;
            aSNotification.ReadState = false;
            aSNotification.RecipientTenantUserId = aSNotificationDTO.RecipientUserId;
            aSNotification.TenantId = aSNotificationDTO.TenantId;
            aSNotification.TextContent = aSNotificationDTO.TextContent;
            aSNotification.AdditionalInfo = aSNotificationDTO.AdditionalInfo;
            _asNotificationDS.UpdateSystemFieldsByOpType(aSNotification, OperationType.Add);
            _asNotificationDS.Add(aSNotification);
            _asNotificationDS.Save();
            return true;
        }

        public override void GenerateASNotification(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, ASNotificationDTO aSNotificationDTO) {
            base.GenerateASNotification(notificationPayload, recepientDataRow, aSNotificationDTO);
        }

        #endregion

        #region Not Implemented Exception

        /// <inheritdoc/>
        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #region Deeplink Payload Methods

        private Dictionary<string, Dictionary<string, string>> GetVendorNewUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,vendoruser,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);   // This URL is not in use


                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKeyTemp(decisonData).ToString());
                    invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetBusinessNewUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetVendorUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
          Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
          try
          {
            if (notificationPayload.EventInfo.ContainsKey("DeeplinkInfo"))
            {
              Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
              Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
              //Set fix information in deeplink
              string action = "vendoruser,forgotpassword";
              invitationDeeplinkInfo.Add("Actions", action);
              // ToDo: Nitin-Notification-Try to add params herre.
              //invitationDeeplinkInfo.Add("Params", action);
              invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

              List<string> decisonData = new List<string>();
              decisonData.Add(eventId.ToString());

              // Set Dynamic info from Notification payload.
              invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
              invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
              invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
              invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
              invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
              invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
              invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
              invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
              invitationDeeplinkInfo.Add("branchkey", GetBranchKeyTemp(decisonData).ToString());
              invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
              invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
              //invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
              deeplinks.Add("activationLinkForgotPassword", invitationDeeplinkInfo);

            }
          }
          catch (Exception ex)
          {
            StringBuilder exceptionDetail = new StringBuilder();
            exceptionDetail.Append("Exception occurred in CustNotificationService.GetVendorUserForgotPasswordDeeplinkPayload:-");
            exceptionDetail.AppendLine();
            exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
            _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
            throw;
          }
          return deeplinks;
        }

    #endregion

    #endregion

  }
}
