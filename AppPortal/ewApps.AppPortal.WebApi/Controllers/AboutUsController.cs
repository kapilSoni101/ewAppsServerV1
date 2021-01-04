/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutUsController : ControllerBase
    {
        #region local member

        IAboutUsDS _aboutUs;

        #endregion local member

        #region constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aboutUs"></param>
        public AboutUsController(IAboutUsDS aboutUs) {
            _aboutUs = aboutUs;
        }

        #endregion constructor

        #region Public Method

        #region Get

        #region Platform About Us
		//<summary>
        /// Get the about us details of the platform.
        ///</summary>
        [HttpGet]
        [Route("plataboutus")]
        public async Task<AboutUsDTO> GetAboutUsDetailsAsync() {
            return await _aboutUs.GetPlatformAboutUsDetailsAsync();
        }
        
        #endregion

        #region Publisher About Us
        ///<summary>
        /// Get the about us details of the platform.
        ///</summary>
        [Route("pubaboutus")]
        [HttpGet]
        public async Task<AboutUsDTO> GetAboutUsDetails() {
            return await _aboutUs.GetPublisherAboutUsDetails();
        }
        #endregion

        #region Business About Us
        ///<summary>
        /// Get the about us details of the platform.
        ///</summary>
        [Route("busaboutus/{appId}")]
        [HttpGet]
        public async Task<AboutUsDTO> BusGetAboutUsDetailsAsync([FromRoute]Guid appId) {
            return await _aboutUs.GetBusinessAboutUsDetails(appId);
        }
        #endregion

        #region Business About Us
        ///<summary>
        /// Get the about us details of the platform.
        ///</summary>
        [Route("vendaboutus/{appId}")]
        [HttpGet]
        public async Task<AboutUsDTO> VendGetAboutUsDetailsAsync([FromRoute]Guid appId) {
            return await _aboutUs.GetVendorAboutUsDetails(appId);
        }
        #endregion



        #endregion

        #endregion
    }
}