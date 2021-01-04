//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ewApps.Core.EmailService {
//    public class HMailDispatcher:IEmailDispatcher {

//        public void SendEmail(string body, string to, string subject, bool isBodyHTML = false) {
//            throw new NotImplementedException();
//        }

//    }

//    public class Office365Dispatcher:IEmailDispatcher {
//        public void SendEmail(string body, string to, string subject, bool isBodyHTML = false) {
//            throw new NotImplementedException();
//        }
//    }

//    //public class SMTPEmailDispatcher {
//    //    IEmailDispatcher dispatcher;

//    //    public SMTPEmailDispatcher(int type) {
//    //        dispatcher = EmailDispatcherFactory.GetEmailDispatcher(type);
//    //    }
//    //    public void SendEmail() {
//    //        dispatcher.SendEmail();
//    //    }
//    //}

//    public static class EmailDispatcherFactory {
//        public static IEmailDispatcher GetEmailDispatcher(int type) {
//            return new Office365Dispatcher();
//        }
//    }
//}
