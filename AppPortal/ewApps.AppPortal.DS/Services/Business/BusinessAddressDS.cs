/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@ewoorkplaceapps.com>
 * Date: 13 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 13 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Contains supportive method for Address entity.
    /// </summary>
    public class BusinessAddressDS:BaseDS<BusinessAddress>, IBusinessAddressDS {

        #region Local Member 

        
        IBusinessAddressRepository _addressRepo;


        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="addressRepo"></param>
        public BusinessAddressDS(IBusinessAddressRepository addressRepo) : base(addressRepo) {
            _addressRepo = addressRepo;
        }

        #endregion

        #region Get
        /// <summary>
        /// Get address detail list by parentEntityid and addressType.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusinessAddressModelDTO>> GetAddressListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid businessId, int addressType, CancellationToken token = default(CancellationToken)) {
            return await _addressRepo.GetAddressListByParentEntityIdAndAddressTypeAsync(tenantId, businessId, addressType, token);
        }

        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="parentEntityId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BusinessAddress>> GetAddressEntityListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid parentEntityId, int addressType, CancellationToken token = default(CancellationToken)) {
            return await _addressRepo.GetAddressEntityListByParentEntityIdAndAddressTypeAsync(tenantId, parentEntityId, addressType, token );
        }

        #endregion Get

        #region Address Add/Update/Delete
        /// <summary>
        /// Update business address.
        /// </summary>
        /// <param name="addressDTOList"></param>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddUpdateBusinessAddressListAsync(List<BusinessAddressModelDTO> addressDTOList, Guid tenantId, Guid businessId, CancellationToken token = default(CancellationToken)) {

            List<BusinessAddress> listAddress = await _addressRepo.GetAddressListEntityByParentEntityIdAsync(businessId, token);
            bool find = false;
            for(int i = 0; i < listAddress.Count; i++) {
                find = false;
                if(addressDTOList != null) {
                    for(int dtoIndex = 0; dtoIndex < addressDTOList.Count; dtoIndex++) {
                        if(listAddress[i].ID == addressDTOList[dtoIndex].ID) {
                            find = true;
                            break;
                        }
                    }
                }
                if(!find) {
                    listAddress[i].Deleted = true;
                    await UpdateAsync(listAddress[i], listAddress[i].ID, token);
                }
            }
            if(addressDTOList != null) {
                List<BusinessAddressModelDTO> updateList = new List<BusinessAddressModelDTO>();
                List<BusinessAddressModelDTO> addList = new List<BusinessAddressModelDTO>();
                for(int i = 0; i < addressDTOList.Count; i++) {
                    BusinessAddress add = listAddress.Find(addrs => addrs.ID == addressDTOList[i].ID);

                    if(add != null) {
                        updateList.Add(addressDTOList[i]);
                    }
                    else {
                        addList.Add(addressDTOList[i]);
                    }
                }
                if(updateList != null && updateList.Count > 0) {
                    for(int i = 0; i < updateList.Count; i++) {
                        await UpdateBusinessAddressAsync(updateList[i], tenantId, businessId, token);
                    }
                }
                if(addList != null && addList.Count > 0) {
                    for(int i = 0; i < addList.Count; i++) {
                        await AddBusinessAddressAsync(addList[i], tenantId, businessId, token);
                    }
                }
            }

        }

        /// <summary>
        /// Add business address.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddBusinessAddressAsync(BusinessAddressModelDTO addressDTO, Guid tenantId, Guid businessId, CancellationToken token = default(CancellationToken)) {
            BusinessAddress address = BusinessAddressModelDTO.MapToEntity(addressDTO);
            address.Label = addressDTO.Label;
            UpdateSystemFieldsByOpType(address, OperationType.Add);
            address.TenantId = tenantId;
            address.BusinessId = businessId;
            await AddAsync(address, token);
        }

        /// <summary>
        /// Update business address.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateBusinessAddressAsync(BusinessAddressModelDTO addressDTO, Guid tenantId, Guid businessId, CancellationToken token = default(CancellationToken)) {
            BusinessAddress address = BusinessAddressModelDTO.MapToEntity(addressDTO);
            UpdateSystemFieldsByOpType(address, OperationType.Update);
            address.Label = addressDTO.Label;
            address.TenantId = tenantId;
            address.BusinessId = businessId;
            await UpdateAsync(address, address.ID, token);
        }

        /// <summary>
        /// Add business address.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="businessId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddBusinessAddressFromSAPAsync(BusinessSyncDTO businessSyncDTO, Guid tenantId, Guid businessId, CancellationToken token = default(CancellationToken)) {
            BusinessAddress address = new BusinessAddress();
            address.Label = "Default Address";
            address.AddressStreet1 = businessSyncDTO.Street;
            address.AddressStreet2 = businessSyncDTO.StreetNo;
            address.AddressStreet3 = businessSyncDTO.Block;
            address.City = businessSyncDTO.City;
            address.Country = businessSyncDTO.Country;
            address.State = businessSyncDTO.State;
            address.Phone = "";
            address.ZipCode = businessSyncDTO.ZipCode;
            address.AddressType = 1;
            address.FaxNumber = "";
            UpdateSystemFieldsByOpType(address, OperationType.Add);
            address.TenantId = tenantId;
            address.BusinessId = businessId;
            await AddAsync(address, token);
        }
        #endregion Address Add/Update/Delete


        #region Validation


        #endregion Validation
    }
}
