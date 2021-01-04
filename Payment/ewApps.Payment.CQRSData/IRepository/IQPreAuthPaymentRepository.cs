using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQPreAuthPaymentRepository {

        Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get preauthorization detail by account id.
        /// </summary>
        /// <param name="customerAccountId">Configured account id</param>
        /// <param name="expirationDate">Date to compare with expiration date or pre-auth.</param>
        /// <returns></returns>
        Task<PreAuthPaymentDTO> GetPreAuthDetailByCardIdAsync(Guid customerAccountId, DateTime expirationDate, CancellationToken token = default(CancellationToken));

    }
}
