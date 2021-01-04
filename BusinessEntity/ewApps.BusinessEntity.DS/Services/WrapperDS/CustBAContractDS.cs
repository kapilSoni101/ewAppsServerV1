using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
    /// <summary>
    /// This class defines contract entity operations for business tenant.
    /// </summary>
    /// <seealso cref="ewApps.BusinessEntity.DS.IBusBAContractDS" />
    public class CustBAContractDS:ICustBAContractDS {

        private IBAContractDS _bAContractDS;
        private IBAContractItemDS _bAContractItemDS;
        IBAContractAttachmentDS _bAContractAttachmentDS;
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAContractDS"/> class.
        /// </summary>
        /// <param name="bAContractDS">The contract data service.</param>
        /// <param name="bAContractItemDS">The contract item data service instance.</param>
        public CustBAContractDS(IBAContractDS bAContractDS, IBAContractItemDS bAContractItemDS, IBAContractAttachmentDS bAContractAttachmentDS) {
            _bAContractDS = bAContractDS;
            _bAContractItemDS = bAContractItemDS;
            _bAContractAttachmentDS = bAContractAttachmentDS;
        }



        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAContractDTO>> GetContractListByBusinessIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAContractDS.GetContractListByTenantIdAsyncForCust(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            CustBAContractViewDTO custBAContractViewDTO = new CustBAContractViewDTO();

            custBAContractViewDTO = await _bAContractDS.GetContractDetailByContractIdAsyncForCust(businessTenantId, contractId, cancellationToken);

            if(custBAContractViewDTO != null) {
                custBAContractViewDTO.ContractItemList = (await _bAContractItemDS.GetContractItemListByContractIdAsyncForCust(businessTenantId, contractId, cancellationToken)).ToList();
                custBAContractViewDTO.AttachmentList = (await _bAContractAttachmentDS.GetContractAttachmentListByContractIdForCustAsync(contractId, cancellationToken)).ToList();
            }

            return custBAContractViewDTO;
        }

        #endregion

    }
}
