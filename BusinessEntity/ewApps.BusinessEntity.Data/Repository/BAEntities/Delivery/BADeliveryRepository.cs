// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Souarbh Agrawal
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
    /// This class contains methods to perform all database operations related to Invoice and related information (like Data Transfer Object).
    /// </summary>
    public class BADeliveryRepository:BaseRepository<BADelivery, BusinessEntityDbContext>, IBADeliveryRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BADeliveryRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BADeliveryRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Business

        #region Get

        /// <inheritdoc/>
        public IQueryable<BusBADeliveryDTO> GetDeliveryListByBusinessTenantId(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO) {
            string sql = @"SELECT ID, ERPDeliveryKey, ERPConnectorKey, ERPCustomerKey, CustomerID, CustomerName, ContactPerson, CustomerRefNo
                            , LocalCurrency, Status, StatusText, PostingDate, DeliveryDate,  DocumentDate,  SalesEmployee, Owner, TotalBeforeDiscount, TotalBeforeDiscountFC, Discount, DiscountFC
                            , Freight, FreightFC, Tax, TaxFC, TotalPaymentDue, TotalPaymentDueFC, Remarks, ShipToAddress, ERPBillToAddressKey, BillToAddress, ShippingType, ShippingTypeText, TrackingNo
                            , StampNo, PickAndPackRemarks, ERPShipToAddressKey, ShipFromAddress, ShipFromAddressKey, ERPDocNum
                            FROM be.BADelivery
                            WHERE TenantId=@BusinessTenantId AND DocumentDate BETWEEN @FromDate AND @ToDate";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            return _context.BusBADeliveryDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam });
        }

        /// <inheritdoc/>
        public async Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken) {
            string sql = @"SELECT d.ID, ERPDeliveryKey, ERPCustomerKey, CustomerID, CustomerName, ContactPerson, CustomerRefNo, 
                            LocalCurrency, PostingDate, DeliveryDate, DocumentDate, d.CreatedOn, tuCreatedBy.FullName AS 'CreatedByName', d.UpdatedOn, tuUpdateBy.FullName AS 'UpdatedByName', 
                            BillToAddress, Status, StatusText, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue,
                            ShipFromAddressKey, ShipFromAddress, TrackingNo, ShippingType, ShippingTypeText, SalesEmployee, [Owner], Remarks, ShipToAddress, ERPShipToAddressKey, ERPDocNum
                            FROM be.BADelivery AS d
                            INNER JOIN am.TenantUser as tuCreatedBy ON d.CreatedBy= tuCreatedBy.ID
                            INNER JOIN am.TenantUser as tuUpdateBy ON d.UpdatedBy= tuUpdateBy.ID
                            where d.ID= @DeliveryId AND d.TenantId=@BusinessTenantId AND d.Deleted= 0";


            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter deliveryIdParam = new SqlParameter("DeliveryId", deliveryId);
            return await _context.BusBADeliveryViewDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deliveryIdParam }).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public IQueryable<CustBADeliveryDTO> GetDeliveryListByBusinessTenantIdForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO) {
            //string sql = @"SELECT ID, ERPDeliveryKey, ERPConnectorKey, ERPCustomerKey, CustomerID, CustomerName, ContactPerson, CustomerRefNo
            //                      , LocalCurrency, Status, StatusText, PostingDate, DeliveryDate,  DocumentDate,  SalesEmployee, Owner, TotalBeforeDiscount, TotalBeforeDiscountFC, Discount, DiscountFC
            //                      , Freight, FreightFC, Tax, TaxFC, TotalPaymentDue, TotalPaymentDueFC, Remarks, ShipToAddress, ERPBillToAddressKey, BillToAddress, ShippingType, ShippingTypeText, TrackingNo
            //                      , StampNo, PickAndPackRemarks, ERPShipToAddressKey, ShipFromAddress, ShipFromAddressKey
            //                      FROM be.BADelivery
            //                      WHERE TenantId=@BusinessTenantId";

            string sql = @"SELECT  d.ID, ERPDeliveryKey, d.ERPConnectorKey,  d.ERPCustomerKey, CustomerID,  d.CustomerName, ContactPerson, CustomerRefNo
, LocalCurrency,  d.Status,  d.StatusText, PostingDate, DeliveryDate,  DocumentDate,  SalesEmployee, Owner, TotalBeforeDiscount, TotalBeforeDiscountFC, Discount, DiscountFC
, Freight, FreightFC, Tax, TaxFC, TotalPaymentDue, TotalPaymentDueFC,  d.Remarks, ShipToAddress, ERPBillToAddressKey, BillToAddress,  d.ShippingType,  d.ShippingTypeText, TrackingNo, StampNo, PickAndPackRemarks, ERPShipToAddressKey, ShipFromAddress, ShipFromAddressKey, d.ERPDocNum
FROM be.BADelivery as d
INNER JOIN be.BACustomer as c on d.CustomerId=c.ID
WHERE c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND d.DocumentDate BETWEEN @FromDate AND @ToDate ";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            return _context.CustBADeliveryDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, fromDateParam, toDateParam });
        }

        /// <inheritdoc/>
        public async Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust(Guid businessPartnerTenantId, Guid deliveryId, CancellationToken cancellationToken) {
            string sql = @"SELECT Distinct d.ID, ERPDeliveryKey, d.ERPCustomerKey, CustomerID, d.CustomerName, ContactPerson, CustomerRefNo, 
                            LocalCurrency, PostingDate, DeliveryDate, DocumentDate, d.CreatedOn, tuCreatedBy.FullName AS 'CreatedByName', d.UpdatedOn, tuUpdateBy.FullName AS 'UpdatedByName',
                            BillToAddress, d.Status, d.StatusText, TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue,
                            ShipFromAddressKey, ShipFromAddress, TrackingNo, d.ShippingType, d.ShippingTypeText, SalesEmployee, [Owner], d.Remarks, ShipToAddress, ERPShipToAddressKey, d.ERPDocNum
                            FROM be.BADelivery AS d
                            INNER JOIN am.TenantUser as tuCreatedBy ON d.CreatedBy= tuCreatedBy.ID
                            INNER JOIN am.TenantUser as tuUpdateBy ON d.UpdatedBy= tuUpdateBy.ID
							INNER JOIN be.BACustomer as c ON c.ID=d.CustomerID
                            where d.ID=@DeliveryId AND c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND d.Deleted= 0";


            SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
            SqlParameter deliveryIdParam = new SqlParameter("DeliveryId", deliveryId);
            return await _context.CustBADeliveryViewDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deliveryIdParam }).FirstOrDefaultAsync(cancellationToken);
        }


        #endregion

        #endregion

    }
}
