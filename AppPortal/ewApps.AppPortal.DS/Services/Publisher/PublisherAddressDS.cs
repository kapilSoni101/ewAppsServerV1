/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@ewoorkplaceapps.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 14 August 2019
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
    public class PublisherAddressDS:BaseDS<PublisherAddress>, IPublisherAddressDS {

        #region Local Member 

        
        IPublisherAddressRepository _addressRepo;


        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="addressRepo"></param>
        public PublisherAddressDS(IPublisherAddressRepository addressRepo) : base(addressRepo) {
            _addressRepo = addressRepo;
        }

        #endregion

        #region Get
        /// <summary>
        /// Get address detail list by parentEntityid and addressType.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<PublisherAddressDTO>> GetAddressListByPublisherIdAndAddressTypeAsync(Guid publisherId, int addressType, CancellationToken token = default(CancellationToken)) {
            return await _addressRepo.GetAddressListByPublisherIdAndAddressTypeAsync(publisherId, addressType, token);
        }

        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<PublisherAddress>> GetAddressEntityListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid publisherId, int addressType, CancellationToken token = default(CancellationToken)) {
            return await _addressRepo.GetAddressEntityListByParentEntityIdAndAddressTypeAsync(tenantId, publisherId, addressType, token );
        }

        #endregion Get

        #region Address Add/Update/Delete
        /// <summary>
        /// Update business address.
        /// </summary>
        /// <param name="addressDTOList"></param>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddUpdatePublisherAddressListAsync(List<PublisherAddressDTO> addressDTOList, Guid tenantId, Guid publisherId, CancellationToken token = default(CancellationToken)) {

            List<PublisherAddress> listAddress = await _addressRepo.GetAddressListEntityByParentEntityIdAsync(publisherId, token);
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
                List<PublisherAddressDTO> updateList = new List<PublisherAddressDTO>();
                List<PublisherAddressDTO> addList = new List<PublisherAddressDTO>();
                for(int i = 0; i < addressDTOList.Count; i++) {
                    PublisherAddress add = listAddress.Find(addrs => addrs.ID == addressDTOList[i].ID);

                    if(add != null) {
                        updateList.Add(addressDTOList[i]);
                    }
                    else {
                        addList.Add(addressDTOList[i]);
                    }
                }
                if(updateList != null && updateList.Count > 0) {
                    for(int i = 0; i < updateList.Count; i++) {
                        await UpdatePublisherAddressAsync(updateList[i], tenantId, publisherId, token);
                    }
                }
                if(addList != null && addList.Count > 0) {
                    for(int i = 0; i < addList.Count; i++) {
                        await AddPublisherAddressAsync(addList[i], tenantId, publisherId, token);
                    }
                }
            }

        }

        /// <summary>
        /// Add business address.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task AddPublisherAddressAsync(PublisherAddressDTO addressDTO, Guid tenantId, Guid publisherId, CancellationToken token = default(CancellationToken)) {
            PublisherAddress address = PublisherAddressDTO.MapToEntity(addressDTO);
            address.Label = addressDTO.Label;
            UpdateSystemFieldsByOpType(address, OperationType.Add);
            address.TenantId = tenantId;
            address.PublisherId = publisherId;
            await AddAsync(address, token);
        }

        /// <summary>
        /// Update business address.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatePublisherAddressAsync(PublisherAddressDTO addressDTO, Guid tenantId, Guid publisherId, CancellationToken token = default(CancellationToken)) {
            PublisherAddress address = PublisherAddressDTO.MapToEntity(addressDTO);
            UpdateSystemFieldsByOpType(address, OperationType.Update);
            address.Label = addressDTO.Label;
            address.TenantId = tenantId;
            address.PublisherId = publisherId;
            await UpdateAsync(address, address.ID, token);
        }
        #endregion Address Add/Update/Delete


        #region Validation


        #endregion Validation
    }
}
