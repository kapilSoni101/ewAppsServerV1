using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Shipment.Common {


  /// <summary>
  /// Email preferece of the business user for the shipment application.
  /// </summary>
  [System.Flags]
  public enum ShipmentBusinessEmailPreferenceEnum:long {

    /// <summary>
    /// No prefernces.
    /// </summary>
    None = 0,

    /// <summary>
    /// New Customer OnBoard.
    /// </summary>
    NewCustomerOnBoard = 1,

    /// <summary>
    /// Customer Account Status Changed.
    /// </summary>
    CustomerAccountStatusChanged = 2,

    /// <summary>
    /// New Sales Order Created.
    /// </summary>
    NewSalesOrderCreated = 4,

    /// <summary>
    /// Sales Order quote generated
    /// </summary>
    SalesOrderUpdated = 8,

    /// <summary>
    /// Sales Order delivery initiated
    /// </summary>
    SalesOrderDeliveryInitiated = 16,

    /// <summary>
    /// Sales Order is closed
    /// </summary>
    SalesOrderClosed = 32,

    /// <summary>
    /// New Delivery initiated
    /// </summary>
    NewDeliveryInitiated = 64,

    /// <summary>
    /// Scheduled Delivery is cancelled
    /// </summary>
    ScheduledDeliveryCancelled = 128,

    /// <summary>
    /// Delivery tracking status is changed
    /// </summary>
    DeliveryTrackingStatusChanged = 265,

    /// <summary>
    /// New Item added
    /// </summary>
    NewItemAdded = 512,

    /// <summary>
    /// Item status is changed
    /// </summary>
    ItemStatusChanged = 1024,

    /// <summary>
    ///  Existing Item is updated
    /// </summary>
    ExistingItemUpdated = 2048,


    /// <summary>
    /// New Packaging added
    /// </summary>
    NewPackagingAdded = 4098,

    /// <summary>
    /// Packaging status is changed
    /// </summary>
    PackagingStatusChanged = 8196,

    /// <summary>
    /// Existing Packaging is updated
    /// </summary>
    ExistingPackagingUpdated = 16392,

    /// <summary>
    /// My permissions changed
    /// </summary>
    MyPermissionsChanged = 32784,

    /// <summary>
    /// My user account status changed
    /// </summary>
    MyUserAccountStatusChanged = 65568,

    /// <summary>
    /// New portal user joined
    /// </summary>
    NewPortalUserJoined = 131136,

    /// <summary>
    /// My ticket status is changed
    /// </summary>
    MyTicketStatusChanged = 262272,

    /// <summary>
    /// My ticket is reassigned
    /// </summary>
    MyTicketReassigned = 524544,

    /// <summary>
    /// My ticket priority is changed
    /// </summary>
    MyTicketPriorityChanged = 1049088,

    /// <summary>
    ///  New comment added in my ticket.
    /// </summary>
    MyTicketNewCommentAdded = 2098176,

    /// <summary>
    /// New attachment added in my ticket
    /// </summary>
    MyTicketNewAttachmentAdded = 4196352,

    /// <summary>
    /// Customer tickets status is changed.
    /// </summary>
    CustomerTicketsStatusChanged = 8392704,

    /// <summary>
    ///  Customer ticket is reassigned.
    /// </summary>
    CustomerTicketReassigned = 16785408,

    /// <summary>
    ///  Customer ticket priority is changed.
    /// </summary>
    CustomerTicketPriorityChange = 33570816,

    /// <summary>
    /// New comment added in customers ticket
    /// </summary>
    CustomerTicketNewCommentAdded = 67141632,

    /// <summary>
    ///  New attachment added in customer ticket.
    /// </summary>
    CustomerTicketNewAttachmentAdded = 134283264,

    /// <summary>
    /// All the perfrences are on.
    /// </summary>
    All = None | NewCustomerOnBoard | CustomerAccountStatusChanged | NewSalesOrderCreated | SalesOrderUpdated
        | SalesOrderDeliveryInitiated | SalesOrderClosed | NewDeliveryInitiated | ScheduledDeliveryCancelled |
        DeliveryTrackingStatusChanged | NewItemAdded | ItemStatusChanged | ExistingItemUpdated |
        NewPackagingAdded | PackagingStatusChanged | ExistingPackagingUpdated | MyPermissionsChanged |
        MyUserAccountStatusChanged | NewPortalUserJoined | MyTicketStatusChanged | MyTicketReassigned |
        MyTicketPriorityChanged | MyTicketNewCommentAdded | MyTicketNewAttachmentAdded | CustomerTicketsStatusChanged |
        CustomerTicketReassigned | CustomerTicketPriorityChange | CustomerTicketNewCommentAdded | CustomerTicketNewAttachmentAdded
  }
}