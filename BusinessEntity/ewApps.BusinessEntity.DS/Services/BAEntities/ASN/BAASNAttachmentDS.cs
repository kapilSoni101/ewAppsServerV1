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

    public class BAASNAttachmentDS:BaseDS<BAASNAttachment>, IBAASNAttachmentDS {

        #region Local variables

        IBAASNAttachmentRepository _asnAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asnAttachmentRepo"></param>
        public BAASNAttachmentDS(IBAASNAttachmentRepository asnAttachmentRepo) : base(asnAttachmentRepo) {
            _asnAttachmentRepo = asnAttachmentRepo;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddASNAttachmentListAsync(List<BAASNAttachmentSyncDTO> asneAttachmentList, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < asneAttachmentList.Count; i++) {
                await AddASNAttachmentAsync(asneAttachmentList[i], tenantId, tenantUserId, asnId);
            }
        }

        private async Task AddASNAttachmentAsync(BAASNAttachmentSyncDTO asnAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid asnId, CancellationToken token = default(CancellationToken)) {


            asnAttachmentSyncDTO.ASNId = asnId;
            BAASNAttachment asnAttachment = BAASNAttachmentSyncDTO.MapToEntity(asnAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            asnAttachment.ID = id;
            asnAttachment.CreatedBy = tenantUserId;
            asnAttachment.UpdatedBy = tenantUserId;
            asnAttachment.CreatedOn = DateTime.UtcNow;
            asnAttachment.UpdatedOn = DateTime.UtcNow;
            asnAttachment.Deleted = false;
            asnAttachment.TenantId = tenantId;
            await AddAsync(asnAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _asnAttachmentRepo.GetASNAttachmentListByASNIdAsync(asnId, cancellationToken);
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAASNAttachmentDTO>> GetASNAttachmentListByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _asnAttachmentRepo.GetASNAttachmentListByASNIdAsyncForCust(asnId, cancellationToken);
        }
        #endregion

    }
}
