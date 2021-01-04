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
    /// with the DB for retriving the data related to BAContractAttachment entity.
    /// </summary>
    public interface IBAContractAttachmentDS:IBaseDS<BAContractAttachment> {


        #region Add

        /// <summary>
        /// add contract item list.
        /// </summary>
        /// <param name="contractAttachmentList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddContractAttachmentListAsync(List<BAContractAttachmentSyncDTO> contractAttachmentList, Guid tenantId, Guid tenantUserId, Guid contractId, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Get

        /// <summary>
        ///  get contract item list.
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBAContractAttachmentDTO>> GetContractAttachmentListByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CustBAContractAttachmentDTO>> GetContractAttachmentListByContractIdForCustAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken));

       #endregion Get
    }
}
