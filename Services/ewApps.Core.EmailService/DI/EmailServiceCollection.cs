/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 02 July 2019
 */
 
 using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.EmailService {

    public static class EmailServiceCollection {

    public static IServiceCollection AddEmailDependency(this IServiceCollection services, IConfiguration Configuration, EmailDispatcherType dispatcherType) {
      services.AddDbContext<EmailDBContext>();
      services.AddScoped<IEmailService, EmailService>();
      services.AddScoped<IEmailDeliveryLogDS, EmailDeliveryLogDS>();
      services.AddScoped<IEmailQueueDS, EmailQueueDS>();

      switch(dispatcherType) {
        case EmailDispatcherType.HMailSMTP:
          services.AddScoped<IEmailDispatcher, SMTPEmailDispatcher>();
          break;
      }

      //// services.AddSingleton<IEmailDispatcherFactory, EmailDispatcherFactory>();
      //services.AddScoped(typeof(EmailNotificationManager));

      var emailSection = Configuration.GetSection("EmailSmtpAppSettings");
      services.Configure<EmailAppSettings>(emailSection);
      return services;
    }
  }
}
