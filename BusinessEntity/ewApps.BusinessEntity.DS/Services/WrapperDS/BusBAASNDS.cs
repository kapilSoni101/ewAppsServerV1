using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
    public class BusBAASNDS:IBusBAASNDS {

        #region Member Variables

        private IBAASNDS _bAASNDS;
        private IBAASNItemDS _bAASNItemDS;
        private IBAASNAttachmentDS _bAASNAttachmentDS;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAASNDS"/> class.
        /// </summary>
        /// <param name="bAASNDS">The ASN data service.</param>
        /// <param name="bAASNItemDS">The ASN item data service.</param>
        public BusBAASNDS(IBAASNDS bAASNDS, IBAASNItemDS bAASNItemDS, IBAASNAttachmentDS bAASNAttachmentDS) {
            _bAASNDS = bAASNDS;
            _bAASNItemDS = bAASNItemDS;
            _bAASNAttachmentDS = bAASNAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inhritdoc/>
        public async Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAASNDS.GetASNListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, ASNTypeConstants.CustomerASNType, cancellationToken);
        }

        /// <inhritdoc/>
        public async Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBAASNViewDTO busBAASNViewDTO = new BusBAASNViewDTO();
            busBAASNViewDTO = await _bAASNDS.GetASNDetailByASNIdAsync(asnId, ASNTypeConstants.CustomerASNType, cancellationToken);
            if(busBAASNViewDTO != null) {
                busBAASNViewDTO.ItemList = _bAASNItemDS.GetASNItemListByASNId(asnId);
                busBAASNViewDTO.AttachmentList = (await _bAASNAttachmentDS.GetASNAttachmentListByASNIdAsync(asnId, cancellationToken)).ToList();
            }
            return busBAASNViewDTO;
        }

        #endregion


        #region Vendor Methods

        public async Task<IEnumerable<BusBAASNDTO>> GetVendorASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAASNDS.GetASNListByBusinessTenantIdAsync(businessTenantId, listDateFilterDTO, ASNTypeConstants.VendorASNType, cancellationToken);
        }

        public async Task<BusBAASNViewDTO> GetVendorASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusBAASNViewDTO busBAASNViewDTO = new BusBAASNViewDTO();
            busBAASNViewDTO = await _bAASNDS.GetASNDetailByASNIdAsync(asnId, ASNTypeConstants.VendorASNType, cancellationToken);
            if(busBAASNViewDTO != null) {
                busBAASNViewDTO.ItemList = _bAASNItemDS.GetASNItemListByASNId(asnId);
                busBAASNViewDTO.AttachmentList = (await _bAASNAttachmentDS.GetASNAttachmentListByASNIdAsync(asnId, cancellationToken)).ToList();
            }
            return busBAASNViewDTO;
        }


        #endregion


    }
}
