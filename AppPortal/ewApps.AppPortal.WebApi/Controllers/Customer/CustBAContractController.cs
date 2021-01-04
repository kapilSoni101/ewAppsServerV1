using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DS;
using ewApps.AppPortal.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi.Controllers.Business {
    [Route("api/[controller]")]
    [ApiController]
    public class CustBAContractController:ControllerBase {

    ICustBAContractDS _busBAContractDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAContractController"/> class.
        /// </summary>
        /// <param name="busBAContractDS">The bus ba contract ds.</param>
        public CustBAContractController(ICustBAContractDS busBAContractDS) {
            _busBAContractDS = busBAContractDS;
        }

        /// <summary>
        /// Gets the contract list by business tenant id.
        /// </summary>
        /// <param name="businessTenantId">The business tenant id to filter contract list.</param>
        /// <param name="listDateFilterDTO">The DTO to filter contract list.</param>
        /// <param name="cancellationToken">The token to cancel async operation.</param>
        /// <returns>Returns contract list that matches given tenant id.</returns>
        [HttpPut]
        [Route("list/{businessTenantId}")]
        public async Task<IEnumerable<CustBAContractDTO>> GetContractListByBusinessIdAsyncForCust([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAContractDS.GetContractListByBusinessIdAsyncForCust(businessTenantId, listDateFilterDTO, cancellationToken);
        }


        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant  and contract id.</returns>
        [HttpGet]
        [Route("view/{businessTenantId}/{contractId}")]
        public async Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust([FromRoute]Guid businessTenantId, [FromRoute]Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _busBAContractDS.GetContractDetailByContractIdAsyncForCust(businessTenantId, contractId, cancellationToken);
        }
    }
}