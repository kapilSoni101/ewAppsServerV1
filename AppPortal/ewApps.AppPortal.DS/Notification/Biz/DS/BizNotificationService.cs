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
    public class BizNotificationService:NotificationService<NotificationRecipient>, IBizNotificationService {

        #region Member Variables

        IASNotificationDS _asNotificationDS;
        IBizNotificationRecipientDS _bizNotificationRecipientDS;
        AppPortalAppSettings _appPortalAppSettings;

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
        public BizNotificationService(ILogger<BizNotificationService> logger, IEmailService emailService, ISMSService smsService, IASNotificationService asNotificationService, IASNotificationDS asNotificationDS, IBizNotificationRecipientDS bizNotificationRecipientDS, IOptions<AppPortalAppSettings> appPortalAppSettingOptions, IOptions<NotificationAppSettings> notificationAppSettingOptions)
            : base(logger, emailService, smsService, asNotificationService, notificationAppSettingOptions) {
            _asNotificationDS = asNotificationDS;
            _bizNotificationRecipientDS = bizNotificationRecipientDS;
            _appPortalAppSettings = appPortalAppSettingOptions.Value;
        }

        #endregion

        #region Override Methods

        /// <inheritdoc/>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                BizNotificationEventEnum parsedEvent;

                if(moduleId <= 0) {
                    valid = false;
                }

                if(Enum.TryParse<BizNotificationEventEnum>(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in BizNotificationService.ValidateNotificationData:-");
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
            Guid entityId = Guid.Empty;
            if(eventInfo.ContainsKey(NotificationConstants.AppIdKey)) {
                appId = Guid.Parse(eventInfo[NotificationConstants.AppIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.TenantIdKey)) {
                businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.AppUserIdKey)) {
                tenantUserId = Guid.Parse(eventInfo[NotificationConstants.AppUserIdKey]);
            }
            if(eventInfo.ContainsKey(NotificationConstants.EntityIdKey)) {
                entityId = Guid.Parse(eventInfo[NotificationConstants.EntityIdKey]);
            }

            switch((BizNotificationEventEnum)payload.EventId) {
                case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                    recipients = _bizNotificationRecipientDS.GetBizUserAppOnBoardRecipients(Guid.Parse(eventInfo["BizSetupAppId"]), businessTenantId, Guid.Parse(eventInfo["onboardedUserId"]), (long)BusinessUserBusinessSetupAppPreferenceEnum.NewBusinessUserOnboard);
                    break;
                          
                case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    recipients = _bizNotificationRecipientDS.GetCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewSalesQuotations | (int)BusinessUserCustomerAppPermissionEnum.ManageSalesQuotations);
                    break;
                case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)BizNotificationEventEnum.AddNoteOnDeliveryForBizUser, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewDeliveries | (int)BusinessUserCustomerAppPermissionEnum.ManageDeliveries);
                    break;
                case BizNotificationEventEnum.AddNoteOnContractForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)BizNotificationEventEnum.AddNoteOnContractForBizUser, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewContractManagement | (int)BusinessUserCustomerAppPermissionEnum.ManageContractManagement);
                    break;
                case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewSalesOrders | (int)BusinessUserCustomerAppPermissionEnum.ManageSalesOrders);
                    break;
                case BizNotificationEventEnum.AddNoteOnASNForBizUser:
                    recipients = _bizNotificationRecipientDS.GetBusinessCustomerUsersForNotes(businessTenantId, tenantUserId, appId, entityId, (int)BizNotificationEventEnum.AddNoteOnASNForBizUser, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserCustomerAppPermissionEnum.ViewASN | (int)BusinessUserCustomerAppPermissionEnum.ManageASN);
                    break;
                case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                    recipients = _bizNotificationRecipientDS.GetCustomerUser(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Customer, (int)TenantUserInvitaionStatusEnum.Accepted, (int)BusinessUserPaymentAppPermissionEnum.ViewInvoices | (int)BusinessUserPaymentAppPermissionEnum.PaymentActivities | (int)BusinessUserPaymentAppPermissionEnum.AccessTransactionHistory);
                    break;
                case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                    recipients = _bizNotificationRecipientDS.GetAppUserOnBusiness(businessTenantId, tenantUserId, appId);
                    break;                
                case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                    recipients = _bizNotificationRecipientDS.GetAppUserPermissionOnBusiness(businessTenantId, tenantUserId, appId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted);
                    break;
                case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                    recipients = _bizNotificationRecipientDS.GetInvitedBusinessUser(businessTenantId, tenantUserId, appId);
                    break;
                case BizNotificationEventEnum.ContactUsNotification:
                    recipients  = _bizNotificationRecipientDS.GetAllPublisherUsersWithPreference(businessTenantId);
                    break;
                case BizNotificationEventEnum.BusinessUserAppAccessAddAndRemoved:
                    recipients = _bizNotificationRecipientDS.GetAppUserAccessUpdateOnBusiness(businessTenantId, tenantUserId);
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

                    switch((BizNotificationEventEnum)payload.EventId) {
                        case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                        case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                        case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                            xslArguments.Add("publisherName", eventInfo["publisherName"]);
                            xslArguments.Add("businessName", eventInfo["businessName"]);
                            xslArguments.Add("appName", eventInfo["appName"]);
                            xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                            xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;
                        //case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                        //case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                        //    xslArguments.Add("publisherName", eventInfo["publisherName"]);
                        //    xslArguments.Add("businessName", eventInfo["businessName"]);
                        //    xslArguments.Add("appName", eventInfo["appName"]);
                        //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    break;
                        //case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                        //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        //    xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        //    xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        //    xslArguments.Add("newUserName", eventInfo["newUserName"]);
                        //    xslArguments.Add("subDomain", eventInfo["subDomain"]);
                        //    xslArguments.Add("appName", eventInfo["appName"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    //xslArguments.Add("portalURL", eventInfo["portalURL"]);
                        //    break;
                        //case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                        //    xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                        //    xslArguments.Add("applicationName", eventInfo["applicationName"]);
                        //    xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                        //    xslArguments.Add("actionTime", eventInfo["actionTime"]);
                        //    xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                        //    xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                        //    break;
                        case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
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
                        case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
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
                        case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
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
                        case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
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
                        case BizNotificationEventEnum.AddNoteOnContractForBizUser:
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
                        case BizNotificationEventEnum.AddNoteOnASNForBizUser:
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
                        case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
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
                        case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("ticketID", eventInfo["ticketID"]);
                            xslArguments.Add("title", eventInfo["title"]);
                            xslArguments.Add("description", eventInfo["description"]);
                            xslArguments.Add("oldPriority", eventInfo["oldPriority"]);
                            xslArguments.Add("newPriority", eventInfo["newPriority"]);
                            xslArguments.Add("oldStatus", eventInfo["oldStatus"]);
                            xslArguments.Add("newStatus", eventInfo["newStatus"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("count", eventInfo["count"]);
                            xslArguments.Add("commentsText", eventInfo["commentsText"]);
                            xslArguments.Add("modifiedOn", eventInfo["modifiedOn"]);
                            xslArguments.Add("updatedBy", eventInfo["updatedBy"]);
                            xslArguments.Add("assignedTo", eventInfo["assignedTo"]);
              xslArguments.Add("oldassignedTo", eventInfo["oldassignedTo"]);
              xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;
                        case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("businessUserName", eventInfo["businessUserName"]);
                            xslArguments.Add("actionTime", eventInfo["actionTime"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            break;                        
                        case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("applicationName", eventInfo["applicationName"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("userNameChange", eventInfo["userNameChange"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("actionTime", eventInfo["actionTime"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            break;

                        case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            xslArguments.Add("invitedUserName", eventInfo["invitedUserName"]);
                            xslArguments.Add("businessInvitingUserName", eventInfo["businessInvitingUserName"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalURL", eventInfo["portalURL"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            break;

                        case BizNotificationEventEnum.BusinessUserAccountStatusChanged:
                          xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                          xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]); 
                          xslArguments.Add("userName", eventInfo["businessUserName"]);
                          xslArguments.Add("businessUserNameChange", eventInfo["businessUserNameChange"]);
                          xslArguments.Add("subDomain", eventInfo["subDomain"]);
                          xslArguments.Add("newStatus", eventInfo["newStatus"]); 
                          xslArguments.Add("oldStatus", eventInfo["oldStatus"]); 
                          xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                          break;
                        case BizNotificationEventEnum.ContactUsNotification:
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
                        case BizNotificationEventEnum.BusinessUserAppAccessAddAndRemoved:                            
                            xslArguments.Add("publisherCompanyName", eventInfo["publisherCompanyName"]);
                            xslArguments.Add("updatedBy", eventInfo["updatedBy"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                            xslArguments.Add("subDomain", eventInfo["subDomain"]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add("copyrightText", eventInfo["copyrightText"]);
                            xslArguments.Add("businessCompanyName", eventInfo["businessCompanyName"]);
                            break;

                    }
                    return xslArguments;
                }
                catch(Exception ex) {
                    StringBuilder exceptionDetail = new StringBuilder();
                    exceptionDetail.Append("Exception occurred in BizNotificationService.GetXMLArgumentsForEmail:-");
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
                emailPayload.XSLTemplateContent = GetEmailXsltFile((BizNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
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
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        #region Private Methods

        // Read email notification xsl template file content as string.
        private string GetEmailXsltFile(BizNotificationEventEnum eventId, string regionLanguage) {
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
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetEmailXsltFile:-");
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
                exceptionDetail.Append("Exception occurred in BizNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        // This method returns xsl file name that contains template name.
        private string GetXSLTemplateFileName(BizNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                    return "BizUserAppOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                //    return "BizPayUserOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessCustomerPortalUserIsOnboard:
                //    return "BizCustAppUserOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                //    return "BusinessUserBusinessSetupOnboard.xslt";
                //case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                //    return "BizUserAppAccessRemoved.xslt";
                case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    return "AddNoteOnARInvoiceForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    return "AddNoteOnAPInvoiceForCustUser.xslt";
                case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                    return "AddNoteOnSalesQuotationForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                    return "AddNoteOnDeliveryForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnContractForBizUser:
                    return "AddNoteOnContractForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnASNForBizUser:
                    return "AddNoteOnDraftDeliveryForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                    return "AddNoteOnSalesOrderForBizUser.xslt";
                case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                    return "CustomerUserMyTicketUpdate.xslt";
                case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                    return "BusinessUserWithNewEmailIdInvite.xslt";
                case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                    return "BizUserAppAccessRemoved.xslt";
                case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                    return "BizUserAppPermissionChange.xslt";
                case BizNotificationEventEnum.BusinessUserAccountStatusChanged:
                  return "BizUserAccountStatusChange.xslt";
                case BizNotificationEventEnum.ContactUsNotification:
                    return "ContactUsEmail.xslt";
                case BizNotificationEventEnum.BusinessUserAppAccessAddAndRemoved:
                    return "BizUserAppAccessAddAndUpdate.xslt";
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
                switch((BizNotificationEventEnum)payload.EventId) {
                    case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                    //Dictionary<string, string> invoicePaymentEventData = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    //xslArguments.Add("appName", eventInfo["appName"]);
                    //xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    //break;
                    case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                    case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                        Dictionary<string, string> BizCustEventData = (Dictionary<string, string>)payload.EventInfo["EventData"];
                        xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                        xslArguments.Add("appName", eventInfo["appName"]);
                        xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                        break;
                    //case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                    //    Dictionary<string, string> BizSetupEventData = (Dictionary<string, string>)payload.EventInfo["EventData"];
                    //    xslArguments.Add("onboardedUserName", eventInfo["onboardedUserName"]);
                    //    xslArguments.Add("onboardTime", eventInfo["onboardTime"]);
                    //    xslArguments.Add("appCount", eventInfo["appCount"]);
                    //    break;
                    case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                        xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                        xslArguments.Add("userTypeText", eventInfo["userTypeText"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnContractForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnASNForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                        xslArguments.Add("ID", eventInfo["ticketID"]);
                        xslArguments.Add("updatedBy", eventInfo["updatedBy"]);
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        break;
                    case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                        xslArguments.Add("applicationName", eventInfo["applicationName"]);
                        xslArguments.Add("userNameChange", eventInfo["userNameChange"]);
                        xslArguments.Add("actionTime", eventInfo["actionTime"]);
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetXSLArgumentsForASNotification:-");
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

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((BizNotificationEventEnum)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetASNotificationPayload:-");
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

        private string GetASNotificationXsltFile(BizNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(BizNotificationEventEnum eventId) {
            switch(eventId) {
                case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                    return "BizUserAppOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                //    return "BizPayUserOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessCustomerPortalUserIsOnboard:
                //    return "BizCustAppUserOnBoard.xslt";
                //case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                //    return "BusinessUserBusinessSetupOnboard.xslt";
                case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                    return "AddNoteOnARInvoiceForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                    return "AddNoteOnAPInvoiceForCustUser.xslt";
                case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                    return "AddNoteOnSalesQuotationForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                    return "AddNoteOnDeliveryForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnContractForBizUser:
                    return "AddNoteOnContractForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnASNForBizUser:
                    return "AddNoteOnDraftDeliveryForBizUser.xslt";
                case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                    return "AddNoteOnSalesOrderForBizUser.xslt";
                case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                    return "CustomerUserMyTicketUpdate.xslt";
                case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                    return "BizUserAppPermissionChange.xslt";
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
                        switch((BizNotificationEventEnum)notificationPayload.EventId) {

                            //case BizNotificationEventEnum.CustomerPaymentDoneByBusinessUser:
                            //    // Here We didn't fill DeeplinkInfoList because email should contains all url target to single
                            //    // application.
                            //    deeplinkPayload.DeeplinkInfo = GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList(notificationPayload, notificationPayload.EventId);
                            //    break;

                            case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                                deeplinkPayload.DeeplinkInfo = GetBusinessNewUserInviteDeeplinkPayload(notificationPayload, (long)BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite);
                                break;

                        }
                    }
                }
                return deeplinkPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetDeeplinkPayload:-");
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
                switch((BizNotificationEventEnum)decisionParameters) {
                    case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:
                        return _appSettings.BusinessBranchApiKey;
                    default:
                        return "";
                        // _appSettingsOption.Value.PublisherBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetBranchKey:-");
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
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList:-");
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
            switch((BizNotificationEventEnum)platformNotificationEvents) {
                case BizNotificationEventEnum.NewBusinessPaymentUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessCustomerAppUserIsOnboard:
                case BizNotificationEventEnum.NewBusinessSetupUserOnboard:
                case BizNotificationEventEnum.AddNoteOnARInvoiceForBizUser:
                case BizNotificationEventEnum.AddNoteOnAPInvoiceForCustomerUser:
                case BizNotificationEventEnum.AddNoteOnDeliveryForBizUser:
                case BizNotificationEventEnum.AddNoteOnContractForBizUser:
                case BizNotificationEventEnum.AddNoteOnSalesOrderForBizUser:
                case BizNotificationEventEnum.AddNoteOnASNForBizUser:
                case BizNotificationEventEnum.BusinessUserPaymentAppPermissionsChanged:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;

                case BizNotificationEventEnum.BusinessUserAppAccessRemoved:
                case BizNotificationEventEnum.AddNoteOnSalesQuotationForBizUser:
                case BizNotificationEventEnum.ExistingCustomerTicketIsUpdatedForCustomer:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
                case BizNotificationEventEnum.BusinessUserWithNewEmailIdInvite:                
                    return NotificationDeliveryType.Email;
                case BizNotificationEventEnum.BusinessUserAccountStatusChanged:
                case BizNotificationEventEnum.ContactUsNotification:
                case BizNotificationEventEnum.BusinessUserAppAccessAddAndRemoved:
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

        #endregion

        #region Deeplink Payload Methods

        private Dictionary<string, Dictionary<string, string>> GetBusinessNewUserInviteDeeplinkPayload(NotificationPayload<NotificationRecipient> notificationPayload, long eventId) {
            Dictionary<string, Dictionary<string, string>> deeplinks = new Dictionary<string, Dictionary<string, string>>();
            try {
                if(notificationPayload.EventInfo.ContainsKey("DeeplinkInfo")) {
                    Dictionary<string, string> deeplinkInfo = (Dictionary<string, string>)(notificationPayload.EventInfo["DeeplinkInfo"]);
                    Dictionary<string, string> invitationDeeplinkInfo = new Dictionary<string, string>();
                    //Set fix information in deeplink
                    string action = "account,invite,businessuser,new";
                    invitationDeeplinkInfo.Add("Actions", action);
                    // ToDo: Nitin-Notification-Try to add params herre.
                    //invitationDeeplinkInfo.Add("Params", action);
                    invitationDeeplinkInfo.Add("RelativeURL", NotificationConstants.BusinessPortalDefaultUrl);


                    //List<string> decisonData = new List<string>();
                    //decisonData.Add(eventId.ToString());

                    // Set Dynamic info from Notification payload.
                    invitationDeeplinkInfo.Add("appuserid", deeplinkInfo["appuserid"]);
                    invitationDeeplinkInfo.Add("tenantid", deeplinkInfo["tenantid"]);
                    invitationDeeplinkInfo.Add("identityserveruserid", deeplinkInfo["identityserveruserid"]);
                    invitationDeeplinkInfo.Add("loginemail", deeplinkInfo["loginEmail"]);
                    invitationDeeplinkInfo.Add("usertype", deeplinkInfo["usertype"]);
                    invitationDeeplinkInfo.Add("code", deeplinkInfo["code"]);
                    invitationDeeplinkInfo.Add("partnerid", deeplinkInfo["partnerid"]);
                    invitationDeeplinkInfo.Add("tokeninfoid", deeplinkInfo["tokeninfoid"]);
                    invitationDeeplinkInfo.Add("branchkey", GetBranchKey(eventId).ToString());
                    invitationDeeplinkInfo.Add("subDomain", deeplinkInfo["subDomain"]);
                    invitationDeeplinkInfo.Add("tokentype", deeplinkInfo["tokentype"]);
                    deeplinks.Add("businessUserInviteSetPwd", invitationDeeplinkInfo);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BizNotificationService.GetBusinessNewUserInviteDeeplinkPayload:-");
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
