//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using ewApps.AppPortal.Common;
//using ewApps.AppPortal.DTO;
//using ewApps.Core.ServiceProcessor;
//using ewApps.Core.UserSessionService;
//using Microsoft.Extensions.Options;

//namespace ewApps.AppPortal.DS {
//    public class VendorBAContractDS:IVendorBAContractDS {

//        private IUserSessionManager _userSessionManager;
//        private AppPortalAppSettings _appPortalAppSettings;
//        private INotesDS _notesDS;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="VendorBAContractDS"/> class.
//        /// </summary>
//        public VendorBAContractDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettingOptions, INotesDS notesDS) {
//            _userSessionManager = userSessionManager;
//            _appPortalAppSettings = appSettingOptions.Value;
//            _notesDS = notesDS;
//        }

//        /// <inheritdoc/>
//        public async Task<VendorBAContractViewDTO> GetContractDetailByContractIdForVendorAsync(Guid businessTenantId, Guid contractId, CancellationToken cancellationToken = default(CancellationToken)) {
//            string url = string.Format("vendorbacontract/view/{0}/{1}", businessTenantId, contractId);
//            UserSession session = _userSessionManager.GetSession();
//            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
//            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

//            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

//            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
//            VendorBAContractViewDTO dto = await httpRequestProcessor.ExecuteAsync<VendorBAContractViewDTO>(requestOptions, false);

//            // Get notes list  
//            if(dto != null)
//                dto.NotesList = await _notesDS.GetNotesViewListByEntityId(contractId, session.TenantId);

//            return dto;
//        }

//        /// <inheritdoc/>
//        public async Task<IEnumerable<VendorBAContractDTO>> GetContractListByBusinessIdAsyncForCust(Guid businessTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken)) {
//            string url = string.Format("vendorbacontract/list/{0}", businessTenantId);

//            List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
//            headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

//            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

//            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
//            return await httpRequestProcessor.ExecuteAsync<IEnumerable<VendorBAContractDTO>>(requestOptions, false);
//        }

//    }
//}
