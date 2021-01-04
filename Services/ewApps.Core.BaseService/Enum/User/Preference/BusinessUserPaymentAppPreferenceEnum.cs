using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    [Flags]
    public enum BusinessUserPaymentAppPreferenceEnum:long {


        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,

        //NewBusinessUserOnboard = 1,


        /// <summary>
        /// New Customer is created.
        /// </summary>
        NewCustomerIsCreated = 1,

        /// <summary>
        /// Existing Customer is updated.
        /// </summary>
        ExistingCustomerIsUpdated = 2,

        /// <summary>
        /// Customer(s) application access updated.
        /// </summary>
        CustomerApplicationAccessUpdated = 4,

        /// <summary>
        /// New A/R Invoice is generated.
        /// </summary>
        NewARInvoiceIsGenerated = 8,

        /// <summary>
        /// Existing A/R Invoice is updated.
        /// </summary>
        ExistingARInvoiceIsUpdated = 16,

        /// <summary>
        /// New Payment is initiated.
        /// </summary>
        NewPaymentIsInitiated = 32,

        /// <summary>
        /// Transaction Status is updated.
        /// </summary>
        TransactionStatusIsUpdated = 64,

        /// <summary>
        /// Void is initiated.
        /// </summary>
        VoidIsInitiated = 128,

        /// <summary>
        /// Refund is initiated.
        /// </summary>
        RefundIsInitiated = 256,

        /// <summary>
        /// Pre – Authorization is secured.
        /// </summary>
        PreAuthorizationIsSecured = 512,

        /// <summary>
        /// Advance Payment is initiated.
        /// </summary>
        AdvancePaymentIsInitiated = 1024,

        /// <summary>
        /// New Recurring Order(s) set.
        /// </summary>
        NewRecurringOrderSet = 2048,

        /// <summary>
        /// Existing Recurring Order(s) updated.
        /// </summary>
        ExistingRecurringOrderUpdated = 4096,


        /// <summary>
        /// New Notes added.
        /// </summary>
        NewNotesAdded = 8192,


        /// <summary>
        /// My Permission(s) updated.
        /// </summary>
        MyPermissionUpdated = 16384,

        /// <summary>
        /// New Ticket raised by Customer.
        /// </summary>
        NewTicketRaisedByCustomer = 32768,


        /// <summary>
        /// Customer’s existing Ticket is updated.
        /// </summary>
        CustomersExistingTicketIsUpdated = 65536,

        /// <summary>
        /// My Ticket is updated.
        /// </summary>
        MyTicketIsUpdated = 131072,

        //AllAPPreference = CustomerApplicationAccessUpdated | NewNotesAdded | MyPermissionUpdated | NewTicketRaisedByCustomer | CustomersExistingTicketIsUpdated | MyTicketIsUpdated,

        //AllPayPreference = NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured |
        //AdvancePaymentIsInitiated,

        //AllBEPreference = NewCustomerIsCreated | ExistingCustomerIsUpdated | NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated |
        //     NewRecurringOrderSet | ExistingRecurringOrderUpdated,

        All = NewCustomerIsCreated | ExistingCustomerIsUpdated | CustomerApplicationAccessUpdated |
              NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated |
              VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated |
              NewRecurringOrderSet | ExistingRecurringOrderUpdated | NewNotesAdded | MyPermissionUpdated |
              NewTicketRaisedByCustomer | CustomersExistingTicketIsUpdated | MyTicketIsUpdated,

        AllEmail = NewCustomerIsCreated | ExistingCustomerIsUpdated | CustomerApplicationAccessUpdated |
              NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated |
              VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated |
              NewNotesAdded | MyPermissionUpdated |
              NewTicketRaisedByCustomer | CustomersExistingTicketIsUpdated | MyTicketIsUpdated,

        AllSMS = NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated |
              VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        AllAS = AllEmail

    }
}
