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
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class provides all support ticket operations related to Level 3 (i.e. Publisher) application.
    /// </summary>
    /// <seealso cref="ewApps.AppPortal.DS.SupportTicketDS" />
    /// <seealso cref="ewApps.AppPortal.DS.IPublisherSupportTicketDS" />
    public class PublisherSupportTicketDSNew:SupportTicketDSNew, IPublisherSupportTicketDSNew {
        IUserSessionManager _userSessionDS = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="IPublisherSupportTicketDSNew"/> class member variables and dependencies..
        /// </summary>
        /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generate system generated numbers..</param>
        /// <param name="userSessionDS">An instance of <see cref="IUserSessionManager"/> to get requester user's information.</param>
        /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to execute support ticket related operations.</param>
        /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to execute support comment related operations.</param>
        /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to execute level/status related operations.</param>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="cacheService">The cache service.</param>
        /// <param name="mapper">The mapper.</param>
        public PublisherSupportTicketDSNew(IUniqueIdentityGeneratorDS identityDS, 
          ISupportTicketRepository supportTicketRepository, ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS,
          IUserSessionManager userSessionManager,  
           IDMDocumentDS documentDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper)
          : base(identityDS, supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager,  documentDS, supportTicketAssigneeHelper) {
            _userSessionDS = userSessionManager;

            base.AppSupportLevel = SupportLevelEnum.Level3;
           // base.ApplicationKey = AppKeyEnum.pub;
        }

        #region Public Methods

        public SupportTicket AddPublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
            return base.AddSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
        }

        ///<inheritdoc/>
        public bool UpdatePublisherSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest) {
            return base.UpdateSupportTicket(supportTicketDTO, AppSupportLevel, documentModel, httpRequest);
        }

        // Get My Ticket List
        public async Task <List<SupportMyTicketDTO>> GetPublisherMyTicketList(Guid tenantId, Guid creatorId, bool onlyDeleted, string appKey, CancellationToken token = default(CancellationToken)) {
           
            return await GetUserSupportTicketByCreatorAndPartnerAndTenantId(appKey, tenantId, (int)AppSupportLevel, creatorId, null, onlyDeleted, token);
        }

        public async Task <List<SupportTicketDTO>> GetSupportTicketAssignedToLevel3List(bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            return await base.GetSupportTicketAssignedToLevel3List(_userSessionDS.GetSession().TenantId, includeDeleted, token);
        }

        #endregion Public Methods

    }
}
