/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 4 February 2020
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 4 February 2020
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS{    

    // This class Contain Business Logic Of Vendor Dashboard 
    public interface IQVendDashboardDS:IBaseDS<BaseDTO> {

        Task<VendApInvoiceStatusDTO> GetVendInvoicesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<VendOrdersLineStatusDTO> GetVendOrderLineStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<VendOrderStatusDTO> GetVendOrderStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<List<VendOpenLinesListDTO>> GetAllVendOpenLineListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<List<VendRecentAPInvoices>> GetAllVendRecentAPInvoicesListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<List<VendRecentPurchaseOrder>> GetAllVendRecentPurchaseOrderListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    }
}
