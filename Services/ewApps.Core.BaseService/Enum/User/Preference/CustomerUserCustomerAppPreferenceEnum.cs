using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum CustomerUserCustomerAppPreferenceEnum:long {
        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        NewQuotationIsGenerated = 1,
        ExistingQuotationIsUpdated = 2,
        NewOrderIsGenerated = 4,

        ExistingOrderIsUpdated = 8,
        NewDeliveryIsInitiated = 16,
        ExistingDeliveryIsUpdated = 32,
        NewDraftDeliveryIsGenerated = 64,

        ExistingDraftDeliveryIsUpdated = 128,
        NewContractIsCreated = 256,
        ExistingContractIsUpdated = 512,
        NewAPInvoiceIsGenerated = 1024,

        ExistingAPInvoiceIsUpdated = 2048,
        NewPaymentIsInitiated = 4096,
        TransactionStatusIsUpdated = 8192,
        VoidIsInitiated = 16384,

        RefundIsInitiated = 32768,
        PreAuthorizationIsSecured = 65536,
        AdvancePaymentIsInitiated = 131072,
        NewRecurringOrderSet = 262144,

        ExistingRecurringOrderUpdated = 524288,
        NewNotesAdded = 1048576,
        MyPermissionUpdated = 2097152,
        MyTicketIsUpdated = 4194304,

        AllAPPreference = NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

        AllPayPreference = NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured,

        AllBEPreference = NewQuotationIsGenerated | ExistingQuotationIsUpdated | NewOrderIsGenerated |
        ExistingOrderIsUpdated | NewDeliveryIsInitiated | ExistingDeliveryIsUpdated | NewDraftDeliveryIsGenerated |
        ExistingDraftDeliveryIsUpdated | NewContractIsCreated | ExistingContractIsUpdated | NewAPInvoiceIsGenerated |
        ExistingAPInvoiceIsUpdated | NewRecurringOrderSet | ExistingRecurringOrderUpdated,

        All = NewQuotationIsGenerated | ExistingQuotationIsUpdated | NewOrderIsGenerated |
          ExistingOrderIsUpdated | NewDeliveryIsInitiated | ExistingDeliveryIsUpdated | NewDraftDeliveryIsGenerated |
          ExistingDraftDeliveryIsUpdated | NewContractIsCreated | ExistingContractIsUpdated | NewAPInvoiceIsGenerated |
          ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated |
          RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated | NewRecurringOrderSet |
          ExistingRecurringOrderUpdated | NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,


        AllEmail = NewQuotationIsGenerated | ExistingQuotationIsUpdated | NewOrderIsGenerated |
          ExistingOrderIsUpdated | NewDeliveryIsInitiated | ExistingDeliveryIsUpdated | NewDraftDeliveryIsGenerated |
          ExistingDraftDeliveryIsUpdated | NewContractIsCreated | ExistingContractIsUpdated | NewAPInvoiceIsGenerated |
          ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated |
          RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated | NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

        AllSMS = NewAPInvoiceIsGenerated |
          ExistingAPInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated |
          RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        AllAS = AllEmail

    }
}
