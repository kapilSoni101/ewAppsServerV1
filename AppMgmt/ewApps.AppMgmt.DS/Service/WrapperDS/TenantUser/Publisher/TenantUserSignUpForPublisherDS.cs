using System;
using System.Collections.Generic;
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
  public class TenantUserSignUpForPublisherDS :ITenantUserSignUpForPublisherDS {

        #region Local Member

        ITenantUserDS _tenantUserDS;
        IUserTenantLinkingDS _userTenantLinkingDS;
        IUserSessionManager _userSessionManager;
        IAppDS _appDS;
        IIdentityServerDS _identityServerDS;
        IUnitOfWork _unitOfWork;
        ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
        ILogger<TenantUserSignUpForPublisherDS> _loggerService;

        #endregion Local Member

        /// <summary>
        /// Dependency injection for all other dataservices for user operations.
        /// </summary>
        public TenantUserSignUpForPublisherDS(ITenantUserDS tenantUserDS, IUserTenantLinkingDS userTenantLinkingDS, IIdentityServerDS identityServerDS, IUnitOfWork unitOfWork,
            IUserSessionManager userSessionManager, IAppDS appDS, ITenantUserAppLinkingDS tenantUserAppLinkingDS, ILogger<TenantUserSignUpForPublisherDS> loggerService) {
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
            int userType = (int)UserTypeEnum.Publisher;


            if(tenantUserSignUpDTO.TenantId == Guid.Empty) {
                tenantUserSignUpDTO.TenantId = userSession.TenantId;
            }

            try {
                App pubApp = await _appDS.GetAppByAppKeyAsync(AppKeyEnum.pub.ToString(), cancellationToken);

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
                    userTenantLinking.PartnerType = null;
                    userTenantLinking.BusinessPartnerTenantId = null;
                    _userTenantLinkingDS.UpdateSystemFieldsByOpType(userTenantLinking, OperationType.Add);
                    userTenantLinking.TenantId = tenantUserSignUpDTO.TenantId;
                    await _userTenantLinkingDS.AddAsync(userTenantLinking, cancellationToken);

                    #region Add User In Identity Server

                    // Add user on identity server.
                    string appKeys = AppKeyEnum.pub.ToString();

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

                    #endregion

                    #region Assign application (TenantUserAppLinking)

                    TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(ut => ut.TenantUserId == tenantUser.ID && ut.AppId == pubApp.ID && ut.TenantId == tenantUserSignUpDTO.TenantId && ut.Deleted == false);
                    if(tenantUserAppLinking == null) {
                        tenantUserAppLinking = new TenantUserAppLinking();
                        tenantUserAppLinking.Deleted = false;
                        tenantUserAppLinking.AppId = pubApp.ID;
                        tenantUserAppLinking.TenantUserId = tenantUser.ID;
                        tenantUserAppLinking.UserType = userType;
                        tenantUserAppLinking.Active = true;
                        tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Invited;

                        _tenantUserAppLinkingDS.UpdateSystemFieldsByOpType(tenantUserAppLinking, OperationType.Add);
                        tenantUserAppLinking.TenantId = tenantUserSignUpDTO.TenantId;
                        tenantUserAppLinking.InvitedBy = tenantUserAppLinking.CreatedBy;
                        tenantUserAppLinking.InvitedOn = tenantUserAppLinking.CreatedOn;
                        tenantUserAppLinking.BusinessPartnerTenantId = null;

                        await _tenantUserAppLinkingDS.AddAsync(tenantUserAppLinking, cancellationToken);
                    }

                    #endregion Asign application (TenantUserAppLinking)

                }
                else {
                    // Duplicate user validation 
                    ThrowDuplicateEmailError(tenantUserSignUpDTO.Email, pubApp.Name);
                }

                #endregion TenantUserLinking and assign application

                // Commit all data base changes.
                _unitOfWork.SaveAll();

                UserAppRelationDTO userAppRelationDTO = new UserAppRelationDTO();
                userAppRelationDTO.AppId = pubApp.ID;
                userAppRelationDTO.AppKey = pubApp.AppKey;

                TenantUserSignUpResponseDTO tenantUserSignUpResponseDTO = new TenantUserSignUpResponseDTO();
                tenantUserSignUpResponseDTO.TenantUserDTO = TenantUserDTO.MapFromTenantUser(tenantUser);
                tenantUserSignUpResponseDTO.UserAppRelationDTOs = new List<UserAppRelationDTO>();
                tenantUserSignUpResponseDTO.UserAppRelationDTOs.Add(userAppRelationDTO);
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
