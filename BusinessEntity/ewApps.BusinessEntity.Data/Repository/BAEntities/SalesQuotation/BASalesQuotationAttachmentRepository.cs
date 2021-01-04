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
    /// This class implements standard database logic and operations for BASalesQuotationAttachment entity.
    /// </summary>
    public class BASalesQuotationAttachmentRepository:BaseRepository<BASalesQuotationAttachment, BusinessEntityDbContext>, IBASalesQuotationAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BASalesQuotationAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPSalesQuotationAttachmentKey, SalesQuotationId, ERPSalesQuotationKey, Name, [FreeText],
                            AttachmentDate FROM be.BASalesQuotationAttachment as sq
                            WHERE sq.SalesQuotationId=@SalesQuotationId";

            SqlParameter salesQuotationIdParam = new SqlParameter("SalesQuotationId", salesQuotationId);
            return await _context.BusBASalesQuotationAttachmentDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam }).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdForCustAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPSalesQuotationAttachmentKey, SalesQuotationId, ERPSalesQuotationKey, Name, [FreeText],
                            AttachmentDate FROM be.BASalesQuotationAttachment as sq
                            WHERE sq.SalesQuotationId=@SalesQuotationId";

            SqlParameter salesQuotationIdParam = new SqlParameter("SalesQuotationId", salesQuotationId);
            return await _context.CustBASalesQuotationAttachmentDTOQuery.FromSql(sql, new object[] { salesQuotationIdParam }).ToListAsync(cancellationToken);
        }
        #endregion
    }
}
