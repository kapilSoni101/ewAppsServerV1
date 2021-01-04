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
    ///  This class implements standard business logic and operations for BADeliveryAttachment entity.
    /// </summary>
    public class BADeliveryAttachmentDS:BaseDS<BADeliveryAttachment>, IBADeliveryAttachmentDS {

        #region Local variables

        IBADeliveryAttachmentRepository _deliveryAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BADeliveryAttachmentDS"/> class.
        /// </summary>
        /// <param name="deliveryAttachmentRepo">The delivery attachment repo.</param>
        public BADeliveryAttachmentDS(IBADeliveryAttachmentRepository deliveryAttachmentRepo) : base(deliveryAttachmentRepo) {
            _deliveryAttachmentRepo = deliveryAttachmentRepo;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddDeliveryAttachmentListAsync(List<BADeliveryAttachmentSyncDTO> deliveryAttachmentList, Guid tenantId, Guid tenantUserId, Guid deliveryId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < deliveryAttachmentList.Count; i++) {
                await AddDeliveryAttachmentAsync(deliveryAttachmentList[i], tenantId, tenantUserId, deliveryId);
            }
        }

        private async Task AddDeliveryAttachmentAsync(BADeliveryAttachmentSyncDTO deliveryAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid deliveryId, CancellationToken token = default(CancellationToken)) {


            deliveryAttachmentSyncDTO.DeliveryId = deliveryId;
            BADeliveryAttachment deliveryAttachment = BADeliveryAttachmentSyncDTO.MapToEntity(deliveryAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            deliveryAttachment.ID = id;
            deliveryAttachment.CreatedBy = tenantUserId;
            deliveryAttachment.UpdatedBy = tenantUserId;
            deliveryAttachment.CreatedOn = DateTime.UtcNow;
            deliveryAttachment.UpdatedOn = DateTime.UtcNow;
            deliveryAttachment.Deleted = false;
            deliveryAttachment.TenantId = tenantId;
            await AddAsync(deliveryAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _deliveryAttachmentRepo.GetDeliveryAttachmentListByDeliveryIdAsync(deliveryId, cancellationToken);
        }

        // <inheritdoc/>
        public async Task<IEnumerable<CustBADeliveryAttachmentDTO>> GetDeliveryAttachmentListByDeliveryIdForCustAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _deliveryAttachmentRepo.GetDeliveryAttachmentListByDeliveryIdForCustAsync(deliveryId, cancellationToken);
        }

        #endregion
    }
}
