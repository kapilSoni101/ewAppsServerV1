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
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Entity;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.Data {

    // Hari Sir Review

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class AppPortalDbContext:DbContext {

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
        public AppPortalDbContext(DbContextOptions<AppPortalDbContext> options, string connString) : base(options) {
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
        public AppPortalDbContext(DbContextOptions<AppPortalDbContext> options, IOptions<AppPortalAppSettings> appSetting, IConnectionManager connectionManager) : base(options) {
            _connString = appSetting.Value.ConnectionString;
            _connectionManager = connectionManager;
        }

        #endregion Constructor

        #region DbContext Override Methods

        /// <inheritdoc />  
        protected override void OnModelCreating(ModelBuilder builder) {

            #region Default Value Section

            #endregion
            //MasterData.Init(builder);
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

        #region Support
        /// <summary>
        /// DbSet&lt;Support&gt; can be used to query and save instances of Support entity. 
        /// Linq queries can written using DbSet&lt;Support&gt; that will be translated to sql query and executed against database Support table. 
        /// </summary>
        public DbSet<Support> Support {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;SupportGroup&gt; can be used to query and save instances of SupportGroup entity. 
        /// Linq queries can written using DbSet&lt;SupportGroup&gt; that will be translated to sql query and executed against database SupportGroup table. 
        /// </summary>
        public DbSet<SupportGroup> SupportGroup {
            get; set;
        }


        /// <summary>
        /// DbSet&lt;SupportTicket&gt; can be used to query and save instances of SupportTicket entity. 
        /// Linq queries can written using DbSet&lt;SupportTicket&gt; that will be translated to sql query and executed against database SupportTicket table. 
        /// </summary>
        public DbSet<SupportTicket> SupportTicket {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;SupportTicket&gt; can be used to query and save instances of SupportTicket entity. 
        /// Linq queries can written using DbSet&lt;SupportTicket&gt; that will be translated to sql query and executed against database SupportTicket table. 
        /// </summary>
        public DbSet<SupportComment> SupportComment {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;LevelTransitionHistory&gt; can be used to query and save instances of LevelTransitionHistory entity. 
        /// Linq queries can written using DbSet&lt;LevelTransitionHistory&gt; that will be translated to sql query and executed against database LevelTransitionHistory table. 
        /// </summary>
        public DbSet<LevelTransitionHistory> LevelTransitionHistory {
            get; set;
        }
        #endregion

        public DbSet<Business> Business {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;BusinessAddress&gt; can be used to query and save instances of BusinessAddress entity. 
        /// Linq queries can written using DbSet&lt;BusinessAddress&gt; that will be translated to sql query and executed against database BusinessAddress table.
        /// </summary>
        public DbSet<BusinessAddress> BusinessAddress {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Customer&gt; can be used to query and save instances of Customer entity. 
        /// Linq queries can written using DbSet&lt;Customer&gt; that will be translated to sql query and executed against database Customer table.
        /// </summary>
        public DbSet<Customer> Customer {
            get;
            set;
        }

    /// <summary>
    /// DbSet&lt;Vendor&gt; can be used to query and save instances of Vendor entity. 
    /// Linq queries can written using DbSet&lt;Vendor&gt; that will be translated to sql query and executed against database Vendor table.
    /// </summary>
    public DbSet<Vendor> Vendor
    {
      get;
      set;
    }

    public DbSet<Platform> Platform {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Portal&gt; can be used to query and save instances of Portal entity. 
        /// Linq queries can written using DbSet&lt;Portal&gt; that will be translated to sql query and executed against database Portal table.
        /// </summary>
        public DbSet<Portal> Portal {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;PortalAppLinking&gt; can be used to query and save instances of PortalAppLinking entity. 
        /// Linq queries can written using DbSet&lt;PortalAppLinking&gt; that will be translated to sql query and executed against database PortalAppLinking table.
        /// </summary>
        public DbSet<PortalAppLinking> PortalAppLinking {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Publisher&gt; can be used to query and save instances of PortalAppLinking entity. 
        /// Linq queries can written using DbSet&lt;Publisher&gt; that will be translated to sql query and executed against database Publisher table.
        /// </summary>
        public DbSet<Entity.Publisher> Publisher {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;PublisherAddress&gt; can be used to query and save instances of PublisherAddress entity. 
        /// Linq queries can written using DbSet&lt;PublisherAddress&gt; that will be translated to sql query and executed against database PublisherAddress table.
        /// </summary>
        public DbSet<PublisherAddress> PublisherAddress {
            get;
            set;
        }

        public DbSet<PublisherAppSetting> PublisherAppSetting {
            get;
            set;
        }

        public DbSet<TenantUserAppPreference> TenantUserAppPreference {
            get;
            set;
        }

        public DbSet<TokenInfo> TokenInfo {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Role&gt; can be used to query and save instances of Role entity. 
        /// Linq queries can written using DbSet&lt;role &gt; that will be translated to sql query and executed against database role table.
        /// </summary>
        public DbSet<Role> Role {
            get; set;
        }

        /// DbSet&lt;role linking&gt; can be used to query and save instances of role linking entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database role linking table. 
        public DbSet<RoleLinking> RoleLinking {
            get; set;
        }

        /// DbSet&lt;role linking&gt; can be used to query and save instances of PublisherAppService entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database PublisherAppService table.    
        public DbSet<PubBusinessSubsPlanAppService> PubBusinessSubsPlanAppService {
            get; set;
        }

        /// DbSet&lt;PubBusinessSubsPlan&gt; can be used to query and save instances of PubBusinessSubsPlan entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database PubBusinessSubsPlan table.    
        public DbSet<PubBusinessSubsPlan> PubBusinessSubsPlan {
            get; set;
        }

        public DbSet<TenantAppLinking> TenantAppLinking {
            get; set;
        }

        /// DbSet&lt;CustomerAccountDetail&gt; can be used to query and save instances of CustomerAccountDetail entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database CustomerAccountDetail table.    
        public DbSet<CustomerAccountDetail> CustomerAccountDetail {
            get; set;
        }

        public DbSet<Notes> Notes {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Favorite&gt; can be used to query and save instances of Favorite entity. 
        /// Linq queries can written using DbSet&lt;Favorite&gt; that will be translated to sql query and executed against database Favorite table. 
        /// </summary>
        public DbSet<Favorite> Favorite {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;ASNotification&gt; can be used to query and save instances of Favorite entity. 
        /// Linq queries can written using DbSet&lt;ASNotification&gt; that will be translated to sql query and executed against database ASNotification table. 
        /// </summary>
        public DbSet<ASNotification> ASNotification {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;ViewSettings&gt; can be used to query and save instances of ViewSettings entity. 
        /// Linq queries can written using DbSet&lt;ViewSettings&gt; that will be translated to sql query and executed against database ViewSettings table. 
        /// </summary>
        public DbSet<ViewSettings> ViewSettings {
            get; set;
        }

        

        #endregion DbSets
    }
}