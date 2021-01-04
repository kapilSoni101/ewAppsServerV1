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
    public class TenantUserUpdateForCustomerDS:ITenantUserUpdateForCustomerDS {

        ITenantUserDS _tenantUserDS;
        IUnitOfWork _unitOfWork;
        IUserSessionManager _userSessionManager;
        IAppDS _appDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IIdentityServerDS _identityServerDS;

        public TenantUserUpdateForCustomerDS(IUserSessionManager userSessionManager, ITenantUserDS tenantUserDS, IIdentityServerDS identityServerDS,
            IAppDS appDS, IUnitOfWork unitOfWork, IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS) {
            _tenantUserDS = tenantUserDS;
            _unitOfWork = unitOfWork;
            _userSessionManager = userSessionManager;
            _appDS = appDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _identityServerDS = identityServerDS;
        }

        ///<inheritdoc/>
        public async Task<UpdateTenantUserResponseDTO> UpdateUserAsync(TenantUserUpdateRequestDTO tenantUserUpdateRequestDTO) {

            // Get user initialize response DTO and get session.
            int userType = (int)UserTypeEnum.Customer;
            UserSession userSession = _userSessionManager.GetSession();
            TenantUser tenantUser = await _tenantUserDS.GetAsync(tenantUserUpdateRequestDTO.TenantUserId);
            UpdateTenantUserResponseDTO updateTenantUserResponseDTO = new UpdateTenantUserResponseDTO();
            if(tenantUser != null) {

                // Validation
                _tenantUserDS.ValidateTenantUser(tenantUser, OperationType.Update);

                // Update TenantUser related data.
                tenantUser.FirstName = tenantUserUpdateRequestDTO.FirstName;
                tenantUser.LastName = tenantUserUpdateRequestDTO.LastName;
                tenantUser.FullName = tenantUserUpdateRequestDTO.FullName;
                tenantUser.Phone = tenantUserUpdateRequestDTO.Phone;
                _tenantUserDS.UpdateSystemFieldsByOpType(tenantUser, OperationType.Update);
                await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);

                // Put data in response DTO
                updateTenantUserResponseDTO.TenantUserId = tenantUser.ID;
                updateTenantUserResponseDTO.TenantId = tenantUserUpdateRequestDTO.TenantId;

                // Get User tenant linking.
                UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserUpdateRequestDTO.TenantId);

                #region Applciation Update

                // Get the list of application to be added update and delete.
                List<UserAppRelationDTO> addAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Add).ToList();
                List<UserAppRelationDTO> deleteAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Delete).ToList();
                List<UserAppRelationDTO> updateAppList = tenantUserUpdateRequestDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Update).ToList();

                #region Add

                // Assign application to the user
                foreach(UserAppRelationDTO addApp in addAppList) {
                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserUpdateRequestDTO.TenantId && ut.AppId == addApp.AppId && ut.Deleted == false);
                    if(tenantUserAppLinking == null) {
                        // Asign application on serve
                        tenantUserAppLinking = new TenantUserAppLinking();
                        tenantUserAppLinking.ID = Guid.NewGuid();
                        tenantUserAppLinking.Deleted = false;
                        tenantUserAppLinking.TenantId = tenantUserUpdateRequestDTO.TenantId;
                        tenantUserAppLinking.AppId = addApp.AppId;
                        tenantUserAppLinking.TenantUserId = tenantUser.ID;
                        tenantUserAppLinking.UserType = userType;
                        tenantUserAppLinking.Active = true;
                        tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                        tenantUserAppLinking.BusinessPartnerTenantId = userTenantLinking.BusinessPartnerTenantId;
                        tenantUserAppLinking.CreatedBy = userSession.TenantUserId;
                        tenantUserAppLinking.CreatedOn = DateTime.UtcNow;
                        tenantUserAppLinking.UpdatedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.UpdatedOn = tenantUserAppLinking.CreatedOn;
                        tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                        await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking);

                        // asign application on identity server
                        await _identityServerDS.AssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserAppLinking.TenantId, addApp.AppKey);
                    }
                    else {
                        // throw exception application already asigned
                    }
                }

                #endregion Add

                #region Delete

                // DeAsign application to the user
                foreach(UserAppRelationDTO deleteApp in deleteAppList) {
                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserUpdateRequestDTO.TenantId && ut.BusinessPartnerTenantId == userTenantLinking.BusinessPartnerTenantId && ut.AppId == deleteApp.AppId && ut.Deleted == false);
                    if(tenantUserAppLinking == null) {
                        // throw exception application not assigned
                    }
                    else {
                        // deasign application on server
                        tenantUserAppLinking.Deleted = true;
                        await _tenantUserAppLinkingDS.UpdateAsync(tenantUserAppLinking, tenantUserAppLinking.ID);

                        // deasign appliation on identity server.
                        await _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserAppLinking.TenantId, deleteApp.AppKey);
                    }
                }

                #endregion Delete

                #region Update

                // Update application status active inactive for updated apps.
                foreach(UserAppRelationDTO updateApp in updateAppList) {

                    // Get user applinking.
                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(tu => tu.AppId == updateApp.AppId && tu.TenantId == tenantUserUpdateRequestDTO.TenantId && tu.BusinessPartnerTenantId == userTenantLinking.BusinessPartnerTenantId && tu.TenantUserId == tenantUser.ID && tu.Deleted == false);

                    if(tenantUserAppLinking != null) {

                        // User Active/InActive logic.
                        if(tenantUserAppLinking.Active != updateApp.Active) {

                            updateTenantUserResponseDTO.StatusChanged = true;

                            App app = await _appDS.GetAsync(updateApp.AppId);
                            if(updateApp.Active) {
                //Task markUserActive =
                await _identityServerDS.MarkActiveInActiveOnIdentityServerAsync(updateApp.Active, tenantUser.IdentityUserId, tenantUserUpdateRequestDTO.TenantId);
                                await _identityServerDS.AssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserUpdateRequestDTO.TenantId, app.AppKey);
                            }
                            else {
                                List<TenantUserAppLinking> tenantUserAppLinkings = (await _tenantUserAppLinkingDS.FindAllAsync(tu => tu.TenantId == tenantUserUpdateRequestDTO.TenantId && tu.TenantUserId == tenantUser.ID && tu.Active == true && tu.Deleted == false)).ToList();
                                if(tenantUserAppLinkings != null && tenantUserAppLinkings.Count > 0) {
                                    if(tenantUserAppLinkings.Count == 1) {
                                        await _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserUpdateRequestDTO.TenantId, app.AppKey);
                                        await _identityServerDS.MarkActiveInActiveOnIdentityServerAsync(updateApp.Active, tenantUser.IdentityUserId, tenantUserUpdateRequestDTO.TenantId);
                                    }
                                    else {
                                        await _identityServerDS.DeAssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserUpdateRequestDTO.TenantId, app.AppKey);
                                    }
                                }
                                await _userSessionManager.DeletedByAppUserAndAppId(tenantUser.ID, updateApp.AppId);
                            }
                            tenantUserAppLinking.Active = updateApp.Active;
                            _tenantUserAppLinkingDS.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Update);
                            await _tenantUserAppLinkingDS.UpdateAsync(tenantUserAppLinking, tenantUserAppLinking.ID);
                        }
                    }
                }

                #endregion Update

                #endregion Applciation Update
            }
            else {
                // Throw user does not exists validation.
            }
            _unitOfWork.SaveAll();
            return updateTenantUserResponseDTO;
        }
    }
}
