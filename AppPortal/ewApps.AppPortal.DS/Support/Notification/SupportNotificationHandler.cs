///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Atul Badgujar <abadgujar@batchmaster.com>
// * Date: 26 August 2019
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 26 August 2019
// */

//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ewApps.AppPortal.Common;
//using ewApps.Core.BaseService;

//namespace ewApps.AppPortal.DS {

//    /// <summary>DataService implementing Support notification service</summary>
//    public class SupportNotificationHandler:ISupportNotificationHandler {

//    #region Local member

//    ISupportNotificationService _supportNotificationDataService;

//    #endregion Local member

//    #region Constructor

//    /// <summary>Initilizing the objects</summary>
//    public SupportNotificationHandler(ISupportNotificationService supportNotificationDataService) {
//      _supportNotificationDataService = supportNotificationDataService;
//    }

//    #endregion Constructor

//    #region Public Methods

//    #region Add

//    ///<inheritdoc/>
//    public async Task AddLevel1TicketAsync(SupportNotificationDTO supportNotificationDTO) {

//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["tenantName"]);
//      eventData.Add("createdBy", notificationEvent["customerUserName"]);
//      eventData.Add("createdOn", DateTime.UtcNow.ToString());
//      eventData.Add("customerId", notificationEvent["customerRefId"]);
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      if(supportNotificationDTO.SupportCommentDTO != null) {
//        eventData.Add("comment", supportNotificationDTO.SupportCommentDTO.CommentText);
//      }
//      else {
//        eventData.Add("comment", "");
//      }
//      eventData.Add("companyid", notificationEvent["companyid"]);
//      eventData.Add("userType", ((int)UserTypeEnum.Customer).ToString());
//      eventData.Add("appId", supportNotificationDTO.UserSession.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Payment, (long)SupportNotificationEventEnum.AddLevel1Ticket, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    ///<inheritdoc/>
//    public async Task AddLevel2TicketAsync(SupportNotificationDTO supportNotificationDTO) {

//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }
//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["tenantName"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", DateTime.UtcNow.ToString());
//      eventData.Add("customerId", notificationEvent["tenantIdentity"]);
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      if(supportNotificationDTO.SupportCommentDTO != null) {
//        eventData.Add("comment", supportNotificationDTO.SupportCommentDTO.CommentText);
//      }
//      else {
//        eventData.Add("comment", "");
//      }
//      eventData.Add("userType", ((int)UserTypeEnum.Customer).ToString());
//      eventData.Add("appId", supportNotificationDTO.UserSession.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.AddLevel2Ticket, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    #endregion Add

//    #region Forward from one level to another

//    ///<inheritdoc/>
//    public async Task TicketIsAssingedAsync(SupportNotificationDTO supportNotificationDTO) {

//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      if(supportNotificationDTO.SupportCommentDTO != null) {
//        eventData.Add("comment", supportNotificationDTO.SupportCommentDTO.CommentText);
//      }
//      else {
//        eventData.Add("comment", "");
//      }
//      eventData.Add("appId", supportNotificationDTO.UserSession.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketIsAssigned, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    #endregion Forward from one level to another

//    #region Ticket Update

//    ///<inheritdoc/>
//    public async Task TicketStatusIsChangedAsync(SupportNotificationDTO supportNotificationDTO) {

//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      eventData.Add("appId", supportNotificationDTO.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());
//      eventData.Add("modifiedBy", notificationEvent["modifiedBy"]);
//      eventData.Add("modifiedOn", supportNotificationDTO.SupportTicket.UpdatedOn.ToString());
//      eventData.Add("ownerappuserid", supportNotificationDTO.SupportTicket.CreatedBy.ToString());
//      eventData.Add("ispublisherassinged", supportNotificationDTO.IsPublisherAssinged.ToString());

//      eventData.Add("oldStatus", notificationEvent["oldStatus"]);
//      eventData.Add("newStatus", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketStatusChanged, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    ///<inheritdoc/>
//    public async Task TicketIsReassingedAsync(SupportNotificationDTO supportNotificationDTO) {
//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      eventData.Add("appId", supportNotificationDTO.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());
//      eventData.Add("modifiedBy", notificationEvent["modifiedBy"]);
//      eventData.Add("modifiedOn", supportNotificationDTO.SupportTicket.UpdatedOn.ToString());
//      eventData.Add("ownerappuserid", supportNotificationDTO.SupportTicket.CreatedBy.ToString());
//      eventData.Add("ispublisherassinged", supportNotificationDTO.IsPublisherAssinged.ToString());

//      eventData.Add("newAssignedTo", notificationEvent["newAssignedTo"]);
//      eventData.Add("oldAssignedTo", notificationEvent["oldAssignedTo"]);

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketReassigned, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);

//    }

//    ///<inheritdoc/>
//    public async Task TicketPriorityIsChangedAsync(SupportNotificationDTO supportNotificationDTO) {
//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      eventData.Add("appId", supportNotificationDTO.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());
//      eventData.Add("modifiedBy", notificationEvent["modifiedBy"]);
//      eventData.Add("modifiedOn", supportNotificationDTO.SupportTicket.UpdatedOn.ToString());
//      eventData.Add("ownerappuserid", supportNotificationDTO.SupportTicket.CreatedBy.ToString());
//      eventData.Add("ispublisherassinged", supportNotificationDTO.IsPublisherAssinged.ToString());

//      eventData.Add("oldPriority", notificationEvent["oldPriority"]);
//      eventData.Add("newPriority", ((SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketPriorityChanged, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    ///<inheritdoc/>
//    public async Task CommentIsAddToTicketAsync(SupportNotificationDTO supportNotificationDTO) {
//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((Common.SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((Common.SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      if(supportNotificationDTO.SupportCommentDTO != null) {
//        eventData.Add("comment", supportNotificationDTO.SupportCommentDTO.CommentText);
//      }
//      else {
//        eventData.Add("comment", "");
//      }
//      eventData.Add("appId", supportNotificationDTO.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());
//      eventData.Add("modifiedBy", notificationEvent["modifiedBy"]);
//      eventData.Add("modifiedOn", supportNotificationDTO.SupportTicket.UpdatedOn.ToString());
//      eventData.Add("ownerappuserid", supportNotificationDTO.SupportTicket.CreatedBy.ToString());
//      eventData.Add("ispublisherassinged", supportNotificationDTO.IsPublisherAssinged.ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketNewCommentAdded, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }

//    ///<inheritdoc/>
//    public async Task AttachmentIsAddedToTicketAsync(SupportNotificationDTO supportNotificationDTO) {
//      Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//      if(supportNotificationDTO.NotificationEventData != null) {
//        notificationEvent = supportNotificationDTO.NotificationEventData;
//      }

//      // Create notification information.
//      Dictionary<string, string> eventData = new Dictionary<string, string>();
//      eventData.Add("publisherName", supportNotificationDTO.PublisherName);
//      eventData.Add("appName", supportNotificationDTO.AppName);
//      eventData.Add("customerName", notificationEvent["customerName"]);
//      eventData.Add("customerId", notificationEvent["customerId"]);
//      eventData.Add("ticketId", supportNotificationDTO.SupportTicket.IdentityNumber);
//      eventData.Add("companyName", notificationEvent["companyName"]);
//      eventData.Add("companyId", notificationEvent["companyId"]);
//      eventData.Add("createdBy", notificationEvent["createdBy"]);
//      eventData.Add("createdOn", supportNotificationDTO.SupportTicket.CreatedOn.ToString());
//      eventData.Add("title", supportNotificationDTO.SupportTicket.Title);
//      eventData.Add("description", supportNotificationDTO.SupportTicket.Description);
//      eventData.Add("priority", ((Common.SupportPriorityEnum)supportNotificationDTO.SupportTicket.Priority).ToString());
//      eventData.Add("status", ((Common.SupportStatusTypeEnum)supportNotificationDTO.SupportTicket.Status).ToString());
//      eventData.Add("attachmentCount", notificationEvent["attachmentCount"]);
//      eventData.Add("appId", supportNotificationDTO.AppId.ToString());
//      eventData.Add("tenantid", supportNotificationDTO.SupportTicket.TenantId.ToString());
//      eventData.Add("supportlevel", (supportNotificationDTO.SupportTicket.GenerationLevel).ToString());
//      eventData.Add("modifiedBy", notificationEvent["modifiedBy"]);
//      eventData.Add("modifiedOn", supportNotificationDTO.SupportTicket.UpdatedOn.ToString());
//      eventData.Add("ownerappuserid", supportNotificationDTO.SupportTicket.CreatedBy.ToString());
//      eventData.Add("ispublisherassinged", supportNotificationDTO.IsPublisherAssinged.ToString());

//      // Create deeplink info.
//      Dictionary<string, string> deeplinkInfo = new Dictionary<string, string>();
//      deeplinkInfo.Add("tenantid", supportNotificationDTO.UserSession.TenantId.ToString());
//      deeplinkInfo.Add("supportTicketId", supportNotificationDTO.SupportTicket.ID.ToString());

//      // Creates list of dictionary for event data.
//      Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
//      eventDataDict.Add("EventData", eventData);
//      eventDataDict.Add("DeeplinkInfo", deeplinkInfo);
//      eventDataDict.Add("UserSession", supportNotificationDTO.UserSession);

//      await _supportNotificationDataService.GenerateNotificationAsync((int)ModuleTypeEnum.Business, (long)SupportNotificationEventEnum.TicketNewAttachmentAdded, false, eventDataDict, supportNotificationDTO.UserSession.TenantUserId, false);
//    }


//    #endregion Ticket Update

//    #endregion Public Methods

//  }
//}
