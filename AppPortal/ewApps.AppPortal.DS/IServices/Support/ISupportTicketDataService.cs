///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Pulkit Agarwal <pagrawal@eworkplaceapps.com>
// * Date: 19 December 2018
// * 
// * Contributor/s: Nitin Jain
// * Last Updated On: 19 December 2018
// */

//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using ewApps.Support.Common;
//using ewApps.Core.DTO;
//using ewApps.Core.Entity;
//using ewApps.Support.Entity;
//using Microsoft.AspNetCore.Http;
//using ewApps.Support.DTO;
//using ewApps.Core.DS;

//namespace ewApps.AppPortal.DS {
//    /// <summary>
//    /// This interface define methods to perform any type of operations on <see cref="ewApps.Support.Entity.SupportTicket"/> entity.
//    /// </summary> 
//    public interface ISupportTicketDS : IBaseDS<SupportTicket> {

//    /// <summary>
//    /// Adds <see cref="SupportTicket"/> and comments related to the same.
//    /// </summary>
//    /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with support ticket and comment(s) detail to be add.</param>
//    /// <param name="supportLevel">Support ticket generation level.</param>
//    /// <param name="documentModel"> Document model to upload doc.</param>
//    /// <param name="httpRequest">httpRequest parameter to get http files.</param>
//    /// <returns>Returns an instance of <see cref="SupportTicket"/> with all information of newly added ticket.</returns>
//    SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

//      /// <summary>
//      /// Updates requested support ticket and related child entities.
//      /// </summary>
//      /// <param name="supportTicketDTO">An instance of <see cref="SupportAddUpdateDTO"/> with support ticket and comment(s) detail to be add.</param>
//      /// <param name="supportLevel">Support level of requester.</param>
//      /// <returns>Returns true if all informations are updated sucessfully.</returns>
//      bool UpdateSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel);

//    /// <summary>
//    /// Deletes <see cref="SupportTicket"/> that matches given support ticket id.
//    /// </summary>
//    /// <param name="supportId">A unique support ticket id.</param>
//    void Delete(Guid supportId);

//    /// <summary>
//    /// Gets <see cref="SupportTicket"/> entity list in form of <see cref="SupportMyTicketDTO"/> 
//    /// based on requested application, tenant, ticket's generation level, ticket creater user and customer (if applicable).
//    /// </summary>
//    /// <param name="appKey">Requester application key.</param>
//    /// <param name="tenantId">Tenant id to filter specific tenant tickets.</param>
//    /// <param name="generationLevel">Support generation level.</param>
//    /// <param name="creatorId">Support ticket creater user id.</param>
//    /// <param name="customerId">Customer id (in-case of customer support ticket).</param>
//    /// <param name="onlyDeleted">If true return only deleted support ticket otherwise return non-deleted support tickets.</param>
//    /// <returns>Returns List&lt;SupportMyTicketDTO&gt; based on input parameters.</returns>
//    List<SupportMyTicketDTO> GetUserSupportTicketByCreatorAndCustomerAndTenantId(string appKey, Guid tenantId, int generationLevel, Guid creatorId, Guid? customerId, bool onlyDeleted);

//    /// <summary>
//    /// Gets all support tickets (in form of <see cref="SupportTicketDTO"/>) ever assigned to <see cref="SupportLevelEnum.Level2"/> and matches given application key and tenant id .
//    /// </summary>
//    /// <param name="appKey">Key to filter specific application support tickets.</param>
//    /// <param name="tenantId">Id to filter specific tenant's support ticket.</param>
//    /// <param name="onlyDeleted">If true return only deleted support ticket otherwise return non-deleted support tickets.</param>
//    /// <returns>Returns List&lt;SupportTicketDTO&gt; that matches input parameters.</returns>
//    List<SupportTicketDTO> GetLevel2TicketList(string appKey, Guid tenantId, bool onlyDeleted);

//    /// <summary>
//    /// Gets all support tickets (in form of <see cref="SupportTicketDTO"/>) ever assigned to <see cref="SupportLevelEnum.Level3"/>.
//    /// </summary>
//    /// <param name="onlyDeleted">If true return only deleted support ticket otherwise return non-deleted support tickets.</param>
//    /// <returns>Returns List&lt;SupportTicketDTO&gt; that are ever assigned to <see cref="SupportLevelEnum.Level3"/>.</returns>
//    List<SupportTicketDTO> GetLevel3TicketList(bool onlyDeleted);

//    /// <summary>
//    /// Gets <see cref="ewApps.Core.Entity.SupportTicket"/> entity detail (with comments) in form of <see cref="ewApps.Core.DTO.SupportTicketDTO"/> 
//    /// based on unique id of <see cref="ewApps.Core.Entity.SupportTicket"/>.
//    /// </summary>
//    /// <param name="supportId">Support ticket id.</param>
//    /// <param name="includeDeleted">True to include all deleted items.</param>
//    /// <returns>Returns <see cref="ewApps.Core.DTO.SupportTicketDTO"/> that matches given support ticket id.</returns>
//    SupportTicketDetailDTO GetSupportTicketDetailById(Guid supportId, bool includeDeleted);

//    /// <summary>
//    /// Evaluates <see cref="ewApps.Core.Entity.SupportTicket"/> is ever assigned to <see cref="ewApps.Core.Common.SupportLevelEnum.Level3"/> for requested unique id of <see cref="ewApps.Core.Entity.SupportTicket"/>.
//    /// </summary>
//    /// <param name="supportTicketId">Support ticket id.</param>
//    /// <param name="level">Support ticket id.</param>
//    /// <returns>Returns true if requested <see cref="ewApps.Core.Entity.SupportTicket"/> is ever assigned to <see cref="ewApps.Core.Common.SupportLevelEnum.Level3"/>.</returns>
//    int SupportTicketAssignedToLevel3(Guid supportTicketId, int level);

//    List<SupportTicketDTO> GetLevel4TicketList(bool onlyDeleted);
//  }
//}
