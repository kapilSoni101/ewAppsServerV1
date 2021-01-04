using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class BrandingController {

        #region Local member
        IBrandingDS _brandingDS;
        #endregion


        #region Consturctor
        public BrandingController(IBrandingDS brandingDS) {

            _brandingDS = brandingDS;
        }
        #endregion



        #region UPDATE platform Branding

        /// <summary>
        /// Update Platfrom Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="brandingDTO">platform branding model</param>
        [Route("platformupdate")]
        [HttpPut]
        public List<Guid?> UpdatePlatformBrandingDetail([FromBody] BrandingDTO brandingDTO) {
           return _brandingDS.UpdatePlatformBrandingDetail(brandingDTO);
        }

        #endregion UPDATE


        #region Update Publisher branding
        /// <summary>
        /// Update Publisher Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="brandingDTO">platform branding model</param>
        [Route("publisherupdate")]
        [HttpPut]
        public async Task UpdatePublisherBrandingDetail([FromBody] BrandingDTO brandingDTO) {
             await _brandingDS.UpdatePublisherBrandingDetail(brandingDTO);
        }
        #endregion

        #region Update business branding
        /// <summary>
        /// Update Business Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="businessbrandingDTO">platform branding model</param>
        [Route("businessupdate")]
        [HttpPut]
        public async Task UpdatebusinessBrandingDetail([FromBody] BusinessBrandingDTO businessbrandingDTO) {
            await _brandingDS.UpdatebusinessBrandingDetail(businessbrandingDTO);
        }
        #endregion

        #region Update ship-app branding
        /// <summary>
        /// Update ship-app Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="shipAppBrandingDTO">ship-app branding model</param>
        [Route("shipappupdate")]
        [HttpPut]
        public async Task UpdateShipAppBrandingAsync([FromBody] AppPortalBrandingDTO shipAppBrandingDTO) {
            await _brandingDS.UpdateShipAppBrandingAsync(shipAppBrandingDTO);
        }
        #endregion

        #region Update pay-app branding
        /// <summary>
        /// Update pay-app Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="payAppBrandingDTO">pay-app branding model</param>
        [Route("payappupdate")]
        [HttpPut]
        public async Task UpdatePayAppBrandingAsync([FromBody] AppPortalBrandingDTO payAppBrandingDTO) {
            await _brandingDS.UpdatePayAppBrandingAsync(payAppBrandingDTO);
        }
        #endregion

        #region Update cust-app branding
        /// <summary>
        /// Update cust-app Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="custAppBrandingDTO">cust-app branding model</param>
        [Route("custappupdate")]
        [HttpPut]
        public async Task UpdateCustAppBrandingAsync([FromBody] AppPortalBrandingDTO custAppBrandingDTO) {
            await _brandingDS.UpdateCustAppBrandingAsync(custAppBrandingDTO);
        }
        #endregion

        #region Update vend-app branding
        /// <summary>
        /// Update vend-app Branding 
        /// </summary>
        /// <returns>ResponseModel</returns>
        /// <param name="vendAppBrandingDTO">vend-app branding model</param>
        [Route("vendappupdate")]
        [HttpPut]
        public async Task UpdateVendAppBrandingAsync([FromBody] AppPortalBrandingDTO vendAppBrandingDTO) {
            await _brandingDS.UpdateVendAppBrandingAsync(vendAppBrandingDTO);
        }
        #endregion

    }
}
