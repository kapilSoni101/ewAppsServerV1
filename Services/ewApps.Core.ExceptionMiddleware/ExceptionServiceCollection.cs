using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.ExceptionService {

    public static class ExceptionServiceCollection {

        public static IServiceCollection AddExceptionDependency(this IServiceCollection services, IConfiguration Configuration) {
            Microsoft.Extensions.Configuration.IConfigurationSection exceptionSection = Configuration.GetSection("ExceptionAppSettings");
            services.Configure<ExceptionAppSettings>(exceptionSection);
            return services;
        }

        public static IApplicationBuilder AddExceptionMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }

    }
}
