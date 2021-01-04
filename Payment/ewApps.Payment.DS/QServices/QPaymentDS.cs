/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 Septemeber 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.Money;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {
    /// <summary>
    /// A wrapper class contains method to get payment data w.r.t Invoice.
    /// </summary>
    public class QPaymentDS: IQPaymentDS {

        #region Local variables

        IQPaymentRepository _paymentRep;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentRep"></param>
        public QPaymentDS(IQPaymentRepository paymentRep) {
            _paymentRep = paymentRep;
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
            return await _paymentRep.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentDetailDQ> GetPaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            PaymentDetailDQ payDto = await _paymentRep.GetPaymentHistoryByPaymentIdAsync(paymentId, token);

            if (payDto != null && payDto.DocumentCurrencyCode != 0)
            {
              CurrencyCultureInfo info = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)payDto.DocumentCurrencyCode);
              payDto.LocalCurrency = info.Symbol;
            }
            if (!string.IsNullOrEmpty(payDto.CustomerAccountNumber))
            {
              payDto.CustomerAccountNumber = GetDecryptValue(payDto.CustomerAccountNumber);
            }

            return payDto;
        }       


        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            // Get the items in sorted order by created date and Payment Id.
            IList<PaymentDetailDQ> listDTO = await _paymentRep.GetCustomerFilterPaymentHistoryAsync(filter, token);
            int listCount = listDTO.Count;

            Core.CommonService.CryptoHelper cryptoHelper = new Core.CommonService.CryptoHelper();
            List<PaymentDetailDQ> listNewDTO = new List<PaymentDetailDQ>();
            // Getiing loop through to merge multiple payment of invoices.
            int newListCount = 0;
            for(int i = 0; i < listCount; i++) {
                if(string.IsNullOrEmpty(listDTO[i].LocalCurrency) && listDTO[i].DocumentCurrencyCode != 0) {
                    CurrencyCultureInfo info = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
                    listDTO[i].LocalCurrency = info.Symbol;
                }
                listDTO[i].CustomerAccountNumber = GetDecryptValue(listDTO[i].CustomerAccountNumber, cryptoHelper);
                newListCount = listNewDTO.Count;
                listDTO[i].CalculateAmount = listDTO[i].AmountPaidFC;
                if(newListCount > 0 && listDTO[i].PaymentId == listNewDTO[newListCount - 1].PaymentId) {
                    // Previous added item.
                    PaymentDetailDQ dto = listNewDTO[newListCount - 1];
                    // Sum new item because both itesm payment made in single transection.
                    dto.CalculateAmount += listDTO[i].CalculateAmount;
                    // Sum new item because both itesm payment made in single transection.
                    dto.AmountPaid += listDTO[i].AmountPaid;
                    dto.AmountPaidFC += listDTO[i].AmountPaidFC;
                    dto.TotalInvoice += 1;
                }
                else {
                    listNewDTO.Add(listDTO[i]);
                }
            }
            return listNewDTO;
        }

        /// <summary>
        /// Get tenant payment list filter by tenant and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetFilterTenantPaymentHistoryAsync(filter, token);
            return SetPaymentCurrencyAndAccountNumber(list);            
        }

        /// <summary>
        /// Get vendor payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<VendorPaymentDetailDQ>> GetFilterVendorTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<VendorPaymentDetailDQ> list = await _paymentRep.GetFilterVendorTenantPaymentHistoryAsync(filter, token);
            return SetVendorPaymentCurrencyAndAccountNumber(list);
        }

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetVoidFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetVoidFilterTenantPaymentHistoryAsync(filter, token);
            return SetPaymentCurrencyAndAccountNumber(list);
        }

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetSattledFilterTenantPaymentHistoryAsync(filter, token);
            return SetPaymentCurrencyAndAccountNumber(list);
        }

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetPaymentHistoryByInvoiceAsync(invoiceId, token);
            return SetPaymentCurrencyAndAccountNumber(list);
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            PaymentDetailModelDTO paymentDTO = await _paymentRep.GetPaymentDTOAsync(id, invoiceId, token);
            if(paymentDTO != null) {
                paymentDTO.CustomerAccountNumber = GetDecryptValue(paymentDTO.CustomerAccountNumber);                               

                paymentDTO.CustomerAccountNumber = GenerateAccountNumberForReceipt(paymentDTO.CustomerAccountNumber);
            }
            return paymentDTO;
        }

        /// <summary>
        /// Gets Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentInfoDTO> GetPaymentInfoDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetPaymentInfoDTOAsync(paymentId, token);
        }

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid id, CancellationToken token = default(CancellationToken)) {
            List<PaymentDetailModelDTO> listDTO = await _paymentRep.GetPaymentDTOListAsync(id, token);
            PaymentDetailModelDTO paymentDTO = listDTO.Count > 0 ? listDTO[0] : null;
            if(paymentDTO != null) {
                // Getiing loop through to merge multiple payment of invoices.

                for(int i = 1; i < listDTO.Count; i++) {
                    decimal amount = listDTO[i].AmountPaid;
                    paymentDTO.AmountPaid += amount;
                    paymentDTO.AmountPaidFC += listDTO[i].AmountPaidFC;
                    paymentDTO.TotalPaymentDue += listDTO[i].TotalPaymentDue;
                    paymentDTO.TotalPaymentDueFC += listDTO[i].TotalPaymentDueFC;
                    //paymentDTO. += listDTO[i].TotalPaymentDue;
                    //paymentDTO.TotalPaymentDueFC += listDTO[i].TotalPaymentDueFC;

                }
                paymentDTO.CustomerAccountNumber = GetDecryptValue(paymentDTO.CustomerAccountNumber);

                paymentDTO.CustomerAccountNumber = GenerateAccountNumberForReceipt(paymentDTO.CustomerAccountNumber);
            }

            return paymentDTO;
        }

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            PaymentAdvanceDetailModelDTO paymentDTO = await _paymentRep.GetAdvancePaymentDTOAsync(paymentId, token);
            if(paymentDTO != null) {
                paymentDTO.CustomerAccountNumber = GetDecryptValue(paymentDTO.CustomerAccountNumber);

                paymentDTO.CustomerAccountNumber = GenerateAccountNumberForReceipt(paymentDTO.CustomerAccountNumber);
                CurrencyCultureInfo info = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)paymentDTO.CurrencyCode);
                paymentDTO.LocalCurrency = info.Symbol;
            }
            return paymentDTO;
        }

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        public async Task<List<PaymentDetailModelDTO>> GetPaymentDTOListAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetPaymentDTOListAsync(paymentId, token);
        }

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>        
        public async Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoicesByPaymentIdAsync(paymentId, token);
        }

        #endregion Get

        #region Get Advance Payment

        /// <summary>
        /// Get advace payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        public async Task<PaymentDetailDQ> GetAdvancePaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken)) {
            PaymentDetailDQ payDto = await _paymentRep.GetAdvancePaymentHistoryByPaymentIdAsync(paymentId, token);
            if(payDto != null && payDto.DocumentCurrencyCode != 0) {
                CurrencyCultureInfo info = new CurrencyCultureInfoTable(null).GetCultureInfo((CurrencyISOCode)payDto.DocumentCurrencyCode);
                payDto.LocalCurrency = info.Symbol;
            }
            if(!string.IsNullOrEmpty(payDto.CustomerAccountNumber)) {
                payDto.CustomerAccountNumber = GetDecryptValue(payDto.CustomerAccountNumber);
            }

            return payDto;
        }

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetFilterTenantAdvancePaymentHistoryAsync(filter, token);            
            return SetPaymentCurrencyAndAccountNumber(list);
        }

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PaymentDetailDQ> list = await _paymentRep.GetCustomerAdvanceFilterPaymentHistoryAsync(filter, token);
            return SetPaymentCurrencyAndAccountNumber(list);
        }

        #endregion Get Advance Payment

        #region Support

        /// <summary>
        /// Set payment model account number and currency.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public IList<PaymentDetailDQ> SetPaymentCurrencyAndAccountNumber(IList<PaymentDetailDQ> list) {            
            PaymentDetailDQ payDto;
            Core.CommonService.CryptoHelper helper = new Core.CommonService.CryptoHelper();
            
            CurrencyCultureInfoTable curInfoTbl = new CurrencyCultureInfoTable(null);
            for(int i = 0; i < list.Count; i++) {
                payDto = list[i];
                if(payDto.DocumentCurrencyCode != 0) {
                    CurrencyCultureInfo info = curInfoTbl.GetCultureInfo((CurrencyISOCode)payDto.DocumentCurrencyCode);
                    payDto.LocalCurrency = info.Symbol;
                }
                payDto.CustomerAccountNumber = GetDecryptValue(payDto.CustomerAccountNumber, helper);
            }

            return list;
        }

        /// <summary>
        /// Set payment model account number and currency.
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public IList<VendorPaymentDetailDQ> SetVendorPaymentCurrencyAndAccountNumber(IList<VendorPaymentDetailDQ> list) {
            VendorPaymentDetailDQ payDto;
            Core.CommonService.CryptoHelper helper = new Core.CommonService.CryptoHelper();

            CurrencyCultureInfoTable curInfoTbl = new CurrencyCultureInfoTable(null);
            for(int i = 0; i < list.Count; i++) {
                payDto = list[i];
                if(payDto.DocumentCurrencyCode != 0) {
                    CurrencyCultureInfo info = curInfoTbl.GetCultureInfo((CurrencyISOCode)payDto.DocumentCurrencyCode);
                    payDto.LocalCurrency = info.Symbol;
                }
                payDto.CustomerAccountNumber = GetDecryptValue(payDto.CustomerAccountNumber, helper);
            }

            return list;
        }

        /// <summary>
        /// Get formatted account number for showing in Payment receipt.
        /// </summary>
        /// <param name="CustomerAccountNumber"></param>
        /// <returns></returns>
        private string GenerateAccountNumberForReceipt(string CustomerAccountNumber) {
            if(!string.IsNullOrEmpty(CustomerAccountNumber) && CustomerAccountNumber.Length > 4) {
                int len = CustomerAccountNumber.Length;
                string str = CustomerAccountNumber.Substring(len - 4);
                string value = "";
                for(int i = 0; i < len - 4; i++) {
                    value += "X";
                }
                value += str;
                CustomerAccountNumber = value;
            }

            return CustomerAccountNumber;
        }

        private string GetEncryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Encrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        private string GetDecryptValue(string value) {
            return new Core.CommonService.CryptoHelper().Decrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        private string GetDecryptValue(string value, Core.CommonService.CryptoHelper helper) {
            return helper.Decrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        #endregion Support

        #region vendor

        /// <summary>
        /// Get vendor payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        public async Task<IList<VendorPaymentDetailDQ>> GetVendorPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            IList<VendorPaymentDetailDQ> list = await _paymentRep.GetVendorPaymentHistoryByInvoiceAsync(invoiceId, token);
            return SetVendorPaymentCurrencyAndAccountNumber(list);
        }

        #endregion Vendor

    }
}
