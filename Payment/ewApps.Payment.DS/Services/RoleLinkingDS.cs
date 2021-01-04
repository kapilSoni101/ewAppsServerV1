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

using ewApps.Payment.Entity;
using ewApps.Payment.Data;
using ewApps.Core.BaseService;
using System.Threading.Tasks;
using System.Threading;
using ewApps.Payment.DTO;
using System;
using ewApps.Payment.Common;

namespace ewApps.Payment.DS {

    /// <summary>
    ///  This class implements standard business logic and operations for roleLinking entity.
    /// </summary>
    public class RoleLinkingDS:BaseDS<RoleLinking>, IRoleLinkingDS {

        #region Local Member 

        IRoleLinkingRepository _roleLinkingRepository;
        IRoleDS _roleDS;
        IPaymentUnitOfWork _paymentUnitOfWork;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initialinzing local variables
        /// </summary>
        public RoleLinkingDS(IRoleLinkingRepository roleLinkingRep, IRoleDS roleDS, IPaymentUnitOfWork paymentUnitOfWork) : base(roleLinkingRep) {
            _roleLinkingRepository = roleLinkingRep;
            _roleDS = roleDS;
            _paymentUnitOfWork = paymentUnitOfWork;
        }

        #endregion

        #region Add 

        public async Task AddPaymentRolelinkingAsync(TenantUserAppManagmentDTO roleLinkingDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            long permissionBitMask = 0;

            if(roleLinkingDTO.UserType == (int)UserTypeEnum.Business) {
                if(roleLinkingDTO.Admin) {
                    permissionBitMask = (long)BusinessUserPaymentAppPermissionEnum.All;
                }
                else {
                    permissionBitMask = roleLinkingDTO.PermissionBitMask;
                }
            }
            else if(roleLinkingDTO.UserType == (int)UserTypeEnum.Customer) {
                if(roleLinkingDTO.Admin) {
                    permissionBitMask = (long)PaymentAppCustomerUserPermissionEnum.All;
                }
                else {
                    permissionBitMask = roleLinkingDTO.PermissionBitMask;
                }
            }
            
            // Get role/add role based on input permission mask bit.
            Guid roleId = await _roleDS.GetOrCreateRoleAsync(permissionBitMask, roleLinkingDTO.AppId, roleLinkingDTO.UserType, roleLinkingDTO.TenantUserId, cancellationToken);

            // Add Role linking entry for user.
            RoleLinking roleLinking = await FindAsync(rl => rl.TenantUserId == roleLinkingDTO.TenantUserId && rl.AppId == roleLinkingDTO.AppId && rl.TenantId == roleLinkingDTO.TenantId && rl.Deleted == false);

            if(roleLinking == null) {
                // Inisilize the instance.
                roleLinking = new RoleLinking();

                if(roleLinkingDTO.RoleLinkingId == Guid.Empty) {
                    roleLinking.ID = Guid.NewGuid();
                }
                roleLinking.RoleId = roleId;
                roleLinking.AppId = roleLinkingDTO.AppId;
                roleLinking.TenantId = roleLinkingDTO.TenantId;
                roleLinking.TenantUserId = roleLinkingDTO.TenantUserId;
                roleLinking.CreatedBy = roleLinkingDTO.CreatedBy;
                roleLinking.CreatedOn = DateTime.UtcNow;
                roleLinking.UpdatedBy = roleLinking.CreatedBy;
                roleLinking.UpdatedOn = roleLinking.CreatedOn;

                // Add Rolelinking.
                await AddAsync(roleLinking, cancellationToken);
            }
        }

        public async Task<RoleUpdateResponseDTO> UpdatePaymentRoleAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {

            RoleUpdateResponseDTO roleUpdateResponseDTO = new RoleUpdateResponseDTO();

            // Get current role.
           // Role currentRole = await _roleDS.FindAsync(r => r.AppId == tenantUserAppManagmentDTO.AppId && r.UserType == tenantUserAppManagmentDTO.UserType && r.PermissionBitMask == tenantUserAppManagmentDTO.PermissionBitMask);
      

            // If role already exists then no need to update anything
           // if(currentRole == null) {
                // Get current roleId
                RoleLinking roleLinking = await FindAsync(rl => rl.TenantUserId == tenantUserAppManagmentDTO.TenantUserId && rl.AppId == tenantUserAppManagmentDTO.AppId && rl.TenantId == tenantUserAppManagmentDTO.TenantId && rl.Deleted == false);

                // Get role/add role based on input permission mask bit.
                Guid roleId = await _roleDS.GetOrCreateRoleAsync(tenantUserAppManagmentDTO.PermissionBitMask, tenantUserAppManagmentDTO.AppId, tenantUserAppManagmentDTO.UserType, tenantUserAppManagmentDTO.TenantUserId, cancellationToken);
                if(roleId != roleLinking.RoleId) {
                    roleLinking.RoleId = roleId;
                    roleLinking.UpdatedBy = tenantUserAppManagmentDTO.CreatedBy;
                    roleLinking.UpdatedOn = DateTime.UtcNow;
                    await UpdateAsync(roleLinking, roleLinking.ID);

                    Role role = await _roleDS.GetAsync(roleId);

                    // Update respose DTO
                    roleUpdateResponseDTO.RoleUpdated = true;
                    roleUpdateResponseDTO.RoleId = roleId;
                    roleUpdateResponseDTO.PermisssionBitMask = tenantUserAppManagmentDTO.PermissionBitMask;
                    roleUpdateResponseDTO.RoleKey = role.RoleKey;
                }
           // }

            _paymentUnitOfWork.SaveAll();
            return roleUpdateResponseDTO;
        }

        #endregion Add 

    }
}
