/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 September 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ewApps.AppPortal.DS {
    public interface IBusinessUpdateDS {

        /// <summary>
        /// Update business and related entities.
        /// </summary>
        /// <param name="tenantRegistrtionDTO">Busines tenant object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BusinessResponseModelDTO> UpdateBusinessAsync(UpdateBusinessTenantModelDQ tenantRegistrtionDTO, CancellationToken token = default(CancellationToken));

    }
}
