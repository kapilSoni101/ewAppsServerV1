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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.SMSService {
    public class SMSService:ISMSService {
        ISMSDispatcher _smsDispatcher;

        private ISMSQueueDS _smsQueueDS;
        private ILogger<SMSService> _loggerService;

        public SMSService(ISMSQueueDS smsQueueDS, ISMSDispatcher smsDispatcher, ILogger<SMSService> loggerService, IOptions<SMSAppSettings> appSetting) {
            _smsQueueDS = smsQueueDS;
            _smsDispatcher = smsDispatcher;
            _loggerService = loggerService;
        }

        public bool SendSMS(SMSQueue smsNotification) {
            try {
                _smsDispatcher.SendSMS(smsNotification.Recipient, smsNotification.MessagePart2);
                return true;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in SMSService.SendSMS:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        //public bool SendTwilioSMS(SMSQueue smsNotification) {
        //    try {
        //        _smsDispatcher.SendSMS(smsNotification.Recipient, smsNotification.MessagePart2);
        //        return true;
        //    }
        //    catch(Exception ex) {

        //        throw;
        //    }
        //}

        /// <summary>
        /// This method Generates all XML data,Send it to XSLT and gets the email Body
        /// and generates Email notification
        /// </summary>
        public async Task<bool> SendSMSAsync(SMSPayload smsPayload, CancellationToken token = default(CancellationToken)) {
            try {
                #region variables

                Guid recipientUserId = smsPayload.RecepientUserId;
                string smsAddress = smsPayload.SMSRecipient;
                Guid tenantId = smsPayload.TenantId;
                Guid trackingId = new Guid(smsPayload.NotificationInfo["TrackingId"]);
                Guid linkNotificationId = new Guid(smsPayload.NotificationInfo["LinkNotificationId"]);
                DateTime deliveryTime = DateTime.UtcNow;
                Guid appId = Guid.Empty;
                bool hasLinkError = smsPayload.HasLinkError;
                string language = smsPayload.UserLanguage;
                Dictionary<string, string> xsltArguments = smsPayload.XSLTArguments;

                #endregion

                if(smsPayload.DeeplinkResultSet != null) {
                    foreach(string key in smsPayload.DeeplinkResultSet.DeeplinkResults.Keys) {
                        xsltArguments.Add(key, smsPayload.DeeplinkResultSet.DeeplinkResults[key].DeeplinkURL);
                    }
                }

                // Gets SMS body and title string 
                string smsBody = GetSMSBody(smsPayload, xsltArguments, trackingId, language);

                SendSMS(smsBody, recipientUserId, smsAddress, tenantId, appId, linkNotificationId, hasLinkError, smsPayload.SMSUserSession, smsPayload.NotificationDeliveryType);

                return true;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in SMSService.SendSMSAsync:-");
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
        private string GetSMSBody(SMSPayload smsPayload, Dictionary<string, string> xsltArguments, Guid trackingId, string language) {
            // Each event notification has unique id for the given event for all users.


            // Generate the XSLT file if not in cache or cache is not suppose to be used.
            //if(!CacheHelper.IsInCache(templateTitleKey)) {

            string smsXsltFileText = smsPayload.XSLTemplateContent;

            return XSLHelper.XSLTransformByXslAndXmlStringToPlainText(smsXsltFileText, smsPayload.EventXMLData, xsltArguments);
        }

        #region Get XSL Template File


        #endregion

        #region SMS

        /// <summary>Send SMS and add notification queue data.</summary>    
        private void SendSMS(string smsBody, Guid recipientUserId, string smsAddress, Guid tenantId, Guid appId, Guid notificationId, bool hasLinkError, SMSUserSessionDTO userSession, int deliveryType) {
            try {
                SMSQueue notificationQueue = new SMSQueue();
                notificationQueue.Recipient = smsAddress;
                notificationQueue.DeliveryType = deliveryType;
                notificationQueue.DeliveryTime = DateTime.UtcNow;
                notificationQueue.MessagePart1 = string.Empty;
                notificationQueue.MessagePart2 = smsBody;
                // ToDo: Change NotificationId -> LinkNotificationId
                notificationQueue.NotificationId = notificationId;
                notificationQueue.ApplicationId = appId;
                //notificationQueue.SenderName = string.Empty;
                notificationQueue.State = (int)SMSNotificationState.Queued;
                if(userSession != null) {
                    notificationQueue.CreatedBy = userSession.TenantUserId;
                }
                else {
                    notificationQueue.CreatedBy = Guid.Empty;
                }
                notificationQueue.CreatedOn = DateTime.UtcNow;
                notificationQueue.UpdatedBy = notificationQueue.CreatedBy;
                notificationQueue.UpdatedOn = notificationQueue.CreatedOn;
                _smsQueueDS.Add(notificationQueue);

                //_smsDispatcher.SendSMS(smsAddress, smsBody);

                _smsQueueDS.Save();
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in SMSService.SendSMS:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #endregion



    }
}
