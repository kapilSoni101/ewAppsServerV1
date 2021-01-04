using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using ewApps.Payment.Common;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {
    public class PaymentNotificationService:NotificationService<NotificationRecipient>, IPaymentNotificationService {

        #region Member Variables

        IASNotificationDS _asNotificationDS;
        IPaymentNotificationRecipientDS _paymentNotificationRecipientDS;
        PaymentAppSettings _paymentAppSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentNotificationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="asNotificationService">As notification service.</param>
        /// <param name="asNotificationDS">As notification ds.</param>
        public PaymentNotificationService(ILogger<PaymentNotificationService> logger, IEmailService emailService, ISMSService smsService, IASNotificationService asNotificationService, IASNotificationDS asNotificationDS, IPaymentNotificationRecipientDS bizPaymentNotificationRecipientDS, IOptions<PaymentAppSettings> paymentAppSettings, IOptions<NotificationAppSettings> notificationAppSettingOptions)
            : base(logger, emailService, smsService, asNotificationService, notificationAppSettingOptions) {
            _asNotificationDS = asNotificationDS;
            _paymentNotificationRecipientDS = bizPaymentNotificationRecipientDS;
            _paymentAppSettings = paymentAppSettings.Value;
        }

        #endregion

        #region Override Methods

        /// <inheritdoc/>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                PaymentNotificationEventEnum parsedEvent;

                if(moduleId <= 0) {
                    valid = false;
                }

                if(Enum.TryParse<PaymentNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
                    valid = false;
                }

                if(eventInfo.Count <= 0) {
                    valid = false;
                }

                if(valid == false) {
                    throw new InvalidOperationException("Invalid notification arguments.");
                }
                else {
                    return valid;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.ValidateNotificationData:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <inheritdoc/>
        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {
            List<NotificationRecipient> recipients = new List<NotificationRecipient>();
            Dictionary<string, string> eventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;

            Guid appId = Guid.Empty;
            Guid businessTenantId = Guid.Empty;
            Guid businessPartnerTenantId = Guid.Empty;

            if(eventInfo.ContainsKey(NotificationConstants.AppIdKey))
                appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);

            if(eventInfo.ContainsKey(NotificationConstants.TenantIdKey))
                businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);

            if(eventInfo.ContainsKey(NotificationConstants.BusinessPartnerTenantIdKey))
                businessPartnerTenantId = Guid.Parse(eventInfo[NotificationConstants.BusinessPartnerTenantIdKey]);

            switch((PaymentNotificationEventEnum)payload.EventId) {
                case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                    recipients = _paymentNotificationRecipientDS.GetBusinessUserPaymentAppUsers(appId, businessTenantId);
                    break;
                case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                    recipients = _paymentNotificationRecipientDS.GetBusinessUserPaymentAppUsers(appId, businessTenantId);
                    break;
                case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                    recipients = _paymentNotificationRecipientDS.GetBusinessUserPaymentAppUsers(appId, businessTenantId);
                    break;
                case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                    recipients = _paymentNotificationRecipientDS.GetCustomerUserPaymentAppUsers(appId, businessTenantId, businessPartnerTenantId);
                    break;
                case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:  // add new method  
                    recipients = _paymentNotificationRecipientDS.GetCustomerUserPaymentAppUsers(appId, businessTenantId, businessPartnerTenantId);
                    break;
                case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
          recipients = _paymentNotificationRecipientDS.GetCustomerUserPaymentAppUsers(appId, businessTenantId, businessPartnerTenantId);
          break;

        case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                    recipients = _paymentNotificationRecipientDS.GetPaymentAppCustomerUserOnRefundForCust(appId, businessTenantId, businessPartnerTenantId);
                    break;

          

                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                    recipients = _paymentNotificationRecipientDS.GetAdvancePaymentAppCustomerUserOnPaymentForCust(appId, businessTenantId, businessPartnerTenantId, CustomerUserPaymentAppPreferenceEnum.AdvancePaymentIsInitiated);
                    break;
                case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                    recipients = _paymentNotificationRecipientDS.GetAdvancePaymentAppCustomerUserOnPaymentForCust(appId, businessTenantId, businessPartnerTenantId, CustomerUserPaymentAppPreferenceEnum.PreAuthorizationIsSecured);
                    break;
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                    recipients = _paymentNotificationRecipientDS.GetAdvanceAndPreAuthPaymentAppBusinessUserOnPaymentForBusiness(appId, businessTenantId, BusinessUserPaymentAppPreferenceEnum.AdvancePaymentIsInitiated);
                    //_loggerService.Log(LogLevel.Information, " Email recipients ");
                    //if(recipients != null) {
                    //    string logMsg = ((PaymentNotificationEventEnum)payload.EventId).ToString() +  " : " + recipients.Count.ToString();
                    //    logMsg += recipients.Count > 0 ? recipients[0].Email : "";
                    //    _loggerService.Log(LogLevel.Information, " Email recipients " + logMsg);
                    //}
                    break;
                case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                    recipients = _paymentNotificationRecipientDS.GetAdvanceAndPreAuthPaymentAppBusinessUserOnPaymentForBusiness(appId, businessTenantId, BusinessUserPaymentAppPreferenceEnum.PreAuthorizationIsSecured);
                    break;

                default:
                    break;
            }
            return recipients;
        }

        #endregion

        #region Email Methods

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            // Create list of values required to generate required notification from xsl template.
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();

            // Map common xsl parameters
            xslArguments.Add(NotificationConstants.RecipientFullNameKey, userDataRow.FullName);
            xslArguments.Add(NotificationConstants.EventNumberKey, Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));

            Dictionary<string, string> eventInfo = new Dictionary<string, string>();
            if(payload.EventInfo.ContainsKey(NotificationConstants.EventDataKey)) {
                try {
                    eventInfo = payload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>;

                    switch((PaymentNotificationEventEnum)payload.EventId) {
                        case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("newTransactionStatus", eventInfo["newTransactionStatus"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("oldTransactionStatus", eventInfo["oldTransactionStatus"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;
                        case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["customerID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("transactionStatus", eventInfo["transactionStatus"]);
                            xslArguments.Add("initiatedDate", eventInfo["initiatedDate"]);
                            xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                           // xslArguments.Add("customerCompanyID", eventInfo["customerID"]);

                            break;
                        case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            //xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("transactionStatus", eventInfo["transactionStatus"]);
                            xslArguments.Add("initiatedDate", eventInfo["initiatedDate"]);
                            xslArguments.Add("userName", eventInfo["businessUserName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["customerID"]);
                            break;
                        case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("transactionStatus", eventInfo["transactionStatus"]);
                            xslArguments.Add("initiatedDate", eventInfo["initiatedDate"]);
                            xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                           // xslArguments.Add("customerCompanyID", eventInfo["customerID"]);

                            break;
                        case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("newTransactionStatus", eventInfo["newTransactionStatus"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("oldTransactionStatus", eventInfo["oldTransactionStatus"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("paymentApplicationName", eventInfo["paymentApplicationName"]);
                            break;
                        case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("transactionStatus", eventInfo["transactionStatus"]);
                            xslArguments.Add("initiatedDate", eventInfo["initiatedDate"]);
                            xslArguments.Add("userName", eventInfo["businessUserName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("applicationName", eventInfo["paymentApplicationName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerID"]);
                            break;
                        case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                        case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionStatus", eventInfo["transactionStatus"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("actionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("userName", eventInfo["userName"]);

                            xslArguments.Add("accountNumber", eventInfo["accountNumber"]);
                            xslArguments.Add("transactionMode", eventInfo["transactionMode"]);
                            xslArguments.Add("transactionService", eventInfo["transactionService"]);
                            xslArguments.Add("customerId", eventInfo["customerId"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("userID", eventInfo["userID"]);

                            //paymentNotificationDTO.AccountNumber = paymentRelatedDataDTO.AccountNumber;
                            //paymentNotificationDTO.TransactionMode = paymentRelatedDataDTO.TransactionMode;
                            //paymentNotificationDTO.TransactionService = paymentRelatedDataDTO.TransactionService;
                            //paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
                            //paymentNotificationDTO.UserTypeText = ((UserTypeEnum)userSession.UserType).ToString();

                            break;
                        case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                        case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                        case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                        case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("transactionStatus", eventInfo["TransactionStatus"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("transactionDate", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("advanceAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("transactionService", eventInfo["transactionService"]);
                            xslArguments.Add("transactionMode", eventInfo["transactionMode"]);
                            xslArguments.Add("accountNumber", eventInfo["accountNumber"]);
                            xslArguments.Add("customerID", eventInfo["customerID"]);
                            xslArguments.Add("customerName", eventInfo["customerName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("userName", eventInfo["payeeName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerUserName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("customerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("payeeName", eventInfo["payeeName"]);
                            xslArguments.Add("dateTime", eventInfo["actionDate"]);
                            string appName1 = "";
                            if(eventInfo.TryGetValue("applicationName", out appName1)) {
                                xslArguments.Add("applicationName", appName1);
                            }
                            break;
                        case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                        case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("transactionID", eventInfo["transactionID"]);
                            xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("TransactionStatus", eventInfo["TransactionStatus"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("authorizedOn", eventInfo["actionDate"]);
                            xslArguments.Add("dateTime", eventInfo["actionDate"]);
                            xslArguments.Add("businessPartnerCompanyID", eventInfo["businessPartnerCompanyID"]);
                            xslArguments.Add("amount", eventInfo["transactionAmount"]);
                            xslArguments.Add("totalAuthorizedAmount", eventInfo["transactionAmount"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["businessPartnerCompanyName"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("userName", eventInfo["payeeName"]);
                            xslArguments.Add("cardNumber", eventInfo["accountNumber"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerID"]);
                            string appName = "";
                            if(eventInfo.TryGetValue("applicationName", out appName)) {
                                xslArguments.Add("applicationName", appName);
                            }
                            break;
                    }

                    return xslArguments;
                }
                catch(Exception ex) {
                    StringBuilder exceptionDetail = new StringBuilder();
                    exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetXMLArgumentsForEmail:-");
                    exceptionDetail.AppendLine();
                    exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                    _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                    throw;
                }
            }
            return xslArguments;
        }

        /// <inheritdoc/>
        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xslArguments) {
            EmailPayload emailPayload = new EmailPayload();
            try {
                emailPayload.EmailDeliverySubType = (int)EmailSubType.HTML;
                emailPayload.NotificationDeliveryType = (int)NotificationDeliveryType.Email;
                emailPayload.EventXMLData = eventXMLData;
                emailPayload.HasLinkError = hasLinkError;
                emailPayload.RecepientUserId = recepientDataRow.TenantUserId;
                emailPayload.TenantId = recepientDataRow.TenantId;
                emailPayload.UserEmailAddress = Convert.ToString(recepientDataRow.Email);

                if(notificationPayload.LoggedinUserId == emailPayload.RecepientUserId) {
                    emailPayload.IsLoggedInUser = true;
                }
                else {
                    emailPayload.IsLoggedInUser = false;
                }

                // Get user's local region language.
                if(string.IsNullOrEmpty(recepientDataRow.RegionLanguage)) {
                    emailPayload.UserLanguage = ewApps.Payment.Common.Constants.DefaultLanguage;
                }
                else {
                    emailPayload.UserLanguage = Convert.ToString(recepientDataRow.RegionLanguage);
                }

                emailPayload.NotificationInfo = new Dictionary<string, string>();
                emailPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                emailPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());

                emailPayload.XSLTArguments = xslArguments;

                #region XSL Email Template

                // Read xsl template file content as string.
                emailPayload.XSLTemplateContent = GetEmailXsltFile((PaymentNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
                emailPayload.InMemoryXSLTemplate = true;

                #endregion

                #region Email Deeplink

                ////ToDo:Nitin-Notification-This statement is not clear. Review this line.
                //emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);

                // If Condition: If email has url targetting to different client portal url.
                // Like invoice url target to 'Business Portal' for business user and target to 'Customer Portal' for customer user.
                if(notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists != null && notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Count > 0) {

                    // Get deeplink result set for current user type.
                    DeepLinkResultSetList deepLinkResultSetList = notificationPayload.DeeplinkResultSet.DeepLinkResultSetLists.Where(i => i.DeeplingResultKey == recepientDataRow.UserType.ToString()).First();
                    notificationPayload.DeeplinkResultSet.DeeplinkResults = deepLinkResultSetList.DeeplinkResults;

                    // Assign deeplink result i.e. deeplink url to email payload.
                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                }
                // Else Condition: If email has url targetting to single client portal url.
                else {

                    emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

                }

                #endregion

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        #region Private Methods

        // Read email notification xsl template file content as string.
        private string GetEmailXsltFile(PaymentNotificationEventEnum eventId, string regionLanguage) {
            //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
            //if (CacheHelper.IsInCache(cacheKey)) {
            //  return CacheHelper.GetData<string>(cacheKey); ;
            //}
            //else {
            //Engg: should define in Config file.
            // string xsltResourcePath = "/resourcePath/";

            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                string templatePath = Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(templatePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetEmailXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            // CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
            return xsltTextFile;
        }

        private DeeplinkPayload SetDeeplinkPayloadForEmail(string linkKey, Guid tenantId, Guid notificationId, Guid userId) {
            try {
                DeeplinkPayload dPayload = new DeeplinkPayload();
                dPayload.TenantId = tenantId;
                dPayload.NotificationId = notificationId;
                dPayload.LinkNotificationId = notificationId;
                Dictionary<string, string> linkInfo = new Dictionary<string, string>();
                linkInfo.Add("Actions", "notification,list");
                linkInfo.Add("RelativeURL", "notification/");
                linkInfo.Add("tenantid", tenantId.ToString());
                linkInfo.Add("recepientuserid", userId.ToString());
                dPayload.DeeplinkInfo = new Dictionary<string, Dictionary<string, string>>();
                dPayload.DeeplinkInfo.Add(linkKey, linkInfo);
                return dPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        // This method returns xsl file name that contains template name.
        private string GetXSLTemplateFileName(PaymentNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                    return "TransactionStatusChanged.xslt";
                case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                    return "TransactionVoidRequested.xslt";
                case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                    return "BizTransactionRefundRequested.xslt";
                case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                    return "CustTransactionVoidRequested.xslt";
                case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:
                    return "CustTransactionStatusChanged.xslt";
                case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                    return "CustTransactionRefundRequested.xslt";
                case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                    return "BizARInvoicePaymentInitiated.xslt";
                case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
                    return "CustARInvoicePaymentInitiated.xslt";
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                    return "BizAdvancePaymentSecuredForBusiness.xslt";
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                    return "BizAdvancePaymentSecuredForCustomer.xslt";
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                    return "CustAdvancePaymentSecuredForBusiness.xslt";
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                    return "CustAdvancePaymentSecuredForCustomer.xslt";
                case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                    return "BizPreAuthorizationInitiated.xslt";
                case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                    return "CustPreAuthorizationInitiated.xslt";
                default:
                    return "BusinessUserForgotPassword.xslt";
            }
        }

        #endregion

        #endregion

        #region SMS Methods

        /// <inheritdoc/>
        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xslArguments) {
            SMSPayload smsPayload = new SMSPayload();
            try {
                smsPayload.NotificationDeliveryType = (int)NotificationDeliveryType.ASNotification;
                smsPayload.EventXMLData = eventXMLData;
                smsPayload.HasLinkError = hasLinkError;
                smsPayload.RecepientUserId = recepientDataRow.TenantUserId;
                smsPayload.TenantId = recepientDataRow.TenantId;
                smsPayload.SMSRecipient = recepientDataRow.SMSRecipient;

                if(notificationPayload.LoggedinUserId == smsPayload.RecepientUserId) {
                    smsPayload.IsLoggedInUser = true;
                }
                else {
                    smsPayload.IsLoggedInUser = false;
                }

                // Get user's local region language.
                if(string.IsNullOrEmpty(recepientDataRow.RegionLanguage)) {
                    smsPayload.UserLanguage = Core.BaseService.Constants.DefaultLanguage;
                }
                else {
                    smsPayload.UserLanguage = Convert.ToString(recepientDataRow.RegionLanguage);
                }

                smsPayload.NotificationInfo = new Dictionary<string, string>();
                smsPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                smsPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());

                smsPayload.XSLTArguments = xslArguments;

                #region XSL Email Template

                // Read xsl template file content as string.
                smsPayload.XSLTemplateContent = GetASNotificationXsltFile((PaymentNotificationEventEnum)notificationPayload.EventId, smsPayload.UserLanguage);
                smsPayload.InMemoryXSLTemplate = true;

                #endregion

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetSMSPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return smsPayload;
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            try {
                switch((PaymentNotificationEventEnum)payload.EventId) {
                    default:
                        return GetXSLArgumentsForASNotification(userDataRow, payload);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetXSLArgumentsForSMS:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        private string GetSMSNotificationXsltFile(PaymentNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetSMSNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetSMSNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetSMSNotificationXSLTemplateFileName(PaymentNotificationEventEnum eventId) {
            switch(eventId) {
                default:
                    return GetASNotificationXSLTemplateFileName(eventId);
            }
        }

        #endregion

        #region AS-Methods

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForASNotification(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            Dictionary<string, string> xslArguments = new Dictionary<string, string>();
            // Map common xsl parameters
            xslArguments.Add(NotificationConstants.RecipientFullNameKey, userDataRow.FullName);
            xslArguments.Add(NotificationConstants.EventNumberKey, Convert.ToString(payload.EventId));
            xslArguments.Add("userType", Convert.ToString(userDataRow.UserType));
            Dictionary<string, string> eventInfo = payload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>;
            try {
                switch((PaymentNotificationEventEnum)payload.EventId) {
                    case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        xslArguments.Add("newTransactionStatus", eventInfo["newTransactionStatus"]);
                        xslArguments.Add("oldTransactionStatus", eventInfo["oldTransactionStatus"]);
                        break;
                    case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        break;
                    case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        break;
                    case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        break;
                    case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        xslArguments.Add("newTransactionStatus", eventInfo["newTransactionStatus"]);
                        xslArguments.Add("oldTransactionStatus", eventInfo["oldTransactionStatus"]);
                        break;
                    case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                    case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                    case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["userName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        break;
                    case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                    case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                    case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        xslArguments.Add("payeeName", eventInfo["payeeName"]);
                        break;
                    case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                    case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                    case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                        xslArguments.Add("transactionID", eventInfo["transactionID"]);
                        xslArguments.Add("transactionAmount", eventInfo["transactionAmount"]);
                        xslArguments.Add("businessPartnerCompanyName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        xslArguments.Add("actionDate", eventInfo["actionDate"]);
                        xslArguments.Add("customerUserName", eventInfo["businessPartnerCompanyName"]);
                        xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        xslArguments.Add("payeeName", eventInfo["payeeName"]);

                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetXSLArgumentsForASNotification:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xslArguments;

        }

        /// <inheritdoc/>
        public override ASNotificationPayload GetASNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, Dictionary<string, string> xslArguments) {
            ASNotificationPayload asNotificationPayload = new ASNotificationPayload();

            try {
                if(notificationPayload != null && (notificationPayload.SupportedDeliveryType & NotificationDeliveryType.ASNotification) == NotificationDeliveryType.ASNotification) {

                    Dictionary<string, string> eventData = (notificationPayload.EventInfo[NotificationConstants.EventDataKey] as Dictionary<string, string>);

                    asNotificationPayload.TenantId = recepientDataRow.TenantId;
                    asNotificationPayload.RecepientUserId = recepientDataRow.TenantUserId;
                    asNotificationPayload.EventId = notificationPayload.EventId;
                    //  asNotificationPayload.AppId = Guid.Parse(notificationPayload.EventInfo[NotificationConstants.AppIdKey].ToString());
                    asNotificationPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
                    asNotificationPayload.EventXMLData = eventXMLData;
                    asNotificationPayload.XSLTArguments = xslArguments;
                    asNotificationPayload.EntityType = Convert.ToInt32(eventData["asTargetEntityType"]);
                    asNotificationPayload.EntityId = Guid.Parse(eventData["asTargetEntityId"]);
                    asNotificationPayload.AppId = Guid.Parse(eventData["asAppId"]);
                    asNotificationPayload.ASNotificationType = notificationPayload.EventId;
                    if(eventData.ContainsKey(NotificationConstants.ASAdditionalInfoKey)) {
                        asNotificationPayload.AdditionalInfo = eventData[NotificationConstants.ASAdditionalInfoKey];
                    }

                    if(notificationPayload.LoggedinUserId == asNotificationPayload.RecepientUserId) {
                        asNotificationPayload.IsLoggedInUser = true;
                    }
                    else {
                        asNotificationPayload.IsLoggedInUser = false;
                    }

                    string language = string.Empty;

                    // Get user's local region language.
                    if(string.IsNullOrEmpty(recepientDataRow.RegionLanguage)) {
                        language = Core.BaseService.Constants.DefaultLanguage;
                    }
                    else {
                        language = Convert.ToString(recepientDataRow.RegionLanguage);
                    }

                    asNotificationPayload.UserLanguage = language;

                    asNotificationPayload.NotificationInfo = new Dictionary<string, string>();
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.TrackingIdKey, notificationPayload.TrackingId.ToString());
                    asNotificationPayload.NotificationInfo.Add(NotificationConstants.LinkNotificationIdKey, notificationPayload.LinkedNotificationId.ToString());
                    asNotificationPayload.NotificationDeliveryType = (int)Core.NotificationService.NotificationDeliveryType.ASNotification;

                    #region XSL AS-Notification Template

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((PaymentNotificationEventEnum)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetASNotificationPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return asNotificationPayload;
        }

        /// <inheritdoc/>
        public override bool AddASNotification(ASNotificationDTO aSNotificationDTO, NotificationPayload<NotificationRecipient> notificationPayload) {
            ASNotification aSNotification = new ASNotification();
            aSNotification.AppId = aSNotificationDTO.AppId;
            aSNotification.EntityId = aSNotificationDTO.SourceEntityId;
            aSNotification.EntityType = aSNotificationDTO.SourceEntityType;
            aSNotification.HtmlContent = aSNotificationDTO.HtmlContent;
            aSNotification.LinkNotificationId = aSNotificationDTO.LinkNotificationId;
            aSNotification.LogType = aSNotificationDTO.ASNotificationType;
            aSNotification.MetaData = aSNotificationDTO.MetaData;
            aSNotification.ReadState = false;
            aSNotification.RecipientTenantUserId = aSNotificationDTO.RecipientUserId;
            aSNotification.TenantId = aSNotificationDTO.TenantId;
            aSNotification.TextContent = aSNotificationDTO.TextContent;
            aSNotification.AdditionalInfo = aSNotificationDTO.AdditionalInfo;
            _asNotificationDS.UpdateSystemFieldsByOpType(aSNotification, OperationType.Add);
            _asNotificationDS.Add(aSNotification);
            _asNotificationDS.Save();
            return true;
        }

        private string GetASNotificationXsltFile(PaymentNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(PaymentNotificationEventEnum eventId) {
            switch(eventId) {

                case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                    return "BizARInvoicePay.xslt";
                case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
                    return "CustARInvoicePay.xslt";
                case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                    return "TransactionStatusChanged.xslt";
                case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                    return "TransactionVoidRequested.xslt";
                case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                    return "TransactionRefundRequested.xslt";
                case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                    return "CustTransactionVoidRequested.xslt";
                case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:
                    return "CustTransactionStatusChanged.xslt";
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                    return "BizAdvancePaymentSecuredForBusiness.xslt";
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                    return "BizAdvancePaymentSecuredForCustomer.xslt";
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                    return "CustAdvancePaymentSecuredForBusiness.xslt";
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                    return "CustAdvancePaymentSecuredForCustomer.xslt";
                case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                    return "BizPreAuthorizationInitiated.xslt";
                case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                    return "CustPreAuthorizationInitiated.xslt";
                default:
                    return "";
            }
        }

        #endregion

        #region Deeplink Methods

        /// <inheritdoc/>
        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {
            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();
            try {
                if(notificationPayload != null) {
                    // Map properties from notification payload to deeplink payload.
                    deeplinkPayload.ModuleId = notificationPayload.ModuleId;
                    deeplinkPayload.EventId = notificationPayload.EventId;
                    deeplinkPayload.LoggedinUserId = notificationPayload.LoggedinUserId;
                    deeplinkPayload.NotificationId = notificationPayload.NotificationId;
                    deeplinkPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;


                    if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                        Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();
                        // As each action is bound to single event so decision parameter should be event id.
                        string branchKey = Convert.ToString(GetBranchKey(notificationPayload.EventId));

                        // Get deeplink actions for particular notification.
                        // If any email contains more than one deeplink, returned list should contains each deeplink information.
                        switch((PaymentNotificationEventEnum)notificationPayload.EventId) {

                            // Here We didn't fill DeeplinkInfoList because email should contains all url target to single
                            // application.
                        }
                    }
                }
                return deeplinkPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        /// <inheritdoc/>
        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {
            // Note: This method is not required.
            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
            return deeplinkresult;
        }

        /// <inheritdoc/>
        protected override object GetBranchKey(object decisionParameters) {
            try {
                switch((PaymentNotificationEventEnum)decisionParameters) {
                    default:
                        return "";
                        // _appSettingsOption.Value.PublisherBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #region Private Methods

        private Dictionary<string, Dictionary<string, string>> GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            // This object contains multiple entries correspond to every url.
            Dictionary<string, Dictionary<string, string>> deeplinkList = new Dictionary<string, Dictionary<string, string>>();
            try {
                // Get event data from notification payload.
                Dictionary<string, string> notificationPayloadEventData = (Dictionary<string, string>)(notificationPayload.EventInfo[NotificationConstants.DeeplinkInfoKey]);

                // Create deeplink information dictionary to be fill from event data. 
                // This dictionary contain Action, Parameters and BranchKey information.
                Dictionary<string, string> invoiceDeeplinkInformation = new Dictionary<string, string>();

                // Actions values required by branch.
                string action = "account,invite,platformuser,new";
                invoiceDeeplinkInformation.Add("Actions", action);
                invoiceDeeplinkInformation.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                // Set Dynamic info from Notification payload.
                invoiceDeeplinkInformation.Add(NotificationConstants.TenantIdKey, notificationPayloadEventData[NotificationConstants.TenantIdKey]);
                invoiceDeeplinkInformation.Add(NotificationConstants.AppUserIdKey, notificationPayloadEventData[NotificationConstants.AppUserIdKey]);

                deeplinkList.Add("invoiceLink", invoiceDeeplinkInformation);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in PaymentNotificationService.GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }

            return deeplinkList;
        }

        #endregion

        #endregion

        #region Other Methods

        /// <inheritdoc/>
        public override NotificationDeliveryType GetSupportedNotificationType(long platformNotificationEvents) {
            switch((PaymentNotificationEventEnum)platformNotificationEvents) {
                case PaymentNotificationEventEnum.BizARInvoicePaymentInitiated:
                case PaymentNotificationEventEnum.CustARInvoicePaymentInitiated:
                case PaymentNotificationEventEnum.CustTransactionRefundRequested:
                case PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated:
                case PaymentNotificationEventEnum.CustTransactionVoidRequested:
                case PaymentNotificationEventEnum.BizTransactionVoidRequested:
                case PaymentNotificationEventEnum.BizTransactionRefundRequested:
                case PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated:
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness:
                case PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer:
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness:
                case PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer:
                case PaymentNotificationEventEnum.BizPreAuthorizationInitiated:
                case PaymentNotificationEventEnum.CustPreAuthorizationInitiated:
                    return NotificationDeliveryType.ASNotification | NotificationDeliveryType.Email | NotificationDeliveryType.SMS;
                default:
                    return NotificationDeliveryType.None;
            }
        }

        /// <inheritdoc/>
        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void GenerateASNotification(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, ASNotificationDTO aSNotificationDTO) {
            base.GenerateASNotification(notificationPayload, recepientDataRow, aSNotificationDTO);
        }

        /// <inheritdoc/>
        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
