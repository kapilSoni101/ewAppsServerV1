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
using ewApps.Core.BaseService;
using ewApps.AppPortal.Entity;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Contains supportive method for Address entity.
    /// </summary>
    public interface IPublisherAddressRepository:IBaseRepository<PublisherAddress> {

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
        /// Get address detail information by parentEntityid.
        /// </summary>
        /// <param name="parentEntityId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PublisherAddress>> GetAddressListEntityByParentEntityIdAsync(Guid parentEntityId, CancellationToken token = default(CancellationToken));

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

    }
}
