//using System;

//namespace ewApps.Core.EmailService {
//    public class EmailDispatcherFactory:IEmailDispatcherFactory {

//        IServiceProvider _serviceProvider;

//        public EmailDispatcherFactory(IServiceProvider serviceProvider) {
//            _serviceProvider = serviceProvider;
//        }

//        public IEmailDispatcher GetEmailDispatcher(DispatcherType dispatcherType) {
//            switch(dispatcherType) {
//                case DispatcherType.HMailSMTP:
//                    return (IEmailDispatcher)_serviceProvider.GetService(typeof(SMTPEmailDispatcher));
//                default:
//                    throw new Exception("Invalid dispatcher type.");
//            }
             
//        }

//    }
//}
