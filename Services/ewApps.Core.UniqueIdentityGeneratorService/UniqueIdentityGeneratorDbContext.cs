/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Rajesh Thakur<rthakur@eworkplaceapps.com>
 * Date: 27 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace ewApps.Core.UniqueIdentityGeneratorService {

  /// <summary>
  /// Represents the DB context class for  UniqueIdentityGeneratorService
  /// </summary>
  //public class UniqueIdentityGeneratorDbContext:DbContext {
  //}

  public class UniqueIdentityGeneratorDbContext:DbContext {

    private string _connString;
    private UniqueIdentityGeneratorAppSettings _connOptions;
    private DateTime _intiTime;
    private IHttpContextAccessor _accessor;

    public UniqueIdentityGeneratorDbContext(DbContextOptions<UniqueIdentityGeneratorDbContext> context) : base(context) {
      _intiTime = DateTime.UtcNow;
    }

    public UniqueIdentityGeneratorDbContext(DbContextOptions<UniqueIdentityGeneratorDbContext> options, IOptions<UniqueIdentityGeneratorAppSettings> appSetting, IHttpContextAccessor accessor) : base(options) {
      _connOptions = appSetting.Value;
      _connString = _connOptions.ConnectionString;
      _intiTime = DateTime.UtcNow;
      _accessor = accessor;
    }

    /// <summary>
    /// Constructor with Context Options
    /// </summary>
    /// <param name="context"></param>
    /// <param name="loggingService"></param>
    public UniqueIdentityGeneratorDbContext(DbContextOptions<UniqueIdentityGeneratorDbContext> context, string connString) : base(context) {
      _connString = connString;
    }

    protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      // string _connectionString = "Database=ewApps-iPayment; Data Source=CONNECT23;User ID=sa;Password=sql2k14@connect";
      optionsBuilder.UseSqlServer(_connString);
      base.OnConfiguring(optionsBuilder);
    }


    /// <summary>
    /// DbSet&lt;UniqueIdentityGenerator&gt; can be used to query and save instances of UniqueIdentityGenerator entity. 
    /// Linq queries can written using DbSet&lt;UniqueIdentityGenerator&gt; that will be translated to sql query and executed against database UniqueIdentityGenerator table. 
    /// </summary>
    public virtual DbSet<UniqueIdentityGenerator> UniqueIdentityGenerator {
      get;
      set;
    }

  }


}

