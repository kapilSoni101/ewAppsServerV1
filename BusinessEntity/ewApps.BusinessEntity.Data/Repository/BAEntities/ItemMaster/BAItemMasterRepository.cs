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
    public class BAItemMasterRepository:BaseRepository<BAItemMaster, BusinessEntityDbContext>, IBAItemMasterRepository {

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="BAARInvoiceItemRepository"/> class.
        /// </summary>
        /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
        /// <param name="sessionManager">User session manager instance to get login user details.</param>
        public BAItemMasterRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion

        #region Bussiness

        /// <inheritdoc/>
        public IQueryable<BusBAItemMasterDTO> GetItemMasterListByBusTenantId(Guid tenantId, string itemType) {
            string query = @"SELECT ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                              ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                              ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                              ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                              ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                              ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                              ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText,Remarks
                              FROM be.BAItemMaster as i
                              WHERE i.TenantId=@TenantId ORDER BY ItemName ASC";//AND i.ItemType=@ItemType
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramItemType = new SqlParameter("@ItemType", itemType);
            return _context.Query<BusBAItemMasterDTO>().FromSql(query, new object[] { paramTenantId, paramItemType });
        }


        /// <inheritdoc/>
        public BusBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemId(Guid tenantId, Guid itemId) {
            string query = @"SELECT ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                      ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                      ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                      ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText,UpdatedOn
                      ,Remarks, '' AS 'UpdatedByName'
      FROM be.BAItemMaster as i
      WHERE i.TenantId = @TenantId AND i.ID=@ID ";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramItemId = new SqlParameter("@ID", itemId);
            return _context.Query<BusBAItemMasterViewDTO>().FromSql(query, new object[] { paramTenantId, paramItemId }).FirstOrDefault();
        }

        #endregion Business

        #region Customer

        /// <summary>
        /// Get platform branding
        /// </summary>
        /// <param name="tenantId"></param>        
        /// <returns></returns>
        public IQueryable<CustBAItemMasterDTO> GetItemMasterListByBusTenantIdForCust(Guid tenantId) {
            string query = @"SELECT ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                      ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                      ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                      ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText
                      ,Remarks
      FROM be.BAItemMaster as i
      WHERE i.TenantId = @TenantId ORDER BY ItemName ASC";
            //WHERE i.TenantId = @TenantId AND i.Active ='Y' ORDER BY ItemName ASC";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            return _context.Query<CustBAItemMasterDTO>().FromSql(query, new object[] { paramTenantId });
        }


        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public CustBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemIdForCust(Guid tenantId, Guid itemId) {
            string query = @"SELECT ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                      ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                      ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                      ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText
                      ,Remarks, UpdatedOn, '' AS 'UpdatedByName'
      FROM be.BAItemMaster as i
      WHERE i.TenantId = @TenantId AND i.ID=@ID ";
            //WHERE i.TenantId = @TenantId AND i.Active ='Y' AND i.ID=@ID ";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramItemId = new SqlParameter("@ID", itemId);
            return _context.Query<CustBAItemMasterViewDTO>().FromSql(query, new object[] { paramTenantId, paramItemId }).FirstOrDefault();
        }

        #endregion Customer
    }
}
