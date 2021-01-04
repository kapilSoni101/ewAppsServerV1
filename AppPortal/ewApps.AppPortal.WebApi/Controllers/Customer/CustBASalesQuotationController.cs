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
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {


    /// <summary>
    /// This controller contains methods to execute <see cref="BASalesQuotation"/> entity related operations.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class CustBASalesQuotationController : ControllerBase
    {

        #region Member Variables

        ICustBASalesQuotationDS _custBASalesQuotationDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBASalesQuotationController"/> class.
        /// </summary>
        public CustBASalesQuotationController(ICustBASalesQuotationDS custBASalesQuotationDS) {
            _custBASalesQuotationDS = custBASalesQuotationDS;
        }

        #endregion

        #region Business

        #region Get Methods

        /// <summary>
        /// Gets the SalesQuotation list by business tenant identifier.
        /// </summary>
        /// <param name="businessPartnerTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpPut]
        [Route("list/{businessPartnerTenantId}")]
        public async Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByPartnerTenantIdAsyncForCust([FromRoute]Guid businessPartnerTenantId,[FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _custBASalesQuotationDS.GetSalesQuotationListByPartnerTenantIdAsyncForCust(businessPartnerTenantId, listDateFilterDTO, cancellationToken);       }



        /// <summary>
        /// Gets the delivery list by business tenant identifier.
        /// </summary>
        /// <param name="salesquotationId">The sales quotation id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of <see cref="BusBADeliveryDTO"/> that matches provided business tenant id.</returns>
        [HttpGet]
        [Route("{salesquotationId}")]
        public async Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust([FromRoute]Guid salesquotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _custBASalesQuotationDS.GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(salesquotationId, cancellationToken);
        }



    #endregion
    [HttpPost]
    [Route("salesquotationwithitem")]
    public async Task AddSalesQuotationWithItem([FromBody] CustBASalesQuotationAddDTO dto, CancellationToken cancellationToken = default(CancellationToken))
    {
      await _custBASalesQuotationDS.AddSalesQuotationWithItemAsync(dto, cancellationToken);
    }
    #endregion

  }
}