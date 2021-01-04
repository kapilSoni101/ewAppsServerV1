using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// 
    /// </summary>
    public class FavoriteDS:BaseDS<Favorite>, IFavoriteDS {

        #region Local member
        IUserSessionManager _userSessionManager;
        IFavoriteRepository _favoriteRepository;
        IUnitOfWork _unitOfWork;
        #endregion Local member

        #region constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="favoriteRepository"></param>
        /// <param name="unitOfWork"></param>
        public FavoriteDS(IUserSessionManager userSessionManager, IFavoriteRepository favoriteRepository, IUnitOfWork unitOfWork) : base(favoriteRepository) {
            _userSessionManager = userSessionManager;
            _favoriteRepository = favoriteRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion constructor

        /// <summary>
        /// Add favorite menu item
        /// </summary>
        /// <param name="favoriteAddDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> AddFavoriteMenu(FavoriteAddDTO favoriteAddDTO, CancellationToken token = default(CancellationToken)) {
            // Add in Favorite
            UserSession userSession = _userSessionManager.GetSession();
            ResponseModelDTO responseModel = new ResponseModelDTO();

            // Get entity if exists
            Favorite favorite = await FindAsync(f => f.TenantUserId == userSession.TenantUserId && f.TenantId == userSession.TenantId && f.AppId == favoriteAddDTO.AppId && f.MenuKey == favoriteAddDTO.MenuKey && f.Deleted == false);
            if(favorite == null) {
                favorite = new Favorite();
                favorite.ID = Guid.NewGuid();
                favorite.TenantUserId = userSession.TenantUserId;
                favorite.CreatedBy = userSession.TenantUserId;
                favorite.UpdatedBy = userSession.TenantUserId;
                favorite.CreatedOn = DateTime.UtcNow;
                favorite.UpdatedOn = DateTime.UtcNow;
                favorite.Deleted = false;
                favorite.TenantId = userSession.TenantId;

                favorite.MenuKey = favoriteAddDTO.MenuKey;
                favorite.Url = favoriteAddDTO.Url;
                favorite.PortalKey = favoriteAddDTO.PortalKey;
                favorite.AppId = favoriteAddDTO.AppId;

                responseModel.IsSuccess = true;
                await AddAsync(favorite, token);

                // Save Data
                _unitOfWork.SaveAll();
            }
            else {
                responseModel.IsSuccess = true;
            }
            return responseModel;
        }




        /// <summary>
        /// Check menu item favorite or not
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="menukey"></param>
        /// <param name="portalkey"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<FavoriteDTO> CheckFavoriteMenu(Guid appid, string menukey, string portalkey, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            FavoriteDTO isFavoriteModel = new FavoriteDTO();

            // Favorite favorite = await FindAsync(f => f.TenantUserId == userSession.TenantUserId && f.TenantId == userSession.TenantId && f.AppId == appid && f.MenuKey == menukey && f.PortalKey == portalkey && f.Deleted == false);

            Favorite favorite = await FindAsync(f => (f.TenantUserId == userSession.TenantUserId || f.TenantUserId == Guid.Empty) && (f.TenantId == userSession.TenantId || f.TenantId == Guid.Empty) && f.AppId == appid && f.MenuKey == menukey && f.PortalKey == portalkey && f.Deleted == false);

            if(favorite != null) {
                isFavoriteModel.IsFavorite = true;
                isFavoriteModel.SystemFavorite = favorite.System;
            }
            else {
                isFavoriteModel.IsFavorite = false;
                isFavoriteModel.SystemFavorite = false;
            }

            return isFavoriteModel;
        }


        /// <summary>
        /// Get favorite menu item list
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="portalkey"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<FavoriteViewDTO>> GetFavoriteMenuList(Guid appid, string portalkey, CancellationToken token = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            return await _favoriteRepository.GetFavoriteMenuList(appid, portalkey, userSession.TenantId, userSession.TenantUserId, token);
        }

        /// <summary>
        /// Deleted favorite menu item
        /// </summary>
        /// <param name="favoriteUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<ResponseModelDTO> DeleteFavoriteMenu(FavoriteUpdateDTO favoriteUpdateDTO, CancellationToken token = default(CancellationToken)) {

            UserSession userSession = _userSessionManager.GetSession();
            ResponseModelDTO responseModel = new ResponseModelDTO();

            // Get entity if exists
            Favorite favorite = await FindAsync(f => f.TenantUserId == userSession.TenantUserId && f.TenantId == userSession.TenantId && f.AppId == favoriteUpdateDTO.AppId && f.MenuKey == favoriteUpdateDTO.MenuKey && f.Deleted == false);
            if(favorite != null) {
                responseModel.IsSuccess = false;
                Delete(favorite);
                // Save Data
                _unitOfWork.SaveAll();
            }
            else {
                responseModel.IsSuccess = true;
            }            
            return responseModel;
        }
    }
}
