using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.DMService {

    public static class DMServiceCollection {

        public static IServiceCollection AddDMDependency(this IServiceCollection services, IConfiguration Configuration) {

            AddDataDependency(services);
            AddDataServiceDependency(services); 

            services.AddDbContext<DMDBContext>(ServiceLifetime.Transient);           
            
            IConfigurationSection userSessionSection = Configuration.GetSection("DMServiceSettings");
            services.Configure<DMServiceSettings>(userSessionSection);
            return services;
        }

        public static IServiceCollection AddDataDependency(this IServiceCollection services) {          

            
            services.AddScoped<IDMDocumentFolderLinkingRepository, DMDocumentFolderLinkingRepository>();
            services.AddScoped<IDMDocumentRepository, DMDocumentRepository>();
            services.AddScoped<IDMDocumentUserRepository, DMDocumentUserRepository>();
            services.AddScoped<IDMDocumentVersionRepository, DMDocumentVersionRepository>();
            services.AddScoped<IDMFileStorageRepository, DMFileStorageRepository>();
            services.AddScoped<IDMFolderRepository, DMFolderRepository>();
            services.AddScoped<IDMThumbnailRepository, DMThumbnailRepository>();
            services.AddScoped<IEntityThumbnailRepository, EntityThumbnailRepository>();
            
            return services;
        }

        public static IServiceCollection AddDataServiceDependency(this IServiceCollection services) {

            services.AddScoped<IDMDocumentFolderLinkingDS, DMDocumentFolderLinkingDS>();
            services.AddScoped<IDMDocumentDS, DMDocumentDS>();
            //services.AddScoped<IDMDocumentUserDS, DMDocumentUserDS>();
            services.AddScoped<IDMDocumentVersionDS, DMDocumentVersionDS>();
            services.AddScoped<IDMFileStorageDS, DMFileStorageDS>();
            //services.AddScoped<IDMFolderDS, DMFolderDS>();
            services.AddScoped<IDMThumbnailDS, DMThumbnailDS>();
            services.AddScoped<IEntityThumbnailDS, EntityThumbnailDS>();
            services.AddScoped<IEntityThumbnailDS, EntityThumbnailDS>();
            services.AddScoped<IStorageService, EWAppsStorageService>();
            services.AddScoped<IUnitOfWorkDM, UnitOfWorkDM>();
            services.AddScoped<IFileBrowserViewerService, FileBrowserViewerService>();

            return services;
        }

        public static IApplicationBuilder AddDMMiddleware(this IApplicationBuilder app, UserSessionOptions options) {
            app.UseMiddleware<DMMiddleware>(options);
            return app;
        }

    }
}
