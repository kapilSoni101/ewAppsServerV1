﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers.Business {

    /// <summary>
    /// This controller contains methods to execute <see cref="BADelivery"/> entity related operations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CustBADeliveryController:ControllerBase {


    #region Member Variables

    ICustBADeliveryDS _busBADeliveryDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBADeliveryController"/> class.
        /// </summary>
        public CustBADeliveryController(ICustBADeliveryDS busBADeliveryDS) {
            _busBADeliveryDS = busBADeliveryDS;
        }

        #endregion

        #region Business

        #region Get Methods

        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<CustBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId,[FromBody]ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBADeliveryDS.GetDeliveryListByBusinessTenantIdAsyncForCust(businessTenantId,listDateFilterDTO, cancellationToken);
        }

        ///// <summary>
        ///// Gets the delivery item name list by delivery id.
        ///// </summary>
        ///// <param name="deliveryId">The delivery to find delivery items.</param>
        ///// <param name="cancellationToken">The cancellation token.</param>
        ///// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        //[HttpGet]
        //[Route("items/list/{deliveryId}")]
        //public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
        //    return await _busBADeliveryDS.GetDeliveryItemNameListByDeliveryIdAsyncForCust(deliveryId, cancellationToken);
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

        #endregion
        #endregion

    }
}