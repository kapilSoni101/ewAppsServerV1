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
  public interface ICustomerSupportTicketDSNew : ISupportTicketDSNew
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
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList(Guid tenantId, Guid creatorId, Guid? partnerId, bool onlyDeleted, string appKey, CancellationToken token = default(CancellationToken));


    SupportLevelEnum CustomerAppSupportLevel
    {
      get;
      set;
    }


  }
}