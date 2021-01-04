/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Hari Dudani, Asha Sharda
 * Date: 10 Nov 2018
 */

using ewApps.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ewApps.Connector.SAPB1.Data
{
  /// <summary>
  /// Factory class to provide instance of AppDBContext
  /// </summary>
  public class DbContextDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<AppDbContext>
  {
    /// <summary>
    /// Creates AppDBContext Instance
    /// </summary>
    /// <param name="options">DBContext options parameters </param>
    /// <remarks>It uses connection string defined in the base class</remarks>
    /// <returns></returns>
    protected override AppDbContext CreateNewInstance(DbContextOptions<AppDbContext> options)
    {
      return new AppDbContext(options, _conString);
    }
  }
}