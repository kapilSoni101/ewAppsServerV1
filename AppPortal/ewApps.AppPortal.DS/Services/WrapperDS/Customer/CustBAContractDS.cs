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
    /// This class defines contract entity operations for business tenant.
    /// </summary>
    /// <seealso cref="ewApps.AppPortal.DS.IBusBAContractDS" />
    public class CustBAContractDS:ICustBAContractDS {

        private IUserSessionManager _userSessionManager;
        private AppPortalAppSettings _appPortalAppSettings;
        private INotesDS _notesDS;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusBAContractDS"/> class.
        /// </summary>
    public CustBAContractDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettingOptions, INotesDS notesDS) {
            _userSessionManager = userSessionManager;
            _appPortalAppSettings = appSettingOptions.Value;
          _notesDS = notesDS;
        }

        /// <inheritdoc/>
        public async Task<CustBAContractViewDTO> GetContractDetailByContractIdAsyncForCust(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("custbacontract/view/{0}/{1}", businessTenantId, contractId);
      UserSession session = _userSessionManager.GetSession();
      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            CustBAContractViewDTO dto = await httpRequestProcessor.ExecuteAsync<CustBAContractViewDTO>(requestOptions, false);

      // Get notes list  
      if (dto != null)
        dto.NotesList = await _notesDS.GetNotesViewListByEntityId(contractId, session.TenantId);

      return dto;
    }

        /// <inheritdoc/>
        public async Task<IEnumerable<CustBAContractDTO>> GetContractListByBusinessIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
            string url = string.Format("custbacontract/list/{0}", businessTenantId);

            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<CustBAContractDTO>>(requestOptions, false);
        }
    }
}
