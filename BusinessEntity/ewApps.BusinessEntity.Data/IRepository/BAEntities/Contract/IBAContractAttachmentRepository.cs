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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This interface responsible for exposing all the methods that are 
    /// intrecting with the DB for retriving the data related to BAContractAttachment entity.
    /// </summary>
    public interface IBAContractAttachmentRepository:IBaseRepository<BAContractAttachment> {
        Task<IEnumerable<BusBAContractAttachmentDTO>> GetContractAttachmentListByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<CustBAContractAttachmentDTO>> GetContractAttachmentListByContractIdForCustAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
