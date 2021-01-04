using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class BusBAItemMasterController:ControllerBase {

        #region Local Members

        IBusBAItemMasterDS _busItemMasterDS;
        private static IHttpContextAccessor _contextAccessor;

        #endregion Local Members

        #region Constructor 

        public BusBAItemMasterController(IBusBAItemMasterDS busItemMasterDS, IHttpContextAccessor contextAccessor) {
            _busItemMasterDS = busItemMasterDS;
            _contextAccessor = contextAccessor;
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
            return await _busItemMasterDS.GetItemMasterByBusTenantIdAndItemidAsync(tenantId, itemId, token);
        }

        #endregion Customer Get

        #region Vendor Get

        [HttpGet]
        [Route("vendor/list/{tenantId}")]
        public async Task<IEnumerable<BusBAItemMasterDTO>> GetVendorsItemMasterListByBusinessTenantIdAsync([FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterListByBusTenantIdAsync(tenantId, token);
        }


        [HttpGet]
        [Route("vendor/view/{tenantId}/{itemId}")]
        public async Task<BusBAItemMasterViewDTO> GetVendorsItemMasterByBusinessTenantIdAndItemIdAsync([FromRoute] Guid tenantId, [FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            return await _busItemMasterDS.GetItemMasterByBusTenantIdAndItemidAsync(tenantId, itemId, token);
        }

        #endregion Vendor Get

        #region Add item Master

        /// <summary>
        /// Add Item master business portal payment app
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("additemmaster")]
        public async Task<ResponseModelDTO> AddItemMasterByBusinessAsync(CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["AddUpdateItemMasterModel"];
            return await _busItemMasterDS.AddItemMasterByBusinessAsync(httpRequest, request, token);
        }

        #endregion Add


        #region Update item master

        /// <summary>
        /// Update Item Master
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("updateitemmaster/{itemId}")]
        public async Task<ResponseModelDTO> UpdateItemMasterByBusinessPaymentAppWithAttchAppAsync([FromRoute] Guid itemId, CancellationToken token = default(CancellationToken)) {
            HttpRequest httpRequest = _contextAccessor.HttpContext.Request;
            string request = HttpContext.Request.Headers["UpdateItemMasterModel"];
            return await _busItemMasterDS.UpdateItemMasterByBusinessPaymentAppAsync(httpRequest, request, itemId, token);
        }

        #endregion Update item master
    }
}