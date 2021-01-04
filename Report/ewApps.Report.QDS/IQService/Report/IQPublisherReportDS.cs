/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 5 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 5 February 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS {
    public interface IQPublisherReportDS :IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All Publisher Report List For Platform
    /// </summary>
    /// <returns></returns>
    Task<List<PlatPublisherReportDTO>> GetPFPublisherListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Publisher Name  List By App  
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetPublisherNameListByAppIdAsync(Guid appId,  CancellationToken token = default(CancellationToken));

  }
}
