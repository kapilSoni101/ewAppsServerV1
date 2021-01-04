/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Anil nigam <anigam @eworkplaceapps.com>
 * Date: 12 Aug 2019

 */
    using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    [Route("api/[controller]")]
    [ApiController]

    public class PublisherSupportController:ControllerBase {

        #region Local Member

        private static IHttpContextAccessor _contextAccessor;
        IPublisherSupportTicketDSNew _publisherSupportTicketDS;

        #endregion Local Member

        /// <summary>
        /// Initializes a new instance of the <see cref="PublisherSupportController"/> class.
        /// </summary>
        /// <param name="publisherNew">The publisher new.</param>
        /// <param name="contextAccessor">The context accessor.</param>
        public PublisherSupportController(IPublisherSupportTicketDSNew publisherNew, IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
            _publisherSupportTicketDS = publisherNew;
        }

        #region Add Ticket Publisher

        [HttpPost]
        [Route("level3")]
        public async Task<SupportTicket> AddLevel3Ticket() {

            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

            string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
            AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
            string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
            SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

            return _publisherSupportTicketDS.AddPublisherSupportTicket(supportTicketDTO, documentModel, httpRequest);
        }

        #endregion Add Ticket Publisher

        #region Update Ticket Publisher

        [HttpPut]
        [Route("level3")]
        public async Task<bool> UpdateLevel3Ticket() {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

            string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
            AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
            string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
            SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);
            return _publisherSupportTicketDS.UpdatePublisherSupportTicket(supportTicketDTO, documentModel, httpRequest);
        }

        #endregion Update Ticket Publisher

        #region Delete

        [HttpDelete]
        [Route("level3/{supportId}")]
        public async Task DeleteLevel3Ticket(Guid supportId) {
            _publisherSupportTicketDS.Delete(supportId);
        }

        #endregion Delete

        #region Get

        [HttpGet]
        [Route("level3/ticket/list/{onlyDeleted}")]
        public async Task<List<SupportTicketDTO>> GetLevel3TicketList(bool onlyDeleted) {
            return await _publisherSupportTicketDS.GetSupportTicketAssignedToLevel3List(onlyDeleted);
        }


        [HttpGet]
        [Route("ticket/{supportTicketId}")]
        public async Task<SupportTicketDetailDTO> GetSupportTicketDetail(Guid supportTicketId) {
            return await _publisherSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);
        }

        [HttpGet]
        [Route("ticket/level3/{supportTicketId}")]
        public async Task<SupportTicketDetailDTO> GetLevel3SupportTicketDetail(Guid supportTicketId) {
            return await _publisherSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);
        }

        [HttpPut]
        [Route("level3/myticket/list")]
        public async Task<List<SupportMyTicketDTO>> GetLevel3MyTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO) {
            return await _publisherSupportTicketDS.GetPublisherMyTicketList(supportMyTicketListRequestDTO.TenantId, supportMyTicketListRequestDTO.CreatorId, supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.AppKey);
        }
        #endregion Get



    }
}