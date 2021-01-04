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
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;

namespace ewApps.AppPortal.DS
{

  public interface IPlatformSupportTicketDSNew
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    bool UpdatePlatformSupportTicket(SupportAddUpdateDTO supportTicketDTO, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeDeleted"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<List<SupportTicketDTO>> GetSupportTicketAssignedToLevel4List(bool includeDeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportId"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SupportTicketDetailDTO> GetSupportTicketDetailById(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken));

  }

}