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
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Core.DMService {

    /// <summary>
    /// This class performs CRUD database operations for document entity.
    /// </summary>
    public class DMDocumentRepository:BaseRepository<DMDocument, DMDBContext>, IDMDocumentRepository {
       

    #region Constructor 

    /// <summary>
    ///  Constructor initializing the base variables
    /// </summary>
    /// <param name="context">Core DB context instance.</param>
    /// <param name="sessionManager">Session manager instance</param>
    ///  <param name="connectionManager"></param>
    public DMDocumentRepository(DMDBContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
           
    }

    #endregion Constructor  

    #region Public Methods

    /// <inheritdoc/>
    public List<DocumentResponseModel> GetDocumentsByTicketId(Guid ticketId) {
      string sql = @"SELECT d.ID as DocumentId,d.OwnerEntityId,d.FileName,d.FileSizeinKB,d.FileExtension, d.FileStorageId, 
                  d.TenantId, '' as ThumbnailURL, '' as DocumentURL,th.Id as ThumbnailId, th.MediaType , fs.MimeType, 
                  th.FileNAme as ThumbnailFileName
                  FROM core.DMDocument d
                  INNER JOIN core.DMThumbnail th ON d.id = th.DocumentId 
                  INNER JOIN core.DMFileStorage fs ON fs.ID = d.FileStorageId
                  where  d.OwnerEntityId =@ticketId";

      SqlParameter ticketParam = new SqlParameter("ticketId", ticketId);
      object[] sqlParamArray = new object[] { ticketParam };          
          
            // Get the SQL query result in form of Document Response DTO list.
            return _context.DocumentResponseDTOQuery.FromSql(sql, sqlParamArray).ToList();
            //return null;
    }

    /// <inheritdoc/>
    public async Task<DocumentResponseModel> GetDocumentDetailById(Guid documentId) {
      string sql = " Select d.ID as DocumentId, d.Title , fs.FileName , d.FileSizeinKB, fs.Id as FileStorageId," +
           " d.UpdatedOn as DocModifiedDate,d.OwnerEntityId, th.id AS ThumbnailId," +
           " d.TenantId, th.MediaType, th.FileExtension, th.FileName as ThumbnailFileName, " +
           " fs.Mimetype, null as ThumbnailURL, Null as DocumentURL " +
           " FROM core.DMDocument d " +
           " INNER JOIN core.DMFileStorage fs ON d.FileStorageId = fs.ID " +
           " INNER JOIN core.DMThumbnail th on th.DocumentId = d.ID " +
           " where d.Id = @documentId ";

      SqlParameter ticketParam = new SqlParameter("documentId", documentId);
      object[] param = new object[] { ticketParam };

      // Get the SQL query result in form of Document Response model.
      return await GetQueryEntityAsync<DocumentResponseModel>(sql, param);
    }

    #endregion Public Methods

  }
}
