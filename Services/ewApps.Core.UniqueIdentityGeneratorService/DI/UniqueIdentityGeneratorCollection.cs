using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.UniqueIdentityGeneratorService {

    public static class UniqueIdentityGeneratorCollection {

        public static IServiceCollection AddUniqueIdentityGeneratorDependency(this IServiceCollection services, IConfiguration Configuration) {
            services.AddDbContext<UniqueIdentityGeneratorDbContext>();
            services.AddScoped<IUniqueIdentityGeneratorRepository, UniqueIdentityGeneratorRepository>();
            services.AddScoped<IUniqueIdentityGeneratorDS, UniqueIdentityGeneratorDS>();

            IConfigurationSection identityGeneratorSection = Configuration.GetSection("UniqueIdentityGeneratorAppSettings");
            services.Configure<UniqueIdentityGeneratorAppSettings>(identityGeneratorSection);
            return services;
        }



    }
}
