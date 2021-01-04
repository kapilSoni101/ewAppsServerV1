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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS {

    public class PlatformSupportTicketDSNew : SupportTicketDSNew, IPlatformSupportTicketDSNew
  {

    IUserSessionManager _userSessionDS = null;

    /// <summary>Initializes a new instance of the <see cref="PaymentSupportTicketDS"/> class member variables and dependencies..</summary>
    /// <param name="identityDS">An instance of <see cref="IIdentityDS"/> to generate system generated numbers..</param>
    /// <param name="userSessionDS">An instance of <see cref="IUserSessionManager"/> to get requester user's information.</param>   
    /// <param name="supportCommentDS">An instance of <see cref="ISupportCommentDS"/> to execute support comment related operations.</param>
    /// <param name="levelTransitionDS">An instance of <see cref="ILevelTransitionHistoryDS"/> to execute level/status related operations.</param>
    /// <param name="userSessionManager">The user session manager.</param>       
    /// <param name="cacheService">The cache service.</param>
    /// <param name="documentDS">An instance of <see cref="IDMDocumentDS"/> to execute document related operations.</param>
    /// <param name="supportTicketAssigneeHelper">An instance of <see cref="ISupportTicketAssigneeHelper"/> to get support ticket assignee information.</param>
    /// <param name="supportTicketRepository">An instance of <see cref="ISupportTicketRepository"/> to execute support ticket related operations.</param>
    public PlatformSupportTicketDSNew(IUniqueIdentityGeneratorDS identityDS,
      ISupportCommentDS supportCommentDS, ILevelTransitionHistoryDS levelTransitionDS, IUserSessionManager userSessionManager,
       IDMDocumentDS documentDS, ISupportTicketAssigneeHelper supportTicketAssigneeHelper, ISupportTicketRepository supportTicketRepository)
      : base(identityDS,supportTicketRepository, supportCommentDS, levelTransitionDS, userSessionManager,  documentDS, supportTicketAssigneeHelper)
    {
      _userSessionDS = userSessionManager;

      base.AppSupportLevel = SupportLevelEnum.Level4;
     // base.ApplicationKey = AppKeyEnum.plat;
    }

    /// <inheritdoc/>
    public bool UpdatePlatformSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest)
    {
      return base.UpdateSupportTicket(supportTicketDTO, SupportLevelEnum.Level4, documentModel, httpRequest);
    }

    /// <inheritdoc/>
    public async Task <List<SupportTicketDTO>> GetSupportTicketAssignedToLevel4List(bool includeDeleted, CancellationToken token = default(CancellationToken))
    {
      return await base.GetSupportTicketAssignedToLevel4List(_userSessionDS.GetSession().TenantId, includeDeleted, token);
    }

  }
}
