using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// A business wrapper interface, expose all method related to business/user/tenant.
    /// </summary>
    public interface IBusinessSignUpDS {    

        #region Add/Update/Delete

        /// <summary>
        /// Method is used to singup business as well as subscribe the application for business.
        /// </summary>
        /// <param name="businessRegistrtionDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> BusinessSignUpAsync(BusinessSignUpRequestDTO businessRegistrtionDTO, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Update business and chile entities.
    /// </summary>
    /// <param name="tenantRegistrtionDTO">Business detail model with all subscribed application object..</param>
    /// <param name="token"></param>
    /// <returns></returns>
    //Task<BusinessResponseModelDTO> UpdateBusinessTenantAsync(UpdateTenantModelDQ tenantRegistrtionDTO, CancellationToken token = default(CancellationToken));

    Task ReInvitePrimaryBusinessUserAsync(Guid tenantUserId, Guid bizTenantId,  string subDomain, CancellationToken token = default(CancellationToken));

                #endregion Add/Update/Delete

  }
}
