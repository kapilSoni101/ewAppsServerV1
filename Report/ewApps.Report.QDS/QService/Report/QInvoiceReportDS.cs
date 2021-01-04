/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Invoice Report 
    /// </summary>
    public class QInvoiceReportDS :BaseDS<BaseDTO>, IQInvoiceReportDS {

    #region Local Member

    IQInvoiceReportRepository _invoiceRptRepos;
    IUserSessionManager _userSessionManager;

    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="businessPartnerInvoiceReportRepository"></param>
    /// <param name="cacheService"></param>
    public QInvoiceReportDS(IQInvoiceReportRepository invoiceRptRepos,  IUserSessionManager userSessionManager) : base(invoiceRptRepos) {
      _invoiceRptRepos = invoiceRptRepos;
      _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<BizInvoiceReportDTO>> GetBizPayInvoiceListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession us = _userSessionManager.GetSession();
      return await _invoiceRptRepos.GetBizPayInvoiceListByTenantIdAsync(filter, us.TenantId, token);
    }

        ///<inheritdoc/>
        public async Task<List<VendAPInvoicesReportDTO>> GetBizVendInvoiceListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _invoiceRptRepos.GetBizVendInvoiceListByTenantIdAsync(filter, us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<PartInvoiceReportDTO>> GetPartPayInvoiceListByCustomerAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken)) {
      List<PartInvoiceReportDTO>  listDTO = await _invoiceRptRepos.GetPartPayInvoiceListByCustomerAsync(filter, token);
      //Money cuurentMoney;
      //Money cuurentPaidMoney;
      //// calculating the Amount as per exchange rate.
      //for (int i = 0; i < listDTO.Count; i++) {
      //  cuurentMoney = new Money(listDTO[i].Amount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  Money exchangeMoney = cuurentMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateAmount = exchangeMoney.Amount;

      //  cuurentPaidMoney = new Money(listDTO[i].AmountPaid, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  Money exchangePaidMoney = cuurentPaidMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateAmountPaid = exchangePaidMoney.Amount;

      //  cuurentPaidMoney = new Money(listDTO[i].OutStanding, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  exchangePaidMoney = cuurentPaidMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateOutStanding = exchangePaidMoney.Amount;
      //}

      return listDTO;
    }
   

    #endregion
  }
}
