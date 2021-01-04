///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 10 October 2018
// */

//using System;
//using System.Collections.Generic;
//using ewApps.Core.NotificationService;
//using ewApps.Payment.DS;
//using ewApps.Publisher.DS;
//using ewApps.Support.Common;

//namespace ewApps.AppPortal.DS {

//  /// <summary>DataService implementing Support notification receipent service</summary>
//  public class SupportNotificationReceipentDataService :ISupportNotificationReceipentDataService {

//        #region Local member

//        //IPaymentNotificationRecipientDataService _paymentNotificationRecipientDataService;
//        IPaymentBusinessNotificationRecipientDS _paymentNotificationRecipientDataService;
//        IPublisherNotificationRecipientDataService _publisherNotificationRecipientDataService;

//    #endregion Local member

//    #region Construnctor

//    /// <summary>
//    /// Initializing the interfaces.
//    /// </summary>
//    /// <param name="paymentNotificationRecipientDataService"></param>
//    /// <param name="publisherNotificationRecipientDataService"></param>
//    public SupportNotificationReceipentDataService(IPaymentBusinessNotificationRecipientDS paymentNotificationRecipientDataService, IPublisherNotificationRecipientDataService publisherNotificationRecipientDataService) {
//      _paymentNotificationRecipientDataService=paymentNotificationRecipientDataService;
//      _publisherNotificationRecipientDataService=publisherNotificationRecipientDataService;
//    }

//    #endregion

//    ///<inheritdoc/>
//    public List<NotificationRecipient> PartnerAddTicketNotificationRecepientList(Guid tenantId, Guid appId) {
//      return _paymentNotificationRecipientDataService.GetBusinessSupporUserList(tenantId, appId);
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> BusinessAddTicketNotificationRecepientList() {
//      return _publisherNotificationRecipientDataService.GetPublisherSupportUsers();
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> CustomerTicketForwardedToPublisherNotificationReceipentList() {
//      return _publisherNotificationRecipientDataService.GetPublisherSupportUsers();
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> TicketStatusChangedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel) {

//      // Declare the receipent list.      
//      List<NotificationRecipient> receipentList = new List<NotificationRecipient>();

//      // Bussiness ticket.
//      // Get Publisher users.
//      // Get business Owner User.
//      if (supportLevel==(int)SupportLevelEnum.Level2) {
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketStatusChange(tenantId, appId, supportLevel, appUserId));
//        receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketStatusPreference());
//      }

//      // Business Partner ticket.
//      else if (supportLevel==(int)SupportLevelEnum.Level1) {
//        if (isPublisherAssigned) {
//          receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketStatusPreference());
//        }
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketStatusChange(tenantId, appId, supportLevel, appUserId));
//      }
//      return receipentList;
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> TicketPriorityChangedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel) {

//      // Declare the receipent list.      
//      List<NotificationRecipient> receipentList = new List<NotificationRecipient>();

//      // Bussiness ticket.
//      // Get Publisher users.
//      // Get business Owner User.
//      if (supportLevel==(int)SupportLevelEnum.Level2) {
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketPriorityChange(tenantId, appId, supportLevel, appUserId));
//        receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketPriorityChangePreference());
//      }

//      // Business Partner ticket.
//      else if (supportLevel==(int)SupportLevelEnum.Level1) {
//        if (isPublisherAssigned) {
//          receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketPriorityChangePreference());
//        }
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketPriorityChange(tenantId, appId, supportLevel, appUserId));
//      }
//      return receipentList;
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> TicketReassingedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel) {

//      // Declare the receipent list.      
//      List<NotificationRecipient> receipentList = new List<NotificationRecipient>();

//      // Bussiness ticket.
//      // Get Publisher users.
//      // Get business Owner User.
//      if (supportLevel==(int)SupportLevelEnum.Level2) {
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketReassinged(tenantId, appId, supportLevel, appUserId));
//        receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketReassingedPreference());
//      }

//      // Business Partner ticket.
//      else if (supportLevel==(int)SupportLevelEnum.Level1) {
//        if (isPublisherAssigned) {
//          receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithTicketReassingedPreference());
//        }
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForTicketReassinged(tenantId, appId, supportLevel, appUserId));
//      }
//      return receipentList;
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> TicketNewCommentAddedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel) {

//      // Declare the receipent list.      
//      List<NotificationRecipient> receipentList = new List<NotificationRecipient>();

//      // Bussiness ticket.
//      // Get Publisher users.
//      // Get business Owner User.
//      if (supportLevel==(int)SupportLevelEnum.Level2) {
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForCommentAddedToTicket(tenantId, appId, supportLevel, appUserId));
//        receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithCommentAddedToTicketPreference());
//      }

//      // Business Partner ticket.
//      else if (supportLevel==(int)SupportLevelEnum.Level1) {
//        if (isPublisherAssigned) {
//          receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithCommentAddedToTicketPreference());
//        }
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForCommentAddedToTicket(tenantId, appId, supportLevel, appUserId));
//      }
//      return receipentList;
//    }

//    ///<inheritdoc/>
//    public List<NotificationRecipient> TicketNewAttachmentAddedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel) {

//      // Declare the receipent list.      
//      List<NotificationRecipient> receipentList = new List<NotificationRecipient>();

//      // Bussiness ticket.
//      // Get Publisher users.
//      // Get business Owner User.
//      if(supportLevel == (int)SupportLevelEnum.Level2) {
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForCommentAddedToTicket(tenantId, appId, supportLevel, appUserId));
//        receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithAttcahmentAddedToTicketPreference());
//      }

//      // Business Partner ticket.
//      else if(supportLevel == (int)SupportLevelEnum.Level1) {
//        if(isPublisherAssigned) {
//          receipentList.AddRange(_publisherNotificationRecipientDataService.GetPublisherSupportUsersWithAttcahmentAddedToTicketPreference());
//        }
//        receipentList.AddRange(_paymentNotificationRecipientDataService.GetBusinessAndPartnerSupportUserListForCommentAddedToTicket(tenantId, appId, supportLevel, appUserId));
//      }
//      return receipentList;
//    }

//  }
//}
