using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.DI {

    public static class UserSessionServiceCollection {

        public static IServiceCollection AddUserSessionDependency(this IServiceCollection services, IConfiguration Configuration ,UserSessionOptions userSessionOptions ) {
      //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
      services.AddHttpContextAccessor();
            services.AddDbContext<UserSessionDBContext>(ServiceLifetime.Transient);
            if(userSessionOptions.RemoteSession) {
                services.AddScoped<IUserSessionManager, RemoteUserSessionManager>();
            }
            else {
                services.AddScoped<IUserSessionManager, UserSessionManager>();
            }
            services.AddScoped(typeof(UserSessionOptions));
            IConfigurationSection userSessionSection = Configuration.GetSection("UserSessionAppSettings");
            services.Configure<UserSessionAppSettings>(userSessionSection);
            return services;
        }

        public static IApplicationBuilder AddUserSessionMiddleware(this IApplicationBuilder app, UserSessionOptions options) {
            app.UseMiddleware<UserSessionMiddleware>(options);
            return app;
        }

    }
}
