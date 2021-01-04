///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 10 October 2018
// */

//using System;
//using System.Collections.Generic;
//using ewApps.Core.NotificationService;

//namespace ewApps.AppPortal.DS
//{

//  /// <summary>Exposing Payment Email recipient dataservice all the recepient related methods</summary>
//  public interface IPaymentNotificationRecipientDataService
//  {

//    /// <summary>Get invited business user list</summary>
//    /// <param name="tenantId">Tenant id</param>
//    /// <param name="appUserId">AppUserId</param>
//    List<NotificationRecipient> GetInvitedBusinessUserRecipientList(Guid tenantId, Guid appUserId);

//    /// <summary>Get invited business user list</summary>
//    /// <param name="tenantId">Tenant id</param>
//    /// <param name="appUserId">AppUserId</param>
//    /// <param name="applicationId">AppUserId</param>
//    List<NotificationRecipient> GetPartnerUserRecipientList(Guid tenantId, Guid appUserId, Guid applicationId);

//    /// <summary>Get all the business support users</summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    List<NotificationRecipient> GetBusinessSupporUserList(Guid tenantId, Guid appId);

//    /// <summary>
//    /// Gets the receipetnt list with prefrence for ticket status changed
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="level"></param>
//    /// <param name="appUserId">Owner user</param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetBusinessAndPartnerSupportUserListForTicketStatusChange(Guid tenantId, Guid appId, int level, Guid appUserId);

//    /// <summary>
//    /// Gets the receipetnt list with prefrence for ticket is reassingned.
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="level"></param>
//    /// <param name="appUserId">Owner user</param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetBusinessAndPartnerSupportUserListForTicketReassinged(Guid tenantId, Guid appId, int level, Guid appUserId);

//    /// <summary>
//    /// Gets the receipetnt list with prefrence for ticket priority changed.
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="level"></param>
//    /// <param name="appUserId">Owner user</param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetBusinessAndPartnerSupportUserListForTicketPriorityChange(Guid tenantId, Guid appId, int level, Guid appUserId);

//    /// <summary>
//    /// Gets the receipetnt list with prefrence for commnet added to ticket.
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="level"></param>
//    /// <param name="appUserId">Owner user</param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetBusinessAndPartnerSupportUserListForCommentAddedToTicket(Guid tenantId, Guid appId, int level, Guid appUserId);

//    /// <summary>
//    /// Get the receipent list of all the user having invoice permission with preferences
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="parentRefId"></param>
//    /// <param name="paymentNotificationEvent"></param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetInvoicePermissionUserNotificationList(Guid tenantId, Guid appId, Guid parentRefId, long paymentNotificationEvent);

//    /// <summary>
//    ///  Get the receipent list of all the user having support permission with preferences
//    /// </summary>
//    /// <param name="tenantId"></param>
//    /// <param name="appId"></param>
//    /// <param name="level"></param>
//    /// <param name="appUserId"></param>
//    /// <returns></returns>
//    List<NotificationRecipient> GetBusinessAndPartnerSupportUserListForAttchmentAddedToTicket(Guid tenantId, Guid appId, int level, Guid appUserId);
//  }
//}
