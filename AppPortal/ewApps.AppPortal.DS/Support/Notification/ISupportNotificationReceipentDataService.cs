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
using ewApps.Core.NotificationService;

namespace ewApps.AppPortal.DS {

  /// <summary>Interface exposing Support notification receipent service</summary>
  public interface ISupportNotificationReceipentDataService {

    /// <summary>
    /// Get all the user recepients for the partner ticket.
    /// </summary>   
    List<NotificationRecipient> PartnerAddTicketNotificationRecepientList(Guid tenantId, Guid appId);

    /// <summary>
    /// Get all the user recepients for the business ticket.
    /// </summary>
    List<NotificationRecipient> BusinessAddTicketNotificationRecepientList();

    /// <summary>
    /// Get all the user recepients for the ticket forwarded to publisher.
    /// </summary>
    List<NotificationRecipient> CustomerTicketForwardedToPublisherNotificationReceipentList();

    /// <summary>
    /// Get all the user recepients for the ticket status is changed.
    /// </summary>
    List<NotificationRecipient> TicketStatusChangedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel);

    /// <summary>
    /// Get all the user recepients for the ticket priority is changed.
    /// </summary>
    List<NotificationRecipient> TicketPriorityChangedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel);

    /// <summary>
    /// Get all the user recepients for the ticket is reassigned.
    /// </summary>
    List<NotificationRecipient> TicketReassingedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel);

    /// <summary>
    /// Get all the user recepients for the ticket new comment added.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="isPublisherAssigned"></param>
    /// <param name="appUserId"></param>
    /// <param name="supportLevel"></param>
    /// <returns></returns>
    List<NotificationRecipient> TicketNewCommentAddedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel);

    /// <summary>
    ///  Get all the user recepients for the ticket new attachment added.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="appUserId"></param>
    /// <param name="isPublisherAssigned"></param>
    /// <param name="supportLevel"></param>
    /// <returns></returns>
    List<NotificationRecipient> TicketNewAttachmentAddedNotificationReceipentList(Guid tenantId, Guid appId, Guid appUserId, bool isPublisherAssigned, int supportLevel);
  }
}
