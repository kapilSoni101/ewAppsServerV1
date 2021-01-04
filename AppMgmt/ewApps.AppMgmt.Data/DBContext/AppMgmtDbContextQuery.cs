/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 3 December 2018
 */



using ewApps.AppMgmt.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.AppMgmt.Data {

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class AppMgmtDbContext {

        #region DBQuery
        public DbQuery<TenantInfoDTO> TenantInfoDTOQuery {
            get; set;
        }

        public DbQuery<UserShortInfoDQ> UserShortInfoDQQuery {
            get; set;
        }

        public DbQuery<BrandingDTO> BrandingDTOQuery {
            get; set;
        }

        public DbQuery<BusinessBrandingDTO> BusinessBrandingDTOQuery {
            get; set;
        }

        public DbQuery<AppPortalBrandingDTO> ShipAppBrandingDTOQuery {
            get; set;
        }

        public DbQuery<ConfigurationDTO> PublisherConfigurationDTOQuery {
            get; set;
        }

        public DbQuery<AppServiceDTO> AppServiceDTOQuery {
            get; set;
        }

        public DbQuery<AppServiceAttributeDTO> AppServiceAttributeDTOQuery {
            get; set;
        }

        public DbQuery<UpdateTenantModelDQ> UpdateTenantModelDQQuery {
            get; set;
        }

        public DbQuery<TenantAppSubscriptionDQ> TenantAppSubscriptionDQQuery {
            get; set;
        }

        /// <summary>
        /// It is use to retrieve <see cref="SubscriptionPlanInfoDTO"/> data using query/view.
        /// </summary>
        public DbQuery<SubscriptionPlanInfoDTO> SubscriptionPlanInfoDTOQuery {
            get; set;
        }

        /// <summary>
        /// It is use to retrieve <see cref="TenantUserProfileDTO"/> data using query/view.
        /// </summary>
        public DbQuery<TenantUserProfileDTO> TenantUserProfileDTOQuery {
            get; set;
        }

        public DbQuery<SubsPlanServiceInfoDTO> SubsPlanServiceInfoDTOQuery {
            get; set;
        }

        public DbQuery<SubsPlanServiceAttributeInfoDTO> SubsPlanServiceAttributeInfoDTOQuery {
            get; set;
        }

        #endregion
    }
}