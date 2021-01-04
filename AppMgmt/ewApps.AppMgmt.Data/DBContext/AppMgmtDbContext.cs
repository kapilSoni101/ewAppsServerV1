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

using System;
using System.Data.Common;
using System.Data.SqlClient;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.AppMgmt.Data {

    // Hari Sir Review

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class AppMgmtDbContext:DbContext {

        #region Local Members

        protected string _connString;
        protected IConnectionManager _connectionManager;

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
        public AppMgmtDbContext(DbContextOptions<AppMgmtDbContext> options, string connString) : base(options) {
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
        public AppMgmtDbContext(DbContextOptions<AppMgmtDbContext> options, IOptions<AppMgmtAppSettings> appSetting, IConnectionManager connectionManager) : base(options) {
            _connString = appSetting.Value.ConnectionString;
            _connectionManager = connectionManager;
        }

        #endregion Constructor

        #region DbContext Override Methods

        /// <inheritdoc />  
        protected override void OnModelCreating(ModelBuilder builder) {

            #region Default Value Section
            builder.Entity<Tenant>()
                  .Property(b => b.Active)
                  .HasDefaultValue(true);

            builder.Entity<TenantSubscription>()
                             .Property(b => b.AutoRenewal)
                             .HasDefaultValue(false);

            builder.Entity<TenantSubscription>()
                                   .Property(b => b.AutoRenewal)
                                   .HasDefaultValue(false);

            builder.Entity<TenantSubscription>()
                                         .Property(b => b.CustomizeSubscription)
                                         .HasDefaultValue(false);


            builder.Entity<TenantSubscription>()
                                   .Property(b => b.Status)
                                   .HasDefaultValue(1);

            #endregion
            MasterData.Init(builder);
            base.OnModelCreating(builder);
        }


        /// <inheritdoc />  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // For Db Migration 
            if(_connectionManager == null) {
                optionsBuilder.UseSqlServer(_connString);
            }
            else {
                SqlConnection conn = _connectionManager.GetConnection(_connString);
                optionsBuilder.UseSqlServer(conn);
            }

            base.OnConfiguring(optionsBuilder);
        }

        #endregion DbContext Override Methods

        #region DbSets

        public DbSet<App> App {
            get;
            set;
        }

        public DbSet<SubscriptionPlan> SubscriptionPlan {
            get;
            set;
        }

        public DbSet<Tenant> Tenant {
            get;
            set;
        }

        public DbSet<TenantAppServiceLinking> TenantAppServiceLinking {
            get;
            set;
        }

        public DbSet<TenantLinking> TenantLinking {
            get;
            set;
        }

        public DbSet<TenantSubscription> TenantSubscription {
            get;
            set;
        }

        public DbSet<TenantUser> TenantUser {
            get;
            set;
        }

        public DbSet<TenantUserAppLastAccessInfo> TenantUserAppLastAccessInfo {
            get;
            set;
        }

        public DbSet<TenantUserAppLinking> TenantUserAppLinking {
            get;
            set;
        }

        public DbSet<Theme> Theme {
            get;
            set;
        }

        public DbSet<UserTenantLinking> UserTenantLinking {
            get;
            set;
        }
        /// <summary>
        /// DbSet&lt;AppUserTypeLinking&gt; can be used to query and save instances of AppUserTypeLinking entity. 
        /// Linq queries can written using DbSet&lt; AppUserTypeLinking&gt; that will be translated to sql query and executed against database AppUserTypeLinking table.
        /// </summary>
        public DbSet<AppUserTypeLinking> AppUserTypeLinking {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;AppService&gt; can be used to query and save instances of AppService entity. 
        /// Linq queries can written using DbSet&lt;AppService&gt; that will be translated to sql query and executed against database AppService table.
        /// </summary>
        public DbSet<AppService> AppService {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;AppServiceAttribute&gt; can be used to query and save instances of AppServiceAttribute entity. 
        /// Linq queries can written using DbSet&lt;AppServiceAttribute&gt; that will be translated to sql query and executed against database AppServiceAttribute table.
        /// </summary>
        public DbSet<AppServiceAttribute> AppServiceAttribute {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;AppServiceAccountDetail&gt; can be used to query and save instances of AppServiceAccountDetail entity. 
        /// Linq queries can written using DbSet&lt;AppServiceAccountDetail&gt; that will be translated to sql query and executed against database AppServiceAccountDetail table. 
        /// </summary>
        public DbSet<AppServiceAccountDetail> AppServiceAccountDetail {
            get; set;
        }
        /// <summary>
        /// DbSet&lt;CustomerAppServiceLinking&gt; can be used to query and save instances of CustomerAppServiceLinking entity. 
        /// Linq queries can written using DbSet&lt;CustomerAppServiceLinking&gt; that will be translated to sql query and executed against database CustomerAppServiceLinking table. 
        /// </summary>
        public DbSet<CustomerAppServiceLinking> CustomerAppServiceLinking {
            get; set;
        }

        public DbSet<SubscriptionPlanService> SubscriptionPlanService {
          get; set;
        }

        public DbSet<SubscriptionPlanServiceAttribute> SubscriptionPlanServiceAttribute {
          get; set;
        }

    #endregion DbSets





  }
}