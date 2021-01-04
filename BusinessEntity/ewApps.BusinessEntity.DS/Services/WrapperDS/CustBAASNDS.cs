using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;

namespace ewApps.BusinessEntity.DS {
    public class CustBAASNDS:ICustBAASNDS {

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
        public CustBAASNDS(IBAASNDS bAASNDS, IBAASNItemDS bAASNItemDS, IBAASNAttachmentDS bAASNAttachmentDS) {
            _bAASNDS = bAASNDS;
            _bAASNItemDS = bAASNItemDS;
            _bAASNAttachmentDS = bAASNAttachmentDS;
        }

        #endregion

        #region Get Methods

        /// <inhritdoc/>
        public async Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _bAASNDS.GetASNListByBusinessTenantIdAsyncForCust(businessPartnerTenantId, listDateFilterDTO, cancellationToken);
        }

        /// <inhritdoc/>
        public async Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            CustBAASNViewDTO custBAASNViewDTO = new CustBAASNViewDTO();
            custBAASNViewDTO = await _bAASNDS.GetASNDetailByASNIdAsyncForCust(asnId, cancellationToken);

            if(custBAASNViewDTO != null) {
                custBAASNViewDTO.ItemList =  _bAASNItemDS.GetASNItemListByASNIdForCust(asnId);
                custBAASNViewDTO.AttachmentList = await _bAASNAttachmentDS.GetASNAttachmentListByASNIdAsyncForCust(asnId);
            }
            return custBAASNViewDTO;
        }

        #endregion
    }
}
