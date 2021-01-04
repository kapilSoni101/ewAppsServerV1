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
    public class BusBADeliveryDS:IBusBADeliveryDS {

        #region Local Members

        private AppPortalAppSettings _appPortalAppSettings;
        private IUserSessionManager _userSessionManager;
        private INotesDS _notesDS;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BusBADeliveryDS"/> class.
    /// </summary>
    /// <param name="userSessionManager">The user session manager.</param>
    /// <param name="options">The app setting options.</param>
    public BusBADeliveryDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> options, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = options.Value;
            _notesDS = notesDS;
        }

        #endregion

        #region Business Methods

        /// <inheritdoc/>
        public async Task<IEnumerable<BusBADeliveryDTO>> GetDeliveryListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken) {
            string url = string.Format("busbadelivery/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<BusBADeliveryDTO>>(requestOptions, false);
        }

        /// <summary>
        /// Gets the delivery item name list by delivery id.
        /// </summary>
        /// <param name="deliveryId">The delivery to find delivery items.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of delivery item name list that matches provided delivery id.</returns>
        public async Task<IEnumerable<string>> GetDeliveryItemNameListByDeliveryIdAsync(Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("busbadelivery/items/list/{0}", deliveryId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<string>>(requestOptions, false);
        }

        public async Task<BusBADeliveryViewDTO> GetDeliveryDetailByDeliveryIdAsync(Guid businessTenantId, Guid deliveryId, CancellationToken cancellationToken = default(CancellationToken)) {
      UserSession userSession = _userSessionManager.GetSession();
      string url = string.Format("busbadelivery/view/{0}/{1}", businessTenantId, deliveryId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);

      BusBADeliveryViewDTO dto = await httpRequestProcessor.ExecuteAsync<BusBADeliveryViewDTO>(requestOptions, false);

      // Get notes list  
      if (dto != null)
        dto.NotesList = await _notesDS.GetNotesViewListByEntityId(deliveryId, userSession.TenantId);

      return dto;

        }

        #endregion
    }
}
