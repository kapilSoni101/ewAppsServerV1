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

namespace ewApps.AppPortal.DS {

  /// <summary>
  /// This class provides support notification events.
  /// </summary>
  [System.Flags]
  public enum SupportNotificationEventEnum:long {

    /// <summary>
    /// Add Level1 Tickets.
    /// </summary>
    AddLevel1Ticket = 1,

    /// <summary>
    /// Add level2 Tickets.
    /// </summary>
    AddLevel2Ticket = 2,

    /// <summary>
    /// Customer Ticket Forwarded To Publisher.
    /// </summary>
    TicketIsAssigned = 3,

    /// <summary>
    /// Ticket status changed.
    /// </summary>
    TicketStatusChanged = 4,

    /// <summary>
    /// Ticket is reassigned.
    /// </summary>
    TicketReassigned = 5,

    /// <summary>
    /// Ticket Priority is changed.
    /// </summary>
    TicketPriorityChanged = 6,

    /// <summary>
    /// New Comment added in Ticket.
    /// </summary>
    TicketNewCommentAdded = 7,

    /// <summary>
    /// New Attachment added in Ticket.
    /// </summary>
    TicketNewAttachmentAdded = 8,
  }
}



   

    