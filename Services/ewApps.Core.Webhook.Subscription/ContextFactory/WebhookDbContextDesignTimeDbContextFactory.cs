﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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


namespace ewApps.Core.Webhook.Subscriber {
    /// <summary>
    /// Factory class to provides the Database instance
    /// </summary>
    public class WebhookSubscriptionDbContextDesignTimeDbContextFactory :BaseDesignTimeDbContextFactory<WebhookSubscriptionDBContext> {
        /// <inheritdoc/>
        protected override WebhookSubscriptionDBContext CreateNewInstance(DbContextOptions<WebhookSubscriptionDBContext> options) {
            return new WebhookSubscriptionDBContext(options, _conString);
        }
    }
}
