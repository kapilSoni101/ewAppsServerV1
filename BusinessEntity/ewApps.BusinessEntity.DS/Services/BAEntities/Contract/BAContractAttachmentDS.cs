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
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for BAContractAttachment entity.
    /// </summary>
    public class BAContractAttachmentDS:BaseDS<BAContractAttachment>, IBAContractAttachmentDS {

        #region Local variables

        IBAContractAttachmentRepository _contractAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractAttachmentRepo"></param>
        public BAContractAttachmentDS(IBAContractAttachmentRepository contractAttachmentRepo) : base(contractAttachmentRepo) {
            _contractAttachmentRepo = contractAttachmentRepo;
        }

        #endregion Constructor

        #region Add
        ///<inheritdoc/>
        public async Task AddContractAttachmentListAsync(List<BAContractAttachmentSyncDTO> contractAttachmentList, Guid tenantId, Guid tenantUserId, Guid contractId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < contractAttachmentList.Count; i++) {
                await AddContractAttachmentAsync(contractAttachmentList[i], tenantId, tenantUserId, contractId);
            }
        }

        private async Task AddContractAttachmentAsync(BAContractAttachmentSyncDTO contractAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid contractId, CancellationToken token = default(CancellationToken)) {


            contractAttachmentSyncDTO.ContractId = contractId;
            BAContractAttachment contractAttachment = BAContractAttachmentSyncDTO.MapToEntity(contractAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            contractAttachment.ID = id;
            contractAttachment.CreatedBy = tenantUserId;
            contractAttachment.UpdatedBy = tenantUserId;
            contractAttachment.CreatedOn = DateTime.UtcNow;
            contractAttachment.UpdatedOn = DateTime.UtcNow;
            contractAttachment.Deleted = false;
            contractAttachment.TenantId = tenantId;
            await AddAsync(contractAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAContractAttachmentDTO>> GetContractAttachmentListByContractIdAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _contractAttachmentRepo.GetContractAttachmentListByContractIdAsync(contractId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAContractAttachmentDTO>> GetContractAttachmentListByContractIdForCustAsync(Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _contractAttachmentRepo.GetContractAttachmentListByContractIdForCustAsync(contractId, cancellationToken);
        }

        #endregion
    }
}
