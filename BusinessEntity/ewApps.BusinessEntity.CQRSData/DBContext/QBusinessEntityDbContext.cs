/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 * 
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 23 November 2018
 */

using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.Core.NotificationService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.BusinessEntity.QData {

    // Hari Sir Review

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class QBusinessEntityDbContext:DbContext {

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
        public QBusinessEntityDbContext(DbContextOptions<QBusinessEntityDbContext> options, string connString) : base(options) {
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
        public QBusinessEntityDbContext(DbContextOptions<QBusinessEntityDbContext> options, IOptions<BusinessEntityAppSettings> appSetting) : base(options) {
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

        public DbQuery<BusBAARInvoiceAttachmentDTO> BusBAARInvoiceAttachmentDTOQuery {
            get;set;
        }

        public DbQuery<ARInvoiceNotificationDTO> ARInvoiceNotificationDTOQuery {
            get; set;
        }

        public DbQuery<ASNNotificationDTO> ASNNotificationDTOQuery {
            get; set;
        }

        public DbQuery<ContractNotificationDTO> ContractNotificationDTOQuery {
            get; set;
        }

        public DbQuery<DeliveryNotificationDTO> DeliveryNotificationDTOQuery {
            get; set;
        }

        


        public DbQuery<CustomerNotificationDTO> CustomerNotificationDTOQuery {
            get; set;
        }

        public DbQuery<NotificationRecipient> NotificationRecipientQuery {
            get; set;
        }


        public DbQuery<NotificationCommonDetailDTO> NotificationCommonDetailDTOQuery {
            get; set;
        }

        public DbQuery<AppInfoDTO> AppInfoDTOQuery {
            get; set;
        }

        public DbQuery<SONotificationDTO> SONotificationDTOQuery {
            get; set;
        }

        public DbQuery<BusBAItemMasterViewDTO> BusBAItemMasterViewDTOQuery {
            get; set;
        }
        public DbQuery<CustBAItemMasterViewDTO> CustBAItemMasterViewDTOQuery {
            get; set;
        }

        #endregion DbQuery
    }
}