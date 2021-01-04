/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s:Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */


using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;


namespace ewApps.BusinessEntity.Data {
  /// <summary>
  /// This class contains methods to perform all database operations related to Invoice item and related information (like Data Transfer Object).
  /// </summary>
  public class BAPurchaseInquiryItemRepository:BaseRepository<BAPurchaseInquiryItem, BusinessEntityDbContext>, IBAPurchaseInquiryItemRepository  {

    #region Constructor 

    /// <summary>
    /// Initializes a new instance of the <see cref="BAARInvoiceItemRepository"/> class.
    /// </summary>
    /// <param name="context">Instance of PaymentDBContext to executes database operations.</param>
    /// <param name="sessionManager">User session manager instance to get login user details.</param>
    public BAPurchaseInquiryItemRepository(BusinessEntityDbContext context, IUserSessionManager sessionManager) : base(context, sessionManager) {
    }

    #endregion

  }
}
