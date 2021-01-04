using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS {

    public interface IQPaymentReportDS :IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All PaymentRecieve List By Tenant For Business
    /// </summary>
    /// <returns></returns> 
    Task<List<BizPaymentReceivedReportDTO>> GetBizPayRecPayListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All CustomerWise Payment List By Tenant For Business
    /// </summary>
    Task<List<BizCustomerWisePaymentReportDTO>> GetBizPayCustPaymentListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Payment List By Tenant For Business Partner
    /// </summary>
    /// <returns></returns> 
    Task<List<PartPaymentReportDTO>> GetPartPaymentListAsyncByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

  }
}
