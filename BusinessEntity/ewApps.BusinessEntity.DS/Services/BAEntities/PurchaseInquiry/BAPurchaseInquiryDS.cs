/* Copyright © 2018 eWorkplace Apps(https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 *
 * Author: Anil Nigam<anigam @eworkplaceapps.com>
 * Date:08 july 2019
 * 
 */

using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// Contains purchase inquiry required methods to add/update/delete operation.
    /// Also contains method for getting purchase inquiry.
    /// </summary>
    public class BAPurchaseInquiryDS:BaseDS<BAPurchaseInquiry>, IBAPurchaseInquiryDS {

        #region Local variables
        IBAPurchaseInquiryRepository _purchaseInquiryRepo;
        #endregion Local variables

        #region Constructor

        public BAPurchaseInquiryDS(IBAPurchaseInquiryRepository purchaseInquiryRepo) : base(purchaseInquiryRepo) {
            _purchaseInquiryRepo = purchaseInquiryRepo;
        }
        #endregion Constructor
    }
}

