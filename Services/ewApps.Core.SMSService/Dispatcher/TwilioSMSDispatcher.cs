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

using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ewApps.Core.SMSService {
    public class TwilioSMSDispatcher:ISMSDispatcher {
        TwillioServiceAppSettings _twilioAppSetting;
        private ILogger<TwilioSMSDispatcher> _loggerService;

        public TwilioSMSDispatcher(IOptions<TwillioServiceAppSettings> twilioAppSetting, ILogger<TwilioSMSDispatcher> loggerService) {
            _twilioAppSetting = twilioAppSetting.Value;
            _loggerService = loggerService;
        }


        public string SendSMS(string recipient, string body) {
            //// Find your Account Sid and Token at twilio.com/console
            //// DANGER! This is insecure. See http://twil.io/secure
            //const string accountSid = "AC298016366508a9b85689b10b4a6392ea";
            //const string authToken = "62d9e16306f2c2d03e1a30944e9540fa";

            try {
                TwilioClient.Init(_twilioAppSetting.AccountSid, _twilioAppSetting.AuthToken);

                var message = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber(_twilioAppSetting.FromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(recipient)
                );

                return message.Sid;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in TwilioSMSDispatcher.SendSMS:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }
    }
}
