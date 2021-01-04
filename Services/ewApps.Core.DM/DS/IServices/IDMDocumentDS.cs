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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Http;

namespace ewApps.Core.DMService {

    /// <summary>
    /// This interface contains various operational methods of document entity. 
    /// </summary>
    public interface IDMDocumentDS:IBaseDS<DMDocument> {

        /// <summary>
        /// Adds the document to storage.
        /// </summary>
        /// <param name="model">Doc model to add</param>
        /// <param name="httpRequest">http request object to get file list.</param>
        /// <returns>Returns file name.</returns>
        string AddDocumentToStorage(AddUpdateDocumentModel model, HttpRequest httpRequest);

        Guid UploadDocumentFileToStorage(Stream fStream, AddUpdateDocumentModel doc, bool useDocumentIdAsStrorageId);

        /// <summary>
        /// Gets the list of documents with for given ticket id.
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

        /// <summary>
        /// Gets the filestream by given ids.
        /// </summary>
        /// <param name="documentId">DocumentId</param>
        /// <param name="storageFileId">storage file id .</param>
        /// <param name="fileName">Name of file to get stream.</param>
        /// <param name="tenantId">tenant id for Doc entity</param>
        /// <returns>Gets the file stream by given parameters.</returns>
        Task<FileStream> GetFileStream(Guid documentId, Guid storageFileId, string fileName, Guid tenantId);

        /// <summary>
        /// To delete the file.
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteFileAsync(Guid storageId, CancellationToken token = default(CancellationToken));
    }
}
