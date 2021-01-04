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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Logging;

namespace ewApps.AppMgmt.DS {
    public class TenantUpdateForCustomerDS:ITenantUpdateForCustomerDS {

        IUserSessionManager _userSessionManager;
        ITenantUserDS _tenantUserDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        IIdentityServerDS _identityServerDS;
        IUnitOfWork _unitOfWork;
        ILogger<TenantUpdateForCustomerDS> _loggerService;

        public TenantUpdateForCustomerDS(IUserSessionManager userSessionManager, ITenantUserDS tenantUserDS,
        IUserTenantLinkingDS userTenantLinkingDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS,
        IIdentityServerDS identityServerDS, IUnitOfWork unitOfWork, ILogger<TenantUpdateForCustomerDS> loggerService) {
            _userSessionManager = userSessionManager;
            _tenantUserDS = tenantUserDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _identityServerDS = identityServerDS;
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }

        public async Task<TenantUserSignUpResponseDTO> SignUpCustomerUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken token = default(CancellationToken)) {
            IdentityServerAddUserResponseDTO identityServerAddUserResponseDTO = null;
            TenantUser tenantUser = null;
            UserSession userSession = _userSessionManager.GetSession();
            int userType = (int)UserTypeEnum.Customer;

            try {
                #region TenantUser

                // Add TenantUser entity.

                // Check if user with same email id is already exist.
                tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == tenantUserSignUpDTO.Email && tu.Deleted == false);
                if(tenantUser == null) {
                    tenantUser = new TenantUser();
                    if(tenantUserSignUpDTO.ID == Guid.Empty) {
                        tenantUser.ID = Guid.NewGuid();
                    }
                    else {
                        tenantUser.ID = tenantUserSignUpDTO.ID;
                    }

                    tenantUser.Email = tenantUserSignUpDTO.Email;
                    tenantUser.FirstName = tenantUserSignUpDTO.FirstName;
                    tenantUser.LastName = tenantUserSignUpDTO.LastName;
                    tenantUser.FullName = tenantUserSignUpDTO.FirstName + " " + tenantUserSignUpDTO.LastName;
                    tenantUser.CreatedBy = userSession.TenantUserId;
                    tenantUser.CreatedOn = DateTime.UtcNow;
                    tenantUser.UpdatedBy = tenantUser.CreatedBy;
                    tenantUser.UpdatedOn = tenantUser.CreatedOn;
                    tenantUser = await _tenantUserDS.AddTenantUserAsync(tenantUser);
                }

                #endregion TenantUser

                #region TenantUserLinking

                UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(utl => utl.TenantUserId == tenantUser.ID && utl.TenantId == tenantUserSignUpDTO.TenantId && utl.Deleted == false);
                if(userTenantLinking == null) {
                    userTenantLinking = new UserTenantLinking();
                    userTenantLinking.ID = Guid.NewGuid();
                    userTenantLinking.TenantId = tenantUserSignUpDTO.TenantId;
                    userTenantLinking.TenantUserId = tenantUser.ID;
                    userTenantLinking.UserType = userType;
                    if(await _userTenantLinkingDS.CheckPrimaryUserExistsForCustomer((Guid)tenantUserSignUpDTO.BusinessPartnerTenantId)) {
                        userTenantLinking.IsPrimary = false;
                    }
                    else {
                        userTenantLinking.IsPrimary = true;
                    }
                    userTenantLinking.Deleted = false;
                    userTenantLinking.PartnerType = 1;
                    userTenantLinking.BusinessPartnerTenantId = tenantUserSignUpDTO.BusinessPartnerTenantId;
                    userTenantLinking.CreatedBy = tenantUser.CreatedBy;
                    userTenantLinking.CreatedOn = DateTime.UtcNow;
                    userTenantLinking.UpdatedBy = userTenantLinking.CreatedBy;
                    userTenantLinking.UpdatedOn = userTenantLinking.CreatedOn;
                    await _userTenantLinkingDS.AddAsync(userTenantLinking);
                }
                else {
                    // Duplicate user validation 
                    ThrowDuplicateEmailError(tenantUserSignUpDTO.Email);
                }

                #endregion TenantUserLinking

                #region Asign application (TenantUserAppLinking)

                foreach(UserAppRelationDTO item in tenantUserSignUpDTO.UserAppRelationDTOList) {
                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(tu => tu.TenantUserId == tenantUser.ID && tu.AppId == item.AppId && tu.TenantId == tenantUserSignUpDTO.TenantId && tu.Deleted == false);
                    if(tenantUserAppLinking == null) {
                        tenantUserAppLinking = new TenantUserAppLinking();
                        tenantUserAppLinking.ID = Guid.NewGuid();
                        tenantUserAppLinking.Deleted = false;
                        tenantUserAppLinking.TenantId = userTenantLinking.TenantId;
                        tenantUserAppLinking.AppId = item.AppId;
                        tenantUserAppLinking.TenantUserId = tenantUser.ID;
                        tenantUserAppLinking.UserType = userType;
                        tenantUserAppLinking.Active = true;
                        tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                        tenantUserAppLinking.CreatedBy = tenantUser.CreatedBy;
                        tenantUserAppLinking.CreatedOn = DateTime.UtcNow;
                        tenantUserAppLinking.UpdatedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.UpdatedOn = tenantUserAppLinking.CreatedOn;
                        tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                        tenantUserAppLinking.BusinessPartnerTenantId = tenantUserSignUpDTO.BusinessPartnerTenantId;
                        await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking);
                    }
                }

                #endregion Asign application (TenantUserAppLinking)

                #region Add User In Identity Server

                // Add user on identity server.
                string appKeys = string.Join(',', tenantUserSignUpDTO.UserAppRelationDTOList.Select(u => u.AppKey));

                // Create identityUserDTO for adding user on identity server.
                IdentityUserDTO identityUserDTO = new IdentityUserDTO {
                    FirstName = tenantUser.FirstName,
                    LastName = tenantUser.LastName,
                    ClientAppType = appKeys,
                    Email = tenantUser.Email,
                    IsActive = true,
                    TenantId = userTenantLinking.TenantId,
                    UserType = userType
                };

                identityServerAddUserResponseDTO = await _identityServerDS.AddUserOnIdentityServerAsync(identityUserDTO);

                // Assign identityUseID.
                if(identityServerAddUserResponseDTO.Code != null) {
                    tenantUser.IdentityUserId = identityServerAddUserResponseDTO.UserId;
                    tenantUser.Code = identityServerAddUserResponseDTO.Code;
                    await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);
                }

                #endregion

                _unitOfWork.SaveAll();

                TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = new TenantUserSignUpResponseDTO();
                tenantUserSignUpResponseDTO.TenantUserDTO = TenantUserDTO.MapFromTenantUser(tenantUser);
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = new List<UserAppRelationDTO>();
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = tenantUserSignUpDTO.UserAppRelationDTOList;
                tenantUserSignUpResponseDTO.NewUser = await _tenantUserDS.UserAlreadyJoinedAnyApplication(tenantUser.ID);

                return tenantUserSignUpResponseDTO;
            }
            catch(Exception ex) {
                // Log error
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in TenantUpdateForCustomer.SignUpCustomerUserAsync:-");
                exceptionDetail.AppendLine();
                exceptionDetail.AppendFormat("{0}\r\n{1}", ex.Message, ex.StackTrace);
                _loggerService.Log(LogLevel.Error, ex, exceptionDetail.ToString());

                // To delete the user created on identity server incase of any exception after creating the user.
                if(identityServerAddUserResponseDTO != null) {
                    await _identityServerDS.DeleteUserByTenantIdOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserSignUpDTO.TenantId);
                }
                throw;
            }
        }

        public async Task<TenantUserSignUpResponseDTO> UpdateCustomerTenantUser(TenantUserSignUpDTO tenantUserSignUpDTO) {

            // For customer application.
            int userType = (int)UserTypeEnum.Customer;
            UserSession userSession = _userSessionManager.GetSession();

            // Check if user already exists in the tenant.
            TenantUser tenantUser = await _tenantUserDS.FindAsync(tu => tu.ID == tenantUserSignUpDTO.ID && tu.Deleted == false);
            if(tenantUser != null) {

                // Check if user exists.
                UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.Deleted == false);
                if(userTenantLinking != null) {

                    // Update primary flag and user name 
                    tenantUser.FirstName = tenantUserSignUpDTO.FirstName;
                    tenantUser.LastName = tenantUserSignUpDTO.LastName;
                    tenantUser.FullName = tenantUserSignUpDTO.FullName;
                    await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);

                    if(userTenantLinking.IsPrimary != tenantUserSignUpDTO.IsPrimary) {
                        userTenantLinking.IsPrimary = tenantUserSignUpDTO.IsPrimary;
                        await _userTenantLinkingDS.UpdateAsync(userTenantLinking, userTenantLinking.ID);
                    }

                    // Get the list of application to be added and delted.
                    List<UserAppRelationDTO> addAppList = tenantUserSignUpDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Add).ToList();
                    List<UserAppRelationDTO> deleteAppList = tenantUserSignUpDTO.UserAppRelationDTOList.Where(a => a.OperationType == OperationType.Delete).ToList();

                    // Assign application to the user
                    foreach(UserAppRelationDTO addApp in addAppList) {
                        TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.AppId == addApp.AppId && ut.Deleted == false);
                        if(tenantUserAppLinking == null) {
                            // Asign application on serve
                            tenantUserAppLinking = new TenantUserAppLinking();
                            tenantUserAppLinking.ID = Guid.NewGuid();
                            tenantUserAppLinking.Deleted = false;
                            tenantUserAppLinking.TenantId = userTenantLinking.TenantId;
                            tenantUserAppLinking.AppId = addApp.AppId;
                            tenantUserAppLinking.TenantUserId = tenantUser.ID;
                            tenantUserAppLinking.UserType = userType;
                            tenantUserAppLinking.Active = true;
                            tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                            tenantUserAppLinking.CreatedBy = userSession.TenantUserId;
                            tenantUserAppLinking.CreatedOn = DateTime.UtcNow;
                            tenantUserAppLinking.UpdatedBy = tenantUserAppLinking.CreatedBy;
                            tenantUserAppLinking.UpdatedOn = tenantUserAppLinking.CreatedOn;
                            tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                            tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                            tenantUserAppLinking.BusinessPartnerTenantId = tenantUserSignUpDTO.BusinessPartnerTenantId;
                            await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking);

                            // asign application on identity server
                            await _identityServerDS.AssignApplicationOnIdentityServerAsync(tenantUser.IdentityUserId, tenantUserAppLinking.TenantId, addApp.AppKey);
                        }
                        else {
                            // throw exception application already asigned

                        }
                    }

                    // DeAsign application to the user
                    foreach(UserAppRelationDTO deleteApp in deleteAppList) {
                        TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.AppId == deleteApp.AppId && ut.Deleted == false);
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
                }
                else {
                    // throw user does not exists exception.
                }

                _unitOfWork.SaveAll();

                TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = new TenantUserSignUpResponseDTO();
                tenantUserSignUpResponseDTO.TenantUserDTO = TenantUserDTO.MapFromTenantUser(tenantUser);
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = new List<UserAppRelationDTO>();
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = tenantUserSignUpDTO.UserAppRelationDTOList;
                tenantUserSignUpResponseDTO.NewUser = await _tenantUserDS.UserAlreadyJoinedAnyApplication(tenantUser.ID);

                return tenantUserSignUpResponseDTO;
            }
            else {
                // User does not exists exception.
                return null;
            }
        }
            
        private void ThrowDuplicateEmailError(string email) {
            EwpError error = new EwpError();
            error.ErrorType = ErrorType.Duplicate;
            EwpErrorData errorData = new EwpErrorData();
            errorData.ErrorSubType = (int)DuplicateErrorSubType.None;
            errorData.Message = "User with same email already exists ";
            error.EwpErrorDataList.Add(errorData);
            EwpDuplicateNameException exc = new EwpDuplicateNameException("User with email(" + email + ") already exist for this tenant. ", error.EwpErrorDataList);
            throw exc;
        }

    }
}