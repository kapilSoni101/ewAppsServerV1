using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.DeeplinkServices;
using ewApps.Core.EmailService;
using ewApps.Core.NotificationService;
using ewApps.Core.SMSService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.BusinessEntity.DS {
    public class BusinessEntityNotificationService:NotificationService<NotificationRecipient>, IBusinessEntityNotificationService {

        IASNotificationDS _asNotificationDS;
        IBusinessEntityNotificationRecipientDS _businessEntityNotificationRecipientDS;
        BusinessEntityAppSettings _businessEntityAppSetting;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessEntityNotificationService"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="smsService">The SMS service.</param>
        /// <param name="asNotificationService">As notification service.</param>
        /// <param name="asNotificationDS">As notification ds.</param>
        public BusinessEntityNotificationService(ILogger<BusinessEntityNotificationService> logger, IEmailService emailService,
            ISMSService smsService, IASNotificationService asNotificationService, IASNotificationDS asNotificationDS, IBusinessEntityNotificationRecipientDS businessEntityNotificationRecipientDS, IOptions<BusinessEntityAppSettings> businessEntityAppSettingOptions, IOptions<NotificationAppSettings> notificationAppSettingOptions)
            : base(logger, emailService, smsService, asNotificationService, notificationAppSettingOptions) {
            _asNotificationDS = asNotificationDS;
            _businessEntityNotificationRecipientDS = businessEntityNotificationRecipientDS;
            _businessEntityAppSetting = businessEntityAppSettingOptions.Value;
        }

        #endregion


        #region Override Methods

        /// <inheritdoc/>
        protected override bool ValidateNotificationData(int moduleId, long eventId, Dictionary<string, object> eventInfo) {
            bool valid = true;
            try {
                BusinessEntityNotificationEventEnum parsedEvent;

                if(moduleId <= 0) {
                    valid = false;
                }

                if(Enum.TryParse(eventId.ToString(), out parsedEvent) == false) {
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
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.ValidateNotificationData:-");
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
            Guid publisherTenantId = new Guid();
      Guid loginUserId = new Guid();
      Guid businessPartnerTenantId = new Guid();
            Guid businessTenantId = Guid.Parse(eventInfo[NotificationConstants.TenantIdKey]);

            switch((BusinessEntityNotificationEventEnum)payload.EventId) {
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
         // publisherTenantId = Guid.Parse(eventInfo["businessPartnerTenantId"]);
          //int addInvoicePermissionMask = int.Parse(eventInfo["invoicePermissionMask"]);
          recipients = _businessEntityNotificationRecipientDS.GetAddARInvoiceNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    recipients = _businessEntityNotificationRecipientDS.GetUpdateARInvoiceNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    businessPartnerTenantId = Guid.Parse(eventInfo["businessPartnerTenantId"]);
                    recipients = _businessEntityNotificationRecipientDS.GetARInvoiceNotificationCustomerUserRecipientAsync(publisherTenantId, businessTenantId, businessPartnerTenantId).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
          loginUserId = Guid.Parse(eventInfo["loginUserId"]);
          //int customerPermissionMask = int.Parse(eventInfo["customerPermissionMask"]);
          recipients = _businessEntityNotificationRecipientDS.GetAddCustomerNotificationRecipientAsync(publisherTenantId, businessTenantId, loginUserId,(int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizUpdateCutsomer:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
          loginUserId = Guid.Parse(eventInfo["loginUserId"]);
          //int customerPermissionMask = int.Parse(eventInfo["customerPermissionMask"]);
          recipients = _businessEntityNotificationRecipientDS.GetUpdateCustomerNotificationRecipientAsync(publisherTenantId, businessTenantId, loginUserId,(int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    recipients = _businessEntityNotificationRecipientDS.GetAddSalesOrderNotificationRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddASN:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum permissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageASN | BusinessUserCustomerAppPermissionEnum.ViewASN;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)permissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.NewDraftDeliveryIsGenerated).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizUpdateASN:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum asnPermissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageASN | BusinessUserCustomerAppPermissionEnum.ViewASN;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)asnPermissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.ExistingDraftDeliveryIsUpdated).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddContract:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum contractPermissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageContractManagement | BusinessUserCustomerAppPermissionEnum.ViewContractManagement;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)contractPermissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.NewContractIsCreated).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizUpdateContract:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum contractUpdatePermissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageContractManagement | BusinessUserCustomerAppPermissionEnum.ViewContractManagement;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)contractUpdatePermissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.ExistingContractIsUpdated).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizAddDelivery:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum deliveryAddPermissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageDeliveries | BusinessUserCustomerAppPermissionEnum.ViewDeliveries;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)deliveryAddPermissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.NewDraftDeliveryIsGenerated).Result;
                    break;
                case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    BusinessUserCustomerAppPermissionEnum deliveryUpdatePermissionBitMask = BusinessUserCustomerAppPermissionEnum.ManageDeliveries | BusinessUserCustomerAppPermissionEnum.ViewDeliveries;
                    recipients = _businessEntityNotificationRecipientDS.GetNotificationBizRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted, (long)deliveryUpdatePermissionBitMask, (long)BusinessUserCustomerAppPreferenceEnum.ExistingDeliveryIsUpdated).Result;
                    break;

                case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                    publisherTenantId = Guid.Parse(eventInfo["publisherTenantId"]);
                    recipients = _businessEntityNotificationRecipientDS.GetAddSalesQuotationNotificationRecipientAsync(publisherTenantId, businessTenantId, (int)UserTypeEnum.Business, (int)TenantUserInvitaionStatusEnum.Accepted).Result;
                    break;
                default:
                    break;
            }

            return recipients;
        }

        #endregion  Override Methods

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

                    switch((BusinessEntityNotificationEventEnum)payload.EventId) {
                        case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                            xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                            xslArguments.Add("postingDate", eventInfo["postingDate"]);
                            xslArguments.Add("documentDate", eventInfo["documentDate"]);
                            xslArguments.Add("dueDate", eventInfo["dueDate"]);
                            xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                            xslArguments.Add("createdByNo", eventInfo["createdByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                            xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                            xslArguments.Add("postingDate", eventInfo["postingDate"]);
                            xslArguments.Add("documentDate", eventInfo["documentDate"]);
                            xslArguments.Add("dueDate", eventInfo["dueDate"]);
                            xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                            xslArguments.Add("createdByNo", eventInfo["createdByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add("newInvoiceCount", eventInfo["newInvoiceCount"]);
                            xslArguments.Add("updatedInvoiceCount", eventInfo["updatedInvoiceCount"]);
                            xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                            xslArguments.Add("updatedByNo", eventInfo["updatedByNo"]);
                            xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                            xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                            xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                            xslArguments.Add("updatedByNo", eventInfo["updatedByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                            xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                            xslArguments.Add("updatedByNo", eventInfo["updatedByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizUpdateCutsomer:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                            xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                            xslArguments.Add("updatedByNo", eventInfo["updatedByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("salesOrderNo", eventInfo["salesOrderNo"]);
                            xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                            xslArguments.Add("postingDate", eventInfo["postingDate"]);
                            xslArguments.Add("documentDate", eventInfo["documentDate"]);
                            xslArguments.Add("deliveryDate", eventInfo["deliveryDate"]);
                            xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                            xslArguments.Add("createdByNo", eventInfo["createdByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddASN:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("totalAmount", eventInfo["totalAmount"]);
                            xslArguments.Add("trackingNumber", eventInfo["trackingNumber"]);
                            xslArguments.Add("address", eventInfo["address"]);
                            xslArguments.Add("packagingSlipNo", eventInfo["packagingSlipNo"]);
                            xslArguments.Add("shippingType", eventInfo["shippingType"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("shippingPlan", eventInfo["shippingPlan"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizUpdateASN:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddContract:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add("startDate", eventInfo["startDate"]);
                            xslArguments.Add("endDate", eventInfo["endDate"]);
                            xslArguments.Add("terminationDate", eventInfo["terminationDate"]);
                            xslArguments.Add("signingDate", eventInfo["signingDate"]);
                            xslArguments.Add("description", eventInfo["description"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizUpdateContract:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddDelivery:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("totalAmount", eventInfo["totalAmount"]);
                            xslArguments.Add("trackingNumber", eventInfo["trackingNumber"]);
                            //xslArguments.Add("address", eventInfo["address"]);
                            //xslArguments.Add("packagingSlipNo", eventInfo["packagingSlipNo"]);
                            xslArguments.Add("shippingType", eventInfo["shippingType"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("shippingPlan", eventInfo["shippingPlan"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("ID", eventInfo["ID"]);
                            xslArguments.Add("customerCompanyID", eventInfo["customerCompanyID"]);
                            xslArguments.Add("userID", eventInfo["userID"]);
                            xslArguments.Add("dateTime", eventInfo["dateTime"]);
                            xslArguments.Add("userName", eventInfo["userName"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                        case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                            xslArguments.Add(NotificationConstants.PublisherNameKey, eventInfo[NotificationConstants.PublisherNameKey]);
                            xslArguments.Add(NotificationConstants.BusinessNameKey, eventInfo[NotificationConstants.BusinessNameKey]);
                            xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                            xslArguments.Add("customerNo", eventInfo["customerNo"]);
                            xslArguments.Add("salesQuotationNo", eventInfo["salesQuotationNo"]);
                            xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                            xslArguments.Add("postingDate", eventInfo["postingDate"]);
                            xslArguments.Add("documentDate", eventInfo["documentDate"]);
                            xslArguments.Add("deliveryDate", eventInfo["deliveryDate"]);
                            xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                            xslArguments.Add("createdByNo", eventInfo["createdByNo"]);
                            xslArguments.Add(NotificationConstants.SubDomainKey, eventInfo[NotificationConstants.SubDomainKey]);
                            xslArguments.Add("portalUrl", eventInfo["portalUrl"]);
                            xslArguments.Add(NotificationConstants.CopyrightKey, eventInfo[NotificationConstants.CopyrightKey]);
                            break;
                    }
                    return xslArguments;
                }
                catch(Exception ex) {
                    StringBuilder exceptionDetail = new StringBuilder();
                    exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetXMLArgumentsForEmail:-");
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
                    emailPayload.UserLanguage = BusinessEntityConstants.DefaultLanguage;
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
                emailPayload.XSLTemplateContent = GetEmailXsltFile((BusinessEntityNotificationEventEnum)notificationPayload.EventId, emailPayload.UserLanguage);
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
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetEmailPayload:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return emailPayload;
        }

        #region Private Methods

        // Read email notification xsl template file content as string.
        private string GetEmailXsltFile(BusinessEntityNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetXSLTemplateFileName(eventId);
                string templatePath = Path.Combine(_appSettings.XslTemplateRootPath, "Email", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(templatePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetEmailXsltFile:-");
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
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.SetDeeplinkPayloadForEmail:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        // This method returns xsl file name that contains template name.
        private string GetXSLTemplateFileName(BusinessEntityNotificationEventEnum eventNumber) {
            switch(eventNumber) {
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                    return "BizUserNewARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                    return "CustUserNewARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                    return "BizUserNewBulkARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                    return "BizARInvoiceUpdated.xslt";
                case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                    return "BizUserCustomerAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateCutsomer:
                    return "BizUserCustomerUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                    return "BizUserSalesOrderAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizAddASN:
                    return "BizUserDraftDeliveryAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateASN:
                    return "BizUserDraftDeliveryUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddContract:
                    return "BizUserNewContractAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateContract:
                    return "BizUserNewContractUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddDelivery:
                    return "BizUserDeliveryAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                    return "BizUserDeliveryUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                    return "BizUserSalesQuotationAdd.xslt";
                default:
                    return "";
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
                    smsPayload.UserLanguage = BusinessEntityConstants.DefaultLanguage;
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
                smsPayload.XSLTemplateContent = GetASNotificationXsltFile((BusinessEntityNotificationEventEnum)notificationPayload.EventId, smsPayload.UserLanguage);
                smsPayload.InMemoryXSLTemplate = true;

                #endregion

            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetSMSPayload:-");
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
                switch((BusinessEntityNotificationEventEnum)payload.EventId) {
                    default:
                        return GetXSLArgumentsForASNotification(userDataRow, payload);
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetXSLArgumentsForSMS:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        private string GetSMSNotificationXsltFile(BusinessEntityNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetSMSNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetSMSNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetSMSNotificationXSLTemplateFileName(BusinessEntityNotificationEventEnum eventId) {
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
                switch((BusinessEntityNotificationEventEnum)payload.EventId) {
                    case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                        xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                        xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                        xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                        xslArguments.Add("dueDate", eventInfo["dueDate"]);
                        xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                        xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                        xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                        xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                        xslArguments.Add("dueDate", eventInfo["dueDate"]);
                        xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                        xslArguments.Add("newInvoiceCount", eventInfo["newInvoiceCount"]);
                        xslArguments.Add("updatedInvoiceCount", eventInfo["updatedInvoiceCount"]);
                        xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                        xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                        xslArguments.Add("invoiceNo", eventInfo["invoiceNo"]);
                        xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                        xslArguments.Add("updatedOn", eventInfo["updatedOn"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                        xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo["customerName"]);
                        xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                        xslArguments.Add(NotificationConstants.CreatedOnKey, eventInfo[NotificationConstants.CreatedOnKey]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizUpdateCutsomer:
                        xslArguments.Add("customerNo", eventInfo["customerNo"]);
                        xslArguments.Add(NotificationConstants.UpdatedByNameKey, eventInfo[NotificationConstants.UpdatedByNameKey]);
                        xslArguments.Add("updatedOn", eventInfo[NotificationConstants.UpdatedOnKey]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                        xslArguments.Add("salesOrderNo", eventInfo["salesOrderNo"]);
                        xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                        xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                        xslArguments.Add("postingDate", eventInfo["postingDate"]);
                        xslArguments.Add("deliveryDate", eventInfo["deliveryDate"]);
                        xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddASN:
                        xslArguments.Add("trackingNumber", eventInfo["trackingNumber"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("shipDate", eventInfo["shipDate"]);
                        xslArguments.Add("expectedDate", eventInfo["expectedDate"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);

                        break;
                    case BusinessEntityNotificationEventEnum.BizUpdateASN:
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddContract:
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("startDate", eventInfo["startDate"]);
                        xslArguments.Add("endDate", eventInfo["endDate"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizUpdateContract:
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("startDate", eventInfo["startDate"]);
                        xslArguments.Add("endDate", eventInfo["endDate"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddDelivery:
                        xslArguments.Add("trackingNumber", eventInfo["trackingNumber"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("shipDate", eventInfo["shipDate"]);
                        xslArguments.Add("expectedDate", eventInfo["expectedDate"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);

                        break;
                    case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                        xslArguments.Add("dateTime", eventInfo["dateTime"]);
                        xslArguments.Add("ID", eventInfo["ID"]);
                        xslArguments.Add("userName", eventInfo["userName"]);
                        xslArguments.Add("customerCompanyName", eventInfo["customerCompanyName"]);
                        break;
                    case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                        xslArguments.Add("salesQuotationNo", eventInfo["salesQuotationNo"]);
                        xslArguments.Add(NotificationConstants.CustomerNameKey, eventInfo[NotificationConstants.CustomerNameKey]);
                        xslArguments.Add("totalAmountWithCurrency", eventInfo["totalAmountWithCurrency"]);
                        xslArguments.Add("postingDate", eventInfo["postingDate"]);
                        xslArguments.Add("deliveryDate", eventInfo["deliveryDate"]);
                        xslArguments.Add(NotificationConstants.CreatedByNameKey, eventInfo[NotificationConstants.CreatedByNameKey]);
                        break;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetXSLArgumentsForASNotification:-");
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
                    asNotificationPayload.AppId = recepientDataRow.ApplicationId;
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

                    asNotificationPayload.XSLTemplateContent = GetASNotificationXsltFile((BusinessEntityNotificationEventEnum)notificationPayload.EventId, language);
                    asNotificationPayload.InMemoryXSLTemplate = true;

                    #endregion

                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetASNotificationPayload:-");
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

        private string GetASNotificationXsltFile(BusinessEntityNotificationEventEnum eventId, string regionLanguage) {
            string xsltTextFile = string.Empty;
            try {
                string templateFileName = GetASNotificationXSLTemplateFileName(eventId);

                string filePath = Path.Combine(_appSettings.XslTemplateRootPath, "AS", regionLanguage, templateFileName);
                xsltTextFile = FileHelper.ReadFileAsText(filePath);
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetASNotificationXsltFile:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
            return xsltTextFile;
        }

        private string GetASNotificationXSLTemplateFileName(BusinessEntityNotificationEventEnum eventId) {
            switch(eventId) {
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                    return "BizUserNewARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                    return "CustUserNewARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                    return "BizUserNewBulkARInvoiceIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                    return "BizARInvoiceUpdated.xslt";
                case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                    return "NewCustomerIsGenerated.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateCutsomer:
                    return "ExistingCustomerIsUpdated.xslt";
                case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                    return "BizUserSalesOrderAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizAddASN:
                    return "BizUserDraftDeliveryAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateASN:
                    return "BizUserDraftDeliveryUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddContract:
                    return "BizUserNewContractAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateContract:
                    return "BizUserNewContractUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddDelivery:
                    return "BizUserDeliveryAdd.xslt";
                case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                    return "BizUserDeliveryUpdate.xslt";
                case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                    return "BizUserSalesQuotationAdd.xslt";
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
                        switch((BusinessEntityNotificationEventEnum)notificationPayload.EventId) {

                            case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                            case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                            case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                            case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                            case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                            case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                            case BusinessEntityNotificationEventEnum.BizAddASN:
                            case BusinessEntityNotificationEventEnum.BizUpdateASN:
                            case BusinessEntityNotificationEventEnum.BizUpdateContract:
                            case BusinessEntityNotificationEventEnum.BizAddContract:
                            case BusinessEntityNotificationEventEnum.BizAddDelivery:
                            case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                            case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:

                                // Here We didn't fill DeeplinkInfoList because email should contains all url target to single
                                // application.
                                // deeplinkPayload.DeeplinkInfo = GetCustomerPaymentDoneByBusinessUserDeeplinkInfoList(notificationPayload, notificationPayload.EventId);
                                break;

                        }

                    }

                }
                return deeplinkPayload;
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetDeeplinkPayload:-");
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
                switch((BusinessEntityNotificationEventEnum)decisionParameters) {
                    default:
                        return "";
                        // _appSettingsOption.Value.PublisherBranchApiKey;
                }
            }
            catch(Exception ex) {
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in BusinessEntityNotificationService.GetBranchKey:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());
                throw;
            }
        }

        #region Private Methods


        #endregion

        #endregion

        #region Other Methods

        /// <inheritdoc/>
        public override NotificationDeliveryType GetSupportedNotificationType(long businessEntityNotificationEvents) {
            switch((BusinessEntityNotificationEventEnum)businessEntityNotificationEvents) {
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForBusinessUser:
                case BusinessEntityNotificationEventEnum.BizAddARInvoiceForCustomerUser:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.SMS | NotificationDeliveryType.ASNotification;

                case BusinessEntityNotificationEventEnum.BizBulkAddARInvoice:
                case BusinessEntityNotificationEventEnum.BizUpdateARInvoice:
                case BusinessEntityNotificationEventEnum.BizAddCutsomer:
                case BusinessEntityNotificationEventEnum.BizAddSalesOrder:
                case BusinessEntityNotificationEventEnum.BizAddASN:
                case BusinessEntityNotificationEventEnum.BizUpdateASN:
                case BusinessEntityNotificationEventEnum.BizAddContract:
                case BusinessEntityNotificationEventEnum.BizUpdateContract:
                case BusinessEntityNotificationEventEnum.BizAddDelivery:
                case BusinessEntityNotificationEventEnum.BizUpdateDelivery:
                case BusinessEntityNotificationEventEnum.BizAddSalesQuotation:
                default:
                    return NotificationDeliveryType.Email | NotificationDeliveryType.ASNotification;
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
