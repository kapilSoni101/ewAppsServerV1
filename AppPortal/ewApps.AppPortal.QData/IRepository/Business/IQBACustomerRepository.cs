using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {

    /// <summary>
    /// interfcae expose method to get customer and related info.
    /// </summary>
    public interface IQBACustomerRepository {

        /// <summary>
        /// Get customer info by business entity customer id.
        /// </summary>
        /// <param name="baCustomerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerInfoAsync(Guid baCustomerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer info by business partner id.
        /// </summary>
        /// <param name="businessPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CustomerPaymentInfoDTO> GetCustomerInfoByBusinessPartnerIdAsync(Guid businessPartnerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business app service list by application and tenant id.
        /// </summary>
        /// <param name="appIdList">Application id list.</param>
        /// <param name="tenantId">Tenantid.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<BusAppServiceDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(List<Guid> appIdList, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken));

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
        /// Get service account detail.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="entityId"></param>
        /// <returns>return service account detail.</returns>
        Task<List<AppServiceAcctDetailDTO>> GetAppServiceAccountDetailByCustomerIdAsync(Guid tenantId, Guid entityId, CancellationToken token = default(CancellationToken));

    }
}
