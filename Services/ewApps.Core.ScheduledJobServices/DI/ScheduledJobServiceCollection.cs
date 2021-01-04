/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */
 
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.ScheduledJobService {

    /// <summary>
    /// This class provides the collection of ScheduledServices with it's DI scope.
    /// </summary>
    public static class ScheduledJobServiceCollection {

        /// <summary>
        /// Registered scheduled job library service scope in DI framework.
        /// </summary>
        /// <param name="services">The services to get register with DI framework.</param>
        /// <param name="configuration">The service configuration.</param>
        /// <returns>Returns <see cref="IServiceCollection"/> instance with registerd services.</returns>
        public static IServiceCollection ScheduledJobDependency(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<ScheduledJobDBContext>();
            services.AddScoped<IScheduledJobManager, ScheduledJobManager>();
            var scheduledJobAppSection = configuration.GetSection("ScheduledJobAppSettings");
            services.Configure<ScheduledJobAppSettings>(scheduledJobAppSection);
            return services;
        }
    }
}
