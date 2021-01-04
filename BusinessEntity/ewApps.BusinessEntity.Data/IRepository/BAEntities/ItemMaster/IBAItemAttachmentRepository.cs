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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This interface responsible for exposing all the methods that are 
    /// intrecting with the DB for retriving the data related to BAItemMasterAttachment entity.
    /// </summary>
    public interface IBAItemAttachmentRepository:IBaseRepository<BAItemMasterAttachment> {
        Task<IEnumerable<BusBAItemAttachmentDTO>> GetItemAttachmentListByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<CustBAItemAttachmentDTO>> GetItemAttachmentListByItemIdForCustAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken));
    }
}