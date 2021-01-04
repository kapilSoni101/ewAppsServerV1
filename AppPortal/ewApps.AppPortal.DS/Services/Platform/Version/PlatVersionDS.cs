/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    //This class Implement Business Logic of Platform Version  
    public class PlatVersionDS :IPlatVersionDS {

        #region Local Variable 
        AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Consturctor
        /// <summary>
        /// Initialize Local Variable 
        /// </summary>
        /// <param name="appPortalAppSettings"></param>
        public PlatVersionDS(IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager) {
            _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
        } 
        #endregion

        #region Server Version
        public async Task<ServerVersionDTO> ApplicationVersionAsync() {
            UserSession session = _userSessionManager.GetSession();
            ServerVersionDTO serverVersionDTO = new ServerVersionDTO();
            serverVersionDTO.ServerVersionNo = GetType().Assembly.GetName().Version.ToString();
            serverVersionDTO.PayConnectorVersionDTO = await GetPaymentConnectorVersion(session.ID);
            //serverVersionDTO.ShipConnectorVersionDTO = await GetShipmentConnectorVersion(session.ID);
            serverVersionDTO.IdentityServerVersionDTO = await GetIdentityServerVersion(session.ID);
            return serverVersionDTO;
        }
        #endregion


        //Get Payment Connector Version No 
        private async Task<PayConnectorVersionDTO> GetPaymentConnectorVersion(Guid ID) {

            PayConnectorVersionDTO payConnectorVersionDTO = new PayConnectorVersionDTO();

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "PayConnectorVersion/getApplicationVersion";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.PaymentConnectorApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            payConnectorVersionDTO = await httpRequestProcessor.ExecuteAsync<PayConnectorVersionDTO>(requestOptions, false);
            #endregion

            return payConnectorVersionDTO;
        }

        //Get Shipment Connector Version No 
        private async Task<ShipConnectorVersionDTO> GetShipmentConnectorVersion(Guid ID) {

            ShipConnectorVersionDTO shipConnectorVersionDTO = new ShipConnectorVersionDTO();

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "ShipConnectorVersion/getApplicationVersion";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.ShipmentConnectorApiUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            shipConnectorVersionDTO = await httpRequestProcessor.ExecuteAsync<ShipConnectorVersionDTO>(requestOptions, false);
            #endregion

            return shipConnectorVersionDTO;
        }

        //Get identity Server Version No 
        private async Task<IdentityServerVersionDTO> GetIdentityServerVersion(Guid ID) {

            IdentityServerVersionDTO identityServerVersionDTO = new IdentityServerVersionDTO();

            // Preparing api calling process model.
            #region Get Tenantlinking Information
            string methodUri = "user/ApplicationVersionAsync";
            List<KeyValuePair<string, string>> headerParams = new List<KeyValuePair<string, string>>();
            headerParams.Add(new KeyValuePair<string, string>("clientsessionid", ID.ToString()));
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Get, AcceptMediaTypeEnum.JSON, methodUri, null, _appPortalAppSettings.AppName, _appPortalAppSettings.IdentityServerUrl, headerParams);
            string baseuri = _appPortalAppSettings.IdentityServerUrl;
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            identityServerVersionDTO = await httpRequestProcessor.ExecuteAsync<IdentityServerVersionDTO>(requestOptions, false);
            #endregion

            return identityServerVersionDTO;
        }
    }
}
