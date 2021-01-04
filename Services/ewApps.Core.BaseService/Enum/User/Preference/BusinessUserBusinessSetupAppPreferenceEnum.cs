using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum BusinessUserBusinessSetupAppPreferenceEnum:long {

        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        NewBusinessUserOnboard = 1,
        BusinessUserAccountStatusIsChanged = 2,

        NewCustomerIsCreated = 4,
        ExistingCustomerIsUpdated = 8,
        NewVendorIsCreated = 16,
        ExistingVendorIsUpdated = 32,

        ApplicationSubscriptionIsupdated = 64,
        ApplicationSetupIsUpdated = 128,

        MyPermissionUpdated = 256,
        ApplicationAccessUpdatedForMe = 512,

        NewTicketRaisedByCustomer = 1024,
        CustomerExistingTicketIsUpdated = 2048,
        NewTicketRaisedByVendor = 4096,
        VendorExistingTicketIsUpdated = 8192,
        NewTicketRaisedByBusinessUser = 16384,
        BusinessUserExistingTicketIsUpdated = 32768,
        MyTicketIsUpdated = 65536,

        //AllAPPreference = NewBusinessUserOnboard | BusinessUserAccountStatusIsChanged | ApplicationSubscriptionIsupdated |
        //    ApplicationSetupIsUpdated | MyPermissionUpdated | ApplicationAccessUpdatedForMe | NewTicketRaisedByCustomer |
        //    CustomerExistingTicketIsUpdated | NewTicketRaisedByVendor | VendorExistingTicketIsUpdated | NewTicketRaisedByBusinessUser |
        //    BusinessUserExistingTicketIsUpdated | MyTicketIsUpdated,

        //AllBEPreference = NewCustomerIsCreated | ExistingCustomerIsUpdated | NewVendorIsCreated | ExistingVendorIsUpdated,

        //AllPayPreference = None,

        All = NewBusinessUserOnboard | BusinessUserAccountStatusIsChanged | NewCustomerIsCreated |
              ExistingCustomerIsUpdated | NewVendorIsCreated | ExistingVendorIsUpdated | ApplicationSubscriptionIsupdated |
              ApplicationSetupIsUpdated | MyPermissionUpdated | ApplicationAccessUpdatedForMe | NewTicketRaisedByCustomer |
              CustomerExistingTicketIsUpdated | NewTicketRaisedByVendor | VendorExistingTicketIsUpdated | NewTicketRaisedByBusinessUser |
              BusinessUserExistingTicketIsUpdated | MyTicketIsUpdated,

        AllEmail = NewBusinessUserOnboard | BusinessUserAccountStatusIsChanged | NewCustomerIsCreated |
              ExistingCustomerIsUpdated | MyPermissionUpdated | NewTicketRaisedByCustomer | CustomerExistingTicketIsUpdated
            | NewTicketRaisedByBusinessUser | BusinessUserExistingTicketIsUpdated | MyTicketIsUpdated,

        AllSMS = None,

        AllAS = AllEmail

    }
}
