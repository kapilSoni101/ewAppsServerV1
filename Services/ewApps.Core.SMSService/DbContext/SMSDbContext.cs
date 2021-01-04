using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Core.SMSService {
    public class SMSDbContext:DbContext {
        #region Constructor and Veriable
        private SMSAppSettings _connOptions;
        private string _connString;


        public SMSDbContext(DbContextOptions<SMSDbContext> context, string connString) : base(context) {
            _connString = connString;
        }


        public SMSDbContext(DbContextOptions<SMSDbContext> context, IOptions<SMSAppSettings> appSetting) : base(context) {
            _connOptions = appSetting.Value;
            _connString = _connOptions.ConnectionString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Defines all the configuratiion option for the Database.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_connString); // Use Sql Server as Backend
            base.OnConfiguring(optionsBuilder);
        }

        #endregion

        #region DataSets


        public DbSet<SMSQueue> SMSQueues {
            get;
            set;
        }

        public DbSet<SMSDeliveryLog> SMSDeliveryLogs {
            get; set;
        }
        #endregion
    }
}
