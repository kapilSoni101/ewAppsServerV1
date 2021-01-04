using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class PlatBusinessController : ControllerBase
    {
        #region Local Member

        IQPlatBusinessDS _platBusinessDS;
        

        #endregion Local Member

        #region Constructor

        public PlatBusinessController(IQPlatBusinessDS platBusinessDS) {
            _platBusinessDS = platBusinessDS;           
        }

        #endregion Constructor

        [Route("tenantsonplatform")]
        [HttpPut]
        public async Task<List<PlatBusinessDTO>> GetTenantListOnPlatformAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _platBusinessDS.GetTenantListOnPlatformAsync(filter, token);
        }
    }
}