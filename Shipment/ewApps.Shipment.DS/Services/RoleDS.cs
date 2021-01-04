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
using System.Threading;
using System.Threading.Tasks;
using ewApps.Shipment.Entity;
using ewApps.Shipment.Data;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.Shipment.DS {

    /// <summary>
    /// This class implements standard business logic and operations for role entity.
    /// </summary>
    public class RoleDS:BaseDS<Role>, IRoleDS {

        #region Local Member 

        //IUnitOfWork _unitOfWork;
        IRoleRepository _roleRepository;
        IUserSessionManager _sessionmanager;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="sessionmanager"></param>
        public RoleDS(IRoleRepository roleRepository, IUserSessionManager sessionmanager) : base(roleRepository) {
            //_unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _sessionmanager = sessionmanager;
        }

        #endregion

        #region Get

        ///<inheritdoc/>
        public async Task<Role> GetAdminRoleIdByAppIdAndUserTypeAsync(Guid appId, int userType, CancellationToken token = default(CancellationToken)) {
            //return await _roleRepository.GetAdminRoleIdByAppIdAndUserTypeAsync(appId, userType, token);
            return null;
        }


        ///<inheritdoc/>
        public async Task<Guid> GetOrCreateRoleAsync(long userPermissionbitMask, Guid appId, int userType, Guid loginTenantUserId, CancellationToken token = default(CancellationToken)) {

            // Initialize roleId and get role if already present.
            Guid? roleid = null;
            roleid = await _roleRepository.GetRoleAsync(userPermissionbitMask, appId, userType);

            // Craete new role if role is not present.
            if(roleid == null) {
                Role role = new Role();
                role.ID = Guid.NewGuid();
                role.CreatedBy = loginTenantUserId;
                role.UpdatedBy = loginTenantUserId;
                role.CreatedOn = DateTime.UtcNow;
                role.UpdatedOn = role.CreatedOn;
                role.Deleted = false;
                role.RoleName = role.ID.ToString();
                role.RoleKey = "";
                role.AppId = appId;
                role.Active = true;
                role.PermissionBitMask = userPermissionbitMask;
                roleid = role.ID;
                role.UserType = userType;
                await _roleRepository.AddAsync(role);
                return (Guid)roleid;
            }
            return (Guid)roleid;
        }


        ///<inheritdoc/>
        public Role GetEntityByAppUserAndAppId() {
            UserSession session = _sessionmanager.GetSession();
            //return _roleRepository.GetEntityByAppUserAndAppId(session.AppId, session.TenantUserId);
            return null;
        }

        #endregion Get

    }
}
