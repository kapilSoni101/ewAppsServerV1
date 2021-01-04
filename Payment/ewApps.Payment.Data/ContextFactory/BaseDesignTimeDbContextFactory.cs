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
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ewApps.Payment.Data
{

  public abstract class BaseDesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
  {

    protected string _conString;

    public TContext CreateDbContext(string[] args)
    {
      return Create(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
    }

    protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

    public TContext Create()
    {
      var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      Console.WriteLine("env name: " + environmentName);
      var basePath = AppContext.BaseDirectory;
      return Create(basePath, environmentName);
    }

    private TContext Create(string basePath, string environmentName)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "AppSettings"))
          .AddJsonFile("appsettings.dev.json", optional: true, reloadOnChange: true);

      var config = builder.Build();

      var connstr = config.GetConnectionString("DefaultConnection");
      _conString = connstr;
      if (String.IsNullOrWhiteSpace(connstr) == true)
      {
        throw new InvalidOperationException("Could not find a connection string named 'default'.");
      }
      else
      {
        return Create(connstr);
      }
    }

    private TContext Create(string connectionString)
    {
      if (string.IsNullOrEmpty(connectionString))
      {
        throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));
      }

      var optionsBuilder = new DbContextOptionsBuilder<TContext>();

      Console.WriteLine("MyDesignTimeDbContextFactory.Create(string): Connection string: {0}", connectionString);

      optionsBuilder.UseSqlServer(connectionString);

      DbContextOptions<TContext> options = optionsBuilder.Options;
      return CreateNewInstance(options);
    }
  }
}
