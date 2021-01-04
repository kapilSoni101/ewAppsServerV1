/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// This class implements standard business logic and operations for Pu bSubscriptionDS.
    /// </summary>
    public class PubSubscriptionPlanDS:IPubSubscriptionPlanDS {

        #region Local Member

        AppPortalAppSettings _appPortalAppSettings;
        IUserSessionManager _userSessionManager;

        #endregion

        #region Constructor 

        /// <summary>
        /// Initializing local variables
        /// </summary>
        /// <param name="entityAccess">entity access class reference.</param>
        public PubSubscriptionPlanDS(IOptions<AppPortalAppSettings> appPortalAppSettings, IUserSessionManager userSessionManager) {
            _appPortalAppSettings = appPortalAppSettings.Value;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor        

        #region Get Methods

        /// <inheritdoc/>       
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByPubTenantIdAsync(bool planState, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "subscriptionplan/list/publisherplan/" + planState;

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            try {
                return await httpRequestProcessor.ExecuteAsync<List<SubscriptionPlanInfoDTO>>(requestOptions, false);
            }
            catch(Exception ex) {
                throw;
            }
        }

        ///<inheritdoc/>
        public async Task<SubscriptionPlanInfoDTO> GetPubSubscriptionPlaninfoByIdAsync(Guid planId, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = "subscriptionplan/detail/" + planId;

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            return await httpRequestProcessor.ExecuteAsync<SubscriptionPlanInfoDTO>(requestOptions, false);
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<string>> GetSubscriptionServiceNameListBySubscriptionPlanIdAsync(Guid subsPlanId, CancellationToken cancellationToken = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();

            // Preparing api calling process model.
            string baseuri = _appPortalAppSettings.AppMgmtApiUrl;
            string requesturl = string.Format("subscriptionplan/{0}/servicenames", subsPlanId);

            RequestOptions requestOptions = new RequestOptions();
            requestOptions.AcceptType = AcceptMediaTypeEnum.JSON;
            requestOptions.Method = requesturl;
            requestOptions.MethodData = null;
            requestOptions.ProtocolType = ServiceProtocolTypeEnum.REST;
            requestOptions.ServiceRequestType = RequestTypeEnum.Get;
            requestOptions.HeaderParameters.Add(new KeyValuePair<string, string>("clientsessionid", session.ID.ToString()));

            ServiceExecutor httpRequestProcessor = new ServiceExecutor(baseuri);
            return await httpRequestProcessor.ExecuteAsync<IEnumerable<string>>(requestOptions, false);
        }


        #endregion Get Methods

    }
}

