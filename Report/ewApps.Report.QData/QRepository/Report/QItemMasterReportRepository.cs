/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Item Master Report and services related to it
    /// </summary>
    public class QItemMasterReportRepository:BaseRepository<BaseDTO, QReportDbContext>, IQItemMasterReportRepository {

        #region Constructor 

        /// <summary>
        ///  Constructor initializing the base variables
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sessionManager"></param>
        public QItemMasterReportRepository(QReportDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
        }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<VendItemMasterReportDTO>> GetBizVendItemMasterListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPItemKey,ItemName,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText
                                      ,SalesLength,SalesLengthUnit,SalesLengthUnitText
                                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                                      ,SalesWeight,SalesWeightUnit,SalesWeightUnitText
                                      ,Deleted,Remarks,'' AS PreferredVendor
                                      FROM be.BAItemMaster as i
                                      WHERE i.TenantId = @TenantId  AND (CreatedOn BETWEEN @FromDate AND @ToDate)
                                      ORDER BY ItemName ASC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<VendItemMasterReportDTO> vendItemMasterReportDTO = await GetQueryEntityListAsync<VendItemMasterReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return vendItemMasterReportDTO;

        }
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartItemMasterReportDTO>> GetCustItemMasterListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {

            FormattableString query = $@"SELECT ID,ERPItemKey,ItemName,ManagedItem,Active,PurchaseLength,PurchaseLengthUnit
                      ,PurchaseLengthUnitText,PurchaseWidth,PurchaseWidthUnit,PurchaseWidthUnitText,PurchaseHeight
                      ,PurchaseHeightUnit,PurchaseHeightUnitText,PurchaseWeight,PurchaseWeightUnit,PurchaseWeightUnitText
                      ,SalesLength,SalesLengthUnit,SalesLengthUnitText
                      ,SalesWidth,SalesWidthUnit,SalesWidthUnitText,SalesHeight,SalesHeightUnit,SalesHeightUnitText
                      ,SalesWeight,SalesWeightUnit,SalesWeightUnitText
                      ,Deleted,Remarks
                      FROM be.BAItemMaster as i
                      WHERE i.TenantId = @TenantId  AND (CreatedOn BETWEEN @FromDate AND @ToDate)
                      ORDER BY ItemName ASC";
            SqlParameter fromDate = new SqlParameter("@FromDate", filter.FromDate);
            SqlParameter toDate = new SqlParameter("@ToDate", filter.ToDate);
            SqlParameter tenantParam = new SqlParameter("@TenantId", tenantId);
            List<PartItemMasterReportDTO> partItemMasterReportDTO = await GetQueryEntityListAsync<PartItemMasterReportDTO>(query.ToString(), parameters: new object[] { fromDate, toDate, tenantParam });
            return partItemMasterReportDTO;

        } 
        #endregion
    }

}
