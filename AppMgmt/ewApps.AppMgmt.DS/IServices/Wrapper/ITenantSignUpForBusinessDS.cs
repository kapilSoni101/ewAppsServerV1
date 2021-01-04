using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Common;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UniqueIdentityGeneratorService;
using ewApps.Core.UserSessionService;
using Newtonsoft.Json;

namespace ewApps.AppMgmt.DS {
    public interface ITenantSignUpForBusinessDS {

        /// <summary>
        /// Method is used to singup business as well as subscribe the application for business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="token"></param>
        /// <returns>return response model.</returns>
        Task<TenantSignUpForBusinessResDTO> BusinessSignupAsync(BusinessSignUpDTO businessRegistrtionDTO, CancellationToken token = default(CancellationToken));       

    }
}
