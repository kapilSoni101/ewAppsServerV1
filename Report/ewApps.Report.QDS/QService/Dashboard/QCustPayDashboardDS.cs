/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 07 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 07 October 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Logic of Customer Payment Dashboard 
    /// </summary>
    public class QCustPayDashboardDS:BaseDS<BaseDTO>, IQCustPayDashboardDS {

        #region Local Varialbe 
        IQCustPayDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        public QCustPayDashboardDS(IQCustPayDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }

        #region PartPay DataService
        ///<inheritdoc/>
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            BACInvoiceStatusCountDTO bACInvoiceStatusCountDTO = new BACInvoiceStatusCountDTO();
            int totalCount = 0;
            bACInvoiceStatusCountDTO = await _dashboardRepository.GetInvoicesStatusCountForDashBoardByCustomerAsync(customerId, token);
            if(bACInvoiceStatusCountDTO != null) {
                totalCount = bACInvoiceStatusCountDTO.TotalOpenInvoices + bACInvoiceStatusCountDTO.TotalPaidInvoices + bACInvoiceStatusCountDTO.PartialPaid;
                bACInvoiceStatusCountDTO.NotPaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.TotalOpenInvoices / totalCount) * 100), 2);
                bACInvoiceStatusCountDTO.PartialPaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.PartialPaid / totalCount) * 100), 2);
                bACInvoiceStatusCountDTO.PaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.TotalPaidInvoices / totalCount) * 100), 2);
            }
            return bACInvoiceStatusCountDTO;
        }

        ///<inheritdoc/>
        public async Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardRepository.GetBusinessNameAndSumOfInvoiceByCustomerListAsync(customerId, token);
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            List<UpComingPaymentDTO> listDTO = new List<UpComingPaymentDTO>();
            listDTO.AddRange(await _dashboardRepository.GetAllUpcomingPaymentByCustomerListAsync(customerId, token));
            //listDTO.AddRange(await _dashboardRepository.GetAllUpcomingRecurringPaymentByCustomerListAsync(customerId, token));
            //Money cuurentMoney;
            //Money cuurentPaidMoney;

            //// calculating the Amount as per exchange rate.
            //for(int i = 0; i < listDTO.Count; i++) {
            //    cuurentMoney = new Money(listDTO[i].Amount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
            //    Money exchangeMoney = cuurentMoney * listDTO[i].FinalConversionRate;
            //    listDTO[i].CalculateAmount = exchangeMoney.Amount;

            //    cuurentPaidMoney = new Money(listDTO[i].OutStanding, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
            //    Money exchangePaidMoney = cuurentPaidMoney * listDTO[i].FinalConversionRate;
            //    listDTO[i].CalculateOutStanding = exchangePaidMoney.Amount;
            //}

            return listDTO;
        }

        ///<inheritdoc/>
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            List<RecentPaymentDTO> listDTO = await _dashboardRepository.GetAllRecentPaymentByCustomerListAsync(customerId, token);
            //Money cuurentMoney;
            //Money cuurentPaidMoney;

            //// calculating the Amount as per exchange rate.
            //for(int i = 0; i < listDTO.Count; i++) {
            //    cuurentMoney = new Money(listDTO[i].Amount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
            //    Money exchangeMoney = cuurentMoney * listDTO[i].FinalConversionRate;
            //    listDTO[i].CalculateAmount = exchangeMoney.Amount;

            //    cuurentPaidMoney = new Money(listDTO[i].CreditAmount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
            //    Money exchangePaidMoney = cuurentPaidMoney * listDTO[i].FinalConversionRate;
            //    listDTO[i].CalculateCreditAmount = exchangePaidMoney.Amount;
            //}

            return listDTO;
        }

        ///<inheritdoc/>
        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByCustomerListAsync(Guid customerId,CancellationToken token = default(CancellationToken)) {            
            return await _dashboardRepository.GetAllRecentInvoicesByCustomerListAsync(customerId, token);
        }

        #endregion

    }
}
