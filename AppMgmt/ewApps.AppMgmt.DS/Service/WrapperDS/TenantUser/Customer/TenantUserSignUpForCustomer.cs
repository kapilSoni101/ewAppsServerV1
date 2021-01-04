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
   public class TenantUserSignUpForCustomer :ITenantUserSignUpForCustomer {

        #region Local Member

        ITenantUserDS _tenantUserDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        IUserSessionManager _userSessionManager;
        IAppDS _appDS;
        IIdentityServerDS _identityServerDS;
        IUnitOfWork _unitOfWork;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        ILogger<TenantUserSignUpForBusiness> _loggerService;

        #endregion Local Member

        /// <summary>
        /// Dependency injection for all other dataservices for user operations.
        /// </summary>
        public TenantUserSignUpForCustomer(ITenantUserDS tenantUserDS, IUserTenantLinkingDS userTenantLinkingDS, IIdentityServerDS identityServerDS, IUnitOfWork unitOfWork,
            IUserSessionManager userSessionManager, IAppDS appDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS, ILogger<TenantUserSignUpForBusiness> loggerService) {
            _tenantUserDS = tenantUserDS;
            _userTenantLinkingDS = userTenantLinkingDS;
            _userSessionManager = userSessionManager;
            _appDS = appDS;
            _identityServerDS = identityServerDS;
            _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
            _loggerService = loggerService;
            _unitOfWork = unitOfWork;
        }

        ///<inhertdoc/>
        public async Task<TenantUserSignUpResponseDTO> SignUpUserAsync(TenantUserSignUpDTO tenantUserSignUpDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            // Add user on identity server DTO initializations.
            IdentityServerAddUserResponseDTO identityServerAddUserResponseDTO = null;
            TenantUser tenantUser = null;
            UserSession userSession = _userSessionManager.GetSession();
            int userType = (int)UserTypeEnum.Customer;

            if(tenantUserSignUpDTO.TenantId == Guid.Empty) {
                tenantUserSignUpDTO.TenantId = userSession.TenantId;
            }

            try {

                #region TenantUser

                // Add TenantUser entity.
                // Check if user with same email id is already exist.
                tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == tenantUserSignUpDTO.Email && tu.Deleted == false);
                if(tenantUser == null) {
                    tenantUser = new TenantUser();
                    tenantUser.Email = tenantUserSignUpDTO.Email;
                    tenantUser.FirstName = tenantUserSignUpDTO.FirstName;
                    tenantUser.LastName = tenantUserSignUpDTO.LastName;
                    tenantUser.FullName = tenantUserSignUpDTO.FullName;
                    tenantUser.Phone = tenantUserSignUpDTO.Phone;
                    if(tenantUserSignUpDTO.ID == Guid.NewGuid()) {
                        tenantUser.ID = Guid.NewGuid();
                    }
                    else {
                        tenantUser.ID = tenantUserSignUpDTO.ID;
                    }
                    _tenantUserDS.UpdateSystemFields(tenantUser, SystemFieldMask.CreatedOn | SystemFieldMask.CreatedBy | SystemFieldMask.UpdatedOn | SystemFieldMask.UpdatedBy);
                    tenantUser = await _tenantUserDS.AddTenantUserAsync(tenantUser);
                }

                #endregion TenantUser

                #region TenantUserLinking and assign application

                UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.Deleted == false);
                if(userTenantLinking == null) {
                    userTenantLinking = new UserTenantLinking();
                    userTenantLinking.TenantUserId = tenantUser.ID;
                    userTenantLinking.UserType = userType;
                    userTenantLinking.Deleted = false;
                    userTenantLinking.PartnerType = 1;
                    userTenantLinking.BusinessPartnerTenantId = tenantUserSignUpDTO.BusinessPartnerTenantId;
                    _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Add);
                    userTenantLinking.TenantId = tenantUserSignUpDTO.TenantId;
                    await _userTenantLinkingDS.AddAsync(userTenantLinking, cancellationToken);

                    #region Add User In Identity Server

                    // Add user on identity server.
                    string appKeys = string.Join(',', tenantUserSignUpDTO.UserAppRelationDTOList.Select(ur => ur.AppKey));

                    // Create identityUserDTO for adding user on identity server.
                    IdentityUserDTO identityUserDTO = new IdentityUserDTO {
                        FirstName = tenantUser.FirstName,
                        LastName = tenantUser.LastName,
                        ClientAppType = appKeys,
                        Email = tenantUser.Email,
                        IsActive = true,
                        TenantId = tenantUserSignUpDTO.TenantId,
                        UserType = userType
                    };

                    identityServerAddUserResponseDTO = await _identityServerDS.AddUserOnIdentityServerAsync(identityUserDTO);

                    // Update TenantUser entity with identity server user information.
                    if(identityServerAddUserResponseDTO.Code != null) {
                        tenantUser.IdentityUserId = identityServerAddUserResponseDTO.UserId;
                        tenantUser.Code = identityServerAddUserResponseDTO.Code;
                        await _tenantUserDS.UpdateAsync(tenantUser, tenantUser.ID);
                    }
                }
                #endregion

                #region Assign application (TenantUserAppLinking)

                foreach(UserAppRelationDTO item in tenantUserSignUpDTO.UserAppRelationDTOList) {

                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.AppId == item.AppId && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.Deleted == false);
                    if(tenantUserAppLinking == null) {
                        tenantUserAppLinking = new TenantUserAppLinking();
                        tenantUserAppLinking.Deleted = false;
                        tenantUserAppLinking.AppId = item.AppId;
                        tenantUserAppLinking.TenantUserId = tenantUser.ID;
                        tenantUserAppLinking.UserType = userType;
                        tenantUserAppLinking.Active = true;
                        tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;
                        tenantUserAppLinking.BusinessPartnerTenantId = tenantUserAppLinking.BusinessPartnerTenantId;
                        _tenantUserAppLinkingDS.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Add);
                        tenantUserAppLinking.TenantId = tenantUserSignUpDTO.TenantId;
                        tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                        tenantUserAppLinking.BusinessPartnerTenantId = tenantUserSignUpDTO.BusinessPartnerTenantId;
                       
                        await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking, cancellationToken);
                    }
                    else {
                        // Duplicate user validation 
                        App app = await _appDS.GetAsync(item.AppId);
                        ThrowDuplicateEmailError(tenantUserSignUpDTO.Email, app.Name);
                    }
                }
                #endregion Asign application (TenantUserAppLinking)

                #endregion TenantUserLinking and assign application

                // Commit all data base changes.
                _unitOfWork.SaveAll();

                TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = new TenantUserSignUpResponseDTO();
                tenantUserSignUpResponseDTO.TenantUserDTO = TenantUserDTO.MapFromTenantUser(tenantUser);
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = new List<UserAppRelationDTO>();
                tenantUserSignUpResponseDTO.UserAppRelationDTOs.AddRange(tenantUserSignUpDTO.UserAppRelationDTOList);
                tenantUserSignUpResponseDTO.NewUser = await _tenantUserDS.UserAlreadyJoinedAnyApplication(tenantUser.ID);

                return tenantUserSignUpResponseDTO;

            }
            catch(Exception ex) {
                // Log error
                StringBuilder exceptionDetail = new StringBuilder();
                exceptionDetail.Append("Exception occurred in TenantSignUpDs.PublisherSignUpPrimaryUserAsync:-");
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

        private void ThrowDuplicateEmailError(string email, string appName) {
            EwpError error = new EwpError();
            error.ErrorType = ErrorType.Duplicate;
            EwpErrorData errorData = new EwpErrorData();
            errorData.ErrorSubType = (int)DuplicateErrorSubType.None;
            errorData.Message = "User with same email already exists ";
            error.EwpErrorDataList.Add(errorData);
            EwpDuplicateNameException exc = new EwpDuplicateNameException("User with email(" + email + ") already exist for the application: " + appName, error.EwpErrorDataList);
            throw exc;
        }
    }
}
