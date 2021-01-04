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
    /// Provide supportive method for address entity.
    /// </summary>
    public interface IPublisherAddressDS:IBaseDS<PublisherAddress> {

        #region Get
        /// <summary>
        /// Get address detail list by parentEntityid and addressType.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PublisherAddressDTO>> GetAddressListByPublisherIdAndAddressTypeAsync(Guid publisherId, int addressType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get address detail list by parent and entityid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="addressType"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PublisherAddress>> GetAddressEntityListByParentEntityIdAndAddressTypeAsync(Guid tenantId, Guid publisherId, int addressType, CancellationToken token = default(CancellationToken));

        #endregion Get

        /// <summary>
        /// Add/Update business address.
        /// </summary>
        /// <param name="addressDTOList"></param>
        /// <param name="tenantId"></param>
        /// <param name="publisherId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddUpdatePublisherAddressListAsync(List<PublisherAddressDTO> addressDTOList, Guid tenantId, Guid publisherId, CancellationToken token = default(CancellationToken));

        Task AddPublisherAddressAsync(PublisherAddressDTO addressDTO, Guid tenantId, Guid publisherId, CancellationToken token = default(CancellationToken));

    }
}
