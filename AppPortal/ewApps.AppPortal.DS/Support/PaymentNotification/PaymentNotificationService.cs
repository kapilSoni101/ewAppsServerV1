///* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
// * Unauthorized copying of this file, via any medium is strictly prohibited
// * Proprietary and confidential
// * 
// * Author: Nitin Jain <njain@eworkplaceapps.com>
// * Date: 24 September 2018
// * 
// * Contributor/s: Ishwar Rathore
// * Last Updated On: 10 October 2018
// */

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using AutoMapper;
//using ewApps.Core.CommonService;
//using ewApps.Core.DeeplinkServices;
//using ewApps.Core.EmailService;
//using ewApps.Core.NotificationService;
//using ewApps.Core.SMSService;
//using ewApps.Payment.DS;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace ewApps.AppPortal.DS {
//    /// <summary>Implementing all the Payment Notification methods.</summary>
//    public class PaymentNotificationService:NotificationService<NotificationRecipient>, IPaymentNotificationService {

//        #region Local member

//        IPaymentBusinessNotificationRecipientDS _notificationRecipientDS;
//        IOptions<NotificationAppSettings> _appSettingsOption;
//        IMapper _mapper;

//        #endregion Local member

//        #region Constructor

//        /// <summary>
//        /// Initilize local members.
//        /// </summary>
//        public PaymentNotificationService(IMapper mapper, IEmailService emailService, ISMSService smsService, IPaymentBusinessNotificationRecipientDS notificationRecipientDS, IOptions<NotificationAppSettings> appSettingsOption, ILogger<NotificationService<NotificationRecipient>> loggerService) : base(loggerService, emailService, smsService) {
//            _notificationRecipientDS = notificationRecipientDS;
//            _appSettingsOption = appSettingsOption;
//            _mapper = mapper;
//            base._appSettings = _mapper.Map<IOptions<NotificationAppSettings>>(_appSettingsOption).Value;
//        }

//        #endregion Constructor

//        /// <summary>
//        /// Generates Deeplink payload by the data given in notification payload.
//        /// </summary>
//        public override DeeplinkPayload GetDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload) {

//            DeeplinkPayload deeplinkPayload = new DeeplinkPayload();

//            // Set all common fields for all the deeplink 
//            if(notificationPayload != null) {
//                deeplinkPayload.EventId = notificationPayload.EventId;
//                deeplinkPayload.ModuleId = notificationPayload.ModuleId;
//                deeplinkPayload.NotificationId = notificationPayload.NotificationId;
//                deeplinkPayload.LinkNotificationId = notificationPayload.LinkedNotificationId;
//                deeplinkPayload.LoggedinUserId = notificationPayload.LoggedinUserId;
//            };

//            // Set Deeplink specific data
//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                switch((PaymentNotificationEvent)deeplinkPayload.EventId) {
//                    case PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail:
//                        deeplinkPayload.DeeplinkInfo = GetBusinessUserInviteDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail);
//                        break;
//                    case PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail:
//                        deeplinkPayload.DeeplinkInfo = GetPrimaryPartnerUserInviteDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail);
//                        break;
//                    case PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail:
//                        deeplinkPayload.DeeplinkInfo = GetPartnerUserInviteDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail);
//                        break;
//                    case PaymentNotificationEvent.BusinessForgotPasswordEmail:
//                        deeplinkPayload.DeeplinkInfo = GetBusinessUserForgotPasswordDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.BusinessForgotPasswordEmail);
//                        break;
//                    case PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail:
//                        deeplinkPayload.DeeplinkInfo = GetBusinessPartnerUserForgotPasswordDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail);
//                        break;
//                    case PaymentNotificationEvent.InvoiceAdded:
//                        deeplinkPayload.DeeplinkInfo = GetInvoiceAddedDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.InvoiceAdded);
//                        break;
//                    case PaymentNotificationEvent.InvoiceDeleted:
//                        deeplinkPayload.DeeplinkInfo = GetInvoiceDeletedDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.InvoiceDeleted);
//                        break;
//                    case PaymentNotificationEvent.PaymentDone:
//                        deeplinkPayload.DeeplinkInfo = GetPaymentDoneDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.PaymentDone);
//                        break;
//                    case PaymentNotificationEvent.PaymentVoid:
//                        deeplinkPayload.DeeplinkInfo = GetPaymentVoidDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.PaymentVoid);
//                        break;
//                    case PaymentNotificationEvent.PaymentRefund:
//                        deeplinkPayload.DeeplinkInfo = GetPaymentRefundDeeplinkPayload(notificationPayload, (long)PaymentNotificationEvent.PaymentRefund);
//                        break;
//                    case PaymentNotificationEvent.SystemCancelRefundForBusiness:
//                        deeplinkPayload.DeeplinkInfo = GetSystemCancelRefundDeeplinkPayloadBusiness(notificationPayload, (long)PaymentNotificationEvent.SystemCancelRefundForBusiness);
//                        break;
//                    case PaymentNotificationEvent.SystemCancelRefundForBusinessPartner:
//                        deeplinkPayload.DeeplinkInfo = GetSystemCancelRefundDeeplinkPayloadBusinessPartner(notificationPayload, (long)PaymentNotificationEvent.SystemCancelRefundForBusinessPartner);
//                        break;
//                }
//            }
//            return deeplinkPayload;
//        }

//        /// <summary>
//        /// Generates Push payload by the data given in notification payload and USerDataRow.
//        /// </summary>
//        /// <param name="notificationPayload"></param>
//        /// <param name="recepientRow"></param>
//        /// <param name="eventXMLData"></param>
//        /// <param name="hasLinkError"></param>
//        /// <param name="xmlArguments"></param>
//        /// <returns></returns>
//        public override EmailPayload GetEmailPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {

//            EmailPayload emailPayload = new EmailPayload();

//            //Set all common fields for all the deeplink 
//            if(notificationPayload != null) {
//                emailPayload.TenantId = new Guid(Convert.ToString(recepientRow.TenantId));
//                emailPayload.RecepientUserId = new Guid(Convert.ToString(recepientRow.TenantUserId));
//                emailPayload.HasLinkError = hasLinkError;
//                string language = string.Empty;

//                if(notificationPayload.LoggedinUserId == emailPayload.RecepientUserId) {
//                    emailPayload.IsLoggedInUser = true;
//                }
//                else {
//                    emailPayload.IsLoggedInUser = false;
//                }

//                //// Get user's local region language.
//                //if(string.IsNullOrEmpty(recepientRow.RegionLanguage)) {
//                //    language = "en";
//                //}
//                //else {
//                //    language = Convert.ToString(recepientRow.RegionLanguage);
//                //}
//                language = "en";
//                emailPayload.UserLanguage = language;
//                emailPayload.UserEmailAddress = Convert.ToString(recepientRow.Email);
//                emailPayload.EventXMLData = eventXMLData;
//                emailPayload.XSLTArguments = xmlArguments;
//                emailPayload.AppType = (int)ewApps.Core.Common.CoreEntityTypeEnum.App;
//                emailPayload.NotificationInfo = new Dictionary<string, string>();
//                emailPayload.NotificationInfo.Add("TrackingId", notificationPayload.TrackingId.ToString());
//                emailPayload.NotificationInfo.Add("LinkNotificationId", notificationPayload.LinkedNotificationId.ToString());

//                #region XSL Email Template

//                string xslFileContent = GetEmailXsltFile((PaymentNotificationEvent)notificationPayload.EventId, language);
//                emailPayload.InMemoryXSLTemplate = true;
//                emailPayload.XSLTemplateContent = xslFileContent;

//                #endregion

//                #region  email Deeplink
//                //ToDo:Nitin-Notification-This statement is not clear.DeeplinkPayload
//                emailPayload.DeeplinkPayload = SetDeeplinkPayloadForEmail("deeplink1", emailPayload.TenantId, notificationPayload.NotificationId, emailPayload.RecepientUserId);
//                emailPayload.DeeplinkResultSet = notificationPayload.DeeplinkResultSet;

//                #endregion
//            }
//            return emailPayload;
//        }

//        /// <summary></summary>
//        /// <param name="notificationPayload"></param>
//        /// <param name="deeplinkResultSet"></param>
//        public override Tuple<string, bool> SetDeeplinkResultInNotificationPayload(NotificationPayload<NotificationRecipient> notificationPayload, DeeplinkResultSet deeplinkResultSet) {

//            //string deeplinkJSON = deeplinkResultSet.DeeplinkResults["deeplink1"].DeeplinkJson;
//            bool linkError = deeplinkResultSet.DeeplinkResults.Any(i => i.Value.HasLinkError == true);
//            Tuple<string, bool> deeplinkresult = new Tuple<string, bool>("", linkError);
//            notificationPayload.DeeplinkResultSet = deeplinkResultSet;
//            return deeplinkresult;
//        }

//        /// <summary></summary>
//        /// <param name="userDataRow"></param>
//        /// <param name="payload"></param>
//        protected override int AddBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
//            throw new NotImplementedException();
//        }


//        protected override Core.NotificationService.NotificationLogOpDTO GenerateASNotificationPayload(NotificationPayload<NotificationRecipient> payload, NotificationRecipient recepientDataRow, DateTime deliveryTime, long syncRowId, int badgeCount, string XMLData) {
//            throw new NotImplementedException();
//        }

//        /// <summary></summary>
//        /// <param name="userDataRow"></param>
//        /// <param name="payload"></param>
//        protected override int GetBadgeCount(NotificationPayload<NotificationRecipient> payload, NotificationRecipient userDataRow) {
//            throw new NotImplementedException();
//        }

//        /// <summary>Get recepiant list</summary>
//        /// <param name="payload">Notification payload</param>
//        protected override List<NotificationRecipient> GetRecipientList(NotificationPayload<NotificationRecipient> payload) {
//            Dictionary<string, string> EventInfo = payload.EventInfo["EventData"] as Dictionary<string, string>;
//            Dictionary<string, string> EventInfo1 = payload.EventInfo["DeeplinkInfo"] as Dictionary<string, string>;
//            switch((PaymentNotificationEvent)payload.EventId) {
//                //case PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail:
//                //    return _notificationRecipientDS.GetPartnerUserRecipientList(new Guid(EventInfo1["tenantid"]), new Guid(EventInfo1["appuserid"]), new Guid(EventInfo["appid"]));
//                //case PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail:
//                //    return _notificationRecipientDS.GetPartnerUserRecipientList(new Guid(EventInfo1["tenantid"]), new Guid(EventInfo1["appuserid"]), new Guid(EventInfo["appid"]));
//                //case PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail:
//                //    return _notificationRecipientDS.GetPartnerUserRecipientList(new Guid(EventInfo1["tenantid"]), new Guid(EventInfo1["appuserid"]), new Guid(EventInfo["appid"]));
//                //case PaymentNotificationEvent.BusinessForgotPasswordEmail:
//                //    return _notificationRecipientDS.GetPartnerUserRecipientList(new Guid(EventInfo1["tenantid"]), new Guid(EventInfo1["appuserid"]), new Guid(EventInfo["appid"]));
//                //case PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail:
//                //    return _notificationRecipientDS.GetPartnerUserRecipientList(new Guid(EventInfo1["tenantid"]), new Guid(EventInfo1["appuserid"]), new Guid(EventInfo["appid"]));
//                //case PaymentNotificationEvent.InvoiceAdded:
//                   // return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.InvoiceAdded);
//                case PaymentNotificationEvent.InvoiceDeleted:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.InvoiceDeleted);
//                case PaymentNotificationEvent.PaymentDone:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.PaymentDone);
//                case PaymentNotificationEvent.PaymentVoid:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.PaymentVoid);
//                case PaymentNotificationEvent.PaymentRefund:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.PaymentRefund);
//                case PaymentNotificationEvent.SystemCancelRefundForBusiness:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.SystemCancelRefundForBusiness);
//                case PaymentNotificationEvent.SystemCancelRefundForBusinessPartner:
//                    return _notificationRecipientDS.GetInvoicePermissionUserNotificationList(new Guid(EventInfo["tenantid"]), new Guid(EventInfo["appid"]), new Guid(EventInfo["parentRefId"]), (long)PaymentNotificationEvent.SystemCancelRefundForBusinessPartner);

//                default:
//                    return null;
//            }
//        }

//        /// <summary></summary>
//        /// <param name="payload"></param>
//        protected override long GetSyncRowId(NotificationPayload<NotificationRecipient> payload) {
//            throw new NotImplementedException();
//        }

//        /// <summary>Get email xml argument based on event identifier</summary>
//        /// <param name="userDataRow"></param>
//        /// <param name="payload"></param><NotificationRecipient>
//        protected override Dictionary<string, string> GetXMLArgumentsForEmail(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
//            Dictionary<string, string> xslArguments = new Dictionary<string, string>();

//            #region Common Region

//            xslArguments.Add("RecipientFullName", userDataRow.FullName);
//            xslArguments.Add("EventNumber", Convert.ToString(payload.EventId));

//            #endregion

//            switch((PaymentNotificationEvent)payload.EventId) {
//                case PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail:
//                    Dictionary<string, string> eventInformationBusinessUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("partnerUserName", eventInformationBusinessUser["partnerUserName"]);
//                    xslArguments.Add("appName", eventInformationBusinessUser["appName"]);
//                    xslArguments.Add("hostName", eventInformationBusinessUser["hostName"]);
//                    xslArguments.Add("hostCompany", eventInformationBusinessUser["hostCompany"]);
//                    xslArguments.Add("invitedApplicationName", eventInformationBusinessUser["invitedApplicationName"]);
//                    xslArguments.Add("publisherName", eventInformationBusinessUser["publisherName"]);
//                    break;
//                case PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail:
//                    Dictionary<string, string> eventInformationPartnerPrimaryUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("partnerUserName", eventInformationPartnerPrimaryUser["partnerUserName"]);
//                    xslArguments.Add("appName", eventInformationPartnerPrimaryUser["appName"]);
//                    xslArguments.Add("hostName", eventInformationPartnerPrimaryUser["hostName"]);
//                    xslArguments.Add("inviteeCompany", eventInformationPartnerPrimaryUser["inviteeCompany"]);
//                    xslArguments.Add("invitedApplicationName", eventInformationPartnerPrimaryUser["invitedApplicationName"]);
//                    xslArguments.Add("publisherName", eventInformationPartnerPrimaryUser["publisherName"]);
//                    break;
//                case PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail:
//                    Dictionary<string, string> eventInformationPartnerUser = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("partnerUserName", eventInformationPartnerUser["partnerUserName"]);
//                    xslArguments.Add("appName", eventInformationPartnerUser["appName"]);
//                    xslArguments.Add("hostName", eventInformationPartnerUser["hostName"]);
//                    xslArguments.Add("hostCompany", eventInformationPartnerUser["hostCompany"]);
//                    xslArguments.Add("invitedApplicationName", eventInformationPartnerUser["invitedApplicationName"]);
//                    xslArguments.Add("publisherName", eventInformationPartnerUser["publisherName"]);
//                    break;
//                case PaymentNotificationEvent.BusinessForgotPasswordEmail:
//                    Dictionary<string, string> businessUserForgotPassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("applicationName", businessUserForgotPassword["applicationName"]);
//                    xslArguments.Add("publisherName", businessUserForgotPassword["publisherName"]);
//                    xslArguments.Add("portalUserName", businessUserForgotPassword["portalUserName"]);
//                    break;
//                case PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail:
//                    Dictionary<string, string> businessPartnerUserForgotPassword = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("applicationName", businessPartnerUserForgotPassword["applicationName"]);
//                    xslArguments.Add("publisherName", businessPartnerUserForgotPassword["publisherName"]);
//                    xslArguments.Add("portalUserName", businessPartnerUserForgotPassword["portalUserName"]);
//                    break;
//                case PaymentNotificationEvent.InvoiceAdded:
//                    Dictionary<string, string> addInvoice = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", addInvoice["publisherName"]);
//                    xslArguments.Add("applicationName", addInvoice["applicationName"]);
//                    xslArguments.Add("invoiceId", addInvoice["invoiceId"]);
//                    xslArguments.Add("companyName", addInvoice["companyName"]);
//                    xslArguments.Add("customerName", addInvoice["customerName"]);
//                    xslArguments.Add("invoiceName", addInvoice["invoiceName"]);
//                    xslArguments.Add("createdOn", addInvoice["createdOn"]);
//                    xslArguments.Add("dueOn", addInvoice["dueOn"]);
//                    xslArguments.Add("originalAmount", addInvoice["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", addInvoice["invoiceStatus"]);
//                    xslArguments.Add("outstandingAmount", addInvoice["outstandingAmount"]);
//                    break;
//                case PaymentNotificationEvent.InvoiceDeleted:
//                    Dictionary<string, string> invoiceDeleted = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", invoiceDeleted["publisherName"]);
//                    xslArguments.Add("applicationName", invoiceDeleted["applicationName"]);
//                    xslArguments.Add("invoiceId", invoiceDeleted["invoiceId"]);
//                    xslArguments.Add("companyName", invoiceDeleted["companyName"]);
//                    xslArguments.Add("customerName", invoiceDeleted["customerName"]);
//                    xslArguments.Add("actionDate", invoiceDeleted["actionDate"]);
//                    xslArguments.Add("invoiceName", invoiceDeleted["invoiceName"]);
//                    xslArguments.Add("createdOn", invoiceDeleted["createdOn"]);
//                    xslArguments.Add("dueOn", invoiceDeleted["dueOn"]);
//                    xslArguments.Add("originalAmount", invoiceDeleted["originalAmount"]);
//                    break;
//                case PaymentNotificationEvent.PaymentDone:
//                    Dictionary<string, string> paymentDone = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", paymentDone["publisherName"]);
//                    xslArguments.Add("applicationName", paymentDone["applicationName"]);
//                    xslArguments.Add("invoiceId", paymentDone["invoiceId"]);
//                    xslArguments.Add("companyName", paymentDone["companyName"]);
//                    xslArguments.Add("customerName", paymentDone["customerName"]);
//                    xslArguments.Add("paymentAmount", paymentDone["paymentAmount"]);
//                    xslArguments.Add("actionDate", paymentDone["actionDate"]);
//                    xslArguments.Add("invoiceName", paymentDone["invoiceName"]);
//                    xslArguments.Add("createdOn", paymentDone["createdOn"]);
//                    xslArguments.Add("dueOn", paymentDone["dueOn"]);
//                    xslArguments.Add("originalAmount", paymentDone["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", paymentDone["invoiceStatus"]);
//                    xslArguments.Add("outstandingAmount", paymentDone["outstandingAmount"]);
//                    break;
//                case PaymentNotificationEvent.PaymentVoid:
//                    Dictionary<string, string> paymentVoid = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", paymentVoid["publisherName"]);
//                    xslArguments.Add("applicationName", paymentVoid["applicationName"]);
//                    xslArguments.Add("invoiceId", paymentVoid["invoiceId"]);
//                    xslArguments.Add("companyName", paymentVoid["companyName"]);
//                    xslArguments.Add("customerName", paymentVoid["customerName"]);
//                    xslArguments.Add("actionDate", paymentVoid["actionDate"]);
//                    xslArguments.Add("paymentId", paymentVoid["paymentId"]);
//                    xslArguments.Add("paymentOn", paymentVoid["paymentOn"]);
//                    xslArguments.Add("paymentAmount", paymentVoid["paymentAmount"]);
//                    xslArguments.Add("invoiceName", paymentVoid["invoiceName"]);
//                    xslArguments.Add("originalAmount", paymentVoid["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", paymentVoid["invoiceStatus"]);
//                    break;
//                case PaymentNotificationEvent.PaymentRefund:
//                    Dictionary<string, string> paymentRefund = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", paymentRefund["publisherName"]);
//                    xslArguments.Add("applicationName", paymentRefund["applicationName"]);
//                    xslArguments.Add("invoiceId", paymentRefund["invoiceId"]);
//                    xslArguments.Add("companyName", paymentRefund["companyName"]);
//                    xslArguments.Add("customerName", paymentRefund["customerName"]);
//                    xslArguments.Add("actionDate", paymentRefund["actionDate"]);
//                    xslArguments.Add("paymentId", paymentRefund["paymentId"]);
//                    xslArguments.Add("paymentOn", paymentRefund["paymentOn"]);
//                    xslArguments.Add("paymentAmount", paymentRefund["paymentAmount"]);
//                    xslArguments.Add("invoiceName", paymentRefund["invoiceName"]);
//                    xslArguments.Add("originalAmount", paymentRefund["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", paymentRefund["invoiceStatus"]);
//                    break;
//                case PaymentNotificationEvent.SystemCancelRefundForBusiness:
//                    Dictionary<string, string> systemCancelRefund = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", systemCancelRefund["publisherName"]);
//                    xslArguments.Add("applicationName", systemCancelRefund["applicationName"]);
//                    xslArguments.Add("invoiceId", systemCancelRefund["invoiceId"]);
//                    xslArguments.Add("companyName", systemCancelRefund["companyName"]);
//                    xslArguments.Add("customerName", systemCancelRefund["customerName"]);
//                    xslArguments.Add("actionDate", systemCancelRefund["actionDate"]);
//                    xslArguments.Add("paymentId", systemCancelRefund["paymentId"]);
//                    xslArguments.Add("paymentOn", systemCancelRefund["paymentOn"]);
//                    xslArguments.Add("paymentAmount", systemCancelRefund["paymentAmount"]);
//                    xslArguments.Add("invoiceName", systemCancelRefund["invoiceName"]);
//                    xslArguments.Add("originalAmount", systemCancelRefund["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", systemCancelRefund["invoiceStatus"]);
//                    break;
//                case PaymentNotificationEvent.SystemCancelRefundForBusinessPartner:
//                    Dictionary<string, string> systemCancelRefundForBusinessPartner = (Dictionary<string, string>)payload.EventInfo["EventData"];
//                    xslArguments.Add("publisherName", systemCancelRefundForBusinessPartner["publisherName"]);
//                    xslArguments.Add("applicationName", systemCancelRefundForBusinessPartner["applicationName"]);
//                    xslArguments.Add("invoiceId", systemCancelRefundForBusinessPartner["invoiceId"]);
//                    xslArguments.Add("companyName", systemCancelRefundForBusinessPartner["companyName"]);
//                    xslArguments.Add("customerName", systemCancelRefundForBusinessPartner["customerName"]);
//                    xslArguments.Add("actionDate", systemCancelRefundForBusinessPartner["actionDate"]);
//                    xslArguments.Add("paymentId", systemCancelRefundForBusinessPartner["paymentId"]);
//                    xslArguments.Add("paymentOn", systemCancelRefundForBusinessPartner["paymentOn"]);
//                    xslArguments.Add("paymentAmount", systemCancelRefundForBusinessPartner["paymentAmount"]);
//                    xslArguments.Add("invoiceName", systemCancelRefundForBusinessPartner["invoiceName"]);
//                    xslArguments.Add("originalAmount", systemCancelRefundForBusinessPartner["originalAmount"]);
//                    xslArguments.Add("invoiceStatus", systemCancelRefundForBusinessPartner["invoiceStatus"]);
//                    break;
//                default:
//                    break;
//            }

//            return xslArguments;
//        }

//        /// <summary></summary>
//        /// <param name="userDataRow"></param>
//        /// <param name="payload"></param>
//        protected override Dictionary<string, string> GetXMLArgumentsForPush(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
//            throw new NotImplementedException();
//        }

//        /// <summary>Validation data about notification</summary>
//        /// <param name="moduleId"></param>
//        /// <param name="eventId"></param>
//        /// <param name="eventInfo"></param>
//        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
//            bool valid = true;
//            PaymentNotificationEvent parsedEvent;
//            if(moduleId <= 0) {
//                valid = false;
//            }
//            if(Enum.TryParse<PaymentNotificationEvent>(eventId.ToString(), out parsedEvent) == false) {
//                valid = false;
//            }
//            if(eventInfo.Count <= 0) {
//                valid = false;
//            }
//            if(valid == false) {
//                throw new InvalidOperationException("Invalid notification arguments.");
//            }
//            else {
//                return valid;
//            }
//        }

//        /// <summary>Get branch key on the basis of event type</summary>
//        /// <param name="eventId">Event identitfier</param>
//        protected override object GetBranchKey(object eventId) {
//            long eventLong = long.Parse(eventId.ToString());
//            switch(eventLong) {
//                case (long)PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.BusinessForgotPasswordEmail:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.InvoiceAdded:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                case (long)PaymentNotificationEvent.InvoiceDeleted:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                case (long)PaymentNotificationEvent.PaymentDone:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                case (long)PaymentNotificationEvent.PaymentVoid:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                case (long)PaymentNotificationEvent.PaymentRefund:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                case (long)PaymentNotificationEvent.SystemCancelRefundForBusiness:
//                    return _appSettingsOption.Value.BusinessBranchApiKey;
//                case (long)PaymentNotificationEvent.SystemCancelRefundForBusinessPartner:
//                    return _appSettingsOption.Value.PaymentBranchApiKey;
//                default:
//                    return "";
//            }
//        }

//        #region Private Methods


//        private DeeplinkPayload SetDeeplinkPayloadForEmail(string linkKey, Guid tenantId, Guid notificationId, Guid userId) {

//            DeeplinkPayload dPayload = new DeeplinkPayload();
//            dPayload.TenantId = tenantId;
//            dPayload.NotificationId = notificationId;
//            dPayload.LinkNotificationId = notificationId;
//            Dictionary<string, string> linkInfo = new Dictionary<string, string>();
//            linkInfo.Add("Actions", "notification,list");
//            linkInfo.Add("RelativeURL", "notification/");
//            linkInfo.Add("tenantid", tenantId.ToString());
//            linkInfo.Add("recepientuserid", userId.ToString());
//            dPayload.DeeplinkInfo = new Dictionary<string, Dictionary<string, string>>();
//            dPayload.DeeplinkInfo.Add(linkKey, linkInfo);
//            return dPayload;
//        }


//        #region Deeplink Payload Methods


//        private Dictionary<string, Dictionary<string, string>> GetBusinessUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businessuser,invite";
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
//                invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
//                invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
//                invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
//                invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
//                invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                //invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
//                deeplinks.Add("businessUserInviteSetPwd", invitationDeeplinkInfo);
//            }
//            return deeplinks;
//        }

//        /// <summary>
//        /// Generates deeplinks for business user invite event from notification data.
//        /// </summary>
//        private Dictionary<string, Dictionary<string, string>> GetPrimaryPartnerUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartnerprimaryuser,invite";
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
//                invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
//                invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
//                invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
//                invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
//                invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
//                deeplinks.Add("partnerUserInviteSetPwd", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }


//        private Dictionary<string, Dictionary<string, string>> GetPartnerUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,invite";
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
//                invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
//                invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
//                invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
//                invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
//                invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                // invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
//                deeplinks.Add("partnerUserInviteSetPwd", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetBusinessUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businessuser,forgotpassword";
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
//                invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
//                invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
//                invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
//                invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
//                invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                //invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
//                deeplinks.Add("businessForgotPasswordLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetBusinessPartnerUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
//                invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
//                invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
//                invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
//                invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
//                invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                //invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
//                deeplinks.Add("partnerForgotPasswordLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        #region DeepLink for Invoice and Payment

//        private Dictionary<string, Dictionary<string, string>> GetInvoiceAddedDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";

//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetInvoiceDeletedDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetPaymentDoneDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetPaymentVoidDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetPaymentRefundDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetSystemCancelRefundDeeplinkPayloadBusiness(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        private Dictionary<string, Dictionary<string, string>> GetSystemCancelRefundDeeplinkPayloadBusinessPartner(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
//            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();

//            if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
//                Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
//                Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
//                //Set fix information in deeplink
//                string action = "businesspartneruser,forgotpassword";
//                ;
//                invitationDeeplinkInfo.Add("Actions", action);
//                // ToDo: Nitin-Notification-Try to add params herre.
//                //invitationDeeplinkInfo.Add("Params", action);
//                invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

//                // Set Dynamic info from Notification payload.
//                invitationDeeplinkInfo.Add("invoiceId", deeplinkInfo["invoiceId"]);
//                invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
//                invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
//                deeplinks.Add("activationLink", invitationDeeplinkInfo);

//            }
//            return deeplinks;
//        }

//        #endregion DeepLink for Invoice and Payment

//        #endregion


//        /// <summary>Gets the email XSLT file.</summary>
//        /// <param name="eventId">The event identifier.</param>
//        /// <param name="regionLanguage">The region language.</param>
//        /// <returns></returns>
//        private string GetEmailXsltFile(PaymentNotificationEvent eventId, string regionLanguage) {
//            //string cacheKey = string.Format("{0}-{1}", EventEmailXsltFileKey, regionLanguage);
//            //if (CacheHelper.IsInCache(cacheKey)) {
//            //  return CacheHelper.GetData<string>(cacheKey); ;
//            //}
//            //else {
//            //Engg: should define in Config file.
//            // string xsltResourcePath = "/resourcePath/";
//            string templateFileName = GetXSLTemplateFileName(eventId);
//            string xsltTextFile = FileHelper.ReadFileAsText(Path.Combine(Path.Combine(_appSettings.XslTemplateRootPath, regionLanguage), templateFileName));
//            // CacheHelper.SetData(cacheKey, xsltTextFile, "XsltFileText");
//            return xsltTextFile;
//            //}
//            //string path = @"C:\ewAppsServices\ewAppsServer\ewApps.Payment.DS\XSL\BusinessUserInvitationEmail.xslt";

//            //string xsltTextFile = FileHelper.ReadFileAsText(path);
//            //return xsltTextFile;
//        }

//        private string GetXSLTemplateFileName(PaymentNotificationEvent eventNumber) {
//            switch(eventNumber) {
//                case PaymentNotificationEvent.SetPasswordBusinessUserInvitationEmail:
//                    return "BusinessUserInvitationEmail.xslt";
//                case PaymentNotificationEvent.SetPasswordPrimaryPartnerUserInvitationEmail:
//                    return "PrimaryPartnerUserInvitationEmail.xslt";
//                case PaymentNotificationEvent.SetPasswordPartnerUserInvitationEmail:
//                    return "PartnerUserInvitationEmail.xslt";
//                case PaymentNotificationEvent.BusinessForgotPasswordEmail:
//                    return "BusinessForgotPasswordEmail.xslt";
//                case PaymentNotificationEvent.BusinessPartnerForgotPasswordEmail:
//                    return "PartnerForgotPasswordEmail.xslt";
//                case PaymentNotificationEvent.InvoiceAdded:
//                    return "NewInvoiceAdded.xslt";
//                case PaymentNotificationEvent.InvoiceDeleted:
//                    return "InvoiceDeleted.xslt";
//                case PaymentNotificationEvent.PaymentDone:
//                    return "PaymentDone.xslt";
//                case PaymentNotificationEvent.PaymentVoid:
//                    return "PaymentVoid.xslt";
//                case PaymentNotificationEvent.PaymentRefund:
//                    return "PaymentRefund.xslt";
//                case PaymentNotificationEvent.SystemCancelRefundForBusiness:
//                    return "SystemCancelsRefundBusiness.xslt";
//                case PaymentNotificationEvent.SystemCancelRefundForBusinessPartner:
//                    return "SystemCancelsRefundBusinessPartner.xslt";
//                default:
//                    return "";
//            }
//        }

//        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {
//            throw new NotImplementedException();
//        }

//        protected override Dictionary<string, string> GetXMLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
//            throw new NotImplementedException();
//        }

//        #endregion

//    }
//}
