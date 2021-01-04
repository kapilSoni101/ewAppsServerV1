/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar<abadgujar@batchmaster>
 * Date: 22 August 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 22 August 2019
 */

using Microsoft.EntityFrameworkCore;


namespace ewApps.Core.UniqueIdentityGeneratorService {

    /// <summary>
    /// Factory class to provides the Database instance
    /// </summary>
    public class UniqueIdentityGeneratorDBContextDesignTimeDbContextFactory : BaseDesignTimeDbContextFactory<UniqueIdentityGeneratorDbContext> {
        /// <inheritdoc/>
        protected override UniqueIdentityGeneratorDbContext CreateNewInstance(DbContextOptions<UniqueIdentityGeneratorDbContext> options) {
            return new UniqueIdentityGeneratorDbContext(options, _conString);
        }
    }
}
