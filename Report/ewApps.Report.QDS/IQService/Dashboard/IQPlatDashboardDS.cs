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

namespace ewApps.Report.QDS {
    public interface IQPlatDashboardDS:IBaseDS<BaseDTO> {

        /// <summary>
        /// Get Dashboard for App business and subscription 
        /// </summary>
        /// <returns></returns>
        Task<PlatDashboardAppBusinessAndPublisherCount> GetAllPlatformDashboardDataForApplicationListAsync( CancellationToken token = default(CancellationToken));

       

    }

    }
