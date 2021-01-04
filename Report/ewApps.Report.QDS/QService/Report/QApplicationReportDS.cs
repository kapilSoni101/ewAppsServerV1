// Hari sir comment
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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Application Report
    /// </summary>
    public class QApplicationReportDS : BaseDS<BaseDTO>, IQApplicationReportDS
  {

    #region Local Member
    IQApplicationReportRepository _applicationReportRepository;
    IUserSessionManager _userSessionManager;

        /// <summary>
        /// Its default entery for business tenant application.
        /// </summary>
        public const string BusinessApplicationId = "F4952EF3-F1BD-4621-A5F9-290FD09BC81B";
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="applicationReportRepository"></param>
        /// <param name="cacheService"></param>
        public QApplicationReportDS(IQApplicationReportRepository applicationReportRepository,  IUserSessionManager userSessionManager) : base(applicationReportRepository)
    {
      _applicationReportRepository = applicationReportRepository;
      _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Get

    #region Publisher
    ///<inheritdoc/>
    public async Task<List<ApplicationReportDTO>> GetApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession session = _userSessionManager.GetSession();
      return await _applicationReportRepository.GetApplicationListAsync(filter,session.TenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetApplicaitionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      return await _applicationReportRepository.GetApplicaitionNameListByTenantIdAsync(tenantId, token);
    }
    #endregion

    #region PlatForm
    ///<inheritdoc/>
    public async Task<List<PlatApplicationReportDTO>> GetPFApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      return await _applicationReportRepository.GetPFApplicationListAsync(filter, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFApplicaitionNameListAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
      return await _applicationReportRepository.GetPFApplicaitionNameListAsync(publisherId,token);
    }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetBusinessAppSubscriptionInfoDTOAsync(Guid tenantId) {
            Guid homeAppId = new Guid(BusinessApplicationId);
            UserSession session = _userSessionManager.GetSession();
            List<NameDTO> list = await _applicationReportRepository.GetBusinessAppSubscriptionInfoDTOByBusinessId(tenantId, homeAppId);
            return list;
        }
        #endregion


        #endregion Get
    }
}
