using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    public class CustNotificationService:NotificationService<NotificationRecipient>, ICustNotificationService {

        #region Member Variables

        IASNotificationDS _asNotificationDS;
        ICustNotificationRecipientsDS _custNotificationRecipientsDS;
        AppPortalAppSettings _appPortalAppSettings;
        IOptions<NotificationAppSettings> _appSettingsOption;
        IBusinessNotificationReceipentDataService _businessNotificationReceipentDataService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BizNotificationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="asNotificationService">As notification service.</param>
        /// <param name="asNotificationDS">As notification ds.</param>

        public CustNotificationService(IBusinessNotificationReceipentDataService businessNotificationReceipentDataService, IOptions<NotificationAppSettings> appSettingsOption, ILogger<BizNotificationService> logger, IEmailService emailService, ISMSService smsService, IASNotificationService asNotificationService, IASNotificationDS asNotificationDS, ICustNotificationRecipientsDS custNotificationRecipientsDS, IOptions<AppPortalAppSettings> appPortalAppSettingOptions, IOptions<NotificationAppSettings> notificationAppSettingOptions)
                : base(logger, emailService, smsService, asNotificationService, notificationAppSettingOptions) {
            _asNotificationDS = asNotificationDS;
            _custNotificationRecipientsDS = custNotificationRecipientsDS;
            _appPortalAppSettings = appPortalAppSettingOptions.Value;
            _appSettingsOption = appSettingsOption;
            _businessNotificationReceipentDataService = businessNotificationReceipentDataService;
        }

        #endregion

        #region Override Methods

        /// <inheritdoc/>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                CustNotificationEventEnum parsedEvent;

                if(moduleId <= 0) {
                    valid = false;
                }

                if(Enum.TryParse<CustNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in CustNotificationService.ValidateNotificationData:-");
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
            Guid tenantUserId = Guid.Empty;
            Guid businessTenantId = Guid.Empty;
            Guid businessPartnerTenantId = Guid.Empty;
            if(eventInfo.ContainsKey(NotificationConstants.AppIdKey)) {
                appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.TenantIdKey)) {
                businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.BusinessPartnerTenantIdKey)) {
                businessPartnerTenantId = Guid.Parse(eventInfo[NotificationConstants.BusinessPartnerTenantIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.AppUserIdKey)) {
                tenantUserId = Guid.Parse(eventInfo[NotificationConstants.AppUserIdKey]);
            }

            appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);
            businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);
            businessPartnerTenantId = Guid.Empty;

            if(eventInfo.ContainsKey(NotificationConstants.BusinessPartnerTenantIdKey)) {
                businessPartnerTenantId = Guid.Parse(eventInfo[NotificationConstants.BusinessPartnerTenantIdKey]);
            }

            tenantUserId = Guid.Empty;

            if(eventInfo.ContainsKey("appuserid")) {
                tenantUserId = Guid.Parse(eventInfo["appuserid"]);
            }

            switch((CustNotificationEventEnum)payload.EventId) {
                //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                //    recipients = _custNotificationRecipientsDS.GetCustPaymentUserOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, Guid.Parse(eventInfo["onboardedUserId"]));
                //    break;
                //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                //    recipients = _custNotificationRecipientsDS.GetCustCustomerUserOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, Guid.Parse(eventInfo["onboardedUserId"]));
                //    break;
                //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                //   recipients = _custNotificationRecipientsDS.GetCustUserAppOnBoardRecipients(Guid.Parse(eventInfo["CustSetupAppId"]), businessTenantId, Guid.Parse(eventInfo["onboardedUserId"]), (long)BusinessUserBusinessSetupAppPreferenceEnum.NewBusinessUserOnboard);
                //    break;
                case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                    // recipients = _custNotificationRecipientsDS.GetCustUserAppOnBoardRecipients(Guid.Parse(eventInfo["CustSetupAppId"]), businessTenantId, Guid.Parse(eventInfo["onboardedUserId"]), (long)BusinessUserBusinessSetupAppPreferenceEnum.NewBusinessUserOnboard);
                    recipients = _custNotificationRecipientsDS.GetCustUserAppOnBoardRecipients(appId, businessTenantId, businessPartnerTenantId, Guid.Parse(eventInfo["onboardedUserId"]));
                    break;
                case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, (int)CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewQuotations );
                    break;
                case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, (int)CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewDeliveries);
                    break;
                case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, (int)CustNotificationEventEnum.AddNoteOnContractForCustomerUser, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewContractManagement);
                    break;
                case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, (int)CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewOrders | (int)BusinessUserCustomerAppPermissionEnum.ManageSalesOrders);
                    break;
                case CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, (int)CustNotificationEventEnum.AddNoteOnASNForCustomerUser, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)CustomerUserCustomerAppPermissionEnum.ViewDeliveries);
                    break;
                case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    recipients = _custNotificationRecipientsDS.GetCustomerUsersForNotesNotification(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    recipients = _custNotificationRecipientsDS.GetBusinessUsersForNotesNotification(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case CustNotificationEventEnum.CustomerUserAppAccessRemoved:
                    recipients = _custNotificationRecipientsDS.GetCustomerUserOnAppDeletedRecipients(businessTenantId, tenantUserId, appId);
                    break;
                case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                    recipients = _custNotificationRecipientsDS.GetCustomerUserOnAppPermissionRecipients(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case CustNotificationEventEnum.ForgotPasswordBusinessPartner:
                    return _businessNotificationReceipentDataService.GetForgotPasswordBusinessUser(businessTenantId, tenantUserId);
                case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                    recipients = _custNotificationRecipientsDS.GetBusinessUsersForSupportTicketNotification(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                    recipients = _custNotificationRecipientsDS.GetBusinessUsersForSupportTicketNotification(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case CustNotificationEventEnum.ContactUsNotification:
                    recipients = _custNotificationRecipientsDS.GetAllPublisherUsersWithPreference(businessTenantId);
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

                    switch((CustNotificationEventEnum)payload.EventId) {
                        //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                        //    xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        //    xslArguments.Add("paymentAppName", eventInfo["paymentAppName"]);
                        //    xslArguments.Add("customerName", eventInfo["customerName"]);
                        //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    break;
                        //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                        //    xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        //    xslArguments.Add("appName", eventInfo["appName"]);
                        //    xslArguments.Add("customerName", eventInfo["customerName"]);
                        //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    break;
                        //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                        //    xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        //    xslArguments.Add("appName", eventInfo["appName"]);
                        //    xslArguments.Add("customerName", eventInfo["customerName"]);
                        //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        //    xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    break;
                        case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                        case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                        case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                            xslArguments.Add("publisherName", eventInfo["publisherName"]);
                            xslArguments.Add("appName", eventInfo["appName"]);
                            xslArguments.Add("customerName", eventInfo["customerName"]);
                            xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                            xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                            xslArguments.Add("comment", eventInfo["comment"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            break;
                        case CustNotificationEventEnum.CustomerUserAppAccessRemoved:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("customerUserName", eventInfo["customerUserName"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            break;
                        case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("customerUserName", eventInfo["customerUserName"]);
                            xslArguments.Add("customerUserNameChange", eventInfo["customerUserNameChange"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("actionTime", eventInfo["actionTime"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            break;
                        case CustNotificationEventEnum.ForgotPasswordBusinessPartner:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("customerUserName", eventInfo["businessUserName"]);
                            xslArguments.Add("customerUserID", eventInfo["businessUserID"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);

                            //xslArguments.Add("appuserid", eventInfo["appuserid"]);

                            break;
                        case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("ticketID", eventInfo["ticketID"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("contactEmail", eventInfo["contactEmail"]);
                            xslArguments.Add("createdOn", eventInfo["createdOn"]);
                            xslArguments.Add("title", eventInfo["title"]);
                            xslArguments.Add("description", eventInfo["description"]);
                            xslArguments.Add("priority", eventInfo["newPriority"]);
                            xslArguments.Add("status", eventInfo["newStatus"]);
                            xslArguments.Add("createdBy", eventInfo["createdBy"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);


                            xslArguments.Add("count", eventInfo["count"]);
                            xslArguments.Add("commentsText", eventInfo["commentsText"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);



                            break;
                        case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("customerName", eventInfo["customerName"]);
                            xslArguments.Add("ticketID", eventInfo["ticketID"]);

                            xslArguments.Add("modifiedBy", eventInfo["modifiedBy"]);
                            xslArguments.Add("modifiedOn", eventInfo["modifiedOn"]);
                            xslArguments.Add("newAssignedTo", eventInfo["newAssignedTo"]);
                            xslArguments.Add("oldPriority", eventInfo["oldPriority"]);

                            xslArguments.Add("newPriority", eventInfo["newPriority"]);
                            xslArguments.Add("oldStatus", eventInfo["oldStatus"]);
                            xslArguments.Add("newStatus", eventInfo["newStatus"]);
                            xslArguments.Add("createdOn", eventInfo["createdOn"]);
                            xslArguments.Add("title", eventInfo["title"]);
                            xslArguments.Add("description", eventInfo["description"]);
                            xslArguments.Add("createdBy", eventInfo["createdBy"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);


                            xslArguments.Add("count", eventInfo["count"]);
                            xslArguments.Add("commentsText", eventInfo["commentsText"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);



                            break;
                        case CustNotificationEventEnum.ContactUsNotification:
                            xslArguments.Add("osName", eventInfo["osName"]);
                            xslArguments.Add("browserName", eventInfo["browserName"]);
                            xslArguments.Add("appVersion", eventInfo["appVersion"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userEmail", eventInfo["userEmail"]);
                            xslArguments.Add("phoneNumber", eventInfo["phoneNumber"]);
                            xslArguments.Add("portal", eventInfo["portal"]);
                            xslArguments.Add("timeOfAction", eventInfo["timeOfAction"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("comments", eventInfo["comments"]);
                            xslArguments.Add("accountName", eventInfo["accountName"]);
                            xslArguments.Add("application", eventInfo["application"]);
                            break;
                        default:
                            break;
                    }
                    return xslArguments;
                }
                catch(Exception ex) {
                    StringBuilder exceptionDetail = new StringBuilder();
                    exceptionDetail.Append("Exception occurred in CustNotificationService.GetXMLArgumentsForEmail:-");
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
                    emailPayload.UserLanguage = AppPortalConstants.DefaultLanguage;
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
                emailPayload.XSLTemplateContent = GetEmailXsltFile((CustNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
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
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        #region Private Methods

        // Read email notification xsl template file content as string.
        private string GetEmailXsltFile(CustNotificationEventEnum eventId, string regionLanguage) {
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
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetEmailXsltFile:-");
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
                exceptionDetail.Append("Exception occurred in CustNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        // This method returns xsl file name that contains template name.
        private string GetXSLTemplateFileName(CustNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                //    return "CustPayUserOnBoard.xslt";
                //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                //    return "CustUserCustAppOnBoard.xslt";
                //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                //    return "CustUserCustAppOnBoard.xslt";
                case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                    return "CustUserAppOnBoard.xslt";
                case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                    return "AddNoteOnSalesQuotationForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                    return "AddNoteOnDeliveryForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                    return "AddNoteOnContractForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                    return "AddNoteOnDraftDeliveryForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                    return "AddNoteOnSalesOrderForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    return "AddNoteOnARInvoiceForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    return "AddNoteOnAPInvoiceForCustUser.xslt";
                case CustNotificationEventEnum.ForgotPasswordBusinessPartner:
                    return "CustUserForgotPassword.xslt";
                case CustNotificationEventEnum.CustomerUserAppAccessRemoved:
                    return "CustomerUserAppAccessRemoved.xslt";
                case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                    return "CustUserAppPermissionChanged.xslt";
                case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                    return "NewCustomerTicketReceived.xslt";
                case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                    return "CustomerTicketUpdatedForBizUser.xslt";
                case CustNotificationEventEnum.ContactUsNotification:
                    return "ContactUsEmail.xslt";
                default:
                    return "";
            }
        }

        #endregion

        #endregion

        #region SMS Methods

        /// <inheritdoc/>
        public override SMSPayload GetSMSPayload(NotificationPayload<NotificationRecipient> notificationPayload, NotificationRecipient recepientDataRow, string eventXMLData, bool hasLinkError, Dictionary<string, string> xmlArguments) {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override Dictionary<string, string> GetXSLArgumentsForSMS(NotificationRecipient userDataRow, NotificationPayload<NotificationRecipient> payload) {
            throw new NotImplementedException();
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
                switch((CustNotificationEventEnum)payload.EventId) {
                    //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                    //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    //    xslArguments.Add("paymentAppName", eventInfo["paymentAppName"]);
                    //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    //    break;
                    //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                    //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                    //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    //    break;
                    //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                    //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    //    xslArguments.Add("appName", eventInfo["appName"]);
                    //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    //    break;
                    case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                    case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                    case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                        xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        xslArguments.Add("appName", eventInfo["appName"]);
                        xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                        xslArguments.Add("ticketID", eventInfo["ticketID"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerName"]);
                        break;
                    case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                        xslArguments.Add("ticketID", eventInfo["ticketID"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerName"]);
                        break;
                    case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                        xslArguments.Add("applicationName", eventInfo["applicationName"]);
                        xslArguments.Add("customerUserNameChange", eventInfo["customerUserNameChange"]);
                        xslArguments.Add("actionTime", eventInfo["actionTime"]);
                        break;

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetXSLArgumentsForASNotification:-");
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

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((CustNotificationEventEnum)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetASNotificationPayload:-");
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

        private string GetASNotificationXsltFile(CustNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(CustNotificationEventEnum eventId) {
            switch(eventId) {
                //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                //    return "CustPayUserOnBoard.xslt";
                //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                //    return "CustUserCustAppOnBoard.xslt";
                //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                //    return "CustUserCustAppOnBoard.xslt";
                case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                    return "CustUserAppOnBoard.xslt";

                case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                    return "AddNoteOnSalesQuotationForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                    return "AddNoteOnDeliveryForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                    return "AddNoteOnContractForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnASNForCustomerUser:
                    return "AddNoteOnDraftDeliveryForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                    return "AddNoteOnSalesOrderForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    return "AddNoteOnARInvoiceForBizUser.xslt";
                case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    return "AddNoteOnAPInvoiceForCustUser.xslt";
                case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                    return "NewCustomerTicketReceived.xslt";
                case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                    return "CustUserAppPermissionChanged.xslt";
                case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                    return "CustomerTicketUpdatedForBizUser.xslt";
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
                        //Dictionary<string, object> decisionDictionary = new Dictionary<string, object>();

                        // As each action is bound to single event so decision parameter should be event id.
                        //string branchKey = Convert.ToString(GetBranchKey(notificationPayload.EventId));

                        // Get deeplink actions for particular notification.
                        // If any email contains more than one deeplink, returned list should contains each deeplink information.
                        switch((CustNotificationEventEnum)notificationPayload.EventId) {

                            case CustNotificationEventEnum.ForgotPasswordBusinessPartner:
                                //deeplinkPayload.DeeplinkInfo = GetCustUserForgotPasswordDeeplinkPayload(notificationPayload, (long)BusinessNotificationEventEnum.BusinessUserForgotPassword);
                                deeplinkPayload.DeeplinkInfo = GetCustUserForgotPasswordDeeplinkPayload(notificationPayload, (long)CustNotificationEventEnum.ForgotPasswordBusinessPartner);


                                break;
                                //case CustNotificationEventEnum.CustomerPaymentDoneByBusinessUser:
                                //    // Here We didn't fill DeeplinkInfoList because email should contains all url target to single
                                //    // application.
                                //    deeplinkPayload.DeeplinkInfo = GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList(notificationPayload, notificationPayload.EventId);
                                //    break;

                        }
                    }
                }
                return deeplinkPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetDeeplinkPayload:-");
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


        /// <summary>Get branch key on the basis of event type</summary>
        /// <param name="eventData">Event identitfier</param>
        private object GetBranchKeyTemp(object eventData) {
            List<string> decisionData = eventData as List<string>;
            string appKey = string.Empty;
            long eventLong = long.Parse(decisionData[0]);
            if(decisionData.Count > 1) {
                appKey = decisionData[1];
            }
            switch(eventLong) {
                case (long)CustNotificationEventEnum.ForgotPasswordBusinessPartner:
                    return _appSettingsOption.Value.CustomerBranchApiKey;

                default:
                    return "";
            }
        }

        /// <inheritdoc/>
        protected override object GetBranchKey(object decisionParameters) {
            Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO> branchkeyList = new Dictionary<int, DeeplinkActionParameterAndBranchKeyDTO>();

            try {
                // Get decision paramerts from object.
                Dictionary<string, object> decisionDictionary = decisionParameters as Dictionary<string, object>;
                long eventId = long.Parse(decisionDictionary["eventid"].ToString());
                string appKey = decisionDictionary["appkey"].ToString();

                switch(eventId) {

                    case (long)CustNotificationEventEnum.ForgotPasswordBusinessPartner: {
                            DeeplinkActionParameterAndBranchKeyDTO model = new DeeplinkActionParameterAndBranchKeyDTO();
                            model.DeepLinkAction = "businessuser,forgotpassword";
                            List<string> parameterList = new List<string>();
                            parameterList.Add("tenantid");
                            parameterList.Add("appuserid");
                            parameterList.Add("identityserveruserid");
                            parameterList.Add("loginEmail");
                            parameterList.Add("usertype");
                            parameterList.Add("code");
                            parameterList.Add("tokeninfoid");
                            parameterList.Add("subDomain");
                            model.DeeplinkParametersList = parameterList;
                            //if (appKey == AppKeyEnum.biz.ToString()) {
                            //  model.BranchKey = _appSettingsOption.Value.Notification.BusinessBranchApiKey;
                            //}
                            //else if (appKey == AppKeyEnum.pay.ToString()) {
                            //  model.BranchKey = _appSettingsOption.Value.Notification.PaymentBranchApiKey;
                            //}
                            model.BranchKey = _appSettingsOption.Value.BusinessBranchApiKey;
                            branchkeyList.Add((int)UserTypeEnum.Customer, model);
                            break;
                        }

                    default:
                        return "";
                        // _appSettingsOption.Value.PublisherBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return branchkeyList;

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
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList:-");
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
            switch((CustNotificationEventEnum)platformNotificationEvents) {
                //case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                //    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                //case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                //    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                //case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                //    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.NewCustomerCustomerUserOnboard:
                case CustNotificationEventEnum.NewCustomerPaymentUserOnboard:
                case CustNotificationEventEnum.NewCustomerSetupUserOnboard:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnSalesQuotationForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnDeliveryForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnContractForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnSalesOrderForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnASNForBizUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.CustomerUserAppAccessRemoved:
                    return NotificationDeliveryType.Email;
                case CustNotificationEventEnum.CustomerUserCustomerAppPermissionsChanged:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.AddCustomerTicketForBusinessUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.UpdateCustomerTicketForBusinessUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case CustNotificationEventEnum.ContactUsNotification:
                    return NotificationDeliveryType.Email;
                default:
                    return NotificationDeliveryType.All;
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


        private Dictionary<string, Dictionary<string, string>> GetCustUserForgotPasswordDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "businesspartneruser,forgotpassword";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);

                    List<string> decisonData = new List<string>();
                    decisonData.Add(eventId.ToString());

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKeyTemp(decisonData).ToString());
                    invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    //invitationDeeplinkInfo.Add("tenantLanguage", deeplinkInfo["tenantLanguage"]);
                    deeplinks.Add("activationLinkForgotPassword", invitationDeeplinkInfo);

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in CustNotificationService.GetCustUserForgotPasswordDeeplinkPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return deeplinks;
        }


        #endregion
    }
}
