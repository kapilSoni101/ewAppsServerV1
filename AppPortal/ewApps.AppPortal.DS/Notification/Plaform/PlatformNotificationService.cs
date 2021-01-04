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
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using ewApps.AppPortal.Entity;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.ExceptionService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class PlatformNotificationService:NotificationService<NotificationRecipient>, IPlatformNotificationService {

        IPlatformNotificationRecipientDS _notificationRecipientDS;
        IASNotificationDS _aSNotificationDS;
        IOptions<NotificationAppSettings> _appSettingsOption;
        IMapper _mapper;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformNotificationService"/> class.
        /// </summary>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="notificationRecipientDS">The notification recipient ds.</param>
        /// <param name="appSettingsOption">The application settings option.</param>
        /// <param name="mapper">The mapper.</param>
        public PlatformNotificationService(ILogger<NotificationService<NotificationRecipient>> loggerService, IEmailService emailService, ISMSService smsService
            , IPlatformNotificationRecipientDS notificationRecipientDS, IASNotificationDS aSNotificationDS, IOptions<NotificationAppSettings> appSettingsOption, IMapper mapper)
            : base(loggerService, emailService, smsService) {
            _notificationRecipientDS = notificationRecipientDS;
            _appSettingsOption = appSettingsOption;
            _aSNotificationDS = aSNotificationDS;
            _mapper = mapper;
            base._appSettings = _mapper.Map<IOptions<NotificationAppSettings>>(appSettingsOption).Value;
        }

        #endregion

        #region Override Methods

        #region Validation Methods

        /// <summary>
        /// Validates the given parameters to generate the notification like module and event id.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="eventInfo">The event information.</param>
        /// <returns>
        /// Returns true if all notification related required information is available.
        /// </returns>
        /// <exception cref="InvalidOperationException">Invalid notification arguments.</exception>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                PlatformNotificationEvents parsedEvent;
                if(moduleId <= 0) {
                    valid = false;
                }
                if(Enum.TryParse<PlatformNotificationEvents>(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.ValidateNotificationDate:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #endregion

        #region Recipient List Method

        /// <summary>
        /// Gets notification recipient list based on input nofication payload.
        /// </summary>
        /// <param name="payload">Instance of notification payload use to get nofication recipients.</param>
        /// <returns>
        /// Returns list of notification recipient list.
        /// </returns>
        /// <exception cref="EwpInvalidOperationException">Invalid Notification Event No.</exception>
        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {

            // Get the required filters for getting the receipent list
            Guid loginUserId = Guid.Empty;
            Guid loginTenantId = Guid.Empty;
            Guid appId = Guid.Empty;
            long emailPreference = 0;

            try {
                Dictionary<string, string> eventInfo = (Dictionary<string, string>)(payload.EventInfo["EventData"]);
                if(eventInfo.ContainsKey("appuserid")) {
                    loginUserId = Guid.Parse(eventInfo["appuserid"]);
                }

                if(eventInfo.ContainsKey("tenantid")) {
                    loginTenantId = Guid.Parse(eventInfo["tenantid"]);
                }

                if(eventInfo.ContainsKey("appid")) {
                    appId = Guid.Parse(eventInfo["appid"]);
                }

                // Calling the DS to get the reciepent list.
                switch((PlatformNotificationEvents)payload.EventId) {

                    case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                        return _notificationRecipientDS.GetInvitedPlatformUser(appId, loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PlatformUserExistingEmailInvite:
                        return _notificationRecipientDS.GetInvitedPlatformUser(appId, loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PlatformUserForgotPassword:
                        return _notificationRecipientDS.GetForgotPasswordPlatformUser(loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite:
                        return _notificationRecipientDS.GetInvitedPublihserUser(appId, loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite:
                        return _notificationRecipientDS.GetInvitedPublihserUser(appId, loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPublisherUsers:
                        return _notificationRecipientDS.GetAllPublisherUsers(loginTenantId);

                    case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPlatformUsers:
                        emailPreference = long.Parse(eventInfo["emailPrefrence"]);
                        return _notificationRecipientDS.GetAllPlatformUserWithPrerence(loginTenantId, emailPreference);

                    case PlatformNotificationEvents.PlatformUserPasswordSetSucess:
                        return _notificationRecipientDS.GetInvitedPlatformUser(appId, loginTenantId, loginUserId);

                    case PlatformNotificationEvents.PlatformUserMarkAsActiveInActive:
                        emailPreference = long.Parse(eventInfo["emailPrefrence"]);
                        return _notificationRecipientDS.GetInvitedUserWithPrefrence(appId, loginTenantId, loginUserId, emailPreference);

                    case PlatformNotificationEvents.PlatformUserPermissionChanged:
                        emailPreference = long.Parse(eventInfo["emailPrefrence"]);
                        return _notificationRecipientDS.GetInvitedUserWithPrefrence(appId, loginTenantId, loginUserId, emailPreference);

                    case PlatformNotificationEvents.PlatformUserOnBoard:
                        emailPreference = long.Parse(eventInfo["emailPrefrence"]);
                        return _notificationRecipientDS.GetAllPlatformUserWithPrerence(loginTenantId, emailPreference);

                    case PlatformNotificationEvents.PlatformUserDeleted:
                        return _notificationRecipientDS.GetInvitedPlatformUser(appId, loginTenantId, loginUserId);

                    default:
                        throw new EwpInvalidOperationException("Invalid Notification Event No.");
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetRecipientList:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #endregion

        #region Deeplink Methods

        /// <summary>
        /// Generates Deeplink payload by the data given in notification payload.
        /// </summary>
        /// <param name="notificationPayload"></param>
        /// <returns></returns>
        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {
            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();
            try {

                // Set all common fields for all the deeplink 
                if(notificationPayload != null) {
                    // Map deeplink payload from notification payload.
                    deeplinkPayload.ModuleId = notificationPayload.ModuleId;
                    deeplinkPayload.EventId = notificationPayload.EventId;
                    deeplinkPayload.NotificationId = notificationPayload.NotificationId;
                    deeplinkPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
                    deeplinkPayload.LoggedinUserId = notificationPayload.LoggedinUserId;

                    // Set Deeplink specific data
                    if(notificationPayload.EventInfo.ContainsKey(NotificationConstants.DeeplinkInfoKey)) {
                        switch((PlatformNotificationEvents)deeplinkPayload.EventId) {
                            case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                                deeplinkPayload.DeeplinkInfo = GetPlatformNewEmailUserDeeplinkPayload(notificationPayload, (long)PlatformNotificationEvents.PlatformUserWithNewEmailInvite);
                                break;
                            case PlatformNotificationEvents.PlatformUserExistingEmailInvite:
                                deeplinkPayload.DeeplinkInfo = GetPlatformExistingEmailUserDeeplinkPayload(notificationPayload, (long)PlatformNotificationEvents.PlatformUserExistingEmailInvite);
                                break;
                            case PlatformNotificationEvents.PlatformUserForgotPassword:
                                deeplinkPayload.DeeplinkInfo = GetPlatformUserForgotPasswordDeeplinkPayload(notificationPayload, (long)PlatformNotificationEvents.PlatformUserForgotPassword);
                                break;
                            case PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite:
                                deeplinkPayload.DeeplinkInfo = GetPublisherPrimaryUserWithNewEmailDeeplinkPayload(notificationPayload, (long)PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite);
                                break;
                            case PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite:
                                deeplinkPayload.DeeplinkInfo = GetPublisherPrimaryUserWithExistingEmailDeeplinkPayload(notificationPayload, (long)PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite);
                                break;
                        }
                    }
                }

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in NotificationService.GetDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinkPayload;
        }

        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {
            //string deeplinkJSON = deeplinkResultSet.DeeplinkResults["deeplink1"].DeeplinkJson;
            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
            return deeplinkresult;
        }

        /// <summary>
        /// Returns branch api key to be use in deeplink generation based on Platform module's unique event id.
        /// </summary>
        /// <param name="eventId">A unique event id (<see cref="PlatformNotificationEvents"/>) defined in Platform module.</param>
        /// <returns>
        /// Returns branch Api key.
        /// </returns>
        /// <seealso cref="PlatformNotificationEvents"/>
        protected override object GetBranchKey(object eventId) {
            try {
                switch((PlatformNotificationEvents)eventId) {
                    case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                        return _appSettingsOption.Value.PlatformBranchApiKey;
                    case PlatformNotificationEvents.PlatformUserExistingEmailInvite:
                        return _appSettingsOption.Value.PlatformBranchApiKey;
                    case PlatformNotificationEvents.PlatformUserForgotPassword:
                        return _appSettingsOption.Value.PlatformBranchApiKey;
                    case PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite:
                        return _appSettingsOption.Value.PublisherBranchApiKey;
                    case PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite:
                        return _appSettingsOption.Value.PublisherBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            // ToDo: Review: Raise exception if eventId is not in pre-defined notification event.
            // ToDo: Review: Check xml comments of this method.
            return "";
        }

        #endregion

        #region Badge Count Methods

        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        #endregion

        #region Email Notification

        protected override Dictionary<string, string> GetXSLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();
            try {
                switch((PlatformNotificationEvents)payload.EventId) {
                    case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                        Dictionary<string, string> primaryUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", primaryUser["platformCompanyName"]);
                        xslArguments.Add("invitedUser", primaryUser["invitedUser"]);
                        xslArguments.Add("platformInvitingUser", primaryUser["platformInvitingUser"]);
                        xslArguments.Add("invitedUserEmailId", primaryUser["invitedUserEmailId"]);
                        xslArguments.Add("platformPortalURL", primaryUser["platformPortalURL"]);
                        xslArguments.Add("copyrightText", primaryUser["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserExistingEmailInvite:
                        Dictionary<string, string> platformUserExistingEmailInvite = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", platformUserExistingEmailInvite["platformCompanyName"]);
                        xslArguments.Add("invitedUser", platformUserExistingEmailInvite["invitedUser"]);
                        xslArguments.Add("platformInvitingUser", platformUserExistingEmailInvite["platformInvitingUser"]);
                        xslArguments.Add("invitedUserEmailId", platformUserExistingEmailInvite["invitedUserEmailId"]);
                        xslArguments.Add("platformPortalURL", platformUserExistingEmailInvite["platformPortalURL"]);
                        xslArguments.Add("copyrightText", platformUserExistingEmailInvite["copyrightText"]);
                        xslArguments.Add("emailId", platformUserExistingEmailInvite["emailId"]);
                        xslArguments.Add("portalName", platformUserExistingEmailInvite["portalName"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserForgotPassword:
                        Dictionary<string, string> forgotPassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", forgotPassword["platformCompanyName"]);
                        xslArguments.Add("platformUser", forgotPassword["platformUser"]);
                        xslArguments.Add("platformUserEmailId", forgotPassword["platformUserEmailId"]);
                        xslArguments.Add("copyrightText", forgotPassword["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite:
                        Dictionary<string, string> publisherUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", publisherUser["publisherCompanyName"]);
                        xslArguments.Add("platformCompanyName", publisherUser["platformCompanyName"]);
                        xslArguments.Add("invitedUser", publisherUser["invitedUser"]);
                        xslArguments.Add("subDomain", publisherUser["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", publisherUser["invitedUserEmailId"]);
                        xslArguments.Add("publisherPortalURL", publisherUser["publisherPortalURL"]);
                        xslArguments.Add("copyrightText", publisherUser["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite:
                        Dictionary<string, string> publisherUserExisting = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", publisherUserExisting["publisherCompanyName"]);
                        xslArguments.Add("platformCompanyName", publisherUserExisting["platformCompanyName"]);
                        xslArguments.Add("invitedUser", publisherUserExisting["invitedUser"]);
                        xslArguments.Add("subDomain", publisherUserExisting["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", publisherUserExisting["invitedUserEmailId"]);
                        xslArguments.Add("publisherPortalURL", publisherUserExisting["publisherPortalURL"]);
                        xslArguments.Add("copyrightText", publisherUserExisting["copyrightText"]);
                        xslArguments.Add("emailId", publisherUserExisting["emailId"]);
                        xslArguments.Add("portalName", publisherUserExisting["portalName"]);
                        break;

                    case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPublisherUsers:
                        Dictionary<string, string> activePublisher = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", activePublisher["platformCompanyName"]);
                        xslArguments.Add("publisherCompanyName", activePublisher["publisherCompanyName"]);
                        xslArguments.Add("status", activePublisher["status"]);
                        xslArguments.Add("copyrightText", activePublisher["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPlatformUsers:
                        Dictionary<string, string> activePlatform = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", activePlatform["platformCompanyName"]);
                        xslArguments.Add("publisherCompanyName", activePlatform["publisherCompanyName"]);
                        xslArguments.Add("platformUserChanged", activePlatform["platformUserChanged"]);
                        xslArguments.Add("status", activePlatform["status"]);
                        xslArguments.Add("copyrightText", activePlatform["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserPasswordSetSucess:
                        Dictionary<string, string> platformuserpassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", platformuserpassword["platformCompanyName"]);
                        xslArguments.Add("platformUser", platformuserpassword["platformUser"]);
                        xslArguments.Add("invitedUserEmailId", platformuserpassword["invitedUserEmailId"]);
                        xslArguments.Add("platformPortalURL", platformuserpassword["platformPortalURL"]);
                        xslArguments.Add("copyrightText", platformuserpassword["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserMarkAsActiveInActive:
                        Dictionary<string, string> platformActive = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", platformActive["platformCompanyName"]);
                        xslArguments.Add("platformUser", platformActive["platformUser"]);
                        xslArguments.Add("platformUserChanged", platformActive["platformUserChanged"]);
                        xslArguments.Add("status", platformActive["status"]);
                        xslArguments.Add("copyrightText", platformActive["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserPermissionChanged:
                        Dictionary<string, string> permissionChange = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", permissionChange["platformCompanyName"]);
                        xslArguments.Add("platformUser", permissionChange["platformUser"]);
                        xslArguments.Add("platformUserChanged", permissionChange["platformUserChanged"]);
                        xslArguments.Add("copyrightText", permissionChange["copyrightText"]);
                        break;

                    case PlatformNotificationEvents.PlatformUserOnBoard:
                        Dictionary<string, string> platformOnBoard = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", platformOnBoard["platformCompanyName"]);
                        xslArguments.Add("newUser", platformOnBoard["newUser"]);
                        xslArguments.Add("copyrightText", platformOnBoard["copyrightText"]);
                        break;
                    case PlatformNotificationEvents.PlatformUserDeleted:
                        Dictionary<string, string> platformDeleted = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("platformCompanyName", platformDeleted["platformCompanyName"]);
                        xslArguments.Add("platformUser", platformDeleted["platformUser"]);
                        xslArguments.Add("platformUserChanged", platformDeleted["platformUserChanged"]);
                        xslArguments.Add("copyrightText", platformDeleted["copyrightText"]);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetXMLArgumentsForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;
        }

        /// <summary>
        /// Generates Push payload by the data given in notification payload and USerDataRow.
        /// </summary>
        /// <param name="notificationPayload">Instance of notification payload to map email payload information.</param>
        /// <returns>An instance of EmailPayload.</returns>
        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xslArguments) {
            EmailPayload emailPayload = new EmailPayload();

            try {
                //Set all common fields for all the deeplink 
                if(notificationPayload != null) {
                    //emailPayload.AppId= notificationPayload
                    // TODO:
                    emailPayload.AppType = 1;
                    // emailPayload.CustomData
                    emailPayload.EmailDeliverySubType = (int)EmailSubType.HTML;
                    emailPayload.EventXMLData = eventXMLData;
                    emailPayload.HasLinkError = hasLinkError;

                    // emailPayload.InMemoryXSLTemplate
                    if(notificationPayload.LoggedinUserId == emailPayload.RecepientUserId) {
                        emailPayload.IsLoggedInUser = true;
                    }
                    else {
                        emailPayload.IsLoggedInUser = false;
                    }

                    emailPayload.NotificationDeliveryType = (int)NotificationDeliveryType.Email;
                    emailPayload.NotificationInfo = new Dictionary<string, string>();
                    emailPayload.NotificationInfo.Add("TrackingId", notificationPayload.TrackingId.ToString());
                    emailPayload.NotificationInfo.Add("LinkNotificationId", notificationPayload.LinkedNotificationId.ToString());
                    emailPayload.RecepientUserId = new Guid(Convert.ToString(recepientRow.TenantUserId));

                    emailPayload.TenantId = new Guid(Convert.ToString(recepientRow.TenantId));
                    emailPayload.UserEmailAddress = Convert.ToString(recepientRow.Email);

                    // ToDo: Nitin: Use user language saved in db.
                    //// Get user's local region language.
                    //if(string.IsNullOrEmpty(recepientRow.RegionLanguage)) {
                    //  language = "en";
                    //}
                    //else {
                    //  language = Convert.ToString(recepientRow.RegionLanguage);
                    //}
                    emailPayload.UserLanguage = "en";

                    // emailPayload.UserSession
                    emailPayload.XSLTArguments = xslArguments;

                    #region XSL Email Template

                    string xslFileContent = GetEmailXsltFile((PlatformNotificationEvents)notificationPayload.EventId, emailPayload.UserLanguage);
                    emailPayload.InMemoryXSLTemplate = true;
                    emailPayload.XSLTemplateContent = xslFileContent;

                    #endregion

                    #region email Deeplink

                    // ToDo:Nitin-Notification-This statement is not clear.DeeplinkPayload
                    emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);
                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                    #endregion
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());

                throw;
            }
            return emailPayload;
        }


        #endregion

        #region SMS Notification

        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, string> GetXSLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }


        #endregion

        #region AS-Notification

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForASNotification(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();
            try {
                switch((PlatformNotificationEvents)payload.EventId) {
                    case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                        Dictionary<string, string> primaryUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("invitedUser", primaryUser["invitedUser"]);
                        xslArguments.Add("platformInvitingUser", primaryUser["platformInvitingUser"]);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetXSLArgumentsForASNotification:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;
        }

        public override ASNotificationPayload GetASNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, Dictionary<string, string> xslArguments) {
            ASNotificationPayload asNotificationPayload = new ASNotificationPayload();

            try {
                if(notificationPayload != null && (notificationPayload.SupportedDeliveryType & NotificationDeliveryType.ASNotification) == NotificationDeliveryType.ASNotification) {
                    Dictionary<string, string> eventData = (notificationPayload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>);

                    asNotificationPayload.TenantId = recepientDataRow.TenantId;
                    //asNotificationPayload.PartnerTenantId = new Guid(Convert.ToString(recepientDataRow.PartnerTenantId));
                    asNotificationPayload.RecepientUserId = recepientDataRow.TenantUserId;
                    asNotificationPayload.EventId = notificationPayload.EventId;
                    //  asNotificationPayload.AppId = Guid.Parse(eventData[NotificationConstants.AppIdKey]);
                    asNotificationPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
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

                    //ToDo: Instead of default language, use user's region language.
                    // Get user's local region language.
                    //if(string.IsNullOrEmpty(recepientRow.RegionLanguage)) {
                    //  language = "en";
                    //}
                    //else {
                    //  language = Convert.ToString(recepientRow.RegionLanguage);
                    //}

                    //ToDo: Pick this language from database as per user settings.
                    language = Core.BaseService.Constants.DefaultLanguage;
                    asNotificationPayload.UserLanguage = language;

                    asNotificationPayload.EventXMLData = eventXMLData;
                    asNotificationPayload.XSLTArguments = xslArguments;

                    asNotificationPayload.NotificationInfo = new Dictionary<string, string>();
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());
                    asNotificationPayload.NotificationDeliveryType = (int)Core.NotificationService.NotificationDeliveryType.ASNotification;

                    #region XSL AS-Notification Template

                    string xslFileContent = GetASNotificationXsltFile((PlatformNotificationEvents)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;
                    asNotificationPayload.XSLTemplateContent = xslFileContent;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GenerateASNotificationPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return asNotificationPayload;
        }

        public override bool AddASNotification(ASNotificationDTO aSNotificationDTO, NotificationPayload<NotificationRecipient> notificationPayload) {

            // Add code to insert ASNotification into database table.
            ASNotification aSNotification = new ASNotification();
            aSNotification.HtmlContent = aSNotificationDTO.HtmlContent;
            aSNotification.LinkNotificationId = aSNotificationDTO.LinkNotificationId;
            aSNotification.LogType = aSNotificationDTO.ASNotificationType;
            aSNotification.MetaData = aSNotificationDTO.MetaData;
            //aSNotification.PortalId = Guid.Empty;
            aSNotification.ReadState = false;
            aSNotification.RecipientTenantUserId = aSNotificationDTO.RecipientUserId;
            aSNotification.TenantId = aSNotificationDTO.TenantId;
            aSNotification.TextContent = aSNotificationDTO.TextContent;
            aSNotification.AdditionalInfo = aSNotificationDTO.AdditionalInfo;
            _aSNotificationDS.UpdateSystemFieldsByOpType(aSNotification, Core.BaseService.OperationType.Add);
            _aSNotificationDS.Add(aSNotification);
            _aSNotificationDS.Save();
            return true;
        }

        #endregion

        #region Push Notification

        protected override Dictionary<string, string> GetXSLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #endregion

        #region Other Methods

        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        public override NotificationDeliveryType GetSupportedNotificationType(long platformNotificationEvents) {
            switch((PlatformNotificationEvents)platformNotificationEvents) {
                case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                    return NotificationDeliveryType.All;
                default:
                    return NotificationDeliveryType.All;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private DeeplinkPayload SetDeeplinkPayloadForEmail(string linkKey, Guid tenantId, Guid notificationId, Guid userId) {
            DeeplinkPayload dPayload = new DeeplinkPayload();
            try {
                dPayload.TenantId = tenantId;
                dPayload.NotificationId = notificationId;
                dPayload.LinkNotificationId = notificationId;
                Dictionary<string, string> linkInfo = new Dictionary<string, string>();
                linkInfo.Add("Actions", "notification,list");
                linkInfo.Add("RelativeURL", "notification/");
                linkInfo.Add("tenantid", tenantId.ToString());
                linkInfo.Add("recepientuserid", userId.ToString());
                dPayload.DeeplinkInfo = new Dictionary<string, Dictionary<string, string>>();
                dPayload.DeeplinkInfo.Add(linkKey, linkInfo);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return dPayload;
        }

        // <summary>
        /// Gets the XSLT file based on user language from the cache if available
        /// otherwise load from the file system.
        /// </summary>
        /// <param name="regionLanguage">user language</param>
        /// <returns></returns>
        private string GetEmailXsltFile(PlatformNotificationEvents eventId, string regionLanguage) {
            //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
            //if (CacheHelper.IsInCache(cacheKey)) {NotificationXsltFile
            //  return CacheHelper.GetData<string>(cacheKey); ;
            //}
            //else {
            //Engg: should define in Config file.
            // string xsltResourcePath = "/resourcePath/";

            string xsltTextFile = "";
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetEmailXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            // CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
            return xsltTextFile;
            //}
            //string path = @"C:\ewAppsServices\ewAppsServer\ewApps.Payment.DS\XSL\BusinessUserInvitationEmail.xslt";

            //string xsltTextFile = FileHelper.ReadFileAsText(path);
            //return xsltTextFile;
        }

        private string GetXSLTemplateFileName(PlatformNotificationEvents eventNumber) {
            switch(eventNumber) {
                case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                    return "PlatformNewUserInvite.xslt";
                case PlatformNotificationEvents.PlatformUserExistingEmailInvite:
                    return "PlatformExistingUserInvite.xslt";
                case PlatformNotificationEvents.PlatformUserForgotPassword:
                    return "PlatfromUserForgotPassword.xslt";
                case PlatformNotificationEvents.PublisherPrimaryUserWithNewEmailInvite:
                    return "PublisherPrimaryNewUserInvite.xslt";
                case PlatformNotificationEvents.PublisherPrimaryUserWithExistingEmailInvite:
                    return "PublisherPrimaryExistingUserInvite.xslt";
                case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPublisherUsers:
                    return "PublisherStatusChangedToPublisherUser.xslt";
                case PlatformNotificationEvents.PublisherMarkAsActiveInActiveNotifyPlatformUsers:
                    return "PublisherStatusChangedToPlatfromUser.xslt";
                case PlatformNotificationEvents.PlatformUserPasswordSetSucess:
                    return "PlatformUserSetPasswordSuccess.xslt";
                case PlatformNotificationEvents.PlatformUserMarkAsActiveInActive:
                    return "PlatfromUserStatusChanged.xslt";
                case PlatformNotificationEvents.PlatformUserPermissionChanged:
                    return "PlatfromUserPermissionChanged.xslt";
                case PlatformNotificationEvents.PlatformUserOnBoard:
                    return "PlatfromUserOnBoard.xslt";
                case PlatformNotificationEvents.PlatformUserDeleted:
                    return "PlatformUserDeleted.xslt";
                default:
                    return "";
            }
        }

        private string GetASNotificationXsltFile(PlatformNotificationEvents eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                //filePath = Path.Combine(_appSettings.XslTemplateRootPath, filePath);
                //filePath = Path.Combine(filePath, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(PlatformNotificationEvents eventId) {
            switch(eventId) {
                case PlatformNotificationEvents.PlatformUserWithNewEmailInvite:
                    return "PlatformUserInvite.xslt";
                default:
                    return "";
            }
        }

        #region Deeplink Payload Methods

        private Dictionary<string, Dictionary<string, string>> GetPlatformNewEmailUserDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

            try {
                if(notificationPayload.EventInfo.ContainsKey(NotificationConstants.DeeplinkInfoKey)) {

                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo[NotificationConstants.DeeplinkInfoKey]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,platformuser,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params here.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add(NotificationConstants.TenantIdKey, deeplinkInfo[NotificationConstants.TenantIdKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.AppUserIdKey, deeplinkInfo[NotificationConstants.AppUserIdKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.IdentityServerUserIdKey, deeplinkInfo[NotificationConstants.IdentityServerUserIdKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.UserTypeKey, deeplinkInfo[NotificationConstants.UserTypeKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.CodeKey, deeplinkInfo[NotificationConstants.CodeKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.TokenInfoIdKey, deeplinkInfo[NotificationConstants.TokenInfoIdKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.TokenTypeKey, deeplinkInfo[NotificationConstants.TokenTypeKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.TenantLanguageKey, deeplinkInfo[NotificationConstants.TenantLanguageKey]);
                    invitationDeeplinkInfo.Add(NotificationConstants.BranchKey, GetBranchKey(eventId).ToString());

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetPlatformNewEmailUserDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPlatformExistingEmailUserDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,platformuser,existing";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
                    deeplinks.Add("domainScreenURL", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetPlatformExistingEmailUserDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPlatformUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,forgotpwd,platformuser";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetPlatformUserForgotPasswordDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPublisherPrimaryUserWithNewEmailDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,pubuser,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetPublisherPrimaryUserWithNewEmailDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPublisherPrimaryUserWithExistingEmailDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,pubuser,existing";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
                    deeplinks.Add("domainScreenURL", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PlatformNotificationService.GetPublisherPrimaryUserWithExistingEmailDeeplinkPayload:-");
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
