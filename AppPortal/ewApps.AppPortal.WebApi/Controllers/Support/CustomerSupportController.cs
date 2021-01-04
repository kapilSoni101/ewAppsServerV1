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
    public class CustomerSupportController:ControllerBase {

        ICustomerSupportTicketDSNew _customerSupportTicketDS;
        private static IHttpContextAccessor _contextAccessor;

        public CustomerSupportController(ICustomerSupportTicketDSNew customerSupportTicketDS, IHttpContextAccessor contextAccessor) {
            _contextAccessor = contextAccessor;
            _customerSupportTicketDS = customerSupportTicketDS;
        }

        #region Level1(Customer) Ticket Api
/*
        //[HttpPost]
        [HttpPost, DisableRequestSizeLimit]
        [Route("level1")]
        public async Task<SupportTicket> AddCustomerTicket() {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

            string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
            AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
            string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
            SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

            return _customerSupportTicketDS.AddCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
        }

    //[HttpPut]
    //[Route("level1")]
    //public async Task<bool> UpdateCustomerSupportTicket(SupportAddUpdateDTO supportTicketDTO) {
    //    return _customerSupportTicketDS.UpdateCustomerSupportTicket(supportTicketDTO);
    //}
    [HttpPut]
    [Route("level1")]
    public async Task<bool> UpdateCustomerSupportTicket()
    {
      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);
      return _customerSupportTicketDS.UpdateCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }

    [HttpDelete]
        [Route("level1/{supportId}")]
        public async Task DeleteLevel1SupportTicket(Guid supportId) {
            _customerSupportTicketDS.Delete(supportId);
        }


        [HttpPut]
        [Route("level1/myticket/list")]
        public async Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO) {
            return await _customerSupportTicketDS.GetCustomerMyTicketList(supportMyTicketListRequestDTO.TenantId, supportMyTicketListRequestDTO.CreatorId, supportMyTicketListRequestDTO.CustomerId, supportMyTicketListRequestDTO.OnlyDeleted);
        }

        [HttpGet]
        [Route("level1/ticket/{supportTicketId}")]
        public async Task<SupportTicketDetailDTO> GetLevel1PartnerSupportTicketDetailById(Guid supportTicketId) {
            return await _customerSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);
        }

   
*/



    [HttpPost, DisableRequestSizeLimit]
    [Route("level1")]
    public async Task<SupportTicket> AddCustomerTicket()
    {
      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

      return _customerSupportTicketDS.AddCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }


    [HttpPut]
    [Route("level1")]
    public async Task<bool> UpdateCustomerSupportTicket()
    {
      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);
      return _customerSupportTicketDS.UpdateCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }

    [HttpDelete]
    [Route("level1/{supportId}")]
    public async Task DeleteLevel1SupportTicket(Guid supportId)
    {
      _customerSupportTicketDS.Delete(supportId);
    }


    [HttpPut]
    [Route("level1/myticket/list")]
    public async Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _customerSupportTicketDS.GetCustomerMyTicketList(supportMyTicketListRequestDTO.TenantId, supportMyTicketListRequestDTO.CreatorId, supportMyTicketListRequestDTO.CustomerId, supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.AppKey);
    }

    [HttpGet]
    [Route("level1/ticket/{supportTicketId}")]
    public async Task<SupportTicketDetailDTO> GetLevel1PartnerSupportTicketDetailById(Guid supportTicketId)
    {
      return await _customerSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);
    }
    #endregion




  }
}