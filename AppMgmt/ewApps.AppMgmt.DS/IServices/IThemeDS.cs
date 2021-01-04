/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 Aug 2018
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 14 Aug 2019
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    public interface IThemeDS:IBaseDS<Theme> {
        Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey();
    }
}
 