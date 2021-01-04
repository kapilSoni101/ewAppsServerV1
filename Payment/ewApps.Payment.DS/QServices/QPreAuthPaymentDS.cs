using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.Money;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {

    /// <summary>
    /// Wrapper class to get pre auth payment information.
    /// </summary>
    public class QPreAuthPaymentDS:IQPreAuthPaymentDS {

        #region Local variables

        IQPreAuthPaymentRepository _paymentRep;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentRep"></param>
        public QPreAuthPaymentDS(IQPreAuthPaymentRepository paymentRep) {
            _paymentRep = paymentRep;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get preauthorization detail by account id.
        /// </summary>
        /// <param name="customerAccountId">Configured account id</param>
        /// <param name="expirationDate">Date to compare with expiration date or pre-auth.</param>
        /// <returns></returns>
        public async Task<PreAuthPaymentDTO> GetPreAuthDetailByCardIdAsync(Guid customerAccountId, DateTime expirationDate, CancellationToken token = default(CancellationToken)) {
            PreAuthPaymentDTO dto = await _paymentRep.GetPreAuthDetailByCardIdAsync(customerAccountId, expirationDate, token);
            if(dto != null) {
                dto.AccountJson = GetDecryptValue(dto.AccountJson, new Core.CommonService.CryptoHelper());
            }
            return dto;
        }

        /// <summary>
        /// Get filtered authorized amount.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<PreAuthPaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            IList<PreAuthPaymentDetailDQ> listDTO = await _paymentRep.GetFilterTenantPaymentHistoryAsync(filter, token);
            Core.CommonService.CryptoHelper cryptoHelper = new Core.CommonService.CryptoHelper();
            List<PaymentDetailDQ> listNewDTO = new List<PaymentDetailDQ>();

            for(int i = 0; i < listDTO.Count; i++) {
                listDTO[i].CustomerAccountNumber = GetDecryptValue(listDTO[i].CustomerAccountNumber, cryptoHelper);               
            }

            return listDTO;
        }

        private string GetDecryptValue(string value, Core.CommonService.CryptoHelper helper) {
            return helper.Decrypt(value, Core.CommonService.Constants.DefaultEncryptionAlgo);
        }

        #endregion Get

    }
}
