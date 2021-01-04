/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 31 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;

namespace ewApps.AppMgmt.DS {
    public class TenantUserExtDS :ITenantUserExtDS {

        #region Local Member

        ITenantUserDS _tenantUserDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IUnitOfWork _unitOfWork;
        IEntityThumbnailDS _entityThumbnailDS;

        #endregion Local Member

        #region Constructor

        public TenantUserExtDS(ITenantUserDS tenantUserDS , ITenantUserAppLinkingDS tenantUserAppLinkingDS , IUnitOfWork unitOfWork, IEntityThumbnailDS entityThumbnailDS) {
            _tenantUserDS = tenantUserDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _unitOfWork = unitOfWork;
            _entityThumbnailDS = entityThumbnailDS;
        }

        #endregion Constructor

        #region Public Method

        #region Get

        ///<inheritdoc/>
        public async Task<TenantUserInfoDTO> GetTenantUserInfoAsync(Guid tenantUserId) {
            // Get the user.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserId);
            TenantUserInfoDTO tenantUserInfoDTO = TenantUserInfoDTO.MapFromTenantUser(tenantUser);
            tenantUserInfoDTO.NewUser = await _tenantUserDS.UserAlreadyJoinedAnyApplication(tenantUser.ID);
            return tenantUserInfoDTO;
        }

        ///<inheritdoc/>
        public async Task<TenantUserInfoDTO> GetTenantUserInfoByEmailAsync(string email) {
            // Get the user.
            TenantUser tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == email && tu.Deleted == false);
            TenantUserInfoDTO tenantUserInfoDTO = TenantUserInfoDTO.MapFromTenantUser(tenantUser);
            tenantUserInfoDTO.NewUser = await _tenantUserDS.UserAlreadyJoinedAnyApplication(tenantUser.ID);
            return tenantUserInfoDTO;
        }

        ///<inheritdoc/>
        public async Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid appUserId) {
            TenantUser user = await _tenantUserDS.GetAsync(appUserId);
            TenantUserProfileDTO appUserDTO = new TenantUserProfileDTO();
            appUserDTO.ID = user.ID;
            appUserDTO.FirstName = user.FirstName;
            appUserDTO.LastName = user.LastName;
            appUserDTO.Email = user.Email;
            appUserDTO.Phone = user.Phone;
            appUserDTO.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(appUserId);
            return appUserDTO;
        }

        #endregion Get

        #region Update

        ///<inheritdoc/>
        public async Task UpdateTenantUserInvitationStatus(TenantUserIdentificationDTO tenantUserIdentificationDTO, int invitaionStatus) {
            TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(tu => tu.TenantUserId == tenantUserIdentificationDTO.TenantUserId && tu.TenantId == tenantUserIdentificationDTO.TenantId && tu.AppId == tenantUserIdentificationDTO.AppId && tu.Deleted == false);
            if(tenantUserAppLinking != null) {
                tenantUserAppLinking.Status = invitaionStatus;
                await _tenantUserAppLinkingDS.UpdateAsync(tenantUserAppLinking, tenantUserAppLinking.ID);
                _unitOfWork.SaveAll();
            }
        }

        ///<inheritdoc/>
        public async Task UpdateAppUser(TenantUserProfileDTO appUserDTO) {
            TenantUser user = _tenantUserDS.Get(appUserDTO.ID);
            user.FirstName = appUserDTO.FirstName;
            user.LastName = appUserDTO.LastName;
            user.Email = appUserDTO.Email;
            user.Phone = appUserDTO.Phone;
            user.FullName = appUserDTO.FirstName + " " + appUserDTO.LastName;
            if(appUserDTO.ThumbnailAddUpdateModel != null) {
                if(appUserDTO.IsAddThumbnail == true && appUserDTO.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Add) {
                    appUserDTO.ThumbnailAddUpdateModel.ThumbnailOwnerEntityType = (int)AppMgmtEntityTypeEnum.TenantUser;
                    _entityThumbnailDS.AddThumbnail(appUserDTO.ThumbnailAddUpdateModel);
                }
                else if(appUserDTO.ThumbnailAddUpdateModel.OperationType == (int)OperationType.Update) {
                    _entityThumbnailDS.UpdateThumbnail(appUserDTO.ThumbnailAddUpdateModel);
                }
            }
            else {
                if(appUserDTO.ThumbnailId != null) {
                    _entityThumbnailDS.DeleteThumbnail(appUserDTO.ThumbnailId);
                }
            }

            await _tenantUserDS.UpdateAsync(user, appUserDTO.ID);
            _tenantUserDS.UpdateSystemFieldsByOpType(user, OperationType.Update);
            _unitOfWork.SaveAll();
        }

        #endregion Update

        #endregion Public Method

    }
}
