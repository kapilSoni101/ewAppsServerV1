using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Payment Report 
    /// </summary>
    public class QPaymentReportDS : BaseDS<BaseDTO> ,IQPaymentReportDS {

    #region Local Member
    IQPaymentReportRepository _paymentReportRepository;
    IUserSessionManager _userSessionManager;
    #endregion

    #region Constructor
    /// <summary>
    ///  Constructor Initialize the Base Variable
    /// </summary>
    /// <param name="paymentReportRepository"></param>
    /// <param name="cacheService"></param>
    public QPaymentReportDS(IQPaymentReportRepository paymentReportRepository,  IUserSessionManager userSessionManager) : base(paymentReportRepository) {
      _paymentReportRepository = paymentReportRepository;
      _userSessionManager = userSessionManager;
    }

    #endregion Constructor

    #region Get

    ///<inheritdoc/>
    public async Task<List<BizPaymentReceivedReportDTO>> GetBizPayRecPayListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession us = _userSessionManager.GetSession();
      return await _paymentReportRepository.GetBizPayRecPayListByTenantIdAsync(filter, us.TenantId, token);
    }

    ///<inheritdoc/>
    public async Task<List<BizCustomerWisePaymentReportDTO>> GetBizPayCustPaymentListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      UserSession us = _userSessionManager.GetSession();
      List<BizCustomerWisePaymentReportDTO> listDTO = await _paymentReportRepository.GetBizPayCustPaymentListByTenantIdAsync(filter, us.TenantId, token);
      //Money cuurentMoney;
      //Money cuurentPaidMoney;

      //// calculating the Amount as per exchange rate.
      //for (int i = 0; i < listDTO.Count; i++) {
      //  cuurentMoney = new Money(listDTO[i].CreditAmount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  Money exchangeMoney = cuurentMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateCreditAmount = exchangeMoney.Amount;
      //}

      return listDTO;
    }

    ///<inheritdoc/>
    public async Task<List<PartPaymentReportDTO>> GetPartPaymentListAsyncByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
      List<PartPaymentReportDTO> listDTO = await _paymentReportRepository.GetPartPaymentListAsyncByCustomerAsync(filter, token);
      //Money cuurentMoney;
      //Money cuurentPaidMoney;

      //// calculating the Amount as per exchange rate.
      //for (int i = 0; i < listDTO.Count; i++) {
      //  cuurentMoney = new Money(listDTO[i].CreditAmount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  Money exchangeMoney = cuurentMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateCreditAmount = exchangeMoney.Amount;

      //  cuurentPaidMoney = new Money(listDTO[i].OriginalAmount, (CurrencyISOCode)listDTO[i].DocumentCurrencyCode);
      //  Money exchangePaidMoney = cuurentPaidMoney * listDTO[i].FinalConversionRate;
      //  listDTO[i].CalculateOriginalAmount = exchangePaidMoney.Amount;
      //}

      return listDTO;
    }

    #endregion
  }
}
