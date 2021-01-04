using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum VendorUserVendorAppPreferenceEnum:long {


        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        NewPurchaseOrderIsReceived = 1,
        ExistingPurchaseOrderIsUpdated = 2,
        NewARInvoiceIsGenerated = 4,

        ExistingARInvoiceIsUpdated = 8,
        NewContractIsCreated = 16,
        ExistingContractIsUpdated = 32,
        NewASNIsCreated = 64,

        NewNotesAdded = 128,
        MyPermissionUpdated = 256,
        MyTicketIsUpdated = 512,


        All = None | NewPurchaseOrderIsReceived | ExistingPurchaseOrderIsUpdated | NewARInvoiceIsGenerated |
            ExistingARInvoiceIsUpdated | NewContractIsCreated | ExistingContractIsUpdated | NewASNIsCreated |
            NewNotesAdded | MyPermissionUpdated | MyTicketIsUpdated,

            AllEmail = All,
        AllSMS = None,
        AllAS = None
    }
}
