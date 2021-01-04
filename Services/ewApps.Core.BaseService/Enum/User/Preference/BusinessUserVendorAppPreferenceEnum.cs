using System;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserVendorAppPreferenceEnum:long {

        None = 0,
        NewVendorIsCreated = 1,
        ExistingVendorIsUpdated = 2,
        VendorApplicationAccessUpdated = 4,

        NewPurchaseOrderIsCreated = 8,
        ExistingPurchaseOrderIsUpdated = 16,
        NewAPInvoiceIsGenerated = 32,
        ExistingAPInvoiceIsUpdated = 64,

        NewContractIsCreated = 128,
        ExistingContractIsUpdated = 256,
        NewASNIsReceived = 512,
        NewNotesAdded = 1024,

        MyPermissionUpdated = 2048,
        NewTicketRaisedByVendor = 4096,
        VendorExistingTicketIsUpdated = 8192,
        MyTicketIsUpdated = 16384,

        All = NewVendorIsCreated | ExistingVendorIsUpdated | VendorApplicationAccessUpdated |
            NewPurchaseOrderIsCreated | ExistingPurchaseOrderIsUpdated | NewAPInvoiceIsGenerated | ExistingAPInvoiceIsUpdated |
            NewContractIsCreated | ExistingContractIsUpdated | NewASNIsReceived | NewNotesAdded |
            MyPermissionUpdated | NewTicketRaisedByVendor | VendorExistingTicketIsUpdated | MyTicketIsUpdated,

        AllSMS = All,

        AllAS = All,

        AllEmail = All

    }
}
