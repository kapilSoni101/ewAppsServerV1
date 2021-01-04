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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using ewApps.Core.BaseService;
using ewApps.Core.Common;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationDeliveryType = ewApps.Core.NotificationService.NotificationDeliveryType;

namespace ewApps.AppPortal.DS {

    /// <summary>Support notification service implementing all the abstract methods of base notification service.</summary>
    public class BusinessNotificationService:NotificationService<NotificationRecipient>, IBusinessNotificationService {

        #region Local member

        IOptions<NotificationAppSettings> _appSettingsOption;
        IMapper _mapper;
        IBusinessNotificationReceipentDataService _businessNotificationReceipentDataService;

        #endregion Local member

        #region Constructor

        public BusinessNotificationService(IMapper mapper, IBusinessNotificationReceipentDataService supportNotificationReceipentDataService,
            IEmailService emailService, ISMSService smsService, IOptions<NotificationAppSettings> appSettingsOption, ILogger<NotificationService<NotificationRecipient>> loggerService)
                : base(loggerService, emailService, smsService) {
            _appSettingsOption = appSettingsOption;
            _mapper = mapper;
            _businessNotificationReceipentDataService = supportNotificationReceipentDataService;
            base._appSettings = _mapper.Map<NotificationAppSettings>(appSettingsOption.Value);
        }

        #endregion Constructor

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
                    switch((BusinessNotificationEventEnum)deeplinkPayload.EventId) {
                        case BusinessNotificationEventEnum.BusinessUserInvitation:
                            deeplinkPayload.DeeplinkInfoList = GetBusinessUserPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserInvitation);
                            break;
                        case BusinessNotificationEventEnum.BusinessPartnerUserInvitation:
                            deeplinkPayload.DeeplinkInfoList = GetBusinessPartnerPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerUserInvitation);
                            break;
                        case BusinessNotificationEventEnum.ForgotPasswordBusiness:
                            deeplinkPayload.DeeplinkInfoList = ForgotPasswordBusiness(notificationPayload, (long)BusinessNotificationEventEnum.ForgotPasswordBusiness);
                            break;
                        //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                        //    deeplinkPayload.DeeplinkInfoList = ForgotPasswordBusinessPartner(notificationPayload, (long)BusinessNotificationEventEnum.ForgotPasswordBusinessPartner);
                        //    break;
                        case BusinessNotificationEventEnum.BusinessPartnerNewMail:
                            deeplinkPayload.DeeplinkInfoList = GetBusinessPartnerPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerNewMail);
                            break;
                        case BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                            deeplinkPayload.DeeplinkInfo = GetBusinessNewUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite);
                            break;
                        case BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
                            deeplinkPayload.DeeplinkInfo = GetBusinessExistingUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite);
                            break;
                        case BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
                            deeplinkPayload.DeeplinkInfo = GetPrimaryPartnerNewEmailUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite);
                            break;
                        //case BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
                        //    deeplinkPayload.DeeplinkInfo = GetPrimaryPartnerExistingEmailUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite);
                        //    break;
                        //case BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId:
                        //    deeplinkPayload.DeeplinkInfo = GetOtherPartnerNewEmailUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId);
                        //    break;
                        //case BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId:
                        //    deeplinkPayload.DeeplinkInfo = GetOtherPartnerExistingEmailUserInviteDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId);
                        //    break;
                        case BusinessNotificationEventEnum.BusinessUserForgotPassword:
                            deeplinkPayload.DeeplinkInfo = GetBusinessUserForgotPasswordDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword);
                            break;
                            //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                            //    deeplinkPayload.DeeplinkInfo = GetBusinessUserForgotPasswordDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword);
                            //    break;

                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinkPayload;
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
                    emailPayload.AppType = 1;
                    // emailPayload.CustomData
                    emailPayload.EmailDeliverySubType = (int)EmailSubType.HTML;
                    emailPayload.EventXMLData = eventXMLData;

                    emailPayload.HasLinkError = hasLinkError;

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

                    emailPayload.UserLanguage = ewApps.AppPortal.Common.AppPortalConstants.DefaultLanguage;

                    emailPayload.XSLTArguments = xslArguments;

                    #region XSL Email Template

                    string xslFileContent = GetEmailXsltFile((BusinessNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
                    emailPayload.InMemoryXSLTemplate = true;
                    emailPayload.XSLTemplateContent = xslFileContent;

                    #endregion

                    #region  email Deeplink
                    //ToDo:Nitin-Notification-This statement is not clear.DeeplinkPayload
                    emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);
                    //TODO: Ishwar

                    if(notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists != null && notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Count > 0) {
                        DeepLinkResultSetList deepLinkResultSetList = notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Where(i => i.DeeplingResultKey == recepientRow.UserType.ToString()).First();
                        notificationPayload.DeeplinkResultSet.DeeplinkResults = deepLinkResultSetList.DeeplinkResults;
                        emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;
                    }
                    else {
                        emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;
                    }

                    #endregion
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {
            //string deeplinkJSON = deeplinkResultSet.DeeplinkResults["deeplink1"].DeeplinkJson;
            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
            return deeplinkresult;
        }

        /// <summary></summary>
        /// <param name="userDataRow"></param>
        /// <param name="payload"></param>
        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        /// <param name="recepientDataRow"></param>
        /// <param name="payload"></param>
        /// <param name="deliveryTime"></param>
        /// <param name="syncRowId"></param>
        /// <param name="badgeCount"></param>
        /// <param name="XMLData"></param>
        protected override Core.NotificationService.NotificationLogOpDTO GenerateASNotificationPayload(NotificationPayload<NotificationRecipient> payload, NotificationRecipient recepientDataRow, DateTime deliveryTime, long syncRowId, int badgeCount, string XMLData) {
            throw new NotImplementedException();
        }

        /// <summary></summary>
        /// <param name="userDataRow"></param>
        /// <param name="payload"></param>
        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <summary>Get recepiant list</summary>
        /// <param name="payload">Notification payload</param>
        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {

            Dictionary<string, string> eventData = payload.EventInfo["EventData"] as Dictionary<string, string>;

            // Get the required filters for getting the receipent list
            Guid tenantUserId = Guid.Empty;
            Guid tenantId = Guid.Empty;
            Guid appId = Guid.Empty;
            //long emailPreference = 0;
            Guid businessPartnerTenantId = Guid.Empty;

            if(eventData.ContainsKey("appuserid")) {
                tenantUserId = Guid.Parse(eventData["appuserid"]);
            }
            if(eventData.ContainsKey("tenantid")) {
                tenantId = Guid.Parse(eventData["tenantid"]);
            }
            if(eventData.ContainsKey("appid")) {
                appId = Guid.Parse(eventData["appid"]);
            }
            if(eventData.ContainsKey("businesspartnertenantid")) {
                businessPartnerTenantId = Guid.Parse(eventData["businesspartnertenantid"]);
            }


            try {
                Dictionary<string, string> eventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;
                switch((BusinessNotificationEventEnum)payload.EventId) {
                    case BusinessNotificationEventEnum.BusinessUserInvitation:
                        return _businessNotificationReceipentDataService.GetInvitedUser(new Guid(eventInfo["appId"]), new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appuserid"]));
                    case BusinessNotificationEventEnum.BusinessPartnerUserInvitation:
                        return _businessNotificationReceipentDataService.GetInvitedUser(new Guid(eventInfo["appId"]), new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appuserid"]));
                    case BusinessNotificationEventEnum.ForgotPasswordBusiness:
                        return _businessNotificationReceipentDataService.GetInvitedUser(new Guid(eventInfo["appId"]), new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appuserid"]));
                    //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                    //    return _businessNotificationReceipentDataService.GetInvitedUser(new Guid(eventInfo["appId"]), new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appuserid"]));
                    case BusinessNotificationEventEnum.BusinessPartnerNewMail:
                        return _businessNotificationReceipentDataService.GetInvitedUser(new Guid(eventInfo["appId"]), new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appuserid"]));

                    //case BusinessNotificationEventEnum.BusinessPrimaryUserSetPasswordSucess:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.NewBusinessOnBoard:
                    //    return _businessNotificationReceipentDataService.GetPublisherUserWithPermissionAndPrefrence(tenantId, appId, (long)BusinessNotificationEventEnum.NewBusinessOnBoard);
                    case BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                        return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    case BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
                        return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessUserSetPasswordSucess:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.NewBusinessUserOnBoard:
                    //    return _businessNotificationReceipentDataService.GetAllBusinessUserWithPrefence(tenantId, appId, (long)BusinessNotificationEventEnum.NewBusinessUserOnBoard);
                    //case BusinessNotificationEventEnum.ApplicationAssignedToBusinessUser:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessUser:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessUserMarkActiveInActive:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessUserPermissionChanged:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessUserDeleted:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessUser(tenantId, tenantUserId, appId);
                    case BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
                        return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessPartnerUser:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser:
                    //    return _businessNotificationReceipentDataService.GetAllBusinessPartnerUserWithPermissionAndPrefrence(tenantId, tenantUserId, businessPartnerTenantId, (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser);
                    //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser:
                    //    return _businessNotificationReceipentDataService.GetAllBusinessUserWithPermissionsAndPrefernces(tenantId, appId, (long)BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser);
                    //case BusinessNotificationEventEnum.BusinessPartnerDeletedToPartnerUser:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser:
                    //    return _businessNotificationReceipentDataService.GetAllBusinessUserWithPermissionsAndPrefernces(tenantId, appId, (long)BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser);
                    //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToPartnerUser:
                    //    return _businessNotificationReceipentDataService.GetInvitedBusinessPartnerUser(tenantId, tenantUserId, appId);
                    //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser:
                    //    return _businessNotificationReceipentDataService.GetAllBusinessUserWithPermissionsAndPrefernces(tenantId, appId, (long)BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser);
                    case BusinessNotificationEventEnum.BusinessUserForgotPassword:
                        return _businessNotificationReceipentDataService.GetForgotPasswordBusinessUser(tenantId, tenantUserId);
                    //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                    //    return _businessNotificationReceipentDataService.GetForgotPasswordBusinessUser(tenantId, tenantUserId);
                    default:
                        return null;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetRecipientList:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary></summary>
        /// <param name="payload"></param>
        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        /// <summary>Get email xml argument based on event identifier</summary>
        /// <param name="userDataRow"></param>
        /// <param name="payload"></param>
        protected override Dictionary<string, string> GetXSLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();

            #region Common Region

            xslArguments.Add("RecipientFullName", userDataRow.FullName);
            xslArguments.Add("EventNumber", Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));

            Dictionary<string, string> eventInfo = new Dictionary<string, string>();
            if(payload.EventInfo.ContainsKey("EventData")) {
                eventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;
            }

            #endregion

            try {
                switch((BusinessNotificationEventEnum)payload.EventId) {
                    case BusinessNotificationEventEnum.BusinessUserInvitation:
                        xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        xslArguments.Add("appName", eventInfo["appName"]);
                        xslArguments.Add("portalType", eventInfo["portalType"]);
                        xslArguments.Add("hostName", eventInfo["hostName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("inviteeCompany", eventInfo["inviteeCompany"]);
                        break;
                    case BusinessNotificationEventEnum.BusinessPartnerUserInvitation:
                        xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        xslArguments.Add("appName", eventInfo["appName"]);
                        xslArguments.Add("portalType", eventInfo["portalType"]);
                        xslArguments.Add("hostName", eventInfo["hostName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("inviteeCompany", eventInfo["inviteeCompany"]);
                        break;
                    case BusinessNotificationEventEnum.ForgotPasswordBusiness:
                        xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        xslArguments.Add("applicationName", eventInfo["appName"]);
                        xslArguments.Add("portalUserName", eventInfo["portalUserName"]);
                        break;
                    //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                    //    xslArguments.Add("publisherName", eventInfo["publisherName"]);
                    //    xslArguments.Add("applicationName", eventInfo["appName"]);
                    //    xslArguments.Add("portalUserName", eventInfo["portalUserName"]);
                    //    break;
                    case BusinessNotificationEventEnum.BusinessPartnerNewMail:
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

                    //case BusinessNotificationEventEnum.BusinessPrimaryUserSetPasswordSucess:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="invitedUserEmailID"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.NewBusinessOnBoard:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    case BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                        xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                        xslArguments.Add("businessInvitingUserName", eventInfo["businessInvitingUserName"]);
                        xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        break;
                    case BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
                        xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                        xslArguments.Add("businessInvitingUserName", eventInfo["businessInvitingUserName"]);
                        xslArguments.Add("emailID", eventInfo["emailID"]);
                        xslArguments.Add("portalName", eventInfo["portalName"]);
                        xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                        xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        break;
                    //case BusinessNotificationEventEnum.BusinessUserSetPasswordSucess:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="invitedUserEmailID"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.NewBusinessUserOnBoard:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="userName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("userName", eventInfo["userName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ApplicationAssignedToBusinessUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);                        
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessUserMarkActiveInActive:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="status"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("status", eventInfo["status"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessUserPermissionChanged:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessUserDeleted:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessUserName"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    case BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
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
                    //case BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="invitedUserName"/>
                    //    //<xsl:param name="emailID"/>
                    //    //<xsl:param name="portalName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="invitedUserEmailId"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                    //    xslArguments.Add("emailID", eventInfo["emailID"]);
                    //    xslArguments.Add("portalName", eventInfo["portalName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);                        
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="invitedUserName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="invitedUserEmailId"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="invitedUserName"/>
                    //    //<xsl:param name="emailID"/>
                    //    //<xsl:param name="portalName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="subDomain"/>
                    //    //<xsl:param name="invitedUserEmailId"/>
                    //    //<xsl:param name="portalURL"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                    //    xslArguments.Add("emailID", eventInfo["emailID"]);
                    //    xslArguments.Add("portalName", eventInfo["portalName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                    //    xslArguments.Add("invitedUserEmailId", eventInfo["invitedUserEmailId"]);
                    //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessPartnerUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessPartnerUserName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerUserName", eventInfo["businessPartnerUserName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="applicationName"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessPartnerDeletedToPartnerUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToPartnerUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessPartnerUserName"/>
                    //    //<xsl:param name="businessPartnerType"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessPartnerUserName", eventInfo["businessPartnerUserName"]);
                    //    xslArguments.Add("businessPartnerType", eventInfo["businessPartnerType"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser:
                    //    //<xsl:param name="publisherCompanyName"/>
                    //    //<xsl:param name="businessPartnerCompanyName"/>
                    //    //<xsl:param name="businessPartnerUserName"/>
                    //    //<xsl:param name="businessCompanyName"/>
                    //    //<xsl:param name="businessUserNameChange"/>
                    //    //<xsl:param name="copyrightText"/>
                    //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                    //    xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                    //    xslArguments.Add("businessPartnerUserName", eventInfo["businessPartnerUserName"]);
                    //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                    //    xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                    //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                    //    break;
                    case BusinessNotificationEventEnum.BusinessUserForgotPassword:
                        //<xsl:param name="publisherCompanyName"/>
                        //<xsl:param name="businessUserName"/>
                        //<xsl:param name="businessUserID"/>
                        //<xsl:param name="copyrightText"/>
                        xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("businessUserID", eventInfo["businessUserID"]);
                        xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);

                        break;
                        //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                        //    //<xsl:param name="publisherCompanyName"/>
                        //    //<xsl:param name="businessUserName"/>
                        //    //<xsl:param name="businessUserID"/>
                        //    //<xsl:param name="copyrightText"/>
                        //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        //    xslArguments.Add("businessUserID", eventInfo["businessUserID"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    break;
                        //default:
                        //    break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetXMLArgumentsForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;
        }

        /// <summary></summary>
        /// <param name="userDataRow"></param>
        /// <param name="payload"></param>
        protected override Dictionary<string, string> GetXSLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        /// <summary>Validation data about notification</summary>
        /// <param name="moduleId"></param>
        /// <param name="eventId"></param>
        /// <param name="eventInfo"></param>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            try {
                bool valid = true;
                BusinessNotificationEventEnum parsedEvent;
                if(moduleId <= 0) {
                    valid = false;
                }
                if(Enum.TryParse<BusinessNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.ValidateNotificationData:-");
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
                case (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                case (long)BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
                case (long)BusinessNotificationEventEnum.BusinessUserForgotPassword:
                    return _appSettingsOption.Value.BusinessBranchApiKey;
                case (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
                    //case (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
                    //case (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId:
                    //case (long)BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId:
                    //case (long)BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                    return _appSettingsOption.Value.CustomerBranchApiKey;
                //if(appKey.ToLower() == AppKeyEnum.pay.ToString().ToLower()) {
                //    return _appSettingsOption.Value.PaymentBranchApiKey;
                //}
                //else if(appKey.ToLower() == AppKeyEnum.cust.ToString().ToLower()) {
                //    return _appSettingsOption.Value.CustomerBranchApiKey;
                //}
                //else {
                //    return "";
                //}
                default:
                    return "";
            }
        }

        ///// <summary>Get branch key on the basis of event type</summary>
        ///// <param name="eventId">Event identitfier</param>
        //protected override object GetBranchKey(object eventId) {
        //    long eventLong = long.Parse(eventId.ToString());
        //    switch(eventLong) {
        //        case (long)BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
        //        case (long)BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
        //            return _appSettingsOption.Value.BusinessBranchApiKey;
        //        case (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
        //        case (long)BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
        //            return _appSettingsOption.Value.PaymentBranchApiKey;
        //        default:
        //            return "";
        //    }
        //}

        /// <summary>Get branch key on the basis of decision parameters.</summary>
        /// <param name="decisionParameters">Set of information to make decision about branch key, deeplink actions and parameters.</param>
        /// <returns>Returns a portal wise branch key, deeplink actions and parameters in form of <see cref="Dictionary{System.Int32, TValue}"/> Dictionary&lt;int,object&gt;. Key is portal type and value is of type <see cref=""/> </returns>
        protected override object GetBranchKey(object decisionParameters) {

            Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = new Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>();
            try {
                if(decisionParameters != null) {
                    // Get decision paramerts from object.
                    Dictionary<string, object> decisionDictionary = decisionParameters as Dictionary<string, object>;
                    long eventId = long.Parse(decisionDictionary["eventid"].ToString());
                    string appKey = decisionDictionary["appkey"].ToString();

                    // ToDo: Nitin: Review use of UserType enum dictionary as UserType is part of application and support level is part of support module.
                    switch(eventId) {
                        case (long)BusinessNotificationEventEnum.BusinessUserInvitation: {
                                DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                                model.DeepLinkAction = "businessotheruser,invite";
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
                                model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Business, model);
                                break;
                            }
                        case (long)BusinessNotificationEventEnum.BusinessPartnerUserInvitation: {
                                DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                                model.DeepLinkAction = "businesspartnerprimaryuser,invite";
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
                                model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Customer, model);
                                break;
                            }
                        case (long)BusinessNotificationEventEnum.ForgotPasswordBusiness: {
                                DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                                model.DeepLinkAction = "businessuser,forgotpassword";
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
                                model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Business, model);
                                break;
                            }
                        //case (long)BusinessNotificationEventEnum.ForgotPasswordBusinessPartner: {
                        //        DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                        //        model.DeepLinkAction = "businessuser,forgotpassword";
                        //        List<string> parameterList = new List<string>();
                        //        parameterList.Add("tenantid");
                        //        parameterList.Add("appuserid");
                        //        parameterList.Add("identityserveruserid");
                        //        parameterList.Add("loginEmail");
                        //        parameterList.Add("usertype");
                        //        parameterList.Add("code");
                        //        parameterList.Add("tokeninfoid");
                        //        parameterList.Add("subDomain");
                        //        model.DeeplinkParametersList = parameterList;
                        //        //if (appKey == AppKeyEnum.biz.ToString()) {
                        //        //  model.BranchKey = _appSettingsOption.Value.Notification.BusinessBranchApiKey;
                        //        //}
                        //        //else if (appKey == AppKeyEnum.pay.ToString()) {
                        //        //  model.BranchKey = _appSettingsOption.Value.Notification.PaymentBranchApiKey;
                        //        //}
                        //        model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                        //        branchkeyList.Add((int)UserTypeEnum.Customer, model);
                        //        break;
                        //    }
                        case (long)BusinessNotificationEventEnum.BusinessPartnerNewMail: {
                                DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                                model.DeepLinkAction = "businesspartnerprimaryuser,invite";
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
                                model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Customer, model);
                                break;
                            }
                        default:
                            branchkeyList.Add(0, new DeeplinkActionParameterAndBranchKeyDTO { });
                            break;
                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return branchkeyList;
        }

        #region Private methods

        #region XSLT

        /// <summary>Gets the email XSLT file.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="regionLanguage">The region language.</param>
        /// <returns></returns>
        private string GetEmailXsltFile(BusinessNotificationEventEnum eventId, string regionLanguage) {
            //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
            //if (CacheHelper.IsInCache(cacheKey)) {
            //  return CacheHelper.GetData<string>(cacheKey); ;
            //}
            //else {
            //Engg: should define in Config file.
            // string xsltResourcePath = "/resourcePath/";
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                xsltTextFile = FileHelper.ReadFileAsText(Path.Combine(Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage), templateFileName));

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetEmailXsltFile:-");
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

        /// <summary>Gets the email file name .</summary>
        /// <param name="eventNumber">The event identifier.</param>
        /// <returns>file name of the xslt file. </returns>
        private string GetXSLTemplateFileName(BusinessNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case BusinessNotificationEventEnum.BusinessUserInvitation:
                    return "BusinessPrimaryUserInvitationEmail.xslt";
                case BusinessNotificationEventEnum.BusinessPartnerUserInvitation:
                    return "BusinessPrimaryUserInvitationEmail.xslt";
                case BusinessNotificationEventEnum.ForgotPasswordBusiness:
                    return "BusinessForgotPasswordEmail.xslt";
                //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                //    return "BusinessForgotPasswordEmail.xslt";
                case BusinessNotificationEventEnum.BusinessPartnerNewMail:
                    return "BusinessPartnerPrimaryUserNewEmailIdInvited.xslt";
                //case BusinessNotificationEventEnum.BusinessPrimaryUserSetPasswordSucess:
                //    return "BusinessUserSetsPasswordSuccefully.xslt";
                //case BusinessNotificationEventEnum.NewBusinessOnBoard:
                //    return "NewBusinessOnBoard.xslt";
                case BusinessNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                    return "BusinessUserWithNewEmailIdInvite.xslt";
                case BusinessNotificationEventEnum.BusinessUserWithExistingEmailIdInvite:
                    return "BusinessUserWithExistingEmailIdInvite.xslt";
                //case BusinessNotificationEventEnum.BusinessUserSetPasswordSucess:
                //    return "BusinessUserSetsPasswordSuccefully.xslt";
                //case BusinessNotificationEventEnum.NewBusinessUserOnBoard:
                //    return "NewBusinessUserOnBoard.xslt";
                //case BusinessNotificationEventEnum.ApplicationAssignedToBusinessUser:
                //    return "ApplicationAssignedToBusinessUser.xslt";
                //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessUser:
                //    return "ApplicationDeAssignedToBusinessUser.xslt";
                //case BusinessNotificationEventEnum.BusinessUserMarkActiveInActive:
                //    return "BusinessUserMarkActiveInActive.xslt";
                //case BusinessNotificationEventEnum.BusinessUserPermissionChanged:
                //    return "BusinessUserPermissionChanged.xslt";
                //case BusinessNotificationEventEnum.BusinessUserDeleted:
                //    return "BusinessUserDeleted.xslt";
                case BusinessNotificationEventEnum.BusinessPartnerPrimaryNewUserInvite:
                    return "BusinessPartnerPrimaryNewUserInvite.xslt";
                //case BusinessNotificationEventEnum.BusinessPartnerPrimaryExistingUserInvite:
                //    return "BusinessPartnerPrimaryExistingUserInvite.xslt";
                //case BusinessNotificationEventEnum.BusinessPartnerOtherUserNewEmailId:
                //    return "BusinessPartnerOtherUserNewEmailIdInvited.xslt";
                //case BusinessNotificationEventEnum.BusinessPartnerOtherUserExistingEmailId:
                //    return "BusinessPartnerOtherUserExistingEmailIdInvited.xslt";
                //case BusinessNotificationEventEnum.ApplicationDeAssignedToBusinessPartnerUser:
                //    return "ApplicationDeAssignedToBusinessPartnerUser.xslt";
                //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessPartnerUser:
                //    return "ApplicationRemovedForCustomerToBusinessPartnerUser.xslt";
                //case BusinessNotificationEventEnum.ApplicationRemovedForCustomerToBusinessUser:
                //    return "ApplicationRemovedForCustomerToBusinessUser.xslt";
                //case BusinessNotificationEventEnum.BusinessPartnerDeletedToPartnerUser:
                //    return "BusinessPartnerDeletedToPartnerUser.xslt";
                //case BusinessNotificationEventEnum.BusinessPartnerDeletedToBusinessUser:
                //    return "BusinessPartnerDeletedToBusinessUser.xslt";
                //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToPartnerUser:
                //    return "ExistingBusinessPartnerUserDeletedToPartnerUser.xslt";
                //case BusinessNotificationEventEnum.ExistingBusinessPartnerUserDeletedToBusinessUser:
                //    return "ExistingBusinessPartnerUserDeletedToBusinessUser.xslt";
                case BusinessNotificationEventEnum.BusinessUserForgotPassword:
                    return "BusinessUserForgotPassword.xslt";
                //case BusinessNotificationEventEnum.ForgotPasswordBusinessPartner:
                //    return "BusinessUserForgotPassword.xslt";
                default:
                    return "";
            }
        }

        #endregion XSLT

        #region deeplink

        private DeeplinkPayload SetDeeplinkPayloadForEmail(string linkKey, Guid tenantId, Guid notificationId, Guid userId) {
            try {
                DeeplinkPayload dPayload = new DeeplinkPayload();
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
                return dPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetBusinessUserPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                    Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        decisionDictionary.Add("eventid", eventId);
                        decisionDictionary.Add("appkey", eventData["appkey"]);
                    }

                    // Get barnch key list.
                    Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                    // Create deeplink af all the branch keys.
                    foreach(int item in branchkeyList.Keys) {
                        Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                        Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                        Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                        //Set fix information in deeplink
                        string action = branchkeyList[item].DeepLinkAction; //"businesspartneruser,addticket";
                        invitationDeeplinkInfo.Add("Actions", action);
                        // ToDo: Nitin-Notification-Try to add params herre.
                        //invitationDeeplinkInfo.Add("Params", action);
                        invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                        // Set Dynamic info from Notification payload.
                        foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                            invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                        }

                        invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                        deeplinks.Add("businessUserInviteSetPwdLink", invitationDeeplinkInfo);
                        DeeplinkInfo model = new DeeplinkInfo();
                        model.DeeplinkInfoList = deeplinks;
                        model.DeeplinkKey = item.ToString();
                        list.Add(model);
                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBusinessUserPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return list;
        }


        private List<DeeplinkInfo> GetBusinessPartnerPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                    Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        decisionDictionary.Add("eventid", eventId);
                        decisionDictionary.Add("appkey", eventData["appkey"]);
                    }

                    // Get barnch key list.
                    Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                    // Create deeplink af all the branch keys.
                    foreach(int item in branchkeyList.Keys) {
                        Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                        Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                        Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                        //Set fix information in deeplink
                        string action = branchkeyList[item].DeepLinkAction; //"businesspartneruser,addticket";
                        invitationDeeplinkInfo.Add("Actions", action);
                        // ToDo: Nitin-Notification-Try to add params herre.
                        //invitationDeeplinkInfo.Add("Params", action);
                        invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                        // Set Dynamic info from Notification payload.
                        foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                            invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                        }

                        invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                        deeplinks.Add("businessUserInviteSetPwdLink", invitationDeeplinkInfo);
                        DeeplinkInfo model = new DeeplinkInfo();
                        model.DeeplinkInfoList = deeplinks;
                        model.DeeplinkKey = item.ToString();
                        list.Add(model);
                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBusinessPartnerPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return list;
        }


        private List<DeeplinkInfo> ForgotPasswordBusiness(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                    Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        decisionDictionary.Add("eventid", eventId);
                        decisionDictionary.Add("appkey", eventData["appkey"]);
                    }

                    // Get barnch key list.
                    Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                    // Create deeplink af all the branch keys.
                    foreach(int item in branchkeyList.Keys) {
                        Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                        Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                        Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                        //Set fix information in deeplink
                        string action = branchkeyList[item].DeepLinkAction; //"businesspartneruser,addticket";
                        invitationDeeplinkInfo.Add("Actions", action);
                        // ToDo: Nitin-Notification-Try to add params herre.
                        //invitationDeeplinkInfo.Add("Params", action);
                        invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                        // Set Dynamic info from Notification payload.
                        foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                            invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                        }

                        invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                        deeplinks.Add("businessForgotPasswordLink", invitationDeeplinkInfo);
                        DeeplinkInfo model = new DeeplinkInfo();
                        model.DeeplinkInfoList = deeplinks;
                        model.DeeplinkKey = item.ToString();
                        list.Add(model);
                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.ForgotPasswordBusiness:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return list;
        }

        private List<DeeplinkInfo> ForgotPasswordBusinessPartner(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                    Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        decisionDictionary.Add("eventid", eventId);
                        decisionDictionary.Add("appkey", eventData["appkey"]);
                    }

                    // Get barnch key list.
                    Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                    // Create deeplink af all the branch keys.
                    foreach(int item in branchkeyList.Keys) {
                        Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                        Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                        Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                        //Set fix information in deeplink
                        string action = branchkeyList[item].DeepLinkAction; //"businesspartneruser,addticket";
                        invitationDeeplinkInfo.Add("Actions", action);
                        // ToDo: Nitin-Notification-Try to add params herre.
                        //invitationDeeplinkInfo.Add("Params", action);
                        invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                        // Set Dynamic info from Notification payload.
                        foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                            invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                        }

                        invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                        deeplinks.Add("businessForgotPasswordLink", invitationDeeplinkInfo);
                        DeeplinkInfo model = new DeeplinkInfo();
                        model.DeeplinkInfoList = deeplinks;
                        model.DeeplinkKey = item.ToString();
                        list.Add(model);
                    }
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.ForgotPasswordBusinessPartner:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return list;
        }

        #region New Deeplink methods

        private Dictionary<string, Dictionary<string, string>> GetBusinessNewUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
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
                    deeplinks.Add("businessUserInviteSetPwd", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBusinessNewUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetBusinessExistingUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
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
                    deeplinks.Add("businessUserInviteSetPwd", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBusinessExistingUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPrimaryPartnerNewEmailUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businesspartnerprimaryuser,invite,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    string appKey = string.Empty;
                    if(notificationPayload.EventInfo.ContainsKey("EventData")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        appKey = eventData["appkey"];
                    }
                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());
                    decisonData.Add(appKey);

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
                    // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetPrimaryPartnerUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetPrimaryPartnerExistingEmailUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businesspartnerprimaryuser,invite,existing";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    string appKey = string.Empty;
                    if(notificationPayload.EventInfo.ContainsKey("EventData")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        appKey = eventData["appkey"];
                    }
                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());
                    decisonData.Add(appKey);

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
                    // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    deeplinks.Add("domainURL", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetPrimaryPartnerUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetOtherPartnerNewEmailUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businesspartnerprimaryuser,invite,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    string appKey = string.Empty;
                    if(notificationPayload.EventInfo.ContainsKey("EventData")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        appKey = eventData["appkey"];
                    }
                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());
                    decisonData.Add(appKey);

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
                    // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetPrimaryPartnerUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetOtherPartnerExistingEmailUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businesspartnerprimaryuser,invite,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    string appKey = string.Empty;
                    if(notificationPayload.EventInfo.ContainsKey("EventData")) {
                        Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                        appKey = eventData["appkey"];
                    }
                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());
                    decisonData.Add(appKey);

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
                    // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    deeplinks.Add("domainURL", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetPrimaryPartnerUserInviteDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }

        private Dictionary<string, Dictionary<string, string>> GetBusinessUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businessuser,forgotpassword";
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
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessNotificationService.GetBusinessUserForgotPasswordDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }



        #endregion New Deeplink methods

        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {
            throw new NotImplementedException();
        }

        protected override Dictionary<string, string> GetXSLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #endregion deeplink

        #endregion Private methods

    }
}
