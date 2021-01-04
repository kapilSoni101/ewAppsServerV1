/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

    public class PublisherNotificationService:NotificationService<NotificationRecipient>, IPublisherNotificationService {

        IPublisherNotificationRecipientDS _notificationRecipientDS;
        NotificationAppSettings _notificationAppSettings;
        IASNotificationDS _asNotificationDS;
        

        #region Constructor

        public PublisherNotificationService(IEmailService emailService, ISMSService smsService, IPublisherNotificationRecipientDS notificationRecipientDS, IOptions<NotificationAppSettings> appSettingsOption, ILogger<INotificationService<NotificationRecipient>> loggerService, IOptions<NotificationAppSettings> appPortalAppSettingOptions, IASNotificationDS asNotificationDS, IASNotificationService asNotificationService)
            : base(loggerService, emailService, smsService, asNotificationService, appSettingsOption) {
            _notificationRecipientDS = notificationRecipientDS;
            _notificationAppSettings = appSettingsOption.Value;
            _asNotificationDS = asNotificationDS;
        }

        #endregion

        #region Other Methods

        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            try {

                bool valid = true;
                PublisherNotificationEvent parsedEvent;
                if(moduleId <= 0) {
                    valid = false;
                }
                if(Enum.TryParse<PublisherNotificationEvent>(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.ValidateNotificationData:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        public override NotificationDeliveryType GetSupportedNotificationType(long publisherNotificationEvents) {
            switch((PublisherNotificationEvent)publisherNotificationEvents) {
                case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                case PublisherNotificationEvent.PublisherUserForgotPassword:
                case PublisherNotificationEvent.ContactUsToPlatformUser:               
                    return NotificationDeliveryType.Email;
                case PublisherNotificationEvent.PublisherUserPermissionChanged:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                default:
                    return NotificationDeliveryType.None;
            }
        }

        #endregion

        #region Recipient Method

        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {
            Guid loginUserId = Guid.Empty;
            Guid loginTenantId = Guid.Empty;
            Guid businessPartnerTenantId = Guid.Empty;
            Guid appId = Guid.Empty;
            long emailPreference = 0;

            try {
                switch((PublisherNotificationEvent)payload.EventId) {
                    case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    case PublisherNotificationEvent.PublisherUserForgotPassword:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        return _notificationRecipientDS.GetForgotPasswordPublisherUser(loginTenantId, loginUserId);

                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    //case PublisherNotificationEvent.ApplicationSubscribedToBusiness:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    return _notificationRecipientDS.GetBusinessUserWithApplicationAccessWithoutStatus(loginTenantId, appId);

                    //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToPublisherUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    return _notificationRecipientDS.GetPublisherUserWithApplicationPermission(loginTenantId, appId);

                    //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToBusinessUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    return _notificationRecipientDS.GetBusinessUserWithApplicationAccess(loginTenantId, appId);

                    case PublisherNotificationEvent.BusinessStatusChangedToPubUser:
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetPublisherUsersWithTenantPermissionAndPrefrence(loginTenantId, appId);

                    //case PublisherNotificationEvent.BusinessActiveInactiveToBusinesUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    return _notificationRecipientDS.GetAllBusinessUsers(loginTenantId);

                    //case PublisherNotificationEvent.BusinessActiveInactiveToBusinessPartnerUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    businessPartnerTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["businessPartnerTenantId"]);
                    //    return _notificationRecipientDS.GetAllBusinessPartnerUsers(loginTenantId, businessPartnerTenantId);

                    case PublisherNotificationEvent.BusinessIsDeletedToPubUser:
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetPublisherUserWithApplicationPermission(loginTenantId, appId);

                    //case PublisherNotificationEvent.BusinessDeletedToBusinesUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    return _notificationRecipientDS.GetAllBusinessUsers(loginTenantId);

                    //case PublisherNotificationEvent.BusinessDeletedBusinessPartnerUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    businessPartnerTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["businessPartnerTenantId"]);
                    //    return _notificationRecipientDS.GetAllBusinessPartnerUsers(loginTenantId, businessPartnerTenantId);

                    //case PublisherNotificationEvent.PublisherUserSetPasswordSucess:
                    //    loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    case PublisherNotificationEvent.PublisherUserOnBoard:
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        emailPreference = long.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["emailPrefrence"]);
                        return _notificationRecipientDS.GetPlatformUsersWithApplicationPermissionAndPrefrence(loginTenantId, emailPreference);

                    //case PublisherNotificationEvent.PublisherUserMarkAsActiveInactive:
                    //    loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    emailPreference = long.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["emailPrefrence"]);
                    //    return _notificationRecipientDS.GetInvitedUserWithPrefrence(appId, loginTenantId, loginUserId, emailPreference);

                    case PublisherNotificationEvent.PublisherUserPermissionChanged:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantUserId"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantId"]);
                        //appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        emailPreference = (int)PublisherUserPublisherAppPreferenceEnum.MyPermissionsChanged;
                        return _notificationRecipientDS.GetInvitedUserWithPrefrence(loginTenantId, loginUserId, emailPreference, (int)UserTypeEnum.Publisher, (int)TenantUserInvitaionStatusEnum.Accepted);

                    case PublisherNotificationEvent.PublisherUserDeleted:
                        loginUserId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appuserid"]);
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetInvitedUser(appId, loginTenantId, loginUserId);

                    case PublisherNotificationEvent.ApplicationStatusChangedToPubUser:
                        loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        return _notificationRecipientDS.GetPublisherUserWithApplicationPermission(loginTenantId, appId);

                    //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    return _notificationRecipientDS.GetBusinessUserWithApplicationAccess(loginTenantId, appId);

                    //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessPartnerUser:
                    //    loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                    //    appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                    //    businessPartnerTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["businessPartnerTenantId"]);
                    //    return _notificationRecipientDS.GetBusinessPartnerUserWithApplicationAccess(loginTenantId, businessPartnerTenantId, appId);

                    case PublisherNotificationEvent.ContactUsToPlatformUser:
                        //loginTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["tenantid"]);
                        //appId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["appid"]);
                        //businessPartnerTenantId = Guid.Parse((payload.EventInfo["EventData"] as Dictionary<string, string>)["businessPartnerTenantId"]);
                        return _notificationRecipientDS.GetAllPlatformUsersWithPreference();

                    default:
                        return null;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetRecipientList:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #endregion

        #region Deeplink Payload Methods

        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {
            //string deeplinkJSON = deeplinkResultSet.DeeplinkResults["deeplink1"].DeeplinkJson;
            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
            return deeplinkresult;
        }

        ///<inheritdoc/>
        protected override object GetBranchKey(object eventId) {
            try {
                switch((PublisherNotificationEvent)eventId) {
                    case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                        return _notificationAppSettings.PublisherBranchApiKey;
                    case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                        return _notificationAppSettings.PublisherBranchApiKey;
                    case PublisherNotificationEvent.PublisherUserForgotPassword:
                        return _notificationAppSettings.PublisherBranchApiKey;
                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                        return _notificationAppSettings.BusinessBranchApiKey;
                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                        return _notificationAppSettings.BusinessBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return "";
        }

        /// <summary>
        /// Generates Deeplink payload by the data given in notification payload.
        /// </summary>
        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {
            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();

            try {
                //Set all common fields for all the deeplink 
                if(notificationPayload != null) {
                    deeplinkPayload.EventId = notificationPayload.EventId;
                    deeplinkPayload.ModuleId = notificationPayload.ModuleId;
                    deeplinkPayload.NotificationId = notificationPayload.NotificationId;
                    deeplinkPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
                    deeplinkPayload.LoggedinUserId = notificationPayload.LoggedinUserId;
                };

                // Set Deeplink specific data
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    switch((PublisherNotificationEvent)deeplinkPayload.EventId) {
                        case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                            deeplinkPayload.DeeplinkInfo = GetPublisherUserNewEmailInviteDeeplinkPayload(notificationPayload, (long)PublisherNotificationEvent.InvitePublisherUserWithNewEmail);
                            break;
                        case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                            deeplinkPayload.DeeplinkInfo = GetPublisherUserExistingEmailInviteDeeplinkPayload(notificationPayload, (long)PublisherNotificationEvent.InvitePublisherUserWithExistingEmail);
                            break;
                        case PublisherNotificationEvent.PublisherUserForgotPassword:
                            deeplinkPayload.DeeplinkInfo = GetPublisherUserForgotPasswordDeeplinkPayload(notificationPayload, (long)PublisherNotificationEvent.PublisherUserForgotPassword);
                            break;
                        case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                            deeplinkPayload.DeeplinkInfo = GetPrimaryBusinessUserNewEmailInviteDeeplinkPayload(notificationPayload, (long)PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail);
                            break;
                        case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                            deeplinkPayload.DeeplinkInfo = GetPrimaryBusinessUserExistingEmailInviteDeeplinkPayload(notificationPayload, (long)PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail);
                            break;
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return dPayload;
        }

        private Dictionary<string, Dictionary<string, string>> GetPublisherUserNewEmailInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetPublisherUserNewEmailInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPublisherUserExistingEmailInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetPublisherUserExistingEmailInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }


        private Dictionary<string, Dictionary<string, string>> GetPublisherUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,forgotpwd,pubuser";
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetPublisherUserForgotPasswordDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        /// <summary>
        /// Generates deeplinks for business user invite event from notification data.
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> GetPrimaryBusinessUserNewEmailInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,businessuser,new";
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetPrimaryBusinessUserNewEmailInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        /// <summary>
        /// Generates deeplinks for business user invite event from notification data.
        /// </summary>
        private Dictionary<string, Dictionary<string, string>> GetPrimaryBusinessUserExistingEmailInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,businessuser,existing";
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
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetPrimaryBusinessUserExistingEmailInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        #endregion

        #region Email Notification

        // Gets the XSLT file based on user language from the cache if available
        // otherwise load from the file system.
        private string GetEmailXsltFile(PublisherNotificationEvent eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage, "Publisher", templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetEmailXsltFile:-");
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

        private string GetXSLTemplateFileName(PublisherNotificationEvent eventNumber) {
            switch(eventNumber) {
                case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                    return "PublisherNewUserInvite.xslt";
                case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                    return "PublisherExistingUserInvite.xslt";
                case PublisherNotificationEvent.PublisherUserForgotPassword:
                    return "PublisherUserForgotPassword.xslt";
                case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                    return "BusinessPrimaryNewUserInvite.xslt";
                case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                    return "BusinessPrimaryExistingUserInvite.xslt";
                case PublisherNotificationEvent.ContactUsToPlatformUser:
                    return "ContactUsEmail.xslt";
                case PublisherNotificationEvent.PublisherUserPermissionChanged:
                    return "PublisherUserPermissionChanged.xslt";

                //case PublisherNotificationEvent.ApplicationSubscribedToBusiness:
                //    return "NewApplicationSubscribedToBusiness.xslt";
                //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToPublisherUser:
                //    return "SubscriptionPlanEditToPublisherUser.xslt";
                //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToBusinessUser:
                //    return "SubscriptionPlanEditToBusinessUser.xslt";
                //case PublisherNotificationEvent.BusinessActiveInactiveToPublisherUser:
                //    return "BusinessStatusChangedToPublisherUser.xslt";
                //case PublisherNotificationEvent.BusinessActiveInactiveToBusinesUser:
                //    return "BusinessStatusChangedToBusinessUser.xslt";
                //case PublisherNotificationEvent.BusinessActiveInactiveToBusinessPartnerUser:
                //    return "BusinessStatusChangedToBusinessPartnerUser.xslt";
                //case PublisherNotificationEvent.BusinessDeletedToPublisherUser:
                //    return "BusinessDeletedToPublisherUser.xslt";
                //case PublisherNotificationEvent.BusinessDeletedToBusinesUser:
                //    return "BusinessDeletedToBusinessUser.xslt";
                //case PublisherNotificationEvent.BusinessDeletedBusinessPartnerUser:
                //    return "BusinessDeletedToBusinessPartnerUser.xslt";
                //case PublisherNotificationEvent.PublisherUserSetPasswordSucess:
                //    return "PublisherUserSetPasswordSucess.xslt";
                //case PublisherNotificationEvent.PublisherOnBoard:
                //    return "NewPublisherOnBoard.xslt";
                //case PublisherNotificationEvent.PublisherUserMarkAsActiveInactive:
                //    return "PublisherUserStatusChanged.xslt";

                //case PublisherNotificationEvent.PublisherUserOnBoard:
                //    return "PublisherUserOnBoard.xslt";
                //case PublisherNotificationEvent.PublisherUserDeleted:
                //    return "PublisherUserDeleted.xslt";
                //case PublisherNotificationEvent.PublisherApplicationStatusChangedToPublisherUser:
                //    return "ApplicationStatusChangedToPublisherUser.xslt";
                //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessUser:
                //    return "ApplicationStatusChangedToBusinessUser.xslt";
                //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessPartnerUser:
                //    return "ApplicationStatusChangedToBusinessPartnerUser.xslt";

                default:
                    return "";
            }
        }

        protected override Dictionary<string, string> GetXSLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();

            #region Common Region

            xslArguments.Add("RecipientFullName", userDataRow.FullName);
            xslArguments.Add("EventNumber", Convert.ToString(payload.EventId));

            #endregion

            try {

                switch((PublisherNotificationEvent)payload.EventId) {
                    case PublisherNotificationEvent.InvitePublisherUserWithNewEmail:
                        Dictionary<string, string> pubnewUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", pubnewUser["publisherCompanyName"]);
                        xslArguments.Add("invitedUser", pubnewUser["invitedUser"]);
                        xslArguments.Add("publisherInvitinguserName", pubnewUser["publisherInvitinguserName"]);
                        xslArguments.Add("subDomain", pubnewUser["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", pubnewUser["invitedUserEmailId"]);
                        xslArguments.Add("publisherPortalURL", pubnewUser["publisherPortalURL"]);
                        xslArguments.Add("copyrightText", pubnewUser["copyrightText"]);
                        break;
                    case PublisherNotificationEvent.InvitePublisherUserWithExistingEmail:
                        Dictionary<string, string> publisherUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", publisherUser["publisherCompanyName"]);
                        xslArguments.Add("invitedUser", publisherUser["invitedUser"]);
                        xslArguments.Add("publisherInvitinguserName", publisherUser["publisherInvitinguserName"]);
                        xslArguments.Add("subDomain", publisherUser["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", publisherUser["invitedUserEmailId"]);
                        xslArguments.Add("publisherPortalURL", publisherUser["publisherPortalURL"]);
                        xslArguments.Add("copyrightText", publisherUser["copyrightText"]);
                        xslArguments.Add("emailId", publisherUser["emailId"]);
                        xslArguments.Add("portalName", publisherUser["portalName"]);
                        break;
                    case PublisherNotificationEvent.PublisherUserForgotPassword:
                        Dictionary<string, string> forgotPassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", forgotPassword["publisherCompanyName"]);
                        xslArguments.Add("publisherUserName", forgotPassword["publisherUserName"]);
                        xslArguments.Add("publisherUserEmailId", forgotPassword["publisherUserEmailId"]);
                        xslArguments.Add("copyrightText", forgotPassword["copyrightText"]);
                        break;
                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithNewEmail:
                        Dictionary<string, string> businessUserNew = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", businessUserNew["publisherCompanyName"]);
                        xslArguments.Add("businessCompanyName", businessUserNew["businessCompanyName"]);
                        xslArguments.Add("invitedUser", businessUserNew["invitedUser"]);
                        xslArguments.Add("subDomain", businessUserNew["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", businessUserNew["invitedUserEmailId"]);
                        xslArguments.Add("businessPortalURL", businessUserNew["businessPortalURL"]);
                        xslArguments.Add("copyrightText", businessUserNew["copyrightText"]);
                        break;
                    case PublisherNotificationEvent.InviteBusinessPrimaryUserWithExistingEmail:
                        Dictionary<string, string> busExistingUSer = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", busExistingUSer["publisherCompanyName"]);
                        xslArguments.Add("businessCompanyName", busExistingUSer["businessCompanyName"]);
                        xslArguments.Add("invitedUser", busExistingUSer["invitedUser"]);
                        xslArguments.Add("subDomain", busExistingUSer["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", busExistingUSer["invitedUserEmailId"]);
                        xslArguments.Add("businessPortalURL", busExistingUSer["businessPortalURL"]);
                        xslArguments.Add("copyrightText", busExistingUSer["copyrightText"]);
                        xslArguments.Add("emailId", busExistingUSer["emailId"]);
                        xslArguments.Add("portalName", busExistingUSer["portalName"]);
                        break;
                    //case PublisherNotificationEvent.ApplicationSubscribedToBusiness:
                    //    Dictionary<string, string> appSubs = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", appSubs["publisherCompanyName"]);
                    //    xslArguments.Add("applicationName", appSubs["applicationName"]);
                    //    xslArguments.Add("businessCompanyName", appSubs["businessCompanyName"]);
                    //    xslArguments.Add("subDomain", appSubs["subDomain"]);
                    //    xslArguments.Add("applicationURL", appSubs["applicationURL"]);
                    //    xslArguments.Add("copyrightText", appSubs["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToPublisherUser:
                    //    Dictionary<string, string> busSubChange = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busSubChange["publisherCompanyName"]);
                    //    xslArguments.Add("applicationName", busSubChange["applicationName"]);
                    //    xslArguments.Add("businessCompanyName", busSubChange["businessCompanyName"]);
                    //    xslArguments.Add("oldsubscriptionPlanName", busSubChange["oldsubscriptionPlanName"]);
                    //    xslArguments.Add("newSubscriptionPlanName", busSubChange["newSubscriptionPlanName"]);
                    //    xslArguments.Add("publisherUserNameChanged", busSubChange["publisherUserNameChanged"]);
                    //    xslArguments.Add("copyrightText", busSubChange["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.BusinessExistingSubscriptionPlanChangedToBusinessUser:
                    //    Dictionary<string, string> busSubCahngeBus = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busSubCahngeBus["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", busSubCahngeBus["businessCompanyName"]);
                    //    xslArguments.Add("applicationName", busSubCahngeBus["applicationName"]);
                    //    xslArguments.Add("oldsubscriptionPlanName", busSubCahngeBus["oldsubscriptionPlanName"]);
                    //    xslArguments.Add("newSubscriptionPlanName", busSubCahngeBus["newSubscriptionPlanName"]);
                    //    xslArguments.Add("subDomain", busSubCahngeBus["subDomain"]);
                    //    xslArguments.Add("businessPortalURL", busSubCahngeBus["businessPortalURL"]);
                    //    xslArguments.Add("copyrightText", busSubCahngeBus["copyrightText"]);
                    //    xslArguments.Add("invitedUserEmailId", userDataRow.Email);
                    //    break;
                    case PublisherNotificationEvent.BusinessStatusChangedToPubUser:
                        Dictionary<string, string> busActivePubuser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", busActivePubuser["publisherCompanyName"]);
                        xslArguments.Add("businessCompanyName", busActivePubuser["businessCompanyName"]);
                        xslArguments.Add("publisherUserChanged", busActivePubuser["publisherUserChanged"]);
                        xslArguments.Add("status", busActivePubuser["status"]);
                        xslArguments.Add("administratorMessage", busActivePubuser["administratorMessage"]);
                        xslArguments.Add("copyrightText", busActivePubuser["copyrightText"]);
                        break;
                    //case PublisherNotificationEvent.BusinessActiveInactiveToBusinesUser:
                    //    Dictionary<string, string> busActiveBususer = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busActiveBususer["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", busActiveBususer["businessCompanyName"]);
                    //    xslArguments.Add("status", busActiveBususer["status"]);
                    //    xslArguments.Add("administratorMessage", busActiveBususer["administratorMessage"]);
                    //    xslArguments.Add("copyrightText", busActiveBususer["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.BusinessActiveInactiveToBusinessPartnerUser:
                    //    Dictionary<string, string> busActiveBusPartneruser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busActiveBusPartneruser["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", busActiveBusPartneruser["businessCompanyName"]);
                    //    xslArguments.Add("portalType", busActiveBusPartneruser["portalType"]);
                    //    xslArguments.Add("publisherCompanyName", busActiveBusPartneruser["publisherCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", busActiveBusPartneruser["businessPartnerCompanyName"]);
                    //    xslArguments.Add("status", busActiveBusPartneruser["status"]);
                    //    xslArguments.Add("administratorMessage", busActiveBusPartneruser["administratorMessage"]);
                    //    xslArguments.Add("copyrightText", busActiveBusPartneruser["copyrightText"]);
                    //    break;
                    case PublisherNotificationEvent.BusinessIsDeletedToPubUser:
                        Dictionary<string, string> busdeletePubuser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", busdeletePubuser["publisherCompanyName"]);
                        xslArguments.Add("publisherUserChanged", busdeletePubuser["publisherUserChanged"]);
                        xslArguments.Add("businessCompanyName", busdeletePubuser["businessCompanyName"]);
                        xslArguments.Add("copyrightText", busdeletePubuser["copyrightText"]);
                        break;
                    //case PublisherNotificationEvent.BusinessDeletedToBusinesUser:
                    //    Dictionary<string, string> busDeletedBusUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busDeletedBusUser["publisherCompanyName"]);
                    //    xslArguments.Add("platformCompanyName", busDeletedBusUser["platformCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", busDeletedBusUser["businessCompanyName"]);
                    //    xslArguments.Add("copyrightText", busDeletedBusUser["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.BusinessDeletedBusinessPartnerUser:
                    //    Dictionary<string, string> busDeletedBusPartnerUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", busDeletedBusPartnerUser["publisherCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", busDeletedBusPartnerUser["businessPartnerType"]);
                    //    xslArguments.Add("businessPartnerCompanyName", busDeletedBusPartnerUser["businessPartnerCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", busDeletedBusPartnerUser["businessCompanyName"]);
                    //    xslArguments.Add("portalType", busDeletedBusPartnerUser["portalType"]);
                    //    xslArguments.Add("copyrightText", busDeletedBusPartnerUser["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.PublisherUserSetPasswordSucess:
                    //    Dictionary<string, string> publisherSetPassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", publisherSetPassword["publisherCompanyName"]);
                    //    xslArguments.Add("publisherUserName", publisherSetPassword["publisherUserName"]);
                    //    xslArguments.Add("subDomain", publisherSetPassword["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", publisherSetPassword["invitedUserEmailId"]);
                    //    xslArguments.Add("publisherPortalURL", publisherSetPassword["publisherPortalURL"]);
                    //    xslArguments.Add("copyrightText", publisherSetPassword["copyrightText"]);
                    //    xslArguments.Add("platformCompanyName", publisherSetPassword["platformCompanyName"]);
                    //    break;
                    case PublisherNotificationEvent.PublisherUserOnBoard:
                        Dictionary<string, string> publisherOnBoard = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", publisherOnBoard["publisherCompanyName"]);
                        xslArguments.Add("platformCompanyName", publisherOnBoard["platformCompanyName"]);
                        xslArguments.Add("copyrightText", publisherOnBoard["copyrightText"]);
                        break;
                    //case PublisherNotificationEvent.PublisherUserMarkAsActiveInactive:
                    //    Dictionary<string, string> pubUserActive = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", pubUserActive["publisherCompanyName"]);
                    //    xslArguments.Add("publisherUserName", pubUserActive["publisherUserName"]);
                    //    xslArguments.Add("publisherUserChanged", pubUserActive["publisherUserChanged"]);
                    //    xslArguments.Add("status", pubUserActive["status"]);
                    //    xslArguments.Add("copyrightText", pubUserActive["copyrightText"]);
                    //    break;
                    case PublisherNotificationEvent.PublisherUserPermissionChanged:
                        Dictionary<string, string> pubUserPermissionChange = (Dictionary<string, string>)payload.EventInfo["EventData"];                        
                        xslArguments.Add("publisherName", pubUserPermissionChange["publisherName"]);
                        xslArguments.Add("platformCompanyName", pubUserPermissionChange["platformCompanyName"]);
                        xslArguments.Add("publisherUserName", pubUserPermissionChange["publisherUserName"]);
                        xslArguments.Add("publisherUserNameChange", pubUserPermissionChange["publisherUserNameChange"]);
                        xslArguments.Add("subDomain", pubUserPermissionChange["subDomain"]);
                        xslArguments.Add("copyright", pubUserPermissionChange["copyright"]);
                        break;
                    //case PublisherNotificationEvent.PublisherUserOnBoard:
                    //    Dictionary<string, string> pubUserOnboard = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("publisherCompanyName", pubUserOnboard["publisherCompanyName"]);
                    //    xslArguments.Add("newUserName", pubUserOnboard["newUserName"]);
                    //    xslArguments.Add("copyrightText", pubUserOnboard["copyrightText"]);
                    //    break;
                    case PublisherNotificationEvent.PublisherUserDeleted:
                        Dictionary<string, string> pubUserDeleted = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", pubUserDeleted["publisherCompanyName"]);
                        xslArguments.Add("publisherUserName", pubUserDeleted["publisherUserName"]);
                        xslArguments.Add("publisherUserChanged", pubUserDeleted["publisherUserChanged"]);
                        xslArguments.Add("copyrightText", pubUserDeleted["copyrightText"]);
                        break;
                    case PublisherNotificationEvent.ApplicationStatusChangedToPubUser:
                        Dictionary<string, string> pubUserstatusToPub = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherCompanyName", pubUserstatusToPub["publisherCompanyName"]);
                        xslArguments.Add("applicationName", pubUserstatusToPub["applicationName"]);
                        xslArguments.Add("publisherUserNameChanged", pubUserstatusToPub["publisherUserNameChanged"]);
                        xslArguments.Add("status", pubUserstatusToPub["status"]);
                        xslArguments.Add("administratorMessage", pubUserstatusToPub["administratorMessage"]);
                        xslArguments.Add("copyrightText", pubUserstatusToPub["copyrightText"]);
                        break;
                    //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessUser:
                    //    Dictionary<string, string> pubUserstatusTobus = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("businessCompanyName", pubUserstatusTobus["businessCompanyName"]);
                    //    xslArguments.Add("publisherCompanyName", pubUserstatusTobus["publisherCompanyName"]);
                    //    xslArguments.Add("status", pubUserstatusTobus["status"]);
                    //    xslArguments.Add("applicationName", pubUserstatusTobus["applicationName"]);
                    //    xslArguments.Add("administratorMessage", pubUserstatusTobus["administratorMessage"]);
                    //    xslArguments.Add("copyrightText", pubUserstatusTobus["copyrightText"]);
                    //    break;
                    //case PublisherNotificationEvent.PublisherApplicationStatusChangedToBusinessPartnerUser:
                    //    Dictionary<string, string> pubUserstatusTobusPartner = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("businessCompanyName", pubUserstatusTobusPartner["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", pubUserstatusTobusPartner["businessPartnerType"]);
                    //    xslArguments.Add("publisherCompanyName", pubUserstatusTobusPartner["publisherCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", pubUserstatusTobusPartner["businessPartnerCompanyName"]);
                    //    xslArguments.Add("status", pubUserstatusTobusPartner["status"]);
                    //    xslArguments.Add("applicationName", pubUserstatusTobusPartner["applicationName"]);
                    //    xslArguments.Add("administratorMessage", pubUserstatusTobusPartner["administratorMessage"]);
                    //    xslArguments.Add("copyrightText", pubUserstatusTobusPartner["copyrightText"]);
                    //    break;
                    case PublisherNotificationEvent.ContactUsToPlatformUser:
                        Dictionary<string, string> contactUsFormPlatform = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("osName", contactUsFormPlatform["osName"]);
                        xslArguments.Add("browserName", contactUsFormPlatform["browserName"]);
                        xslArguments.Add("appVersion", contactUsFormPlatform["appVersion"]);
                        xslArguments.Add("userName", contactUsFormPlatform["userName"]);
                        xslArguments.Add("userEmail", contactUsFormPlatform["userEmail"]);
                        xslArguments.Add("phoneNumber", contactUsFormPlatform["phoneNumber"]);
                        xslArguments.Add("portal", contactUsFormPlatform["portal"]);
                        xslArguments.Add("timeOfAction", contactUsFormPlatform["timeOfAction"]);
                        xslArguments.Add("copyrightText", contactUsFormPlatform["copyrightText"]);
                        xslArguments.Add("comments", contactUsFormPlatform["comments"]);
                        xslArguments.Add("accountName", contactUsFormPlatform["accountName"]);
                        xslArguments.Add("application", contactUsFormPlatform["application"]);
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetXMLArgumentsForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;
        }

        /// <summary>
        /// Generates email payload by the data given in notification payload and USerDataRow.
        /// </summary>
        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xslArguments) {
            EmailPayload emailPayload = new EmailPayload();
            try {
                //Set all common fields for all the deeplink 
                if(notificationPayload != null) {
                    // emailPayload.AppId= notificationPayload
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
                    emailPayload.XSLTArguments = xslArguments;

                    #region XSL Email Template

                    string xslFileContent = GetEmailXsltFile((PublisherNotificationEvent)notificationPayload.EventId, emailPayload.UserLanguage);
                    emailPayload.InMemoryXSLTemplate = true;
                    emailPayload.XSLTemplateContent = xslFileContent;

                    #endregion

                    #region  Email Deeplink
                    //ToDo:Nitin-Notification-This statement is not clear.DeeplinkPayload
                    emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);
                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                    #endregion
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PublisherNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());

                throw;
            }
            return emailPayload;
        }

        #endregion

        #region AS Notification

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForASNotification(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();
            // Map common xsl parameters
            xslArguments.Add(NotificationConstants.RecipientFullNameKey, userDataRow.FullName);
            xslArguments.Add(NotificationConstants.EventNumberKey, Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));
            Dictionary<string, string> eventInfo = payload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>;
            try {
                switch((PublisherNotificationEvent)payload.EventId) {
                    case PublisherNotificationEvent.PublisherUserPermissionChanged:
                        Dictionary<string, string> invoicePaymentEventData = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("publisherUserNameChange", eventInfo["publisherUserNameChange"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);                        
                        break;

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetXSLArgumentsForASNotification:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;

        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override ASNotificationPayload GetASNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, Dictionary<string, string> xslArguments) {
            ASNotificationPayload asNotificationPayload = new ASNotificationPayload();

            try {
                if(notificationPayload != null && (notificationPayload.SupportedDeliveryType & NotificationDeliveryType.ASNotification) == NotificationDeliveryType.ASNotification) {

                    Dictionary<string, string> eventData = (notificationPayload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>);

                    asNotificationPayload.TenantId = recepientDataRow.TenantId;
                    asNotificationPayload.RecepientUserId = recepientDataRow.TenantUserId;
                    asNotificationPayload.EventId = notificationPayload.EventId;
                    //  asNotificationPayload.AppId = Guid.Parse(notificationPayload.EventInfo[NotificationConstants.AppIdKey].ToString());
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

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((PublisherNotificationEvent)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetASNotificationPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return asNotificationPayload;
        }

        private string GetASNotificationXsltFile(PublisherNotificationEvent eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, "Publisher", templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(PublisherNotificationEvent eventId) {
            switch(eventId) {
                case PublisherNotificationEvent.PublisherUserPermissionChanged:
                    return "PublisherUserPermissionChanged.xslt";
                default:
                    return "";
            }
        }

        protected override ewApps.Core.NotificationService.NotificationLogOpDTO GenerateASNotificationPayload(NotificationPayload<NotificationRecipient> payload, NotificationRecipient recepientDataRow, DateTime deliveryTime, long syncRowId, int badgeCount, string XMLData) {
            throw new NotImplementedException();
        }

        #endregion

        #region Push Notification

        protected override Dictionary<string, string> GetXSLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
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

        #region Not Implemented Methods

        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
