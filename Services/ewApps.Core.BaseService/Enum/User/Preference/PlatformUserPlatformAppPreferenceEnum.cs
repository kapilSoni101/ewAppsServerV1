using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {

    [System.Flags]
    public enum PlatformUserPlatformAppPreferenceEnum:long {
        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        NewPublisherOnboard = 1,
        PublisherAccountStatusIsChanged = 2,
        ApplicationIsUpdated = 4,

        ApplicationStatusIsChanged = 8,
        NewSubscriptionPlanIsCreated = 16,
        SubscriptionPlanIsUpdated = 32,
        SubscriptionPlanStatusIsChanged = 64,

        MyPermissionsChanged = 128,
        NewPortalUserJoined = 256,
        ExistingPortalUserIsDeleted = 512,
        NewTicketIsReceived = 1024,

        ExistingTicketIsUpdated = 2048,

        All = None | NewPublisherOnboard | PublisherAccountStatusIsChanged | ApplicationIsUpdated |
              ApplicationStatusIsChanged | NewSubscriptionPlanIsCreated | SubscriptionPlanIsUpdated | SubscriptionPlanStatusIsChanged |
              MyPermissionsChanged | NewPortalUserJoined | ExistingPortalUserIsDeleted | NewTicketIsReceived |
              ExistingTicketIsUpdated

    }
}
