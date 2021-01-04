/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 29 August 2019
 *
 *
 * Contributor/s: Sourabh Agrawal
 * Last Updated On: 29 August 2019
 */


using ewApps.Report.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Report.QData {

    // Hari Sir Review

    /// <summary>  
    /// This class contains a session of core database and can be used to query and 
    /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
    /// </summary>
    public partial class QReportDbContext:DbContext {

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
    public QReportDbContext(DbContextOptions<QReportDbContext> options, string connString) : base(options) {
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
    public QReportDbContext(DbContextOptions<QReportDbContext> options, IOptions<ReportAppSettings> appSetting) : base(options) {
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

    #endregion DbQuery
  }
}