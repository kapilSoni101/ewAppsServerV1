namespace ewApps.Core.SMSService {
    public class SMSAppSettings {

        public string ConnectionString {
            get; set;
        }

        public EsendexServiceAppSettings EsendexAppSettings {
            get; set;
        }

        public TwillioServiceAppSettings TwillioAppSettings {
            get; set;
        }

    }
}
