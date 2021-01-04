/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
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
    /// This class Contain Business Logic of Business Payment Dashboard 
    /// </summary>
    public class QBizPayDashboardDS:BaseDS<BaseDTO>, IQBizPayDashboardDS {

        #region Local Varialbe 
        IQBizPayDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager; 
        #endregion

        public QBizPayDashboardDS(IQBizPayDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }

        #region BizPay DataService
        ///<inheritdoc/>
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            BACInvoiceStatusCountDTO bACInvoiceStatusCountDTO = new BACInvoiceStatusCountDTO();
            int totalCount = 0;
            bACInvoiceStatusCountDTO = await _dashboardRepository.GetInvoicesStatusCountForDashBoardByTenantAsync(us.TenantId, token);
            if(bACInvoiceStatusCountDTO != null) {
                totalCount = bACInvoiceStatusCountDTO.TotalOpenInvoices + bACInvoiceStatusCountDTO.TotalPaidInvoices + bACInvoiceStatusCountDTO.PartialPaid;
                bACInvoiceStatusCountDTO.NotPaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.TotalOpenInvoices / totalCount) * 100), 2);
                bACInvoiceStatusCountDTO.PartialPaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.PartialPaid / totalCount) * 100), 2);
                bACInvoiceStatusCountDTO.PaidPercentage = (float)System.Math.Round((((float)bACInvoiceStatusCountDTO.TotalPaidInvoices / totalCount) * 100), 2);
            }
            return bACInvoiceStatusCountDTO;
        }

        ///<inheritdoc/>
        public async Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetMonthNameAndSumOfInvoiceByTenantListAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            List<UpComingPaymentDTO> listDTO = new List<UpComingPaymentDTO>();
            listDTO.AddRange(await _dashboardRepository.GetAllUpcomingPaymentByTenantListAsync(us.TenantId, token));
            //listDTO.AddRange(await _dashboardRepository.GetAllUpcomingRecurringPaymentByTenantListAsync(us.TenantId, token));
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
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            List<RecentPaymentDTO> listDTO = await _dashboardRepository.GetAllRecentPaymentByTenantListAsync(us.TenantId, token);
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
        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllRecentInvoicesByTenantListAsync(us.TenantId, token);
        }


        #endregion

    }
}
