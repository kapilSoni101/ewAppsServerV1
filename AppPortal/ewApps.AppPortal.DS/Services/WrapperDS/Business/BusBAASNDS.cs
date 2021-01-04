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
    public class BusBAASNDS:IBusBAASNDS {

        private IUserSessionManager _userSessionManager;
        private AppPortalAppSettings _appPortalAppSettings;
        private INotesDS _notesDS;

        #region Constructor


        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAASNDS"/> class.
        /// </summary>
        public BusBAASNDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettingOptions, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appSettingOptions.Value;
            _notesDS = notesDS;
        }

        #endregion

        #region Get Methods

        /// <inhritdoc/>
        public async Task<IEnumerable<BusBAASNDTO>> GetASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("busbaasn/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<BusBAASNDTO>>(requestOptions, false);
        }

        /// <inhritdoc/>
        public async Task<BusBAASNViewDTO> GetASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("busbaasn/view/{0}", asnId);
            UserSession session = _userSessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            BusBAASNViewDTO dto = await httpRequestProcessor.ExecuteAsync<BusBAASNViewDTO>(requestOptions, false);

            // Get notes list  
            if(dto != null)
                dto.NotesList = await _notesDS.GetNotesViewListByEntityId(asnId, session.TenantId);

            return dto;
        }

        #endregion

        #region Vendor Methods

        /// <inhritdoc/>
        public async Task<IEnumerable<BusBAASNDTO>> GetVendorASNListByBusinessTenantIdAsync(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("busbaasn/vendor/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<BusBAASNDTO>>(requestOptions, false);
        }

        /// <inhritdoc/>
        public async Task<BusBAASNViewDTO> GetVendorASNDetailByASNIdAsync(Guid asnId, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("busbaasn/vendor/view/{0}", asnId);
            UserSession session = _userSessionManager.GetSession();
            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            BusBAASNViewDTO dto = await httpRequestProcessor.ExecuteAsync<BusBAASNViewDTO>(requestOptions, false);

            // Get notes list  
            if(dto != null)
                dto.NotesList = await _notesDS.GetNotesViewListByEntityId(asnId, session.TenantId);

            return dto;
        }


        #endregion
    }
}
