using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Shipment.Data;
using ewApps.Shipment.DTO;
using ewApps.Shipment.Entity;

namespace ewApps.Shipment.DS {
   public class BusUserAppManagmentDS :IBusUserAppManagmentDS {
        
        #region Local Member

        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IShipmentUnitOfWork _shipmentUnitOfWork;

        #endregion Local Member

        public BusUserAppManagmentDS(IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IShipmentUnitOfWork shipmentUnitOfWork) {
            _roleLinkingDS = roleLinkingDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _shipmentUnitOfWork = shipmentUnitOfWork;
        }

        public async Task AppAssignAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _roleLinkingDS.AddShipmentRolelinkingAsync(tenantUserAppManagmentDTO);
            await _tenantUserAppPreferenceDS.AddTenantUserAppPreferncesAsync(tenantUserAppManagmentDTO);
            _shipmentUnitOfWork.SaveAll();
        }

        public async Task AppDeAssignAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            // Remove rolelinking.
            RoleLinking roleLinking = await _roleLinkingDS.FindAsync(rl => rl.TenantUserId == tenantUserAppManagmentDTO.TenantUserId && rl.TenantId == tenantUserAppManagmentDTO.TenantId && rl.AppId == tenantUserAppManagmentDTO.AppId && rl.Deleted == false);
            if(roleLinking != null) {
                roleLinking.Deleted = true;
                await _roleLinkingDS.UpdateAsync(roleLinking, roleLinking.ID);
            }
            // Remove prefrence linking.
            TenantUserAppPreference tenantUserAppPreference = await _tenantUserAppPreferenceDS.FindAsync(rl => rl.TenantUserId == tenantUserAppManagmentDTO.TenantUserId && rl.TenantId == tenantUserAppManagmentDTO.TenantId && rl.AppId == tenantUserAppManagmentDTO.AppId && rl.Deleted == false);
            if(tenantUserAppPreference != null) {
                tenantUserAppPreference.Deleted = true;
                await _tenantUserAppPreferenceDS.UpdateAsync(tenantUserAppPreference, tenantUserAppPreference.ID);
            }
            // Save changes.
            _shipmentUnitOfWork.SaveAll();
        }

        public async Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _roleLinkingDS.UpdateShipmentRoleAsync(tenantUserAppManagmentDTO, cancellationToken);
        }
    }
}
