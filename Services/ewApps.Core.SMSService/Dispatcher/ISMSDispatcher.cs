using com.esendex.sdk.messaging;

namespace ewApps.Core.SMSService {
    public interface ISMSDispatcher {
        string SendSMS(string recipient, string body);
    }
}
