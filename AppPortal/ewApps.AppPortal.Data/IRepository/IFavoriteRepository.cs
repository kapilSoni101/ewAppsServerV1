using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {

    /// <summary>
    /// Favorite 
    /// </summary>
   public interface IFavoriteRepository : IBaseRepository<Favorite>{
        
        /// <summary>
        /// Get favorite menu item list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="portalkey"></param>
        /// <param name="TenantId"></param>
        /// <param name="TenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<FavoriteViewDTO>> GetFavoriteMenuList(Guid appid, string portalkey, Guid TenantId,Guid TenantUserId, CancellationToken token = default(CancellationToken));
        
    }
}
