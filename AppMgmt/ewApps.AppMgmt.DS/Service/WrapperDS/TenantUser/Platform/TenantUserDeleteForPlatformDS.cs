using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppMgmt.DS{
   public class TenantUserDeleteForPlatformDS :ITenantUserDeleteForPlatformDS {

        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;
        IAppDS _appDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDataService;
        IIdentityServerDS _identityServerDS;

        public TenantUserDeleteForPlatformDS( ITenantUserDS tenantUserDS, IIdentityServerDS identityServerDS,
            IAppDS appDS, IUnitOfWork unitOfWork, IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDataService) {
            _tenantUserDS = tenantUserDS;
            _unitOfWork = unitOfWork;
            _appDS = appDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDataService = tenantUserAppLinkingDataService;
            _identityServerDS = identityServerDS;
        }


        ///<inheritdoc/>
        public async Task<DeleteUserResponseDTO> DeleteTenantUserAsync(TenantUserIdentificationDTO tenantUserIdentificationDTO) {

            // Delete user hierarchy
            await DeleteUserHierarchy(tenantUserIdentificationDTO.TenantUserId, tenantUserIdentificationDTO.TenantId, tenantUserIdentificationDTO.AppId);

            // Delete user dependency we will do it through stored procedure.
            //1) preferecen
            //2) session
            //3) tokeninfo
            //4) rolelinking
            await _tenantUserDS.DeleteUserDependencyAsync(tenantUserIdentificationDTO.TenantUserId, tenantUserIdentificationDTO.TenantId, tenantUserIdentificationDTO.AppId, null);

            // Save Changes
            _unitOfWork.Save();

            return new DeleteUserResponseDTO {
                TenantId = tenantUserIdentificationDTO.TenantId,
                UserDeleted = true
            };
        }

        // This method is deleting user hierarchy.
        private async Task DeleteUserHierarchy(Guid tenantUserId, Guid tenantId, Guid appId) {

            #region Local variable

            bool deassignApplicationOnly = false;
            bool deletedUserIdentityServer = false;

            #endregion Local variable

            // Get user.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserId);
            _tenantUserDS.ValidateTenantUser(tenantUser, OperationType.Delete);

            // IF user exists and is not already deleted.           
            if(tenantUser != null && tenantUser.Deleted == false) {

                List<TenantUserAppLinking> tenantUserAppLinkingList = (await _tenantUserAppLinkingDataService.FindAllAsync(u => u.TenantId == tenantId && u.TenantUserId == tenantUserId && u.Deleted == false)).ToList();
                if(tenantUserAppLinkingList != null && tenantUserAppLinkingList.Count > 0) {
                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDataService.FindAsync(u => u.TenantId == tenantId && u.TenantUserId == tenantUserId && u.AppId == appId && u.Deleted == false);
                    if(tenantUserAppLinking != null) {
                        tenantUserAppLinking.Deleted = true;
                        _tenantUserAppLinkingDataService.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Update);
                        _tenantUserAppLinkingDataService.Update(tenantUserAppLinking, tenantUserAppLinking.ID);
                        // Deasssign this application on identity server.
                        deassignApplicationOnly = true;
                    }
                    if(tenantUserAppLinkingList.Count == 1) {
                        // Now get teanntuser linking
                        List<UserTenantLinking> userTenantLinkingList = (await _userTenantLinkingDS.FindAllAsync(t => t.TenantUserId == tenantUserId && t.Deleted == false)).ToList();
                        if(userTenantLinkingList != null && userTenantLinkingList.Count > 0) {
                            UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(t => t.TenantUserId == tenantUserId && t.TenantId == tenantId);
                            if(userTenantLinking != null) {
                                // If primary is being deleted then change primary user then deleted the user.
                                if(userTenantLinking.IsPrimary == true) {
                                    await ChangePrimaryUserWhenDeleted(userTenantLinking);
                                }
                                userTenantLinking.IsPrimary = false;
                                userTenantLinking.Deleted = true;
                                _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Update);
                                _userTenantLinkingDS.Update(userTenantLinking, userTenantLinking.ID);
                                // As this user is the last user of the tenant we have to delete it from identity server.
                                deletedUserIdentityServer = true;
                                deassignApplicationOnly = false;
                            }
                            if(userTenantLinkingList.Count == 1) {
                                TenantUser tenantUserToDelete = await _tenantUserDS.GetAsync(tenantUserId);
                                tenantUserToDelete.Deleted = true;
                                _tenantUserDS.UpdateSystemFieldsByOpType(tenantUserToDelete, OperationType.Update);
                                _tenantUserDS.Update(tenantUserToDelete, tenantUserToDelete.ID);
                            }
                            if(deassignApplicationOnly == false && deletedUserIdentityServer == true) {
                                Task deleteTask = _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId);
                            }
                            else if(deassignApplicationOnly) {
                                App app = await _appDS.GetAsync(appId);
                                Task deAssignApp = _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, app.AppKey);
                            }
                        }
                    }
                }
                else {
                    // Now get teanntuser linking
                    List<UserTenantLinking> userTenantLinkingList = (await _userTenantLinkingDS.FindAllAsync(t => t.TenantUserId == tenantUserId && t.Deleted == false)).ToList();
                    if(userTenantLinkingList != null && userTenantLinkingList.Count > 0) {
                        UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(t => t.TenantUserId == tenantUserId && t.TenantId == tenantId);
                        if(userTenantLinking != null) {
                            // If primary is being deleted then change primary user then deleted the user.
                            if(userTenantLinking.IsPrimary == true) {
                                await ChangePrimaryUserWhenDeleted(userTenantLinking);
                            }
                            userTenantLinking.IsPrimary = false;
                            userTenantLinking.Deleted = true;
                            _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Update);
                            _userTenantLinkingDS.Update(userTenantLinking, userTenantLinking.ID);
                            // As this user is the last user of the tenant we have to delete it from identity server.
                            deletedUserIdentityServer = true;
                            deassignApplicationOnly = false;
                        }
                        if(userTenantLinkingList.Count == 1) {
                            TenantUser tenantUserToDelete = await _tenantUserDS.GetAsync(tenantUserId);
                            tenantUserToDelete.Deleted = true;
                            _tenantUserDS.UpdateSystemFieldsByOpType(tenantUserToDelete, OperationType.Update);
                            _tenantUserDS.Update(tenantUserToDelete, tenantUserToDelete.ID);
                        }
                        if(deassignApplicationOnly == false && deletedUserIdentityServer == true) {
                            Task deleteTask = _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId);
                        }
                        else if(deassignApplicationOnly) {
                            App app = await _appDS.GetAsync(appId);
                            Task deAssignApp = _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, app.AppKey);
                        }
                    }
                }

            }
            else {
                // throw new EwpValidationException("User already deleted.");
            }
        }

        // If Primary user is being deleted the make any other user primary.
        private async Task ChangePrimaryUserWhenDeleted(UserTenantLinking userTenantLinking) {
            List<UserTenantLinking> userTenantLinkingList = (await _userTenantLinkingDS.FindAllAsync(ut => ut.TenantId == userTenantLinking.TenantId && ut.BusinessPartnerTenantId == userTenantLinking.BusinessPartnerTenantId && ut.Deleted == false)).ToList();
            if(userTenantLinking.IsPrimary) {
                if(userTenantLinkingList != null && userTenantLinkingList.Count > 1) {
                    userTenantLinkingList.Remove(userTenantLinking);
                    userTenantLinkingList = userTenantLinkingList.OrderByDescending(a => a.CreatedOn).ToList();
                    userTenantLinkingList[0].IsPrimary = true;
                    _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinkingList[0], OperationType.Update);
                    await _userTenantLinkingDS.UpdateAsync(userTenantLinkingList[0], userTenantLinkingList[0].ID);
                }
            }
        }
    }
}
