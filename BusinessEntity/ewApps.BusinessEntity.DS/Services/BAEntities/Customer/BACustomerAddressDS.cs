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
    /// This class implements standard business logic and operations for BACustomerAddress entity.
    /// </summary>
    public class BACustomerAddressDS:BaseDS<BACustomerAddress>, IBACustomerAddressDS {

        #region Local Member

        IBACustomerAddressRepository _customerAddressRepo;
        IUnitOfWork _unitOfWork;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="customerAddressRepo"></param>
        /// <param name="unitOfWork"></param>
        public BACustomerAddressDS(IBACustomerAddressRepository customerAddressRepo, IUnitOfWork unitOfWork) : base(customerAddressRepo) {
            _customerAddressRepo = customerAddressRepo;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Add

        ///<inheritdoc/>
        public async Task AddCustomerAddressListAsync(List<BACustomerAddressSyncDTO> customerAddressList, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            for(int i = 0; i < customerAddressList.Count; i++) {
                await AddCustomerAddressAsync(customerAddressList[i], tenantId);
            }
        }

        private async Task AddCustomerAddressAsync(BACustomerAddressSyncDTO customerAddressSyncDTO, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            BACustomerAddress customerAddress = BACustomerAddressSyncDTO.MapToEntity(customerAddressSyncDTO);
            //UpdateSystemFieldsByOpType(customerAddress, OperationType.Add);
            Guid id = Guid.NewGuid();
            customerAddress.ID = id;
            customerAddress.CreatedBy = id;
            customerAddress.UpdatedBy = id;
            customerAddress.CreatedOn = DateTime.UtcNow;
            customerAddress.UpdatedOn = DateTime.UtcNow;
            customerAddress.Deleted = false;
            customerAddress.TenantId = tenantId;
            await AddAsync(customerAddress, token);
        }

        #endregion Add

        #region Get

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="customerId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<CustomerAddressDTO>> GetCustomerAddressListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _customerAddressRepo.GetCustomerAddressListByIdAsync(customerId, token);
        }

        /// <summary>
        /// Get customer address entity list.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BACustomerAddress>> GetCustomerAddressEntityListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _customerAddressRepo.GetCustomerAddressEntityListByIdAsync(customerId, token);
        }

        #endregion Get

        #region Delete

        /// <summary>
        /// To delete customer address associated with Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="commit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteCustomerAddressAsync(Guid customerId, bool commit, CancellationToken token = default(CancellationToken)) {
            List<BACustomerAddress> customerAddressList = await GetCustomerAddressEntityListByIdAsync(customerId, token);
            if(customerAddressList != null) {
                foreach(BACustomerAddress address in customerAddressList) {
                    address.Deleted = true;
                    base.UpdateSystemFieldsByOpType(address, OperationType.Update);
                    await UpdateAsync(address, customerId, token);
                }
                if(commit) {
                    _unitOfWork.Save();
                }
            }
        }

        #endregion Delete
    }
}
