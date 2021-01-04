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

namespace ewApps.AppPortal.Data
{
  /// <summary>
  /// This interface defines repository methods to get <see cref="ewApps.Core.Entity.SupportTicket"/> entity related data.
  /// </summary>
  /// <seealso cref="ewApps.Core.Entity.SupportTicket"/>
  public interface ISupportTicketRepository : IBaseRepository<SupportTicket>
  {

    /// <summary>
    /// Gets <see cref="ewApps.Core.Entity.SupportTicket"/> entity list in form of <see cref="ewApps.Core.DTO.SupportMyTicketDTO"/> 
    /// based on requested application, tenant, ticket's generation level, ticket creater user and customer (if applicable).
    /// </summary>
    /// <param name="appKey">Requester application key.</param>
    /// <param name="tenantId">Tenant id to filter specific tenant tickets.</param>
    /// <param name="generationLevel">Support generation level.</param>
    /// <param name="creatorId">Support ticket creater user id.</param>
    /// <param name="customerId">Customer id (in-case of customer support ticket).</param>
    /// <param name="onlyDeleted">If true return only deleted support ticket otherwise return non-deleted support tickets.</param>
    /// <returns>Returns List&lt;SupportMyTicketDTO&gt; based on input parameters.</returns>
    Task<List<SupportMyTicketDTO>> GetUserSupportTicketByCreatorAndCustomerAndTenantId(string appKey, Guid tenantId, int generationLevel, Guid creatorId, Guid? customerId, bool onlyDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Gets <see cref="ewApps.Core.Entity.SupportTicket"/> entity list in form of <see cref="ewApps.Core.DTO.SupportTicketDTO"/> 
    /// based on requested application, tenant, ticket's generation level.
    /// </summary>
    /// <param name="appKey">Requester application key.</param>
    /// <param name="tenantId">Tenant id to filter specific tenant tickets.</param>
    /// <param name="generationLevel">Support generation level.</param>
    /// <param name="includeDeleted">True to include all deleted items.</param>
    /// <returns>Returns List&lt;SupportTicketDTO&gt; based on input parameters.</returns>
    List<SupportTicketDTO> GetSupportTicketByAppAndTenantIdAndLevel(string appKey, Guid tenantId, short generationLevel, bool includeDeleted);

    /// <summary>
    /// Gets <see cref="ewApps.Core.Entity.SupportTicket"/> entity list in form of <see cref="ewApps.Core.DTO.SupportTicketDTO"/> 
    /// based on support escalation level.
    /// </summary>
    /// <param name="escalationLevel">Support escalation level.</param>
    /// <param name="includeDeleted">True to include all deleted items.</param>
    /// <returns>Returns List&lt;SupportTicketDTO&gt; based on input parameters.</returns>
    List<SupportTicketDTO> GetSupportTicketByEscalationLevel(short escalationLevel, bool includeDeleted);


    /// <summary>
    /// Gets <see cref="ewApps.Core.Entity.SupportTicket"/> entity detail (with comments) in form of <see cref="ewApps.Core.DTO.SupportTicketDTO"/> 
    /// based on unique id of <see cref="ewApps.Core.Entity.SupportTicket"/>.
    /// </summary>
    /// <param name="supportId">Support ticket id.</param>
    /// <param name="includeDeleted">True to include all deleted items.</param>
    /// <returns>Returns <see cref="ewApps.Core.DTO.SupportTicketDTO"/> that matches given support ticket id.</returns>
    SupportTicketDetailDTO GetSupportTicketDetailById(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Evaluates <see cref="ewApps.Core.Entity.SupportTicket"/> is ever assigned to <see cref="ewApps.Core.Common.SupportLevelEnum.Level3"/> for requested unique id of <see cref="ewApps.Core.Entity.SupportTicket"/>.
    /// </summary>
    /// <param name="supportTicketId">Support ticket id.</param>
    /// <param name="level">Support ticket id.</param>
    /// <returns>Returns true if requested <see cref="ewApps.Core.Entity.SupportTicket"/> is ever assigned to <see cref="ewApps.Core.Common.SupportLevelEnum.Level3"/>.</returns>
    int SupportTicketAssignedToLevel3(Guid supportTicketId, int level);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appKey"></param>
    /// <param name="level2TenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    List<SupportTicketDTO> GetSupportTicketAssignedToLevel2List(string appKey, Guid level2TenantId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="publisherTenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    List<SupportTicketDTO> GetSupportTicketAssignedToLevel3List(Guid publisherTenantId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="platformTenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    List<SupportTicketDTO> GetSupportTicketAssignedToLevel4List(Guid platformTenantId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level2TenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="generationLevel"></param>
    /// <returns></returns>
    List<SupportTicketDTO> GetSupportTicketAssignedToLevel2BusinessList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// 
    /// </summary>
    /// <param name="level2TenantId"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="generationLevel"></param>
    /// <returns></returns>
    List<SupportTicketDTO> GetSupportTicketAssignedToLevel2CustomerList(Guid level2TenantId, bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken));

/// <summary>
/// 
/// </summary>
/// <param name="supportId"></param>
/// <param name="includeDeleted"></param>
/// <param name="token"></param>
/// <returns></returns>
    BusinessSupportNotificationDTO GetBusinessSupportNotificationData(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken));

   





  }
}
