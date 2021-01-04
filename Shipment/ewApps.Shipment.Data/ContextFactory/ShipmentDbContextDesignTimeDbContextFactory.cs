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
using System.Collections.Generic;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Shipment.Data {

  public class ShipmentDbContextDesignTimeDbContextFactory:ShipmentDesignTimeDbContextFactory<ShipmentDbContext> {
    protected IConnectionManager _connectionManager;

    //public ShipmentDbContextDesignTimeDbContextFactory(IConnectionManager connectionManager)
    //{
    //  _connectionManager = connectionManager;
    //}

    protected override ShipmentDbContext CreateNewInstance(DbContextOptions<ShipmentDbContext> options)
    {
      return new ShipmentDbContext(options, _conString);
    }

  
  }
}
