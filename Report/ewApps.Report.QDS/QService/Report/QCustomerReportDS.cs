/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Customer Report 
    /// </summary>
    public class QCustomerReportDS :BaseDS<BaseDTO>, IQCustomerReportDS {

    #region Local Member
    IQCustomerReportRepository _customerReportRepository;
    IUserSessionManager _userSessionManager;
    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="customerReportRepository"></param>
    /// <param name="cacheService"></param>
    public QCustomerReportDS(IQCustomerReportRepository customerReportRepository,  IUserSessionManager userSessionManager) : base(customerReportRepository) {
      _customerReportRepository = customerReportRepository;
       _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<CustomerReportDTO>> GetCustomerListByTenantAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession us = _userSessionManager.GetSession();
      return await _customerReportRepository.GetCustomerListByTenantAsync(filter, us.TenantId,us.AppId,token);
    }

    #endregion
  }
}
