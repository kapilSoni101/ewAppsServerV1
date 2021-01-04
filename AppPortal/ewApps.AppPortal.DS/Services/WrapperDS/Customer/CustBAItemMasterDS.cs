using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    public class CustBAItemMasterDS:ICustBAItemMasterDS {


        #region Local Members

        AppPortalAppSettings _appSettings;
        IUserSessionManager _userSessionManager;

        #endregion Local Members 

        #region Constructor

        public CustBAItemMasterDS(IOptions<AppPortalAppSettings> appSettings, IUserSessionManager userSessionManager) {
            _appSettings = appSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Get

        public async Task<IEnumerable<CustBAItemMasterDTO>> GetItemMasterListByBusTenantIdAsyncForCust(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "custbaitemmaster/list/" + tenantId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            IEnumerable<CustBAItemMasterDTO> itemMasterList = await httpRequestProcessor.ExecuteAsync<IEnumerable<CustBAItemMasterDTO>>(requestOptions, false);

            for(int i = 0; i < itemMasterList.Count(); i++) {
                //itemMasterList.ElementAt(i).Size = itemMasterList.ElementAt(i).SalesLength + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + itemMasterList.ElementAt(i).SalesWidth + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + itemMasterList.ElementAt(i).SalesHeight + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                //itemMasterList.ElementAt(i).Size = Math.Truncate(itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + Math.Truncate(itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
                itemMasterList.ElementAt(i).Size = String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesLength) + " " + itemMasterList.ElementAt(i).SalesLengthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesWidth) + " " + itemMasterList.ElementAt(i).SalesWidthUnitText + " x " + String.Format("{0:0.00}", itemMasterList.ElementAt(i).SalesHeight) + " " + itemMasterList.ElementAt(i).SalesHeightUnitText;
            }

            return itemMasterList;
        }


        public async Task<CustBAItemMasterViewDTO> GetItemMasterByBusTenantIdAndItemidAsyncForCust(Guid tenantId, Guid itemId, CancellationToken token = default(CancellationToken)) {
            string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "custbaitemmaster/view/" + tenantId + "/" + itemId;

            UserSession session = _userSessionManager.GetSession();

            List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
            headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                     RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, reqeustMethod, null, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
            CustBAItemMasterViewDTO itemMaster = await httpRequestProcessor.ExecuteAsync<CustBAItemMasterViewDTO>(requestOptions, false);

            return itemMaster;
        }

        public async Task<BASyncItemPriceDTO> PullItemPriceAsync(PullItemPriceReqDTO request, CancellationToken token = default(CancellationToken))
        {
          string baseUrl = _appSettings.BusinessEntityApiUrl;
            string reqeustMethod = "BASync/itemprice";

          UserSession session = _userSessionManager.GetSession();

          List<KeyValuePair<string, string>> headerParameters = new List<KeyValuePair<string, string>>();
          headerParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

          RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST,
                                   RequestTypeEnum.Put, AcceptMediaTypeEnum.JSON, reqeustMethod, request, _appSettings.AppName, _appSettings.IdentityServerUrl, headerParameters);
          ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseUrl);
          BASyncItemPriceDTO itemMaster = await httpRequestProcessor.ExecuteAsync<BASyncItemPriceDTO>(requestOptions, false);

          return itemMaster;
        }

    #endregion Get
  }
}
