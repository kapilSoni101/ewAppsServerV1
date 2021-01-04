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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using SupportLevelEnum = ewApps.AppPortal.Common.SupportLevelEnum;

namespace ewApps.AppPortal.DS
{
  public interface ISupportTicketDSNew
  {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="supportLevel"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    SupportTicket AddSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel, HttpRequest httpRequest);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportTicketDTO"></param>
    /// <param name="supportLevel"></param>
    /// <param name="documentModel"></param>
    /// <param name="httpRequest"></param>
    /// <returns></returns>
    bool UpdateSupportTicket(SupportAddUpdateDTO supportTicketDTO, SupportLevelEnum supportLevel, AddUpdateDocumentModel documentModel = null, HttpRequest httpRequest = null);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportId"></param>
    void Delete(Guid supportId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="supportId"></param>
    /// <param name="includeDeleted"></param>
    /// <returns></returns>
    Task<SupportTicketDetailDTO> GetSupportTicketDetailById(Guid supportId, bool includeDeleted, CancellationToken token = default(CancellationToken));

    SupportLevelEnum AppSupportLevel
    {
      get; set;
    }

    /// <summary>
    /// Gets the name of the commentor.
    /// </summary>
    /// <param name="commentGenerationLevel">The comment generation level.</param>
    /// <param name="ticketGenerationLevel">The ticket generation level.</param>
    /// <param name="ticketCreatedBy">The ticket created by.</param>
    /// <param name="tenantName">Name of the tenant.</param>
    /// <param name="publisherName">Name of the publisher.</param>
    /// <returns>Returns commentor name based on input.</returns>
    string GetCommentorName(short commentGenerationLevel, short ticketGenerationLevel, string ticketCreatedBy, string tenantName, string publisherName);


  }
}