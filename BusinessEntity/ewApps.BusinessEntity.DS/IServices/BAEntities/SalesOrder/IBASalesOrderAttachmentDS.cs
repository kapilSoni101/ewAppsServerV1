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
    /// with the DB for retriving the data related to BASalesOrderAttachment entity.
    /// </summary>
    public interface IBASalesOrderAttachmentDS:IBaseDS<BASalesOrderAttachment> {


        #region Add

        /// <summary>
        /// add salesOrder Attachment list.
        /// </summary>
        /// <param name="salesOrderAttachmentList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddSalesOrderAttachmentListAsync(List<BASalesOrderAttachmentSyncDTO> salesOrderAttachmentList, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Get
        /// <summary>
        /// get salesOrder Attachment list.
        /// </summary>
        /// <param name="salesOrdertId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdAsync(Guid salesOrdertId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// get salesOrder Attachment list for cust.
        /// </summary>
        /// <param name="salesOrderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CustBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdForCustAsync(Guid salesOrderId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get
    }
}
