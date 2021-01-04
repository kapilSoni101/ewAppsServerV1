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

    public class CustBASalesQuotationDS : ICustBASalesQuotationDS {

        #region Local Members

        private AppPortalAppSettings _appPortalAppSettings;
        private IUserSessionManager _userSessionManager;
    private INotesDS _notesDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="CustBASalesQuotationDS"/> class.
    /// </summary>
    /// <param name="userSessionManager">The user session manager.</param>
    /// <param name="options">The app setting options.</param>
        
    public CustBASalesQuotationDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> options, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = options.Value;
            _notesDS = notesDS;
        }

        #endregion

        #region Business Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBASalesQuotationDTO>> GetSalesQuotationListByPartnerTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            string url = string.Format("Custbasalesquotation/list/{0}", businessPartnerTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

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
      UserSession userSession = _userSessionManager.GetSession();

            string url = string.Format("Custbasalesquotation/{0}", salesQuotationId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
      //            return 
      CustBASalesQuotationViewDTO dto = await httpRequestProcessor.ExecuteAsync<CustBASalesQuotationViewDTO>(requestOptions, false);

      // Get notes list  
      if(dto != null)
        dto.NotesList = await _notesDS.GetNotesViewListByEntityId(salesQuotationId, userSession.TenantId);

      return dto;
    }
    /// <inheritdoc/>
    public async Task AddSalesQuotationWithItemAsync(CustBASalesQuotationAddDTO dto, CancellationToken cancellationToken = default(CancellationToken))
    {
      UserSession userSession = _userSessionManager.GetSession();

      string url = string.Format("custbasalesquotation/salesquotationwithitem");

      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, url, dto, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

      await httpRequestProcessor.ExecuteAsync<CustBASalesOrderAddDTO>(requestOptions, false);
    }
    #endregion
  }
}
