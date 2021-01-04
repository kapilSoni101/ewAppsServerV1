/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:21 October 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This class implements standard database logic and operations for BADeliveryAttachment entity.
    /// </summary>
    public class BAASNAttachmentRepository:BaseRepository<BAASNAttachment, BusinessEntityDbContext>, IBAASNAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAASNAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPASNAttachmentKey, ASNId, ERPASNKey, Name, [FreeText],
                            AttachmentDate FROM be.BAASNAttachment as asn
                            WHERE asn.ASNId=@ASNId";

            SqlParameter asnIdParam = new SqlParameter("ASNId", asnId);
            return await _context.BusBAASNAttachmentDTOQuery.FromSql(sql, new object[] { asnIdParam }).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPASNAttachmentKey, ASNId, ERPASNKey, Name, [FreeText],
                            AttachmentDate FROM be.BAASNAttachment as asn
                            WHERE asn.ASNId=@ASNId";

            SqlParameter asnIdParam = new SqlParameter("ASNId", asnId);
            return await _context.CustBAASNAttachmentDTOQuery.FromSql(sql, new object[] { asnIdParam }).ToListAsync(cancellationToken);
        }

        #endregion
    }
}