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
    /// This class implements standard business logic and operations for BACustomerContactDS entity.
    /// </summary>
    public class BACustomerContactDS:BaseDS<BACustomerContact>, IBACustomerContactDS {

        #region Local Member

        IBACustomerContactRepository _customerContactRepo;
        IUnitOfWork _unitOfWork;

        #endregion Local Member

        #region Contructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="customerContactRepo"></param>
        /// <param name="unitOfWork"></param>
        public BACustomerContactDS(IBACustomerContactRepository customerContactRepo, IUnitOfWork unitOfWork) : base(customerContactRepo) {
            _customerContactRepo = customerContactRepo;
            _unitOfWork = unitOfWork;
        }

        #endregion Contructor

        #region Add

        ///<inheritdoc/>
        public async Task AddCustomerContactListAsync(List<BACustomerContactSyncDTO> customerContactList, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < customerContactList.Count; i++) {
                await AddCustomerContactAsync(customerContactList[i], tenantId);
            }
        }
       
        private async Task AddCustomerContactAsync(BACustomerContactSyncDTO customerContactSyncDTO, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            BACustomerContact customerContact = BACustomerContactSyncDTO.MapToEntity(customerContactSyncDTO);
            //UpdateSystemFieldsByOpType(customerContact, OperationType.Add);
            Guid id = Guid.NewGuid();
            customerContact.ID = id;
            customerContact.CreatedBy = id;
            customerContact.UpdatedBy = id;
            customerContact.CreatedOn = DateTime.UtcNow;
            customerContact.UpdatedOn = DateTime.UtcNow;
            customerContact.Deleted = false;
            customerContact.TenantId = tenantId;
            await AddAsync(customerContact, token);
        }

        #endregion Add

        #region Get

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="customerId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<CustomerContactDTO>> GetCustomerContactListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _customerContactRepo.GetCustomerContactListByIdAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<BACustomerContact>> GetCustomerContactListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _customerContactRepo.GetCustomerContactListByCustomerIdAsync(customerId, token);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// To delete customer contact associated with Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="commit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteCustomerAsync(Guid customerId, bool commit, CancellationToken token = default(CancellationToken)) {
            List<BACustomerContact> customerContactList = await GetCustomerContactListByCustomerIdAsync(customerId, token);
            if(customerContactList != null) {
                foreach(BACustomerContact customer in customerContactList) {
                    customer.Deleted = true;
                    base.UpdateSystemFieldsByOpType(customer, OperationType.Update);
                    await UpdateAsync(customer, customer.ID, token);
                }
                if(commit) {
                    _unitOfWork.SaveAll();
                }
            }
        }

        #endregion Delete
    }
}
