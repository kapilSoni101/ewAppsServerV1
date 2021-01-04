
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>/Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.ScheduledJobService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Recurring payment contains all schedule information.
    /// 1) Add recurring and Generate schedule job and add.
    /// 2) Make payment for each Schedule job.
    /// 3) Generate the log for each schedule payment.
    /// 4) Update the Recurring for each payment for nextSchedule payment and remaining schedule job. 
    /// </summary>
    public class RecurringPaymentDS:BaseDS<RecurringPayment>, IRecurringPaymentDS {

        #region Member variable

        IRecurringPaymentRepository _recurringPaymentRepository;
        IPaymentUnitOfWork _unitOfWork;
        IPaymentDataService _paymentDataService;
        IUserSessionManager _usManager;
        IRecurringPaymentLogDS _recurringPaymentLogDS;

        IPayAppServiceDS _payAppServiceDS;
        PaymentAppSettings _appSetting;

        private const string RecurringDocumentType = "Recurring";

        IScheduledJobManager _scheduledJobManager;

        //ICustomerDataService _customerDS;
        //IScheduledJobManager _scheduledJobManager;
        //IServiceAccountDetailDS _serviceAccountDetailDS;
        //ITenantUserDataService _tenantUserDataService;
        //IBusinessDataService _businessDataService;
        //IIdentityDataService _idenDS;
        //IAppDataService _appDataService;
        //ITenantDataService _tenantDataService;
        //ITenantLinkingDS _tenantLinkingDS;
        //IPublisherDS _publisherDS;
        //IPublisherAppSettingDS _publisherAppSettingDS;
        //IPaymentBusinessNotificationHandler _paymentEmailerDataservice;

        #endregion Member variable

        #region Contructor

        /// <summary>Initializing local variablesInitializing local variables</summary>        
        public RecurringPaymentDS(IRecurringPaymentRepository recurringPaymentRepository,
            IPaymentUnitOfWork unitOfWork,
            IPaymentDataService paymentDataService,
            IUserSessionManager usManager,
            IRecurringPaymentLogDS recurringPaymentLogDS,
            IPayAppServiceDS payAppServiceDS,
            IOptions<PaymentAppSettings> appSetting) : base(recurringPaymentRepository) {
            _recurringPaymentRepository = recurringPaymentRepository;
            _unitOfWork = unitOfWork;
            _paymentDataService = paymentDataService;
            _usManager = usManager;
            _recurringPaymentLogDS = recurringPaymentLogDS;
            _payAppServiceDS = payAppServiceDS;
            _appSetting = appSetting.Value;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get recurring payment schedule list.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentAsync(CancellationToken token = default(CancellationToken)) {
            return await _recurringPaymentRepository.GetRecurrtingPaymentAsync(_usManager.GetSession().TenantId, token);
        }

        /// <summary>
        /// Get recurring payment schedule list by customerid.
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentByCustomerAsync(Guid customerid, CancellationToken token = default(CancellationToken)) {
            return await _recurringPaymentRepository.GetRecurrtingPaymentByCustomerAsync(customerid, token);
        }

        /*
        /// <summary>
        /// Get the recurring detail by id and also get the customer subscribed app service list. 
        /// </summary>
        /// <param name="recurringId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<RecurringAndCustomerAppServiceDetailDTO> GetRecurringModelById(Guid recurringId, CancellationToken token = default(CancellationToken)) {
            RecurringAndCustomerAppServiceDetailDTO recDetail = null;
            RecurringDTO recurringDTO = await _recurringPaymentRepository.GetRecurringModelById(recurringId);
            if(recurringDTO != null) {
                recDetail = new RecurringAndCustomerAppServiceDetailDTO();
                recDetail.recurringDTO = recurringDTO;
                UserSession uSession = _usManager.GetSession();
                Core.Entity.Customer cust = await _customerDS.GetAsync(recurringDTO.RecurringPaymentDetail.CustomerId, token);
                if(cust != null) {
                    recDetail.CustomerName = cust.Name;
                    recDetail.CustomerRefId = cust.CustomerRefId;
                    //recurringDTO.RecurringPaymentDetail.ContactName = cust.Name;
                    recDetail.CustomerBusinessPartnerTenantId = cust.BusinessPartnerTenantId;
                    recDetail.customerBusAppServiceList = _payAppServiceDS.GetAppServiceListWithAccountDetailByAppAndTenantAndCustomerId(uSession.AppId, uSession.TenantId, cust.BusinessPartnerTenantId);
                }
                //recDetail.listRecurringPaymentViewDTO = await _recurringPaymentRepository.GetRecurrtingPaymentByCustomerOrderAsync(recurringDTO.RecurringPaymentDetail.CustomerId, recurringDTO.RecurringPaymentDetail.OrderId, token;)
                recDetail.listPaymentHistory = await _paymentDataService.GetPaymentHistoryByCustomerOrderIdAsync(recurringDTO.RecurringPaymentDetail.CustomerId, recurringDTO.RecurringPaymentDetail.OrderId, token);
                TenantUser user = await _tenantUserDataService.GetAsync(recurringDTO.CreatedBy);
                if(user != null) {
                    recurringDTO.CreatedByName = user.FullName;
                }
            }

            return recDetail;
        }*/

        #endregion Get

        
        //#region  Payment

        ///// <summary>
        ///// Add invoice and make payment for each recurring payment.
        ///// </summary>
        ///// <param name="schedule"></param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //public Core.ScheduledJobService.ResponseDTO AddInvoiceAndMakePayment(ScheduledJobDTO schedule, CancellationToken token = default(CancellationToken)) {
        //    RecurringPaymentDTO recurringPaymentDTO = JsonSerializer.DeSerialize<RecurringPaymentDTO>(schedule.SourceEventPayload);
        //    Core.ScheduledJobService.ResponseDTO response = new Core.ScheduledJobService.ResponseDTO() {
        //        ReponseType = "Http-200",
        //        Response = ScheduledJobStatusEnum.Success.ToString()
        //    };
        //    AddInvoiceAndMakePaymentAsync(schedule);
            
        //    return response;
        //}

        ///// <summary>
        ///// Execute for each scheduled recurring payment. Method does following operations:
        /////   1) Generate invoice
        /////   2) Paying invoice.
        /////   3) Update schedule job status for each schedule recurring payment.
        /////   4) Creating log for each success/unsucess payment.
        ///// </summary>
        ///// <param name="schedule">Schedule job for which recurring payment is executing.</param>
        ///// <param name="token"></param>
        ///// <returns></returns>
        //private void AddInvoiceAndMakePaymentAsync(ScheduledJobDTO schedule, CancellationToken token = default(CancellationToken)) {
        //    // Getting recurring paynment.
        //    RecurringPayment recPayment = GetAsync(schedule.SourceId, token).Result;
        //    // Parsing json to to object.
        //    RecurringPaymentDTO recurringPaymentDTO = JsonSerializer.DeSerialize<RecurringPaymentDTO>(schedule.SourceEventPayload);
        //    Guid tenantId = Guid.Empty;
        //    try {
        //        if(recurringPaymentDTO != null && recPayment != null && !recPayment.Deleted) {
        //            // If status inactive then return.
        //            if(recPayment.Status == 0) {
        //                AddPaymentLog(schedule, recurringPaymentDTO, RecurringPaymentLogStateEnum.RecurringInActivate, tenantId, token);
        //                // Update schedulejob log.
        //                UpdateScheduleJob(schedule, Guid.Empty, ScheduledJobStatusEnum.Failed.ToString(), RecurringPaymentLogStateEnum.RecurringInActivate.ToString());
        //                _unitOfWork.Save();
        //                return;
        //            }

        //            CustomerDTO custDto = _customerDS.GetCustomerById(recurringPaymentDTO.CustomerId).Result;
        //            //GenerateCustomerDTO(cust, recurringPaymentDTO);
        //            if(custDto != null) {
        //                tenantId = custDto.TenantId;
        //                //actDetail.AccountJson = new Core.CommonService.CryptoHelper().Decrypt(actDetail.AccountJson, Core.Common.Constants.DefaultEncryptionAlgo);

        //                // Generating the invice adn payment model.
        //                AddInvoiceAndPay addInvoiceAndPay = GenerateInvoiceAndPayModel(recPayment.ID, recurringPaymentDTO, schedule.ScheduledTime, custDto);
        //                // Mapping account detail
        //                MapAccountDetail(addInvoiceAndPay.AddPaymentDTO, recurringPaymentDTO.);
        //                // Generating the invoice as well as paying invoice.
        //                Tuple<PaymentDetailDTO, List<PaymentAndInvoiceNotificationDTO>> tupleParam = _paymentDataService.AddInvoiceAndPayWithoutAuthenticateAsync(addInvoiceAndPay, tenantId, false, token).Result;
        //                //  Add payment log.
        //                AddPaymentLog(schedule, recurringPaymentDTO, RecurringPaymentLogStateEnum.Processed, tenantId, token);
        //                // Update RecurringPayment NextPaymentDate.
        //                UpdateRecurringNextPaymentDate(recPayment);
        //                // Commit all changes.
        //                _unitOfWork.Save();
        //                if(tupleParam != null) {
        //                    listNotification = tupleParam.Item2;
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception ex) {
        //        //  Add payment log.
        //        AddPaymentLog(schedule, recurringPaymentDTO, RecurringPaymentLogStateEnum.Error, tenantId, token);
        //        // in case of fail, update the schedule job status.
        //        UpdateScheduleJob(schedule, Guid.Empty, ScheduledJobStatusEnum.Failed.ToString(), ex.Message);
        //        _unitOfWork.Save();
        //        throw;
        //    }            

        //    // Update schedule job status.
        //    UpdateScheduleJob(schedule, Guid.Empty, ScheduledJobStatusEnum.Success.ToString(), "Success");
        //    return;
        //}

        ///// <summary>
        ///// Generate model for creating thje invoice and making payment for invoice.
        ///// </summary>
        ///// <param name="recurringId">Unique if of recurring scheduler.</param>
        ///// <param name="recurringPaymentDTO"></param>
        ///// <param name="scheduleDate"></param>
        ///// <param name="cust">Customer short object.</param>
        ///// <param name="custDtl">Customer detail object with address detail.</param>
        ///// <returns></returns>
        //private AddInvoiceAndPay GenerateInvoiceAndPayModel(Guid recurringId, RecurringPaymentDTO recurringPaymentDTO, DateTime scheduleDate, UserPaymentInfoModel cust) {
        //    AddInvoiceAndPay addInvoiceAndPay = new AddInvoiceAndPay();
        //    // Generate the payment model.
        //    addInvoiceAndPay.AddPaymentDTO = GeneratPaymentModel(recurringPaymentDTO, cust, cust.TenantId);
        //    // Generate invoice object.
        //    addInvoiceAndPay.InvoiceDTO = GeneratInvoiceModel(recurringId, recurringPaymentDTO, cust, scheduleDate, cust.TenantId);

        //    addInvoiceAndPay.AddPaymentDTO.FromCurrencyCode = addInvoiceAndPay.InvoiceDTO.FromCurrencyCode;
        //    addInvoiceAndPay.AddPaymentDTO.ToCurrencyCode = addInvoiceAndPay.InvoiceDTO.ToCurrencyCode;
        //    addInvoiceAndPay.AddPaymentDTO.ExchangeRate = addInvoiceAndPay.InvoiceDTO.ExchangeRate;

        //    return addInvoiceAndPay;
        //}

        ///// <summary>
        ///// Generating payment model.
        ///// </summary>
        ///// <param name="recurringPaymentDTO"></param>
        ///// <param name="cust"></param>
        ///// <param name="tenantId"></param>
        ///// <returns></returns>
        //private AddPaymentDTO GeneratPaymentModel(RecurringPaymentDTO recurringPaymentDTO, UserPaymentInfoModel cust, Guid tenantId) {
        //    AddPaymentDTO addPaymentDTO = new AddPaymentDTO();
        //    decimal amountToPay = recurringPaymentDTO.Amount;// * recurringPaymentDTO.Tax / 100;
        //    addPaymentDTO.AmountPaid = amountToPay;
        //    addPaymentDTO.TotalAmount = amountToPay;
        //    addPaymentDTO.BusinessId = tenantId;
        //    addPaymentDTO.TenantId = tenantId;
        //    addPaymentDTO.Note = recurringPaymentDTO.Description;
        //    // addPaymentDTO.PaymentType = ((int)recurringPaymentDTO.AccountType).ToString();
        //    addPaymentDTO.PaymentEntryType = recurringPaymentDTO.PaymentEntryType;
        //    addPaymentDTO.PartnerId = recurringPaymentDTO.CustomerId;
        //    addPaymentDTO.userPaymentInfoModel = cust;
        //    /* addPaymentDTO.Customer.ABARoutingNumber = recurringPaymentDTO.ABARoutingNumber;
        //    addPaymentDTO.Customer.AccountNumber = recurringPaymentDTO.AccountNumber;
        //    addPaymentDTO.Customer.AccountType = recurringPaymentDTO.AccountType;
        //    addPaymentDTO.Customer.BankAccountName = recurringPaymentDTO.BankAccountName;
        //    addPaymentDTO.Customer.BankName = recurringPaymentDTO.BankName; */
        //    addPaymentDTO.OriginationDate = DateTime.UtcNow;
        //    addPaymentDTO.AppServiceId = recurringPaymentDTO.AppServiceId;
        //    addPaymentDTO.AppServiceAttributeId = recurringPaymentDTO.AppServiceAttributeId;
        //    addPaymentDTO.SaveAccountInfo = false;

        //    return addPaymentDTO;
        //}

        //private void MapAccountDetail(AddPaymentDTO addPaymentDTO, AppServiceAccountDetail accDetail) {
        //    string AccountJson = new Core.CommonService.CryptoHelper().Decrypt(accDetail.AccountJson, Core.CommonService.Constants.DefaultEncryptionAlgo);
        //    CustVCACHPayAttrDTO actDtl = Newtonsoft.Json.JsonConvert.DeserializeObject<CustVCACHPayAttrDTO>(AccountJson);
        //    addPaymentDTO.userPaymentInfoModel.SelectedCustVCACHPayAttr = actDtl;
        //    addPaymentDTO.userPaymentInfoModel.AppServiceId = accDetail.ServiceId;           
        //    addPaymentDTO.PaymentEntryType = actDtl.SECCode;
        //}

        ///// <summary>
        ///// Generateing invoice model object.
        ///// </summary>
        ///// <param name="recurringPaymentDTO"></param>
        ///// <param name="cust"></param>
        ///// <param name="dueDate"></param>
        ///// <param name="tenantId"></param>
        ///// <returns></returns>
        //private InvoiceDTO GeneratInvoiceModel(Guid recurringId, RecurringPaymentDTO recurringPaymentDTO, CustomerDTO cust, DateTime dueDate, Guid tenantId) {
        //    InvoiceDTO invoiceDTO = new InvoiceDTO();
        //    invoiceDTO.AmountPaid = 0;
        //    invoiceDTO.Amount = recurringPaymentDTO.Amount;
        //    invoiceDTO.SubTotal = recurringPaymentDTO.Amount;
        //    invoiceDTO.DueDate = dueDate;
        //    invoiceDTO.TenantId = tenantId;
        //    invoiceDTO.Tax = recurringPaymentDTO.Tax;
        //    invoiceDTO.DiscountType = "%";
        //    invoiceDTO.TaxType = "%";
        //    invoiceDTO.CustomerDTO = cust;
        //    invoiceDTO.CustomerId = cust.ID;
        //    if(invoiceDTO.Address == null) {
        //        invoiceDTO.Address = new AddressDTO();
        //    }
        //    invoiceDTO.Address.AddressStreet1 = recurringPaymentDTO.Street1;
        //    invoiceDTO.Address.AddressStreet2 = recurringPaymentDTO.Street2;
        //    invoiceDTO.Address.AddressStreet3 = recurringPaymentDTO.Street3;
        //    invoiceDTO.Address.City = recurringPaymentDTO.City;
        //    invoiceDTO.Address.State = recurringPaymentDTO.State;
        //    invoiceDTO.Address.Country = recurringPaymentDTO.Country;
        //    invoiceDTO.Address.ZipCode = recurringPaymentDTO.ZipCode;
        //    invoiceDTO.ContactName = recurringPaymentDTO.ContactName;
        //    invoiceDTO.ContactPhoneNo = recurringPaymentDTO.ContactPhoneNo;
        //    //invoiceDTO.Phone = recurringPaymentDTO.Phone;
        //    invoiceDTO.CreatedBy = recurringPaymentDTO.CreatedBy;
        //    invoiceDTO.InvoicePrfixNo = 100001;
        //    invoiceDTO.InvoicePrefixString = "RECINV";
        //    invoiceDTO.OrderId = recurringPaymentDTO.OrderId;
        //    //int orderIdentityNo = _idenDS.GetIdentityNo(tenantId, (int)CoreEntityTypeEnum.Invoice, Core.Common.Constants.OrderIdPrefix, 100001);
        //    //invoiceDTO.OrderId = Core.Common.Constants.OrderIdPrefix + "" + orderIdentityNo;
        //    invoiceDTO.InvoiceName = "Invoice" + invoiceDTO.OrderId;
        //    // invoiceDTO.OrderId = recurringPaymentDTO.OrderId;
        //    invoiceDTO.PONumber = "PO-" + recurringPaymentDTO.OrderId;
        //    //DocumentCurrency dc = _documentCurrencyManager.GetDocumentCurrency(recurringId, RecurringDocumentType);
        //    //if(dc != null) {
        //    //    invoiceDTO.FromCurrencyCode = dc.DocumentCurrencyCode;
        //    //    invoiceDTO.ToCurrencyCode = dc.AgentCurrencyCode;
        //    //    invoiceDTO.ExchangeRate = dc.FinalConversionRate;
        //    //}
        //    //else {
        //    //    invoiceDTO.FromCurrencyCode = (int)CurrencyISOCode.USD;
        //    //    invoiceDTO.ToCurrencyCode = (int)CurrencyISOCode.USD;
        //    //    invoiceDTO.ExchangeRate = dc.FinalConversionRate > 0 ? dc.FinalConversionRate : 1;
        //    //}

        //    return invoiceDTO;
        //}

        ///// <summary>
        ///// Generating the short customer dto object.
        ///// </summary>
        ///// <param name="custDtlDto"></param>
        ///// <param name="recurringPaymentDTO"></param>
        ///// <returns></returns>
        //private UserPaymentInfoModel GenerateCustomerDTO(BACCustomerDetailDTO custDtlDto, RecurringPaymentDTO recurringPaymentDTO) {
        //    UserPaymentInfoModel customerDTO = new UserPaymentInfoModel();
        //    customerDTO.ID = custDtlDto.ID;
        //    customerDTO.Name = custDtlDto.Name;
        //    customerDTO.TenantId = custDtlDto.TenantId;

        //    return customerDTO;
        //}

        //#endregion  Payment

        

        #region PaymentLog

        /// <summary>
        /// Add the log after sucessfull payment.
        /// </summary>    
        /// <param name="scheduleJobDto">Schedule job object, for which making the payment.</param>
        /// <param name="recPayDto"></param>
        /// <param name="state">Whether payment successfuly processed.</param>
        /// <param name="tenantId"></param>
        private async Task AddPaymentLog(ScheduledJobDTO scheduleJobDto, RecurringPaymentDTO recPayDto, RecurringPaymentLogStateEnum state, Guid tenantId, CancellationToken token = default(CancellationToken)) {
            RecurringPaymentLog payLog = new RecurringPaymentLog();
            payLog.ScheduledDate = scheduleJobDto.ScheduledTime;
            payLog.RecurringPaymentId = scheduleJobDto.SourceId;
            payLog.TenantId = tenantId;
            payLog.Status = (int)state;
            if(recPayDto != null) {
                payLog.CreatedBy = recPayDto.CreatedBy;
                payLog.UpdatedBy = recPayDto.CreatedBy;
            }
            payLog.ID = Guid.NewGuid();

            await _recurringPaymentLogDS.AddAsync(payLog, token);
        }

        /// <summary>
        /// Update the schedule job status and log.
        /// </summary>
        /// <param name="schJob"></param>
        /// <param name="logId"></param>
        /// <param name="status"></param>
        /// <param name="reason"></param>
        private void UpdateScheduleJob(ScheduledJobDTO schJob, Guid logId, string status, string reason) {
            ScheduledJobUpdateDTO schDTO = new ScheduledJobUpdateDTO();
            schDTO.LogId = logId;
            schDTO.Status = status;
            schDTO.ScheduleJobId = schJob.ID;
            schDTO.Reason = reason;

            _scheduledJobManager.UpdateScheduledJobAndLog(schDTO);
        }

        #endregion PaymentLog

        #region Validation

        /// <summary>
        /// Validating recurring entity blank field nd duplicacy check.
        /// </summary>
        /// <param name="recurring"></param>
        /// <returns></returns>
        private bool ValidateRecurring(RecurringPayment recurring) {
            IList<EwpErrorData> listErrData;
            // validating tenant entity.
            if(recurring.Validate(out listErrData)) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Validation;
                error.EwpErrorDataList = listErrData;
                EwpValidationException exc = new EwpValidationException("Recurring validation error.", error.EwpErrorDataList);
                throw exc;
            }
            if(_recurringPaymentRepository.IsOrderIdExist(recurring.OrderId, recurring.TenantId, recurring.ID)) {
                EwpError error = new EwpError();
                error.ErrorType = ErrorType.Duplicate;
                EwpErrorData errorData = new EwpErrorData();
                errorData.ErrorSubType = (int)DuplicateErrorSubType.None;
                errorData.Message = "Duplicate Orderid";
                error.EwpErrorDataList.Add(errorData);
                EwpDuplicateNameException exc = new EwpDuplicateNameException("OrderIdDuplicateMessage", error.EwpErrorDataList);
                throw exc;
            }
            return true;
        }

        #endregion Validation

    }

}

