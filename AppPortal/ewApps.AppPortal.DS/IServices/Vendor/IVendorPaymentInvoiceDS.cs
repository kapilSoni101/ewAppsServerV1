/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 02 Aug 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using ewApps.Core.Money;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {


    public interface IVendorPaymentInvoiceDS {

        #region Get

        /// <summary>
        /// Get vendor invoice list by tenant id. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendorAPInvoiceDTO>> GetInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">Unique invoice id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<VendorAPInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceId id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        Task<object> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by invoice.
        /// </summary>
        /// <param name="invoiceId">filter by invoice.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<VendorPaymentDetailDTO>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

    Task<List<VendorAPInvoiceDTO>> GetInvoiceListByVendorAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Get

  }
}
