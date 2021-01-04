//using ewApps.Core.Services.DI;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace ewApps.Shipment.DI {
//    public class ShipmentDIConfigurator {

//        DIConfigurationModel _diConfigurationModel;
//        CoreServiceConfigurator _coreServiceConfigurator;

//        public ShipmentDIConfigurator(DIConfigurationModel diConfigurationModel) {
//            _diConfigurationModel = diConfigurationModel;
//            //  _coreServiceConfigurator = new CoreServiceConfigurator(diConfigurationModel);
//        }

//        public ShipmentDIConfigurator(bool addDSDependencies, bool addDataDependencies, bool addOtherDependencies, bool addAppSettings) {
//            _diConfigurationModel = new DIConfigurationModel();
//            _diConfigurationModel.AddDataDependencies = addDataDependencies;
//            _diConfigurationModel.AddDSDependencies = addDSDependencies;
//            _diConfigurationModel.AddAppSettings = addAppSettings;
//            _diConfigurationModel.AddOtherDependencies = addOtherDependencies;
//        }

//        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) {

//            if(_diConfigurationModel.AddAppSettings) {
//                services = services.ConfigureShipmentAppSettings(configuration);
//            }

//            //if(_diConfigurationModel.ApplyWebApiConfiguration) {
//            //    _coreServiceConfigurator.ConfigureServices(services, configuration);
//            //}

//            //if(_diConfigurationModel.InitializeBaseConfiguration) {
//            //    base.ConfigureServices(services, configuration);
//            //}

//            if(_diConfigurationModel.AddDSDependencies) {
//                services.AddShipmentDataServiceDependency();
//            }

//            if(_diConfigurationModel.AddDataDependencies) {
//                services.AddShipmentDataDependency();
//            }

//            if(_diConfigurationModel.AddOtherDependencies) {
//                services.AddShipmentOtherDependency(configuration);
//            }

//            if(_diConfigurationModel.ApplyWebApiConfiguration) {
//                //services.ConfigureAuthenticationService(configuration);

//                //services.ConfigurationCORSService();

//                services.ConfigureSwaggerService();

//                //if(_diConfigurationModel.AddReportDependencies) {
//                //    services.AddReportDependency(configuration);
//                //}
//            }
//        }

//        public void Configure(IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env) {
//            //if(_diConfigurationModel.InitializeBaseConfiguration) {
//            //    base.Configure(configuration, app, env);
//            //}

//            //if(_diConfigurationModel.ApplyWebApiConfiguration) {
//            //    app.AddShipmentMiddleware();

//            //    app.ConfigurationAuthentication();

//            app.ConfigureSwagger();

//            //    app.ConfigureCORS();
//            //}
//        }
//    }
//}
