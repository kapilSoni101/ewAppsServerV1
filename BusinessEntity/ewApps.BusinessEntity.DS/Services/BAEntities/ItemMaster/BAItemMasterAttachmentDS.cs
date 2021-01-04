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
    public class BAItemMasterAttachmentDS:BaseDS<BAItemMasterAttachment>, IBAItemAttachmentDS {

        #region Local variables

        IBAItemAttachmentRepository _itemAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemAttachmentRepo"></param>
        public BAItemMasterAttachmentDS(IBAItemAttachmentRepository itemAttachmentRepo) : base(itemAttachmentRepo) {
            _itemAttachmentRepo = itemAttachmentRepo;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddItemAttachmentListAsync(List<BAItemMasterAttachmentSyncDTO> itemAttachmentList, Guid tenantId, Guid tenantUserId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < itemAttachmentList.Count; i++) {
                await AddItemAttachmentAsync(itemAttachmentList[i], tenantId, tenantUserId, itemId);
            }
        }

        private async Task AddItemAttachmentAsync(BAItemMasterAttachmentSyncDTO itemAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid itemId, CancellationToken token = default(CancellationToken)) {


            itemAttachmentSyncDTO.ItemMasterId = itemId;
            BAItemMasterAttachment itemAttachment = BAItemMasterAttachmentSyncDTO.MapToEntity(itemAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            itemAttachment.ID = id;
            itemAttachment.CreatedBy = tenantUserId;
            itemAttachment.UpdatedBy = tenantUserId;
            itemAttachment.CreatedOn = DateTime.UtcNow;
            itemAttachment.UpdatedOn = DateTime.UtcNow;
            itemAttachment.Deleted = false;
            itemAttachment.TenantId = tenantId;
            await AddAsync(itemAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAItemAttachmentDTO>> GetItemAttachmentListByItemIdAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _itemAttachmentRepo.GetItemAttachmentListByItemIdAsync(itemId, cancellationToken);
        }

        // <inheritdoc/>
        public async Task<IEnumerable<CustBAItemAttachmentDTO>> GetItemAttachmentListByItemIdForCustAsync(Guid itemId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _itemAttachmentRepo.GetItemAttachmentListByItemIdForCustAsync(itemId, cancellationToken);
        }

        #endregion

        }
}