/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ewApps.Core.BaseService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {

    /// <summary>Support notification service implementing all the abstract methods of base notification service.</summary>
    public class SupportNotificationService:NotificationService<NotificationRecipient>, ISupportNotificationService {

        #region Local member

        IOptions<NotificationAppSettings> _appSettingsOption;
        ISupportNotificationReceipentDataService _supportNotificationReceipentDataService;

        #endregion Local member

        #region Constructor

        /// <summary>
        /// Initilize local members.
        /// </summary>
        public SupportNotificationService(ISupportNotificationReceipentDataService supportNotificationReceipentDataService, IEmailService emailService, ISMSService smsService, IOptions<NotificationAppSettings> appSettingsOption, ILogger<NotificationService<NotificationRecipient>> loggerService) : base(loggerService, emailService, smsService) {
            _appSettingsOption = appSettingsOption;
            _supportNotificationReceipentDataService = supportNotificationReceipentDataService;
        }

        #endregion Constructor


        /// <summary>
        /// Generates Deeplink payload by the data given in notification payload.
        /// </summary>
        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {

            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();

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
                switch((SupportNotificationEventEnum)deeplinkPayload.EventId) {
                    case SupportNotificationEventEnum.AddLevel1Ticket:
                        deeplinkPayload.DeeplinkInfoList = GetLevel1AddDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.AddLevel1Ticket);
                        break;
                    case SupportNotificationEventEnum.AddLevel2Ticket:
                        deeplinkPayload.DeeplinkInfoList = GetLevel2AddDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.AddLevel2Ticket);
                        break;
                    case SupportNotificationEventEnum.TicketIsAssigned:
                        deeplinkPayload.DeeplinkInfoList = GetCustomerTicketForwardeddDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketIsAssigned);
                        break;
                    case SupportNotificationEventEnum.TicketStatusChanged:
                        deeplinkPayload.DeeplinkInfoList = GetTicketStatusChangedDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketStatusChanged);
                        // deeplinkPayload.DeeplinkInfo = GetTicketStatusChangedDeeplinkPayload(notificationPayload,(long)SupportNotificationEventEnum.TicketStatusChanged);
                        break;
                    case SupportNotificationEventEnum.TicketReassigned:
                        deeplinkPayload.DeeplinkInfoList = GetTicketReassingedDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketReassigned);
                        break;
                    case SupportNotificationEventEnum.TicketPriorityChanged:
                        deeplinkPayload.DeeplinkInfoList = GetTicketPriorityChangedDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketPriorityChanged);
                        break;
                    case SupportNotificationEventEnum.TicketNewCommentAdded:
                        deeplinkPayload.DeeplinkInfoList = GetCommentAddedToTicketDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketNewCommentAdded);
                        break;
                    case SupportNotificationEventEnum.TicketNewAttachmentAdded:
                        deeplinkPayload.DeeplinkInfoList = GetAttachmentAddedToTicketDeeplinkPayload(notificationPayload, (long)SupportNotificationEventEnum.TicketNewAttachmentAdded);
                        break;
                }
            }
            return deeplinkPayload;
        }

        /// <summary>
        /// Generates Push payload by the data given in notification payload and USerDataRow.
        /// </summary>
        /// <param name="notificationPayload"></param>
        /// <param name="recepientRow"></param>
        /// <param name="eventXMLData"></param>
        /// <param name="hasLinkError"></param>
        /// <param name="xmlArguments"></param>
        /// <returns></returns>
        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {

            EmailPayload emailPayload = new EmailPayload();

            //Set all common fields for all the deeplink 
            if(notificationPayload != null) {
                emailPayload.TenantId = new Guid(Convert.ToString(recepientRow.TenantId));
                emailPayload.RecepientUserId = new Guid(Convert.ToString(recepientRow.TenantUserId));
                emailPayload.HasLinkError = hasLinkError;
                string language = string.Empty;

                if(notificationPayload.LoggedinUserId == emailPayload.RecepientUserId) {
                    emailPayload.IsLoggedInUser = true;
                }
                else {
                    emailPayload.IsLoggedInUser = false;
                }

                // Get user's local region language.
                if(string.IsNullOrEmpty(recepientRow.RegionLanguage)) {
                    language = "en";
                }
                else {
                    language = Convert.ToString(recepientRow.RegionLanguage);
                }
                emailPayload.UserLanguage = language;
                emailPayload.UserEmailAddress = Convert.ToString(recepientRow.Email);
                emailPayload.EventXMLData = eventXMLData;
                emailPayload.XSLTArguments = xmlArguments;
                //emailPayload.AppType = (int)CoreEntityTypeEnum.App;
                emailPayload.AppType = 0;
                emailPayload.NotificationInfo = new Dictionary<string, string>();
                emailPayload.NotificationInfo.Add("TrackingId", notificationPayload.TrackingId.ToString());
                emailPayload.NotificationInfo.Add("LinkNotificationId", notificationPayload.LinkedNotificationId.ToString());

                #region XSL Email Template

                string xslFileContent = GetEmailXsltFile((SupportNotificationEventEnum)notificationPayload.EventId, language);
                emailPayload.InMemoryXSLTemplate = true;
                emailPayload.XSLTemplateContent = xslFileContent;

                #endregion

                #region  email Deeplink
                //ToDo:Nitin-Notification-This statement is not clear.DeeplinkPayload
                emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);
                //TODO: Ishwar

                DeepLinkResultSetList deepLinkResultSetList = notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Where(i => i.DeeplingResultKey == recepientRow.UserType.ToString()).First();
                notificationPayload.DeeplinkResultSet.DeeplinkResults = deepLinkResultSetList.DeeplinkResults;
                emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                #endregion
            }
            return emailPayload;
        }

        /// <summary></summary>
        /// <param name="notificationPayload"></param>
        /// <param name="deeplinkResultSet"></param>
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
            Dictionary<string, string> eventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;
            switch((SupportNotificationEventEnum)payload.EventId) {
                case SupportNotificationEventEnum.AddLevel1Ticket:
                    return _supportNotificationReceipentDataService.PartnerAddTicketNotificationRecepientList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]));
                case SupportNotificationEventEnum.AddLevel2Ticket:
                    return _supportNotificationReceipentDataService.BusinessAddTicketNotificationRecepientList();
                case SupportNotificationEventEnum.TicketIsAssigned:
                    return _supportNotificationReceipentDataService.CustomerTicketForwardedToPublisherNotificationReceipentList();
                case SupportNotificationEventEnum.TicketStatusChanged:
                    return _supportNotificationReceipentDataService.TicketStatusChangedNotificationReceipentList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]), new Guid(eventInfo["ownerappuserid"]), bool.Parse(eventInfo["ispublisherassinged"]), int.Parse(eventInfo["supportlevel"]));
                case SupportNotificationEventEnum.TicketReassigned:
                    return _supportNotificationReceipentDataService.TicketReassingedNotificationReceipentList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]), new Guid(eventInfo["ownerappuserid"]), bool.Parse(eventInfo["ispublisherassinged"]), int.Parse(eventInfo["supportlevel"]));
                case SupportNotificationEventEnum.TicketPriorityChanged:
                    return _supportNotificationReceipentDataService.TicketPriorityChangedNotificationReceipentList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]), new Guid(eventInfo["ownerappuserid"]), bool.Parse(eventInfo["ispublisherassinged"]), int.Parse(eventInfo["supportlevel"]));
                case SupportNotificationEventEnum.TicketNewCommentAdded:
                    return _supportNotificationReceipentDataService.TicketNewCommentAddedNotificationReceipentList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]), new Guid(eventInfo["ownerappuserid"]), bool.Parse(eventInfo["ispublisherassinged"]), int.Parse(eventInfo["supportlevel"]));
                case SupportNotificationEventEnum.TicketNewAttachmentAdded:
                    return _supportNotificationReceipentDataService.TicketNewAttachmentAddedNotificationReceipentList(new Guid(eventInfo["tenantid"]), new Guid(eventInfo["appId"]), new Guid(eventInfo["ownerappuserid"]), bool.Parse(eventInfo["ispublisherassinged"]), int.Parse(eventInfo["supportlevel"]));
                default:
                    return null;
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
            #endregion

            switch((SupportNotificationEventEnum)payload.EventId) {
                case SupportNotificationEventEnum.AddLevel1Ticket:
                    Dictionary<string, string> addLevel1SupportTicket = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", addLevel1SupportTicket["publisherName"]);
                    xslArguments.Add("appName", addLevel1SupportTicket["appName"]);
                    xslArguments.Add("customerName", addLevel1SupportTicket["customerName"]);
                    xslArguments.Add("ticketId", addLevel1SupportTicket["ticketId"]);
                    xslArguments.Add("companyName", addLevel1SupportTicket["companyName"]);
                    xslArguments.Add("createdBy", addLevel1SupportTicket["createdBy"]);
                    xslArguments.Add("createdOn", addLevel1SupportTicket["createdOn"]);
                    xslArguments.Add("customerId", addLevel1SupportTicket["customerId"]);
                    xslArguments.Add("title", addLevel1SupportTicket["title"]);
                    xslArguments.Add("description", addLevel1SupportTicket["description"]);
                    xslArguments.Add("priority", addLevel1SupportTicket["priority"]);
                    xslArguments.Add("status", addLevel1SupportTicket["status"]);
                    xslArguments.Add("comment", addLevel1SupportTicket["comment"]);
                    xslArguments.Add("companyid", addLevel1SupportTicket["companyid"]);
                    break;
                case SupportNotificationEventEnum.AddLevel2Ticket:
                    Dictionary<string, string> addLevel2SupportTicket = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", addLevel2SupportTicket["publisherName"]);
                    xslArguments.Add("appName", addLevel2SupportTicket["appName"]);
                    xslArguments.Add("ticketId", addLevel2SupportTicket["ticketId"]);
                    xslArguments.Add("companyName", addLevel2SupportTicket["companyName"]);
                    xslArguments.Add("createdBy", addLevel2SupportTicket["createdBy"]);
                    xslArguments.Add("createdOn", addLevel2SupportTicket["createdOn"]);
                    xslArguments.Add("customerId", addLevel2SupportTicket["customerId"]);
                    xslArguments.Add("title", addLevel2SupportTicket["title"]);
                    xslArguments.Add("description", addLevel2SupportTicket["description"]);
                    xslArguments.Add("priority", addLevel2SupportTicket["priority"]);
                    xslArguments.Add("status", addLevel2SupportTicket["status"]);
                    xslArguments.Add("comment", addLevel2SupportTicket["comment"]);
                    xslArguments.Add("tenantid", addLevel2SupportTicket["tenantid"]);
                    break;
                case SupportNotificationEventEnum.TicketIsAssigned:
                    Dictionary<string, string> ticketIsAssigned = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", ticketIsAssigned["publisherName"]);
                    xslArguments.Add("appName", ticketIsAssigned["appName"]);
                    xslArguments.Add("customerName", ticketIsAssigned["customerName"]);
                    xslArguments.Add("customerId", ticketIsAssigned["customerId"]);
                    xslArguments.Add("ticketId", ticketIsAssigned["ticketId"]);
                    xslArguments.Add("companyName", ticketIsAssigned["companyName"]);
                    xslArguments.Add("companyId", ticketIsAssigned["companyId"]);
                    xslArguments.Add("createdBy", ticketIsAssigned["createdBy"]);
                    xslArguments.Add("createdOn", ticketIsAssigned["createdOn"]);
                    xslArguments.Add("title", ticketIsAssigned["title"]);
                    xslArguments.Add("description", ticketIsAssigned["description"]);
                    xslArguments.Add("priority", ticketIsAssigned["priority"]);
                    xslArguments.Add("status", ticketIsAssigned["status"]);
                    xslArguments.Add("comment", ticketIsAssigned["comment"]);
                    break;
                case SupportNotificationEventEnum.TicketStatusChanged:
                    Dictionary<string, string> ticketStatusChanged = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", ticketStatusChanged["publisherName"]);
                    xslArguments.Add("appName", ticketStatusChanged["appName"]);
                    xslArguments.Add("customerName", ticketStatusChanged["customerName"]);
                    xslArguments.Add("customerId", ticketStatusChanged["customerId"]);
                    xslArguments.Add("ticketId", ticketStatusChanged["ticketId"]);
                    xslArguments.Add("companyName", ticketStatusChanged["companyName"]);
                    xslArguments.Add("companyId", ticketStatusChanged["companyId"]);
                    xslArguments.Add("createdBy", ticketStatusChanged["createdBy"]);
                    xslArguments.Add("createdOn", ticketStatusChanged["createdOn"]);
                    xslArguments.Add("title", ticketStatusChanged["title"]);
                    xslArguments.Add("description", ticketStatusChanged["description"]);
                    xslArguments.Add("priority", ticketStatusChanged["priority"]);
                    xslArguments.Add("status", ticketStatusChanged["status"]);
                    xslArguments.Add("oldStatus", ticketStatusChanged["oldStatus"]);
                    xslArguments.Add("newStatus", ticketStatusChanged["newStatus"]);
                    xslArguments.Add("modifiedBy", ticketStatusChanged["modifiedBy"]);
                    xslArguments.Add("modifiedOn", ticketStatusChanged["modifiedOn"]);
                    break;
                case SupportNotificationEventEnum.TicketReassigned:
                    Dictionary<string, string> ticketReassingned = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", ticketReassingned["publisherName"]);
                    xslArguments.Add("appName", ticketReassingned["appName"]);
                    xslArguments.Add("customerName", ticketReassingned["customerName"]);
                    xslArguments.Add("ticketId", ticketReassingned["ticketId"]);
                    xslArguments.Add("companyName", ticketReassingned["companyName"]);
                    xslArguments.Add("companyId", ticketReassingned["companyId"]);
                    xslArguments.Add("createdBy", ticketReassingned["createdBy"]);
                    xslArguments.Add("createdOn", ticketReassingned["createdOn"]);
                    xslArguments.Add("customerId", ticketReassingned["customerId"]);
                    xslArguments.Add("title", ticketReassingned["title"]);
                    xslArguments.Add("description", ticketReassingned["description"]);
                    xslArguments.Add("priority", ticketReassingned["priority"]);
                    xslArguments.Add("status", ticketReassingned["status"]);
                    xslArguments.Add("tenantid", ticketReassingned["tenantid"]);
                    xslArguments.Add("modifiedBy", ticketReassingned["modifiedBy"]);
                    xslArguments.Add("modifiedOn", ticketReassingned["modifiedOn"]);
                    xslArguments.Add("newAssignedTo", ticketReassingned["newAssignedTo"]);
                    xslArguments.Add("oldAssignedTo", ticketReassingned["oldAssignedTo"]);
                    break;
                case SupportNotificationEventEnum.TicketPriorityChanged:
                    Dictionary<string, string> ticketPriorityChanged = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", ticketPriorityChanged["publisherName"]);
                    xslArguments.Add("appName", ticketPriorityChanged["appName"]);
                    xslArguments.Add("customerName", ticketPriorityChanged["customerName"]);
                    xslArguments.Add("ticketId", ticketPriorityChanged["ticketId"]);
                    xslArguments.Add("companyName", ticketPriorityChanged["companyName"]);
                    xslArguments.Add("companyId", ticketPriorityChanged["companyId"]);
                    xslArguments.Add("createdBy", ticketPriorityChanged["createdBy"]);
                    xslArguments.Add("createdOn", ticketPriorityChanged["createdOn"]);
                    xslArguments.Add("customerId", ticketPriorityChanged["customerId"]);
                    xslArguments.Add("title", ticketPriorityChanged["title"]);
                    xslArguments.Add("description", ticketPriorityChanged["description"]);
                    xslArguments.Add("priority", ticketPriorityChanged["priority"]);
                    xslArguments.Add("status", ticketPriorityChanged["status"]);
                    xslArguments.Add("tenantid", ticketPriorityChanged["tenantid"]);
                    xslArguments.Add("modifiedBy", ticketPriorityChanged["modifiedBy"]);
                    xslArguments.Add("modifiedOn", ticketPriorityChanged["modifiedOn"]);
                    xslArguments.Add("oldPriority", ticketPriorityChanged["oldPriority"]);
                    xslArguments.Add("newPriority", ticketPriorityChanged["newPriority"]);
                    break;
                case SupportNotificationEventEnum.TicketNewCommentAdded:
                    Dictionary<string, string> newCommentAdded = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", newCommentAdded["publisherName"]);
                    xslArguments.Add("appName", newCommentAdded["appName"]);
                    xslArguments.Add("customerName", newCommentAdded["customerName"]);
                    xslArguments.Add("ticketId", newCommentAdded["ticketId"]);
                    xslArguments.Add("companyName", newCommentAdded["companyName"]);
                    xslArguments.Add("companyId", newCommentAdded["companyId"]);
                    xslArguments.Add("createdBy", newCommentAdded["createdBy"]);
                    xslArguments.Add("createdOn", newCommentAdded["createdOn"]);
                    xslArguments.Add("customerId", newCommentAdded["customerId"]);
                    xslArguments.Add("title", newCommentAdded["title"]);
                    xslArguments.Add("description", newCommentAdded["description"]);
                    xslArguments.Add("priority", newCommentAdded["priority"]);
                    xslArguments.Add("status", newCommentAdded["status"]);
                    xslArguments.Add("comment", newCommentAdded["comment"]);
                    xslArguments.Add("tenantid", newCommentAdded["tenantid"]);
                    xslArguments.Add("modifiedBy", newCommentAdded["modifiedBy"]);
                    xslArguments.Add("modifiedOn", newCommentAdded["modifiedOn"]);
                    break;
                case SupportNotificationEventEnum.TicketNewAttachmentAdded:
                    Dictionary<string, string> ticketNewAttachmentAdded = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    xslArguments.Add("publisherName", ticketNewAttachmentAdded["publisherName"]);
                    xslArguments.Add("appName", ticketNewAttachmentAdded["appName"]);
                    xslArguments.Add("customerName", ticketNewAttachmentAdded["customerName"]);
                    xslArguments.Add("ticketId", ticketNewAttachmentAdded["ticketId"]);
                    xslArguments.Add("companyName", ticketNewAttachmentAdded["companyName"]);
                    xslArguments.Add("companyId", ticketNewAttachmentAdded["companyId"]);
                    xslArguments.Add("createdBy", ticketNewAttachmentAdded["createdBy"]);
                    xslArguments.Add("createdOn", ticketNewAttachmentAdded["createdOn"]);
                    xslArguments.Add("customerId", ticketNewAttachmentAdded["customerId"]);
                    xslArguments.Add("title", ticketNewAttachmentAdded["title"]);
                    xslArguments.Add("description", ticketNewAttachmentAdded["description"]);
                    xslArguments.Add("priority", ticketNewAttachmentAdded["priority"]);
                    xslArguments.Add("status", ticketNewAttachmentAdded["status"]);
                    xslArguments.Add("attachmentCount", ticketNewAttachmentAdded["attachmentCount"]);
                    xslArguments.Add("tenantid", ticketNewAttachmentAdded["tenantid"]);
                    xslArguments.Add("modifiedBy", ticketNewAttachmentAdded["modifiedBy"]);
                    xslArguments.Add("modifiedOn", ticketNewAttachmentAdded["modifiedOn"]);
                    break;
                default:
                    break;
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
            bool valid = true;
            SupportNotificationEventEnum parsedEvent;
            if(moduleId <= 0) {
                valid = false;
            }
            if(Enum.TryParse<SupportNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
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

        /// <summary>Get branch key on the basis of decision parameters.</summary>
        /// <param name="decisionParameters">Set of information to make decision about branch key, deeplink actions and parameters.</param>
        /// <returns>Returns a portal wise branch key, deeplink actions and parameters in form of <see cref="Dictionary{System.Int32, TValue}"/> Dictionary&lt;int,object&gt;. Key is portal type and value is of type <see cref=""/> </returns>
        protected override object GetBranchKey(object decisionParameters) {

            Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = new Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>();
            bool isDefaultAssignedLevelChanged = false;
            if(decisionParameters != null) {
                // Get decision paramerts from object.
                Dictionary<string, object> decisionDictionary = decisionParameters as Dictionary<string, object>;
                long eventId = long.Parse(decisionDictionary["eventid"].ToString());
                if(decisionDictionary.ContainsKey("islevelassigned")) {
                    isDefaultAssignedLevelChanged = bool.Parse(decisionDictionary["islevelassigned"].ToString());
                }
                int supportLevel = int.Parse(decisionDictionary["supportlevel"].ToString());

                // ToDo: Nitin: Review use of UserType enum dictionary as UserType is part of application and support level is part of support module.
                switch(eventId) {
                    case (long)SupportNotificationEventEnum.AddLevel1Ticket: {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            break;
                        }
                    case (long)SupportNotificationEventEnum.AddLevel2Ticket: {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            break;
                        }
                    case (long)SupportNotificationEventEnum.TicketIsAssigned: {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            break;
                        }
                    case (long)SupportNotificationEventEnum.TicketStatusChanged:
                        if(supportLevel == (int)SupportLevelEnum.Level1) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,myticket";
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            if(isDefaultAssignedLevelChanged) {
                                model = new DeeplinkActionParameterAndBranchKeyDTO();
                                parameterList = new List<string>();
                                parameterList.Add("tenantid");
                                parameterList.Add("supportTicketId");
                                model.DeepLinkAction = "helpnsupport,customerticket";
                                model.DeeplinkParametersList = parameterList;
                                model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            }
                        }
                        else if(supportLevel == (int)SupportLevelEnum.Level2) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,myticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,businessticket";
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                        }
                        break;
                    case (long)SupportNotificationEventEnum.TicketReassigned:
                        if(supportLevel == (int)SupportLevelEnum.Level1) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,myticket";
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            if(isDefaultAssignedLevelChanged) {
                                model = new DeeplinkActionParameterAndBranchKeyDTO();
                                parameterList = new List<string>();
                                parameterList.Add("tenantid");
                                parameterList.Add("supportTicketId");
                                model.DeepLinkAction = "helpnsupport,customerticket";
                                model.DeeplinkParametersList = parameterList;
                                model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            }
                        }
                        else if(supportLevel == (int)SupportLevelEnum.Level2) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,myticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,businessticket";
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                        }
                        break;
                    case (long)SupportNotificationEventEnum.TicketPriorityChanged:
                        if(supportLevel == (int)SupportLevelEnum.Level1) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,myticket";
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            if(isDefaultAssignedLevelChanged) {
                                model = new DeeplinkActionParameterAndBranchKeyDTO();
                                parameterList = new List<string>();
                                parameterList.Add("tenantid");
                                parameterList.Add("supportTicketId");
                                model.DeepLinkAction = "helpnsupport,customerticket";
                                model.DeeplinkParametersList = parameterList;
                                model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            }
                        }
                        else if(supportLevel == (int)SupportLevelEnum.Level2) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,myticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,businessticket";
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                        }
                        break;
                    case (long)SupportNotificationEventEnum.TicketNewCommentAdded:
                        if(supportLevel == (int)SupportLevelEnum.Level1) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,myticket";
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            if(isDefaultAssignedLevelChanged) {
                                model = new DeeplinkActionParameterAndBranchKeyDTO();
                                parameterList = new List<string>();
                                parameterList.Add("tenantid");
                                parameterList.Add("supportTicketId");
                                model.DeepLinkAction = "helpnsupport,customerticket";
                                model.DeeplinkParametersList = parameterList;
                                model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            }
                        }
                        else if(supportLevel == (int)SupportLevelEnum.Level2) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,myticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,businessticket";
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                        }
                        break;
                    case (long)SupportNotificationEventEnum.TicketNewAttachmentAdded:
                        if(supportLevel == (int)SupportLevelEnum.Level1) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,customerticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,myticket";
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            if(isDefaultAssignedLevelChanged) {
                                model = new DeeplinkActionParameterAndBranchKeyDTO();
                                parameterList = new List<string>();
                                parameterList.Add("tenantid");
                                parameterList.Add("supportTicketId");
                                model.DeepLinkAction = "helpnsupport,customerticket";
                                model.DeeplinkParametersList = parameterList;
                                model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                                branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                            }
                        }
                        else if(supportLevel == (int)SupportLevelEnum.Level2) {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "helpnsupport,myticket";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.BranchKey = _appSettingsOption.Value.PaymentBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Business, model);
                            model = new DeeplinkActionParameterAndBranchKeyDTO();
                            parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("supportTicketId");
                            model.DeeplinkParametersList = parameterList;
                            model.DeepLinkAction = "helpnsupport,businessticket";
                            model.BranchKey = _appSettingsOption.Value.PublisherBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Publisher, model);
                        }
                        break;
                    default:
                        branchkeyList.Add(0, new DeeplinkActionParameterAndBranchKeyDTO { });
                        break;
                }
            }
            return branchkeyList;
        }

        #region Private methods

        #region XSLT

        /// <summary>Gets the email XSLT file.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="regionLanguage">The region language.</param>
        /// <returns></returns>
        private string GetEmailXsltFile(SupportNotificationEventEnum eventId, string regionLanguage) {
            //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
            //if (CacheHelper.IsInCache(cacheKey)) {
            //  return CacheHelper.GetData<string>(cacheKey); ;
            //}
            //else {
            //Engg: should define in Config file.
            // string xsltResourcePath = "/resourcePath/";
            string templateFileName = GetXSLTemplateFileName(eventId);
            // string xsltTextFile = FileHelper.ReadFileAsText(Path.Combine(Path.Combine(_appSettings.XslTemplateRootPath, regionLanguage), templateFileName));
            string xsltTextFile = null;
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
        private string GetXSLTemplateFileName(SupportNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case SupportNotificationEventEnum.AddLevel1Ticket:
                    return "AddBusinessPartnerTicket.xslt";
                case SupportNotificationEventEnum.AddLevel2Ticket:
                    return "AddBusinessTicket.xslt";
                case SupportNotificationEventEnum.TicketIsAssigned:
                    return "CustomerTicketForwardedToPublisher.xslt";
                case SupportNotificationEventEnum.TicketStatusChanged:
                    return "TicketStatusChanged.xslt";
                case SupportNotificationEventEnum.TicketReassigned:
                    return "TicketReassinged.xslt";
                case SupportNotificationEventEnum.TicketPriorityChanged:
                    return "TicketPriorityChanged.xslt";
                case SupportNotificationEventEnum.TicketNewCommentAdded:
                    return "CommentAddedToTicket.xslt";
                case SupportNotificationEventEnum.TicketNewAttachmentAdded:
                    return "AttachmentAdded.xslt";
                default:
                    return "";
            }
        }

        #endregion XSLT

        #region deeplink

        private DeeplinkPayload SetDeeplinkPayloadForEmail(string linkKey, Guid tenantId, Guid notificationId, Guid userId) {

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

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetLevel1AddDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();

                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    if(eventData.ContainsKey("ispublisherassinged")) {
                        decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    }
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
                }

                // Get barnch key list.
                Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>; // DTO=> BranchKey, Action, Param=>string,

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

                    // TODO:
                    invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetLevel2AddDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    if(eventData.ContainsKey("ispublisherassinged")) {
                        decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    }
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
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

                    // TODO:
                    invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetCustomerTicketForwardeddDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    if(eventData.ContainsKey("ispublisherassinged")) {
                        decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    }
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
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

                    // TODO:
                    invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);
                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetTicketStatusChangedDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {

            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    if(eventData.ContainsKey("ispublisherassinged")) {
                        decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    }
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
                }

                // Get barnch key list.
                Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                // Create deeplink af all the branch keys.
                foreach(int item in branchkeyList.Keys) {
                    Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = branchkeyList[item].DeepLinkAction;//"businesspartneruser,addticket";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                        invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                    }
                    invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        private List<DeeplinkInfo> GetTicketReassingedDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
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

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetCommentAddedToTicketDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
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

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetTicketPriorityChangedDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
                }

                // Get barnch key list.
                Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = GetBranchKey(decisionDictionary) as Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>;

                // Create deeplink af all the branch keys.
                foreach(int item in branchkeyList.Keys) {
                    Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = branchkeyList[item].DeepLinkAction; // "businesspartneruser,addticket";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    // Set Dynamic info from Notification payload.
                    foreach(string paramerts in branchkeyList[item].DeeplinkParametersList) {
                        invitationDeeplinkInfo.Add(paramerts, deeplinkInfo[paramerts]);
                    }
                    invitationDeeplinkInfo.Add("branchkey", branchkeyList[item].BranchKey);

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

        // TODO : changed the retrun type;
        private List<DeeplinkInfo> GetAttachmentAddedToTicketDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            List<DeeplinkInfo> list = new List<DeeplinkInfo>();
            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {

                Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> eventData = notificationPayload.EventInfo["EventData"] as Dictionary<string, string>;
                    decisionDictionary.Add("eventid", eventId);
                    decisionDictionary.Add("islevelassigned", eventData["ispublisherassinged"]);
                    decisionDictionary.Add("supportlevel", eventData["supportlevel"]);
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

                    deeplinks.Add("activationLink", invitationDeeplinkInfo);
                    DeeplinkInfo model = new DeeplinkInfo();
                    model.DeeplinkInfoList = deeplinks;
                    model.DeeplinkKey = item.ToString();
                    list.Add(model);
                }
            }
            return list;
        }

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
