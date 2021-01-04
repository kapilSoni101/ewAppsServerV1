using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    public class VendorBAItemMasterController:ControllerBase {

        #region Local Members

        IVendorBAItemMasterDS _busItemMasterDS;

        #endregion Local Members

        #region Constructor 

        public VendorBAItemMasterController(IVendorBAItemMasterDS busItemMasterDS) {
            _busItemMasterDS = busItemMasterDS;
        }

        #endregion Constructor


    }
}