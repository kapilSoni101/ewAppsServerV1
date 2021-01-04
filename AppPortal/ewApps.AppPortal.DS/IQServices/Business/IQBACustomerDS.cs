using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {
    public interface IQBACustomerDS {

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerInfoAsync(Guid baCustomerId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer info by businessPartnerTenantId.
        /// </summary>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerIdAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get app services and attrubute list.
        /// </summary>
        /// <param name="appId">Application id.</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="customerId">customer id</param>
        /// <param name="includeAttributeAccountDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusAppServiceDTO>> GetAppServiceListByAppIdAndTenantAsync(Guid appId, Guid tenantId, Guid customerId, bool includeAttributeAccountDetail, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get service attribute list by service ids.
        /// </summary>
        /// <param name="serviceIdList">List of AppService Id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BusAppServiceAttributeDTO>> GetBusinessAppServiceAttributeListByServiceIdsAsync(List<Guid> serviceIdList, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get app service by appkey and tenantid.
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppKeyAndTenantIdAsync(string appKey, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get app services and attrubute list.
        /// </summary>
        /// <param name="appKey">Application key.</param>
        /// <param name="tenantId">Tenant id</param>
        /// <param name="customerId">customer id</param>
        /// <param name="includeAttributeAccountDetail"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusAppServiceDTO>> GetAppServiceListByAppKeyAndTenantAsync(string appKey, Guid tenantId, Guid customerId, bool includeAttributeAccountDetail, CancellationToken token = default(CancellationToken));

    }
}
