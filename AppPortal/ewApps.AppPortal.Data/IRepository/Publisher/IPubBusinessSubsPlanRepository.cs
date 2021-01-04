/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 September 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 06 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data{

    /// <summary>
    /// This class implements standard database logic and operations for PubBusinessSubsPlan entity.
    /// </summary>
    public interface IPubBusinessSubsPlanRepository:IBaseRepository<PubBusinessSubsPlan> {
        /// <summary>
        /// Gets the by publisher tenant and application and plan identifier.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="appId">The application identifier.</param>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns <see cref="PubBusinessSubsPlan"/> that matches given publisher tenant, app id and subscription plan id.</returns>
        Task<PubBusinessSubsPlan> GetByPublisherTenantAndAppAndPlanIdAsync(Guid publisherTenantId, Guid appId, Guid subscriptionPlanId, CancellationToken cancellationToken = default(CancellationToken));

    }
}
