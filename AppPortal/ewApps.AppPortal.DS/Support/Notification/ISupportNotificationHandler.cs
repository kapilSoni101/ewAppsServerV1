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

using System.Threading.Tasks;

namespace ewApps.AppPortal.DS {

  /// <summary>Interface exposing Support notification service</summary>
  public interface ISupportNotificationHandler {

    /// <summary>
    /// Notification handler for adding business partner support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task AddLevel1TicketAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for adding business support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task AddLevel2TicketAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for adding business support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task TicketIsAssingedAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for changing the status of support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task TicketStatusIsChangedAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for changing the assingnee of support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task TicketIsReassingedAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for changing the priority of support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task TicketPriorityIsChangedAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for adding comments to the support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"> Contains all the data required for notifications.</param>
    /// <returns></returns>
    Task CommentIsAddToTicketAsync(SupportNotificationDTO supportNotificationDTO);

    /// <summary>
    /// Notification handler for adding attachment to the support ticket.
    /// </summary>
    /// <param name="supportNotificationDTO"></param>
    /// <returns></returns>
    Task AttachmentIsAddedToTicketAsync(SupportNotificationDTO supportNotificationDTO);
  }
}
