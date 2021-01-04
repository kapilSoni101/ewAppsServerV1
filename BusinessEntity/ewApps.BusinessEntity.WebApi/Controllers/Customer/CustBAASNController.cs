using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustBAASNController : ControllerBase
    {
        ICustBAASNDS _custBAASNDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustBAASNController"/> class.
        /// </summary>
        public CustBAASNController(ICustBAASNDS custBAASNDS) {
           _custBAASNDS = custBAASNDS;
        }

    /// <summary>
    /// Gets the asn list by business tenant id and provided filter.
    /// </summary>
    /// <param name="businessPartnerTenantId">The business tenant id.</param>
    /// <param name="listDateFilterDTO">The list date filter dto.</param>
    /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
    /// <returns>Returns list of ASN list that matches provided </returns>
    [HttpPut]
    [Route("list/{businessPartnerTenantId}")]
    public async Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust([FromRoute]Guid businessPartnerTenantId, [FromBody]ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _custBAASNDS.GetASNListByBusinessTenantIdAsyncForCust(businessPartnerTenantId, listDateFilterDTO, cancellationToken);
    }

    /// <summary>
    /// Gets the ASN detail by ASN identifier.
    /// </summary>
    /// <param name="asnId">The asn identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
    [HttpGet]
    [Route("view/{asnId}")]
    public async Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust([FromRoute]Guid asnId, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _custBAASNDS.GetASNDetailByASNIdAsyncForCust(asnId, cancellationToken);
    }

  }
}