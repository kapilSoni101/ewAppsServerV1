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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice item and related information (like Data Transfer Object).
    /// </summary>
    public class BADeliveryItemRepository:BaseRepository<BADeliveryItem, BusinessEntityDbContext>, IBADeliveryItemRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BADeliveryItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BADeliveryItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Get

        /// <inheritdoc/>
        public IQueryable<string> GetDeliveryItemNameListByDeliveryId(Guid deliveryId) {
            return _context.BADeliveryItem.Where(i => i.DeliveryID == deliveryId && i.Deleted == false).Select(k => k.ItemName);
        }


        /// <inheritdoc/>
        public IQueryable<BusBADeliveryItemDTO> GetDeliveryItemListByDeliveryId(Guid deliveryId) {
            string sql = @"SELECT SerialOrBatchNo, ID, ERPItemKey, ItemName, Quantity, UnitPrice, DiscountPercent, TaxCode, TaxPercent, BlanketAgreementNo, Freight FROM be.BADeliveryItem AS di
                           Where di.DeliveryID=@DeliveryId";

            SqlParameter deliveryIdParam = new SqlParameter("DeliveryId", deliveryId);
            return _context.BusBADeliveryItemDTOQuery.FromSql(sql, new object[] { deliveryIdParam });
        }

        /// <inheritdoc/>
        public IQueryable<CustBADeliveryItemDTO> GetDeliveryItemListByDeliveryIdForCust(Guid deliveryId)
        {
          string sql = @"SELECT SerialOrBatchNo, ID, ERPItemKey, ItemName, Quantity, UnitPrice, DiscountPercent, TaxCode, TaxPercent, BlanketAgreementNo, Freight FROM be.BADeliveryItem AS di
                               Where di.DeliveryID=@DeliveryId";

          SqlParameter deliveryIdParam = new SqlParameter("DeliveryId", deliveryId);
          return _context.CustBADeliveryItemDTOQuery.FromSql(sql, new object[] { deliveryIdParam });
        }


    #endregion

  }
}
