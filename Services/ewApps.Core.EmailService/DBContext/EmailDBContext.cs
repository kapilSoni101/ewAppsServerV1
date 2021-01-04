/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 July 2019
 */

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Core.EmailService {
    
    public class EmailDBContext:DbContext {

        #region Constructor and Veriable

        private EmailAppSettings _connOptions;
        private string _connString;

        /// <summary>
        /// Constructor with Context Options
        /// </summary>
        public EmailDBContext(DbContextOptions<EmailDBContext> context, string connString) : base(context) {
            _connString = connString;
        }

        /// <summary>
        /// Constructor with AppSetting
        /// </summary>
        public EmailDBContext(DbContextOptions<EmailDBContext> context, IOptions<EmailAppSettings> appSetting) : base(context) {
            _connOptions = appSetting.Value;// appSetting.Value.EmailConnectionStrings;
            _connString = _connOptions.ConnectionString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Defines all the configuratiion option for the Database
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.UseSqlServer(_connString);//Use Sql Server as Backend
            base.OnConfiguring(optionsBuilder);
        }
        #endregion

        #region DataSets

        /// <summary>
        /// Email Queue DataSet
        /// </summary>
        public virtual DbSet<EmailQueue> EmailQueues {
            get;
            set;
        }

        /// <summary>
        /// Email Notification Delivery log DataSet
        /// </summary>
        public virtual DbSet<EmailDeliveryLog> EmailDeliveryLogs {
            get;
            set;
        }

        #endregion



    }
}
