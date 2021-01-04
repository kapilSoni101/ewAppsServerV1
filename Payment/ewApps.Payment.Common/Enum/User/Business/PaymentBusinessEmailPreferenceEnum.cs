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

namespace ewApps.Payment.Common {

  /// <summary>
  /// This class provides publisher prefernces list.
  /// </summary>
  [System.Flags]
  public enum PaymentBusinessEmailPreferenceEnum:long {

    /// <summary>
    /// All the prefernece are false.
    /// </summary>
    None = 0,

    /// <summary>
    /// New Customer Onboard.
    /// </summary>
    NewCustomerOnboard = 1,

    /// <summary>
    /// Customer account status is changed
    /// </summary>
    CustomerAccountStatusChanged = 2,

    /// <summary>
    /// Invoice payment is received.
    /// </summary>
    InvoicePaymentReceived = 4,

    /// <summary>
    /// Invoice payment status is changed.
    /// </summary>
    InvoicePaymentStatusChanged = 8,

    /// <summary>
    /// My permissions changed
    /// </summary>
    MyPermissionsChanged = 16,

    /// <summary>
    /// My user account status changed.
    /// </summary>
    MyAccountStatusChanged = 32,

    /// <summary>
    /// New portal user joined.
    /// </summary>
    NewPortalUserJoined = 64,

    /// <summary>
    /// My Ticket Status is changed.
    /// </summary>
    MyTicketStatusChanged = 128,

    /// <summary>
    /// My Ticket is reassigned.
    /// </summary>
    MyTicketReassigned = 256,

    /// <summary>
    /// My Ticket Priority is changed.
    /// </summary>
    MyTicketPriorityChanged = 512,

    /// <summary>
    /// New Comment added in My Ticket.
    /// </summary>
    MyTicketNewCommentAdded = 1024,

    /// <summary>
    /// New Attachment added in My Ticket.
    /// </summary>
    MyTicketAttachmentAdded = 2048,

    /// <summary>
    /// Customer Ticket Status Changed.
    /// </summary>
    CustomerTicketStatusChanged = 4096,

    /// <summary>
    /// Customer’s Ticket is reassigned.
    /// </summary>
    CustomerTicketReassigned = 8192,

    /// <summary>
    /// Customer’s Ticket priority is changed.
    /// </summary>
    CustomerTicketPriorityChanged = 16384,

    /// <summary>
    /// New Comment added in Customer’s Ticket.
    /// </summary>
    CustomerTicketNewCommentAdded = 32768,

    /// <summary>
    /// New Attachment added in Customer’s Ticket.
    /// </summary>
    CustomerTicketAttachmentAdded = 65536,

    /// <summary>
    /// All the prefernece are true.
    /// </summary>
    All = None | NewCustomerOnboard | CustomerAccountStatusChanged | InvoicePaymentReceived | InvoicePaymentStatusChanged | MyPermissionsChanged |
    MyAccountStatusChanged | NewPortalUserJoined | MyTicketStatusChanged | MyTicketReassigned | MyTicketPriorityChanged | MyTicketNewCommentAdded | MyTicketAttachmentAdded | CustomerTicketStatusChanged | CustomerTicketReassigned
         | CustomerTicketPriorityChanged | CustomerTicketNewCommentAdded  | CustomerTicketAttachmentAdded

  }
}
