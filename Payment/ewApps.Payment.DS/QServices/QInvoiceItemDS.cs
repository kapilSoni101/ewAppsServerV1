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
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {
    /// <summary>
    /// A wrapper class contains method to get invoice items data w.r.t Invoice.
    /// </summary>
    public class QInvoiceItemDS:IQInvoiceItemDS {

        #region Local variables

        IQInvoiceItemRepository _qInvoiceItemRepository;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentRep"></param>
        public QInvoiceItemDS(IQInvoiceItemRepository paymentRep) {
            _qInvoiceItemRepository = paymentRep;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice items by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceItemViewDTO>> GetInvoiceItemsByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _qInvoiceItemRepository.GetInvoiceItemsByInvoiceIdAsync(invoiceId, token);
        }

        #endregion Get

    }
}
