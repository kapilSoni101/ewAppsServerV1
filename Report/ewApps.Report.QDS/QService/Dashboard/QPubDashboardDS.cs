/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {
    /// <summary>
    /// This class Contain Business Login of Publisher Dashboard 
    /// </summary>
    public class QPubDashboardDS:BaseDS<BaseDTO>, IQPubDashboardDS {

        #region Local Member
        IQPubDashboardRepository _dashboardRepository;        
        private IUserSessionManager _sessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="dashboardRepository"></param>
        /// <param name="cacheService"></param>
        public QPubDashboardDS(IQPubDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;            
            _sessionManager = sessionManager;
        }

        #endregion Constructors

        #region private methods

        #region Get

        #region Publisher DataService

        ///<inheritdoc/>
        public async Task<PubDashboardAppBusinessAndSubcriptionCount> GetAllPublisherDashboardDataForBusinessAppAndSubscriptionListAsync(CancellationToken token = default(CancellationToken)) {
            List<AppDTO> app = await GetAllPubAppListAsync(token);
            PubDashboardAppBusinessAndSubcriptionCount pubDashboardAppBusinessAndSubcriptionCount = new PubDashboardAppBusinessAndSubcriptionCount();
            pubDashboardAppBusinessAndSubcriptionCount.AppAndBusinessCountDTO = await GetAllApplicationAndBusinessCountAsync(token);
            pubDashboardAppBusinessAndSubcriptionCount.ApplicationUserCountDTO = await GetAllApplicationUserCountListAsync(token);
            pubDashboardAppBusinessAndSubcriptionCount.BusinessCountDTO = await GetAllBusinessCountListAsync(token);
            pubDashboardAppBusinessAndSubcriptionCount.ShipmentServiceNameAndCountDTO = await GetAllServiceNameForShipmentsCountListForPublisherAsync(token);

            foreach(AppDTO ap in app) {
                if(ap.AppKey.Equals(AppKeyEnum.pay.ToString(), StringComparison.CurrentCultureIgnoreCase)) {                
                    pubDashboardAppBusinessAndSubcriptionCount.BusinessAndSubscriptionCountDTO = await GetAllBusinessAndSubscriptionCountAsync(ap.ID, token);
                    pubDashboardAppBusinessAndSubcriptionCount.BusinessAddedCountAndMonthDTO = await GetAllBusinessCountPerMonthListAsync(ap.ID, token);
                    pubDashboardAppBusinessAndSubcriptionCount.BusinessNameAndSumCount = await GetAllBusinessNameWithHeightestAmountListAsync(ap.ID, token);
                }
                if(ap.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    pubDashboardAppBusinessAndSubcriptionCount.ShipmentBusinessAndSubscriptionCountDTO = await GetAllShipBusinessAndSubscriptionCountAsync(ap.ID, token);
                    pubDashboardAppBusinessAndSubcriptionCount.ShipmentBusinessAddedCountAndMonthDTO = await GetAllShipBusinessCountPerMonthListAsync(ap.ID, token);
                    pubDashboardAppBusinessAndSubcriptionCount.ShipmentBusinessNameAndSumCount = await GetAllShipBusinessNameWithMaximumShippingOrdersListAsync(ap.ID, token);
                }
            }
            return pubDashboardAppBusinessAndSubcriptionCount;
        }

        ///<inheritdoc/>
        private async Task<AppAndBusinessCountDTO> GetAllApplicationAndBusinessCountAsync(CancellationToken token = default(CancellationToken)) {
            AppAndBusinessCountDTO appAndBusinessCountDTO = new AppAndBusinessCountDTO();
            UserSession us = _sessionManager.GetSession();
            appAndBusinessCountDTO = await _dashboardRepository.GetAllApplicationAndBusinessCountAsync(us.TenantId, token);
            return appAndBusinessCountDTO;
        }

        ///<inheritdoc/>
        private async Task<BusinessAndSubscriptionCountDTO> GetAllBusinessAndSubscriptionCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            BusinessAndSubscriptionCountDTO businessAndSubscriptionCountDTO = new BusinessAndSubscriptionCountDTO();
            UserSession us = _sessionManager.GetSession();
            businessAndSubscriptionCountDTO = await _dashboardRepository.GetAllBusinessAndSubscriptionCountAsync(appId, us.TenantId, token);
            return businessAndSubscriptionCountDTO;
        }

        ///<inheritdoc/>
        private async Task<ShipmentBusinessAndSubscriptionCountDTO> GetAllShipBusinessAndSubscriptionCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            ShipmentBusinessAndSubscriptionCountDTO businessAndSubscriptionCountDTO = new ShipmentBusinessAndSubscriptionCountDTO();
            UserSession us = _sessionManager.GetSession();
            businessAndSubscriptionCountDTO = await _dashboardRepository.GetAllShipBusinessAndSubscriptionCountAsync(appId, us.TenantId, token);
            return businessAndSubscriptionCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<ApplicationUserCountDTO>> GetAllApplicationUserCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<ApplicationUserCountDTO> applicationUserCountDTO = new List<ApplicationUserCountDTO>();
            UserSession us = _sessionManager.GetSession();
            applicationUserCountDTO = await _dashboardRepository.GetAllApplicationUserCountListAsync(us.TenantId, token);
            int total = 0;
            float NoofUser = 0;
            if(applicationUserCountDTO != null) {
                foreach(ApplicationUserCountDTO item in applicationUserCountDTO) {
                    total = item.NoofUser + total;
                }
                foreach(ApplicationUserCountDTO item in applicationUserCountDTO) {
                    NoofUser = item.NoofUser;
                    item.PercentageOfUser = (float)System.Math.Round(((NoofUser / total) * 100), 2);
                }
            }
            return applicationUserCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<BusinessCountDTO>> GetAllBusinessCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<BusinessCountDTO> businessCountDTO = new List<BusinessCountDTO>();
            UserSession us = _sessionManager.GetSession();
            businessCountDTO = await _dashboardRepository.GetAllBusinessCountListAsync(us.TenantId, token);
            int total = 0;
            float noofbusiness = 0;
            //businessCountDTO = await _dashboardRepository.GetAllBusinessCountListAsync();
            if(businessCountDTO != null) {
                foreach(BusinessCountDTO item in businessCountDTO) {
                    total = item.NoofBusiness + total;
                }
                foreach(BusinessCountDTO item in businessCountDTO) {
                    noofbusiness = item.NoofBusiness;
                    item.PercentageOfBusiness = (float)System.Math.Round(((noofbusiness / total) * 100), 2);
                }
            }
            return businessCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<BusinessAddedCountAndMonthDTO>> GetAllBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<BusinessAddedCountAndMonthDTO> businessAddedCountAndMonthDTO = new List<BusinessAddedCountAndMonthDTO>();
            UserSession us = _sessionManager.GetSession();
            businessAddedCountAndMonthDTO = await _dashboardRepository.GetAllBusinessCountPerMonthListAsync(appId, us.TenantId, token);
            return businessAddedCountAndMonthDTO;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllShipBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<ShipmentBusinessAddedCountAndMonthDTO> businessAddedCountAndMonthDTO = new List<ShipmentBusinessAddedCountAndMonthDTO>();
            UserSession us = _sessionManager.GetSession();
            businessAddedCountAndMonthDTO = await _dashboardRepository.GetAllShipBusinessCountPerMonthListAsync(appId, us.TenantId, token);
            return businessAddedCountAndMonthDTO;
        }

        ///<inheritdoc/>
        private async Task<List<BusinessNameAndSumCount>> GetAllBusinessNameWithHeightestAmountListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<BusinessNameAndSumCount> businessNameAndSumCount = new List<BusinessNameAndSumCount>();
            UserSession us = _sessionManager.GetSession();
            businessNameAndSumCount = await _dashboardRepository.GetAllBusinessNameWithHeightestAmountListAsync(appId, us.TenantId, token);
            return businessNameAndSumCount;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentBusinessNameAndSumCount>> GetAllShipBusinessNameWithMaximumShippingOrdersListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<ShipmentBusinessNameAndSumCount> businessNameAndSumCount = new List<ShipmentBusinessNameAndSumCount>();
            UserSession us = _sessionManager.GetSession();
            businessNameAndSumCount = await _dashboardRepository.GetAllShipBusinessNameWithMaximumShippingOrdersListAsync(appId, us.TenantId, token);
            return businessNameAndSumCount;
        }        

        ///<inheritdoc/>
        private async Task<List<AppDTO>> GetAllPubAppListAsync(CancellationToken token = default(CancellationToken)) {
            List<AppDTO> AppDetailDTO = new List<AppDTO>();
            AppDetailDTO = await _dashboardRepository.GetAllPubAppListAsync(token);
            return AppDetailDTO;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPublisherAsync(CancellationToken token = default(CancellationToken)) {
            List<ShipmentServiceNameAndCountDTO> shipmentserviceCountDTO = new List<ShipmentServiceNameAndCountDTO>();
            //UserSession us = _sessionManager.GetSession();
            shipmentserviceCountDTO = await _dashboardRepository.GetAllServiceNameForShipmentsCountListForPublisherAsync(token);
            int total = 0;
            float TotalNoofShipment = 0;
            if(shipmentserviceCountDTO != null) {
                foreach(ShipmentServiceNameAndCountDTO item in shipmentserviceCountDTO) {
                    total = item.NoOfShipment + total;
                }
                foreach(ShipmentServiceNameAndCountDTO item in shipmentserviceCountDTO) {
                    TotalNoofShipment = item.NoOfShipment;
                    item.PercentageOfShipment = (float)System.Math.Round(((TotalNoofShipment / total) * 100), 2);
                }
            }
            return shipmentserviceCountDTO;
        }
        #endregion      

        #endregion

        #endregion
    }
}
