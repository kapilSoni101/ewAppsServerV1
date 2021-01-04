using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {
    /// <summary>
    /// This class defines business sales order entity operations for business tenant.
    /// </summary>
    /// <seealso cref="ewApps.AppPortal.DS.IBusBASalesOrderDS" />
    public class CustBASalesOrderDS: ICustBASalesOrderDS
  {

        #region Local Members

        private AppPortalAppSettings _appPortalAppSettings;
        private IUserSessionManager _userSessionManager;
        private INotesDS _notesDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BusBASalesOrderDS"/> class.
    /// </summary>
    /// <param name="userSessionManager">The user session manager.</param>
    /// <param name="options">The app setting options.</param>
    public CustBASalesOrderDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> options, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _notesDS = notesDS;
            _appPortalAppSettings = options.Value;
        }

        #endregion

        #region Business Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByBusinessTenantIdAsyncForCust(Guid businessTenantId, CancellationToken cancellationToken) {
            string url = string.Format("custbasalesquotation/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<CustBASalesQuotationDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets the SalesQuotation  by SalesQuotation id.
        /// </summary>
        /// <param name="salesQuotationId">The delivery to find SalesQuotation items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of SalesQuotation item name list that matches provided SalesQuotation id.</returns>
        public async Task<CustBASalesQuotationViewDTO> GetSalesQuotationDetailBySalesQuotationIdAsyncForCust(Guid salesQuotationId, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("custbasalesquotation/{0}", salesQuotationId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<CustBASalesQuotationViewDTO>(requestOptions, false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesOrderDTO>> GetSalesOrderListByBusinessTenantIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("custbasalesorder/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<CustBASalesOrderDTO>>(requestOptions, false);
        }

        /// <inheritdoc/>
        public async Task<CustBASalesOrderViewDTO> GetSalesOrderDetailBySOIdAsyncForCust(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();

            string url = string.Format("custbasalesorder/view/{0}/{1}", businessTenantId, soId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

            CustBASalesOrderViewDTO salesOrderDTO = await httpRequestProcessor.ExecuteAsync<CustBASalesOrderViewDTO>(requestOptions, false);

            // Get notes list
            if(salesOrderDTO != null)  
              salesOrderDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(soId, userSession.TenantId);

            return salesOrderDTO;
        }

    /// <inheritdoc/>
    public async Task AddSalesOrderWithItemAsync(CustBASalesOrderAddDTO dto, CancellationToken cancellationToken = default(CancellationToken))
    {
      UserSession userSession = _userSessionManager.GetSession();

      string url = string.Format("custbasalesorder/salesorderwithitem");

      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, url, dto, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

      await httpRequestProcessor.ExecuteAsync<CustBASalesOrderAddDTO>(requestOptions, false);
    }

    /// <inheritdoc/>
    public async Task UpdateSalesOrderWithItemAsync(CustBASalesOrderUpdateDTO dto, CancellationToken cancellationToken = default(CancellationToken)) {
      UserSession userSession = _userSessionManager.GetSession();

      string url = string.Format("custbasalesorder/salesorderwithitem");

      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, dto, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

      await httpRequestProcessor.ExecuteAsync<CustBASalesOrderUpdateDTO>(requestOptions, false);
    }

  #endregion

  }
}
