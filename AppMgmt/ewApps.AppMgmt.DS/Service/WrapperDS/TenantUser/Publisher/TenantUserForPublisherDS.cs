using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {
    public class TenantUserForPublisherDS :ITenantUserForPublisherDS {

        ITenantUserDS _tenantUserDS;

        public TenantUserForPublisherDS(ITenantUserDS tenantUserDS) {
            _tenantUserDS = tenantUserDS;
        }

        /// <summary>
        /// Get publisher primary user and get userid from email.
        /// </summary>
        /// <param name="reqDto">Publisher request model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PublisherTenantInfoDTO> GetPublisherAndUserAsync(PublisherRequestDTO reqDto, CancellationToken token = default(CancellationToken)) {
            UserShortInfoDQ user = await _tenantUserDS.GetTenantPrimaryUsersByTenantIdAndAppIdAndUserTypeAsync(reqDto.PublisherTenantId, reqDto.PubHomeAppId, (UserTypeEnum)reqDto.UserType, token);

            PublisherTenantInfoDTO dto = new PublisherTenantInfoDTO();

            dto.PublisherAdmin = user;

            TenantUser tenantUser = await _tenantUserDS.FindAsync(tu => tu.Email == reqDto.UserEmail && tu.Deleted == false);

            if(tenantUser != null) {
                dto.UserId = tenantUser.ID;
            }

            return dto;
        }
    }
}
