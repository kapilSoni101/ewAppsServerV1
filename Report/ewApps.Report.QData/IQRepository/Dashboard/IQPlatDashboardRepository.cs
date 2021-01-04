/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Platform Dashboard and services related to it
    /// </summary>
    public interface IQPlatDashboardRepository:IBaseRepository<BaseDTO> {

        /// <summary>
        /// Get All Application and Publisher Count 
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationPublisherCountDTO>> GetAllApplicationPublisherCountListAsync(CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Publisher and Tenant Count 
        /// </summary>
        /// <returns></returns>
        Task<List<PublisherTenantCountDTO>> GetAllPublisherTenantCountListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application and businesscount for platform
        /// </summary>
        /// <returns></returns>
        Task<PlatAppAndBusinessCountDTO> GetAllPlatApplicationAndBusinessCountAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business And Subscription Count For Platform
        /// </summary>
        /// <returns></returns>
        Task<AapNameAndBusinessCountDTO> GetAllPlatAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business And Subscription Count For Platform
        /// </summary>
        /// <returns></returns>
        Task<ShipmentAapNameAndBusinessCountDTO> GetAllPlatShipAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Application Count List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<ApplicationUserCountDTO>> GetAllPlatApplicationUserCountListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business Count List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessCountDTO>> GetAllPlatBusinessCountListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business Name with highst AMount  List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessNameAndSumCount>> GetAllPlatBusinessNameWithHeightestAmountListAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business Name with highst AMount  List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<ShipmentBusinessNameAndSumCount>> GetAllPlatShipBusinessNameWithMaximumShippedOrderListAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Business Count Per Month List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<BusinessAddedCountAndMonthDTO>> GetAllPlatBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Shipment Business Count Per Month List For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllPlatShipBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Service name with Its Shipment Count For Platform 
        /// </summary>
        Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPlatformAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get app detail 
        /// </summary>
        Task<List<AppDTO>> GetAllPlatAppListAsync(CancellationToken token = default(CancellationToken));
    }
}
