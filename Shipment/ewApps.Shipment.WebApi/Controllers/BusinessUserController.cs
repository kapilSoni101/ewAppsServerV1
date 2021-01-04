using System.Threading;
using System.Threading.Tasks;
using ewApps.Shipment.DS;
using ewApps.Shipment.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Shipment.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserController:ControllerBase {

        #region Local Member

        IBusUserAppManagmentDS _busUserAppManagmentDS;

        #endregion Local Member


        public BusinessUserController(IBusUserAppManagmentDS busUserAppManagmentDS) {
            _busUserAppManagmentDS = busUserAppManagmentDS;
        }

        //ToDo: nitin- similar to payment api.
        [HttpPost]
        [Route("appassign")]
        public async Task AppAsignForBusinessUserAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _busUserAppManagmentDS.AppAssignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("appdeassign")]
        public async Task AppDeAsignForBusinessUserAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _busUserAppManagmentDS.AppDeAssignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("updaterole")]
        public async Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync([FromBody]TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busUserAppManagmentDS.UpdateTenantUserRoleAsync(tenantUserAppManagmentDTO, cancellationToken);
        }
    }
}