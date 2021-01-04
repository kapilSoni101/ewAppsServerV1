using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {

    public class BAPurchaseOrderAttachmentRepository:BaseRepository<BAPurchaseOrderAttachment, BusinessEntityDbContext>, IBAPurchaseOrderAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        public BAPurchaseOrderAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <summary>
        /// Get purchase order attachment list.
        /// </summary>
        /// <param name="pOrderId">Purchase orderid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BAARPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByOrderIdAsync(Guid pOrderId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPPurchaseOrderAttachmentKey, PurchaseOrderId, ERPPurchaseOrderKey, Name, [FreeText],
                            AttachmentDate FROM be.BAARPurchaseOrderAttachment as da
                            WHERE da.PurchaseOrderID = @PurchaseOrderId";

            SqlParameter invoiceIdParam = new SqlParameter("PurchaseOrderId", pOrderId);
            return await _context.BAARPurchaseOrderAttachmentDTOQuery.FromSql(sql, new object[] { invoiceIdParam }).ToListAsync(cancellationToken);
        }

        #endregion Get

    }

}