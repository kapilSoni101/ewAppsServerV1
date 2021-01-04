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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;

using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {
  public class PaymentSupportTicketDSNew:SupportTicketDSNew, IPaymentSupportTicketDSNew {

    #region Local Member

    IUserSessionManager _userSessionDS = null;
    // ISupportNotificationHandler _supportNotificationHandler;
    IBizNotificationHandler _bizNotificationHandler;
    ISupportTicketAssigneeHelper _supportTicketAssigneeHelper;
    ICustNotificationHandler _custNotificationHandler;

    #endregion Local Member

    #region Constructor

    public PaymentSupportTicketDSNew(IUniqueIdentityGeneratorDS identityDS,
        IUserSessionManager userSessionManager,
ISupportCommentDS supportCommentDS,
        ILevelTransitionHistoryDS levelTransitionDS,
        ISupportTicketAssigneeHelper supportTicketAssigneeHelper, IDMDocumentDS documentDS,
        ISupportTicketRepository supportTicketRepository, IBizNotificationHandler bizNotificationHandler, ICustNotificationHandler custNotificationHandler)
        : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager, documentDS, supportTicketAssigneeHelper) {
      _userSessionDS = userSessionManager;
      // TO DO NITIN SIR
      //_supportNotificationHandler = supportNotificationHandler;    
      _bizNotificationHandler = bizNotificationHandler;
      _custNotificationHandler = custNotificationHandler;
      _supportTicketAssigneeHelper = supportTicketAssigneeHelper;

      if(_userSessionDS.GetSession().UserType == (int)UserTypeEnum.Business) {
        base.AppSupportLevel = BusinessAppSupportLevel;
      }
      else if(_userSessionDS.GetSession().UserType == (int)UserTypeEnum.Customer) {
        base.AppSupportLevel = CustomerAppSupportLevel;
      }

      // base.ApplicationKey = AppKeyEnum.pay;
    }

    #endregion Constructor

    public SupportTicket AddCustomerSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
      // return base.AddSupportTicket(supportTicketDTO, CustomerAppSupportLevel, documentModel, httpRequest);
      bool result = AddSupportTicketCustomer(supportTicketDTO, CustomerAppSupportLevel, documentModel, httpRequest).Result;
      return new SupportTicket();
    }

    public bool UpdateCustomerSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
      //return base.UpdateSupportTicket(supportTicketDTO, CustomerAppSupportLevel, documentModel, httpRequest);
      bool res = UpdateSupportTicketFromCustomer(supportTicketDTO, BusinessAppSupportLevel, documentModel, httpRequest).Result;
      return true;
    }


    // Get My Ticket List (My Ticket) On Customer Portal
    public async Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList(Guid tenantId, Guid creatorId, Guid? partnerId, bool onlyDeleted, string appKey, CancellationToken token = default(CancellationToken)) {
      return await GetUserSupportTicketByCreatorAndPartnerAndTenantId(appKey, tenantId, (int)CustomerAppSupportLevel, creatorId, partnerId, onlyDeleted, token);
    }

    public SupportTicket AddBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
      return base.AddSupportTicket(supportTicketDTO, BusinessAppSupportLevel, documentModel, httpRequest);
    }

    public bool UpdateBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
      //return base.UpdateSupportTicket(supportTicketDTO, BusinessAppSupportLevel, documentModel, httpRequest);
      bool res = UpdateSupportTicketFromBusiness(supportTicketDTO, BusinessAppSupportLevel, documentModel, httpRequest).Result;

      return true;
    }

    public async Task<List<SupportMyTicketDTO>> GetBusinessMyTicketList(Guid tenantId, Guid creatorId, bool onlyDeleted, string appKey) {
      return await GetUserSupportTicketByCreatorAndPartnerAndTenantId(appKey, tenantId, (int)BusinessAppSupportLevel, creatorId, null, onlyDeleted);
    }


    public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2List(bool includeDeleted, string appKey, CancellationToken token = default(CancellationToken)) {

      return await base.GetSupportTicketAssignedToLevel2List(appKey, _userSessionDS.GetSession().TenantId, includeDeleted);
    }

    public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2BusinessList(bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken)) {

      return await base.GetSupportTicketAssignedToLevel2BusinessList(_userSessionDS.GetSession().TenantId, includeDeleted, generationLevel, token);
    }

    public async Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2CustomerList(bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken)) {

      return await base.GetSupportTicketAssignedToLevel2CustomerList(_userSessionDS.GetSession().TenantId, includeDeleted, generationLevel, token);
    }


    public SupportLevelEnum BusinessAppSupportLevel {
      get {
        return SupportLevelEnum.Level2;
      }
      set {
        throw new InvalidOperationException("BusinessAppSupportLevel property can't be change.");
      }
    }


    public SupportLevelEnum CustomerAppSupportLevel {
      get {
        return SupportLevelEnum.Level1;
      }
      set {
        throw new InvalidOperationException("CustomerAppSupportLevel property can't be change.");
      }
    }

    #region Update From Business
    // Update Business/Customer Support Ticket
    private async Task<bool> UpdateSupportTicketFromBusiness(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {


      // Get ticket entity.
      SupportTicket supportTicketOld = Get((Guid)supportTicketDTO.SupportTicketId);
      // Get all the events for which notification needs to be raised.

      // Store the data before updating the data needed in creating notification data.
      Dictionary<string, string> notificationOldData = new Dictionary<string, string>();
      notificationOldData.Add("oldPriority", ((SupportPriorityEnum)supportTicketOld.Priority).ToString());
      notificationOldData.Add("oldStatus", ((SupportStatusTypeEnum)supportTicketOld.Status).ToString());
      string createdByUserName = null;
      string oldSupportLevel = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketOld.CustomerId, supportTicketOld.TenantId, createdByUserName, supportTicketOld.CurrentLevel, supportTicketOld.GenerationLevel);


      string newSupportLevel = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketDTO.CustomerId, supportTicketDTO.TenantId, createdByUserName, supportTicketDTO.CurrentLevel, supportTicketOld.GenerationLevel);

      notificationOldData.Add("oldAssignedTo", oldSupportLevel);
      notificationOldData.Add("newAssignedTo", newSupportLevel);

      // Update ticket all the logical operation for the update is done in this method.
      //ToDo: Replace this method with SupportTicketDSNew method.
      bool result = base.UpdateSupportTicket(supportTicketDTO, supportLevel, documentModel, httpRequest);

      // Loop over the events to send the notifications.
      //foreach (SupportNotificationEventEnum item in eventList)
      //{
      //  Task task = SendUpdateNotificationAsync(supportTicketDTO, item, notificationOldData);
      //}

      // Task task = SendUpdateNotificationAsync(supportTicketDTO, notificationOldData);
      if(supportTicketOld.GenerationLevel == (short)SupportLevelEnum.Level1) {
        // await SendUpdateNotificationAsync(supportTicketDTO, notificationOldData, documentModel, httpRequest);
        Task task = SendUpdateNotificationAsync(supportTicketDTO, notificationOldData, documentModel, httpRequest);
      }
      return result;
    }

    #endregion Update From Business

    #region Customer
    // Add Customer Support Ticket
    private async Task<bool> AddSupportTicketCustomer(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {

      //Add Support Ticket
      SupportTicket supportTicket = base.AddSupportTicket(supportTicketDTO, supportLevel, documentModel, httpRequest);

      if(supportTicket.GenerationLevel == (short)SupportLevelEnum.Level1) {
        await SendAddCustomerTicketNotificationAsync(supportTicketDTO, supportTicket, documentModel, httpRequest);
      }

      return true;
    }


    // Update Customer Support Ticket
    private async Task<bool> UpdateSupportTicketFromCustomer(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {


      // Get ticket entity.
      SupportTicket supportTicketOld = Get((Guid)supportTicketDTO.SupportTicketId);

      Dictionary<string, string> notificationOldData = new Dictionary<string, string>();
      notificationOldData.Add("oldPriority", ((SupportPriorityEnum)supportTicketOld.Priority).ToString());
      notificationOldData.Add("oldStatus", ((SupportStatusTypeEnum)supportTicketOld.Status).ToString());
      string createdByUserName = null;
      string oldSupportLevel = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketOld.CustomerId, supportTicketOld.TenantId, createdByUserName, supportTicketOld.CurrentLevel, supportTicketOld.GenerationLevel);


      string newSupportLevel = await _supportTicketAssigneeHelper.GetAssigneeName(supportTicketDTO.CustomerId, supportTicketDTO.TenantId, createdByUserName, supportTicketDTO.CurrentLevel, supportTicketOld.GenerationLevel);

      notificationOldData.Add("oldAssignedTo", oldSupportLevel);
      notificationOldData.Add("newAssignedTo", newSupportLevel);

      // Update ticket all the logical operation for the update is done in this method.
      //ToDo: Replace this method with SupportTicketDSNew method.
      bool result = base.UpdateSupportTicket(supportTicketDTO, supportLevel, documentModel, httpRequest);

      //Check Customer Ticket By Generation Level  
      if(supportTicketOld.GenerationLevel == (short)SupportLevelEnum.Level1) {
        //Send Customer Update Notification 
        Task task = SendUpdateCustomerTicketNotificationAsync(supportTicketDTO, notificationOldData, documentModel, httpRequest);
      }
      return result;
    }
    #endregion Customer

    #region Notification Methods

    #region Business
    private async Task SendUpdateNotificationAsync(SupportAddUpdateDTO supportAddUpdateDTO, Dictionary<string, string> notificationOldData, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {

      // Get all the entities needed to generate the notifications  data.     
      BusinessSupportNotificationDTO businessSupportNotificationDTO = new BusinessSupportNotificationDTO();
      businessSupportNotificationDTO = await base.GetBusinessSupportNotificationData(supportAddUpdateDTO, token);
      SupportTicket supportTicket = Get((Guid)supportAddUpdateDTO.SupportTicketId);

      businessSupportNotificationDTO.NewAssignee = notificationOldData["newAssignedTo"];
businessSupportNotificationDTO.OldAssignee = notificationOldData["oldAssignedTo"];
      businessSupportNotificationDTO.OldStatus = notificationOldData["oldStatus"];
      businessSupportNotificationDTO.NewStatus = ((SupportStatusTypeEnum)businessSupportNotificationDTO.Status).ToString();
      businessSupportNotificationDTO.OldPriority = notificationOldData["oldPriority"];
      businessSupportNotificationDTO.NewPriority = ((SupportPriorityEnum)businessSupportNotificationDTO.Priority).ToString();

      if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
        SupportCommentDTO newComment = supportAddUpdateDTO.SupportCommentList.FirstOrDefault(i => i.OperationType == (int)OperationType.Add);
        if(newComment != null) {
          businessSupportNotificationDTO.CommentText = newComment.CommentText;
        }
        else
          businessSupportNotificationDTO.CommentText = "";
      }

      if(documentModel != null && documentModel.HasFiles) {
        businessSupportNotificationDTO.Count = httpRequest.Form.Files.Count.ToString();
      }
      else {
        businessSupportNotificationDTO.Count = "0";
      }

      businessSupportNotificationDTO.UserSessionInfo = _userSessionDS.GetSession();
      int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, (int)SupportLevelEnum.Level3);
      if(supportLevelChange == 0) {
        businessSupportNotificationDTO.IsPublisherAssinged = false;
      }
      else {
        businessSupportNotificationDTO.IsPublisherAssinged = true;
      }

      await _bizNotificationHandler.SendBusinessSupportNotificationAsync(businessSupportNotificationDTO);
    }

    #endregion Business

    #region Customer
    private async Task SendAddCustomerTicketNotificationAsync(SupportAddUpdateDTO supportAddUpdateDTO, SupportTicket supportTicketDTO, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {

      // Get all the entities needed to generate the notifications  data.     
      BusinessSupportNotificationDTO businessSupportNotificationDTO = new BusinessSupportNotificationDTO();
      supportAddUpdateDTO.SupportTicketId = supportTicketDTO.ID;
      businessSupportNotificationDTO = await base.GetBusinessSupportNotificationData(supportAddUpdateDTO, token);
      SupportTicket supportTicket = Get((Guid)supportTicketDTO.ID);

      // businessSupportNotificationDTO.NewAssignee = notificationOldData["newAssignedTo"];
      // businessSupportNotificationDTO.OldStatus = notificationOldData["oldStatus"];
      businessSupportNotificationDTO.NewStatus = ((SupportPriorityEnum)businessSupportNotificationDTO.Status).ToString();
      //businessSupportNotificationDTO.OldPriority = notificationOldData["oldPriority"];
      businessSupportNotificationDTO.NewPriority = ((SupportPriorityEnum)businessSupportNotificationDTO.Priority).ToString();


      if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
        SupportCommentDTO newComment = supportAddUpdateDTO.SupportCommentList.FirstOrDefault(i => i.OperationType == (int)OperationType.Add);
        if(newComment != null) {
          businessSupportNotificationDTO.CommentText = newComment.CommentText;
        }
        else
          businessSupportNotificationDTO.CommentText = "";
      }


      //if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
      //  businessSupportNotificationDTO.CommentText = supportAddUpdateDTO.SupportCommentList[0].CommentText;
      //}
      //else
      //  businessSupportNotificationDTO.CommentText = "";

      if(documentModel != null && documentModel.HasFiles) {
        businessSupportNotificationDTO.Count = httpRequest.Form.Files.Count.ToString();
      }
      else {
        businessSupportNotificationDTO.Count = "0";
      }


      businessSupportNotificationDTO.UserSessionInfo = _userSessionDS.GetSession();
      businessSupportNotificationDTO.IsPublisherAssinged = false;
      //int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, (int)SupportLevelEnum.Level3);
      //if(supportLevelChange == 0) {
      //  businessSupportNotificationDTO.IsPublisherAssinged = false;
      //}
      //else {
      //  businessSupportNotificationDTO.IsPublisherAssinged = true;
      //}
      await _custNotificationHandler.AddSupportTicketFromCustomerNotificationAsync(businessSupportNotificationDTO);
    }

    private async Task SendUpdateCustomerTicketNotificationAsync(SupportAddUpdateDTO supportAddUpdateDTO, Dictionary<string, string> notificationOldData, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null, CancellationToken token = default(CancellationToken)) {

      // Get all the entities needed to generate the notifications  data.     
      BusinessSupportNotificationDTO businessSupportNotificationDTO = new BusinessSupportNotificationDTO();
      businessSupportNotificationDTO = await base.GetBusinessSupportNotificationData(supportAddUpdateDTO, token);
      SupportTicket supportTicket = Get((Guid)supportAddUpdateDTO.SupportTicketId);

      businessSupportNotificationDTO.NewAssignee = notificationOldData["newAssignedTo"];
      businessSupportNotificationDTO.OldStatus = notificationOldData["oldStatus"];
      businessSupportNotificationDTO.NewStatus = ((SupportPriorityEnum)businessSupportNotificationDTO.Status).ToString();
      businessSupportNotificationDTO.OldPriority = notificationOldData["oldPriority"];
      businessSupportNotificationDTO.NewPriority = ((SupportPriorityEnum)businessSupportNotificationDTO.Priority).ToString();

      if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
        SupportCommentDTO newComment = supportAddUpdateDTO.SupportCommentList.FirstOrDefault(i => i.OperationType == (int)OperationType.Add);
        if(newComment != null) {
          businessSupportNotificationDTO.CommentText = newComment.CommentText;
        }
        else
          businessSupportNotificationDTO.CommentText = "";
      }


      //if(supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0) {
      //  businessSupportNotificationDTO.CommentText = supportAddUpdateDTO.SupportCommentList[0].CommentText;
      //}
      //else
      //  businessSupportNotificationDTO.CommentText = "";

      if(documentModel != null && documentModel.HasFiles) {
        businessSupportNotificationDTO.Count = httpRequest.Form.Files.Count.ToString();
      }
      else {
        businessSupportNotificationDTO.Count = "0";
      }
      businessSupportNotificationDTO.UserSessionInfo = _userSessionDS.GetSession();
      businessSupportNotificationDTO.IsPublisherAssinged = false;

      await _custNotificationHandler.SendCustomerSupportNotificationAsync(businessSupportNotificationDTO);

    }
    #endregion Customer
    #endregion Notification Methods
  }
}





//private void SendAddTicketNotification(SupportAddUpdateDTO supportTicketDTO, SupportTicket supportTicket) {

//    // Get all the entities needed to generate th enotifications.
//    SupportNotificationDTO supportNotificationDTO = new SupportNotificationDTO();
//    Dictionary<string, string> notificationEvent = new Dictionary<string, string>();
//    supportNotificationDTO.UserSession = _userSessionDS.GetSession();
//    Tenant Tenant = _tenantDataService.Get(supportNotificationDTO.UserSession.TenantId);
//    TenantUser appUser = _appUserDS.Get(supportTicket.CreatedBy);

//    // Create the notification dTO for the notification.
//    supportNotificationDTO.PublisherName = "eworkplace";
//    supportNotificationDTO.AppName = _appDataService.Find(a => a.AppKey == ApplicationKey.ToString()).Name;
//    supportNotificationDTO.SupportTicket = supportTicket;
//    if(supportTicketDTO.SupportCommentList != null && supportTicketDTO.SupportCommentList.Count > 0) {
//        supportNotificationDTO.SupportCommentDTO = supportTicketDTO.SupportCommentList.First();
//    }

//    if(supportTicket.GenerationLevel == (int)SupportLevelEnum.Level1) {
//        //ToDo: Get Customer from new table structure.
//        BACustomer customer = null;// _customerDataService.Get(appUser.ParentRefId);
//        if(customer != null) {
//            notificationEvent.Add("customerName", customer.CustomerName);
//            notificationEvent.Add("customerRefId", customer.ERPCustomerKey);
//        }
//        else {
//            notificationEvent.Add("customerName", "");
//            notificationEvent.Add("customerRefId", "");
//        }
//        notificationEvent.Add("customerUserName", appUser.FullName);
//        notificationEvent.Add("tenantName", Tenant.Name);
//        notificationEvent.Add("companyid", Tenant.IdentityNumber);
//        supportNotificationDTO.NotificationEventData = notificationEvent;
//        _supportNotificationHandler.AddLevel1TicketAsync(supportNotificationDTO);
//    }
//    else if(supportTicket.GenerationLevel == (int)SupportLevelEnum.Level2) {
//        notificationEvent.Add("tenantName", Tenant.Name);
//        notificationEvent.Add("tenantIdentity", Tenant.IdentityNumber);
//        notificationEvent.Add("createdBy", appUser.FullName);
//        supportNotificationDTO.NotificationEventData = notificationEvent;
//        // Send notitfication.
//        _supportNotificationHandler.AddLevel2TicketAsync(supportNotificationDTO);
//    }
//}

//private List<SupportNotificationEventEnum> GetNotificationEvents(SupportAddUpdateDTO supportAddUpdateDTO, SupportTicket supportTicket)
//{
//  List<SupportNotificationEventEnum> eventList = new List<SupportNotificationEventEnum>();
//  // Find priority is changed.
//  if (supportTicket.Priority != supportAddUpdateDTO.Priority)
//  {
//    eventList.Add(SupportNotificationEventEnum.TicketPriorityChanged);
//  }
//  // Find status is changed
//  if (supportTicket.Status != supportAddUpdateDTO.Status)
//  {
//    eventList.Add(SupportNotificationEventEnum.TicketStatusChanged);
//  }
//  // Find comment is added.
//  if (supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0)
//  {
//    foreach (var item in supportAddUpdateDTO.SupportCommentList)
//    {
//      if (item.OperationType == (int)OperationType.Add)
//      {
//        eventList.Add(SupportNotificationEventEnum.TicketNewCommentAdded);
//        break;
//      }
//    }
//  }
//  // Assignment and Reassignemnet cases.
//  if (supportAddUpdateDTO.CurrentLevel != supportTicket.CurrentLevel)
//  {
//    if (supportAddUpdateDTO.CurrentLevel == supportTicket.GenerationLevel)
//    {
//      eventList.Add(SupportNotificationEventEnum.TicketReassigned);
//    }
//    else
//    {
//      int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, supportAddUpdateDTO.CurrentLevel);
//      if (supportLevelChange == 0)
//      {
//        eventList.Add(SupportNotificationEventEnum.TicketIsAssigned);
//      }
//      else
//      {
//        eventList.Add(SupportNotificationEventEnum.TicketReassigned);
//      }
//    }
//  }
//  return eventList;
//}





//    private async Task SendUpdateNotificationAsync(SupportAddUpdateDTO supportAddUpdateDTO, SupportNotificationEventEnum eventValue, Dictionary<string, string> notificationOldData)
//    {
//      // Get all the entities needed to generate the notifications  data.     
//      BusinessSupportNotificationDTO businessSupportNotificationDTO = new BusinessSupportNotificationDTO();
//      SupportTicket supportTicket = Get((Guid)supportAddUpdateDTO.SupportTicketId);
//      //PublisherSetting publisherSettings = _publisherSettingDataService.GetAll().FirstOrDefault();

//      //businessSupportNotificationDTO.TicketId = supportTicket.ID;
//      //businessSupportNotificationDTO.CustomerCompanyName = "";
//      //businessSupportNotificationDTO.BusinessCompanyName = "";
//      //businessSupportNotificationDTO.PublisherCompanyName = "";
//      //businessSupportNotificationDTO.AppName = "";
//      //businessSupportNotificationDTO.ModifiedOn = DateTime.UtcNow;
//      //businessSupportNotificationDTO.ModifiedBy = _userSessionDS.GetSession().UserName;
//      businessSupportNotificationDTO.NewAssignee = notificationOldData["newAssignedTo"];
//      businessSupportNotificationDTO.OldStatus = notificationOldData["oldStatus"];
//      businessSupportNotificationDTO.NewStatus = notificationOldData["newStatus"];
//      businessSupportNotificationDTO.OldPriority = notificationOldData["oldPriority"];
//      businessSupportNotificationDTO.NewPriority = notificationOldData["newPriority"];
//      businessSupportNotificationDTO.CommentText = "Comment";
//      businessSupportNotificationDTO.Count = "2";
//      businessSupportNotificationDTO.UserSessionInfo = _userSessionDS.GetSession();
//      int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, (int)SupportLevelEnum.Level3);
//      if (supportLevelChange == 0)
//      {
//        businessSupportNotificationDTO.IsPublisherAssinged = false;
//      }
//      else
//      {
//        businessSupportNotificationDTO.IsPublisherAssinged = true;
//      }




//      //Tenant Tenant = _tenantDataService.Get(supportAddUpdateDTO.TenantId);
//      //TenantUser createrAppUser = _appUserDS.Get(supportTicket.CreatedBy);
//      //TenantUser modifierAppUser = _appUserDS.Get(supportTicket.UpdatedBy);
//      //App app = _appDataService.Find(a => a.AppKey == ApplicationKey.ToString());


//      // Dictionary<string, string> eventData = new Dictionary<string, string>();
//      // Create the notification dTO for the notification.
//      //supportNotificationDTO.AppName = _appDataService.Find(a => a.AppKey == ApplicationKey.ToString()).Name;
//      //supportNotificationDTO.AppId = app.ID;
//      //supportNotificationDTO.PublisherName = "eworkplace";
//      //int supportLevelChange = SupportTicketAssignedToLevel3(supportTicket.ID, (int)SupportLevelEnum.Level3);
//      //if (supportLevelChange == 0)
//      //{
//      //  supportNotificationDTO.IsPublisherAssinged = false;
//      //}
//      //else
//      //{
//      //  supportNotificationDTO.IsPublisherAssinged = true;
//      //}
//      //if (supportAddUpdateDTO.SupportCommentList != null && supportAddUpdateDTO.SupportCommentList.Count > 0)
//      //{
//      //  int countComment = supportAddUpdateDTO.SupportCommentList.Count;
//      //  supportNotificationDTO.SupportCommentDTO = supportAddUpdateDTO.SupportCommentList[countComment - 1];
//      //}
//      //supportNotificationDTO.SupportTicket = supportTicket;
//      //supportNotificationDTO.UserSession = _userSessionDS.GetSession();

//      ////ToDo: Uncomment this code and use new table strucutre instead of ParentRefId.
//      //// Common event data collection.
//      //if(createrAppUser.ParentRefId != null && createrAppUser.ParentRefId != Guid.Empty) {
//      //  Customer customer = _customerDataService.Get(createrAppUser.ParentRefId);
//      //  if(customer != null) {
//      //    eventData.Add("customerName", customer.Name);
//      //    eventData.Add("customerId", customer.CustomerRefId);
//      //  }
//      //}
//      //else {
//      //  eventData.Add("customerName", "");
//      //  eventData.Add("customerId", "");
//      //}

////comented by anil 
//      //eventData.Add("companyName", Tenant.Name);
//      //eventData.Add("companyId", Tenant.IdentityNumber);
//      //eventData.Add("createdBy", createrAppUser.FullName);
//      //eventData.Add("modifiedBy", modifierAppUser.FullName);

//      await _bizNotificationHandler.SendBusinessSupportNotificationAsync(businessSupportNotificationDTO);

//      // Depending upon event send notification after adding event specific data.
//      switch (eventValue)
//      {
//        //case SupportNotificationEventEnum.TicketIsAssigned:
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.SendBusinessSupportNotificationAsync(supportNotificationDTO);
//        //  break;
//        //case SupportNotificationEventEnum.TicketStatusChanged:
//        //  eventData.Add("oldStatus", notificationOldData["oldStatus"]);
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.SendBusinessSupportNotificationAsync(supportNotificationDTO);
//        //  break;
//        //case SupportNotificationEventEnum.TicketPriorityChanged:
//        //  eventData.Add("oldPriority", notificationOldData["oldPriority"]);
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.SendBusinessSupportNotificationAsync(supportNotificationDTO);
//        //  break;
//        //case SupportNotificationEventEnum.TicketReassigned:
//        //  eventData.Add("oldAssignedTo", notificationOldData["oldAssignedTo"]);
//        //  eventData.Add("newAssignedTo", notificationOldData["newAssignedTo"]);
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.SendBusinessSupportNotificationAsync(supportNotificationDTO);
//        //  break;
//        //case SupportNotificationEventEnum.TicketNewCommentAdded:
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.CommentIsAddToTicketAsync(supportNotificationDTO);
//        //  break;
//        //case SupportNotificationEventEnum.TicketNewAttachmentAdded:
//        //  //TODO: Get the attchment count
//        //  eventData.Add("attachmentCount", "2");
//        //  supportNotificationDTO.NotificationEventData = eventData;
//        //  await _bizNotificationHandler.AttachmentIsAddedToTicketAsync(supportNotificationDTO);
//        //  break;
//        default:
//          break;
//      }
//    }


