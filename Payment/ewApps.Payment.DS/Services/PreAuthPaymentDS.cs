/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 29 October 2019
 * 
 * Contributor/s: 
 * Last Updated On: 29 October 2019
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.Money;
using ewApps.Core.NotificationService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {
    /// <summary>
    /// Contains methos to authrozie some amount for future use.
    /// </summary>
    public class PreAuthPaymentDS:BaseDS<PreAuthPayment>, IPreAuthPaymentDS {

        #region Local variables

        IPreAuthPaymentRepository _paymentRep;
        IQPreAuthPaymentDS _qPreAuthPaymentDS;
        IUserSessionManager _sessionManager;
        IPaymentUnitOfWork _unitOfWork;
        PaymentAppSettings _appSetting;
        IPaymentAccess _entityAccess;
        IUniqueIdentityGeneratorDS _iIdentityNumbersDataService;
        IPaymentNotificationHandler _paymentNotificationHandler;
        IPaymentNotificationRecipientDS _paymentNotificationDS;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Constrtuctor with reporitory objects and setting object using DI.
        /// </summary>
        public PreAuthPaymentDS(IPaymentAccess entityAccess,
            IPreAuthPaymentRepository paymentRep,
            IQPreAuthPaymentDS qPreAuthPaymentDS,
            IUniqueIdentityGeneratorDS iIdentityNumbersDataService,
            IPaymentUnitOfWork unitOfWork,
            IPaymentNotificationHandler paymentNotificationHandler, IPaymentNotificationRecipientDS paymentNotificationDS, IUserSessionManager sessionManager, IOptions<PaymentAppSettings> appSetting) : base(paymentRep) {
            _entityAccess = entityAccess;
            _paymentRep = paymentRep;
            _qPreAuthPaymentDS = qPreAuthPaymentDS;
            _iIdentityNumbersDataService = iIdentityNumbersDataService;
            _unitOfWork = unitOfWork;
            _paymentNotificationHandler = paymentNotificationHandler;
            _paymentNotificationDS = paymentNotificationDS;
            _sessionManager = sessionManager;
            _appSetting = appSetting.Value;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get preauthorized payment history.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _qPreAuthPaymentDS.GetFilterTenantPaymentHistoryAsync(filter, token);
        }

        #endregion Get

        #region Make Payment

        /// <summary>
        /// Method will authorized some amount for card.
        /// </summary>
        /// <param name="payDto"> entity to be added</param>
        /// <param name="token"></param>
        public async Task<PaymentDQ> AddPreAuthPaymentAsync(AddPreAuthPaymentDTO payDto, CancellationToken token = default(CancellationToken)) {
            await ValidateAmount(payDto, token);
            Guid preAuthId = Guid.NewGuid();
            payDto.ID = preAuthId;
            PaymentDQ payDq = await AuthorizeCreditCradPaymentByCardTokenAsync(payDto, token);
            await AddPreAuthPaymentAsync(payDto, payDq, token);
            UserSession session = _sessionManager.GetSession();
            await GeneratePreAuthPaymentNotification(payDto, payDq.TenantId, payDq, session, token);

            return payDq;
        }

        /// <summary>
        /// Add payment.
        /// </summary>
        /// <param name="e"> entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddPreAuthPaymentAsync(AddPreAuthPaymentDTO e, PaymentDQ paymentDTO, CancellationToken token = default(CancellationToken)) {
            //Maps Payment entity from DTO              
            PreAuthPayment payment = new PreAuthPayment();
            DateTime todayDate = DateTime.Now.Date;
            payment.OriginationDate = todayDate.ToUniversalTime(); //paymentDTO.OriginationDate;
            int expirationDays = GetAuthorizeDays();
            payment.ExpirationDate = todayDate.AddDays(expirationDays).AddMinutes(-1).ToUniversalTime();
            payment.BACustomerId = e.userPaymentInfoModel.BACustomerId;
            payment.Description = e.Note;
            payment.NameOnCard = e.userPaymentInfoModel.Name;
            payment.CustomerAccountDetailId = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.ID;
            payment.BusinessId = e.BusinessId;
            payment.TenantId = e.TenantId;


            payment.Amount = e.AmountPaid;
            payment.AmountFC = e.AmountPaidFC;
            payment.RemainingAmount = e.AmountPaid;
            payment.RemainingAmountFC = e.AmountPaidFC;

            //payment.AppServiceId = e.Customer.AppServiceId;
            payment.AppServiceId = e.userPaymentInfoModel.AppServiceId;
            payment.AppServiceAttributeId = e.userPaymentInfoModel.AppServiceAttributeId;

            payment.PaymentTransectionCurrency = e.PaymentTransectionCurrency;
            payment.TransactionId = paymentDTO.VCTransactionId;
            payment.CardType = paymentDTO.CardType;
            payment.AuthCode = paymentDTO.AuthCode;
            string cardNumber = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
            cardNumber = MaskNumber(cardNumber);
            payment.CardNumber = GetEncryptValue(cardNumber);
            payment.NameOnCard = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.NameOnCard;
            payment.MaxTotalPaymentCount = 10; // e.MaxTotalPaymentCount == 0 ? 1 : e.MaxTotalPaymentCount;
            payment.CurrentPaymentSequenceNumber = 0;
            payment.Captured = PaymentCapturedStatusEnum.Open.ToString();

            int identityNumber = _iIdentityNumbersDataService.GetIdentityNo(Guid.Empty, 100, "TXN", 100001);
            payment.IdentityNumber = "TXN" + Convert.ToString(identityNumber);
            payment.Status = paymentDTO.Status;
            if(_sessionManager.GetSession() != null) {
                //Add Sytem default fields for add operation
                UpdateSystemFieldsByOpType(payment, OperationType.Add);
            }
            payment.ID = e.ID;
            payment.PayeeName = e.PayeeName;
            payment = await AddAsync(payment, token);
            _unitOfWork.SaveAll();

            return paymentDTO;
        }

        private DateTime GetStartDay() {
            return DateTime.Now.Date;
        }

        /// <summary>
        /// Mask the number by X.
        /// </summary>
        /// <param name="cardNumber">card number</param>
        /// <returns></returns>
        public string MaskNumber(string cardNumber) {
            if(!string.IsNullOrEmpty(cardNumber) && cardNumber.Length > 4) {
                string value = "";
                for(int i = 0; i < cardNumber.Length - 4; i++) {
                    value += "x";
                }
                cardNumber = value + cardNumber.Substring(cardNumber.Length - 4);
            }

            return cardNumber;
        }

        /// <summary>
        /// Number of days to expire authorize amount. 
        /// </summary>
        /// <returns>return number of days to expire amount.</returns>
        private int GetAuthorizeDays() {
            return 30;
        }

        #endregion Make Payment 

        #region Void PreAuth

        /// <summary>
        /// Void pre auth payment.
        /// </summary>
        /// <param name="prePaymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task voidPreAuthPaymentAsync(Guid prePaymentId, CancellationToken token = default(CancellationToken)) {
            PreAuthPayment payment = await _paymentRep.GetAsync(prePaymentId, token);
            if(payment != null) {

                Guid tenantId = _sessionManager.GetSession().TenantId;
                bool isRefund = false;
                TSysResponseDTO response = await voidTSysPaymentTransectionAsync(payment, prePaymentId, payment.Amount, tenantId, token);

                if(response != null && response.Transaction != null) {
                    //payment = await _paymentDS.GetAsync(paymentId, token);
                    if(payment != null) {
                        payment.Status = response.Transaction.Status; //PaymentStatusEnum.Voided.ToString();
                        base.UpdateSystemFieldsByOpType(payment, OperationType.Update);
                        await _paymentRep.UpdateAsync(payment, prePaymentId, token);
                    }
                    _unitOfWork.SaveAll();
                }
            }
        }

        private string GetRoundingValue(decimal value) {
            string val = value.ToString();
            if(!val.Contains(".")) {
                return val;
            }
            string amtStr = "";
            for(int index = val.Length - 1; index >= 0; index--) {
                if(val[index] == '0') {
                    continue;
                }
                else if(val[index] == '.' && amtStr == "") {
                    continue;
                }
                else {
                    amtStr += val[index];
                }
            }
            string mainValue = "";
            for(int index = amtStr.Length - 1; index >= 0; index--) {
                mainValue += amtStr[index];
            }

            return mainValue;
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<TSysResponseDTO> voidTSysPaymentTransectionAsync(PreAuthPayment payment, Guid paymentId, decimal amount, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string Method = "sales/return";
            CreditCardRefundDTO refDTO = new CreditCardRefundDTO();
            refDTO.transactionAmount = GetRoundingValue(amount);
            refDTO.paymentId = paymentId.ToString();
            refDTO.invoiceId = "";
            refDTO.transactionID = payment.TransactionId;
            CurrencyCultureInfo curInfo = GetCurrencyBySymbol(payment.PaymentTransectionCurrency);
            refDTO.currencyCode = curInfo.ShortName;
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", tenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, refDTO, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO res = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(res != null && res.Transaction == null && res.Status != "Success") {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Validation;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)ValidationErrorSubType.OutOfRange;
                errorData.Message = string.Format(res.ErrorMessage);
                error.EwpErrorDataList.Add(errorData);
                EwpValidationException exc = new EwpValidationException(errorData.Message, error.EwpErrorDataList);

                throw exc;
            }
            return res;
        }

        #endregion Void PreAuth

        #region API Call

        /// <summary>
        /// Authorized amount for a card.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AuthorizeCreditCradPaymentByCardTokenAsync(AddPreAuthPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            CreditCardPaymentTokenDTO model = new CreditCardPaymentTokenDTO();
            CreditCardPayAttrModel cardInfo = e.userPaymentInfoModel.selectedVCCreditCardPayAttr;
            model.expirationDate = cardInfo.ExpiryMonth + cardInfo.ExpiryYear;
            model.cvvNumber = cardInfo.CVV;
            model.transactionAmount = e.AmountPaid.ToString();
            model.paymentId = e.ID.ToString();
            model.invoiceId = "";
            model.Token = cardInfo.Token.ToString();
            CurrencyCultureInfo info = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            model.currencyCode = info.ShortName;
            string Method = "authorization/token";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GeneratePaymentDQFromTSysResponse(tSysRes, e);
        }

        private CurrencyCultureInfo GetCurrencyBySymbol(string currencySymbol) {
            Dictionary<CurrencyISOCode, CurrencyCultureInfo> info = new CurrencyCultureInfoTable(null).GetCurrencyCultureInfo();
            foreach(CurrencyCultureInfo curInfo in info.Values) {
                if(curInfo.Symbol == currencySymbol || curInfo.ShortName == currencySymbol)
                    return curInfo;
            }

            return null;
        }

        private PaymentDQ GeneratePaymentDQFromTSysResponse(TSysResponseDTO tSysRes, AddPreAuthPaymentDTO e) {
            PaymentDQ payDq = new PaymentDQ();
            payDq.AmountPaid = e.AmountPaid;
            payDq.Status = tSysRes.Transaction.Status;//"Sattled";
            payDq.PaymentType = "Credit";
            payDq.PaymentEntryType = "";
            payDq.TenantId = e.TenantId;
            payDq.BusinessId = e.BusinessId;
            payDq.ID = e.ID;
            payDq.InvoiceId = Guid.Empty;
            if(tSysRes.Response.SaleResponse != null) {
                payDq.AuthCode = tSysRes.Response.SaleResponse.authCode;
                payDq.VCTransactionId = tSysRes.Response.SaleResponse.transactionID;
                payDq.CardType = tSysRes.Response.SaleResponse.cardType;
            }
            else {
                payDq.CardType = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardType;
                // 
                if(tSysRes.Transaction != null) {
                    payDq.VCTransactionId = tSysRes.Transaction.CurrentTransactionId;
                }
            }

            return payDq;
        }

        #endregion API Call

        #region Support

        private bool IsValidJsonError(string value) {
            try {
                Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(value);
                return true;
            }
            catch(Exception) {
                return false;
            }
        }

        /// <summary>
        /// validating amount to pay.        
        /// </summary>
        /// <param name="payDto"></param>
        private async Task ValidateAmount(AddPreAuthPaymentDTO payDto, CancellationToken token = default(CancellationToken)) {
            if(payDto != null) {
                decimal paidAmount = payDto.AmountPaid;
                if(paidAmount <= 0) {
                    EwpError error = new EwpError();
                    error.ErrorType = ErrorType.Validation;
                    EwpErrorData errorData = new EwpErrorData();
                    errorData.ErrorSubType = (int)ValidationErrorSubType.OutOfRange;
                    errorData.Message = "Amount must be greater then 0.";
                    error.EwpErrorDataList.Add(errorData);
                    EwpValidationException exc = new EwpValidationException(errorData.Message, error.EwpErrorDataList);

                    throw exc;
                }

                DateTime todayDate = DateTime.Now.Date;
                DateTime expirationDate = todayDate.AddMinutes(-1).ToUniversalTime();
                PreAuthPaymentDTO preAuthDto = await _qPreAuthPaymentDS.GetPreAuthDetailByCardIdAsync(payDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.ID, expirationDate, token);
                if(preAuthDto != null && preAuthDto.MaxTotalPaymentCount > preAuthDto.CurrentPaymentSequenceNumber && preAuthDto.Status != PaymentStatusEnum.Voided.ToString() && preAuthDto.Captured != PaymentCapturedStatusEnum.Closed.ToString() && preAuthDto.Status != "Returned") {
                    EwpError error = new EwpError();
                    error.ErrorType = ErrorType.Validation;
                    EwpErrorData errorData = new EwpErrorData();
                    errorData.ErrorSubType = (int)ValidationErrorSubType.OutOfRange;
                    errorData.Message = "Card already authorized for payment.";
                    error.EwpErrorDataList.Add(errorData);
                    EwpValidationException exc = new EwpValidationException(errorData.Message, error.EwpErrorDataList);

                    throw exc;
                }
            }
        }

        private void RaiseError(string message) {
            EwpValidationException exValidation = new EwpValidationException(message);
            throw exValidation;
        }

        private void RaiseException(EwpError ewp, Exception ex) {
            switch(ewp.ErrorType) {
                case ErrorType.Validation:
                    string message = ewp.MessageList.Count > 0 ? ewp.MessageList[0] : "";
                    EwpValidationException valid = new EwpValidationException(message, ewp.EwpErrorDataList);
                    throw valid;
                default:
                    throw ex;
            }
        }

        private string GetEncryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Encrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        private string GetDecryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Decrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        #endregion Support

        #region Pre-Auth Notification

        private async Task GeneratePreAuthPaymentNotification(AddPreAuthPaymentDTO payDto, Guid tenantId, PaymentDQ payDq, UserSession userSession, CancellationToken token = default(CancellationToken)) {
            try {
                PreAuthPaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPreAuthPaymentNotificationInfo(payDq.ID, userSession.TenantUserId, AppKeyEnum.pay.ToString());
                await GeneratePreAuthPaymentNotificationForBusinessAsync(payDto, payDq, payDq.ID, tenantId, userSession, paymentRelatedDataDTO, token);
                await GeneratePreAuthPaymentNotificationForCustAsync(payDto, payDq, payDq.ID, tenantId, userSession, paymentRelatedDataDTO, token);
            }
            catch(Exception ex) {
            }
        }

        private async Task GeneratePreAuthPaymentNotificationForBusinessAsync(AddPreAuthPaymentDTO payDto, PaymentDQ payDq, Guid paymentId, Guid tenantId, UserSession userSession, PreAuthPaymentRelatedDataDTO paymentRelatedDataDTO, CancellationToken token = default(CancellationToken)) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
            paymentNotificationDTO.ActionDate = DateTime.UtcNow.ToString();
            paymentNotificationDTO.AppId = paymentRelatedDataDTO.AppId;
            paymentNotificationDTO.BusinessCompanyName = paymentRelatedDataDTO.BusinessCompanyName;
            paymentNotificationDTO.CopyRightText = paymentRelatedDataDTO.CopyRightText;
            paymentNotificationDTO.CustomerCompanyId = paymentRelatedDataDTO.CustomerCompanyId;
            paymentNotificationDTO.CustomerCompanyName = paymentRelatedDataDTO.CustomerCompanyName;
            paymentNotificationDTO.LoginUserTenantUserId = userSession.TenantUserId;
            paymentNotificationDTO.PortalURL = URLHelper.GetSubDomainURL(paymentRelatedDataDTO.SubDomain, _appSetting.PaymentPortalSubDomainBizURL);
            paymentNotificationDTO.PublisherCompanyName = paymentRelatedDataDTO.PublisherCompanyName;
            paymentNotificationDTO.SubDomain = paymentRelatedDataDTO.SubDomain;
            paymentNotificationDTO.TenantId = tenantId;
            paymentNotificationDTO.TransactionAmountWithCurrency = PaymentAndInvoiceDS.GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency);  // string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.TransactionId;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.Pending.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;

            paymentNotificationDTO.TransactionService = paymentRelatedDataDTO.ServiceName;
            paymentNotificationDTO.TransactionMode = paymentRelatedDataDTO.ServiceAttributeName;
            string cardNumber = payDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
            cardNumber = MaskNumber(cardNumber);
            paymentNotificationDTO.AccountNumber = cardNumber; //payDq.CustomerAccountNumber;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.ERPCustomerKey;
            paymentNotificationDTO.CustomerName = payDq.CustomerName;
            paymentNotificationDTO.PaidByUserIdentityNumber = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaidByUserName = payDto.PayeeName;// payeeName
            ModuleTypeEnum moduleType = ModuleTypeEnum.Business;
            await _paymentNotificationHandler.GeneratePreAuthPaymentNotificationForBusinessAsync(paymentNotificationDTO, moduleType);
        }

        private async Task GeneratePreAuthPaymentNotificationForCustAsync(AddPreAuthPaymentDTO payDto, PaymentDQ payDq, Guid paymentId, Guid tenantId, UserSession userSession, PreAuthPaymentRelatedDataDTO paymentRelatedDataDTO, CancellationToken token = default(CancellationToken)) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
            //PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
            paymentNotificationDTO.ActionDate = DateTime.UtcNow.ToString();
            paymentNotificationDTO.AppId = paymentRelatedDataDTO.AppId;
            paymentNotificationDTO.BusinessCompanyName = paymentRelatedDataDTO.BusinessCompanyName;
            paymentNotificationDTO.CopyRightText = paymentRelatedDataDTO.CopyRightText;
            paymentNotificationDTO.CustomerCompanyId = paymentRelatedDataDTO.CustomerCompanyId;
            paymentNotificationDTO.CustomerCompanyName = paymentRelatedDataDTO.CustomerCompanyName;
            paymentNotificationDTO.LoginUserTenantUserId = userSession.TenantUserId;
            paymentNotificationDTO.PortalURL = URLHelper.GetSubDomainURL(paymentRelatedDataDTO.SubDomain, _appSetting.PaymentPortalSubDomainCustomerURL);
            paymentNotificationDTO.PublisherCompanyName = paymentRelatedDataDTO.PublisherCompanyName;
            paymentNotificationDTO.SubDomain = paymentRelatedDataDTO.SubDomain;
            paymentNotificationDTO.TenantId = tenantId;
            paymentNotificationDTO.TransactionAmountWithCurrency = PaymentAndInvoiceDS.GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency);  //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.TransactionId;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.Pending.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.TransactionService = paymentRelatedDataDTO.ServiceName;
            paymentNotificationDTO.TransactionMode = paymentRelatedDataDTO.ServiceAttributeName;
            string cardNumber = payDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
            cardNumber = MaskNumber(cardNumber);
            paymentNotificationDTO.AccountNumber = cardNumber; //payDq.CustomerAccountNumber;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.ERPCustomerKey;
            paymentNotificationDTO.CustomerName = payDq.CustomerName;
            paymentNotificationDTO.PaidByUserIdentityNumber = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaidByUserName = payDto.PayeeName;// payeeName

            if(paymentRelatedDataDTO.BusinessPartnerTenantId != null)
                paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            else {
                paymentNotificationDTO.BusinessPartnerTenantId = Guid.Empty.ToString();
            }
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            ModuleTypeEnum moduleType = ModuleTypeEnum.Business;

            await _paymentNotificationHandler.GeneratePreAuthPaymentNotificationForCustomerAsync(paymentNotificationDTO, moduleType);
        }

        #endregion Pre-Auth Notification

    }
}
