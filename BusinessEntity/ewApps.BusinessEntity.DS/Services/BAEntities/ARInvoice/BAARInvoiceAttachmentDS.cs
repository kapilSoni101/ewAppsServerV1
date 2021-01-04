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
    ///  This class implements standard business logic and operations for BAARinvoiceAttachment entity.
    /// </summary>
    public class BAARInvoiceAttachmentDS:BaseDS<BAARInvoiceAttachment>, IBAARInvoiceAttachmentDS {

        #region Local variables

        IBAARInvoiceAttachmentRepository _invoiceAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceAttachmentRepo"></param>
        public BAARInvoiceAttachmentDS(IBAARInvoiceAttachmentRepository invoiceAttachmentRepo) : base(invoiceAttachmentRepo) {
            _invoiceAttachmentRepo = invoiceAttachmentRepo;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddARInvoiceAttachmentListAsync(List<BAARInvoiceAttachmentSyncDTO> aRInvoiceAttachmentList, Guid tenantId, Guid tenantUserId, Guid aRInvoiceId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < aRInvoiceAttachmentList.Count; i++) {
                await AddARInvoiceAttachmentAsync(aRInvoiceAttachmentList[i], tenantId, tenantUserId, aRInvoiceId);
            }
        }

        private async Task AddARInvoiceAttachmentAsync(BAARInvoiceAttachmentSyncDTO aRInvoiceAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid aRInvoiceId, CancellationToken token = default(CancellationToken)) {

            BAARInvoiceAttachment aRInvoiceAttachment = await FindAsync(invattach => invattach.ERPARInvoiceAttachmentKey == aRInvoiceAttachmentSyncDTO.ERPARInvoiceAttachmentKey
            && invattach.ERPARInvoiceKey == aRInvoiceAttachmentSyncDTO.ERPARInvoiceKey && invattach.TenantId == tenantId);
            if(aRInvoiceAttachment != null) {
                aRInvoiceAttachment = BAARInvoiceAttachmentSyncDTO.MapToEntity(aRInvoiceAttachmentSyncDTO);
                UpdateSystemFieldsByOpType(aRInvoiceAttachment, OperationType.Update);
                await UpdateAsync(aRInvoiceAttachment, aRInvoiceAttachment.ID, token);
            }
            else {
                aRInvoiceAttachmentSyncDTO.ARInvoiceId = aRInvoiceId;
                aRInvoiceAttachment = BAARInvoiceAttachmentSyncDTO.MapToEntity(aRInvoiceAttachmentSyncDTO);
                //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
                Guid id = Guid.NewGuid();
                aRInvoiceAttachment.ID = id;
                aRInvoiceAttachment.CreatedBy = tenantUserId;
                aRInvoiceAttachment.UpdatedBy = tenantUserId;
                aRInvoiceAttachment.CreatedOn = DateTime.UtcNow;
                aRInvoiceAttachment.UpdatedOn = DateTime.UtcNow;
                aRInvoiceAttachment.Deleted = false;
                aRInvoiceAttachment.TenantId = tenantId;
                await AddAsync(aRInvoiceAttachment, token);
            }
        }

        #endregion Add
    }
}
