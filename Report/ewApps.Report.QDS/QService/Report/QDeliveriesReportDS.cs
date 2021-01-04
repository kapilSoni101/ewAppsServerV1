/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {   
        /// <summary>
        /// This class Contain Business Login of Deliveries Report
        /// </summary>
        public class QDeliveriesReportDS:BaseDS<BaseDTO>, IQDeliveriesReportDS {

        #region Local Member
        IQDeliveriesReportRepository _deliveriesReportRepository;
            IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="deliveriesReportRepository"></param>
        /// <param name="cacheService"></param>
        public QDeliveriesReportDS(IQDeliveriesReportRepository deliveriesReportRepository, IUserSessionManager userSessionManager) : base(deliveriesReportRepository) {
            _deliveriesReportRepository = deliveriesReportRepository;
                _userSessionManager = userSessionManager;
            }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<BizCustSalesDeliveriesReportDTO>> GetBizCustSalesDeliveriesListByTenantIdAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _deliveriesReportRepository.GetBizCustSalesDeliveriesListByTenantIdAsync(filter, us.TenantId, token);

        }
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartCustDeliveriesReportDTO>> GetPartCustSalesDeliveriesListByCustomerIdAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken)) {
            return await _deliveriesReportRepository.GetPartCustSalesDeliveriesListByCustomerIdAsync(filter,  token);
        }
        #endregion
    }
}
