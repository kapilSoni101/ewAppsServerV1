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
using ewApps.Report.Common;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Support Ticket Report 
    /// </summary>
    public class QSupportTicketReportDS:BaseDS<BaseDTO>, IQSupportTicketReportDS {

    #region Local Member

    IQSupportTicketReportRepository _ticketRptRepos;
    IUserSessionManager _userSessionManager;

    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="helpDeskReportRepository"></param>
    /// <param name="cacheService"></param>
    public QSupportTicketReportDS(IQSupportTicketReportRepository ticketRptRepos,  IUserSessionManager userSessionManager) : base(ticketRptRepos) {
      _ticketRptRepos = ticketRptRepos;
      _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<PlatSupportTicketReportDTO>> GetPFSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      return await _ticketRptRepos.GetPFSupportTicketListAsync(filter, (short)SupportLevelEnum.Level4, token);
    }

    ///<inheritdoc/>
    public async Task<List<PubSupportTicketReportDTO>> GetPubSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _ticketRptRepos.GetPubSupportTicketListAsync(filter, (short)SupportLevelEnum.Level3, us.TenantId,token);
    }

    ///<inheritdoc/>
    public async Task<List<BizSupportTicketReportDTO>> GetBizPaySupportTicketListByTenantAsync(PartReportSupportTicketParamDTO filter, CancellationToken token = default(CancellationToken)) {
      //UserSession us = _userSessionManager.GetSession();
      return await _ticketRptRepos.GetBizPaySupportTicketListByTenantAsync(filter, (short)SupportLevelEnum.Level1, token);
    }

    ///<inheritdoc/>
    public async Task<List<PartSupportTicketReportDTO>> GetPartPaySupportTicketListByCustomerIdAsync(PartReportSupportTicketParamDTO filter, CancellationToken token = default(CancellationToken)) {
      
      return await _ticketRptRepos.GetPartPaySupportTicketListByCustomerIdAsync(filter,  token);
    }

    #endregion
  }
}
