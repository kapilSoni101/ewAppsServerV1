/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018C:\ewAppsServices\ewAppsServerV1\AppPortal\ewApps.AppPortal.QData\DBContext\QAppPortalDbContext.cs
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 23 November 2018
 */

using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.QData {

    // Hari Sir Review

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public class QAppPortalDbContext:DbContext {

        #region Local Members

        protected string _connString;

        #endregion Local Members

        #region Constructor

        /// <summary>
        /// Default constructor to initialize member variables (if any).
        /// </summary>
        /// <param name="options">The DbContextOptions instance carries configuration information such as: 
        /// (a) The database provider to use, UseSqlServer or UseSqlite
        /// (b) Connection string or identifier of the database instance    
        /// </param>
        /// <param name="connString">The Core database connection string.</param>
        public QAppPortalDbContext(DbContextOptions<QAppPortalDbContext> options, string connString) : base(options) {
            _connString = connString;
        }


        /// <summary>
        /// Constructor to initialize member variables (if any).
        /// </summary>
        /// <param name="options">The DbContextOptions instance carries configuration information such as: 
        /// (a) The database provider to use, UseSqlServer or UseSqlite
        /// (b) Connection string or identifier of the database instance    
        /// </param>
        /// <param name="appSetting">Instance of Appsettings object that contains core database 
        /// connection string.
        /// </param>
        public QAppPortalDbContext(DbContextOptions<QAppPortalDbContext> options, IOptions<AppPortalAppSettings> appSetting) : base(options) {
            _connString = appSetting.Value.QConnectionString;
        }

        #endregion Constructor

        #region DbContext Override Methods

        /// <inheritdoc />  
        protected override void OnModelCreating(ModelBuilder builder) {

            #region Default Value Section

            #endregion

            base.OnModelCreating(builder);
        }


        /// <inheritdoc />  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // For Db Migration 
            optionsBuilder.UseSqlServer(_connString);
            base.OnConfiguring(optionsBuilder);
        }

        #endregion DbContext Override Methods

        #region DbQuery

        public DbQuery<PlatfromUserSessionDTO> PlatfromUserSessionDTOQuery {
            get; set;
        }

        public DbQuery<PublisherUserSessionDTO> PublisherUserSessionDTOQuery {
            get; set;
        }

        public DbQuery<BusinessUserSessionDTO> BusinessUserSessionDTOQuery {
            get; set;
        }

        public DbQuery<CustomerUserSessionDTO> CustomerUserSessionDTOQuery {
            get; set;
        }

        public DbQuery<VendorUserSessionDTO> VendorUserSessionDTOQuery {
            get; set;
        }

        public DbQuery<UserSessionAppDTO> UserSessionAppDTOQuery {
            get; set;
        }

        public DbQuery<NotificationRecipient> NotificationRecipientQuery {
            get; set;
        }

        public DbQuery<TenantUserDetailsDTO> TenantUserDetailsDTOQuery {
            get; set;
        }

        public DbQuery<RoleKeyCountDTO> RoleKeyCountDTOQuery {
            get; set;
        }

        public DbQuery<CustConfigurationViewDTO> CustConfigurationViewDTOQuery {
            get; set;
        }

        public DbQuery<CustomerAccountDTO> CustomerAccountDTOQuery {
            get; set;
        }

        public DbQuery<CustCustomerAddressDTO> CustCustomerAddressDTOQuery {
            get; set;
        }

        public DbQuery<CustCustomerContactDTO> CustCustomerContactDTOQuery {
            get; set;
        }
        public DbQuery<BankAcctDetailDTO> BankAcctDetailDTOQuery {
            get; set;
        }
        public DbQuery<CreditCardDetailDTO> CreditCardDetailDTOQuery {
            get; set;
        }

        public DbQuery<UserEmailDTO> UserEmailDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatformBrandingDQ view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatformBrandingDQ.
        /// </remarks>
        public DbQuery<PlatformBrandingDQ> platformBrandingDQQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PublisherBrandingDQ view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PublisherBrandingDQ.
        /// </remarks>
        public DbQuery<PublisherBrandingDQ> publisherBrandingDQQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusinessBrandingDQ view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusinessBrandingDQ.
        /// </remarks>
        public DbQuery<BusinessBrandingDQ> businessBrandingDQQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get AppPortalBrandingDQ view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppPortalBrandingDQ.
        /// </remarks>
        public DbQuery<AppPortalBrandingDQ> appPortalBrandingDQQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get ConfigurationDQ view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of ConfigurationDQ.
        /// </remarks>
        public DbQuery<ConfigurationDTO> configurationDQQuery {
            get; set;
        }

        public DbQuery<AppDetailDQ> AppDetailDQQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get AppDetailDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppDetailDTO.
        /// </remarks>
        public DbQuery<AppDetailDTO> AppDetailDTOQuery {
            get; set;
        }

        /// <summary>
        /// TenantAppServiceDQ
        /// </summary>
        public DbQuery<TenantAppServiceDQ> TenantAppServiceDQQuery {
            get; set;
        }

        /// <summary>
        /// AppDQQuery
        /// </summary>
        public DbQuery<AppDQ> AppDQQuery {
            get; set;
        }

        /// <summary>
        /// TenantApplicationSubscriptionDQ
        /// </summary>
        public DbQuery<TenantApplicationSubscriptionDQ> TenantApplicationSubscriptionDQQuery {
            get; set;
        }

        public DbQuery<BusinessViewModelDQ> BusinessViewModelDQDQQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PlatBusinessDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PlatBusinessDTO.
        /// </remarks>
        public DbQuery<PlatBusinessDTO> PlatBusinessDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get SubscriptionPlanInfoDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SubscriptionPlanInfoDTO.
        /// </remarks>
        public DbQuery<SubscriptionPlanInfoDTO> SubscriptionPlanInfoDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get SubsPlanServiceInfoDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SubsPlanServiceInfoDTO.
        /// </remarks>
        public DbQuery<SubsPlanServiceInfoDTO> SubsPlanServiceInfoDTOQuery {
            get; set;
        }

        public DbQuery<ServiceInfoDTO> ServiceInfoDTOQuery {
            get; set;
        }
        /// <summary>
        /// This is use to get SubsPlanServiceAttributeInfoDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of SubsPlanServiceAttributeInfoDTO.
        /// </remarks>
        public DbQuery<SubsPlanServiceAttributeInfoDTO> SubsPlanServiceAttributeInfoDTOQuery {
            get; set;
        }


        /// <summary>
        /// This is use to get PublisherListDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PublisherListDTOQuery.
        /// </remarks>
        public DbQuery<PublisherDetailsDTO> PublisherListDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PubBusinessAppSubscriptionInfoDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PubBusinessAppSubscriptionInfoDTO.
        /// </remarks>
        public DbQuery<PubBusinessAppSubscriptionInfoDTO> PubBusinessAppSubscriptionInfoDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get BusConfigurationDTOQuery view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of BusConfigurationDTOQuery.
        /// </remarks>
        public DbQuery<BusConfigurationDTO> BusConfigurationDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PubAppSettingDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PubAppSettingDTO.
        /// </remarks>
        public DbQuery<PubAppSettingDTO> PubAppSettingDTOQuery {
            get; set;
        }




        /// <summary>
        /// This is use to get AppDetailInfoDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppDetailInfoDTO.
        /// </remarks>
        public DbQuery<AppDetailInfoDTO> AppDetailInfoDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get AppServiceDetailDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppServiceDetailDTO.
        /// </remarks>
        public DbQuery<AppServiceDetailDTO> AppServiceDetailDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get AppServiceAttributeDetailDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of AppServiceAttributeDetailDTO.
        /// </remarks>
        public DbQuery<AppServiceAttributeDetailDTO> AppServiceAttributeDetailDTOQuery {
            get; set;
        }

        /// <summary>
        /// This is use to get PublisherViewDTO view data. Any linq queries against Microsoft.EntityFrameworkCore.DbQuery`1 
        /// is translated into database query.
        /// </summary>
        /// <remarks>
        /// All database queries to get Microsoft.EntityFrameworkCore.DbQuery`1 should contains all the columns corresponding 
        /// to properties of PublisherViewDTO.
        /// </remarks>
        public DbQuery<PublisherViewDTO> PublisherViewDTOQuery {
            get; set;
        }


        public DbQuery<AppInfoDTO> AppInfoDTOQuery {
            get; set;
        }

        public DbQuery<CustomerPaymentInfoDTO> CustomerPaymentInfoDTOQuery {
            get; set;
        }

        /// <summary>
        /// Business subscribed service.
        /// </summary>
        public DbQuery<BusAppServiceDTO> BusAppServiceDTOQuery {
            get; set;
        }

        /// <summary>
        /// Business subscribed service.
        /// </summary>
        public DbQuery<BusAppServiceAttributeDTO> BusAppServiceAttributeDTOQuery {
            get; set;
        }

        public DbQuery<BusCustomerUserDTO> BusCustomerUserDTOQuery {
            get; set;
        }

        public DbQuery<BusCustomerApplicationDTO> BusCustomerApplicationDTOQuery {
            get; set;
        }
        public DbQuery<BusVendorUserDTO> BusVendorUserDTOQuery {
            get; set;
        }

        public DbQuery<BusVendorApplicationDTO> BusVendorApplicationDTOQuery {
            get; set;
        }

        /// <summary>
        /// Business subscribed service.
        /// </summary>
        public DbQuery<StringDTO> StringDTOQuery {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DbQuery<TenantUserAppPermissionDTO> TenantUserAppPermissionDTOQuery {
            get; set;
        }


        public DbQuery<TenantUserAndAppViewDTO> TenantUserAndAppViewDTOQuery {
            get; set;
        }

        public DbQuery<TenantUserAndPermissionViewDTO> TenantUserAndPermissionViewDTOQuery {
            get; set;
        }

        public DbQuery<AppServiceAcctDetailDTO> AppServiceAcctDetailDTOQuery {
            get; set;
        }

        public DbQuery<BusCustomerApplicationCountDTO> BusCustomerApplicationCountDTOQuery {
            get; set;
        }
        public DbQuery<BusVendorApplicationCountDTO> BusVendorApplicationCountDTOQuery {
            get; set;
        }

        public DbQuery<NotesViewDTO> NotesViewDTOQuery {
            get; set;
        }

        public DbQuery<TenantUserSetupListDTO> TenantUserSetupListDTOQuery {
            get; set;
        }

        public DbQuery<AppShortInfoDTO> AppShortInfoDTOQuery {
            get; set;
        }

        public DbQuery<VendorAddressDTO> VendorAddressDTOQuery {
            get; set;
        }

        public DbQuery<VendorConfigurationDTO> VendorConfigurationDTOQuery {
            get; set;
        }

        public DbQuery<VendorContactDTO> VendorContactDTOQuery {
            get; set;
        }

        #region Notification Predata

        public DbQuery<BusinessOnBoardNotificationDTO> BusinessOnBoardNotificationDTOQuery {
            get; set;
        }

        public DbQuery<CustomerOnBoardNotificationDTO> CustomerOnBoardNotificationDTOQuery {
            get; set;
        }

        public DbQuery<BusinessUserNotificationGeneralDTO> BusinessUserNotificationGeneralDTOQuery {
            get; set;
        }
        public DbQuery<BusinessUserNotificationAppAccessUpdateDTO> BusinessUserNotificationAppAccessUpdateDTOQuery {
            get; set;
        }

        public DbQuery<BusinessUserPermissionNotificationGeneralDTO> BusinessUserPermissionNotificationGeneralDTOQuery {
            get; set;
        }
        public DbQuery<CustomerUserNotificationGeneralDTO> CustomerUserNotificationGeneralDTOQuery {
            get; set;
        }

        public DbQuery<CustomerUserPermissionChangeNotificationGeneralDTO> CustomerUserPermissionChangeNotificationGeneralDTOQuery {
            get; set;
        }

        public DbQuery<BusinessNotesNotificationDTO> BusinessNotesNotificationDTOQuery {
            get; set;
        }

        public DbQuery<PublisherPermissionNotificationDTO> PublisherPermissionNotificationDTOQuery {
            get; set;
        }

        public DbQuery<VendorOnBoardNotificationDTO> VendorOnBoardNotificationDTOQuery {
            get; set;
        }

        #endregion

        #endregion DbQuery

    }
}