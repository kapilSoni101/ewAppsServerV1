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
using Microsoft.EntityFrameworkCore;


namespace ewApps.Core.Webhook.Server {
/// <summary>
/// Context factory responsible to provide the Database instance
/// </summary>
  public class WebhookDbContextDesignTimeDbContextFactory: BaseDesignTimeDbContextFactory<WebhookDBContext> {
        /// <inheritdoc/>
        protected override WebhookDBContext CreateNewInstance(DbContextOptions<WebhookDBContext> options) {
      return new WebhookDBContext(options, _conString);
    }
  }
}
