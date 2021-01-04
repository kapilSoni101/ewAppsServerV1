/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */


using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {

    //This class Implement Business Logic of Platform Version  
    public interface IPlatVersionDS {

        /// <summary>
        /// Get Applicaition Version 
        /// </summary>
        /// <returns></returns>
        Task<ServerVersionDTO> ApplicationVersionAsync();

    }
}
