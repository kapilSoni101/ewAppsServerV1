using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessEntity.DTO;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Contains all required customer SignUp methods.
    /// </summary>
    public interface ICustomerSignupDS {

        /// <summary>
        /// SignUp customer
        /// </summary>
        /// <param name="customerSyncDTO"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        //Task<bool> SignUp(BACustomerSyncDTO customerSyncDTO, Guid tenantId, Guid tenantUserId, CancellationToken token = default(CancellationToken));
        Task<bool> AddCustomerListAsync(List<BACustomerSyncDTO> customerDetailList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        #region Signup Customer in Standalone Mode

        /// <summary>
        /// Signs up customer.
        /// </summary>
        /// <param name="customerSyncDTO">customer dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<CustomerSignUpDTO> SignUpCustomerAsync(SignUpBACustomerDTO customerSyncDTO, CancellationToken token = default(CancellationToken));

        #endregion Signup Customer in Standalone Mode
    }
}
