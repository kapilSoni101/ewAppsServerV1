/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal<sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal/Amit Mundra 
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
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.Data {
    /// <summary>
    /// This class contains methods to perform all database operations related to Invoice item and related information (like Data Transfer Object).
    /// </summary>
    public class BAPurchaseOrderRepository:BaseRepository<BAPurchaseOrder, BusinessEntityDbContext>, IBAPurchaseOrderRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BAARInvoiceItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BAPurchaseOrderRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken)) {
            string sql = @"SELECT ID,ERPPurchaseOrderKey, ERPConnectorKey, ERPVendorKey, VendorId, VendorName, 
ContactPerson, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
BillToAddress, ShippingType, ShippingTypeText, ERPDocNum FROM be.BAPurchaseOrder AS so
                            WHERE so.TenantId=@BusinessTenantId AND so.Deleted=@Deleted AND so.DocumentDate BETWEEN @FromDate AND @ToDate
                            ORDER BY so.CreatedOn DESC";

            SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
            SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
            SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
            return await _context.BAPurchaseOrderDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, fromDateParam, toDateParam }).ToListAsync(token);
        }
    /// <inheritdoc/>
    public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdForVendAsync(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken token = default(CancellationToken))
    {
      string sql = @"SELECT so.ID,so.ERPPurchaseOrderKey, so.ERPConnectorKey, so.ERPVendorKey, so.VendorId, so.VendorName, 
ContactPerson, so.LocalCurrency, so.Status, so.StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, so.Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
BillToAddress, so.ShippingType, so.ShippingTypeText, so.ERPDocNum FROM be.BAPurchaseOrder AS so
                           INNER JOIN be.BAVendor as c on so.VendorId=c.ID
WHERE c.BusinessPartnerTenantId=@BusinessPartnerTenantId AND so.Deleted=@Deleted AND so.DocumentDate BETWEEN @FromDate AND @ToDate
ORDER BY so.createdon DESC";
      SqlParameter businessTenantIdParam = new SqlParameter("BusinessPartnerTenantId", businessPartnerTenantId);
      SqlParameter deletedParam = new SqlParameter("Deleted", listDateFilterDTO.Deleted);
      SqlParameter fromDateParam = new SqlParameter("FromDate", listDateFilterDTO.FromDate);
      SqlParameter toDateParam = new SqlParameter("ToDate", listDateFilterDTO.ToDate);
      return await _context.BAPurchaseOrderDTOQuery.FromSql(sql, new object[] { businessTenantIdParam, deletedParam, fromDateParam, toDateParam }).ToListAsync(token);
    }

    /// <inheritdoc/>
    public async Task<BAPurchaseOrderViewDTO> GetPurcahseDetailByPOIdAsync(Guid businessTenantId, Guid poId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT so.ERPPurchaseOrderKey, ERPConnectorKey, ERPVendorKey,  VendorName, ContactPerson, LocalCurrency, Status, StatusText, PostingDate, DeliveryDate, DocumentDate, PickAndPackRemarks,
                            TotalBeforeDiscount, Discount, Freight, Tax, TotalPaymentDue, Remarks, ShipFromAddress, ShipFromAddressKey, ERPShipToAddressKey, ShipToAddress, ERPBillToAddressKey,
                            BillToAddress, ShippingType, ShippingTypeText, so.CreatedOn, so.UpdatedOn, 
							so.ERPDocNum FROM be.BAPurchaseOrder AS so 
                            WHERE  so.Deleted=@Deleted AND so.ID=@PurchaseOrderId";

           // SqlParameter businessTenantIdParam = new SqlParameter("BusinessTenantId", businessTenantId);
            SqlParameter deletedParam = new SqlParameter("Deleted", false);
            SqlParameter soIDParam = new SqlParameter("PurchaseOrderId", poId);
            return await _context.BAPurchaseOrderViewDTOQuery.FromSql(sql, new object[] { deletedParam, soIDParam }).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<BAPurchaseOrderItemDTO>> GetPurchaseOrderItemListByPOIdAsync(Guid poId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ERPPurchaseOrderItemKey, ERPConnectorKey, ERPPurchaseOrderKey, ERPItemKey, SerialOrBatchNo, ItemName, Quantity, QuantityUnit, UnitPrice, Unit,
                        DiscountPercent, DiscountAmount, TaxCode, TaxPercent, TotalLC, Whse, BlanketAgreementNo, ShipFromAddress,'' AS ShipFromAddressKey,  
                        ShipToAddress, ERPShipToAddressKey, ItemId, LotNo, PurchaseOrderId 
                        FROM be.BAPurchaseOrderItem WHERE PurchaseOrderId=@POID";

            SqlParameter soIDParam = new SqlParameter("POID", poId);
            return await _context.BAPurchaseOrderItemDTOQuery.FromSql(sql, new object[] { soIDParam }).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<BAPurchaseOrderAttachmentDTO>> GetPurchaseOrderAttachmentListByIdAsync(Guid purchaseOrderId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, PurchaseOrderAttachmentKey, PurchaseOrderId, ERPPurchaseOrderKey, Name, [FreeText],
                            AttachmentDate FROM be.BAPurchaseOrderAttachment as da
                            WHERE da.PurchaseOrderId=@PurchaseOrderId";

            SqlParameter salesOrderIdIdParam = new SqlParameter("PurchaseOrderId", purchaseOrderId);
            return await _context.BAPurchaseOrderAttachmentDTOQuery.FromSql(sql, new object[] { salesOrderIdIdParam }).ToListAsync(cancellationToken);
        }

        #endregion Public Methods

    }
}
