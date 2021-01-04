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
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS
{
  public interface IPaymentSupportTicketDSNew : ISupportTicketDSNew
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    SupportTicket AddCustomerSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    bool UpdateCustomerSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="creatorId"></param>
    /// <param name="partnerId"></param>
    /// <param name="onlyDeleted"></param>
    /// <param name="appKey"></param>
    /// <returns></returns>
    Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList(Guid tenantId, Guid creatorId, Guid? partnerId, bool onlyDeleted, string appKey, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    SupportTicket AddBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    bool UpdateBusinessSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="creatorId"></param>
    /// <param name="onlyDeleted"></param>
    /// <param name="appKey"></param>
    /// <returns></returns>
    Task<List<SupportMyTicketDTO>> GetBusinessMyTicketList(Guid tenantId, Guid creatorId, bool onlyDeleted, string appKey);


    // In Use
    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketId"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    int SupportTicketAssignedToLevel3(Guid supportTicketId, int level);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <param name="appKey"></param>
    /// <returns></returns>
    Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2List(bool includeDeleted, string appKey, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <param name="generationLevel"></param>
    /// <returns></returns>
    Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2BusinessList(bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <param name="generationLevel"></param>
    /// <returns></returns>
    Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel2CustomerList(bool includeDeleted, int generationLevel, CancellationToken token = default(CancellationToken));

    SupportLevelEnum BusinessAppSupportLevel
    {
      get;
      set;
    }

    SupportLevelEnum CustomerAppSupportLevel
    {
      get;
      set;
    }

  }
}