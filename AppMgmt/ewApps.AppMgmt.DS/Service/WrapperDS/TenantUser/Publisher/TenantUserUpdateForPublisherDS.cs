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

namespace ewApps.AppMgmt.DS {
  public  class TenantUserUpdateForPublisherDS:ITenantUserUpdateForPublisherDS {


        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;
        IAppDS _appDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDataService;
        IIdentityServerDS _identityServerDS;

        public TenantUserUpdateForPublisherDS(IUserSessionManager userSessionManager, ITenantUserDS tenantUserDS, IIdentityServerDS identityServerDS,
            IAppDS appDS, IUnitOfWork unitOfWork, IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDataService) {
            _tenantUserDS = tenantUserDS;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
            _appDS = appDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDataService = tenantUserAppLinkingDataService;
            _identityServerDS = identityServerDS;
        }

        ///<inheritdoc/>
        public async Task<UpdateTenantUserResponseDTO> UpdateUserAsync(TenantUserDTO tenantUserDTO, Guid tenantId, Guid appId) {

            // Get user.
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserDTO.TenantUserId);
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = new UpdateTenantUserResponseDTO();
            if(tenantUser != null) {

                // Validation
                _tenantUserDS.ValidateTenantUser(tenantUser, OperationType.Update);

                // Put data in response DTO
                updateTenantUserResponseDTO.TenantUserId = tenantUser.ID;
                updateTenantUserResponseDTO.TenantId = tenantId;

                // Get user applinking.
                TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDataService.FindAsync(tu => tu.AppId == appId && tu.TenantId == tenantId && tu.TenantUserId == tenantUser.ID && tu.Deleted == false);

                if(tenantUserAppLinking != null) {

                    // User Active/InActive logic.
                    if(tenantUserAppLinking.Active != tenantUserDTO.Active) {

                        updateTenantUserResponseDTO.StatusChanged = true;

                        App app = await _appDS.GetAsync(appId);
                        if(tenantUserDTO.Active) {
                            await _identityServerDS.MarkActiveInActiveOnIdentityServerAsync(tenantUserDTO.Active, tenantUser.IdentityUserId, tenantId);
                            await _identityServerDS.AssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, app.AppKey);
                        }
                        else {
                            List<TenantUserAppLinking> tenantUserAppLinkings = (await _tenantUserAppLinkingDataService.FindAllAsync(tu => tu.TenantId == tenantId && tu.TenantUserId == tenantUser.ID && tu.Active == true && tu.Deleted == false)).ToList();
                            if(tenantUserAppLinkings != null && tenantUserAppLinkings.Count > 0) {
                                if(tenantUserAppLinkings.Count == 1) {
                                    await _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, app.AppKey);
                                    await _identityServerDS.MarkActiveInActiveOnIdentityServerAsync(tenantUserDTO.Active, tenantUser.IdentityUserId, tenantId);
                                }
                                else {
                                    await _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantId, app.AppKey);
                                }
                            }
                            await _userSessionManager.DeletedByAppUserAndAppId(tenantUser.ID, appId);
                        }
                        tenantUserAppLinking.Active = tenantUserDTO.Active;
                        _tenantUserAppLinkingDataService.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Update);
                        await _tenantUserAppLinkingDataService.UpdateAsync(tenantUserAppLinking, tenantUserAppLinking.ID);
                    }

                    // Update TenantUser.
                    tenantUser.FirstName = tenantUserDTO.FirstName;
                    tenantUser.LastName = tenantUserDTO.LastName;
                    tenantUser.FullName = tenantUserDTO.FullName;
                    tenantUser.Phone = tenantUserDTO.Phone;
                    _tenantUserDS.UpdateSystemFieldsByOpType(tenantUser, OperationType.Update);
                    await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);

                    _unitOfWork.SaveAll();
                }
            }
            return updateTenantUserResponseDTO;
        }

    }
}
