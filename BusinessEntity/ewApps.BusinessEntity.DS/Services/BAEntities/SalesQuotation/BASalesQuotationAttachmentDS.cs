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
    ///  This class implements standard business logic and operations for BASalesQuotationAttachment entity.
    /// </summary>
    public class BASalesQuotationAttachmentDS:BaseDS<BASalesQuotationAttachment>, IBASalesQuotationAttachmentDS {

        #region Local variables

        IBASalesQuotationAttachmentRepository _salesQuotationAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesQuotationAttachmentRepo"></param>
        public BASalesQuotationAttachmentDS(IBASalesQuotationAttachmentRepository salesQuotationAttachmentRepo) : base(salesQuotationAttachmentRepo) {
            _salesQuotationAttachmentRepo = salesQuotationAttachmentRepo;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddSalesQuotationAttachmentListAsync(List<BASalesQuotationAttachmentSyncDTO> salesQuotationAttachmentList, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < salesQuotationAttachmentList.Count; i++) {
                await AddSalesQuotationAttachmentAsync(salesQuotationAttachmentList[i], tenantId, tenantUserId, salesQuotationId);
            }
        }

        private async Task AddSalesQuotationAttachmentAsync(BASalesQuotationAttachmentSyncDTO salesQuotationAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid salesQuotationId, CancellationToken token = default(CancellationToken)) {


            salesQuotationAttachmentSyncDTO.SalesQuotationId = salesQuotationId;
            BASalesQuotationAttachment salesQuotationAttachment = BASalesQuotationAttachmentSyncDTO.MapToEntity(salesQuotationAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            salesQuotationAttachment.ID = id;
            salesQuotationAttachment.CreatedBy = tenantUserId;
            salesQuotationAttachment.UpdatedBy = tenantUserId;
            salesQuotationAttachment.CreatedOn = DateTime.UtcNow;
            salesQuotationAttachment.UpdatedOn = DateTime.UtcNow;
            salesQuotationAttachment.Deleted = false;
            salesQuotationAttachment.TenantId = tenantId;
            await AddAsync(salesQuotationAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _salesQuotationAttachmentRepo.GetSalesQuotationAttachmentListBySalesQuotationIdAsync(salesQuotationId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationAttachmentDTO>> GetSalesQuotationAttachmentListBySalesQuotationIdForCustAsync(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _salesQuotationAttachmentRepo.GetSalesQuotationAttachmentListBySalesQuotationIdForCustAsync(salesQuotationId, cancellationToken);
        }

        #endregion
    }
}
