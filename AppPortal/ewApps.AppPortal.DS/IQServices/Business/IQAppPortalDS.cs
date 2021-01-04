using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQAppPortalDS {


        /// <summary>
        /// Get Ship-App branding model
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetShipAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Pay-App branding model
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetPayAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Cust-App branding model
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetCustAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Vend-App branding model
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AppPortalBrandingDQ> GetVendAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


    }
}
