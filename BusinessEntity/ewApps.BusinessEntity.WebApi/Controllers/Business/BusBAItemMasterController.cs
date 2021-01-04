using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class BusBAItemMasterController:ControllerBase {

        #region Local Members

        IBAItemMasterDS _itemMasterDS;
        IBusBAItemMasterDS _busItemMasterDS;
        private static IHttpContextAccessor _httpContextAccessor;

        #endregion Local Members

        #region Constructor 

        public BusBAItemMasterController(IBAItemMasterDS itemMasterDS, IBusBAItemMasterDS busItemMasterDS, IHttpContextAccessor httpContextAccessor) {
            _itemMasterDS = itemMasterDS;
            _busItemMasterDS = busItemMasterDS;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion Constructor

        #region Customer Get

        [HttpGet]
        [Route("list/{tenantId}")]
        public async Task<IEnumerable<BusBAItemMasterDTO>> GetItemMasterListByBusinessTenantIdAsync([FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterListByBusTenantIdAsync(tenantId, token);
        }

        [HttpGet]
        [Route("view/{tenantId}/{itemId}")]
        public async Task<BusBAItemMasterViewDTO> GetItemMasterByBusinessTenantIdAndItemIdAsync([FromRoute] Guid tenantId, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterByBusTenantIdAndItemIdAsync(tenantId, itemId, token);
        }

        #endregion Customer Get

        #region Vendor Get


        [HttpGet]
        [Route("vendors/list/{tenantId}")]
        public async Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusinessTenantIdAsync([FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetVendorsItemMasterListByBusTenantIdAsync(tenantId, token);
        }

        [HttpGet]
        [Route("vendors/view/{tenantId}/{itemId}")]
        public async Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusinessTenantIdAndItemIdAsync([FromRoute] Guid tenantId, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetVendorsItemMasterByBusTenantIdAndItemIdAsync(tenantId, itemId, token);
        }

        #endregion Vendor Get

        #region Add Item Master

        [HttpPost("additemmaster")]
        public async Task<ResponseModelDTO> AddItemMasterByBusinessAsync(CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _httpContextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["AddUpdateItemMasterModel"];
            return await _busItemMasterDS.AddItemMasterByBusinessAsync(httpRequest, request, token);
        }


        [HttpPost("add")]
        public async Task<ResponseModelDTO> AddItemMasterByBusinessPayAppWithoutAttchAsync([FromBody] BusBAItemMasterViewDTO request, CancellationToken token = default(CancellationToken)) {            
            return await _busItemMasterDS.AddItemMasterByBusinessPayAppWithoutAttchAsync(request, token);
        }

        #endregion Add item Master

        #region Update item master

        [HttpPut("Updateitemmaster/{itemId}")]
        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAsync([FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _httpContextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["UpdateItemMasterModel"];
            return await _busItemMasterDS.UpdateItemMasterByBusinessPaymentAppWithAttchAsync(httpRequest, request, itemId, token);
        }


        [HttpPut("Update/{itemId}")]
        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync([FromBody] BusBAItemMasterViewDTO request, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.UpdateItemMasterByBusinessPaymentAppWithoutAttchAppAsync(request, itemId, token);
        }
        
        #endregion Update item master

    }
}