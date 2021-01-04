/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System.Data.Common;
using System.Data.SqlClient;
using ewApps.Core.DbConProvider;
using ewApps.Core.NotificationService;
using ewApps.Payment.Common;
using ewApps.Payment.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.Data {

    public partial class PaymentDbContext:DbContext {


        #region Local Members

        protected string _connString;
        protected IConnectionManager _connectionManager;
        //protected DbConnection _connection;

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
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options, string connString) : base(options) {
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
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options, IOptions<PaymentAppSettings> appSetting, IConnectionManager connectionManager) : base(options) {
            _connString = appSetting.Value.ConnectionString;
            _connectionManager = connectionManager;
        }

        #endregion Constructor

        #region DbContext Override Methods

        protected override void OnModelCreating(ModelBuilder builder) {
            MasterData.Init(builder);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(_connectionManager == null) {

                optionsBuilder.UseSqlServer(_connString);
            }
            else {
                SqlConnection con = _connectionManager.GetConnection(_connString);
                optionsBuilder.UseSqlServer(con);
            }

            base.OnConfiguring(optionsBuilder);
        }

        #endregion DbContext Override Methods

        #region DbSets

        public DbSet<Entity.Payment> Payment {
            get; set;
        }

        public DbSet<PreAuthPayment> PreAuthPayment {
            get; set;
        }

        public DbSet<PaymentLog> PaymentLog {
            get; set;
        }


        public DbSet<PaymentInvoiceLinking> PaymentInvoiceLinking {
            get; set;
        }

        public DbSet<RecurringPaymentLog> RecurringPaymentLog {
            get; set;
        }

        public DbSet<RecurringPayment> RecurringPayment {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;Role&gt; can be used to query and save instances of Role entity. 
        /// Linq queries can written using DbSet&lt;role &gt; that will be translated to sql query and executed against database role table.
        /// </summary>
        public DbSet<Role> Role {
            get; set;
        }

        /// DbSet&lt;role linking&gt; can be used to query and save instances of role linking entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database role linking table.    /// DbSet&lt;VerifiedAddress&gt; can be used to query and save instances of VerifiedAddress entity. 
        public DbSet<RoleLinking> RoleLinking {
            get; set;
        }

        /// DbSet&lt;role linking&gt; can be used to query and save instances of TenantUserAppPreference entity. 
        /// Linq queries can written using DbSet&lt;RL&gt; that will be translated to sql query and executed against database role linking table.    /// DbSet&lt;VerifiedAddress&gt; can be used to query and save instances of TenantUserAppPreference entity. 
        public DbSet<TenantUserAppPreference> TenantUserAppPreference {
            get; set;
        }
        public DbSet<ASNotification> ASNotification {
            get; set;
        }

        #endregion
    }
}
