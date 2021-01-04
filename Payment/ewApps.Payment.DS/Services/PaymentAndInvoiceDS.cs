/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.Common;
using ewApps.Core.CommonService;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {

    /// <summary>
    /// A wrapper class contains method to get payment and invoice list.    
    /// </summary>
    public class PaymentAndInvoiceDS:IPaymentAndInvoiceDS {

        #region Local variables

        IPaymentDataService _paymentDS;
        IQPaymentDS _qPaymentDS;
        IQInvoiceDS _qInvoiceDS;
        IUserSessionManager _sessionManager;
        PaymentAppSettings _appSetting;
        IPaymentUnitOfWork _unitOfWork;
        IPaymentLogDS _paymentLogDS;
        IPaymentInvoiceLinkingDataServices _payInvLinkService;
        IUniqueIdentityGeneratorDS _iIdentityNumbersDataService;
        ICreditCardAccountDS _creditCardAccountDS;
        IQInvoiceItemDS _qInvoiceItemDS;
        IPaymentNotificationHandler _paymentNotificationHandler;
        IPaymentNotificationRecipientDS _paymentNotificationDS;
        IPreAuthPaymentDS _preAuthPaymentDS;
        ILogger<PaymentAndInvoiceDS> _logger;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Constrtuctor with reporitory objects and setting object using DI.
        /// </summary>
        public PaymentAndInvoiceDS(ILogger<PaymentAndInvoiceDS> logger, IPaymentDataService paymentDS,
               IPaymentLogDS paymentLogDS,
               IPaymentInvoiceLinkingDataServices payInvLinkService,
               IUniqueIdentityGeneratorDS iIdentityNumbersDataService,
               IPaymentUnitOfWork unitOfWork,
               IUserSessionManager sessionManager,
               IQPaymentDS qPaymentDS, IQInvoiceDS qInvoiceDS, IQInvoiceItemDS qInvoiceItemDS,
               ICreditCardAccountDS creditCardAccountDS, IOptions<PaymentAppSettings> appSetting,
               IPaymentNotificationHandler paymentNotificationHandler, IPaymentNotificationRecipientDS paymentNotificationDS, IPreAuthPaymentDS preAuthPaymentDS) {
            _logger = logger;
            _paymentDS = paymentDS;
            _paymentLogDS = paymentLogDS;
            _payInvLinkService = payInvLinkService;
            _iIdentityNumbersDataService = iIdentityNumbersDataService;
            _unitOfWork = unitOfWork;
            _appSetting = appSetting.Value;
            _qPaymentDS = qPaymentDS;
            _qInvoiceDS = qInvoiceDS;
            _qInvoiceItemDS = qInvoiceItemDS;
            _creditCardAccountDS = creditCardAccountDS;
            _preAuthPaymentDS = preAuthPaymentDS;
            _sessionManager = sessionManager;
            _paymentNotificationHandler = paymentNotificationHandler;
            _paymentNotificationDS = paymentNotificationDS;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            BAARInvoiceViewDTO model = await _qInvoiceDS.GetInvoiceDetailByInvoiceIdAsync(invoiceId, token);
            if(model != null) {
                model.invoiceItems = await _qInvoiceItemDS.GetInvoiceItemsByInvoiceIdAsync(invoiceId, token);
            }

            return model;
        }

        /// <summary>
        /// Get customer by id.
        /// </summary>
        /// <param name="customerId">Unique customerid</param>
        /// <param name="token"></param>
        /// <returns>return customer entity.</returns>
        private async Task<BACustomerEntityDTO> GetCustomerEntityAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return null;
        }

        /// <summary>
        /// Get invoice by invoice id.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceid</param>
        /// <param name="token"></param>
        /// <returns>return invoice entity.</returns>
        private async Task<BAARInvoiceEntityDTO> GetInvoiceEntityAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _qInvoiceDS.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        // UserTenantLinking link = await _userTenantLinkingDS.GetPrimaryUserOfCustomerAsync(customer.BusinessPartnerTenantId, token);
        /// <summary>
        /// return primary user of a customer tenant.
        /// </summary>
        /// <param name="BusinessPartnerTenantId">Business partner tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfCustomerAsync(Guid BusinessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            try {
                return await _qInvoiceDS.GetPrimaryUserOfCustomerAsync(BusinessPartnerTenantId, token);
            }
            catch {
                return null;
            }
        }

        ///<summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            Guid tenantId = _sessionManager.GetSession().TenantId;
            return await _qInvoiceDS.GetInvoiceByTenantAsync(filter, tenantId, token);
        }

        /// <summary>
        /// Get invoice by invoiceId.
        /// </summary>
        /// <param name="invoiceId">Ids of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _qInvoiceDS.GetBAARInvoiceDTOListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            Guid tenantId = _sessionManager.GetSession().TenantId;
            return await _qInvoiceDS.GetCancelInvoiceByTenantAsync(filter, tenantId, token);
        }

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _qInvoiceDS.GetInvoiceByCustomerAsync(filter, token);
        }

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto with required field.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            Guid tenantId = _sessionManager.GetSession().TenantId;
            return await _qInvoiceDS.GetQuickPayInvoicesByTenantAsync(filter, tenantId, token);
        }

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _qInvoiceDS.GetCustomerPaybleinvoicesByCustomerIdAsync(customerId, token);
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetPaymentDTOAsync(id, invoiceId, token);
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetPaymentDTOAsyncByPaymentIdAsync(id, token);
        }

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetAdvancePaymentDTOAsync(paymentId, token);
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>  
        /// <param name="paymentId">PaymentId</param>
        /// <param name="token"></param>
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetInvoicesByPaymentIdAsync(paymentId, token);
        }

        #endregion Get

        #region Get Payment History

        /// <summary>
        /// Get tenant payment list filter by tenant and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _qPaymentDS.GetFilterTenantPaymentHistoryAsync(filter, token);
            return list;
        }

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Get the items in sorted order by created date and Payment Id.
            return await _qPaymentDS.GetCustomerFilterPaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetVoidFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Get the items in sorted order by created date and Payment Id.
            return await _qPaymentDS.GetVoidFilterTenantPaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Get the items in sorted order by created date and Payment Id.
            return await _qPaymentDS.GetSattledFilterTenantPaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>   
        public async Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            // Get the items in sorted order by created date and Payment Id.
            return await _qPaymentDS.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
        }

        #endregion Get Payment History

        #region Make Payment

        /// <summary>
        /// Adds Payment for mulitple invoices.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        public async Task<PaymentDetailDQ> AddPaymentsAsync(AddPaymentDTO[] e, CancellationToken token = default(CancellationToken)) {
            bool isIntegratedMode = false;
            List<InvoicePaymentDTO> invoicePaymentDTOs = new List<InvoicePaymentDTO>();
            List<PaymentDQ> list = new List<PaymentDQ>();
            PaymentDQ paymentInfo = null;
            BAARInvoiceEntityDTO invoice;
            int paymentCount = e.Length;
            AddPaymentDTO addPaymentDto = null;
            List<BAARInvoiceEntityDTO> listInvoice = new List<BAARInvoiceEntityDTO>();
            UserSession userSession = null;

            if (paymentCount > 0) {
                // Mapping AddPayment properties.
                addPaymentDto = await GenerateAddPaymentModel(e[0]);
            }

            #region Calculating SUM of payment for multiple invoice

            PaymentDetailDQ payDq = null;
            string loggeing = "";
            // Calculating the SUM of payment.
            for(int i = 0; i < paymentCount; i++) {
                InvoicePaymentDTO invoicePaymentDTO = new InvoicePaymentDTO();
                invoicePaymentDTO.ConversionRate = 1;
                invoice = await GetInvoiceEntityAsync(e[i].InvoiceId, token);
                // validating invoice amount to pay.
                if(e[i].PaymentAmountInBusinessCurrency) {
                    ValidateInvoicePaidAmount(invoice, e[i].AmountPaid, e[i].PaymentAmountInBusinessCurrency);
                }
                else {
                    ValidateInvoicePaidAmount(invoice, e[i].AmountPaidFC, e[i].PaymentAmountInBusinessCurrency);
                }
                if(invoice != null) {
                    invoicePaymentDTO.InvoiceId = invoice.ERPDocNum;
                    listInvoice.Add(invoice);
                }
                if(e[i].PaymentAmountInBusinessCurrency) {
                    addPaymentDto.AmountPaid += e[i].AmountPaid;
                    // calulating the amount is paying when currency pay in business currency.
                    if(e[i].LocalCurrency == e[i].CustomerCurrency) {
                        // if business and customer currency are same.
                        addPaymentDto.AmountPaidFC += e[i].AmountPaid;
                        e[i].AmountPaidFC = e[i].AmountPaid;
                    }
                    else {
                        //decimal conversionRate = addPaymentDto.TotalPaymentDueFC / e[i].TotalPaymentDue;
                        decimal conversionRate = invoice.TotalPaymentDueFC / invoice.TotalPaymentDue;
                        decimal fcAmt = e[i].AmountPaid * conversionRate;
                        addPaymentDto.AmountPaidFC += fcAmt;
                        e[i].AmountPaidFC = fcAmt;
                        invoicePaymentDTO.ConversionRate = conversionRate;
                    }
                    invoicePaymentDTO.ReceivedAmount = string.Format("{0} {1}", e[i].PaymentTransectionCurrency, e[i].AmountPaid.ToString());
                    invoicePaymentDTO.OrignalAmount = string.Format("{0} {1}", e[i].PaymentTransectionCurrency, e[i].TotalAmount.ToString());
                }
                else {
                    addPaymentDto.AmountPaidFC += e[i].AmountPaidFC;
                    // calulating the amount is paying when currency pay in customer currency.
                    if(e[i].LocalCurrency == e[i].CustomerCurrency) {
                        // if business and customer currency are same.
                        addPaymentDto.AmountPaid += e[i].AmountPaidFC;
                        e[i].AmountPaid = e[i].AmountPaidFC;
                    }
                    else {
                        //decimal conversionRate = addPaymentDto.TotalPaymentDueFC / e[i].TotalPaymentDue;
                        decimal conversionRate = invoice.TotalPaymentDueFC / invoice.TotalPaymentDue;
                        decimal amt = e[i].AmountPaidFC / conversionRate;
                        addPaymentDto.AmountPaid += amt;
                        e[i].AmountPaid = amt;
                        invoicePaymentDTO.ConversionRate = conversionRate;
                    }
                    invoicePaymentDTO.ReceivedAmount = string.Format("{0} {1}", e[i].PaymentTransectionCurrency, e[i].AmountPaidFC.ToString());
                    invoicePaymentDTO.OrignalAmount = string.Format("{0} {1}", e[i].PaymentTransectionCurrency, e[i].TotalAmountFC.ToString());
                }
                addPaymentDto.TotalAmount += e[i].TotalAmount;

                invoicePaymentDTOs.Add(invoicePaymentDTO);
            }

            #endregion Calculating SUM of payment for multiple invoice
            PreAuthPayment preAuthPayment = null;
            bool iscredicardPay = false;
            UserSession session = _sessionManager.GetSession();
            TenantInfo tInfo;
            if (paymentCount > 0) {
                //BACustomerEntityDTO customer = await GetCustomerEntityAsync(e[0].userPaymentInfoModel.BACustomerId);
                //if(customer == null) {
                //    return null;
                //}

                loggeing = string.Format("Invoice Payment initiated calling connector for invoice {0} and paymentid {0} ", e[0].InvoiceId.ToString(), addPaymentDto.ID.ToString());
                _logger.LogInformation(loggeing, null);
                if(addPaymentDto.userPaymentInfoModel.SelectedCustVCACHPayAttr != null) {
                    VeriCheckAddPaymentDTO vcPayModel = VeriCheckAddPaymentDTO.MapAddPaymentDTOtoVeriCheck(addPaymentDto);
                    // Add VC check payment Infomration.
                    paymentInfo = await AddVCPaymentAsync(vcPayModel);
                }
                else if(addPaymentDto.userPaymentInfoModel.PreAuthPaymentID != null && addPaymentDto.userPaymentInfoModel.PreAuthPaymentID.Value != Guid.Empty) {
                    preAuthPayment = await _preAuthPaymentDS.GetAsync(addPaymentDto.userPaymentInfoModel.PreAuthPaymentID.Value, token);
                    // Payment through PreAuthrorized payment.
                    paymentInfo = await AddPaymentByPreAuthPaymentAsync(addPaymentDto, preAuthPayment, token);
                    iscredicardPay = true;
                }
                else {
                    // process credit card payment.
                    if(string.IsNullOrEmpty(addPaymentDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.Token))
                        paymentInfo = await AddCreditCradPaymentAsync(addPaymentDto);
                    else
                        paymentInfo = await AddCreditCradPaymentByCardTokenAsync(addPaymentDto);
                    iscredicardPay = true;
                }
                loggeing = string.Format("Invoice Payment initiated sucess(connector) for invoice {0} and paymentid {1} ", e[0].InvoiceId.ToString(), addPaymentDto.ID.ToString());
                _logger.LogInformation(loggeing, null);

                // Adding payment transection information.
                PaymentDQ paymentDTO = await AddPaymentAsync(addPaymentDto, paymentInfo, preAuthPayment, e[0].userPaymentInfoModel.BusinessPartnerTenantId, token);

                Payment.Entity.Payment paymentEntity = await _paymentDS.GetAsync(paymentDTO.ID);

                #region Add payment invoice linking
                List<BAARInvoiceEntityDTO> updateInvoiceList = new List<BAARInvoiceEntityDTO>();
                // Adding payment information for invoices he paid.
                for(int i = 0; i < paymentCount; i++) {
                    invoice = listInvoice.Find(inv => inv.ID == e[i].InvoiceId); // await _invoiceDS.GetAsync(e[i].InvoiceId);
                    if(invoice == null) {
                        continue;
                    }
                    // Adding payment and updating invoice detail.                    
                    await AddPaymentInvoiceLinkingAsync(e[i], paymentInfo, invoice, token);
                    if(session != null) {
                        BAARInvoiceEntityDTO dto = this.GenerateUpdateInvoiceEntity(e[i], session.TenantUserId, invoice);
                        updateInvoiceList.Add(dto);
                    }
                    else {
                        Guid updateBy = paymentEntity.UpdatedBy != Guid.Empty ? paymentEntity.UpdatedBy : invoice.UpdatedBy;
                        BAARInvoiceEntityDTO dto = this.GenerateUpdateInvoiceEntity(e[i], updateBy, invoice);
                        updateInvoiceList.Add(dto);
                    }
                }
                tInfo = await _qInvoiceDS.GetBusinessInfoByTenantAsync(e[0].userPaymentInfoModel.TenantId, token);
                _logger.Log(LogLevel.Information, "START:  calling   UpdateInvoiceAfterPaymentAsync  ", null);
                if(tInfo != null && !tInfo.IntegratedMode) {
                    _logger.Log(LogLevel.Information, "calling   UpdateInvoiceAfterPaymentAsync NOT NULL  ", null);
                    // Updating invoice amount in case of standalone.
                    await UpdateInvoiceAfterPaymentAsync(updateInvoiceList, token);
                }
                isIntegratedMode = tInfo != null ? tInfo.IntegratedMode : isIntegratedMode;
                #endregion Add payment invoice linking

                // Update pre auth payment.
                if(preAuthPayment != null) {
                    preAuthPayment.RemainingAmount = preAuthPayment.RemainingAmount - paymentEntity.Amount;
                    if(preAuthPayment.RemainingAmount < 0) {
                        preAuthPayment.RemainingAmount = 0;
                    }
                    preAuthPayment.CurrentPaymentSequenceNumber = preAuthPayment.CurrentPaymentSequenceNumber + 1;
                    if(preAuthPayment.CurrentPaymentSequenceNumber == preAuthPayment.MaxTotalPaymentCount || preAuthPayment.RemainingAmount == 0) {
                        preAuthPayment.Captured = PaymentCapturedStatusEnum.Closed.ToString();
                    }
                    else if(addPaymentDto.userPaymentInfoModel.PartialPayment == "Y") {
                        preAuthPayment.Captured = PaymentCapturedStatusEnum.Partial.ToString();
                    }
                    else {
                        preAuthPayment.Captured = PaymentCapturedStatusEnum.Closed.ToString();
                    }
                    //preAuthPayment.Captured = addPaymentDto.userPaymentInfoModel.PartialPayment == "Y" ? "N" : "Y";
                    await _preAuthPaymentDS.UpdateAsync(preAuthPayment, preAuthPayment.ID, token);
                }

                // Saving payment information as transection.
                _unitOfWork.SaveAll();
                loggeing = string.Format("Invoice payment initiated sucessful(paymentid {0}) ", addPaymentDto.ID.ToString());
                _logger.LogInformation(loggeing, null);
                // Appening payment information.
                list.Add(paymentInfo);

                #region PaymentLog

                //Adding Payment Log Information 
                PaymentLog paymentLog = new PaymentLog();
                //ValidatePayment(payment, invoice);
                paymentLog.ID = Guid.NewGuid();
                paymentLog.CreatedOn = DateTime.UtcNow;
                paymentLog.UpdatedOn = paymentLog.CreatedOn;
                paymentLog.TenantId = e[0].TenantId;
                paymentLog.Deleted = false;
                paymentLog.ClientIP = e[0].ClientIP;
                paymentLog.ClientBrowser = e[0].ClientBrowser;
                paymentLog.ClientOS = e[0].ClientOS;
                paymentLog.PaymentId = paymentDTO.ID;

                /*
                TODO: Atul
                UserTenantLinking link = _userTenantLinkingDS.GetPrimaryUserOfCustomer(customer.BusinessPartnerTenantId);
                if(link != null) {
                    paymentLog.CreatedBy = link.TenantUserId;
                    paymentLog.UpdatedBy = link.TenantUserId;
                }


                //Add Tracing Information In payment Log Table 
                await _paymentLogDS.AddPaymentLogAsync(paymentLog);
                _paymentLogDS.Save();
                */

                #endregion PaymentLog

                if(addPaymentDto.SaveAccountInfo && addPaymentDto.CustomerAccountId == Guid.Empty) {
                     userSession = _sessionManager.GetSession();
                    if(userSession != null) {
                        await SaveCustomerAccountInfoAsync(addPaymentDto, e[0].userPaymentInfoModel.BusinessPartnerTenantId, e[0].userPaymentInfoModel.TenantId, userSession.AppId, token);
                    }
                }

                try {

                    if(tInfo != null && tInfo.IntegratedMode) {
                        // Notify SAP for invoice payment.
                        await PushPaymentToSAPAsync(addPaymentDto, e, listInvoice, paymentEntity, iscredicardPay,token);
                    }

                    #region Notification

                     userSession = _sessionManager.GetSession();
                    payDq = await _qPaymentDS.GetPaymentHistoryByPaymentIdAsync(addPaymentDto.ID, token);

                    // generate payment related notificaitons for both business and customer users.
                    // if session is numm i.e. it is the case of quick pay so generate  session here.

                    if(userSession == null){
                       userSession = GenerateSessionObject(paymentInfo.TenantId, paymentEntity.CreatedBy, (int)UserTypeEnum.Customer, Guid.Empty, tInfo.Subdomain, "", tInfo.Name);
                    }

                    GeneratePaymentNotificationForBusinessAsync(paymentEntity.ID, paymentEntity.TenantId, userSession, invoicePaymentDTOs, payDq);
                    GenerateTransactionIntiatePaymentNotificationForCustAsync(paymentEntity.ID, paymentEntity.TenantId, userSession, invoicePaymentDTOs, payDq);




          /*
                          if (userSession != null) {

                            if (userSession.UserType == (int)UserTypeEnum.Business) {
                              // Ishwer to handle
                              //await GeneratePaymentDoneByBusinessNotification(customer, invoice, paymentEntity, e[i].AmountPaid, _userTenantLinkingDS.GetPrimaryUserOfCustomer(customer.BusinessPartnerTenantId).TenantUserId);
                            }
                            else if (userSession.UserType == (int)UserTypeEnum.Customer)
                            {
                              // Ishwer to handle
                              //await GeneratePaymentDoneByCustomerNotification(customer, invoice, paymentEntity, e[i].AmountPaid, _userTenantLinkingDS.GetPrimaryUserOfCustomer(customer.BusinessPartnerTenantId).TenantUserId);
                            }
                          }

                          else {
                            // Ishwer to handle
                            //await GeneratePaymentDoneByCustomerNotification(customer, invoice, paymentEntity, e[i].AmountPaid, _userTenantLinkingDS.GetPrimaryUserOfCustomer(customer.BusinessPartnerTenantId).TenantUserId);
                          }
          */

          #endregion Notification

          //UserSession userSession = _sessionManager.GetSession();
          //// ToDo: Discuss with amit sir and nitin sir about session to be passed null, in case of payment when session is not available.
          //GenerateTransactionIntiatePaymentNotificationForCust(paymentEntity.ID, paymentEntity.TenantId, userSession, invoicePaymentDTOs);
        }
        catch (Exception ex) { }
                try {
                    if(iscredicardPay) {
                        // TODO: Remove once implemented Schedular. 
                        await PushTSysPaymentBatchUpdateAsync(token);
                    }
                }
                catch(Exception ex) { }
            }

            if(payDq != null)
                payDq.IntegratedMode = isIntegratedMode;

            return payDq;
        }

        public const string PaymentIdentityPrefix = "TXN";

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddPaymentAsync(AddPaymentDTO e, PaymentDQ paymentDTO, PreAuthPayment preAuthPayment, Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            //Maps Payment entity from DTO  
            //ewApps.Payment.Entity.Payment payment = _mapper.Map<ewApps.Payment.Entity.Payment>(e);
            Payment.Entity.Payment payment = new Payment.Entity.Payment();
            payment.OriginationDate = paymentDTO.OriginationDate;
            payment.PartnerId = e.PartnerId;
            payment.Description = e.Note;
            payment.CustomerName = e.userPaymentInfoModel.Name;
            payment.CheckImageBack = e.CheckImageBack;
            payment.CheckImageFront = e.CheckImageFront;
            payment.BusinessId = e.BusinessId;
            payment.CheckNumber = e.CheckNumber.ToString();
            payment.Type = e.PaymentType;
            payment.TenantId = e.TenantId;
            payment.PaymentType = e.TransectionPaymentType;
            payment.PaymentTransectionCurrency = e.PaymentTransectionCurrency;
            payment.TransactionId = paymentDTO.VCTransactionId;
            payment.CardType = paymentDTO.CardType;
            payment.AuthCode = paymentDTO.AuthCode;

            payment.Amount = e.AmountPaid;
            payment.AmountFC = e.AmountPaidFC;
            //payment.AppServiceId = e.Customer.AppServiceId;
            payment.AppServiceId = e.AppServiceId;
            payment.AppServiceAttributeId = e.AppServiceAttributeId;
            payment.PreAuthPaymentID = e.userPaymentInfoModel.PreAuthPaymentID;

            if(e.userPaymentInfoModel.SelectedCustVCACHPayAttr != null) {
                string acNumber = MaskNumber(e.userPaymentInfoModel.SelectedCustVCACHPayAttr.AccountNo);
                payment.CustomerAccountNumber = GetEncryptValue(acNumber);
                payment.CustomerAccountType = e.userPaymentInfoModel.SelectedCustVCACHPayAttr.AccountType == ewApps.Payment.Common.Constants.SavingAccount ? 0 : 1;
                payment.CustomerName = e.userPaymentInfoModel.SelectedCustVCACHPayAttr.NameInBank;
                payment.CustomerRoutingNumber = GetEncryptValue(e.userPaymentInfoModel.SelectedCustVCACHPayAttr.ABARounting);
            }
            else if(e.userPaymentInfoModel.PreAuthPaymentID != null && e.userPaymentInfoModel.PreAuthPaymentID.Value != Guid.Empty) {
                // card already saved in masked form so no need to encrypt.
                payment.CustomerAccountNumber = preAuthPayment.CardNumber;
                payment.CustomerAccountType = (int)BankAccountTypeEnum.CreditCard;
                payment.CustomerName = preAuthPayment.NameOnCard;
                payment.CustomerRoutingNumber = "";
            }
            else {
                string cardNumber = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
                cardNumber = MaskNumber(cardNumber);
                payment.CustomerAccountNumber = GetEncryptValue(cardNumber);
                payment.CustomerAccountType = (int)BankAccountTypeEnum.CreditCard;
                payment.CustomerName = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.NameOnCard;
                payment.CustomerRoutingNumber = "";
            }

            int identityNumber = _iIdentityNumbersDataService.GetIdentityNo(Guid.Empty, 100, PaymentIdentityPrefix, 100001);
            payment.IdentityNumber = PaymentIdentityPrefix + Convert.ToString(identityNumber);
            payment.Status = paymentDTO.Status;
            payment.Type = paymentDTO.PaymentType;
            //ValidatePayment(payment, invoice);
            if(_sessionManager.GetSession() != null) {
                //Add Sytem default fields for add operation
                _paymentDS.UpdateSystemFieldsByOpType(payment, OperationType.Add);
            }
            else {
                payment.CreatedOn = DateTime.UtcNow;
                payment.UpdatedOn = payment.CreatedOn;
                UserTenantLinkingEntityDTO link = await GetPrimaryUserOfCustomerAsync(businessPartnerTenantId, token);
                if(link != null) {
                    payment.CreatedBy = link.TenantUserId;
                    payment.UpdatedBy = link.TenantUserId;
                }
            }
            payment.ID = e.ID;
            payment.PayeeName = e.PayeeName;
            payment = await _paymentDS.AddAsync(payment, token);

            // Adding payment information.
            //await AddDocumentCurrencyForRecurringAsync(payment, e, token);

            return paymentDTO;
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

        #endregion Make Payment

        #region Advanced Payment

        /// <summary>
        /// Add advance Payment for mulitple invoices.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        public async Task<PaymentDetailDQ> AddAdvancePaymentsAsync(AddAdvancedPaymentDTO e, CancellationToken token = default(CancellationToken)) {

            List<PaymentDQ> list = new List<PaymentDQ>();
            PaymentDQ paymentInfo = null;
            AddAdvancedPaymentDTO addPaymentDto = e;
            List<BAARInvoiceEntityDTO> listInvoice = new List<BAARInvoiceEntityDTO>();
            e.ID = Guid.NewGuid();
            addPaymentDto.TotalAmount = e.AmountPaid;
            bool iscredicardPay = false;
            string formatLog = string.Format("Advance Payment initiated(calling connector) Paymentid {0} ", addPaymentDto.ID.ToString());
            _logger.LogInformation(formatLog, null);
            if(addPaymentDto.userPaymentInfoModel.SelectedCustVCACHPayAttr != null) {
                VeriCheckAddPaymentDTO vcPayModel = VeriCheckAddPaymentDTO.MapAdvancePaymentDTOtoVeriCheck(addPaymentDto);
                // Add VC check payment Infomration.
                paymentInfo = await AddVCPaymentAsync(vcPayModel);
            }
            else {
                iscredicardPay = true;
                // process credit card payment.
                if(string.IsNullOrEmpty(addPaymentDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.Token))
                    paymentInfo = await PayAdvancePayByCreditCradAsync(addPaymentDto);
                else
                    paymentInfo = await PayAdvanceCreditCradPaymentByCardTokenAsync(addPaymentDto);
            }
            formatLog = string.Format("Advance Payment initiated(calling connector) success Paymentid {0} ", addPaymentDto.ID.ToString());            _logger.LogInformation(formatLog, null);
            _logger.LogInformation(formatLog, null);
            // Adding payment transection information.
            PaymentDQ paymentDTO = await AddAdvancePaymentAsync(addPaymentDto, paymentInfo, e.userPaymentInfoModel.BusinessPartnerTenantId, token);
            Payment.Entity.Payment paymentEntity = await _paymentDS.GetAsync(paymentDTO.ID);
            formatLog = string.Format("Advance Payment row created Paymentid {0} ", addPaymentDto.ID.ToString());
            _logger.LogInformation(formatLog, null);

            #region Notification

            #endregion Notification

            // Saving payment information as transection.
            _unitOfWork.SaveAll();
            formatLog = string.Format("Advance Payment complete success Paymentid {0} ", addPaymentDto.ID.ToString());
            _logger.LogInformation(formatLog, null);
            // Appening payment information.
            list.Add(paymentInfo);

            #region PaymentLog

            //Adding Payment Log Information 
            PaymentLog paymentLog = new PaymentLog();
            //ValidatePayment(payment, invoice);
            paymentLog.ID = Guid.NewGuid();
            paymentLog.CreatedOn = DateTime.UtcNow;
            paymentLog.UpdatedOn = paymentLog.CreatedOn;
            paymentLog.TenantId = e.TenantId;
            paymentLog.Deleted = false;
            paymentLog.ClientIP = e.ClientIP;
            paymentLog.ClientBrowser = e.ClientBrowser;
            paymentLog.ClientOS = e.ClientOS;
            paymentLog.PaymentId = paymentDTO.ID;

            /*
            TODO: Atul
            UserTenantLinking link = _userTenantLinkingDS.GetPrimaryUserOfCustomer(customer.BusinessPartnerTenantId);
            if(link != null) {
                paymentLog.CreatedBy = link.TenantUserId;
                paymentLog.UpdatedBy = link.TenantUserId;
            }


            //Add Tracing Information In payment Log Table 
            await _paymentLogDS.AddPaymentLogAsync(paymentLog);
            _paymentLogDS.Save();
            */

            #endregion PaymentLog

            PaymentDetailDQ payDq = await _qPaymentDS.GetAdvancePaymentHistoryByPaymentIdAsync(paymentDTO.ID, token);
            await GenerateAdvancedPaymentNotification(paymentEntity.ID, paymentEntity.TenantId, payDq, _sessionManager.GetSession(), token);

            //PaymentDetailDQ payDq = await _qPaymentDS.GetAdvancePaymentHistoryByPaymentIdAsync(paymentDTO.ID, token);
            try {
                if(iscredicardPay) {
                    // TODO: Remove once implemented Schedular. 
                    await PushTSysPaymentBatchUpdateAsync(token);
                }
            }
            catch(Exception ex) { }
            return payDq;
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddAdvancePaymentAsync(AddAdvancedPaymentDTO e, PaymentDQ paymentDTO, Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            //Maps Payment entity from DTO  
            //ewApps.Payment.Entity.Payment payment = _mapper.Map<ewApps.Payment.Entity.Payment>(e);
            Payment.Entity.Payment payment = new Payment.Entity.Payment();
            payment.OriginationDate = paymentDTO.OriginationDate;
            payment.PartnerId = e.PartnerId;
            payment.Description = e.Note;
            payment.CustomerName = e.userPaymentInfoModel.Name;
            payment.CheckImageBack = e.CheckImageBack;
            payment.CheckImageFront = e.CheckImageFront;
            payment.BusinessId = e.BusinessId;
            payment.CheckNumber = e.CheckNumber.ToString();
            payment.Type = e.PaymentType;
            payment.TenantId = e.TenantId;
            payment.PaymentType = e.TransectionPaymentType;
            payment.PaymentTransectionCurrency = e.PaymentTransectionCurrency;
            payment.TransactionId = paymentDTO.VCTransactionId;
            payment.CardType = paymentDTO.CardType;
            payment.AuthCode = paymentDTO.AuthCode;

            payment.Amount = e.AmountPaid;
            payment.AmountFC = e.AmountPaidFC;
            //payment.AppServiceId = e.Customer.AppServiceId;
            payment.AppServiceId = e.userPaymentInfoModel.AppServiceId;
            payment.AppServiceAttributeId = e.userPaymentInfoModel.AppServiceAttributeId;

            if(e.userPaymentInfoModel.SelectedCustVCACHPayAttr != null) {
                string acNumber = MaskNumber(e.userPaymentInfoModel.SelectedCustVCACHPayAttr.AccountNo);
                payment.CustomerAccountNumber = GetEncryptValue(acNumber);
                payment.CustomerAccountType = e.userPaymentInfoModel.SelectedCustVCACHPayAttr.AccountType == ewApps.Payment.Common.Constants.SavingAccount ? 0 : 1;
                payment.CustomerName = e.userPaymentInfoModel.SelectedCustVCACHPayAttr.NameInBank;
                payment.CustomerRoutingNumber = GetEncryptValue(e.userPaymentInfoModel.SelectedCustVCACHPayAttr.ABARounting);
            }
            else {
                string cardNumber = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
                cardNumber = MaskNumber(cardNumber);
                payment.CustomerAccountNumber = GetEncryptValue(cardNumber);
                payment.CustomerAccountType = (int)BankAccountTypeEnum.CreditCard;
                payment.CustomerName = e.userPaymentInfoModel.selectedVCCreditCardPayAttr.NameOnCard;
                payment.CustomerRoutingNumber = "";
            }

            int identityNumber = _iIdentityNumbersDataService.GetIdentityNo(Guid.Empty, 100, PaymentIdentityPrefix, 100001);
            payment.IdentityNumber = PaymentIdentityPrefix + Convert.ToString(identityNumber);
            payment.Status = paymentDTO.Status;
            payment.Type = paymentDTO.PaymentType;
            //ValidatePayment(payment, invoice);
            if(_sessionManager.GetSession() != null) {
                //Add Sytem default fields for add operation
                _paymentDS.UpdateSystemFieldsByOpType(payment, OperationType.Add);
            }
            else {
                payment.CreatedOn = DateTime.UtcNow;
                payment.UpdatedOn = payment.CreatedOn;
                UserTenantLinkingEntityDTO link = await GetPrimaryUserOfCustomerAsync(businessPartnerTenantId, token);
                if(link != null) {
                    payment.CreatedBy = link.TenantUserId;
                    payment.UpdatedBy = link.TenantUserId;
                }
            }
            payment.ID = e.ID;
            payment.PayeeName = e.PayeeName;
            payment = await _paymentDS.AddAsync(payment, token);

            // Adding payment information.
            //await AddDocumentCurrencyForRecurringAsync(payment, e, token);

            return paymentDTO;
        }

        #endregion Advanced Payment

        #region Get Advance Payment

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetFilterTenantAdvancePaymentHistoryAsync(filter, token);
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _qPaymentDS.GetCustomerAdvanceFilterPaymentHistoryAsync(filter, token);
        }


        #endregion Get Advance Payment

        #region Payment Support Method

        /// <summary>
        /// Get currency amount.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string GetFormattedAmount(decimal value, string currency) {
            try {
                string decValue = value.ToString();
                string[] splitVal = decValue.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if(splitVal.Length > 1) {
                    splitVal[1] = GetValueByReplacingSuffixZeroValue(splitVal[1]);
                    if(splitVal[1] == "" || splitVal[1].Length == 1) {
                        return string.Format("{0} {1:F2}", currency, value);
                    }
                    double d = Truncate(Convert.ToDouble(decValue), splitVal[1].Length > 1 ? splitVal[1].Length : 2);
                    return string.Format("{0} {1}", currency, d);
                }
                return string.Format("{0} {1}", currency, decValue);
            }
            catch { }
            return string.Format("{0} {1}", currency, value);
        }

        private static string GetValueByReplacingSuffixZeroValue(string value) {
            string assignValue = "";
            for(int i = value.Length - 1; i >= 0; i--) {
                if(!string.IsNullOrEmpty(assignValue)) {
                    assignValue += value[i];
                }
                else if(value[i] != '0') {
                    assignValue += value[i];
                }
            }

            char[] charArray = assignValue.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// To formate double value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static double Truncate(double value, int digits) {
            double mult = System.Math.Pow(10.0, digits);
            return System.Math.Truncate(value * mult) / mult;
        }

        /// <summary>
        /// Save customer account information for future use.
        /// </summary>
        /// <param name="addPaymentDto">AddPaymentDTO model</param>
        /// <param name="cust">customer object.</param>
        /// <param name="appId">applicationid</param>
        /// <param name="token"></param>
        private async Task SaveCustomerAccountInfoAsync(AddPaymentDTO addPaymentDto, Guid businessPartnerTenantId, Guid customerTenantId, Guid appId, CancellationToken token = default(CancellationToken)) {
            try {

                /*
                AppServiceAccountDetail appServiceAccountDetail = new AppServiceAccountDetail();
                _serviceAccountDetailDS.UpdateSystemFieldsByOpType(appServiceAccountDetail, OperationType.Add);
                CustVCACHPayAttrDTO custInfo = new CustVCACHPayAttrDTO();
                custInfo.ABARounting = addPaymentDto.Customer.ABARoutingNumber;
                custInfo.AccountNo = addPaymentDto.Customer.AccountNumber;
                custInfo.NameInBank = addPaymentDto.Customer.BankAccountName;
                custInfo.BankName = addPaymentDto.Customer.BankName;
                custInfo.SECCode = addPaymentDto.Customer.AccountType.ToString();
                CryptoHelper cryptoHelper = new CryptoHelper();
                string str = Newtonsoft.Json.JsonConvert.SerializeObject(custInfo);
                appServiceAccountDetail.AccountJson = cryptoHelper.Encrypt(str, Core.Common.Constants.DefaultEncryptionAlgo);
                appServiceAccountDetail.AppId = appId;
                appServiceAccountDetail.Deleted = false;
                //appServiceAccountDetail.EntityId = cust.BusinessPartnerTenantId;
                appServiceAccountDetail.EntityType = (int)CoreEntityTypeEnum.Customer;
                appServiceAccountDetail.ID = Guid.NewGuid();
                appServiceAccountDetail.ServiceAttributeId = addPaymentDto.AppServiceAttributeId; //attributes.ID;
                appServiceAccountDetail.ServiceId = addPaymentDto.AppServiceId;
                appServiceAccountDetail.TenantId = cust.TenantId;
                _serviceAccountDetailDS.Add(appServiceAccountDetail);
                _serviceAccountDetailDS.Save();*/
            }
            catch { }
        }

        /// <summary>
        /// Generate payment model with all basic information for multiple invoice payment.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task<AddPaymentDTO> GenerateAddPaymentModel(AddPaymentDTO e) {
            AddPaymentDTO addPaymentDto = new AddPaymentDTO();
            addPaymentDto.ID = Guid.NewGuid();
            addPaymentDto.PayeeName = e.PayeeName;
            addPaymentDto.InvoiceId = e.InvoiceId;
            addPaymentDto.userPaymentInfoModel = e.userPaymentInfoModel;
            addPaymentDto.TransectionPaymentType = e.TransectionPaymentType;
            addPaymentDto.PaymentTransectionCurrency = e.PaymentTransectionCurrency;
            addPaymentDto.PaymentAmountInBusinessCurrency = e.PaymentAmountInBusinessCurrency;
            addPaymentDto.PartnerId = e.PartnerId;
            addPaymentDto.BusinessId = e.BusinessId;
            addPaymentDto.Note = e.Note;
            addPaymentDto.TenantId = e.TenantId;
            addPaymentDto.CheckNumber = e.CheckNumber;
            addPaymentDto.CheckImageBack = e.CheckImageBack;
            addPaymentDto.CheckImageFront = e.CheckImageFront;
            addPaymentDto.OriginationDate = e.OriginationDate;
            addPaymentDto.PaymentEntryType = e.PaymentEntryType;
            addPaymentDto.PaymentType = e.PaymentType;

            //addPaymentDto.FromCurrencyCode = e.FromCurrencyCode;
            //addPaymentDto.ToCurrencyCode = e.ToCurrencyCode;
            //addPaymentDto.ExchangeRate = e.ExchangeRate;

            addPaymentDto.AppServiceId = e.userPaymentInfoModel.AppServiceId;
            addPaymentDto.AppServiceAttributeId = e.userPaymentInfoModel.AppServiceAttributeId;

            addPaymentDto.CustomerAccountId = e.CustomerAccountId;
            addPaymentDto.SaveAccountInfo = e.SaveAccountInfo;

            return addPaymentDto;
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddPaymentInvoiceLinkingAsync(AddPaymentDTO e, PaymentDQ paymentDTO, BAARInvoiceEntityDTO invoice, CancellationToken token = default(CancellationToken)) {
            //Maps Payment entity from DTO  
            PaymentInvoiceLinking payment = new PaymentInvoiceLinking();
            payment.InvoiceId = e.InvoiceId;
            payment.Amount = e.AmountPaid;
            payment.AmountFC = e.AmountPaidFC;
            payment.PaymentId = paymentDTO.ID;
            payment.TenantId = e.TenantId;
            UserSession session = this._sessionManager.GetSession();
            if(session != null) {
                //Add Sytem default fields for add operation
                payment.CreatedBy = session.TenantUserId;
                payment.UpdatedBy = session.TenantUserId;
                payment.CreatedOn = DateTime.UtcNow;
                payment.UpdatedOn = DateTime.UtcNow;
            }
            else {
                payment.CreatedOn = DateTime.UtcNow;
                payment.UpdatedOn = DateTime.UtcNow;
                payment.UpdatedBy = invoice.UpdatedBy;
                payment.CreatedBy = invoice.UpdatedBy;
            }

            // Add to repository.
            payment = await _payInvLinkService.AddAsync(payment, token);

            /*
            invoice.AmountPaid = invoice.AmountPaid + payment.Amount;
            invoice.UpdatedOn = payment.UpdatedOn;
            if(payment.UpdatedBy != Guid.Empty)
                invoice.UpdatedBy = payment.UpdatedBy;
            await _invoiceDS.UpdateAsync(invoice, invoice.ID, token);      
            */


            return paymentDTO;
        }

        private BAARInvoiceEntityDTO GenerateUpdateInvoiceEntity(AddPaymentDTO e, Guid updatedBy, BAARInvoiceEntityDTO invoice) {
            invoice.AppliedAmount += e.AmountPaid;
            invoice.AppliedAmountFC += e.AmountPaidFC;
            invoice.UpdatedOn = DateTime.UtcNow;
            invoice.UpdatedBy = updatedBy;
            invoice.BalanceDue = invoice.BalanceDue - e.AmountPaid;
            invoice.BalanceDueFC = invoice.BalanceDueFC - e.AmountPaidFC;

            return invoice;
        }

        /// <summary>
        /// validating invoice amount to pay.
        /// 1) If outstanding amount to pay is 0 then raise exception.
        /// 2) If paying invoice amount is more then "invoice amount" then also raise exception.
        /// </summary>
        /// <param name="invoice"></param>
        /// <param name="amountPaying"></param>
        /// <param name="isBusinessCurrency">amount in business currency</param>
        private void ValidateInvoicePaidAmount(BAARInvoiceEntityDTO invoice, decimal amountPaying, bool isBusinessCurrency) {
            if(invoice != null) {
                decimal paidAmount = isBusinessCurrency ? invoice.BalanceDue - amountPaying : invoice.BalanceDueFC - amountPaying;
                //decimal paidAmountIncludeingPayingCurrent = invoice.Amount - (invoice.AmountPaid + amountPaying);
                if(paidAmount < 0) {
                    EwpError error = new EwpError();
                    error.ErrorType = ErrorType.Validation;
                    EwpErrorData errorData = new EwpErrorData();
                    errorData.ErrorSubType = (int)ValidationErrorSubType.OutOfRange;
                    errorData.Message = string.Format("invoice ({0}) amount is more then payble amount.", invoice.TrackingNo);
                    error.EwpErrorDataList.Add(errorData);
                    EwpValidationException exc = new EwpValidationException(errorData.Message, error.EwpErrorDataList);

                    throw exc;
                }
                else {
                    if(invoice.BalanceDue == 0 || amountPaying == 0) {
                        EwpError error = new EwpError();
                        error.ErrorType = ErrorType.Validation;
                        EwpErrorData errorData = new EwpErrorData();
                        errorData.ErrorSubType = (int)ValidationErrorSubType.OutOfRange;
                        errorData.Message = string.Format("Invoice({0}) already paid.", invoice.ERPDocNum);
                        error.EwpErrorDataList.Add(errorData);
                        EwpValidationException exc = new EwpValidationException(errorData.Message, error.EwpErrorDataList);

                        throw exc;
                    }
                }
            }
        }

        #endregion Payment Support Method

        #region Void Transaction

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="serviceName">Name of service, use for paying invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task VoidPaymentAsync(Guid paymentId, string serviceName, CancellationToken token = default(CancellationToken)) {
            bool isVoid = false;
            if(serviceName == Payment.Common.Constants.VeriCheckServiceKey) {
                isVoid = await voidPaymentTransectionAsync(paymentId, token);
            }
            else if((string.Compare(serviceName, Payment.Common.Constants.TSYSCheckServiceKey, true) == 0)) {
                isVoid = await voidTSysPaymentTransectionAsync(paymentId, token);
            }
            Payment.Entity.Payment payment;
            payment = await _paymentDS.GetAsync(paymentId, token);
            if(isVoid) {
                payment = await _paymentDS.GetAsync(paymentId, token);
                if(payment != null) {
                    payment.Status = PaymentStatusEnum.Voided.ToString();
                    _paymentDS.UpdateSystemFieldsByOpType(payment, OperationType.Update);
                    await _paymentDS.UpdateAsync(payment, paymentId, token);
                }
                _unitOfWork.SaveAll();

                #region Notification

                try {
                    UserSession userSession = _sessionManager.GetSession();

                    // Now generate notifications for both customer and business users. 
                    GenerateVoidPaymentNotification(paymentId, userSession.TenantId, userSession);
                    GenerateVoidPaymentNotificationForCustAsync(paymentId, userSession.TenantId, userSession);

        }
        catch (Exception ex) {
                }

                #endregion Notification
            }
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<bool> voidPaymentTransectionAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string Method = "payment/void/" + paymentId.ToString();
            try {
                UserSession session = _sessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.VeriCheckApiUrl);
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
                return true;
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return false;
            }
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<bool> voidTSysPaymentTransectionAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string Method = "sales/return/" + paymentId.ToString();
            try {
                UserSession session = _sessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.VeriCheckApiUrl);
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
                return true;
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return false;
            }
        }

        #endregion Void Transaction

        #region Refund Transaction

        /// <summary>
        /// Refund payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task RefundPaymentAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            Payment.Entity.Payment payment = await _paymentDS.GetAsync(paymentId, token);
            if(payment != null) {
                PaymentInfoDTO dto = await _qPaymentDS.GetPaymentInfoDTOAsync(paymentId, token);
                if(dto == null) {
                    return;
                }
                Guid tenantId = _sessionManager.GetSession().TenantId;
                bool isRefund = false;
                if(dto.PayServiceName == Core.BaseService.Constants.VeriCheckServiceKey) {
                    isRefund = await RefundPaymentTransectionAsync(paymentId, token);
                }
                else {
                    isRefund = await RefundTSysPaymentTransectionAsync(payment, paymentId, payment.Amount, tenantId, token);
                }

                if(isRefund) {
                    //payment = await _paymentDS.GetAsync(paymentId, token);
                    if(payment != null) {
                        payment.Status = PaymentStatusEnum.RefundRequested.ToString();
                        _paymentDS.UpdateSystemFieldsByOpType(payment, OperationType.Update);
                        await _paymentDS.UpdateAsync(payment, paymentId, token);
                    }
                    _unitOfWork.SaveAll();

                    #region Notification

                    try {
                        UserSession userSession = _sessionManager.GetSession();

                        // Generate notifications for both customer and business users.
                        GenerateRefundPaymentNotification(paymentId, userSession.TenantId, userSession);
                        GenerateRefundPaymentNotificationForCustAsync(paymentId, userSession.TenantId, userSession);
                    }
                    catch(Exception ex) {
                    }

                    #endregion Notification
                }
            }
        }

        /// <summary>
        /// Call vericheck api and Refund payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<bool> RefundPaymentTransectionAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            string Method = "payment/refund/" + paymentId.ToString();
            try {
                UserSession session = _sessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.VeriCheckApiUrl);
                object obj = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
                return true;
            }
            catch(Exception ex) {
                _logger.LogError(" ERROR OCCURED In REFUND: " + ex.Message, null);
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw;
                else {
                    RaiseException(ewp, ex);
                }
                return false;
            }
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<bool> RefundTSysPaymentTransectionAsync(Entity.Payment payment, Guid paymentId, decimal amount, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string Method = "sales/return";
            try {
                CreditCardRefundDTO refDTO = new CreditCardRefundDTO();
                refDTO.transactionAmount = GetRoundingValue(amount);
                if(payment.PreAuthPaymentID != null && payment.PreAuthPaymentID.Value != Guid.Empty) {
                    refDTO.paymentId = payment.PreAuthPaymentID.Value.ToString();
                }
                else {
                    refDTO.paymentId = paymentId.ToString();
                }
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
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);

                try {
                    // TODO: Remove once implemented Schedular. 
                    await PushTSysPaymentBatchUpdateAsync(token);
                }
                catch(Exception) { }

                return true;
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw ex;
                else {
                    RaiseException(ewp, ex);
                }
                return false;
            }
        }

        #endregion Refund Transaction

        #region Update Status

        /// <summary>
        /// Update payment status.
        /// </summary>
        /// <param name="vhTransactioStatusChangeDTO">Payment transection object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdatePaymentStatusAsync(WHTransactioStatusChangeDTO vhTransactioStatusChangeDTO, CancellationToken token = default(CancellationToken)) {
            try {
                _logger.Log(LogLevel.Information, "IN STARTING: Webhook updating Payment Status change(PaymentDataService/UpdatePaymentStatusAsync) " + (vhTransactioStatusChangeDTO != null).ToString());
                Payment.Entity.Payment payment = await _paymentDS.GetAsync(vhTransactioStatusChangeDTO.PaymentId, token);
                string oldStatus = string.Empty;
                //Invoice inv = new Invoice();
                //Invoice invoiceForNotification = new Invoice();
                if(payment != null) {
                    _logger.Log(LogLevel.Information, "START: Webhook updating Payment Status change(PaymentDataService/UpdatePaymentStatusAsync) " + vhTransactioStatusChangeDTO.PaymentId.ToString());
                    oldStatus = payment.Status;
                    payment.Status = vhTransactioStatusChangeDTO.Status;
                    payment.ReturnCode = vhTransactioStatusChangeDTO.ReturnCode;
                    payment.ReturnString = vhTransactioStatusChangeDTO.ReturnString;
                    await _paymentDS.UpdateAsync(payment, payment.ID, token);

                    //List<PaymentInvoiceLinking> listPayInvLink = await _payInvLinkService.GetPaymentInvoiceLinkingByPaymentIdAsync(payment.ID, token);
                    //invoiceForNotification = await _invoiceDS.GetAsync(listPayInvLink[0].InvoiceId, token);

                    // If Payment status refunded then rollback the invoice amount by PaidAmount.
                    if(oldStatus != PaymentStatusEnum.Refunded.ToString() && (payment.Status == PaymentStatusEnum.Refunded.ToString() || payment.Status == PaymentStatusEnum.Error.ToString())) {

                        /* for(int i = 0; i < listPayInvLink.Count; i++) {
                             inv = await _invoiceDS.GetAsync(listPayInvLink[i].InvoiceId, token);
                             if(inv == null) {
                                 continue;
                             }
                             inv.AmountPaid = inv.AmountPaid - listPayInvLink[i].Amount; // payment.Amount;
                             if(inv.AmountPaid < 0) {
                                 inv.AmountPaid = 0;
                             }
                             await _invoiceDS.UpdateAsync(inv, inv.ID, token);
                         }*/
                    }

                }
                else {
                    //_loggerService.Log(LogLevel.Information, "START: Webhook updating Payment Status change found NULL (PaymentDataService/UpdatePaymentStatusAsync)" + vhTransactioStatusChangeDTO.PaymentId.ToString());
                }

                _unitOfWork.Save();
                //_loggerService.Log(LogLevel.Information, "END: Webhook updating Payment Status UnitOfWorkSave (PaymentDataService/UpdatePaymentStatusAsync)" + vhTransactioStatusChangeDTO.PaymentId.ToString());

                #region Notification

                UserSession userSession = GenerateSessionObject(payment.TenantId, payment.CreatedBy, (int)UserTypeEnum.Business, Guid.Empty, "", "", "");

                //await GeneratePaymentStatusChangeNotificationAsync(payment, invoiceForNotification.CustomerId, oldStatus);
                GenerateTransactionStatusChangedNotificationBusAsync(payment.ID, userSession.TenantId, userSession, payment.Status,oldStatus );
                GenerateTransactionStatusChangedNotificationForCustAsync(payment.ID, userSession.TenantId, userSession, payment.Status,oldStatus);

              #endregion Notification
            }

            catch(Exception ex) {
                _logger.Log(LogLevel.Information, "START: Webhook updating Payment Status ERROR (PaymentDataService/UpdatePaymentStatusAsync)" + vhTransactioStatusChangeDTO.PaymentId.ToString() + ":     " + ex.ToString());
            }
        }

        #endregion Update Status

        #region Support

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

        /// <summary>
        /// Method will round the value. Remove all last zeros.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        #endregion Support

        #region API Call

        /// <summary>
        /// Get payment transection status flow.
        /// </summary>
        /// <param name="paymentId">Unique paymentId</param>
        /// <param name="paymentMode">Mode of payment Vericheck or card</param>
        /// <param name="token"></param>
        /// <returns>retun payment status.</returns>
        public async Task<List<PaymentTransectionStatusDTO>> GetPaymentStatusFlowListAsync(Guid paymentId, string paymentMode, CancellationToken token = default(CancellationToken)) {
            string Method = string.Format("payment/{0}/allstatus", paymentId.ToString());
            try {
                UserSession session = _sessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.VeriCheckApiUrl);
                return await httpRequestProcessor.ExecuteAsync<List<PaymentTransectionStatusDTO>>(requestOptions, false);

            }
            catch(Exception ex) {
                //EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                _logger.LogError(ex.InnerException, null);
                return new List<PaymentTransectionStatusDTO>();
            }
        }

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<bool> UpdateInvoiceAfterPaymentAsync(List<BAARInvoiceEntityDTO> listInv, CancellationToken token = default(CancellationToken)) {
            string Method = "invoice/updateinvoiceamount";
            try {
                UserSession session = _sessionManager.GetSession();
                string clientsessionid = session == null ? "" : session.ID.ToString();
                List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
                KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
                listHeader.Add(hdr);
                RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, listInv, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
                ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.BusinessEntityApiUrl);
                await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
                return true;
            }
            catch(Exception ex) {
                //EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                _logger.LogError(ex.InnerException, null);
                return false;
            }
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddVCPaymentAsync(VeriCheckAddPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "payment";
            requestOptions.MethodData = e;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.VeriCheckApiUrl);
            try {
                return await httpRequestProcessor.ExecuteAsync<PaymentDQ>(requestOptions, false);

            }
            catch(Exception ex) {
                if(IsValidJsonError(ex.Message)) {
                    EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                    if(ewp == null)
                        throw;
                    else {
                        RaiseException(ewp, ex);
                    }
                }
                else {
                    throw ex;
                }
            }
            return null;
        }

        private bool IsValidJsonError(string value) {
            try {
                Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(value);
                return true;
            }
            catch(Exception) {
                return false;
            }
        }

        private CurrencyCultureInfo GetCurrencyBySymbol(string currencySymbol) {
            Dictionary<CurrencyISOCode, CurrencyCultureInfo> info = new CurrencyCultureInfoTable(null).GetCurrencyCultureInfo();
            foreach(CurrencyCultureInfo curInfo in info.Values) {
                if(curInfo.Symbol == currencySymbol || curInfo.ShortName == currencySymbol)
                    return curInfo;
            }

            return null;
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddCreditCradPaymentAsync(AddPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            //string transectionKey = await _creditCardAccountDS.GetTSysTransectionKeyAsync(clientsessionid, token);

            CreditCardPaymentDTO model = new CreditCardPaymentDTO();
            if(session == null || session.UserType == (int)UserTypeEnum.Customer) {
                model.saleByCustomer = "Y";
            } 
            model.transactionKey = "";// transectionKey;
            CreditCardPayAttrModel cardInfo = e.userPaymentInfoModel.selectedVCCreditCardPayAttr;
            model.expirationDate = cardInfo.ExpiryMonth + cardInfo.ExpiryYear;
            model.cardDataSource = "MANUAL";
            model.cardNumber = cardInfo.CardNumber;
            model.cvvNumber = cardInfo.CVV;
            model.transactionAmount = e.PaymentAmountInBusinessCurrency ? e.AmountPaid.ToString() : e.AmountPaidFC.ToString();
            model.deviceID = _appSetting.CreditCardAPIdeviceID;
            model.paymentId = e.ID.ToString();
            model.invoiceId = e.InvoiceId.ToString();
            CurrencyCultureInfo curInfo = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            if(curInfo != null) {
                model.currencyCode = curInfo.ShortName;
            }
            else {
                model.currencyCode = e.PaymentTransectionCurrency;
            }

            string Method = "sales/card";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            //if(session != null) {
            //    KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            //    listHeader.Add(hdr);
            //}
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GeneratePaymentDQFromTSysResponse(tSysRes, e);
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddCreditCradPaymentByCardTokenAsync(AddPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            //string transectionKey = await _creditCardAccountDS.GetTSysTransectionKeyAsync(clientsessionid, token);

            CreditCardPaymentTokenDTO model = new CreditCardPaymentTokenDTO();
            if(session == null || session.UserType == (int)UserTypeEnum.Customer) {
                model.saleByCustomer = "Y";
            }
            CreditCardPayAttrModel cardInfo = e.userPaymentInfoModel.selectedVCCreditCardPayAttr;
            model.expirationDate = cardInfo.ExpiryMonth + cardInfo.ExpiryYear;
            model.cvvNumber = cardInfo.CVV;
            model.transactionAmount = e.PaymentAmountInBusinessCurrency ? e.AmountPaid.ToString() : e.AmountPaidFC.ToString();
            model.paymentId = e.ID.ToString();
            model.invoiceId = e.InvoiceId.ToString();
            model.Token = cardInfo.Token.ToString();
            CurrencyCultureInfo curInfo = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            if(curInfo != null) {
                model.currencyCode = curInfo.ShortName;
            }
            else {
                model.currencyCode = e.PaymentTransectionCurrency;
            }
            string Method = "sales/token";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            //if(session != null) {
            //    KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            //    listHeader.Add(hdr);
            //}
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GeneratePaymentDQFromTSysResponse(tSysRes, e);
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e"> entity to be added</param>
        /// <param name="preAuthPayment">Pre authroized entity model.</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddPaymentByPreAuthPaymentAsync(AddPaymentDTO e, PreAuthPayment preAuthPayment, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            //string transectionKey = await _creditCardAccountDS.GetTSysTransectionKeyAsync(clientsessionid, token);
            // PreAuthPayment preAuthPayment = await _preAuthPaymentDS.GetAsync(e.userPaymentInfoModel.PreAuthPaymentID.Value, token);
            CaptureRequestDTO model = new CaptureRequestDTO();
            model.isPartialShipment = e.userPaymentInfoModel.PartialPayment; //preAuthPayment.Amount > e.AmountPaid ? "Y" : "N";
            model.transactionAmount = e.PaymentAmountInBusinessCurrency ? e.AmountPaid.ToString() : e.AmountPaidFC.ToString();
            model.transactionID = preAuthPayment.TransactionId;
            model.invoiceId = e.InvoiceId.ToString();
            model.paymentId = e.userPaymentInfoModel.PreAuthPaymentID.Value.ToString();
            model.partialShipmentData = new PartialShipmentData();
            model.partialShipmentData.totalPaymentCount = preAuthPayment.MaxTotalPaymentCount.ToString();
            model.partialShipmentData.currentPaymentSequenceNumber = preAuthPayment.CurrentPaymentSequenceNumber.ToString();
            //CurrencyCultureInfo curInfo = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            //if(curInfo != null) {
            //    model.currencyCode = curInfo.ShortName;
            //}
            //else {
            //    model.currencyCode = e.PaymentTransectionCurrency;
            //}            
            string Method = "authorization/capture";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            AppCaptureResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<AppCaptureResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GeneratePaymentDQFromTSysCaptureResponse(tSysRes, e);
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> PayAdvancePayByCreditCradAsync(AddAdvancedPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            //string transectionKey = await _creditCardAccountDS.GetTSysTransectionKeyAsync(clientsessionid, token);


            CreditCardPaymentDTO model = new CreditCardPaymentDTO();
            if(session == null || session.UserType == (int)UserTypeEnum.Customer) {
                model.saleByCustomer = "Y";
            }

            model.transactionKey = "";// transectionKey;
            CreditCardPayAttrModel cardInfo = e.userPaymentInfoModel.selectedVCCreditCardPayAttr;
            model.expirationDate = cardInfo.ExpiryMonth + cardInfo.ExpiryYear;
            model.cardDataSource = "MANUAL";
            model.cardNumber = cardInfo.CardNumber;
            model.cvvNumber = cardInfo.CVV;
            model.transactionAmount = e.PaymentAmountInBusinessCurrency ? e.AmountPaid.ToString() : e.AmountPaidFC.ToString();
            model.deviceID = _appSetting.CreditCardAPIdeviceID;
            model.paymentId = e.ID.ToString();
            model.invoiceId = e.InvoiceId.ToString();
            CurrencyCultureInfo curInfo = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            if(curInfo != null) {
                model.currencyCode = curInfo.ShortName;
            }
            else {
                model.currencyCode = e.PaymentTransectionCurrency;
            }
            string Method = "sales/card";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            //if(session != null) {
            //    KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            //    listHeader.Add(hdr);
            //}
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GenerateAdvancePaymentDQFromTSysResponse(tSysRes, e);
        }

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> PayAdvanceCreditCradPaymentByCardTokenAsync(AddAdvancedPaymentDTO e, CancellationToken token = default(CancellationToken)) {
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session == null ? "" : session.ID.ToString();
            //string transectionKey = await _creditCardAccountDS.GetTSysTransectionKeyAsync(clientsessionid, token);

            CreditCardPaymentTokenDTO model = new CreditCardPaymentTokenDTO();
            if(session == null || session.UserType == (int)UserTypeEnum.Customer) {
                model.saleByCustomer = "Y";
            }
            CreditCardPayAttrModel cardInfo = e.userPaymentInfoModel.selectedVCCreditCardPayAttr;
            model.expirationDate = cardInfo.ExpiryMonth + cardInfo.ExpiryYear;
            model.cvvNumber = cardInfo.CVV;
            model.transactionAmount = e.PaymentAmountInBusinessCurrency ? e.AmountPaid.ToString() : e.AmountPaidFC.ToString();
            model.paymentId = e.ID.ToString();
            model.invoiceId = e.InvoiceId.ToString();
            model.Token = cardInfo.Token.ToString();
            CurrencyCultureInfo curInfo = GetCurrencyBySymbol(e.PaymentTransectionCurrency);
            if(curInfo != null) {
                model.currencyCode = curInfo.ShortName;
            }
            else {
                model.currencyCode = e.PaymentTransectionCurrency;
            }
            string Method = "sales/token";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", e.TenantId.ToString());
            //KeyValuePair<string, string> hdrTenantID = new KeyValuePair<string, string>("TenantId", Guid.Empty.ToString());
            listHeader.Add(hdrTenantID);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            TSysResponseDTO tSysRes = await httpRequestProcessor.ExecuteAsync<TSysResponseDTO>(requestOptions, false);
            if(tSysRes.Status != "Success") {
                RaiseError(tSysRes.ErrorMessage);
            }
            return GenerateAdvancePaymentDQFromTSysResponse(tSysRes, e);
        }

        private PaymentDQ GeneratePaymentDQFromTSysResponse(TSysResponseDTO tSysRes, AddPaymentDTO e) {
            PaymentDQ payDq = new PaymentDQ();
            payDq.AmountPaid = e.AmountPaid;
            payDq.Status = tSysRes.Transaction.Status;//"Sattled";
            payDq.PaymentType = "Credit";
            payDq.PaymentEntryType = "";
            payDq.TenantId = e.TenantId;
            payDq.BusinessId = e.BusinessId;
            payDq.ID = e.ID;
            payDq.InvoiceId = e.InvoiceId;
            payDq.AuthCode = tSysRes.Response.SaleResponse.authCode;
            payDq.VCTransactionId = tSysRes.Transaction.CurrentTransactionId;
            payDq.CardType = tSysRes.Response.SaleResponse.cardType;

            return payDq;
        }

        private PaymentDQ GeneratePaymentDQFromTSysCaptureResponse(AppCaptureResponseDTO tSysRes, AddPaymentDTO e) {
            PaymentDQ payDq = new PaymentDQ();
            payDq.AmountPaid = e.AmountPaid;
            payDq.Status = tSysRes.Transaction.Status;//"Sattled";
            payDq.PaymentType = "Credit";
            payDq.PaymentEntryType = "";
            payDq.TenantId = e.TenantId;
            payDq.BusinessId = e.BusinessId;
            payDq.ID = e.ID;
            payDq.InvoiceId = e.InvoiceId;
            payDq.AuthCode = tSysRes.Response.CaptureResponse.authCode;
            payDq.VCTransactionId = tSysRes.Transaction.CurrentTransactionId;
            payDq.CardType = tSysRes.Response.CaptureResponse.cardType;

            return payDq;
        }

        private PaymentDQ GenerateAdvancePaymentDQFromTSysResponse(TSysResponseDTO tSysRes, AddAdvancedPaymentDTO e) {
            PaymentDQ payDq = new PaymentDQ();
            payDq.AmountPaid = e.AmountPaid;
            payDq.Status = tSysRes.Transaction.Status;//"Sattled";
            payDq.PaymentType = "Credit";
            payDq.PaymentEntryType = "";
            payDq.TenantId = e.TenantId;
            payDq.BusinessId = e.BusinessId;
            payDq.ID = e.ID;
            payDq.InvoiceId = e.InvoiceId;
            payDq.AuthCode = tSysRes.Response.SaleResponse.authCode;
            payDq.VCTransactionId = tSysRes.Transaction.CurrentTransactionId;
            payDq.CardType = tSysRes.Response.SaleResponse.cardType;

            return payDq;
        }

        private void RaiseError(string message) {
            EwpValidationException exValidation = new EwpValidationException(message);
            throw exValidation;
        }

        #endregion API Call

        #region Push Invoice Payment To SAP

        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<object> PushPaymentToSAPAsync(AddPaymentDTO e, AddPaymentDTO[] listPayModel, List<BAARInvoiceEntityDTO> listInvoice, Entity.Payment pay, bool isCreditcardPay, CancellationToken token = default(CancellationToken)) {
            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = "payment";
            requestOptions.MethodData = GenerateSAPPushPayModel(e, listPayModel, listInvoice, pay, isCreditcardPay);

            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Post;
            requestOptions.BearerTokenInfo = new BearerTokenOption();
            requestOptions.BearerTokenInfo.AppClientName = _appSetting.AppName;
            requestOptions.BearerTokenInfo.AuthServiceUrl = _appSetting.IdentityServerUrl;
            string pushAPiUrl = _appSetting.BusinessConnectorApiUrl + "payment";
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(pushAPiUrl);

            object obj = null;

            try {
                obj = await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
            }
            catch(Exception exx) {
            }

            return obj;
        }

        /// <summary>
        /// PUSH batch update to TSys.
        /// </summary>
        /// <param name="token"></param>
        private async Task PushTSysPaymentBatchUpdateAsync(CancellationToken token = default(CancellationToken)) {
            // Preparing api calling process model.           
            string Method = "sales/batchclose";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();
            UserSession session = _sessionManager.GetSession();
            string clientsessionid = session != null ? session.ID.ToString() : "";
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, null, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            await httpRequestProcessor.ExecuteAsync<object>(requestOptions, false);
        }

        private SAPPaymentPushRequestDTO GenerateSAPPushPayModel(AddPaymentDTO addPaymentDto, AddPaymentDTO[] listPayModel, List<BAARInvoiceEntityDTO> listInvoice, Entity.Payment pay, bool isCreditCardPay) {
            SAPPaymentPushRequestDTO sapModel = new SAPPaymentPushRequestDTO();
            sapModel.BAPaymentTransId = pay.IdentityNumber.Replace(PaymentIdentityPrefix, "");//pay.TransactionId;
            sapModel.Notes = addPaymentDto.Note;
            sapModel.PaymentDate = DateTime.Now;//pay.CreatedOn.Value;
            //pay.CreatedOn.Value;
            sapModel.ARInvoices = new List<BAPaymentInvoiceSyncDTO>();
            string erpCustomerKey = "";
            for(int i = 0; i < listPayModel.Length; i++) {
                BAARInvoiceEntityDTO invEntDTO = listInvoice.Find(inv => inv.ID == listPayModel[i].InvoiceId);
                if(invEntDTO == null)
                    continue;
                BAPaymentInvoiceSyncDTO payInv = new BAPaymentInvoiceSyncDTO();
                payInv.ERPARInvoiceId = invEntDTO.ERPARInvoiceKey;
                erpCustomerKey = invEntDTO.ERPCustomerKey;
                payInv.TransactionAmount = listPayModel[i].PaymentAmountInBusinessCurrency ? listPayModel[i].AmountPaid : listPayModel[i].AmountPaidFC;
                sapModel.ARInvoices.Add(payInv);
            }

            sapModel.ERPCustomerKey = erpCustomerKey;
            if(isCreditCardPay) {
                sapModel.CreditCards = new List<BAPaymentCreditCardsSyncDTO>();
                BAPaymentCreditCardsSyncDTO card = new BAPaymentCreditCardsSyncDTO();
                card.Amount = addPaymentDto.PaymentAmountInBusinessCurrency ? addPaymentDto.AmountPaid : addPaymentDto.AmountPaidFC;
                card.TransactionAmount = card.Amount;
                card.CardExpiryMonth = Convert.ToInt32(addPaymentDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.ExpiryMonth);
                card.CardExpiryYear = Convert.ToInt32(addPaymentDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.ExpiryYear);
                card.CreditCardNo = addPaymentDto.userPaymentInfoModel.selectedVCCreditCardPayAttr.CardNumber;
                card.PaymentTransactionDate = pay.CreatedOn.Value;
                card.TransactionType = "T";
                // TODO: Amit.
                card.Voucher = "259";
                sapModel.CreditCards.Add(card);
            }
            else {
                BAPaymentChecksDetailDTO bankInfo = GenerateBankSyncDTO(addPaymentDto);
                sapModel.Checks = new List<BAPaymentChecksDetailDTO>();
                sapModel.Checks.Add(bankInfo);
            }
            return sapModel;
        }

        private BAPaymentChecksDetailDTO GenerateBankSyncDTO(AddPaymentDTO addPaymentDto) {
            BAPaymentChecksDetailDTO card = new BAPaymentChecksDetailDTO();
            CustVCACHPayAttrDTO bankInfo = addPaymentDto.userPaymentInfoModel.SelectedCustVCACHPayAttr;
            card.BankAccount = bankInfo.AccountNo;
            card.Amount = addPaymentDto.PaymentAmountInBusinessCurrency ? addPaymentDto.AmountPaid : addPaymentDto.AmountPaidFC;
            //card.BankCode = bankInfo.ABARounting;
            card.Details = addPaymentDto.Note;
            card.DueDate = DateTime.Now.ToString("yyyyMMdd");

            return card;
        }

        #endregion PUSH Invoice Payment To SAP

        #region Notification Private Method

        private async Task GeneratePaymentNotificationForBusinessAsync(Guid paymentId, Guid tenantId, UserSession userSession, List<InvoicePaymentDTO> invoicePaymentDTOs, PaymentDetailDQ payDq) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
            paymentNotificationDTO.InvoicePaymentList = invoicePaymentDTOs;

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.TransactionStatus = paymentRelatedDataDTO.TransactionStatus;
            paymentNotificationDTO.AccountNumber = payDq.CustomerAccountNumber;
            paymentNotificationDTO.TransactionMode = payDq.PayServiceAttributeName;
            paymentNotificationDTO.TransactionService = payDq.PayServiceName;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
            paymentNotificationDTO.UserTypeText = ((UserTypeEnum)userSession.UserType).ToString();
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;

            ModuleTypeEnum moduleType = ModuleTypeEnum.Business;

            await _paymentNotificationHandler.GeneratePaymentNotificationForBusinessAsync(paymentNotificationDTO, moduleType);
         }

         private UserSession GenerateSessionObject( Guid tenantId, Guid tenantUserId, int userType,Guid appId,string subdomain = "",string userName= "",string tenantName = "") {
            UserSession session = new UserSession() {
              TenantId = tenantId,
              TenantUserId = tenantUserId,
              UserType = userType,
              AppId = appId,
              IdentityServerId = "",
              ID = Guid.NewGuid(),
              Subdomain =subdomain,
              UserName=userName,
              TenantName =""
            };

            return session;
          }

        private void GenerateTransactionIntiatePaymentNotificationForCustAsync(Guid paymentId, Guid tenantId, UserSession userSession, List<InvoicePaymentDTO> invoicePaymentDTOs, PaymentDetailDQ payDq) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
            paymentNotificationDTO.InvoicePaymentList = invoicePaymentDTOs;

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
            paymentNotificationDTO.ActionDate = DateTime.UtcNow.ToString();
            paymentNotificationDTO.AppId = paymentRelatedDataDTO.AppId;
            paymentNotificationDTO.BusinessCompanyName = paymentRelatedDataDTO.BusinessCompanyName;
            paymentNotificationDTO.CopyRightText = paymentRelatedDataDTO.CopyRightText;
            paymentNotificationDTO.CustomerCompanyId = paymentRelatedDataDTO.CustomerCompanyId;
            paymentNotificationDTO.CustomerCompanyName = paymentRelatedDataDTO.CustomerCompanyName;
            paymentNotificationDTO.LoginUserTenantUserId = userSession.TenantUserId;
            paymentNotificationDTO.PortalURL = URLHelper.GetSubDomainURL(paymentRelatedDataDTO.SubDomain, _appSetting.PaymentPortalSubDomainCustomerURL);
            ;
            paymentNotificationDTO.PublisherCompanyName = paymentRelatedDataDTO.PublisherCompanyName;
            paymentNotificationDTO.SubDomain = paymentRelatedDataDTO.SubDomain;
            paymentNotificationDTO.TenantId = tenantId;
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0} {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.Originated.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;
            paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = paymentRelatedDataDTO.TransactionStatus;
            paymentNotificationDTO.AccountNumber = payDq.CustomerAccountNumber;
            paymentNotificationDTO.TransactionMode = payDq.PayServiceAttributeName;
            paymentNotificationDTO.TransactionService = payDq.PayServiceName;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
            paymentNotificationDTO.UserTypeText = ((UserTypeEnum)userSession.UserType).ToString();

            _paymentNotificationHandler.GenerateTransactionInitiateRequestedNotificationForCust(paymentNotificationDTO);
        }

        private void GenerateVoidPaymentNotification(Guid paymentId, Guid tenantId, UserSession userSession) {


            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.VoidRequested.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
      paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;

      _paymentNotificationHandler.GenerateTransactionVoidRequestedNotification(paymentNotificationDTO);
        }

        private void GenerateVoidPaymentNotificationForCustAsync(Guid paymentId, Guid tenantId, UserSession userSession) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency);//string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber; //paymentRelatedDataDTO.TransactionId;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.VoidRequested.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;
      paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;

      _paymentNotificationHandler.GenerateTransactionVoidRequestedNotificationForCust(paymentNotificationDTO);
        }

        private void GenerateRefundPaymentNotification(Guid paymentId, Guid tenantId, UserSession userSession) {


            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.RefundRequested.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;

            _paymentNotificationHandler.GenerateTransactionRefundRequestedNotification(paymentNotificationDTO);
        }

        private void GenerateRefundPaymentNotificationForCustAsync(Guid paymentId, Guid tenantId, UserSession userSession) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;

            paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.RefundRequested.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;
            paymentNotificationDTO.CustomerId = paymentRelatedDataDTO.CustomerRefId;

            if(paymentRelatedDataDTO.BusinessPartnerTenantId != null)
                paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            else {
                paymentNotificationDTO.BusinessPartnerTenantId = Guid.Empty.ToString();
            }

            _paymentNotificationHandler.GenerateTransactionRefundRequestedNotificationForCust(paymentNotificationDTO);
        }

        private void GenerateTransactionStatusChangedNotificationForCustAsync(Guid paymentId, Guid tenantId, UserSession userSession, string newStatus, string oldStatus) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
      paymentNotificationDTO.NewTransactionStatus = newStatus;  //paymentRelatedDataDTO.TransactionStatus; 
      paymentNotificationDTO.OldTransactionStatus = oldStatus;  //paymentRelatedDataDTO.TransactionStatus; 
      paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;

            if (paymentRelatedDataDTO.BusinessPartnerTenantId != null)
              paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            else
            {
              paymentNotificationDTO.BusinessPartnerTenantId = Guid.Empty.ToString();
            }

      _paymentNotificationHandler.GenerateTransactionStatusChangedNotificationForCust(paymentNotificationDTO);
        }

        private void GenerateTransactionStatusChangedNotificationBusAsync(Guid paymentId, Guid tenantId, UserSession userSession, string newStatus, string oldStatus) {
          PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

          PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
          paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
          paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
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
          paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
          paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
          paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.IdentityNumber;
          paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
          paymentNotificationDTO.NewTransactionStatus = newStatus;  //paymentRelatedDataDTO.TransactionStatus; 
          paymentNotificationDTO.OldTransactionStatus = oldStatus;  //paymentRelatedDataDTO.TransactionStatus; 
          paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
          paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
          paymentNotificationDTO.PaymentId = paymentId;
          paymentNotificationDTO.UserSessionInfo = userSession;
          paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;

          _paymentNotificationHandler.GenerateTransactionStatusChangedNotification(paymentNotificationDTO);
        }

    /*
            private async Task GeneratePaymentNotificationForCustAsync(Guid paymentId, Guid tenantId, UserSession userSession, CancellationToken token = default(CancellationToken)) {
                PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();

                PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
                paymentNotificationDTO.ActionDate = DateTime.UtcNow.ToString();
                paymentNotificationDTO.AppId = paymentRelatedDataDTO.AppId;
                paymentNotificationDTO.BusinessCompanyName = paymentRelatedDataDTO.BusinessCompanyName;
                paymentNotificationDTO.CopyRightText = paymentRelatedDataDTO.CopyRightText;
                paymentNotificationDTO.CustomerCompanyId = paymentRelatedDataDTO.CustomerCompanyId;
                paymentNotificationDTO.CustomerCompanyName = paymentRelatedDataDTO.CustomerCompanyName;
                paymentNotificationDTO.LoginUserTenantUserId = userSession.TenantUserId;
                paymentNotificationDTO.PortalURL = " ";
                paymentNotificationDTO.PublisherCompanyName = paymentRelatedDataDTO.PublisherCompanyName;
                paymentNotificationDTO.SubDomain = paymentRelatedDataDTO.SubDomain;
                paymentNotificationDTO.TenantId = tenantId;
                paymentNotificationDTO.TransactionAmountWithCurrency = string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
                paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
                paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.TransactionId;
                paymentNotificationDTO.TransactionStatus = PaymentStatusEnum.Pending.ToString();
                paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
                paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
                paymentNotificationDTO.PaymentId = paymentId;
                paymentNotificationDTO.UserSessionInfo = userSession;
                ModuleTypeEnum moduleType = ModuleTypeEnum.Payment;
                await _paymentNotificationHandler.GeneratePaymentNotificationForCustomerAsync(paymentNotificationDTO, moduleType);
            }
    */
    private async Task GenerateAdvancedPaymentNotification(Guid paymentId, Guid tenantId, PaymentDetailDQ payDq, UserSession userSession, CancellationToken token = default(CancellationToken)) {
            try {
                PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());

                await GenerateAdvancedPaymentNotificationForBusinessAsync(payDq, paymentId, tenantId, userSession, paymentRelatedDataDTO, token);
                await GenerateAdvancedPaymentNotificationForCustAsync(payDq, paymentId, tenantId, userSession, paymentRelatedDataDTO, token);

                _logger.LogInformation( " GenerateAdvancedPaymentNotification: " , null);
            }
            catch(Exception ex) {
                _logger.LogError( ex.Message + " GenerateAdvancedPaymentNotification: " + ex.StackTrace, null);
            }
        }

        private async Task GenerateAdvancedPaymentNotificationForBusinessAsync(PaymentDetailDQ payDq, Guid paymentId, Guid tenantId, UserSession userSession, PaymentRelatedDataDTO paymentRelatedDataDTO, CancellationToken token = default(CancellationToken)) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
            paymentNotificationDTO.TimeZone = paymentRelatedDataDTO.TimeZone;
            paymentNotificationDTO.DateTimeFormat = paymentRelatedDataDTO.DateTimeFormat;
            //            PaymentRelatedDataDTO paymentRelatedDataDTO = _paymentNotificationDS.GetPaymentNotificationInfo(paymentId, userSession.TenantUserId, AppKeyEnum.pay.ToString());
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.TransactionId;
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = payDq.Status; //PaymentStatusEnum.Pending.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;

            paymentNotificationDTO.TransactionService = payDq.PayServiceName;
            paymentNotificationDTO.TransactionMode = payDq.PayServiceAttributeName;
            paymentNotificationDTO.AccountNumber = payDq.CustomerAccountNumber;
            paymentNotificationDTO.CustomerId = payDq.CustomerId;
            paymentNotificationDTO.CustomerName = payDq.CustomerName;
            paymentNotificationDTO.PaidByUserIdentityNumber = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaidByUserName = payDq.PayeeName;// payeeName
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;


            ModuleTypeEnum moduleType = ModuleTypeEnum.Business;
            if(userSession.UserType == (int)UserTypeEnum.Business)
                await _paymentNotificationHandler.GenerateBizAdvancedPaymentNotificationForBusinessAsync(paymentNotificationDTO, moduleType);
            else
                await _paymentNotificationHandler.GenerateCustAdvancedPaymentNotificationForBusinessAsync(paymentNotificationDTO, moduleType);

        }

        private async Task GenerateAdvancedPaymentNotificationForCustAsync(PaymentDetailDQ payDq, Guid paymentId, Guid tenantId, UserSession userSession, PaymentRelatedDataDTO paymentRelatedDataDTO, CancellationToken token = default(CancellationToken)) {
            PaymentNotificationDTO paymentNotificationDTO = new PaymentNotificationDTO();
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
            paymentNotificationDTO.TransactionAmountWithCurrency = GetFormattedAmount(paymentRelatedDataDTO.TransactionAmount, paymentRelatedDataDTO.Currency); //string.Format("{0}  {1}", paymentRelatedDataDTO.Currency, paymentRelatedDataDTO.TransactionAmount);
            paymentNotificationDTO.TransactionDate = paymentRelatedDataDTO.TransactionDate.ToString();
            paymentNotificationDTO.TransactionId = paymentRelatedDataDTO.TransactionId;
            paymentNotificationDTO.IdentityNumber = paymentRelatedDataDTO.IdentityNumber;
            paymentNotificationDTO.TransactionStatus = payDq.Status; //PaymentStatusEnum.Pending.ToString();
            paymentNotificationDTO.UserFullName = paymentRelatedDataDTO.UserFullName;
            paymentNotificationDTO.UserId = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.TransactionService = payDq.PayServiceName;
            paymentNotificationDTO.TransactionMode = payDq.PayServiceAttributeName;
            paymentNotificationDTO.AccountNumber = payDq.CustomerAccountNumber;
            paymentNotificationDTO.CustomerId = payDq.CustomerId;
            paymentNotificationDTO.CustomerName = payDq.CustomerName;
            paymentNotificationDTO.PaidByUserIdentityNumber = paymentRelatedDataDTO.UserId;
            paymentNotificationDTO.PaidByUserName = payDq.PayeeName;// payeeName
            paymentNotificationDTO.PaymentApplicationName = paymentRelatedDataDTO.PaymentApplicationName;
            if(paymentRelatedDataDTO.BusinessPartnerTenantId != null)
                paymentNotificationDTO.BusinessPartnerTenantId = paymentRelatedDataDTO.BusinessPartnerTenantId.Value.ToString();
            else {
                paymentNotificationDTO.BusinessPartnerTenantId = Guid.Empty.ToString();
            }
            paymentNotificationDTO.PaymentId = paymentId;
            paymentNotificationDTO.UserSessionInfo = userSession;
            ModuleTypeEnum moduleType = ModuleTypeEnum.Business;

            if(userSession.UserType == (int)UserTypeEnum.Business)
                await _paymentNotificationHandler.GenerateBizAdvancedPaymentNotificationForCustomerAsync(paymentNotificationDTO, moduleType);
            else {
                moduleType = ModuleTypeEnum.Customer;
                await _paymentNotificationHandler.GenerateCustAdvancedPaymentNotificationForCustomerAsync(paymentNotificationDTO, moduleType);
            }
        }


        #endregion Notification Private Method

    }
}

/*
 
        /// <summary>
        /// Adds VC check payment new Payment entity
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        private async Task<PaymentDQ> AddVCPaymentAsync(AddPaymentDTO e, CancellationToken token = default(CancellationToken)) {

            HttpClient client = new HttpClient();
            string url = _appSetting.VeriCheckApiUrl + "payment";
            HttpRequestProcessor reqProc = new HttpRequestProcessor(client);
            UserSession session = _sessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            if(session != null) {

                KeyValuePair<string, string> tenantKey = new KeyValuePair<string, string>("tenantid", session.TenantId.ToString());
                headerParameters.Add(tenantKey);

                KeyValuePair<string, string> appUserIdKey = new KeyValuePair<string, string>("appuserid", session.TenantUserId.ToString());
                headerParameters.Add(appUserIdKey);

                KeyValuePair<string, string> appidKey = new KeyValuePair<string, string>("appid", session.AppId.ToString());
                headerParameters.Add(appidKey);
            }
            string bearerToken = await reqProc.GetBearerTokenAsync(_appSetting.AppClientName, _appSetting.IdentityServerUrl);
            client.SetBearerToken(bearerToken);
            PaymentDQ paymentDTO = null;
            try {
                paymentDTO = await reqProc.ExecutePOSTRequestAsync<PaymentDQ, AddPaymentDTO>(url, "", AcceptMediaTypeEnum.JSON, headerParameters, null, null, e);
            }
            catch(Exception ex) {
                EwpError ewp = Newtonsoft.Json.JsonConvert.DeserializeObject<EwpError>(ex.Message);
                if(ewp == null)
                    throw;
                else {
                    RaiseException(ewp, ex);
                }
            }
            return paymentDTO;
        }
     */
