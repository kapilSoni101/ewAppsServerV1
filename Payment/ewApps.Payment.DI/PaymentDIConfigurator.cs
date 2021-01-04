//using ewApps.Core.DI;
//using ewApps.Core.Services.DI;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace ewApps.Payment.DI {

//    public class PaymentDIConfigurator {

//        DIConfigurationModel _diConfigurationModel;
//       // CoreServiceConfigurator _coreServiceConfigurator;

//        public PaymentDIConfigurator(DIConfigurationModel diConfigurationModel)
//             {
//            _diConfigurationModel = diConfigurationModel;
//          //  _coreServiceConfigurator = new CoreServiceConfigurator(diConfigurationModel);
//        }

//        public PaymentDIConfigurator(bool addDSDependencies, bool addDataDependencies, bool addOtherDependencies, bool addAppSettings)
//             {
//            _diConfigurationModel = new DIConfigurationModel();
//            _diConfigurationModel.AddDataDependencies = addDataDependencies;
//            _diConfigurationModel.AddDSDependencies = addDSDependencies;
//            _diConfigurationModel.AddAppSettings = addAppSettings;
//            _diConfigurationModel.AddOtherDependencies = addOtherDependencies;
//        }

//        public void ConfigureServices(IServiceCollection services, IConfiguration configuration) {
//            if(_diConfigurationModel.AddAppSettings) {
//                services = services.ConfigurePaymentAppSettings(configuration);
//            }

//            //if(_diConfigurationModel.ApplyWebApiConfiguration) {
//            //    _coreServiceConfigurator.ConfigureServices(services, configuration);
//            //}

//            //if(_diConfigurationModel.InitializeBaseConfiguration) {
//            //    base.ConfigureServices(services, configuration);
//            //}

//            if(_diConfigurationModel.AddDSDependencies) {
//                services.AddPaymentDataServiceDependency();
//            }

//            if(_diConfigurationModel.AddDataDependencies) {
//                services.AddPaymentDataDependency();
//            }

//            if(_diConfigurationModel.AddOtherDependencies) {
//                services.AddPaymentOtherDependency(configuration);
//            }

//            if(_diConfigurationModel.ApplyWebApiConfiguration) {
//                //services.ConfigureResponseCompressionService();

//                //services.ConfigureAuthenticationService(configuration);

//                //services.ConfigurationCORSService();

//                services.ConfigureSwaggerService();
//            }
//        }

//        public void Configure(IConfiguration configuration, IApplicationBuilder app, IHostingEnvironment env) {
//            //if(_diConfigurationModel.InitializeBaseConfiguration) {
//            //    base.Configure(configuration, app, env);
//            //}

//            if(_diConfigurationModel.ApplyWebApiConfiguration) {
//                //app.ConfigureResponseCompressionMiddleware();
//                //app.AddPaymentMiddleware();
//                //app.ConfigurationAuthentication();
//                app.ConfigureSwagger();
                
//            }

//            //_coreServiceConfigurator.Configure(configuration, app, env);
//        }
//    }
//}