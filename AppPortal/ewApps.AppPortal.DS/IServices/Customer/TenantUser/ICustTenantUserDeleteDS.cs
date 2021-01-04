/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 9 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 9 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
   public interface ICustTenantUserDeleteDS {

        Task<DeleteUserResponseDTO> DeleteTenantUser(TenantUserIdentificationDTO tenantUserIdentificationDTO);
    }
}
