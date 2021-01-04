/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 26 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 26 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface ISupportTicketAssigneeHelper {
        //string GetPublisherAssignedToName(short currentLevel, Guid tenantId, Guid createdBy);

        //string GetPlatformAssignedToName(short currentLevel, Guid tenantId, Guid createdBy);

        //string GetBusinessAssignedToName(short currentLevel, Guid tenantId, Guid createdBy);

        //string GetPaymentAssignedToName(short currentLevel, Guid tenantId, Guid createdBy);

        Task <List<KeyValuePair<short, string>>> FillAssigneeList(SupportTicketDetailDTO supportTicket, SupportLevelEnum requesterLevel);

        //string GetAssigneeName(SupportTicketDTO supportTicketDetail);


       Task <string> GetAssigneeName(Guid partnerTenantId, Guid creatorTenantId, string createdByName, short currentLevel, short generationLevel);
    }
}
