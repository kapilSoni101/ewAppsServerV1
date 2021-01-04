/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.ExceptionService;
using ewApps.Core.UniqueIdentityGeneratorService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// This class implements standard business logic and operations for tenant user and user managment logics.
    /// </summary>
    public class TenantUserDS:BaseDS<TenantUser>, ITenantUserDS {

        #region Local Member 

        ITenantUserRepository _tenantUserRepository;
        IUniqueIdentityGeneratorDS _uniqueIdentityGeneratorDS;
        IEntityThumbnailDS _entityThumbnailDS;
        IUnitOfWork _unitOfWork;

        #endregion Local Member

        #region Constructor


        public TenantUserDS(ITenantUserRepository tenantUserRepository, IUniqueIdentityGeneratorDS uniqueIdentityGeneratorDS, IEntityThumbnailDS entityThumbnailDS, IUnitOfWork unitOfWork) : base(tenantUserRepository) {
            _tenantUserRepository = tenantUserRepository;
            _uniqueIdentityGeneratorDS = uniqueIdentityGeneratorDS;
            _entityThumbnailDS = entityThumbnailDS;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        #region Public Method

        #region Get

        /// <summary>
        /// Get primary user of a application by tenantid and appid.
        /// </summary>
        /// <param name="tenantId">Id of Business Tenant</param>
        /// <param name="appId"></param>
        /// <param name="uType"></param>
        /// <param name="token"></param>
        /// <returns>return usershort info.</returns>
        public async Task<UserShortInfoDQ> GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(Guid tenantId, Guid appId, UserTypeEnum uType, CancellationToken token = default(CancellationToken)) {
            return await _tenantUserRepository.GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(tenantId, appId, uType, token);
        }

        public Task<Tuple<string, bool>> UserAlreadyJoinedAnyApplication(Guid tenantUserId) {
            return _tenantUserRepository.UserAlreadyJoinedAnyApplication(tenantUserId);
        }

        #endregion Get

        ///<inheritdoc/>
        public async Task<TenantUser> AddTenantUserAsync(TenantUser tenantUser) {
            int identityNo = _uniqueIdentityGeneratorDS.GetIdentityNo(Guid.Empty, 1, Common.Constants.UserIdPrefix, Common.Constants.UserIdstartnumber);
            tenantUser.IdentityNumber = ewApps.AppMgmt.Common.Constants.UserIdPrefix + identityNo;
            // UpdateSystemFieldsByOpType(tenantUser, OperationType.Add);
            _tenantUserRepository.Add(tenantUser);
            return tenantUser;
        }

        ///<inheritdoc/>
        public async Task<TenantUser> GetUserbyEmailAndTenantIdAsync(string email, Guid tenantId) {
            //return await _tenantUserRepository.GetTenantUserbyEmailAndTenantIdAsync(email, tenantId);
            return null;
        }

        public async Task<TenantUserDTO> GetTenantUserByEmailAsync(string userEmail, CancellationToken cancellationToken = default(CancellationToken)) {
            TenantUser tenantUser = (await _tenantUserRepository.FindAllAsync(i => i.Email.ToLower() == userEmail.ToLower() && i.Deleted == false, cancellationToken)).FirstOrDefault();
            if(tenantUser != null) {
                return TenantUserDTO.MapFromTenantUser(tenantUser);
            }
            else {
                return null;
            }
        }

        public async Task DeleteUserDependencyAsync(Guid tenantUserId, Guid tenantId, Guid appId, Guid? businessAppId) {
            await _tenantUserRepository.DeleteUserDependencyAsync(tenantUserId, tenantId, appId, businessAppId);
        }


        #endregion Public Method

        #region Validation 

        public void ValidateTenantUser(TenantUser entity, OperationType operationType) {

            TenantUser tenantUser = Find(tu => tu.Email == entity.Email && tu.Deleted == false);
            if(operationType == OperationType.Add) {
                if(tenantUser != null) {
                    throw new EwpValidationException("TenantUser already exists.");
                }
            }
            else if(operationType == OperationType.Update) {
                if(tenantUser == null) {
                    throw new EwpValidationException("TenantUser not exists.");
                }
            }

            else if(operationType == OperationType.Delete) {
                if(tenantUser == null) {
                    throw new EwpValidationException("TenantUser not exists.");
                }
                else {
                    if(tenantUser.Deleted) {
                        throw new EwpValidationException("TenantUser not exists.");
                    }
                }
            }

            if(operationType != OperationType.Delete) {
                IList<EwpErrorData> listErrData;
                // validating tenantUser entity.
                if(entity.Validate(out listErrData)) {
                    EwpError error = new EwpError();
                    error.ErrorType = ErrorType.Validation;
                    error.EwpErrorDataList = listErrData;
                    EwpValidationException exc = new EwpValidationException("TenantUser validation error.", error.EwpErrorDataList);
                    throw exc;
                }
            }
        }

        #endregion Validation 

    }
}
