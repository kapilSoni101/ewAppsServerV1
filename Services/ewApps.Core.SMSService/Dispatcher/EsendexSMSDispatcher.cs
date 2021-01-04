/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Nitin Jain <njain@eworkplaceapps.com>
 * Date: 14 April 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 April 2019
 */

using com.esendex.sdk;
using com.esendex.sdk.messaging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.SMSService {
    public class EsendexSMSDispatcher:ISMSDispatcher {
        private MessagingService _messagingService;
        private EsendexServiceAppSettings _esendexServiceAppSetting;
        private readonly string _accountRef = "";

        public EsendexSMSDispatcher(IOptions<EsendexServiceAppSettings> smsServiceAppSetting) {
            _esendexServiceAppSetting = smsServiceAppSetting.Value;
            _accountRef = _esendexServiceAppSetting.AccountReference;
            InitializeService();
        }

        private void InitializeService() {
            EsendexCredentials credential = new EsendexCredentials(_esendexServiceAppSetting.UserId, _esendexServiceAppSetting.Password);
            _messagingService = new MessagingService(true, credential);
        }

        public string SendSMS(string recipient, string body) {
            SmsMessage smsMessage = new SmsMessage(recipient, body, _accountRef);
            return _messagingService.SendMessage(smsMessage).MessageIds[0].Id.ToString();
        }
    }
}
