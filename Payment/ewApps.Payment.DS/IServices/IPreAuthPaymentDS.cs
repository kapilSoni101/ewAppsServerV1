using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {
    /// <summary>
    /// Contains methos to authrozie some amount for future use.
    /// </summary>
    public interface IPreAuthPaymentDS:IBaseDS<PreAuthPayment> {

        /// <summary>
        /// Method will authorized some amount for card.
        /// </summary>
        /// <param name="payDto"> entity to be added</param>
        /// <param name="token"></param>
        Task<PaymentDQ> AddPreAuthPaymentAsync(AddPreAuthPaymentDTO payDto, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Void pre auth payment transaction.
        /// </summary>
        /// <param name="prePaymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task voidPreAuthPaymentAsync(Guid prePaymentId, CancellationToken token = default(CancellationToken));

        #region Get

        /// <summary>
        /// Get preauthorized payment history.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
