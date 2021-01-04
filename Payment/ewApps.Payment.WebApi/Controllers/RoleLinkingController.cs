using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.Payment.DS;
using ewApps.Payment.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.Payment.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class RoleLinkingController:ControllerBase {

        IRoleLinkingDS _roleLinkingDS;

        public RoleLinkingController(IRoleLinkingDS roleLinkingDS) {
            _roleLinkingDS = roleLinkingDS;
        }

       

    }
}