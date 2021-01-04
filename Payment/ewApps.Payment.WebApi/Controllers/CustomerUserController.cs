using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerUserController:ControllerBase {

        #region Local Member

        ICustUserAppManagmentDS _custUserAppManagmentDS;

        #endregion Local Member


        public CustomerUserController(ICustUserAppManagmentDS custUserAppManagmentDS) {
            _custUserAppManagmentDS = custUserAppManagmentDS;
        }

        //ToDo: nitin- It requires one wrapper class. don't use multiple DS from controller. Also these methods are not working in db transaction.
        //ToDo: nitin- Review url.
        //ToDo: nitin- dto name is not correct.
        //ToDo: nitin- method name should be close to Signup opeation. -- not
        [HttpPost]
        [Route("appassign")]
        public async Task AppAsignForBusinessUserAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _custUserAppManagmentDS.AppAsignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("appdeassign")]
        public async Task AppDeAsignForBusinessUserAsync(TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _custUserAppManagmentDS.AppDeAssignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("updaterole")]
        public async Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync([FromBody]TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _custUserAppManagmentDS.UpdateTenantUserRoleAsync(tenantUserAppManagmentDTO, cancellationToken);
        }
    }
}