/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 Septemeber 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.Common;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;
using System.Threading;
using ewApps.Payment.DTO;
using ewApps.Core.BaseService;


namespace ewApps.Payment.DS {
    /// <summary>
    /// Interface contains method to get payment data.
    /// </summary>
    public interface IQInvoiceItemDS {

        #region Get

        /// <summary>
        /// Get invoice items by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceItemViewDTO>> GetInvoiceItemsByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
