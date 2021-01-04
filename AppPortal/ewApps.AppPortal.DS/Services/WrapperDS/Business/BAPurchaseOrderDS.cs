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
    /// This class defines business purchase order entity operations for business tenant.
    /// </summary>
    /// <seealso cref="ewApps.AppPortal.DS.IBAPurchaseOrderDS" />
    public class BAPurchaseOrderDS:IBAPurchaseOrderDS {

        #region Local Members

        private AppPortalAppSettings _appPortalAppSettings;
        private IUserSessionManager _userSessionManager;
        private INotesDS _notesDS;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BAPurchaseOrderDS"/> class.
        /// </summary>
        /// <param name="userSessionManager">The user session manager.</param>
        /// <param name="options">The app setting options.</param>
        public BAPurchaseOrderDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> options, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = options.Value;
            _notesDS = notesDS;
        }

        #endregion

        #region Business Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BAPurchaseOrderDTO>> GetPurchaseOrderListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("BAPurchaseOrder/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<BAPurchaseOrderDTO>>(requestOptions, false);
        }

        /// <inheritdoc/>
        public async Task<BAPurchaseOrderViewDTO> GetPurchaseOrderDetailByPOIdAsync(Guid businessTenantId, Guid soId, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession userSession = _userSessionManager.GetSession();
            string url = string.Format("BAPurchaseOrder/view/{0}/{1}", businessTenantId, soId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

            BAPurchaseOrderViewDTO salesOrderDTO = await httpRequestProcessor.ExecuteAsync<BAPurchaseOrderViewDTO>(requestOptions, false);

            // Get notes list  
            if(salesOrderDTO != null)
                salesOrderDTO.NotesList = await _notesDS.GetNotesViewListByEntityId(soId, userSession.TenantId);

            return salesOrderDTO;
        }

        #endregion
    }
}
