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
    /// This class implements standard database logic and operations for BAARinvoiceAttachment entity.
    /// </summary>
    public class BAARInvoiceAttachmentRepository:BaseRepository<BAARInvoiceAttachment, BusinessEntityDbContext>, IBAARInvoiceAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAARInvoiceAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPARInvoiceAttachmentKey, ARInvoiceId, ERPARInvoiceKey, Name, [FreeText],
                            AttachmentDate FROM be.BAARInvoiceAttachment as da
                            WHERE da.ARInvoiceId=@InvoiceId";

            SqlParameter invoiceIdParam = new SqlParameter("InvoiceId", invoiceId);
            return await _context.BusBAARInvoiceAttachmentDTOQuery.FromSql(sql, new object[] { invoiceIdParam }).ToListAsync(cancellationToken);
        }
    }
}
