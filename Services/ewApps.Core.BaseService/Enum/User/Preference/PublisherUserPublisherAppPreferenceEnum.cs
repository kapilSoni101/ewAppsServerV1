using System;
using System.Collections.Generic;
using System.Text;

namespace ewApps.Core.BaseService {
    [Flags]
    public enum PublisherUserPublisherAppPreferenceEnum:long {
        /// <summary>
        /// All the prefernece are false.
        /// </summary>
        None = 0,
        NewBusinessCompanyOnboard = 1,
        ExistingBusinessCompanyAccountStatusIsChanged = 2,
        ExistingBusinessCompanyAccountIsDeleted = 4,

        ApplicationAccessForBusinessUpdated = 8,
        ApplicationIsUpdated = 16,
        ApplicationStatusIsChanged = 32,
        MyPermissionsChanged = 64,

        NewPortalUserJoined = 128,
        ExistingPortalUserIsDeleted = 256,
        NewTicketRaisedByBusiness = 512,
        BusinessExistingTicketIsUpdated = 1024,

        NewTicketRaisedByBusinessPartner = 2048,
        BusinessPartnerExistingTicketIsUpdated = 4096,
        MyTicketIsUpdated = 8192,

        All = None | NewBusinessCompanyOnboard | ExistingBusinessCompanyAccountStatusIsChanged | ExistingBusinessCompanyAccountIsDeleted |
              ApplicationAccessForBusinessUpdated | ApplicationIsUpdated | ApplicationStatusIsChanged | MyPermissionsChanged |
              NewPortalUserJoined | ExistingPortalUserIsDeleted | NewTicketRaisedByBusiness | BusinessExistingTicketIsUpdated |
              NewTicketRaisedByBusinessPartner | BusinessPartnerExistingTicketIsUpdated | MyTicketIsUpdated
            
    }
}
