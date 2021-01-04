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
    /// This class implements standard database logic and operations for BASalesOrderAttachment entity.
    /// </summary>
    public class BASalesOrderAttachmentRepository:BaseRepository<BASalesOrderAttachment, BusinessEntityDbContext>, IBASalesOrderAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BASalesOrderAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdAsync(Guid salesOrderId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPSalesOrderAttachmentKey, SalesOrderId, ERPSalesOrderKey, Name, [FreeText],
                            AttachmentDate FROM be.BASalesOrderAttachment as da
                            WHERE da.SalesOrderId=@SalesOrderId";

            SqlParameter salesOrderIdIdParam = new SqlParameter("SalesOrderId", salesOrderId);
            return await _context.BusBASalesOrderAttachmentDTOQuery.FromSql(sql, new object[] { salesOrderIdIdParam }).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdForCustAsync(Guid salesOrderId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPSalesOrderAttachmentKey, SalesOrderId, ERPSalesOrderKey, Name, [FreeText],
                            AttachmentDate FROM be.BASalesOrderAttachment as da
                            WHERE da.SalesOrderId=@SalesOrderId";

            SqlParameter salesOrderIdIdParam = new SqlParameter("SalesOrderId", salesOrderId);
            return await _context.CustBASalesOrderAttachmentDTOQuery.FromSql(sql, new object[] { salesOrderIdIdParam }).ToListAsync(cancellationToken);
        }

        #endregion
    }
}

