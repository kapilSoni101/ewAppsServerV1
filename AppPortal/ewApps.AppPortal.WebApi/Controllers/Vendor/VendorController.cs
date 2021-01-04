using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Vendor
{
  [Route("api/[controller]")]
  [ApiController]
  public class VendorController : ControllerBase
  {

    #region Local Member

    IVendorSignUpDS _vendorSignUpDS;
        IVendorDS _vendorDS;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// initilize constructor.
        /// </summary>
        /// <param name="vendorSignUpDS"></param>
        public VendorController(IVendorSignUpDS vendorSignUpDS, IVendorDS vendorDS)
    {
      _vendorSignUpDS = vendorSignUpDS;
            _vendorDS = vendorDS;
    }

        #endregion Constructor

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getconfiguration/{buspartnertenantid}")]
        public async Task<VendorConfigurationDTO> GetVendorConfigurationAsync(Guid buspartnertenantid, CancellationToken token = default(CancellationToken)) {
            return await _vendorDS.GetVendorConfigurationDetailAsync(buspartnertenantid, token);
        }

        #endregion Get

        /// <summary>
        ///SignUp customer .
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
    [Route("signup")]
    public async Task<VendorSignUpResDTO> VendorSignUpAsync([FromBody] List<VendorSignUpReqDTO> request, CancellationToken token = default(CancellationToken))
    {
      VendorSignUpResDTO result = await _vendorSignUpDS.VendorSignUpAsync(request, token);
      return result;
    }
  }
}