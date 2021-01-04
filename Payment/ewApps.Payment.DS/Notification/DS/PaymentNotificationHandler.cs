using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.NotificationService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.DS {
    public class PaymentNotificationHandler:IPaymentNotificationHandler {
        IPaymentNotificationService _paymentNotificationService;

        public PaymentNotificationHandler(IPaymentNotificationService paymentNotificationService) {
            _paymentNotificationService = paymentNotificationService;
        }

        //public async Task SendBizPaymentUserOnBoardNotificationAsync(BusinessOnBoardNotificationDTO businessOnBoardNotificationDTO, CancellationToken cancellationToken = default(CancellationToken)) {
        //    Dictionary<string, string> eventData = new Dictionary<string, string>();

        //    eventData.Add(NotificationConstants.AppIdKey, businessOnBoardNotificationDTO.AppId.ToString());
        //    eventData.Add(NotificationConstants.TenantIdKey, businessOnBoardNotificationDTO.BusinessTenantId.ToString());
        //    // ToDo: Replace it with some entity type enum.
        //    eventData.Add("asAppId", businessOnBoardNotificationDTO.AppId.ToString());
        //    eventData.Add("asTargetEntityType", "1");
        //    eventData.Add("asTargetEntityId", businessOnBoardNotificationDTO.OnBoardedUserId.ToString());
        //    eventData.Add("onboardedUserName", businessOnBoardNotificationDTO.UserName);
        //    eventData.Add("onboardedUserId", businessOnBoardNotificationDTO.OnBoardedUserId.ToString());
        //    eventData.Add("paymentAppName", businessOnBoardNotificationDTO.AppName);
        //    eventData.Add("onboardTime", businessOnBoardNotificationDTO.JoinedDate.ToString());
        //    eventData.Add("publisherName", businessOnBoardNotificationDTO.PublisherName);
        //    eventData.Add("businessName", businessOnBoardNotificationDTO.BusinessName);
        //    eventData.Add("copyrightText", businessOnBoardNotificationDTO.Copyright);

        //    // Creates list of dictionary for event data.
        //    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
        //    eventDataDict.Add("EventData", eventData);
        //    eventDataDict.Add("UserSession", MapNotitificationUserSession(businessOnBoardNotificationDTO.UserSessionInfo));

        //    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
        //    generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
        //    generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.NewBusinessPaymentUserIsOnboard;
        //    generateNotificationDTO.EventInfo = eventDataDict;
        //    generateNotificationDTO.LoggedinUserId = businessOnBoardNotificationDTO.OnBoardedUserId;
        //    generateNotificationDTO.UseCacheForTemplate = false;
        //    await _bizPaymentNotificationService.GenerateNotificationAsync(generateNotificationDTO, cancellationToken);
        //}

        public async Task GenerateTransactionVoidRequestedNotification(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/> 
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="transactionStatus"/> 
            //<xsl:param name="initiatedDate"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="userID"/> 
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionDate"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);


            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);

            DateTime initParseDate = DateTime.Parse(paymentNotificationDTO.TransactionDate);
            initParseDate = DateTime.SpecifyKind(initParseDate, DateTimeKind.Utc);
            initParseDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(initParseDate, paymentNotificationDTO.TimeZone, false);
            dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string initDate = DateTimeHelper.FormatDate(initParseDate, dtPickList.JSDateTimeFormat);

            eventData.Add("initiatedDate", initDate);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);
            eventData.Add("userID", paymentNotificationDTO.UserId);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);



            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizTransactionVoidRequested;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        public async Task GenerateTransactionRefundRequestedNotification(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/> 
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="transactionStatus"/> 
            //<xsl:param name="initiatedDate"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="userID"/> 
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionDate"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("initiatedDate", paymentNotificationDTO.TransactionDate);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);
            eventData.Add("userID", paymentNotificationDTO.UserId);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizTransactionRefundRequested;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        public async Task GenerateTransactionStatusChangedNotification(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="newTransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="oldTransactionStatus"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="newTransactionStatus"/>
            //<xsl:param name="oldTransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("newTransactionStatus", paymentNotificationDTO.NewTransactionStatus);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("oldTransactionStatus", paymentNotificationDTO.OldTransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizExistingTransactionStatusUpdated;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        #region Private Methods

        private NotificationUserSessionDTO MapNotitificationUserSession(UserSession userSession) {
            NotificationUserSessionDTO NotificationUserSessionDTO = new NotificationUserSessionDTO();
            NotificationUserSessionDTO.AppId = userSession.AppId;
            NotificationUserSessionDTO.ID = userSession.ID;
            NotificationUserSessionDTO.IdentityServerId = userSession.IdentityServerId;
            NotificationUserSessionDTO.IdentityToken = userSession.IdentityToken;
            NotificationUserSessionDTO.Subdomain = userSession.Subdomain;
            NotificationUserSessionDTO.TenantId = userSession.TenantId;
            NotificationUserSessionDTO.TenantName = userSession.TenantName;
            NotificationUserSessionDTO.TenantUserId = userSession.TenantUserId;
            NotificationUserSessionDTO.UserName = userSession.UserName;
            NotificationUserSessionDTO.UserType = userSession.UserType;
            return NotificationUserSessionDTO;
        }

        #endregion

        #region Payment Notification

        /// <summary>
        /// Send payment notification email to usr.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GeneratePaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="TransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);// .OldTransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("userName", paymentNotificationDTO.UserFullName);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);// .OldTransactionStatus);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);// .OldTransactionStatus);
            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);// .OldTransactionStatus);
            eventData.Add("customerId", paymentNotificationDTO.CustomerId);// .OldTransactionStatus);
            eventData.Add("userTypeText", paymentNotificationDTO.UserTypeText);// .OldTransactionStatus);
            eventData.Add("userID", paymentNotificationDTO.UserId);// .OldTransactionStatus);

            //paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
            //paymentNotificationDTO. = paymentRelatedDataDTO.AccountNumber;
            //paymentNotificationDTO. = paymentRelatedDataDTO.TransactionMode;
            //paymentNotificationDTO. = paymentRelatedDataDTO.TransactionService;

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();

            // Generate xml data for invoice list
            string xml = XMLSerialzer.Serialize(paymentNotificationDTO.InvoicePaymentList);

            eventDataDict.Add(NotificationConstants.EventDataXmlKey, xml);
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizARInvoicePaymentInitiated;

            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }


        // Note:- method by mundra G, commented by Rajesh as this noti was already done by rajesh.
        /*
                /// <summary>
                /// Send payment notification email to user.
                /// </summary>
                /// <param name="paymentNotificationDTO">Payment notification dto</param>
                /// <param name="moduleType">Currenct logged in module</param>
                /// <returns></returns>
                public async Task GeneratePaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

                    #region XML parameters email
                    //<xsl:param name="publisherCompanyName"/>
                    //<xsl:param name="transactionID"/>
                    //<xsl:param name="businessPartnerCompanyName"/>
                    //<xsl:param name="TransactionStatus"/>
                    //<xsl:param name="businessCompanyName"/>
                    //<xsl:param name="actionDate"/>
                    //<xsl:param name="businessPartnerCompanyID"/>
                    //<xsl:param name="transactionAmount"/>
                    //<xsl:param name="subDomain"/>
                    //<xsl:param name="portalURL"/> 
                    //<xsl:param name="copyrightText"/> 
                    #endregion XML parameters email

                    #region XML parameters AS
                    //<xsl:param name="transactionID"/>
                    //<xsl:param name="transactionAmount"/>
                    //<xsl:param name="businessPartnerCompanyName"/>
                    //<xsl:param name="actionDate"/>
                    //<xsl:param name="TransactionStatus"/>
                    //asTargetEntityType
                    //asTargetEntityId
                    //asAppId
                    #endregion XML parameters AS

                    Dictionary<string, string> eventData = new Dictionary<string, string>();

                    // Add XSLT parameters.
                    eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
                    eventData.Add("transactionID", paymentNotificationDTO.TransactionId);
                    eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
                    eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
                    eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
                    eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
                    eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
                    eventData.Add("TransactionStatus", paymentNotificationDTO.OldTransactionStatus);
                    eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
                    eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
                    eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);

                    eventData.Add("asTargetEntityType", "1");
                    eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
                    eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

                    // For getting recepients.
                    // For getting recepients.
                    eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
                    eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
                    eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

                    // Creates list of dictionary for event data.
                    Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
                    eventDataDict.Add("EventData", eventData);
                    eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

                    GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
                    generateNotificationDTO.ModuleId = (int)moduleType;
                    generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustARInvoicePaymentInitiated;

                    generateNotificationDTO.EventInfo = eventDataDict;
                    generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
                    generateNotificationDTO.UseCacheForTemplate = false;
                    generateNotificationDTO.NotificationToLoginUser = true;
                    await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
                }
        */

        /// <summary>
        /// Generate the notification object and send the notification email to all subscribed business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GeneratePreAuthPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.TransactionId);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("TransactionStatus", paymentNotificationDTO.OldTransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);

            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.PreAuthListUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;

            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizPreAuthorizationInitiated;

            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = true;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GeneratePreAuthPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.TransactionId);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("TransactionStatus", paymentNotificationDTO.OldTransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);

            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());
            //ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            //aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.PreAuthListUrl.ToString();
            //string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            //eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;

            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustPreAuthorizationInitiated;

            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = true;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        #endregion Payment Notification

        #region Advance Payment Notification

        /// <summary>
        /// Generate the notification object and send the notification email to all subscribed business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GenerateBizAdvancedPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>

            /*
                              <xsl:param name="transactionService"/>
 <xsl:param name="transactionMode"/>
 <xsl:param name="accountNumber"/>
 <xsl:param name="customerID"/>
 <xsl:param name="customerName"/>
 <xsl:param name="userID"/>
                            */
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);

            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("TransactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);

            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // For getting recepients.
            //eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            //eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            //eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizAdvancePaymentSecuredForBusiness;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GenerateBizAdvancedPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("TransactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);


            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);


            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;

            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.BizAdvancePaymentSecuredForCustomer;

            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = true;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        /// <summary>
        /// Generate the notification object and send the notification email to all subscribed business user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GenerateCustAdvancedPaymentNotificationForBusinessAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>

            /*
                              <xsl:param name="transactionService"/>
 <xsl:param name="transactionMode"/>
 <xsl:param name="accountNumber"/>
 <xsl:param name="customerID"/>
 <xsl:param name="customerName"/>
 <xsl:param name="userID"/>
                            */
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("TransactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);

            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // For getting recepients.
            //eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            //eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            //eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustAdvancePaymentSecuredForBusiness;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = true;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        /// <summary>
        /// Generate the notification object and send the payment notification email to subscribed customer user.
        /// </summary>
        /// <param name="paymentNotificationDTO">Payment notification model.</param>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public async Task GenerateCustAdvancedPaymentNotificationForCustomerAsync(PaymentNotificationDTO paymentNotificationDTO, ModuleTypeEnum moduleType, CancellationToken token = default(CancellationToken)) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="TransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>            
            //<xsl:param name="TransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);
            eventData.Add("applicationName", paymentNotificationDTO.PaymentApplicationName);
            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("TransactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);


            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);
            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);
            eventData.Add("customerName", paymentNotificationDTO.CustomerName);
            eventData.Add("userID", paymentNotificationDTO.PaidByUserIdentityNumber);
            eventData.Add("payeeName", paymentNotificationDTO.PaidByUserName);


            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)moduleType;

            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustAdvancePaymentSecuredForCustomer;

            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = true;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        #endregion Advance Payment Notification

        #region Customer Methods

        public async Task GenerateTransactionVoidRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO) {
            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/> 
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="transactionStatus"/> 
            //<xsl:param name="initiatedDate"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="userID"/> 
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionDate"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();
            DateTime actionDate = DateTime.Parse(paymentNotificationDTO.ActionDate);
            actionDate = DateTime.SpecifyKind(actionDate, DateTimeKind.Utc);
            actionDate = DateTimeHelper.ConvertUTCToLocalInGivenTimeZone(actionDate, paymentNotificationDTO.TimeZone, false);
            DateTimeFormatPicklist dtPickList = DateTimeFormatPicklist.GetById(paymentNotificationDTO.DateTimeFormat);
            string format = string.Format("{0} {1}", dtPickList.JSDateTimeFormat, Core.CommonService.Constants.DefaultTimeFormat);
            string formatDate = DateTimeHelper.FormatDate(actionDate, format);
            eventData.Add("actionDate", formatDate);

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            //eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("initiatedDate", paymentNotificationDTO.TransactionDate);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);
            eventData.Add("userID", paymentNotificationDTO.UserId);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustTransactionVoidRequested;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        public async Task GenerateTransactionStatusChangedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email

            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="newTransactionStatus"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="oldTransactionStatus"/>
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            //<xsl:param name="paymentApplicationName"></xsl:param

            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="newTransactionStatus"/>
            //<xsl:param name="oldTransactionStatus"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("newTransactionStatus", paymentNotificationDTO.NewTransactionStatus);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("oldTransactionStatus", paymentNotificationDTO.OldTransactionStatus);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("paymentApplicationName", paymentNotificationDTO.PaymentApplicationName);

            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustExistingTransactionStatusUpdated;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        public async Task GenerateTransactionRefundRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/> 
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="transactionStatus"/> 
            //<xsl:param name="initiatedDate"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="userID"/> 
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionDate"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("initiatedDate", paymentNotificationDTO.TransactionDate);
            eventData.Add("businessUserName", paymentNotificationDTO.UserFullName);
            eventData.Add("userName", paymentNotificationDTO.UserFullName);
            eventData.Add("userID", paymentNotificationDTO.UserId);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());
            eventData.Add("paymentApplicationName", paymentNotificationDTO.PaymentApplicationName);
            eventData.Add("customerID", paymentNotificationDTO.CustomerId);


            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionStatusUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add("EventData", eventData);
            eventDataDict.Add("UserSession", MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustTransactionRefundRequested;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        public async Task GenerateTransactionInitiateRequestedNotificationForCust(PaymentNotificationDTO paymentNotificationDTO) {

            #region XML parameters email
            //<xsl:param name="publisherCompanyName"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="transactionID"/>
            //<xsl:param name="businessCompanyName"/>
            //<xsl:param name="actionDate"/>
            //<xsl:param name="businessPartnerCompanyID"/> 
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="transactionStatus"/> 
            //<xsl:param name="initiatedDate"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="userID"/> 
            //<xsl:param name="subDomain"/>
            //<xsl:param name="portalURL"/> 
            //<xsl:param name="copyrightText"/> 
            #endregion XML parameters email

            #region XML parameters AS
            //<xsl:param name="transactionID"/>
            //<xsl:param name="transactionAmount"/>
            //<xsl:param name="businessPartnerCompanyName"/>
            //<xsl:param name="businessUserName"/>
            //<xsl:param name="actionDate"/>
            //asTargetEntityType
            //asTargetEntityId
            //asAppId
            #endregion XML parameters AS

            Dictionary<string, string> eventData = new Dictionary<string, string>();

            // Add XSLT parameters.
            eventData.Add("publisherCompanyName", paymentNotificationDTO.PublisherCompanyName);
            eventData.Add("businessPartnerCompanyName", paymentNotificationDTO.CustomerCompanyName);
            eventData.Add("transactionID", paymentNotificationDTO.IdentityNumber);
            eventData.Add("businessCompanyName", paymentNotificationDTO.BusinessCompanyName);
            eventData.Add("actionDate", paymentNotificationDTO.ActionDate);
            eventData.Add("businessPartnerCompanyID", paymentNotificationDTO.CustomerCompanyId);
            eventData.Add("transactionAmount", paymentNotificationDTO.TransactionAmountWithCurrency);
            eventData.Add("transactionStatus", paymentNotificationDTO.TransactionStatus);
            eventData.Add("initiatedDate", paymentNotificationDTO.TransactionDate);
            eventData.Add("userName", paymentNotificationDTO.UserFullName);
            eventData.Add("userID", paymentNotificationDTO.UserId);
            eventData.Add("subDomain", paymentNotificationDTO.SubDomain);
            eventData.Add("portalURL", paymentNotificationDTO.PortalURL);
            eventData.Add("copyrightText", paymentNotificationDTO.CopyRightText);
            eventData.Add("asTargetEntityType", ((int)EntityTypeEnum.Payment).ToString());
            eventData.Add("asTargetEntityId", paymentNotificationDTO.PaymentId.ToString());
            eventData.Add("asAppId", paymentNotificationDTO.AppId.ToString());
            eventData.Add("paymentApplicationName", paymentNotificationDTO.PaymentApplicationName);

            eventData.Add("accountNumber", paymentNotificationDTO.AccountNumber);// .OldTransactionStatus);
            eventData.Add("transactionMode", paymentNotificationDTO.TransactionMode);// .OldTransactionStatus);
            eventData.Add("transactionService", paymentNotificationDTO.TransactionService);// .OldTransactionStatus);
            eventData.Add("customerId", paymentNotificationDTO.CustomerId);// .OldTransactionStatus);
            eventData.Add("userTypeText", paymentNotificationDTO.UserTypeText);// .OldTransactionStatus);

            // For getting recepients.
            eventData.Add(NotificationConstants.TenantIdKey, paymentNotificationDTO.TenantId.ToString());
            eventData.Add(NotificationConstants.AppIdKey, paymentNotificationDTO.AppId.ToString());
            eventData.Add(NotificationConstants.BusinessPartnerTenantIdKey, paymentNotificationDTO.BusinessPartnerTenantId.ToString());

            ASNotificationAdditionalInfo aSNotificationAdditionalInfo = new ASNotificationAdditionalInfo();
            aSNotificationAdditionalInfo.NavigationKey = BizNavigationKeyEnum.TransactionReceiptUrl.ToString();
            string navigationJSON = Newtonsoft.Json.JsonConvert.SerializeObject(aSNotificationAdditionalInfo);
            eventData.Add(NotificationConstants.ASAdditionalInfoKey, navigationJSON);

            // Generate xml data for invoice list
            string xml = XMLSerialzer.Serialize(paymentNotificationDTO.InvoicePaymentList);

            // Creates list of dictionary for event data.
            Dictionary<string, object> eventDataDict = new Dictionary<string, object>();
            eventDataDict.Add(NotificationConstants.EventDataXmlKey, xml);
            eventDataDict.Add(NotificationConstants.EventDataKey, eventData);
            eventDataDict.Add(NotificationConstants.UserSessionKey, MapNotitificationUserSession(paymentNotificationDTO.UserSessionInfo));

            GenerateNotificationDTO generateNotificationDTO = new GenerateNotificationDTO();
            generateNotificationDTO.ModuleId = (int)ModuleTypeEnum.Business;
            generateNotificationDTO.EventId = (long)PaymentNotificationEventEnum.CustARInvoicePaymentInitiated;
            generateNotificationDTO.EventInfo = eventDataDict;
            generateNotificationDTO.LoggedinUserId = paymentNotificationDTO.LoginUserTenantUserId;
            generateNotificationDTO.UseCacheForTemplate = false;
            generateNotificationDTO.NotificationToLoginUser = false;
            await _paymentNotificationService.GenerateNotificationAsync(generateNotificationDTO);
        }

        #endregion Customer 

    }
}
