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

    public interface IQInvoiceReportDS:IBaseDS<BaseDTO> {

        /// <summary>
        /// Get All Invoice List By Tenant For Business
        /// </summary>
        /// <returns></returns>  
        Task<List<BizInvoiceReportDTO>> GetBizPayInvoiceListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Invoices For Vendor Application on Business Portal
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendAPInvoicesReportDTO>> GetBizVendInvoiceListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Invoice List By Tenant For Business Partner 
        /// </summary>
        /// <returns></returns>
        Task<List<PartInvoiceReportDTO>> GetPartPayInvoiceListByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));
    }
}
