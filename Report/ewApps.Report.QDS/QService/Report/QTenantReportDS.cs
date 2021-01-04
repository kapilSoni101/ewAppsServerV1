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
    /// This class Contain Business Logic of Tenant Report 
    /// </summary>
    public class QTenantReportDS:BaseDS<BaseDTO>, IQTenantReportDS {

    #region Local Member
    IQTenantReportRepository _tenantReportRepository;
    IUserSessionManager _userSessionManager;
        //ITenantSubscriptionRepository _tenantSubscriptionRepository;
        /// <summary>
        /// Its default entery for business tenant application.
        /// </summary>
        public const string BusinessApplicationId = "F4952EF3-F1BD-4621-A5F9-290FD09BC81B";

    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="tenantReportRepository"></param>
    /// <param name="cacheService"></param>
    public QTenantReportDS(IQTenantReportRepository tenantReportRepository, IUserSessionManager userSessionManager) : base(tenantReportRepository) {
      _tenantReportRepository = tenantReportRepository;
      _userSessionManager = userSessionManager;
            
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<TenantReportDTO>> GetTenantListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      Guid tenantId = _userSessionManager.GetSession().TenantId;
            Guid appId = _userSessionManager.GetSession().AppId;
            Guid homeAppId = new Guid(BusinessApplicationId);
      return await _tenantReportRepository.GetTenantListAsync(filter, tenantId, homeAppId, appId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetServiceNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetServiceNameListByTenantIdAsync(tenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetTenantNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetTenantNameListByAppIdAsync(appId, tenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            Guid tenantId = _userSessionManager.GetSession().TenantId;
            return await _tenantReportRepository.GetServiceNameListByAppIdAsync(appId, tenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetPFServiceNameListByAppIdAsync(appId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetTenantNameListBySubscriptionPlanIdAsync(Guid subscriptionPlanId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetTenantNameListBySubscriptionPlanIdAsync(subscriptionPlanId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFTenantNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetPFTenantNameListByAppIdAsync(appId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetPFTenantNameListByPublisherIdAsync(publisherId, token);
    }

    ///<inheritdoc/>
    public async Task<List<NameDTO>> GetPFActiveTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken)) {
      return await _tenantReportRepository.GetPFActiveTenantNameListByPublisherIdAsync(publisherId, token);
    }


        #region Platform 

        ///<inheritdoc/>
        public async Task<List<PlatformTenantDTO>> GetTenantReportListOnPlatformAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            //Guid tenantId = _userSessionManager.GetSession().TenantId;
            //Guid appId = _userSessionManager.GetSession().AppId;
            Guid homeAppId = new Guid(BusinessApplicationId);
            return await _tenantReportRepository.GetTenantReportListOnPlatformAsync(filter, homeAppId, token);
            
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetPFServiceNameListByPlanIdAsync(Guid planId, CancellationToken token = default(CancellationToken)) {
            return await _tenantReportRepository.GetPFServiceNameListByPlanIdAsync(planId, token);
        }

        #endregion

        #endregion
    }
}
