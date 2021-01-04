/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class implements standard business logic and operations for BACustomerPaymentDetailDS entity.
    /// </summary>
    public class BACustomerPaymentDetailDS:BaseDS<BACustomerPaymentDetail>, IBACustomerPaymentDetailDS {
        #region Local Member

        IBACustomerPaymentDetailRepository _customerPaymentDetailRepo;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="customerPaymentDetailRepo"></param>
        public BACustomerPaymentDetailDS(IBACustomerPaymentDetailRepository customerPaymentDetailRepo) : base(customerPaymentDetailRepo) {
            _customerPaymentDetailRepo = customerPaymentDetailRepo;
        }

        #endregion Constructor

        #region Add
        ///<inheritdoc/>
        public async Task AddCustomerPaymentDetailListAsync(List<BACustomerPaymentDetailSyncDTO> customerPaymentDetailList, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < customerPaymentDetailList.Count; i++) {
                await AddCustomerPaymentDetailAsync(customerPaymentDetailList[i], tenantId);
            }
        }

        private async Task AddCustomerPaymentDetailAsync(BACustomerPaymentDetailSyncDTO customerPaymentDetailDTO, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            BACustomerPaymentDetail customerPayment = BACustomerPaymentDetailSyncDTO.MapToEntity(customerPaymentDetailDTO);
            //UpdateSystemFieldsByOpType(customerContact, OperationType.Add);
            Guid id = Guid.NewGuid();
            customerPayment.ID = id;
            customerPayment.CreatedBy = id;
            customerPayment.UpdatedBy = id;
            customerPayment.CreatedOn = DateTime.UtcNow;
            customerPayment.UpdatedOn = DateTime.UtcNow;
            customerPayment.Deleted = false;
            customerPayment.TenantId = tenantId;
            await AddAsync(customerPayment, token);
        }

        #endregion Add
    }
}