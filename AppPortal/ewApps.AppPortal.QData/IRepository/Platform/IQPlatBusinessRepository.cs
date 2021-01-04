/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {

    /// <summary>
    /// Platfrom business respository for exposing the methods related to platform business database intrection.
    /// </summary>
    public interface IQPlatBusinessRepository:IBaseRepository<BaseEntity> {

        Task<List<PlatBusinessDTO>> GetTenantListOnPlatformAsync(ListDateFilterDTO filter, Guid homeAppId, CancellationToken token = default(CancellationToken));

        }
}
