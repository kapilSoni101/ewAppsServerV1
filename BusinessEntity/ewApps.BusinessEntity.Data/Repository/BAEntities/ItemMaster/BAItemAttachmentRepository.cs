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
    public class BAItemAttachmentRepository:BaseRepository<BAItemMasterAttachment, BusinessEntityDbContext>, IBAItemAttachmentRepository {

        #region Constructor

        /// <summary>
        /// Constructor initializing the base variables
        /// </summary>
        /// <param name="context">ship Db context reference</param>
        /// <param name="sessionManager">Session manager reference</param>
        public BAItemAttachmentRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAItemAttachmentDTO>> GetItemAttachmentListByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken)) {
            //string sql = @"SELECT ID, ERPConnectorKey, ERPItemMasterAttachmentKey, ItemMasterId, ERPItemMasterKey, Name, [FreeText],
            //                AttachmentDate FROM be.BADeliveryAttachment as item
            //                WHERE item.ItemMasterId=@ItemId";

            string sql = @"SELECT ID, ERPConnectorKey, ERPItemAttachmentKey AS ERPItemMasterAttachmentKey,ItemId AS ItemMasterId, ERPItemKey AS ERPItemMasterKey, Name, [FreeText],
                            AttachmentDate FROM be.BAItemMasterAttachment as item
                            WHERE item.ItemId=@ItemId";

            SqlParameter itemIdParam = new SqlParameter("ItemId", itemId);
            return await _context.BusBAItemAttachmentDTOQuery.FromSql(sql, new object[] { itemIdParam }).ToListAsync(cancellationToken);
        } /// <inheritdoc/>
        public async Task<IEnumerable<CustBAItemAttachmentDTO>> GetItemAttachmentListByItemIdForCustAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPItemAttachmentKey AS ERPItemMasterAttachmentKey,ItemId AS ItemMasterId, ERPItemKey AS ERPItemMasterKey, Name, [FreeText],
                            AttachmentDate FROM be.BAItemMasterAttachment as item
                            WHERE item.ItemId=@ItemId";

            SqlParameter itemIdParam = new SqlParameter("ItemId", itemId);
            return await _context.CustBAItemAttachmentDTOQuery.FromSql(sql, new object[] { itemIdParam }).ToListAsync(cancellationToken);
        }

        #endregion
    }
}