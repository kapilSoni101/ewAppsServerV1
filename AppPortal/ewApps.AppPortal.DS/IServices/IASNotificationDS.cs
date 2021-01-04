/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 06 Nov 2019

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

    public interface IASNotificationDS:IBaseDS<ASNotification> {

        Task<List<ASNotificationDTO>> GetASNotificationList(Guid AppId, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        Task<List<ASNotificationDTO>> GetBizSetupASNotificationListAsync(List<KeyValuePair<string, Guid>> appIdList, int fromCount, int toCount, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> ReadASNotification(Guid Id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<ASNotificationDTO>> GetUnreadASNotificationList(Guid AppId, CancellationToken token = default(CancellationToken));

    }


}
