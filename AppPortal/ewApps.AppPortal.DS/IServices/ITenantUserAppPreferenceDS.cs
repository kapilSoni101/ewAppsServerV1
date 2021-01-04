using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.PreferenceDTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public interface ITenantUserAppPreferenceDS : IBaseDS<TenantUserAppPreference> {


        /// <summary>
        /// Get platform preference list
        /// </summary>
        /// <param name="token"></param>
        /// <param name="appid"></param>
        /// <returns></returns>
        Task<PreferenceViewDTO> GetPlatformPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));
        
        /// <summary>
        /// Get publisher setup preference list
        /// </summary>
        Task<PreferenceViewDTO> GetPublisherPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business setup preference list
        /// </summary>
        Task<PreferenceViewDTO> GetBusSetupPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business customer app preference list
        /// </summary>
        Task<PreferenceViewDTO> GetBusCustPreferenceListAsync(Guid appid,CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business payment app preference list.
        /// </summary>
        Task<PreferenceViewDTO> GetBusPayPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business vendor app preference list.
        /// </summary>
        Task<PreferenceViewDTO> GetBusVendorPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer setup preference list
        /// </summary>
        Task<PreferenceViewDTO> GetCustSetupPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get customer customer app preference list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PreferenceViewDTO> GetCustCustAppPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get customer Payment app preference list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PreferenceViewDTO> GetCustPayAppPreferenceListAsync(Guid appid, CancellationToken token = default(CancellationToken));


        
        /// <summary>
        /// Update platform preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdatePlatformPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update publisher preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdatePublisherPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update business setup preference list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateBusSetupPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update business customer app list 
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateBusCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update business payment app list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateBusPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

        Task UpdateBusinessPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update customer portal customer app list 
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateCustCustPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));

        
        /// <summary>
        /// Update Customer payment app list
        /// </summary>
        /// <param name="preferenceUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdateCustPayPreferenceListAsync(PreferenceUpdateDTO preferenceUpdateDTO, CancellationToken token = default(CancellationToken));


        Task<PreferenceViewDTO> GetBusinessPreferenceListByAppIdAsync(Guid appid, CancellationToken token = default(CancellationToken));
    }
}
