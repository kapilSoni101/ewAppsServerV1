using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.Common;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.UserSessionService;

namespace ewApps.BusinessEntity.DS
{
  /// <summary>
  /// This class implements <see cref="BASalesOrder"/> entity operations for business.
  /// </summary>
  /// <seealso cref="ewApps.BusinessEntity.DS.IBusBASalesOrderDS" />
  public class CustBASalesOrderDS : ICustBASalesOrderDS
  {

    #region Member Variables

    IBASalesOrderDS _bASODS;
    IBASalesOrderItemDS _bASalesOrderItemDS;
    IBASalesOrderAttachmentDS _bASalesOrderAttachmentDS;
    IUserSessionManager _userSessionManager;
    IAppSyncServiceDS _syncServiceDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BusBASalesOrderDS"/> class.
    /// </summary>
    /// <param name="bASODS">The sales order data service instance.</param>
    /// <param name="bASalesOrderItemDS">The sales order item data service instance.</param>
    public CustBASalesOrderDS(IAppSyncServiceDS syncServiceDS, IBASalesOrderDS bASODS, IBASalesOrderItemDS bASalesOrderItemDS, IBASalesOrderAttachmentDS bASalesOrderAttachmentDS, IUserSessionManager userSessionManager)
    {
      _bASODS = bASODS;
      _bASalesOrderItemDS = bASalesOrderItemDS;
      _bASalesOrderAttachmentDS = bASalesOrderAttachmentDS;
      _userSessionManager = userSessionManager;
      _syncServiceDS = syncServiceDS;
    }

    #endregion

    #region Get Methods

    /// <inheritdoc/>
    public async Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _bASODS.GetSalesOrderListByBusinessTenantIdAsyncForCust(businessTenantId, listDateFilterDTO, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken))
    {
      CustBASalesOrderViewDTO custBASalesOrderViewDTO = new CustBASalesOrderViewDTO();
      custBASalesOrderViewDTO = await _bASODS.GetSalesOrderDetailBySOIdAsyncForCust(businessTenantId, soId, cancellationToken);
      if (custBASalesOrderViewDTO != null)
      {
        custBASalesOrderViewDTO.SalesOrderItemList = await _bASalesOrderItemDS.GetSalesOrderItemListBySOIdAsync(soId, cancellationToken);
        custBASalesOrderViewDTO.AttachmentList = (await _bASalesOrderAttachmentDS.GetSalesOrderAttachmentListByIdForCustAsync(soId, cancellationToken)).ToList();
      }

      return custBASalesOrderViewDTO;
    }

    #endregion Get Methods 

    #region Add/Update Methods

    public async Task AddSalesOrderWithItem(CustBASalesOrderAddDTO addDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      //SalesOrder 
      UserSession session = _userSessionManager.GetSession();

      // Map entity to order 
      //await _bASODS.AddSalesOrderAsyncForCust(addDTO, session.TenantId, session.TenantUserId);

      // Now add in SAP.
      BASalesOrderSyncDTO syncDTO = BASalesOrderSyncDTO.MapFromCustBASalesORderAddDTO(addDTO);
      //async Task<bool> PushSalesOrderDataInERPAsync(BASalesOrderSyncDTO request, Guid tenantId, CancellationToken token = default(CancellationToken))
      await _syncServiceDS.PushSalesOrderDataInERPAsync(syncDTO, session.TenantId);

      //Pull Sales order from ERP
      // Preparing api calling process model.   
      PullERPDataReqDTO request = new PullERPDataReqDTO();
      request.SelectedBAEntityTypes = new List<int>() { (int)BAEntityEnum.SalesOrder };
      request.TenantId = session.TenantId;
      await _syncServiceDS.PullERPDataAsync(request, cancellationToken);

    }

    public async Task UpdateSalesOrderWithItem(CustBASalesOrderUpdateDTO updateDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      //SalesOrder 
      UserSession session = _userSessionManager.GetSession();

      // Map entity to order 
      await _bASODS.UpdateSalesOrderAsyncForCust(updateDTO, session.TenantId, session.TenantUserId);
    }

    #endregion Add/Update Methods
  }
}
