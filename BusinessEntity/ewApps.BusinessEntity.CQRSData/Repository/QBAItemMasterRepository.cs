using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ewApps.BusinessEntity.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.QData {

    public class QBAItemMasterRepository:IQBAItemMasterRepository {

        private QBusinessEntityDbContext _context;

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="QBAItemMasterRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public QBAItemMasterRepository(QBusinessEntityDbContext context) {
            _context = context;
        }

        #endregion


        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public BusBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemId(Guid tenantId, Guid itemId, string itemType) {
            string query = @"SELECT i.ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                      ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                      ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                      ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText,i.UpdatedOn
                      ,Remarks, tu.FullName AS 'UpdatedByName'
                    FROM be.BAItemMaster as i
                    INNER JOIN am.TenantUser AS tu ON tu.ID=i.UpdatedBy
                    WHERE i.TenantId = @TenantId AND i.ID=@ID ";// AND i.ItemType=@ItemType

            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramItemId = new SqlParameter("@ID", itemId);
            SqlParameter paramItemType = new SqlParameter("@ItemType", itemType);

            return _context.Query<BusBAItemMasterViewDTO>().FromSql(query, new object[] { paramTenantId, paramItemId, paramItemType }).FirstOrDefault();
        }

        /// <summary>
        /// Gets the item master list by bus tenant identifier and item identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public CustBAItemMasterViewDTO GetItemMasterByBusTenantIdAndItemIdForCust(Guid tenantId, Guid itemId) {
            string query = @"SELECT i.ID,ERPConnectorKey,ERPItemKey,ItemType,ItemName,BarCode,Price,PriceFC,PriceUnit
                             ,PriceUniText,ShippingType,ShippingTypeText,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                             ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                             ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseVolume,PurchaseVolumeUnit,PurchaseVolumeUnitText
                             ,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText,SalesLength,SalesLengthUnit,SalesLengthUnitText
                             ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                             ,SalesVolume,SalesVolumeUnit,SalesVolumeUnitText,SalesWeight,SalesWeightUnit,SalesWeightUnitText
                             ,Remarks, i.UpdatedOn, tu.FullName AS 'UpdatedByName'
                            FROM be.BAItemMaster as i
                            INNER JOIN am.TenantUser AS tu ON tu.ID=i.UpdatedBy
                            WHERE i.TenantId = @TenantId AND i.ID=@ID ";
            SqlParameter paramTenantId = new SqlParameter("@TenantId", tenantId);
            SqlParameter paramItemId = new SqlParameter("@ID", itemId);
            return _context.Query<CustBAItemMasterViewDTO>().FromSql(query, new object[] { paramTenantId, paramItemId }).FirstOrDefault();
        }

    }
}
