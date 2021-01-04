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
    public class CustBADeliveryController:ControllerBase {

        #region Local Members

        ICustBADeliveryDS _busBADeliveryDS;

        #endregion Local Members

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBADeliveryController"/> class.
        /// </summary>
        /// <param name="busBADeliveryDS">The bus ba delivery ds.</param>
        public CustBADeliveryController(ICustBADeliveryDS busBADeliveryDS) {
            _busBADeliveryDS = busBADeliveryDS;
        }

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<CustBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsyncForCust([FromRoute]Guid businessTenantId, [FromBody]ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            return await _busBADeliveryDS.GetDeliveryListByBusinessTenantIdAsyncForCust(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        //[HttpGet]
        //[Route("items/list/{deliveryId}")]
        //public async Task<IEnumerable<String>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
        //    return await _busBADeliveryDS.GetDeliveryItemNameListByDeliveryIdAsync(deliveryId, cancellationToken);
        //}

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="deliveryId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpGet]
        [Route("view/{businessTenantId}/{deliveryId}")]
        public async Task<CustBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsyncForCust([FromRoute]Guid businessTenantId, [FromRoute]Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBADeliveryDS.GetDeliveryDetailByDeliveryIdAsyncForCust(businessTenantId, deliveryId, cancellationToken);
        }


    }
}