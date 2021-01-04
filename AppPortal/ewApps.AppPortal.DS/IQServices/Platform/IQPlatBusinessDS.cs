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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic related to business entity by creating an object to this class.
    /// </summary>
    public interface IQPlatBusinessDS {

        Task<List<PlatBusinessDTO>> GetTenantListOnPlatformAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));
    }
}
