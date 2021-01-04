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
using ewApps.Report.DTO;


namespace ewApps.Report.QDS {

    public interface IQSupportTicketReportDS :IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All Support Ticket List Report For Platform
    /// </summary>
    /// <returns></returns> 
    Task<List<PlatSupportTicketReportDTO>> GetPFSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Support Ticket List Report For Puslisher
    /// </summary>
    /// <returns></returns> 
    Task<List<PubSupportTicketReportDTO>> GetPubSupportTicketListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Support Ticket List Report by Tenant For Business
    /// </summary>
    /// <returns></returns> 
    Task<List<BizSupportTicketReportDTO>> GetBizPaySupportTicketListByTenantAsync(PartReportSupportTicketParamDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Support Ticket List Report by Customer For Business Partner
    /// </summary>
    /// <returns></returns> 
    Task<List<PartSupportTicketReportDTO>> GetPartPaySupportTicketListByCustomerIdAsync(PartReportSupportTicketParamDTO filter,  CancellationToken token = default(CancellationToken));
  }
}
