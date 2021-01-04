using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class BusinessUserController:ControllerBase {

        #region Local Member

        IBusUserAppManagmentDS _busUserAppManagmentDS;

        #endregion Local Member


        public BusinessUserController(IBusUserAppManagmentDS busUserAppManagmentDS) {
            _busUserAppManagmentDS = busUserAppManagmentDS;
        }

        //ToDo: nitin- It requires one wrapper class. don't use multiple DS from controller. Also these methods are not working in db transaction.
        //ToDo: nitin- Review url.
        //ToDo: nitin- dto name is not correct.
        //ToDo: nitin- method name should be close to Signup opeation. -- not
        [HttpPost]
        [Route("appassign")]
        public async Task AppAsignForBusinessUserAsync([FromBody]TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _busUserAppManagmentDS.AppAssignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("appdeassign")]
        public async Task AppDeAsignForBusinessUserAsync([FromBody]TenantUserAppManagmentDTO tenantUserAppManagmentDTO) {
            await _busUserAppManagmentDS.AppDeAssignAsync(tenantUserAppManagmentDTO);
        }

        [HttpPut]
        [Route("updaterole")]
        public async Task<RoleUpdateResponseDTO> UpdateTenantUserRoleAsync([FromBody]TenantUserAppManagmentDTO tenantUserAppManagmentDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busUserAppManagmentDS.UpdateTenantUserRoleAsync(tenantUserAppManagmentDTO, cancellationToken);
        }
    }
}