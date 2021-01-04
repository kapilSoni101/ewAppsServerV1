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
    /// with the DB for retriving the data related to BASalesQuotationAttachment entity.
    /// </summary>
    public interface IBASalesQuotationAttachmentDS:IBaseDS<BASalesQuotationAttachment> {


        #region Add

        /// <summary>
        /// add salesQuotation attachment list.
        /// </summary>
        /// <param name="#endregion Add"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddSalesQuotationAttachmentListAsync(List<BASalesQuotationAttachmentSyncDTO> salesQuotationAttachmentList, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Get

        /// <summary>
        ///  get salesQuotation attachment list.
        /// </summary>
        /// <param name="salesQuotationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// get salesQuotation attachment list For Cust.
        /// </summary>
        /// <param name="salesQuotationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CustBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdForCustAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken));
        #endregion Get
    }
}

