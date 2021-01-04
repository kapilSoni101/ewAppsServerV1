

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.Money {

  public static class DocumentCurrencyServiceCollection {
    /// <summary>
    /// Add all the dependencies at one place
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDocumentCurrencyDependency(this IServiceCollection services, IConfiguration Configuration) {
      services.AddDbContext<DocumentCurrencyDBContext>();
      services.AddScoped<CurrencyCultureInfoTable>();   
      services.AddScoped<ICurrencyConversion, RealtimeCurrencyConversion>();
      services.AddScoped<CurrencyConverter>();
      services.AddScoped<IDocumentCurrencyManager, DocumentCurrencyManager>();
      var moneyAppSettingsSection = Configuration.GetSection("MoneyAppSettings");
      services.Configure<MoneyAppSettings>(moneyAppSettingsSection);
      return services;
    }

    /// <summary>
    /// Add middleware to initiaze tenant specific Data
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddCurrencyConverterMiddleware(this IApplicationBuilder app) {
      //Use middleware for FixedRateCurrencyConversion
      app.UseMiddleware<CurrencyConverterMiddleware>();
      return app;
    }

  }
}
