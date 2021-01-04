using ewApps.Core.ThumbnailService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ewApps.Core.ThumbnailService {

  public static class ThumbnailServiceCollection {

    public static IServiceCollection AddThumbnailDependency(this IServiceCollection services, IConfiguration Configuration) {
      var thumbnailSection = Configuration.GetSection("ThumbnailServiceSettings");
      services.Configure<ThumbnailServiceSettings>(thumbnailSection);
      return services;
    }




  }
}
