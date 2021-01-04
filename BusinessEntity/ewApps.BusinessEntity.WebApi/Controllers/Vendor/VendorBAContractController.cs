using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DS;
using ewApps.BusinessEntity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.BusinessEntity.WebApi {

    [Route("api/[controller]")]
    [ApiController]
    public class VendorBAContractController:ControllerBase {
        IVendorBAContractDS _vendorBAContractDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="VendorBAContractController"/> class.
        /// </summary>
        public VendorBAContractController(IVendorBAContractDS custBAContractDS)  {
            _vendorBAContractDS = custBAContractDS;
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
        public async Task<IEnumerable<VendorBAContractDTO>> GetContractListByBusinessIdAsyncForVendorAsync([FromRoute]Guid businessTenantId, [FromBody] ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _vendorBAContractDS.GetContractListByBusinessIdAsyncForVendorAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }


        /// <summary>
        /// Gets the contract detail by business tenant id and contract identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="contractId">The contract identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns contract detail that matches given business tenant and contract id.</returns>
        [HttpGet]
        [Route("view/{businessTenantId}/{contractId}")]
        public async Task<VendorBAContractViewDTO> GetContractDetailByContractIdAsyncForVendorAsync([FromRoute]Guid businessTenantId, [FromRoute]Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _vendorBAContractDS.GetContractDetailByContractIdForVendorAsync(businessTenantId, contractId, cancellationToken);
        }
    }
}