/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 31 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 31 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
    public interface IViewSettingsDS :  IBaseDS<ViewSettings> {


        /// <summary>
        /// Add Update View Settings
        /// </summary>
        /// <param name="viewSettingDTO"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddUpdateViewSettings(ViewSettingDTO viewSettingDTO, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Delete View Setting If Exist 
        /// </summary>
        /// <param name="viewSettingDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> DeleteViewSettings(Guid id,CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get View Settings List 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appkey"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<ViewSettingDTO>> GetViewSettingsListAsync(string screenkey, string appkey, CancellationToken token = default(CancellationToken));
    }
}
