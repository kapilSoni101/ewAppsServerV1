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
    /// with the DB for retriving the data related to BAASNAttachment entity.
    /// </summary>
    public interface IBAASNAttachmentDS:IBaseDS<BAASNAttachment> {


        #region Add

        /// <summary>
        /// add ASN Attachment list.
        /// </summary>
        /// <param name="asnAttachmentList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddASNAttachmentListAsync(List<BAASNAttachmentSyncDTO> asnAttachmentList, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Get

        /// <summary>
        /// get ASN Attachment List
        /// </summary>
        /// <param name="asnId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asnId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<CustBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken));

        #endregion Get
    }
}
