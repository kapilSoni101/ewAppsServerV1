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
    public class QPlatDashboardDS:BaseDS<BaseDTO>, IQPlatDashboardDS {

        #region Local Member
        IQPlatDashboardRepository _dashboardRepository;
       // IAppDS _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="dashboardRepository"></param>
        public QPlatDashboardDS(IQPlatDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            //_appDataService = appDataService;
            _sessionManager = sessionManager;
        }

        #endregion Constructors

        #region private methods

        #region Get
        
        #region Platform DataService
        ///<inheritdoc/>

        ///<inheritdoc/>
        public async Task<PlatDashboardAppBusinessAndPublisherCount> GetAllPlatformDashboardDataForApplicationListAsync(CancellationToken token = default(CancellationToken)) {
            PlatDashboardAppBusinessAndPublisherCount platDashboardAppBusinessAndPublisherCount = new PlatDashboardAppBusinessAndPublisherCount();
            List<AppDTO> app = await GetAllPlatAppListAsync(token);
            platDashboardAppBusinessAndPublisherCount.PlatAppAndBusinessCountDTO = await GetAllPlatApplicationAndBusinessCountAsync(token);
            platDashboardAppBusinessAndPublisherCount.ApplicationUserCountDTO = await GetAllPlatApplicationUserCountListAsync(token);
            platDashboardAppBusinessAndPublisherCount.BusinessCountDTO = await GetAllPlatBusinessCountListAsync(token);            
            platDashboardAppBusinessAndPublisherCount.ApplicationPublisherCountDTO = await GetAllApplicationPublisherCountListAsync(token);
            platDashboardAppBusinessAndPublisherCount.PublisherTenantCountDTO = await GetAllPublisherTenantCountListAsync(token);
            platDashboardAppBusinessAndPublisherCount.ShipmentServiceNameAndCountDTO = await GetAllServiceNameForShipmentsCountListForPlatformAsync(token);
           
            foreach(AppDTO ap in app) {
                if(ap.AppKey.Equals(AppKeyEnum.pay.ToString(),StringComparison.CurrentCultureIgnoreCase)) {
                    platDashboardAppBusinessAndPublisherCount.BusinessAddedCountAndMonthDTO = await GetAllPlatBusinessCountPerMonthListAsync(ap.ID, token);
                    platDashboardAppBusinessAndPublisherCount.BusinessNameAndSumCount = await GetAllPlatBusinessNameWithHeightestAmountListAsync(ap.ID, token);
                    platDashboardAppBusinessAndPublisherCount.AapNameAndBusinessCountDTO = await GetAllPlatAppNameAndBusinessCountAsync(ap.ID, token);
                }
                if(ap.AppKey.Equals(AppKeyEnum.ship.ToString(), StringComparison.CurrentCultureIgnoreCase)) {
                    platDashboardAppBusinessAndPublisherCount.ShipmentBusinessAddedCountAndMonthDTO = await GetAllPlatShipBusinessCountPerMonthListAsync(ap.ID, token);
                    platDashboardAppBusinessAndPublisherCount.ShipmentBusinessNameAndSumCount = await GetAllPlatShipBusinessNameWithMaximumShippedOrderListAsync(ap.ID, token);
                    platDashboardAppBusinessAndPublisherCount.ShipmentAapNameAndBusinessCountDTO = await GetAllPlatShipAppNameAndBusinessCountAsync(ap.ID, token);
                }
            }

            return platDashboardAppBusinessAndPublisherCount;
        }

        ///<inheritdoc/>
        private async Task<List<ApplicationPublisherCountDTO>> GetAllApplicationPublisherCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<ApplicationPublisherCountDTO> applicationPublisherCountDTO = new List<ApplicationPublisherCountDTO>();
            applicationPublisherCountDTO = await _dashboardRepository.GetAllApplicationPublisherCountListAsync(token);
            int total = 0;
            float NoofUser = 0;
            if(applicationPublisherCountDTO != null) {
                foreach(ApplicationPublisherCountDTO item in applicationPublisherCountDTO) {
                    total = item.NoOfPublisher + total;
                }
                foreach(ApplicationPublisherCountDTO item in applicationPublisherCountDTO) {
                    NoofUser = item.NoOfPublisher;
                    item.PercentageOfPublisher = (float)System.Math.Round(((NoofUser / total) * 100), 2);
                }
            }
            return applicationPublisherCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<PublisherTenantCountDTO>> GetAllPublisherTenantCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<PublisherTenantCountDTO> publisherTenantCountDTO = new List<PublisherTenantCountDTO>();
            publisherTenantCountDTO = await _dashboardRepository.GetAllPublisherTenantCountListAsync(token);
            int total = 0;
            float NoofUser = 0;
            if(publisherTenantCountDTO != null) {
                foreach(PublisherTenantCountDTO item in publisherTenantCountDTO) {
                    total = item.NoOfBusiness + total;
                }
                foreach(PublisherTenantCountDTO item in publisherTenantCountDTO) {
                    NoofUser = item.NoOfBusiness;
                    item.PercentageOfBusiness = (float)System.Math.Round(((NoofUser / total) * 100), 2);
                }
            }
            return publisherTenantCountDTO;
        }

        ///<inheritdoc/>
        private async Task<PlatAppAndBusinessCountDTO> GetAllPlatApplicationAndBusinessCountAsync(CancellationToken token = default(CancellationToken)) {
            PlatAppAndBusinessCountDTO appAndBusinessCountDTO = new PlatAppAndBusinessCountDTO();
            appAndBusinessCountDTO = await _dashboardRepository.GetAllPlatApplicationAndBusinessCountAsync(token);
            return appAndBusinessCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<ApplicationUserCountDTO>> GetAllPlatApplicationUserCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<ApplicationUserCountDTO> applicationUserCountDTO = new List<ApplicationUserCountDTO>();
            applicationUserCountDTO = await _dashboardRepository.GetAllPlatApplicationUserCountListAsync(token);
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
        private async Task<AapNameAndBusinessCountDTO> GetAllPlatAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            AapNameAndBusinessCountDTO aapNameAndBusinessCountDTO = new AapNameAndBusinessCountDTO();
            aapNameAndBusinessCountDTO = await _dashboardRepository.GetAllPlatAppNameAndBusinessCountAsync(appId, token);
            return aapNameAndBusinessCountDTO;
        }


        ///<inheritdoc/>
        private async Task<ShipmentAapNameAndBusinessCountDTO> GetAllPlatShipAppNameAndBusinessCountAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            ShipmentAapNameAndBusinessCountDTO aapNameAndBusinessCountDTO = new ShipmentAapNameAndBusinessCountDTO();
            aapNameAndBusinessCountDTO = await _dashboardRepository.GetAllPlatShipAppNameAndBusinessCountAsync(appId, token);
            return aapNameAndBusinessCountDTO;
        }

        ///<inheritdoc/>
        private async Task<List<BusinessCountDTO>> GetAllPlatBusinessCountListAsync(CancellationToken token = default(CancellationToken)) {
            List<BusinessCountDTO> businessCountDTO = new List<BusinessCountDTO>();
            businessCountDTO = await _dashboardRepository.GetAllPlatBusinessCountListAsync(token);
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
        private async Task<List<BusinessAddedCountAndMonthDTO>> GetAllPlatBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<BusinessAddedCountAndMonthDTO> businessAddedCountAndMonthDTO = new List<BusinessAddedCountAndMonthDTO>();
            businessAddedCountAndMonthDTO = await _dashboardRepository.GetAllPlatBusinessCountPerMonthListAsync(appId, token);
            return businessAddedCountAndMonthDTO;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentBusinessAddedCountAndMonthDTO>> GetAllPlatShipBusinessCountPerMonthListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<ShipmentBusinessAddedCountAndMonthDTO> businessAddedCountAndMonthDTO = new List<ShipmentBusinessAddedCountAndMonthDTO>();
            businessAddedCountAndMonthDTO = await _dashboardRepository.GetAllPlatShipBusinessCountPerMonthListAsync(appId, token);
            return businessAddedCountAndMonthDTO;
        }

        ///<inheritdoc/>
        private async Task<List<BusinessNameAndSumCount>> GetAllPlatBusinessNameWithHeightestAmountListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<BusinessNameAndSumCount> businessNameAndSumCount = new List<BusinessNameAndSumCount>();
            businessNameAndSumCount = await _dashboardRepository.GetAllPlatBusinessNameWithHeightestAmountListAsync(appId, token);
            return businessNameAndSumCount;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentBusinessNameAndSumCount>> GetAllPlatShipBusinessNameWithMaximumShippedOrderListAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            List<ShipmentBusinessNameAndSumCount> businessNameAndSumCount = new List<ShipmentBusinessNameAndSumCount>();
            businessNameAndSumCount = await _dashboardRepository.GetAllPlatShipBusinessNameWithMaximumShippedOrderListAsync(appId, token);
            return businessNameAndSumCount;
        }

        ///<inheritdoc/>
        private async Task<List<ShipmentServiceNameAndCountDTO>> GetAllServiceNameForShipmentsCountListForPlatformAsync(CancellationToken token = default(CancellationToken)) {
            List<ShipmentServiceNameAndCountDTO> shipmentserviceCountDTO = new List<ShipmentServiceNameAndCountDTO>();
            //UserSession us = _sessionManager.GetSession();
            shipmentserviceCountDTO = await _dashboardRepository.GetAllServiceNameForShipmentsCountListForPlatformAsync(token);
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

        ///<inheritdoc/>
        private async Task<List<AppDTO>> GetAllPlatAppListAsync(CancellationToken token = default(CancellationToken)) {
            List<AppDTO> AppDetailDTO = new List<AppDTO>();
            AppDetailDTO = await _dashboardRepository.GetAllPlatAppListAsync(token);
            return AppDetailDTO;
        }


        #endregion        

        #endregion

        #endregion
    }
}
