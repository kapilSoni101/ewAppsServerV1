/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 31 August 2019
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {


    /// <summary>
    /// This controller contains methods to execute <see cref="BASalesQuotation"/> entity related operations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class BusBASalesQuotationController:ControllerBase {

        #region Member Variables

        IBusBASalesQuotationDS _busBASalesQuotationDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBASalesQuotationController"/> class.
        /// </summary>
        public BusBASalesQuotationController(IBusBASalesQuotationDS busBASalesQuotationDS) {
            _busBASalesQuotationDS = busBASalesQuotationDS;
        }

        #endregion

        #region Business

        #region Get Methods

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="listDateFilterDTO">The date filter values to filter sales quatation data on Document Date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<BusBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBASalesQuotationDS.GetSalesQuotationListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }



        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="deliveryId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpGet]
        [Route("view/{salesquotationId}")]
        public async Task<BusBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsync([FromRoute]Guid salesquotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBASalesQuotationDS.GetSalesQuotationDetailBySalesQuotationIdAsync(salesquotationId, cancellationToken);
        }



        #endregion
        #endregion

    }
}