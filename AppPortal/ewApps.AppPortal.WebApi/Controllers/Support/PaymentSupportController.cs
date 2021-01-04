using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.DMService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{

  // This class implements Get, Add And Update Method for businesss and customer ticket.
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentSupportController : ControllerBase
  {

    #region Local Member

    IPaymentSupportTicketDSNew _paymentSupportTicketDS;
    private static IHttpContextAccessor _contextAccessor;

    #endregion Local Member

    #region Constructor

    public PaymentSupportController(IPaymentSupportTicketDSNew paymentSupportTicketDS, IHttpContextAccessor contextAccessor)
    {
      _contextAccessor = contextAccessor;
      _paymentSupportTicketDS = paymentSupportTicketDS;
    }

    #endregion Constructor

    #region Level1(Customer) Ticket Api

    //[HttpPost]
    [HttpPost, DisableRequestSizeLimit]
    [Route("level1")]
    public async Task<SupportTicket> AddCustomerTicket()
    {
      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

      return _paymentSupportTicketDS.AddCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
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
      return _paymentSupportTicketDS.UpdateCustomerSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }

    [HttpDelete]
    [Route("level1/{supportId}")]
    public async Task DeleteLevel1SupportTicket(Guid supportId)
    {
      _paymentSupportTicketDS.Delete(supportId);
    }


    [HttpPut]
    [Route("level1/myticket/list")]
    public async Task<List<SupportMyTicketDTO>> GetCustomerMyTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _paymentSupportTicketDS.GetCustomerMyTicketList(supportMyTicketListRequestDTO.TenantId, supportMyTicketListRequestDTO.CreatorId, supportMyTicketListRequestDTO.CustomerId, supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.AppKey);
    }

    [HttpGet]
    [Route("level1/ticket/{supportTicketId}")]
    public async Task<SupportTicketDetailDTO> GetLevel1PartnerSupportTicketDetailById(Guid supportTicketId)
    {
      return await _paymentSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);
    }

    #endregion


    #region Level2(Business) Ticket Api

    [HttpPost]
    [Route("level2")]
    public async Task<SupportTicket> AddBusinessSupportTicket()
    {

      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);

      return _paymentSupportTicketDS.AddBusinessSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }


    [HttpPut]
    [Route("level2")]
    public async Task<bool> UpdateBusinessSupportTicket()
    {

      HttpRequest httpRequest = _contextAccessor.HttpContext.Request;

      string request = HttpContext.Request.Headers["AddUpdateDocumentModel"];
      AddUpdateDocumentModel documentModel = Newtonsoft.Json.JsonConvert.DeserializeObject<AddUpdateDocumentModel>(request);
      string supportRequest = HttpContext.Request.Headers["SupportAddUpdateDTO"];
      SupportAddUpdateDTO supportTicketDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SupportAddUpdateDTO>(supportRequest);
      return _paymentSupportTicketDS.UpdateBusinessSupportTicket(supportTicketDTO, documentModel, httpRequest);
    }

    [HttpDelete]
    [Route("level2/{supportId}")]
    public async Task DeleteBusinessTicket(Guid supportId)
    {
      _paymentSupportTicketDS.Delete(supportId);
    }


    [HttpPut]
    [Route("level2/myticket/list")]
    public async Task<List<SupportMyTicketDTO>> GetLevel2MyTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _paymentSupportTicketDS.GetBusinessMyTicketList(supportMyTicketListRequestDTO.TenantId, supportMyTicketListRequestDTO.CreatorId, supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.AppKey);
    }

    [HttpPut]
    [Route("level2/ticket/list")]
    public async Task<List<SupportTicketDTO>> GetLevel2TicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _paymentSupportTicketDS.GetSupportTicketAssignedToLevel2List(supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.AppKey);
    }


    [HttpGet]
    [Route("level2/ticket/{supportTicketId}")]
    public async Task<SupportTicketDetailDTO> GetPaymentBusinessSupportTicketDetail(Guid supportTicketId)
    {
      return await _paymentSupportTicketDS.GetSupportTicketDetailById(supportTicketId, true);

    }


    // Get All business ticket in businessSetup 
    [HttpPut]
    [Route("level2/allbusinessticket/list")]
    public async Task<List<SupportTicketDTO>> GetLevel2AllBusinessTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _paymentSupportTicketDS.GetSupportTicketAssignedToLevel2BusinessList(supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.GenerationLevel);
    }

    // Get All Customer ticket in businessSetup 
    [HttpPut]
    [Route("level2/allcustomerticket/list")]
    public async Task<List<SupportTicketDTO>> GetLevel2AllCustomerTicketList([FromBody] SupportMyTicketListRequestDTO supportMyTicketListRequestDTO)
    {
      return await _paymentSupportTicketDS.GetSupportTicketAssignedToLevel2CustomerList(supportMyTicketListRequestDTO.OnlyDeleted, supportMyTicketListRequestDTO.GenerationLevel);
    }

    #endregion

  }
}