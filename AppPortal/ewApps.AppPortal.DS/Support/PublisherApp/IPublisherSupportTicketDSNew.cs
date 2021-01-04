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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {
    public interface IPublisherSupportTicketDSNew:ISupportTicketDSNew {

        SupportTicket AddPublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    bool UpdatePublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

        Task <List<SupportMyTicketDTO>> GetPublisherMyTicketList(Guid tenantId, Guid creatorId, bool onlyDeleted, string appKey, CancellationToken token = default(CancellationToken));

        Task <List<SupportTicketDTO>> GetSupportTicketAssignedToLevel3List(bool includeDeleted, CancellationToken token = default(CancellationToken));

        SupportLevelEnum AppSupportLevel {
            get;
            set;
        }

        //AppKeyEnum ApplicationKey {
        //    get;
        //    set;
        //}
    }
}