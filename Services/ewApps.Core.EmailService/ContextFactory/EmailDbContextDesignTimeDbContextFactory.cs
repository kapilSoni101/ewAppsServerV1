/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 27 March 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 27 March 2018
 */
using ewApps.Core.Webhook.Server;
using Microsoft.EntityFrameworkCore;


namespace ewApps.Core.EmailService {
    /// <summary>
    /// Factory class to provides the Database instance
    /// </summary>
    public class EmailDbContextDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<EmailDBContext> {
        protected override EmailDBContext CreateNewInstance(DbContextOptions<EmailDBContext> options) {
            return new EmailDBContext(options, _conString);
        }
    }
}