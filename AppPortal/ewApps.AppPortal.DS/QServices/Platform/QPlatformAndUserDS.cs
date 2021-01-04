/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 24 September 2018

 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.QData;
using ewApps.AppPortal.DTO;
using ewApps.Core.DMService;
using ewApps.AppPortal.DTO.DBQuery;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public class QPlatformAndUserDS:IQPlatformAndUserDS {


        #region Local member

        IQPlatformAndUserRepository _qPlatformAndUserRepository;
        IEntityThumbnailDS _entityThumbnailDS;

        #endregion


        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qIPlatformRepository"></param>
        /// <param name="entityThumbnailDS"></param>
        public QPlatformAndUserDS(IQPlatformAndUserRepository qIPlatformRepository, IEntityThumbnailDS entityThumbnailDS) {
            _qPlatformAndUserRepository = qIPlatformRepository;
            _entityThumbnailDS = entityThumbnailDS;

        }

        #endregion


        #region GET Platform Branding

        /// <summary>
        /// get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PlatformBranding Model</returns>
        public async Task<PlatformBrandingDQ> GetPlatformBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            PlatformBrandingDQ platformBrandingDQ = await _qPlatformAndUserRepository.GetPlatformBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            platformBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return platformBrandingDQ;
        }

        #endregion

        #region Get App Details

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsAsync() {

            List<AppDetailDTO> appList = await _qPlatformAndUserRepository.GetAppDetailsAsync();
            return appList;
        }

        ///<inheritdoc/>
        public async Task<AppDetailDTO> GetAppDetailsByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {

            AppDetailDTO appDetail = await _qPlatformAndUserRepository.GetAppDetailsByAppIdAsync(appId, token);
            //if(appDetail.ThumbnailId != null) {
            //    // appList[i].ThumbnailUrl = string.Format(_dMAppSettings.ThumbnailUrl + appList[i].ThumbnailId + "/" + appList[i].FileName);
            //}
            return appDetail;
        }


        #endregion

        #region Users

        public async Task<List<TenantUserDetailsDTO>> GetAllPlatfromUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qPlatformAndUserRepository.GetPlatformTenantUsers(userType, tenantId, appId, deleted);
        }

        // Check last admin user.
        public async Task<Tuple<bool, Guid>>  CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId, Guid? businessPartnerTenantId) {

            bool result = false;

            List<RoleKeyCountDTO> roleKeyCountDTO = await _qPlatformAndUserRepository.CheckUserIsLastAdminUserAsync(tenantId, userType, appId, businessPartnerTenantId);
            if(roleKeyCountDTO != null) {
                if(roleKeyCountDTO.Count == 1) {
                    result = true;
                    Tuple<bool, Guid> t = new Tuple<bool, Guid>(result, roleKeyCountDTO[0].TenantUserId);
                    return t;
                }
            }
            Tuple<bool, Guid> t1 = new Tuple<bool, Guid>(result, Guid.Empty);
            return t1;
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted) {
            return await _qPlatformAndUserRepository.GetTenantUserDetails(tenantUserId, tenantId, appKey, deleted);
        }

        #endregion Users
    }
}
