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
using ewApps.Report.QData;

namespace ewApps.Report.QDS {
    /// <summary>
    /// This class Contain Business Login of Publisher Report
    /// </summary>
    public class QPublisherReportDS :BaseDS<BaseDTO>, IQPublisherReportDS {

    #region Local Member
    IQPublisherReportRepository _publisherReportRepository;
    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="publisherReportRepository"></param>
    /// <param name="cacheService"></param>
    public QPublisherReportDS(IQPublisherReportRepository publisherReportRepository) : base(publisherReportRepository) {
      _publisherReportRepository = publisherReportRepository;
    }

    #endregion Constructor

    #region Get

    

    #region PlatForm
    ///<inheritdoc/>
    public async Task<List<PlatPublisherReportDTO>> GetPFPublisherListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      return await _publisherReportRepository.GetPFPublisherListAsync(filter, token);
    }
    #endregion

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPublisherNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
      return await _publisherReportRepository.GetPublisherNameListByAppIdAsync(appId,token);
    }



    #endregion Get

  }
}
