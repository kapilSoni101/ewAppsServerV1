/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.SMSService {

    public static class SMSServiceCollection {

        public static IServiceCollection AddSMSServiceDependency(this IServiceCollection services, IConfiguration Configuration, SMSDispatcherType dispatcherType) {
            services.AddDbContext<SMSDbContext>();
            //services.AddScoped<ISMSQueueDS, SMSQueueDS>();
            services.AddTransient<ISMSQueueDS, SMSQueueDS>();
            //services.AddScoped<ISMSDeliveryLogDS, SMSDeliveryLogDS>();
            services.AddTransient<ISMSDeliveryLogDS, SMSDeliveryLogDS>();
            //services.AddScoped<ISMSService, SMSService>();
            services.AddTransient<ISMSService, SMSService>();

            // services.AddScoped<ISMSDispatcher, TwilioSMSDispatcher>();

            IConfigurationSection smsServiceSection = Configuration.GetSection("SMSAppSettings");
            services.Configure<SMSAppSettings>(smsServiceSection);

            switch(dispatcherType) {
                case SMSDispatcherType.Twilio:
                    services.AddScoped<ISMSDispatcher, TwilioSMSDispatcher>();
                    IConfigurationSection twillioSmsServiceSection = Configuration.GetSection("SMSAppSettings:TwillioAppSettings");
                    services.Configure<TwillioServiceAppSettings>(twillioSmsServiceSection);
                    break;
            }
          
            //IConfigurationSection esendexSmsServiceSection = Configuration.GetSection("EsendexAppSettings");
            //services.Configure<EsendexServiceAppSettings>(esendexSmsServiceSection);
            //IConfigurationSection twillioSmsServiceSection = Configuration.GetSection("SMSAppSettings:TwillioAppSettings");
            //services.Configure<TwillioServiceAppSettings>(twillioSmsServiceSection);
            return services;
        }
    }
}
