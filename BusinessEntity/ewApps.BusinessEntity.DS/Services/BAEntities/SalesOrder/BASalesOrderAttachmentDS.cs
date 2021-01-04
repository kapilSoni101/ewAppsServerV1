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
    ///  This class implements standard business logic and operations for BASalesOrderAttachment entity.
    /// </summary>
    public class BASalesOrderAttachmentDS:BaseDS<BASalesOrderAttachment>, IBASalesOrderAttachmentDS {

        #region Local variables

        IBASalesOrderAttachmentRepository _salesOrderAttachmentRepo;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="salesOrderAttachmentRepo"></param>
        public BASalesOrderAttachmentDS(IBASalesOrderAttachmentRepository salesOrderAttachmentRepo) : base(salesOrderAttachmentRepo) {
            _salesOrderAttachmentRepo = salesOrderAttachmentRepo;
        }
        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddSalesOrderAttachmentListAsync(List<BASalesOrderAttachmentSyncDTO> salesOrderAttachmentList, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < salesOrderAttachmentList.Count; i++) {
                await AddSalesOrderAttachmentAsync(salesOrderAttachmentList[i], tenantId, tenantUserId, salesOrderId);
            }
        }

        private async Task AddSalesOrderAttachmentAsync(BASalesOrderAttachmentSyncDTO salesOrderAttachmentSyncDTO, Guid tenantId, Guid tenantUserId, Guid salesOrderId, CancellationToken token = default(CancellationToken)) {


            salesOrderAttachmentSyncDTO.SalesOrderId = salesOrderId;
            BASalesOrderAttachment salesOrderAttachment = BASalesOrderAttachmentSyncDTO.MapToEntity(salesOrderAttachmentSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            salesOrderAttachment.ID = id;
            salesOrderAttachment.CreatedBy = tenantUserId;
            salesOrderAttachment.UpdatedBy = tenantUserId;
            salesOrderAttachment.CreatedOn = DateTime.UtcNow;
            salesOrderAttachment.UpdatedOn = DateTime.UtcNow;
            salesOrderAttachment.Deleted = false;
            salesOrderAttachment.TenantId = tenantId;
            await AddAsync(salesOrderAttachment, token);
        }

        #endregion Add

        #region Get

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdAsync(Guid salesOrdertId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _salesOrderAttachmentRepo.GetSalesOrderAttachmentListByIdAsync(salesOrdertId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesOrderAttachmentDTO>> GetSalesOrderAttachmentListByIdForCustAsync(Guid salesOrdertId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _salesOrderAttachmentRepo.GetSalesOrderAttachmentListByIdForCustAsync(salesOrdertId, cancellationToken);
        }

        #endregion

    }
}
