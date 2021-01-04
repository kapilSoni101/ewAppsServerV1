/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: 
 * Last Updated On: 18 September 2019
 */
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.ServiceProcessor;
using ewApps.Payment.Common;
using ewApps.Payment.DTO;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {

    /// <summary>
    /// A singleton class to support credit card api.
    /// Creadit card helper class to make payment, Get credit card api key info etc.
    /// </summary>
    public class CreditCardAccountDS:ICreditCardAccountDS {

        #region Local variables

        PaymentAppSettings _appSetting;
        private string transectionKey = null;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initlize local variable.
        /// </summary>
        /// <param name="appSetting"></param>
        public CreditCardAccountDS(IOptions<PaymentAppSettings> appSetting) {
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get credit card transection key.
        /// </summary>
        /// <param name="clientsessionid">session clientid</param>
        /// <param name="token"></param>
        /// <returns>return transectionKey</returns>
        public async Task<string> GetTSysTransectionKeyAsync(string clientsessionid, CancellationToken token = default(CancellationToken)) {
            if(!string.IsNullOrEmpty(transectionKey)) {
                return transectionKey;
            }
            // CreditCardGenerateTokenDTO
            CreditCardGenerateTokenDTO model = new CreditCardGenerateTokenDTO();
            model.mid = _appSetting.CreditCardAPIMID;
            model.userID = _appSetting.CreditCardAPIUserID;
            model.password = _appSetting.CreditCardAPIPassword;
            string Method = "authentication/key";
            List<KeyValuePair<string, string>> listHeader = new List<KeyValuePair<string, string>>();           
            KeyValuePair<string, string> hdr = new KeyValuePair<string, string>("clientsessionid", clientsessionid);
            listHeader.Add(hdr);
            RequestOptions requestOptions = RequestOptions.CreateInstance(ServiceProtocolTypeEnum.REST, RequestTypeEnum.Post, AcceptMediaTypeEnum.JSON, Method, model, _appSetting.AppName, _appSetting.IdentityServerUrl, listHeader);
            ServiceExecutor httpRequestProcessor = new ServiceExecutor(_appSetting.CreditCardApiUrl);
            transectionKey = await httpRequestProcessor.ExecuteAsync<string>(requestOptions, false);
            return transectionKey;
        }

        #endregion Get

    }
}
