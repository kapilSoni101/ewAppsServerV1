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
using System.Data.SqlClient;
using System.Linq;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppPortal.Data {
    /// <summary>
    /// This repository class implements <seealso cref="ISupportCommentRepository"/> interface to get <see cref="SupportComment"/> entity related data.
    /// </summary>
    /// <seealso cref="SupportComment"/>
    /// <seealso cref="CoreDbContext"/>
    /// <seealso cref="ISupportTicketRepository"/>
    public class SupportCommentRepository:BaseRepository<SupportComment, AppPortalDbContext>, ISupportCommentRepository {

    /// <summary>
    /// Initializes the member variables and required dependencies.
    /// </summary>
    /// <param name="context">An instance of <see cref="CoreDbContext"/> to communicate with data storage.</param>
    /// <param name="sessionManager">An instance of <see cref="IUserSessionManager"/> to get requester user session information.</param>
    public SupportCommentRepository(AppPortalDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    /// <inheritdoc/>
    public IEnumerable<SupportCommentDTO> GetCommentListBySupportId(Guid supportId) {
      string sql = @"SELECT ID AS 'CommentId', CommentText, CreatorLevel, CreatedOn FROM ap.SupportComment WHERE SupportId=@SupportId ORDER BY CreatedOn DESC ";

      SqlParameter supportIdParam = new SqlParameter("SupportId", supportId);

      return _context.SupportCommentDTOQuery.FromSql(sql, new object[] { supportIdParam }).ToArray();
    }
  }
}
