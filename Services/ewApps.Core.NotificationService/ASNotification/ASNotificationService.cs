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
using ewApps.Core.SMSService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.NotificationService {
    public class ASNotificationService:IASNotificationService {

        private ILogger<ASNotificationService> _loggerService;

        public ASNotificationService(ILogger<ASNotificationService> loggerService) {
            _loggerService = loggerService;
        }


        /// <summary>
        /// This method Generates all XML data,Send it to XSLT and gets the email Body
        /// and generates Email notification
        /// </summary>
        public async Task<ASNotificationDTO> GetASNotificationAsync(ASNotificationPayload asPayload, CancellationToken token = default(CancellationToken)) {
            try {
                #region variables

                string language = asPayload.UserLanguage;

                Dictionary<string, string> xsltArguments = asPayload.XSLTArguments;

                #endregion

                // Gets AS body and title string 
                string asBody = GetASBody(asPayload, xsltArguments, language);

                ASNotificationDTO aSNotificationDTO = new ASNotificationDTO();
                aSNotificationDTO.AppId = asPayload.AppId;
                aSNotificationDTO.HtmlContent = "";
                aSNotificationDTO.Read = false;
                aSNotificationDTO.RecipientUserId = asPayload.RecepientUserId;
                aSNotificationDTO.SourceEntityId = asPayload.EntityId;
                aSNotificationDTO.SourceEntityType = asPayload.EntityType;
                aSNotificationDTO.LinkNotificationId = asPayload.LinkNotificationId;
                aSNotificationDTO.TenantId = asPayload.TenantId;
                aSNotificationDTO.TextContent = asBody;
                aSNotificationDTO.AdditionalInfo = asPayload.AdditionalInfo;
                aSNotificationDTO.ASNotificationType = asPayload.ASNotificationType;

                return aSNotificationDTO;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in AsNotificationService.GetASNotificationAsync:-");
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
        private string GetASBody(ASNotificationPayload smsPayload, Dictionary<string, string> xsltArguments, string language) {
            string smsXsltFileText = smsPayload.XSLTemplateContent;
            return XSLHelper.XSLTransformByXslAndXmlStringToPlainText(smsXsltFileText, smsPayload.EventXMLData, xsltArguments);
        }

        #region Get XSL Template File


        #endregion

    }
}
