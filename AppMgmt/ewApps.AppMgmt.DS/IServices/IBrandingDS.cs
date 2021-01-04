using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {
    public interface IBrandingDS {

        /// <summary>
        /// Update Platform branding
        /// </summary>        
        List<Guid?> UpdatePlatformBrandingDetail(BrandingDTO platformBrandingDTO);

        /// <summary>
        /// Update Publisher branding
        /// </summary>        
         Task UpdatePublisherBrandingDetail(BrandingDTO publisherBrandingDTO);

        /// <summary>
        /// Update business branding
        /// </summary>        
        Task UpdatebusinessBrandingDetail(BusinessBrandingDTO businessBrandingDTO);

        /// <summary>
        /// Update ship-app branding
        /// </summary>        
        Task UpdateShipAppBrandingAsync(AppPortalBrandingDTO shipAppBrandingDTO);

        /// <summary>
        /// Update pay-app branding
        /// </summary>        
        Task UpdatePayAppBrandingAsync(AppPortalBrandingDTO payAppBrandingDTO);

        /// <summary>
        /// Update cust-app branding
        /// </summary>        
        Task UpdateCustAppBrandingAsync(AppPortalBrandingDTO custAppBrandingDTO);

        /// <summary>
        /// Update vend-app branding
        /// </summary>        
        Task UpdateVendAppBrandingAsync(AppPortalBrandingDTO vendAppBrandingDTO);

    }
}
