/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 06 Nov 2019

 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ASNotificationController : ControllerBase
  {

    #region Local Member

    IASNotificationDS _aSNotificationDS;

    #endregion Local Member

    #region Constructor

    public ASNotificationController(IASNotificationDS aSNotificationDS)
    {
      _aSNotificationDS = aSNotificationDS;
    }

    #endregion Constructor

    #region Get

    [Route("bizentity/{AppId}/{fromCount}/{toCount}")]
    [HttpGet]
    public async Task<List<ASNotificationDTO>> GetASNotificationList([FromRoute] Guid AppId, [FromRoute] int fromCount, [FromRoute] int toCount)
    {
      return await _aSNotificationDS.GetASNotificationList(AppId, fromCount, toCount);
    }

    [Route("bizentity/{AppId}")]
    [HttpGet]
    public async Task<List<ASNotificationDTO>> GetUnreadASNotificationCount([FromRoute] Guid AppId) {

      return await _aSNotificationDS.GetUnreadASNotificationList(AppId);

    }

    #endregion Get

    #region Update

    [Route("readasnotification/{Id}")]
    [HttpPut]
    public async Task<ResponseModelDTO> ReadASNotification([FromRoute] Guid Id)
    {
      return await _aSNotificationDS.ReadASNotification(Id);
    }

    #endregion Update

  }
}
