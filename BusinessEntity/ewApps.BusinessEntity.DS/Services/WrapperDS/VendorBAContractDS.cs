using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
    public class VendorBAContractDS:IVendorBAContractDS {

        private IBAVendorContractDS _bAContractDS;
        private IBAContractItemDS _bAContractItemDS;
        private IBAContractAttachmentDS _bAContractAttachmentDS;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAContractDS"/> class.
        /// </summary>
        /// <param name="bAContractDS">The contract data service.</param>
        /// <param name="bAContractItemDS">The contract item data service instance.</param>
        public VendorBAContractDS(IBAVendorContractDS bAContractDS, IBAContractItemDS bAContractItemDS, IBAContractAttachmentDS bAContractAttachmentDS) {
            _bAContractDS = bAContractDS;
            _bAContractItemDS = bAContractItemDS;
            _bAContractAttachmentDS = bAContractAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<VendorBAContractDTO>> GetContractListByBusinessIdAsyncForVendorAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAContractDS.GetContractListByTenantIdAsyncForVendor(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<VendorBAContractViewDTO> GetContractDetailByContractIdForVendorAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            VendorBAContractViewDTO custBAContractViewDTO = new VendorBAContractViewDTO();

            custBAContractViewDTO = await _bAContractDS.GetContractDetailByContractIdForVendorAsync(businessTenantId, contractId, cancellationToken);

            if(custBAContractViewDTO != null) {
                custBAContractViewDTO.ContractItemList = (await _bAContractItemDS.GetContractItemListByContractIdAsyncForCust(businessTenantId, contractId, cancellationToken)).ToList();
                custBAContractViewDTO.AttachmentList = (await _bAContractAttachmentDS.GetContractAttachmentListByContractIdForCustAsync(contractId, cancellationToken)).ToList();
            }

            return custBAContractViewDTO;
        }

        #endregion
    }
}
