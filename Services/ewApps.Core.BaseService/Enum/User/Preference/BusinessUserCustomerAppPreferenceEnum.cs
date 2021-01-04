using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserCustomerAppPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        //NewBusinessUserOnboard = 1,

        NewCustomerIsCreated = 1,
        ExistingCustomerIsUpdated = 2,
        CustomerApplicationAccessUpdated = 4,

        NewSalesQuotationIsGenerated = 8,
        ExistingSalesQuotationIsUpdated = 16,
        NewSalesOrderIsGenerated = 32,
        ExistingSalesOrderIsUpdated = 64,

        NewDeliveryIsInitiated = 128,
        ExistingDeliveryIsUpdated = 256,
        NewDraftDeliveryIsGenerated = 512,
        ExistingDraftDeliveryIsUpdated = 1024,

        NewContractIsCreated = 2048,
        ExistingContractIsUpdated = 4096,
        NewARInvoiceIsGenerated = 8192,
        ExistingARInvoiceIsUpdated = 16384,

        NewPaymentIsInitiated = 32768,
        TransactionStatusIsUpdated = 65536,
        VoidIsInitiated = 131072,
        RefundIsInitiated = 262144,

        PreAuthorizationIsSecured = 524288,
        AdvancePaymentIsInitiated = 1048576,
        NewRecurringOrderSet = 2097152,
        ExistingRecurringOrderupdated = 4194304,

        NewNotesAdded = 8388608,
        MyPermissionUpdated = 16777216,
        NewTicketRaisedByCustomer = 33554432,
        CustomerExistingTicketIsUpdated = 67108864,

        MyTicketIsUpdated = 134217728,


        //AllAPPreference = CustomerApplicationAccessUpdated | NewNotesAdded | MyPermissionUpdated | NewTicketRaisedByCustomer | MyTicketIsUpdated,
        //AllPayPreference = NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated |
        //PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        //AllBEPreference = NewRecurringOrderSet | ExistingRecurringOrderupdated,

        All = NewCustomerIsCreated | ExistingCustomerIsUpdated | CustomerApplicationAccessUpdated |
            NewSalesQuotationIsGenerated | ExistingSalesQuotationIsUpdated | NewSalesOrderIsGenerated | ExistingSalesOrderIsUpdated |
            NewDeliveryIsInitiated | ExistingDeliveryIsUpdated | NewDraftDeliveryIsGenerated | ExistingDraftDeliveryIsUpdated |
            NewContractIsCreated | ExistingContractIsUpdated | NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated |
            NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated |
            PreAuthorizationIsSecured | AdvancePaymentIsInitiated | NewRecurringOrderSet | ExistingRecurringOrderupdated |
            NewNotesAdded | MyPermissionUpdated | NewTicketRaisedByCustomer | CustomerExistingTicketIsUpdated |
            MyTicketIsUpdated,
        //| NewBusinessUserOnboard

        AllEmail = NewCustomerIsCreated | ExistingCustomerIsUpdated | CustomerApplicationAccessUpdated |
            NewSalesQuotationIsGenerated | ExistingSalesQuotationIsUpdated | NewSalesOrderIsGenerated | ExistingSalesOrderIsUpdated |
            NewDeliveryIsInitiated | ExistingDeliveryIsUpdated | NewDraftDeliveryIsGenerated | ExistingDraftDeliveryIsUpdated |
            NewContractIsCreated | ExistingContractIsUpdated | NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated |
            NewPaymentIsInitiated | TransactionStatusIsUpdated | VoidIsInitiated | RefundIsInitiated |
            PreAuthorizationIsSecured | AdvancePaymentIsInitiated |
            NewNotesAdded | MyPermissionUpdated | NewTicketRaisedByCustomer | CustomerExistingTicketIsUpdated |
            MyTicketIsUpdated,

        AllSMS = NewARInvoiceIsGenerated | ExistingARInvoiceIsUpdated | NewPaymentIsInitiated | TransactionStatusIsUpdated |
        VoidIsInitiated | RefundIsInitiated | PreAuthorizationIsSecured | AdvancePaymentIsInitiated,

        AllAS = AllEmail

    }
}
