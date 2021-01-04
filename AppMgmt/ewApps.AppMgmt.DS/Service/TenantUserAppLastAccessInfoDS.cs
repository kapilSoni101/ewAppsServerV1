/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 14 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 Aug 2019
 */

using System;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// This class implements standard business logic and operations for AppLastAccessInfo entity.
    /// </summary>
    public class TenantUserAppLastAccessInfoDS:BaseDS<TenantUserAppLastAccessInfo>, ITenantUserAppLastAccessInfoDS {

        #region Local Member 

        ITenantUserAppLastAccessInfoRepository _tenantUserAppLastAccessInfoRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables through dependency injection.
        /// </summary>
        public TenantUserAppLastAccessInfoDS(ITenantUserAppLastAccessInfoRepository tenantUserAppLastAccessInfoRepository) : base(tenantUserAppLastAccessInfoRepository) {
            _tenantUserAppLastAccessInfoRepository = tenantUserAppLastAccessInfoRepository;
        }

        #endregion

        #region public methods 

        ///<inheritdoc/>
        public async Task<bool> AddUpdateTenantUserLastAccessInfoAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO, Guid appId) {

            // Add entity flag.
            bool addFlag = false;

            // Get entity if already exists.
            TenantUserAppLastAccessInfo existingAappLastAccessInfo = await _tenantUserAppLastAccessInfoRepository.FindAsync(i => i.AppId == appId && i.TenantUserId == tenantUserAppLastAccessInfoRequestDTO.TenantUserId && i.TenantId == tenantUserAppLastAccessInfoRequestDTO.TenantId, false);

            // If entity does not exists then the add case. Else update the entity.
            if(existingAappLastAccessInfo == null) {

                // Mapping the entity.
                TenantUserAppLastAccessInfo appLastAccessInfo = new TenantUserAppLastAccessInfo();
                appLastAccessInfo.AppId = appId;
                appLastAccessInfo.TenantUserId = tenantUserAppLastAccessInfoRequestDTO.TenantUserId;
                appLastAccessInfo.LoginDateTime = DateTime.UtcNow;
                if(string.IsNullOrEmpty(tenantUserAppLastAccessInfoRequestDTO.Language)) {
                    appLastAccessInfo.Language = ewApps.AppMgmt.Common.Constants.DefaultLanguage;
                }
                else {
                    appLastAccessInfo.Language = tenantUserAppLastAccessInfoRequestDTO.Language;
                }
                if(string.IsNullOrEmpty(tenantUserAppLastAccessInfoRequestDTO.Region)) {
                    appLastAccessInfo.Region = ewApps.AppMgmt.Common.Constants.DefaultRegion;
                }
                else {
                    appLastAccessInfo.Region = tenantUserAppLastAccessInfoRequestDTO.Language;
                }
                appLastAccessInfo.TimeZone = tenantUserAppLastAccessInfoRequestDTO.TimeZone;
                appLastAccessInfo.Browser = tenantUserAppLastAccessInfoRequestDTO.Browser;
                appLastAccessInfo.CreatedOn = DateTime.UtcNow;
                appLastAccessInfo.CreatedBy = tenantUserAppLastAccessInfoRequestDTO.TenantUserId;
                appLastAccessInfo.UpdatedBy = appLastAccessInfo.CreatedBy;
                appLastAccessInfo.UpdatedOn = appLastAccessInfo.CreatedOn;
                //UpdateSystemFieldsByOpType(appLastAccessInfo, OperationType.Add);
                appLastAccessInfo.TenantId = tenantUserAppLastAccessInfoRequestDTO.TenantId;
                _tenantUserAppLastAccessInfoRepository.Add(appLastAccessInfo);
                addFlag = true;
            }
            else {
                // Mapping the entity.
                existingAappLastAccessInfo.LoginDateTime = DateTime.UtcNow;
                if(string.IsNullOrEmpty(tenantUserAppLastAccessInfoRequestDTO.Language)) {
                    existingAappLastAccessInfo.Language = ewApps.AppMgmt.Common.Constants.DefaultLanguage;
                    ;
                }
                else {
                    existingAappLastAccessInfo.Language = tenantUserAppLastAccessInfoRequestDTO.Language;
                }
                if(string.IsNullOrEmpty(tenantUserAppLastAccessInfoRequestDTO.Region)) {
                    existingAappLastAccessInfo.Region = ewApps.AppMgmt.Common.Constants.DefaultRegion;
                }
                else {
                    existingAappLastAccessInfo.Region = tenantUserAppLastAccessInfoRequestDTO.Language;
                }
                existingAappLastAccessInfo.TimeZone = tenantUserAppLastAccessInfoRequestDTO.TimeZone;
                existingAappLastAccessInfo.Browser = tenantUserAppLastAccessInfoRequestDTO.Browser;
                existingAappLastAccessInfo.UpdatedOn = DateTime.UtcNow;
                //UpdateSystemFieldsByOpType(existingAappLastAccessInfo, OperationType.Update);
                _tenantUserAppLastAccessInfoRepository.Update(existingAappLastAccessInfo, existingAappLastAccessInfo.ID);
            }
            return addFlag;
        }

        #endregion

    }
}
