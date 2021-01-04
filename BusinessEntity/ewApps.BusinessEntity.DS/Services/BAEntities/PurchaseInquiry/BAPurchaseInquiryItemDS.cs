using System;
using ewApps.BusinessEntity.Data;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This class manages the CRUD operations related methods and business logics for PurchaseInquiryItem entity.
    /// </summary>
    public class BAPurchaseInquiryItemDS : BaseDS<BAPurchaseInquiryItem>, IBAPurchaseInquiryItemDS {

    #region Local Variables

    IBAPurchaseInquiryItemRepository _purchaseInquiryItemRepo;
     
    #endregion Local Variables

    #region Constructor

    /// <summary>
    /// Public constructor for PurchaseInquiryItemDS class.
    /// </summary>
    /// <param name="purchaseInquiryItemRepo">Repository class dependancy for enquiry item.</param>
    public BAPurchaseInquiryItemDS(IBAPurchaseInquiryItemRepository purchaseInquiryItemRepo) : base(purchaseInquiryItemRepo) {
      _purchaseInquiryItemRepo = purchaseInquiryItemRepo;
    }

    #endregion Constructor

  }
}



