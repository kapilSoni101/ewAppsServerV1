using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// 
    /// </summary>
    public interface IAppPortalDS  {


        /// <summary>
        /// Get Ship App branding branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetShipAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Ship App branding
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        Task UpdateShipAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ);


        /// <summary>
        /// Get Pay App branding branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetPayAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Pay App branding
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        Task UpdatePayAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ);


        /// <summary>
        /// Get Cust App branding branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetCustAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Cust App branding
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        Task UpdateCustAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ);

        /// <summary>
        /// Get Vend App branding branding details by Tenantid and AppId.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetVendAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Update Vend App branding
        /// </summary>
        /// <param name="appPortalBrandingDQ"></param>
        Task UpdateVendAppBrandingAsync(AppPortalBrandingDQ appPortalBrandingDQ);



        /// <summary>
        /// Get Theme name theme key 
        /// </summary>
        /// <returns></returns>
        Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey();

        

    }
}
