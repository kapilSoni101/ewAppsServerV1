using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusBAASNController : ControllerBase
    {
        private IBusBAASNDS _busBAASNDS;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAASNController"/> class.
        /// </summary>
        /// <param name="busBAASNDS">The bus baasnds.</param>
        public BusBAASNController(IBusBAASNDS busBAASNDS) {
            _busBAASNDS = busBAASNDS;
        }

        #endregion


        #region Get Methods

        /// <summary>
        /// Gets the asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody]ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAASNDS.GetASNListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <summary>
        /// Gets the ASN detail by ASN identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        [HttpGet]
        [Route("view/{asnId}")]
        public async Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync([FromRoute]Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAASNDS.GetASNDetailByASNIdAsync(asnId, cancellationToken);
        }

        #endregion

        #region Vendor Methods


        /// <summary>
        /// Gets the asn list by business tenant id and provided filter.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id.</param>
        /// <param name="listDateFilterDTO">The list date filter dto.</param>
        /// <param name="cancellationToken">The cancellation token to cancel async task.</param>
        /// <returns>Returns list of ASN list that matches provided </returns>
        [HttpPut]
        [Route("vendor/list/{businessTenantId}")]
        public async Task<IEnumerable<BusBAASNDTO>> GetVendorASNListByBusinessTenantIdAsync([FromRoute]Guid businessTenantId, [FromBody]ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAASNDS.GetVendorASNListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <summary>
        /// Gets the ASN detail by ASN identifier.
        /// </summary>
        /// <param name="asnId">The asn identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns ASN detail with line items that matches provided asn id.</returns>
        [HttpGet]
        [Route("vendor/view/{asnId}")]
        public async Task<BusBAASNViewDTO> GetVendorASNDetailByASNIdAsync([FromRoute]Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAASNDS.GetVendorASNDetailByASNIdAsync(asnId, cancellationToken);
        }

        #endregion
    }
}