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
using ewApps.Core.DbConProvider;
using Microsoft.EntityFrameworkCore;


namespace ewApps.BusinessEntity.Data {

  public class BusinessEntityDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<BusinessEntityDbContext> {

        protected IConnectionManager _connectionManager;

        //public CoreDbContextDesignTimeDbContextFactory(IConnectionManager connectionManager) {
        //    _connectionManager = connectionManager;
        //}

        protected override BusinessEntityDbContext CreateNewInstance(DbContextOptions<BusinessEntityDbContext> options) {
            return new BusinessEntityDbContext(options, _conString);
        }

    }
}
