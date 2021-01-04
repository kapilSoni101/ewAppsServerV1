using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController:ControllerBase {

        #region Local member

        IFavoriteDS _favoriteDS;

        #endregion Local member

        #region cunstructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="favoriteDS"></param>
        public FavoriteController(IFavoriteDS favoriteDS) {
            _favoriteDS = favoriteDS;
        }

        #endregion cunstructor

        #region Add method

        /// <summary>
        /// Add favorite Menu Item
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("addfavoritemenu")]
        public async Task<ResponseModelDTO> AddFavoriteMenu(FavoriteAddDTO favoriteAddDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _favoriteDS.AddFavoriteMenu(favoriteAddDTO, token);
            return responseModelDTO;

        }

        #endregion Add method

        #region Get method

        /// <summary>
        /// Check menu item is favorite or not
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("isfavoritemenu/{appid}/{menukey}/{portalkey}")]
        public async Task<FavoriteDTO> CheckFavoriteMenu([FromRoute] Guid appid, [FromRoute] string menukey, string portalkey, CancellationToken token = default(CancellationToken)) {
            return await _favoriteDS.CheckFavoriteMenu(appid, menukey, portalkey, token);
        }


        /// <summary>
        /// Get favorite menu item list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getfavoritemenu/{appid}/{portalkey}")]
        public async Task<List<FavoriteViewDTO>> GetFavoriteMenuList([FromRoute] Guid appid, [FromRoute] string portalkey, CancellationToken token = default(CancellationToken)) {
            return await _favoriteDS.GetFavoriteMenuList(appid, portalkey, token);
        }
        
        #endregion Get method

        #region Delete method

        /// <summary>
        /// delete favorite menu item 
        /// </summary>
        /// <param name="favoriteUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("deletefavoritemenu")]
        public async Task<ResponseModelDTO> DeleteFavoriteMenu(FavoriteUpdateDTO favoriteUpdateDTO, CancellationToken token = default(CancellationToken)) {
            ResponseModelDTO responseModelDTO = await _favoriteDS.DeleteFavoriteMenu(favoriteUpdateDTO);
            return responseModelDTO;

        }

        #endregion Delete method

    }
}
