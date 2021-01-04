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
    public class BADeliveryAttachmentRepository:BaseRepository<BADeliveryAttachment, BusinessEntityDbContext>, IBADeliveryAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BADeliveryAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPDeliveryAttachmentKey, DeliveryId, ERPDeliveryKey, Name, [FreeText],
                            AttachmentDate FROM be.BADeliveryAttachment as da
                            WHERE da.DeliveryId=@DeliveryId";

            SqlParameter deliveryIdParam = new SqlParameter("DeliveryId",deliveryId);
            return await _context.BusBADeliveryAttachmentDTOQuery.FromSql(sql, new object[] { deliveryIdParam }).ToListAsync(cancellationToken);
        } /// <inheritdoc/>
        public async Task<IEnumerable<CustBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdForCustAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPDeliveryAttachmentKey, DeliveryId, ERPDeliveryKey, Name, [FreeText],
                            AttachmentDate FROM be.BADeliveryAttachment as da
                            WHERE da.DeliveryId=@DeliveryId";

            SqlParameter deliveryIdParam = new SqlParameter("DeliveryId", deliveryId);
            return await _context.CustBADeliveryAttachmentDTOQuery.FromSql(sql, new object[] { deliveryIdParam }).ToListAsync(cancellationToken);
        }

        #endregion
    }
}

