using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {
  public class TenantUserStatusDS:ITenantUserStatusDS {

    #region Local Member

    ITenantUserAppLastAccessInfoDS _tenantUserAppLastAccessInfoDS;
    IUserTenantLinkingDS _userTenantLinkingDS;
    ITenantUserAppLinkingDS _tenantUserAppLinkingDS;
    ITenantDS _tenantDS;
    IUnitOfWork _unitOfWork;
    IAppDS _appDS;
    ITenantUserDS _tenantUserDS;

    #endregion Local Member

    public TenantUserStatusDS(ITenantUserAppLastAccessInfoDS tenantUserAppLastAccessInfoDS, IUserTenantLinkingDS userTenantLinkingDS,
    ITenantUserAppLinkingDS tenantUserAppLinkingDS, ITenantDS tenantDS, IUnitOfWork unitOfWork, IAppDS appDS, ITenantUserDS tenantUserDS) {
      _tenantUserAppLastAccessInfoDS = tenantUserAppLastAccessInfoDS;
      _userTenantLinkingDS = userTenantLinkingDS;
      _tenantUserAppLinkingDS = tenantUserAppLinkingDS;
      _unitOfWork = unitOfWork;
      _tenantDS = tenantDS;
      _appDS = appDS;
      _tenantUserDS = tenantUserDS;
    }

    // Add/Update user last login info.
    // Update user status and joined date if first login.
    // If primary user update tenant join date.
    public async Task<bool> UpdateTenantUserLoginJoinedStatus(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO) {

      App app = await _appDS.FindAsync(a => a.AppKey == tenantUserAppLastAccessInfoRequestDTO.AppKey);
      if(tenantUserAppLastAccessInfoRequestDTO.TenantUserId == null || tenantUserAppLastAccessInfoRequestDTO.TenantUserId == Guid.Empty) {
        TenantUser tenantUser = await _tenantUserDS.FindAsync(tu => tu.IdentityUserId == tenantUserAppLastAccessInfoRequestDTO.IdentityUserId);
        tenantUserAppLastAccessInfoRequestDTO.TenantUserId = tenantUser.ID;
      }

      // Update/Add user last login info.
      bool firstLogin = await _tenantUserAppLastAccessInfoDS.AddUpdateTenantUserLastAccessInfoAsync(tenantUserAppLastAccessInfoRequestDTO, app.ID);
      if(firstLogin) {
        UserTenantLinking userTenantLinking = await _userTenantLinkingDS.FindAsync(li => li.TenantUserId == tenantUserAppLastAccessInfoRequestDTO.TenantUserId && li.TenantId == tenantUserAppLastAccessInfoRequestDTO.TenantId && li.Deleted == false);
        if(userTenantLinking != null) {
          Tenant tenant = await _tenantDS.GetAsync(tenantUserAppLastAccessInfoRequestDTO.TenantId);
          if(userTenantLinking.IsPrimary == true) {
            tenant.JoinedOn = DateTime.UtcNow;
          }
        }
      }

//changed this code because user can assign/deassign application multiple times.
      firstLogin = false;
      TenantUserAppLinking tenantUserAppLinking = await _tenantUserAppLinkingDS.FindAsync(li => li.TenantUserId == tenantUserAppLastAccessInfoRequestDTO.TenantUserId && li.AppId == app.ID && li.TenantId == tenantUserAppLastAccessInfoRequestDTO.TenantId && li.Deleted == false);
      if(tenantUserAppLinking != null && tenantUserAppLinking.Status != (int)TenantUserInvitaionStatusEnum.Accepted) {
        tenantUserAppLinking.Status = (int)TenantUserInvitaionStatusEnum.Accepted;
        tenantUserAppLinking.JoinedDate = DateTime.UtcNow;
        firstLogin = true;
      }

      _unitOfWork.SaveAll();
      return firstLogin;
    }

  }
}
