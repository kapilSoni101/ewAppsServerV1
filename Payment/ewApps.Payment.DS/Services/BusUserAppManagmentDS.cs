﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {
    public class BusUserAppManagmentDS:IBusUserAppManagmentDS {

        #region Local Member

        IRoleLinkingDS _roleLinkingDS;
        ITenantUserAppPreferenceDS _tenantUserAppPreferenceDS;
        IPaymentUnitOfWork _paymentUnitOfWork;

        #endregion Local Member

        public BusUserAppManagmentDS(IRoleLinkingDS roleLinkingDS, ITenantUserAppPreferenceDS tenantUserAppPreferenceDS, IPaymentUnitOfWork paymentUnitOfWork) {
            _roleLinkingDS = roleLinkingDS;
            _tenantUserAppPreferenceDS = tenantUserAppPreferenceDS;
            _paymentUnitOfWork = paymentUnitOfWork;
        }

        public async Task AppAssignAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _roleLinkingDS.AddPaymentRolelinkingAsync(tenantUserAppManagmentDTO);
            await _tenantUserAppPreferenceDS.AddTenantUserAppPreferncesAsync(tenantUserAppManagmentDTO, (int)UserTypeEnum.Business);
            _paymentUnitOfWork.SaveAll();
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
            _paymentUnitOfWork.SaveAll();
        }

        public async Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _roleLinkingDS.UpdatePaymentRoleAsync(tenantUserAppManagmentDTO, cancellationToken);
        }

    }
}
