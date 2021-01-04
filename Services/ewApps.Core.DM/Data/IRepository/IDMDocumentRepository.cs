/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.Core.BaseService;

namespace ewApps.Core.DMService {

    /// <summary>
    /// Represents all the methods to be performed on a document entity.
    /// </summary>
    public interface IDMDocumentRepository:IBaseRepository<DMDocument> {

    /// <summary>
    /// Gets the list of documents withfor given ticket id.
    /// </summary>
    /// <param name="ticketId">Ticket id for documents</param>
    /// <returns>List of document by given ticket id.</returns>
    List<DocumentResponseModel> GetDocumentsByTicketId(Guid ticketId);

    /// <summary>
    /// Gets the document detail by given doc id.
    /// </summary>
    /// <param name="documentId">Document id to get document detail.</param>
    /// <returns>Document detail for given doc id.</returns>
    Task<DocumentResponseModel> GetDocumentDetailById(Guid documentId);
  }
}
