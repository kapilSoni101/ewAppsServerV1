using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Shipment.DS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Shipment.WebApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class RoleLinkingController:ControllerBase {

        IRoleLinkingDS _roleLinkingDS;

        public RoleLinkingController(IRoleLinkingDS roleLinkingDS) {
            _roleLinkingDS = roleLinkingDS;
        }

        
    }
}