using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {
    public class TenantUserDeleteForBusinessDS :ITenantUserDeleteForBusinessDS {

        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;
        IAppDS _appDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDataService;
        IIdentityServerDS _identityServerDS;

        public TenantUserDeleteForBusinessDS(ITenantUserDS tenantUserDS, IIdentityServerDS identityServerDS,
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
            await DeleteUserHierarchy(tenantUserIdentificationDTO.TenantUserId, tenantUserIdentificationDTO.TenantId);

            // Delete user dependency we will do it through stored procedure.
            //1) preferecen
            //2) session
            //3) tokeninfo
            //4) rolelinking
            await _tenantUserDS.DeleteUserDependencyAsync(tenantUserIdentificationDTO.TenantUserId, tenantUserIdentificationDTO.TenantId, tenantUserIdentificationDTO.AppId, null);

            // Save Changes
            _unitOfWork.SaveAll();

            return new DeleteUserResponseDTO {
                TenantId = tenantUserIdentificationDTO.TenantId,
                UserDeleted = true
            };
        }

        // This method is deleting user hierarchy.
        private async Task DeleteUserHierarchy(Guid tenantUserId, Guid tenantId) {

            // Get user.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserId);
            _tenantUserDS.ValidateTenantUser(tenantUser, OperationType.Delete);

            // IF user exists and is not already deleted.           
            if(tenantUser != null && tenantUser.Deleted == false) {

                List<TenantUserAppLinking> tenantUserAppLinkingList = (await _tenantUserAppLinkingDataService.FindAllAsync(u => u.TenantId == tenantId && u.TenantUserId == tenantUserId && u.Deleted == false)).ToList();
                if(tenantUserAppLinkingList != null && tenantUserAppLinkingList.Count > 0) {
                    foreach(TenantUserAppLinking item in tenantUserAppLinkingList) {
                        item.Deleted = true;
                        _tenantUserAppLinkingDataService.UpdateSystemFieldsByOpType(item, OperationType.Update);
                        _tenantUserAppLinkingDataService.Update(item, item.ID);
                    }
                }

                // Now get teanntuser linking.
                List<UserTenantLinking> userTenantLinkingList = (await _userTenantLinkingDS.FindAllAsync(t => t.TenantUserId == tenantUserId && t.Deleted == false)).ToList();
                if(userTenantLinkingList != null && userTenantLinkingList.Count > 0) {
                    UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(t => t.TenantUserId == tenantUserId && t.TenantId == tenantId);
                    if(userTenantLinking != null) {
                        // If primary is being deleted then change primary user then deleted the user.
                        if(userTenantLinking.IsPrimary == true) {
                            await ChangePrimaryUserWhenDeleted(userTenantLinking);
                        }
                        userTenantLinking.Deleted = true;
                        _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Update);
                        _userTenantLinkingDS.Update(userTenantLinking, userTenantLinking.ID);
                    }
                    if(userTenantLinkingList.Count == 1) {
                        TenantUser tenantUserToDelete = await _tenantUserDS.GetAsync(tenantUserId);
                        tenantUserToDelete.Deleted = true;
                        _tenantUserDS.UpdateSystemFieldsByOpType(tenantUserToDelete, OperationType.Update);
                        await _tenantUserDS.UpdateAsync(tenantUserToDelete, tenantUserToDelete.ID);
                    }
                }
                await _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId);
            }
            else {
                // throw new EwpValidationException("User already deleted.");
            }
        }

        // If Primary user is being deleted the make any other user primary.
        private async Task ChangePrimaryUserWhenDeleted(UserTenantLinking userTenantLinking) {
            List<UserTenantLinking> userTenantLinkingList = (await _userTenantLinkingDS.FindAllAsync(ut => ut.TenantId == userTenantLinking.TenantId && ut.BusinessPartnerTenantId == userTenantLinking.BusinessPartnerTenantId && ut.UserType == (int)UserTypeEnum.Business && ut.Deleted == false)).ToList();
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
