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

using ewApps.Shipment.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ewApps.Shipment.QData {

  // Hari Sir Review

  /// <summary>  
  /// This class contains a session of core database and can be used to query and 
  /// save instances of core entities. It is a combination of the Unit Of Work and Repository patterns.  
  /// </summary>
  public partial class QShipmentDbContext:DbContext {

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
    public QShipmentDbContext(DbContextOptions<QShipmentDbContext> options, string connString) : base(options) {
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
    public QShipmentDbContext(DbContextOptions<QShipmentDbContext> options, IOptions<ShipmentAppSettings> appSetting) : base(options) {
      _connString = appSetting.Value.CQRSConnectionString;
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