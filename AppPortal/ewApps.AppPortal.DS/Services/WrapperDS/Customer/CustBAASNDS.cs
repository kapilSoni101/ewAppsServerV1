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

namespace ewApps.AppPortal.DS
{
  public class CustBAASNDS : ICustBAASNDS
  {

    private IUserSessionManager _userSessionManager;
    private AppPortalAppSettings _appPortalAppSettings;
    private INotesDS _notesDS;

    /// <summary>
    /// Initializes a new instance of the <see cref="CustBAASNDS"/> class.
    /// </summary>
    public CustBAASNDS(IUserSessionManager userSessionManager, IOptions<AppPortalAppSettings> appSettingOptions, INotesDS notesDS)
    {
      _userSessionManager = userSessionManager;
      _appPortalAppSettings = appSettingOptions.Value;
      _notesDS = notesDS;
    }

    public  async Task<CustBAASNViewDTO> GetASNDetailByASNIdAsyncForCust(Guid asnId, CancellationToken cancellationToken = default(CancellationToken))
    {
      UserSession session = _userSessionManager.GetSession();
      string url = string.Format("custbaasn/view/{0}", asnId);

      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, url, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
      CustBAASNViewDTO dto = await httpRequestProcessor.ExecuteAsync<CustBAASNViewDTO>(requestOptions, false);

      // Get notes list  
      if (dto != null)
        dto.NotesList = await _notesDS.GetNotesViewListByEntityId(asnId, session.TenantId);

      return dto;
    }

    public async Task<IEnumerable<CustBAASNDTO>> GetASNListByBusinessTenantIdAsyncForCust(Guid businessPartnerTenantId, ListDateFilterDTO listDateFilterDTO, CancellationToken cancellationToken = default(CancellationToken))
    {
      string url = string.Format("custbaasn/list/{0}", businessPartnerTenantId);

      List<KeyValuePair<string, string>> headerParameterList = new List<KeyValuePair<string, string>>();
      headerParameterList.Add(new KeyValuePair<string, string>("clientsessionid", _userSessionManager.GetSession().ID.ToString()));

      RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, url, listDateFilterDTO, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParameterList);

      ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appPortalAppSettings.BusinessEntityApiUrl);
      return await httpRequestProcessor.ExecuteAsync<IEnumerable<CustBAASNDTO>>(requestOptions, false);
    }
  }
}
