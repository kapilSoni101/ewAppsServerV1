/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for appUser entity.
    /// </summary>
    public interface IAboutUsDS {

        /// <summary>
        /// Get About us Detail by for platform
        /// </summary>
        /// <returns></returns>
        Task<AboutUsDTO> GetPlatformAboutUsDetailsAsync();


        

        /// <summary>
        /// Get About us Detail on publisher
        /// </summary>
        /// <returns></returns>
        Task<AboutUsDTO> GetPublisherAboutUsDetails();

        /// <summary>
        /// Get About us Detail on business 
        /// </summary>
        /// <returns></returns>
        Task<AboutUsDTO> GetBusinessAboutUsDetails(Guid appId);

        /// <summary>
        /// Get About us Detail on Vendor 
        /// </summary>
        /// <returns></returns>
        Task<AboutUsDTO> GetVendorAboutUsDetails(Guid appId);

        }

}