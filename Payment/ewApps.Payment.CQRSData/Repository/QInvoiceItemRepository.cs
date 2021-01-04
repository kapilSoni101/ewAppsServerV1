/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 29 August 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public class QInvoiceItemRepository:QBaseRepository<QPaymentDBContext>, IQInvoiceItemRepository {

        #region Local         

        #endregion Local

        #region Constructor         

        /// <summary>
        /// Initializes a new instance of the <see cref="QInvoiceItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of CQRSAppPortalDbContext to executes database operations.</param>        
        public QInvoiceItemRepository(QPaymentDBContext context): base(context) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceItemViewDTO>> GetInvoiceItemsByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {

            string query = @"SELECT inv.* FROM BE.BAARInvoiceItem inv                             
                            WHERE inv.ArInvoiceID = @invoiceId  ";

            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            return await GetQueryEntityListAsync<BAARInvoiceItemViewDTO>(query, new object[] { parameterS });
        }


        #endregion Get

    }
}
