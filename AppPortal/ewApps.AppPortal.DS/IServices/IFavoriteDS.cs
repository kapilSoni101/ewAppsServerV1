using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// Favorite Add/update/delete
    /// </summary>
    public interface IFavoriteDS : IBaseDS<Favorite> {

        /// <summary>
        /// Check item is favorite or not
        /// </summary>
        /// <param name="token"></param>
        /// <param name="appid"></param>
        /// <param name="menukey"></param>
        /// <param name="portalkey"></param>
        /// <returns></returns>
        Task<FavoriteDTO> CheckFavoriteMenu(Guid appid, string menukey, string portalkey, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get favorite menu item list
        /// </summary>
        /// <returns></returns>
        Task<List<FavoriteViewDTO>> GetFavoriteMenuList(Guid appid, string portalkey, CancellationToken token = default(CancellationToken));
        
        /// <summary>
        /// Add favorite menu item
        /// </summary>
        /// <param name="favoriteAddDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> AddFavoriteMenu(FavoriteAddDTO favoriteAddDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// deleted favorite menu item
        /// </summary>
        /// <param name="favoriteUpdateDTO"></param>
        /// <param name="token"></param>
        Task<ResponseModelDTO> DeleteFavoriteMenu(FavoriteUpdateDTO favoriteUpdateDTO, CancellationToken token = default(CancellationToken));

    }
}
