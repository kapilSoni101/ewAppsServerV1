using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum CustomerUserPaymentAppPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,

        NewAPInvoiceIsGenerated = 1,
        ExistingAPInvoiceIsUpdated = 2,
        NewPaymentIsInitiated = 4,

        TransactionStatusIsUpdated = 8,
        VoidIsInitiated = 16,
        RefundIsInitiated = 32,
        PreAuthorizationIsSecured = 64,

        AdvancePaymentIsInitiated = 128,
        NewRecurringOrderSet = 256,
        ExistingRecurringOrderUpdated = 512,
        NewNotesAdded = 1024,

        MyPermissionUpdated = 2048,
        MyTicketIsUpdated = 4096,


        AllAPPreference = NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

        AllPayPreference = NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        AllBEPreference = NewAPInvoiceIsGenerated | ExistingAPInvoiceIsUpdated | NewRecurringOrderSet | ExistingRecurringOrderUpdated,

        All = NewAPInvoiceIsGenerated | ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated |
            TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured |
            AdvancePaymentIsInitiated | NewRecurringOrderSet | ExistingRecurringOrderUpdated |
            NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

        AllEmail = NewAPInvoiceIsGenerated | ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated |
            TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured |
            AdvancePaymentIsInitiated | NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

        AllSMS = NewAPInvoiceIsGenerated | ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated |
        VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        AllAS = AllEmail
    }
}