/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {
    /// <summary>
    /// This class Contain Business Logic of Customer Portal Customer APp Dashboard 
    /// </summary>
    public class QCustPortCustDashboardDS:BaseDS<BaseDTO>, IQCustPortCustDashboardDS {

        #region Local Varialbe 
        IQCustPortCustDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        #region Constructor
        public QCustPortCustDashboardDS(IQCustPortCustDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }
        #endregion        

        ///<inheritdoc/>
        public async Task<List<RecentPurchaseQuotationsFCDTO>> GetAllRecentPurchaseQuotationsByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {            
            return await _dashboardRepository.GetAllRecentPurchaseQuotationsByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<RecentSalesOrdersFCDTO>> GetAllRecentSalesOrdersByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {            
            return await _dashboardRepository.GetAllRecentSalesOrdersByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<RecentDeliveriesFCDTO>> GetAllRecentDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {           
            return await _dashboardRepository.GetAllRecentDeliveriesByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<UpcomingDeliveriesFCDTO>> GetAllUpcomingDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {            
            return await _dashboardRepository.GetAllUpcomingDeliveriesByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<DeliveriesStatusCountFCDTO> GetDeliveriesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {           
            return await _dashboardRepository.GetDeliveriesStatusCountForDashBoardByCustomerAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<PurchaseAndMonthNameFCDTO>> GetMonthNameAndSumOfPurchaseByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardRepository.GetMonthNameAndSumOfPurchaseByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<SalesOrdersItemsCountFCDTO>> GetItemNameAndSumOfItemsQuantityByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            List<SalesOrdersItemsCountFCDTO> salesOrdersItemsCountFCDTO = new List<SalesOrdersItemsCountFCDTO>();
            salesOrdersItemsCountFCDTO =  await _dashboardRepository.GetItemNameAndSumOfItemsQuantityByCustomerListAsync(customerId, token);
            int total = 0;
            float TotalNoofShipment = 0;
            if(salesOrdersItemsCountFCDTO != null) {
                foreach(SalesOrdersItemsCountFCDTO item in salesOrdersItemsCountFCDTO) {
                    total = item.NoOfItems + total;
                }
                foreach(SalesOrdersItemsCountFCDTO item in salesOrdersItemsCountFCDTO) {
                    TotalNoofShipment = item.NoOfItems;
                    item.PercentageOfItem = (float)System.Math.Round(((TotalNoofShipment / total) * 100), 2);
                }
            }
            return salesOrdersItemsCountFCDTO;            
        }
    }
}
