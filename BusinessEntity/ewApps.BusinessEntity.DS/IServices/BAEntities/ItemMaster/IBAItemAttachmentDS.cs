 /* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Sourabh Agrawal<sagrawal @eworkplaceapps.com>
 * Date:05 september 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// Responsible for exposing all the methods that are intrecting 
    /// with the DB for retriving the data related to BAItemMasterAttachment entity.
    /// </summary>
    public interface IBAItemAttachmentDS:IBaseDS<BAItemMasterAttachment> {


        #region Add

        /// <summary>
        /// add  item attachmnetlist.
        /// </summary>
        /// <param name="itemAttachmentList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddItemAttachmentListAsync(List<BAItemMasterAttachmentSyncDTO> itemAttachmentList, Guid tenantId, Guid tenantUserId, Guid itemId, CancellationToken token = default(CancellationToken));

        #endregion Add

        Task<IEnumerable<BusBAItemAttachmentDTO>> GetItemAttachmentListByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<CustBAItemAttachmentDTO>> GetItemAttachmentListByItemIdForCustAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
