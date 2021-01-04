/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.Data {

    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice item and related information (like Data Transfer Object).
    /// </summary>
    public class BAARInvoiceItemRepository:BaseRepository<BAARInvoiceItem, BusinessEntityDbContext>, IBAARInvoiceItemRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BAARInvoiceItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BAARInvoiceItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice items by invoice id.
        /// </summary>
        /// <param name="invoiceId">InvoiceID</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceItemDQ>> GetInvoiceItemListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            string query = @"SELECT[ID],[CreatedBy], [CreatedOn],[UpdatedBy], [UpdatedOn], [TenantId]
                            ,[ARInvoiceID], [LotNo],[SerialOrBatchNo], [ItemId], [ItemName],[ItemType]
                            ,[Quantity],[QuantityUnit], [UnitPrice], [UnitPriceFC], [Unit]
                            ,[DiscountPercent], [DiscountAmount],[DiscountAmountFC],
                            [TaxCode],[TaxPercent], [TotalLC], [TotalLCFC]
                            ,[ShipFromAddress], [ShipToAddress], [BillToAddress]
                            FROM [dbo].[BAARInvoiceItem]
                            WHERE ARInvoiceID=@invoiceId ";

            SqlParameter parameterS = new SqlParameter("@invoiceId", invoiceId);
            List<BAARInvoiceItemDQ> invoiceDetailDTOs = await GetQueryEntityListAsync<BAARInvoiceItemDQ>(query, new object[] { parameterS }, token);
           
            return invoiceDetailDTOs;
        }

        #endregion Get

    }
}
