using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IBusCustomerDS {

        #region Get

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId">BACustomer unique id</param>
        /// <param name="appId">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerAndPaymentInfoAsync(Guid baCustomerId, Guid appId, bool includeAccountDetl, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer info by businessPartnerTenantId.
        /// </summary>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="appId">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerAsync(Guid businessPartnerTenantId, Guid appId, bool includeAccountDetl, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business update model with all child entities models.
        /// </summary>
        /// <param name="tenantId">Unique tenant id of business.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusCustomerDTO>> GetCustomerListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        Task<BusCustomerDetailDTO> GetCustomerDetailByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));
        Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId">BACustomer unique id</param>
        /// <param name="appKey">Application id</param>
        /// <param name="includeAccountDetl">Include attribute account detail.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerAndPaymentAppKeyInfoAsync(Guid baCustomerId, string appKey, bool includeAccountDetl, CancellationToken token = default(CancellationToken));

        #endregion Get 

        ///<inheritdoc/>   
        Task<List<BusCustomerDTO>> GetCustomerListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken));

        Task UpdateCustomerDetail(BusCustomerDetailDTO customeAccDetailDTO, CancellationToken token = default(CancellationToken));

        Task<bool> UpdateCustomerDetailForBizSetupApp(BusCustomerUpdateDTO customerUpdateDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<bool> DeleteCustomerAsync(Guid customerId, Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken));

    }
}
