using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.AppMgmt.DS;
using ewApps.AppMgmt.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppMgmt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {

        #region Local member
        IThemeDS _themeDS;
        #endregion

        #region Consturctor
        public ThemeController(IThemeDS themeDS) {

            _themeDS = themeDS;
        }
        #endregion

        [HttpGet]
        [Route("getthemenameandthemekey")]
        public async Task<List<ThemeResponseDTO>> GetThemeNameAndThemeKeyAsync() {
            return await _themeDS.GetThemeNameAndThemeKey();
        }
    }
}