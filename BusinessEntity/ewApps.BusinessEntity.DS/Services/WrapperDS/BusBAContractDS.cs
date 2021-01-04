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
    public class BusBAContractDS:IBusBAContractDS {

        private IBAContractDS _bAContractDS;
        private IBAContractItemDS _bAContractItemDS;
        private IBAContractAttachmentDS _bAContractAttachmentDS;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAContractDS"/> class.
        /// </summary>
        /// <param name="bAContractDS">The contract data service.</param>
        /// <param name="bAContractItemDS">The contract item data service instance.</param>
        public BusBAContractDS(IBAContractDS bAContractDS, IBAContractItemDS bAContractItemDS, IBAContractAttachmentDS bAContractAttachmentDS) {
            _bAContractDS = bAContractDS;
            _bAContractItemDS = bAContractItemDS;
            _bAContractAttachmentDS = bAContractAttachmentDS;
        }



        #endregion

        #region Get Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAContractDTO>> GetContractListByBusinessIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAContractDS.GetContractListByTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BusBAContractViewDTO> GetContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBAContractViewDTO busBAContractViewDTO = new BusBAContractViewDTO();

            busBAContractViewDTO = await _bAContractDS.GetContractDetailByContractIdAsync(businessTenantId, contractId, cancellationToken);

            if(busBAContractViewDTO != null) {
                busBAContractViewDTO.ContractItemList = (await _bAContractItemDS.GetContractItemListByContractIdAsync(businessTenantId, contractId, cancellationToken)).ToList();
                busBAContractViewDTO.AttachmentList = (await _bAContractAttachmentDS.GetContractAttachmentListByContractIdAsync(contractId, cancellationToken)).ToList();
            }

            return busBAContractViewDTO;
        }

        #endregion

        //#region Vendor

        ///// <inheritdoc/>
        //public async Task<IEnumerable<BusBAContractDTO>> GetVendorContractListByBusinessIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
        //    // ToDo: Add filter in below method to identify b/w Vendor and Customer.
        //    return await _bAContractDS.GetContractListByTenantIdAsync(businessTenantId, listDateFilterDTO, cancellationToken);
        //}

        ///// <inheritdoc/>
        //public async Task<BusBAContractViewDTO> GetVendorContractDetailByContractIdAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
        //    BusBAContractViewDTO busBAContractViewDTO = new BusBAContractViewDTO();

        //    // ToDo: Add filter in below method to identify b/w Vendor and Customer.
        //    busBAContractViewDTO = await _bAContractDS.GetContractDetailByContractIdAsync(businessTenantId, contractId, cancellationToken);

        //    if(busBAContractViewDTO != null) {
        //        busBAContractViewDTO.ContractItemList = (await _bAContractItemDS.GetContractItemListByContractIdAsync(businessTenantId, contractId, cancellationToken)).ToList();
        //        busBAContractViewDTO.AttachmentList = (await _bAContractAttachmentDS.GetContractAttachmentListByContractIdAsync(contractId, cancellationToken)).ToList();
        //    }

        //    return busBAContractViewDTO;
        //}

        //#endregion

    }
}
