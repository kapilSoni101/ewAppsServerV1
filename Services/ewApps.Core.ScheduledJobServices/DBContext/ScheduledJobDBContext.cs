/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 27 March 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 27 March 2019
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Core.ScheduledJobService {
    /// <summary>
    /// This class contains a session of scheduler database and can be used to query and 
    /// save instances of related entities. It is a combination of the 'Unit Of Work' and 'Repository' patterns.  
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ScheduledJobDBContext:DbContext {

        #region Constructor and Veriable

        private string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledJobDBContext"/> class.
        /// </summary>
        /// <param name="contextOption">The DbContextOptions instance carries configuration information such as: 
        /// (a) The database provider to use, UseSqlServer or UseSqlite
        /// (b) Connection string or identifier of the database instance    
        /// </param>
        /// <param name="connectionString">The scheduler database connection string.</param>
        public ScheduledJobDBContext(DbContextOptions<ScheduledJobDBContext> contextOption, string connectionString) : base(contextOption) {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledJobDBContext"/> class.
        /// </summary>
        /// <param name="contextOption">The DbContextOptions instance carries configuration information such as: 
        /// (a) The database provider to use, UseSqlServer or UseSqlite
        /// (b) Connection string or identifier of the database instance    
        /// </param>
        /// <param name="appSetting">An instance of <see cref="ScheduledJobAppSettings"/> options to get configuration information.</param>
        public ScheduledJobDBContext(DbContextOptions<ScheduledJobDBContext> contextOption, IOptions<ScheduledJobAppSettings> appSetting) : base(contextOption) {
            _connectionString = appSetting.Value.ConnectionString;

        }

        /// <summary>
        /// Initates master data generation process.
        /// </summary>
        /// <param name="builder">The model builder instance to generate database records.</param>
        protected override void OnModelCreating(ModelBuilder builder) {
            SchedulerMasterData.Init(builder);
            base.OnModelCreating(builder);
        }


        /// <summary>
        /// Defines all the configuration option for the Database.
        /// </summary>
        /// <param name="optionsBuilder">An instance of DbContextOptionsBuilder to assign database connection string.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // Assign database connection string.
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        #endregion

        #region DataSets

        /// <summary>
        /// DbSet&lt;ScheduledJob&gt; can be used to query and save instances of ScheduledJob entity. 
        /// Linq queries can written using DbSet&lt;ScheduledJob&gt; that will be translated to sql query and executed against database ScheduledJob table.
        /// </summary>
        public virtual DbSet<ScheduledJob> ScheduledJobs {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;ScheduledJobLog&gt; can be used to query and save instances of ScheduledJobLog entity. 
        /// Linq queries can written using DbSet&lt;ScheduledJobLog&gt; that will be translated to sql query and executed against database ScheduledJobLog table.
        /// </summary>
        public virtual DbSet<ScheduledJobLog> ScheduledJobLogs {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;Scheduler&gt; can be used to query and save instances of Scheduler entity. 
        /// Linq queries can written using DbSet&lt;Scheduler&gt; that will be translated to sql query and executed against database Scheduler table.
        /// </summary>
        public DbSet<Scheduler> Scheduler {
            get; set;
        }
        
        #endregion
    }
}
