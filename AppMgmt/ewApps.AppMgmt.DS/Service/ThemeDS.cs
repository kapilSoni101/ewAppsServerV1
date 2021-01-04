// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 14 Aug 2018
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 14 Aug 2018
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppMgmt.Data;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {


    /// <summary>
    /// Represents all the operations to be performed on Theme entity.
    /// </summary>
    public class ThemeDS:BaseDS<Theme>, IThemeDS {
        #region Local Member 
        
        IUnitOfWork _unitOfWork;
        IThemeRepository _themeRep;
        #endregion


        #region Constructor

        /// <summary>
        ///  Initialinzing local variables
        /// </summary>
        /// <param name="themeRep"></param>
        /// <param name="cacheService"></param>
        /// <param name="mapper"></param>
        public ThemeDS(IThemeRepository themeRep) : base(themeRep) {
            _themeRep = themeRep;
        }
        #endregion

        #region public methods 
        #region GET

        /// <summary>
        /// Get Theme Name and ThemeKey
        /// </summary>
        /// <returns></returns>
        public async Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKey() {
            IEnumerable<Theme> theme = await _themeRep.GetEntityAsync();            
            List<ThemeResponseDTO> listDto = new List<ThemeResponseDTO>();
            ThemeResponseDTO dto = new ThemeResponseDTO();
            foreach(Theme item in theme) {
                dto = ThemeResponseDTO.MapEntityToThemeReponseDTO(item);
                listDto.Add(dto);
            }         

            return listDto;

        }
        #endregion GET 
        #endregion
    }
}

