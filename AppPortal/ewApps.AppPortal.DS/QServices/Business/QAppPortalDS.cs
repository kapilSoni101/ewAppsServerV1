using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.DMService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {
    public class QAppPortalDS :IQAppPortalDS {

        #region Local Member
        IUserSessionManager _userSessionManager;
        IEntityThumbnailDS _entityThumbnailDS;
        IQAppPortalRepository _qAppPortalRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public QAppPortalDS(IUserSessionManager userSessionManager,IEntityThumbnailDS entityThumbnailDS, IQAppPortalRepository qAppPortalRepository) {
            _userSessionManager = userSessionManager;
            _entityThumbnailDS = entityThumbnailDS;
            _qAppPortalRepository = qAppPortalRepository;
        }
        #endregion



        #region GET Ship-App Branding

        /// <summary>
        /// get Ship-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<AppPortalBrandingDQ> GetShipAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBrandingDQ = await _qAppPortalRepository.GetShipAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBrandingDQ;
        }

        #endregion


        #region GET Pay-App Branding

        /// <summary>
        /// get Pay-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<AppPortalBrandingDQ> GetPayAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBrandingDQ = await _qAppPortalRepository.GetPayAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBrandingDQ;
        }

        #endregion


        #region GET Cust-App Branding

        /// <summary>
        /// get Cust-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<AppPortalBrandingDQ> GetCustAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBrandingDQ = await _qAppPortalRepository.GetCustAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBrandingDQ;
        }

        #endregion

        #region GET Vend-App Branding

        /// <summary>
        /// get Vend-App branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<AppPortalBrandingDQ> GetVendAppBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            AppPortalBrandingDQ appPortalBrandingDQ = await _qAppPortalRepository.GetVendAppBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            appPortalBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return appPortalBrandingDQ;
        }

        #endregion

    }
}
