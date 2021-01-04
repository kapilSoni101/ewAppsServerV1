

/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 27 March 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 27 March 2018
 */

using ewApps.Core.Webhook.Server;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Core.SMSService {
    /// <summary>
    /// Factory class to provides the Database instance
    /// </summary>
    public class SMSDbContextDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<SMSDbContext> {
        protected override SMSDbContext CreateNewInstance(DbContextOptions<SMSDbContext> options) {
            return new SMSDbContext(options, _conString);
        }
    }
}