/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Anil nigam <anigam@eworkplaceapps.com>
 * Date: 12 Aug 2019

 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

// This class implements Get, Add and update methods for support ticket.

    [Route("api/[controller]")]
    [ApiController]
    public class PlatformSupportController:ControllerBase {

        #region Local Member

        private static IHttpContextAccessor _contextAccessor;
        IPlatformSupportTicketDSNew _platformSupportTicketDSNew;

        #endregion Local Member

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformSupportController"/> class.
        /// </summary>
        /// <param name="platformSupportTicketDSNew">The platform support ticket ds new.</param>
        /// <param name="contextAccessor">The context accessor.</param>
        public PlatformSupportController(IPlatformSupportTicketDSNew platformSupportTicketDSNew, IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
            _platformSupportTicketDSNew = platformSupportTicketDSNew;
        }        

        #region Get

        [HttpGet]
        [Route("level4/ticket/list/{onlyDeleted}")]
        public async Task<List<SupportTicketDTO>> GetLevel4TicketList(bool onlyDeleted) {
            return await _platformSupportTicketDSNew.GetSupportTicketAssignedToLevel4List(onlyDeleted);
        }

        [HttpGet]
        [Route("ticket/level4/{supportTicketId}")]
        public async Task<SupportTicketDetailDTO> GetLevel4SupportTicketDetail(Guid supportTicketId) {
            return await _platformSupportTicketDSNew.GetSupportTicketDetailById(supportTicketId, true);
        }

        #endregion Get

        #region Update

        [HttpPut]
        [Route("level4")]
        public async Task<bool> UpdateLevel4Ticket() {
            //return _platformSupportTicketDSNew.UpdatePlatformSupportTicket(supportTicketDTO);

            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

            string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
            AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
            string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
            SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

            return _platformSupportTicketDSNew.UpdatePlatformSupportTicket(supportTicketDTO, documentModel, httpRequest);
        }

        #endregion Update 
    }
}