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
using ewApps.Shipment.Common;
using ewApps.Shipment.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Shipment.Data {

    public partial class ShipmentDbContext:DbContext {

        private string _connString;
        IConnectionManager _connectionManager;
        SqlConnection _connection;

        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options, string connString) : base(options) {
            _connString = connString;
        }

        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options, IOptions<ShipmentAppSettings> appSetting, IConnectionManager connectionManager) : base(options) {
            _connString = appSetting.Value.ConnectionString;
            _connectionManager = connectionManager;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Query<NotificationRecipient>().Ignore(a => a.SMSPreference).Ignore(a => a.SMSRecipient);
            //builder.Query<Core.NotificationService.NotificationRecipient>();
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

        #region DbSet

        /// <summary>
        /// DbSet&lt;PackageMaster&gt; can be used to query and save instances of PackageMaster entity. 
        /// Linq queries can written using DbSet&lt;PackageMaster&gt; that will be translated to sql query and executed against database PackageMaster table.
        /// </summary>
        public DbSet<PackageMaster> PackageMasters {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;PackageMaster&gt; can be used to query and save instances of CarrierPackageDetail entity. 
        /// Linq queries can written using DbSet&lt;CarrierPackageDetail&gt; that will be translated to sql query and executed against database CarrierPackageDetail table.
        /// </summary>
        public DbSet<CarrierPackageDetail> CarrierPackageDetail {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;CarrierPackageLinking&gt; can be used to query and save instances of CarrierPackageLinking entity. 
        /// Linq queries can written using DbSet&lt;CarrierPackageLinking&gt; that will be translated to sql query and executed against database CarrierPackageLinking table.
        /// </summary>
        public DbSet<CarrierPackageLinking> CarrierPackageLinking {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;FavouriteShipmentPkgSetting&gt; can be used to query and save instances of FavouriteShipmentPkgSetting entity. 
        /// Linq queries can written using DbSet&lt;FavouriteShipmentPkgSetting&gt; that will be translated to sql query and executed against database FavouriteShipmentPkgSetting table.
        /// </summary>
        public DbSet<FavouriteShipmentPkgSetting> FavouriteShipmentPkgSetting {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;SalesOrderPkg&gt; can be used to query and save instances of SalesOrderPkg entity. 
        /// Linq queries can written using DbSet&lt;SalesOrderPkg&gt; that will be translated to sql query and executed against database SalesOrderPkg table.
        /// </summary>
        public DbSet<SalesOrderPkg> SalesOrderPkg {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;SalesOrderPkgItem&gt; can be used to query and save instances of SalesOrderPkgItem entity. 
        /// Linq queries can written using DbSet&lt;SalesOrderPkgItem&gt; that will be translated to sql query and executed against database SalesOrderPkgItem table.
        /// </summary>
        public DbSet<SalesOrderPkgItem> SalesOrderPkgItem {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;Shipment&gt; can be used to query and save instances of Shipment entity. 
        /// Linq queries can written using DbSet&lt;Shipment&gt; that will be translated to sql query and executed against database Shipment table.
        /// </summary>
        public DbSet<ewApps.Shipment.Entity.Shipment> Shipment {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;ShipmentItem&gt; can be used to query and save instances of ShipmentItem entity. 
        /// Linq queries can written using DbSet&lt;ShipmentItem&gt; that will be translated to sql query and executed against database ShipmentItem table.
        /// </summary>
        public DbSet<ShipmentItem> ShipmentItem {
            get; set;
        }


        /// <summary>
        /// DbSet&lt;ShipmentPkgItem&gt; can be used to query and save instances of ShipmentPkgItem entity. 
        /// Linq queries can written using DbSet&lt;ShipmentPkgItem&gt; that will be translated to sql query and executed against database ShipmentPkgItem table.
        /// </summary>
        public DbSet<ShipmentPkgItem> ShipmentPkgItem {
            get; set;
        }

        /// <summary>
        /// DbSet&lt;VerifiedAddress&gt; can be used to query and save instances of VerifiedAddress entity. 
        /// Linq queries can written using DbSet&lt;VerifiedAddress&gt; that will be translated to sql query and executed against database VerifiedAddress table.
        /// </summary>
        public DbSet<VerifiedAddress> VerifiedAddress {
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

        #endregion DbSet

    }
}
