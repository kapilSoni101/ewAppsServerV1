/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Amit
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
    public class BASalesOrderRepository:BaseRepository<BASalesOrder, BusinessEntityDbContext>, IBASalesOrderRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BASalesOrderRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BASalesOrderRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get sales order item list by teanntid.
        /// </summary>
        /// <param name="tenantId">Tenant unique id.</param>
        /// <param name="includeDeleted">return sales order with deleted items.</param>        
        /// <returns>return list of sales order entity.</returns>
        public async Task<List<BASalesOrder>> GetSalesOrderListByTenantIdAsync(Guid tenantId, bool includeDeleted, CancellationToken token = default(CancellationToken)) {
            if(includeDeleted) {
                return await _context.BASalesOrder.Where(so => so.TenantId == tenantId).ToListAsync(token);
            }
            return await _context.BASalesOrder.Where(so => so.TenantId == tenantId && !so.Deleted).ToListAsync(token);
        }


        /// <inheritdoc/>
        public IQueryable<BusBASalesOrderDTO> GetSalesOrderListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO) {
            string sql = @"SELECT ID, ERPSalesOrderKey, ERPConnectorKey, ERPCustomerKey, CustomerId, CustomerName, ContactPerson, CustomerRefNo, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
                            SalesEmployee, Owner, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
                            BillToAddress, ShippingType, ShippingTypeText, ERPDocNum FROM be.BASalesOrder AS so
                            WHERE so.TenantId=@BusinessTenantId AND so.Deleted=@Deleted AND so.DocumentDate BETWEEN @FromDate AND @ToDate
                            ORDER BY ERPSalesOrderKey DESC";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            return _context.BusBASalesOrderDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, fromDateParam, toDateParam });
        }


        /// <inheritdoc/>
        public async Task<BusBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ERPSalesOrderKey, ERPConnectorKey, ERPCustomerKey, CustomerName, ContactPerson, CustomerRefNo, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
                            SalesEmployee, Owner, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
                            BillToAddress, ShippingType, ShippingTypeText, so.CreatedOn, so.UpdatedOn, so.ERPDocNum FROM be.BASalesOrder AS so
                            WHERE so.TenantId=@BusinessTenantId AND so.Deleted=@Deleted AND so.ID=@SalesOrderId";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", false);
            SqlParameter soIDParam = new SqlParameter("SalesOrderId", soId);
            return await _context.BusBASalesOrderViewDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, soIDParam }).FirstOrDefaultAsync(cancellationToken);
        }

    /// <inheritdoc/>
     public IQueryable<CustBASalesOrderDTO> GetSalesOrderListByBusinessTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO)
    {
      //string sql = @"SELECT Id, ERPSalesOrderKey, ERPConnectorKey, ERPCustomerKey, CustomerName, ContactPerson, CustomerRefNo, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
      //                      SalesEmployee, Owner, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
      //                      BillToAddress, ShippingType, ShippingTypeText FROM be.BASalesOrder AS so
      //                      WHERE so.TenantId=@BusinessTenantId AND so.Deleted=@Deleted AND so.DocumentDate BETWEEN @FromDate AND @ToDate
      //                      ORDER BY ERPSalesOrderKey DESC";

      string sql = @"SELECT so.Id, ERPSalesOrderKey, so.ERPConnectorKey, so.ERPCustomerKey, so.CustomerName, ContactPerson, CustomerRefNo, LocalCurrency, so.Status, so.StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
SalesEmployee, Owner, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, so.Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey, BillToAddress, so.ShippingType, so.ShippingTypeText, so.CustomerId , so.ERPDocNum
 FROM be.BASalesOrder AS so
INNER JOIN be.BACustomer as c on so.CustomerId=c.ID
WHERE c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND so.Deleted=@Deleted AND so.DocumentDate BETWEEN @FromDate AND @ToDate
ORDER BY so.createdon DESC";

      SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
      SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
      SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
      return _context.CustBASalesOrderDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, fromDateParam, toDateParam });
    }


    /// <inheritdoc/>
     public async Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken))
    {
      string sql = @"SELECT ERPSalesOrderKey, ERPConnectorKey, ERPCustomerKey, CustomerName, ContactPerson, CustomerRefNo, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
                            SalesEmployee, Owner, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
                            BillToAddress, ShippingType, ShippingTypeText, ERPDocNum FROM be.BASalesOrder AS so
                            WHERE so.Deleted=@Deleted AND so.ID=@SalesOrderId";

      SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", false);
      SqlParameter soIDParam = new SqlParameter("SalesOrderId", soId);
      return await _context.CustBASalesOrderViewDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, soIDParam }).FirstOrDefaultAsync(cancellationToken);
    }

    #endregion Get

  }
}
