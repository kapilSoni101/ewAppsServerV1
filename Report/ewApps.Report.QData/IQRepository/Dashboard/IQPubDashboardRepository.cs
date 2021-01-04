/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {
    /// <summary>
    /// This is the repository responsible for filtering data realted to Dashboard and services related to it
    /// </summary>
    public interface IQPubDashboardRepository:IBaseRepository<BaseDTO> {

        /// <summary>
        /// Get application and businesscount
        /// </summary>
        /// <returns></returns>
        Task<AppAndBusinessCountDTO> GetAllApplicationAndBusinessCountAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Subscription and businesscount
        /// </summary>
        /// <returns></returns>
        Task<BusinessAndSubscriptionCountDTO> GetAllBusinessAndSubscriptionCountAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Subscription and businesscount
        /// </summary>
        /// <returns></returns>
        Task<ShipmentBusinessAndSubscriptionCountDTO> GetAllShipBusinessAndSubscriptionCountAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application and businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUserCountDTO>> GetAllApplicationUserCountListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application and businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessCountDTO>> GetAllBusinessCountListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application and businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessNameAndSumCount>> GetAllBusinessNameWithHeightestAmountListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application and businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<ShipmentBusinessNameAndSumCount>> GetAllShipBusinessNameWithMaximumShippingOrdersListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Subscription and businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessAddedCountAndMonthDTO>> GetAllBusinessCountPerMonthListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get  businesscount
        /// </summary>
        /// <returns></returns>
        Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllShipBusinessCountPerMonthListAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Service name with Its Shipment Count For Platform 
        /// </summary>
        Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPublisherAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Application list for publisher
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<AppDTO>> GetAllPubAppListAsync(CancellationToken token = default(CancellationToken));


    }
}

