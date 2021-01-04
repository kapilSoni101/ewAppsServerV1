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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;

namespace ewApps.Core.EmailService {
    /// <summary>
    /// This class is responsible to send Email.
    /// </summary>
    public class EmailService:IEmailService {
        IEmailQueueDS _emailQueueDS;
        ILogger<EmailService> _loggerService;
        IEmailDispatcher _emailDispatcher;

        // ToDo: Remove IEmailDispatcher DI from constrcutor.        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService"/> class.
        /// </summary>
        /// <param name="emailQueueDS">The email queue data service instance.</param>
        /// <param name="emailDispatcher">The email dispatcher to send email.</param>
        /// <param name="loggerService">The logger service to generate log.</param>
        public EmailService(IEmailQueueDS emailQueueDS, IEmailDispatcher emailDispatcher, ILogger<EmailService> loggerService) {
            _emailQueueDS = emailQueueDS;
            _emailDispatcher = emailDispatcher;
            _loggerService = loggerService;
        }

        /// <inheritdoc/>
        public async Task<bool> SendEmailAsync(EmailPayload emailPayload, CancellationToken token = default(CancellationToken)) {
            try {
                // Gets recipient user id and other data from data table.
                #region variables

                Guid recipientUserId = emailPayload.RecepientUserId;
                string emailAddress = emailPayload.UserEmailAddress;
                Guid tenantId = emailPayload.TenantId;
                Guid trackingId = new Guid(emailPayload.NotificationInfo["TrackingId"]);
                Guid linkNotificationId = new Guid(emailPayload.NotificationInfo["LinkNotificationId"]);
                DateTime deliveryTime = DateTime.UtcNow;
                //Guid appId = EDApplicationInfo.AppId;
                Guid appId = Guid.Empty;
                bool hasLinkError = emailPayload.HasLinkError;
                string language = emailPayload.UserLanguage;
                Dictionary<string, string> xsltArguments = emailPayload.XSLTArguments;

                string emailTitle = string.Empty, emailBody = string.Empty;

                #endregion

                // Nitin: Added this code.
                if(emailPayload.DeeplinkResultSet != null) {
                    foreach(string key in emailPayload.DeeplinkResultSet.DeeplinkResults.Keys) {
                        xsltArguments.Add(key, emailPayload.DeeplinkResultSet.DeeplinkResults[key].DeeplinkURL);
                    }
                }

                // Gets Push body and title string 
                Tuple<string, string> emailDetail = GetEmailTitleAndBody(emailPayload, xsltArguments, trackingId, language);
                emailTitle = emailDetail.Item1;
                emailBody = emailDetail.Item2;

                // Get user tags
                SendEmail(emailTitle, emailBody, recipientUserId, emailAddress, tenantId, appId, linkNotificationId, hasLinkError, emailPayload.EmailUserSession, emailPayload.NotificationDeliveryType);
                return true;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in EmailService.SendEmailAsync:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary>
        /// Gets the Template from Cache or
        /// Generate the template by XML and XSLT arguments and sets in cache fornext use.
        /// </summary>
        private Tuple<string, string> GetEmailTitleAndBody(EmailPayload emailPayload, Dictionary<string, string> xsltArguments, Guid trackingId, string language) {
            //Get email title and body template key.
            //Each event notification has unique id for the given event for all users.
            string templateTitleKey = string.Format("{0}-{1}-{2}", trackingId, "EmailTitle", language);

            string templateBodyKey = string.Format("{0}-{1}-{2}", trackingId, "EmailBody", language);

            string emailTitle, emailBody;

            // Generate the XSLT file if not in cache or cache is not suppose to be used.
            //if(!CacheHelper.IsInCache(templateTitleKey)) {

            string emailXsltFileText = emailPayload.XSLTemplateContent; //XSLTHelper.GetEmailXsltFile(language);

            emailBody = XSLHelper.XSLTransformByXslAndXmlString(emailXsltFileText, emailPayload.EventXMLData, "title", "body", out emailTitle, xsltArguments);
            // emailBody = ewApps.CommonRuntime.Common.Utils.HtmlDecode(emailBody);
            //Add to cache if cahe can be used.
            // CacheHelper.SetData(templateTitleKey, emailTitle, string.Empty);
            // CacheHelper.SetData(templateBodyKey, emailBody, string.Empty);
            //}
            //else {
            //    emailTitle = CacheHelper.GetData<string>(templateTitleKey);
            //    emailBody = CacheHelper.GetData<string>(templateBodyKey);
            //}
            return new Tuple<string, string>(emailTitle, emailBody);
        }

        #region Get XSL Template File

        //// <summary>
        ///// Gets the XSLT file based on user language from the cache if available
        ///// otherwise load from the file system.
        ///// </summary>
        ///// <param name="regionLanguage">user language</param>
        ///// <returns></returns>
        //public static string GetEmailXsltFile(string regionLanguage) {
        //  string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
        //  if (CacheHelper.IsInCache(cacheKey))
        //  {
        //    return CacheHelper.GetData<string>(cacheKey); ;
        //  }
        //  else
        //  {
        //    //Engg: should define in Config file.
        //    string xsltResourcePath = "/resourcePath/";
        //    string xsltTextFile = FileHelper.ReadFileAsText(GetEmailLocalizedTemplatePath(regionLanguage, xsltResourcePath));
        //    //CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
        //    return xsltTextFile;
        //  }
        //}

        #endregion

        #region Email

        /// <summary>
        /// Send Email and add notification queue data.
        /// </summary>
        private void SendEmail(string messagePart1, string messagePart2, Guid recipientUserId, string emailAddress, Guid tenantId, Guid appId, Guid linkNotificationId, bool hasLinkError, EmailUserSessionDTO userSession, int deliveryType) {
            try {
                EmailQueue notificationQueue = new EmailQueue();
                //notificationQueue.DeliveryType = (int)NotificationDeliveryType.Email;
                notificationQueue.DeliveryType = deliveryType;
                notificationQueue.DeliveryTime = DateTime.UtcNow;
                notificationQueue.MessagePart1 = messagePart1;
                notificationQueue.MessagePart2 = messagePart2;
                // ToDo: Change NotificationId -> LinkNotificationId
                notificationQueue.NotificationId = linkNotificationId;
                notificationQueue.ReplyTo = notificationQueue.Recipient = emailAddress;
                notificationQueue.ApplicationId = appId;
                notificationQueue.SenderName = string.Empty;
                notificationQueue.State = (int)EmailNotificationState.Queued;

                if(userSession != null) {
                    notificationQueue.CreatedBy = userSession.TenantUserId;
                }
                else {
                    notificationQueue.CreatedBy = Guid.Empty;
                }

                notificationQueue.CreatedOn = DateTime.UtcNow;
                notificationQueue.UpdatedBy = notificationQueue.CreatedBy;
                notificationQueue.UpdatedOn = notificationQueue.CreatedOn;
                _emailQueueDS.Add(notificationQueue);

                _emailQueueDS.Save();

                ////ToDo: Remove below line to send email once notification manager class is working fine.
                //_emailDispatcher.SendEmail(messagePart2, emailAddress, messagePart1, true);


            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in EmailService.SendEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <summary>
        /// Sends the adhoc email.
        /// </summary>
        /// <param name="adhocEmailDTO">The adhoc email dto.</param>
        public void SendAdhocEmail(AdhocEmailDTO adhocEmailDTO) {
            SendEmail(adhocEmailDTO.MessagePart1, adhocEmailDTO.MessagePart2, Guid.Empty, adhocEmailDTO.EmailAddress, Guid.Empty, adhocEmailDTO.AppId, Guid.NewGuid(), false, null, adhocEmailDTO.DeliveryType);
        }

        #endregion
    }
}
