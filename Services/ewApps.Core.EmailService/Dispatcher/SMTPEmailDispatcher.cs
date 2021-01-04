/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 03 OCT 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace ewApps.Core.EmailService {

    public class SMTPEmailDispatcher:IEmailDispatcher {

        #region member variable

        EmailAppSettings _emailSmtpSettings;

        #endregion

        #region Constructor

        public SMTPEmailDispatcher(IOptions<EmailAppSettings> appSetting) {
            _emailSmtpSettings = appSetting.Value;
        }

        #endregion

        #region public methods 

        public void SendEmail(string body, string to, string subject, bool isBodyHTML = true) {
            string senderEmail = _emailSmtpSettings.SenderEmail;
            string[] toList = to.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            MailAddress mailAddress = new MailAddress(_emailSmtpSettings.SenderEmail, _emailSmtpSettings.SenderDisplayName);

            MailMessage message = new MailMessage();
            message.Body = body;
            message.IsBodyHtml = isBodyHTML;
            message.Subject = subject;

            for(int i = 0; i < toList.Length; i++) {
                if(string.IsNullOrWhiteSpace(toList[i]) == false) {
                    MailAddress toAddr = new MailAddress(toList[i].Trim());
                    message.To.Add(toAddr);
                }
            }

            message.From = mailAddress;

            string fromPassword = _emailSmtpSettings.SenderPwd;

            SmtpClient smtp = new SmtpClient {
                Host = _emailSmtpSettings.SMTPServer,
                Port = Convert.ToInt32(_emailSmtpSettings.SMTPPort),
                EnableSsl = Convert.ToBoolean(_emailSmtpSettings.EnableSSL),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailAddress.Address, fromPassword)
            };

            smtp.Send(message);

        }
        #endregion
    }
}
