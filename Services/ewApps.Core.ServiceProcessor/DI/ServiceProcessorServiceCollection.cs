using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.ServiceProcessor
{

    public static class ServiceProcessorServiceCollection
  {

        public static IServiceCollection AddServiceProcessorDependency(this IServiceCollection services, IConfiguration Configuration) {
      services.AddTransient<IHttpRequestProcessor, HttpRequestProcessor>();
      services.AddTransient<IHttpHeaderHelper, HttpHeaderHelper>();
      services.AddTransient<IHttpRequestProcessor, HttpRequestProcessor>();
      return services;
        }

        public static IApplicationBuilder AddExceptionMiddleware(this IApplicationBuilder app) {
//            app.UseMiddleware<ErrorHandlingMiddleware>();
            return app;
        }

    }
}
