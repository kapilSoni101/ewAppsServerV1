/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 10 February 2019
 * 
 * Contributor/s: Hari Dudani.
* Last Updated On: 10 February 2019
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.ExceptionService;
using Microsoft.EntityFrameworkCore;

namespace ewApps.Core.AppDeeplinkService {

    /// <summary>
    /// AppDeeplinkManager class, for generating the deep link.
    /// It is a singleton class. It provides support for:
    /// (1) Generating the deep link.
    /// (2) Manage to keep the clicked deeplink log..  
    /// </summary>
    public class AppDeeplinkManager:IAppDeeplinkManager {

        #region Constructor/Properties

        AppDeeplinkDBContext _appDeeplinkDBContext;

        /// <summary>
        /// Constructor with a db context object. 
        /// </summary>
        /// <param name="appDeeplinkDBContext"></param>
        public AppDeeplinkManager(AppDeeplinkDBContext appDeeplinkDBContext) {
            _appDeeplinkDBContext = appDeeplinkDBContext;
        }

        #endregion Constructor/Properties

        #region Public Methods

        /// <summary>
        /// Generate the deeplink with all necessary payload information.
        /// Url query endparam contains all necessary information(payload) to access the page and required data.
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO">Contains the information of url to generate.</param>
        /// <param name="useShortUrl">whether generated short number should be convert into base62.</param>
        /// <returns></returns>
        public async Task<string> GeneratingDeeplink(AppDeeplinkPayloadDTO appDeeplinkPayloadDTO, bool useShortUrl, CancellationToken token = default(CancellationToken)) {
            // Generating rendom number.
            long number = AppDeeplinkNumberIdGenerator.GenerateRendomNumber();
            // Checking for duplicacy of a number. If exist the regenrate.
            if(await _appDeeplinkDBContext.IsNumberExistAsync(number, token)) {
                number = AppDeeplinkNumberIdGenerator.GenerateRendomNumber();
            }
            string url = number.ToString();
            if(useShortUrl) {
                // Generating shorturl. converting into base62.
                string shortUrl = AppDeeplinkShortUrlKeyGenerator.NumberToShortURL(number);
                // Save the deeplink.
                await Add(appDeeplinkPayloadDTO, number, shortUrl, token);
                url = shortUrl;
            }
            else {
                // Save the deeplink.
                await Add(appDeeplinkPayloadDTO, number, number.ToString(), token);
            }

            return url;
        }

        #endregion Public Methods

        #region Data Methods

        /// <summary>
        /// Map model object into AppDeepLink entity and create deeplink entry in database. 
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO"></param>
        /// <param name="number"></param>
        /// <param name="shortUrl"></param>
        /// <param name="token"></param>
        private async Task Add(AppDeeplinkPayloadDTO appDeeplinkPayloadDTO, long number, string shortUrl, CancellationToken token = default(CancellationToken)) {
            AppDeeplink apppDeepLink = mapProperties(appDeeplinkPayloadDTO, number, shortUrl, true);
            await _appDeeplinkDBContext.AddAsync(apppDeepLink, token);
            _appDeeplinkDBContext.SaveChanges();
        }

        /// <summary>
        /// Get deep link detail from short url.
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        public async Task<AppDeeplinkPayloadDTO> GetDeeplinkAccessAsync(string shortUrl, string accessMachineIpAddress, CancellationToken token = default(CancellationToken)) {
            AppDeeplinkPayloadDTO appDeeplinkPayloadDTO = await _appDeeplinkDBContext.GetDeeplinkAsync(shortUrl, token);
            if(appDeeplinkPayloadDTO.UserAccessCount >= appDeeplinkPayloadDTO.MaxUseCount) {
                // map propeties for making entry in in access log.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "Number of access increases", false, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
                // throw max access and add entry in access log. 
                // Raising security exception.
                throw GenerateSecurityException("Number of access increases.");
            }
            else if(appDeeplinkPayloadDTO.ExpirationDate != null && appDeeplinkPayloadDTO.ExpirationDate.Value < DateTime.UtcNow) {
                // Generating the access log with a exception, Deeplink expired.
                // map propeties for making entry in in access log.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "Deeplink expire.", false, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
                // throw max access and add entry in access log. 
                // Raising security exception.
                throw GenerateSecurityException("Quick Pay URL has been expired.");
            }
            else {
                // map properties for accesslog.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "", true, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
            }

            return appDeeplinkPayloadDTO;
        }

        /// <summary>
        /// Get deep link detail from short url.
        /// </summary>
        /// <param name="number">Unique generated number for a url.</param>
        /// <param name="accessMachineIpAddress">From which machine url is accessing.</param>
        /// <returns>Get dto object from unique number.</returns>
        public async Task<AppDeeplinkPayloadDTO> GetDeeplinkAccessByNumberAsync(long number, string accessMachineIpAddress, CancellationToken token = default(CancellationToken)) {
            AppDeeplinkPayloadDTO appDeeplinkPayloadDTO = await _appDeeplinkDBContext.GetDeeplinkByNumberAsync(number, token);
            if(appDeeplinkPayloadDTO.UserAccessCount >= appDeeplinkPayloadDTO.MaxUseCount) {
                // Generating the access log with a exception, Number of access a urlincrease.
                // map propeties for making entry in in access log.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "Number of access increases", false, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
                // throw max access and add entry in access log. 
                // Raising security exception.
                throw GenerateSecurityException("Number of access increases.");
            }
            else if(appDeeplinkPayloadDTO.ExpirationDate != null && appDeeplinkPayloadDTO.ExpirationDate.Value < DateTime.UtcNow) {
                // Generating the access log with a exception, Deeplink expirt.
                // map propeties for making entry in in access log.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "Deeplink expire.", false, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
                // throw max access and add entry in access log. 
                // Raising security exception.
                throw GenerateSecurityException("Deeplink expire.");
            }
            else {
                // map properties for accesslog.
                AppDeeplinkAccessLog accessLog = mapPropertiesInAcesssLog(appDeeplinkPayloadDTO.ID, "", true, accessMachineIpAddress);
                // Add entry in access log.
                await AddDeeplinkAccessLog(accessLog, appDeeplinkPayloadDTO.UserAccessCount + 1, token);
            }

            return appDeeplinkPayloadDTO;
        }

        /// <summary>
        /// Gets the AppDeeplink based on input parameters.
        /// </summary>
        /// <param name="appDeeplinkId"></param>
        /// <param name="token"></param>
        /// <returns>Returns AppDeeplink entity based on input SQL and parameters.</returns>
        public async Task<AppDeeplink> GetAppDeeplink(Guid appDeeplinkId, CancellationToken token = default(CancellationToken)) {
            AppDeeplink link = await _appDeeplinkDBContext.AppDeeplink.Where(appLink => appLink.ID == appDeeplinkId).SingleOrDefaultAsync();
            return link;
        }

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="shortUrl">Unique short url key</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ExpireDeepLink(string shortUrl, CancellationToken token = default(CancellationToken)) {
            await _appDeeplinkDBContext.ExpireDeepLink(shortUrl, token);
        }

        /// <summary>
        /// Method will expire the deeplink.
        /// </summary>
        /// <param name="tenantId">Tenant Id.</param>
        /// <param name="entityId">Action data enitytid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ExpireDeepLink(Guid tenantId, Guid entityId, CancellationToken token = default(CancellationToken)) {
            await _appDeeplinkDBContext.ExpireDeepLink(tenantId, entityId, token);
        }


        #endregion Data Methods

        #region Deeplink Access Log

        /// <summary>
        /// Add access log in database and update appdeeplink object by number of time access the url.
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO"></param>
        /// <param name="number"></param>
        /// <param name="shortUrl"></param>
        private async Task AddDeeplinkAccessLog(AppDeeplinkAccessLog apppDeepLinkAccessLog, int userAccessCount, CancellationToken token = default(CancellationToken)) {
            await _appDeeplinkDBContext.AddAsync(apppDeepLinkAccessLog, token);
            AppDeeplink appDeeplink = await GetAppDeeplink(apppDeepLinkAccessLog.AppDeeplinkId, token);
            appDeeplink.UserAccessCount = userAccessCount;
            _appDeeplinkDBContext.Update(appDeeplink);
            _appDeeplinkDBContext.SaveChanges();
        }

        /// <summary>
        /// Gets the AppDeeplinkAccessLog entity list based on input SQL and parameters.
        /// </summary>
        /// <param name="appDeeplinkId"></param>
        /// <param name="token"></param>
        /// <returns>Returns AppDeeplinkAccessLog entity list based on input SQL and parameters.</returns>
        public async Task<List<AppDeeplinkAccessLog>> GetAppDeeplinkAccessLogEntityList(Guid appDeeplinkId, CancellationToken token = default(CancellationToken)) {
            //string sql = "SELECT * FROM AppDeeplinkAccessLog WHERE AppDeeplinkId=@AppDeeplinkId";
            //SqlParameter parameters = new SqlParameter("@AppDeeplinkId", appDeeplinkId);
            //return await _appDeeplinkDBContext.AppDeeplinkAccessLog.FromSql(sql, parameters).ToListAsync(token);
            return await _appDeeplinkDBContext.AppDeeplinkAccessLog.Where(appLink => appLink.AppDeeplinkId == appDeeplinkId).ToListAsync();
        }

        #endregion Deeplink Access Log

        #region Support

        /// <summary>
        /// Map the deeplink generator model to deeplink entity.
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO">Map DTO object into AppDeepLink entity.</param>
        /// <param name="number">Generated unique number for url.</param>
        /// <param name="shortUrl">Generated unique number shorturl.</param>
        /// <param name="isAdd">whether item is adding or updating.</param>
        /// <returns>return AppDeeplink Entity object.</returns>
        private AppDeeplink mapProperties(AppDeeplinkPayloadDTO appDeeplinkPayloadDTO, long number, string shortUrl, bool isAdd) {
            AppDeeplink apppDeepLink = new AppDeeplink();
            apppDeepLink.ActionData = appDeeplinkPayloadDTO.ActionData;
            apppDeepLink.ActionName = appDeeplinkPayloadDTO.ActionName;
            apppDeepLink.MaxUseCount = appDeeplinkPayloadDTO.MaxUseCount;
            apppDeepLink.ExpirationDate = appDeeplinkPayloadDTO.ExpirationDate;
            apppDeepLink.Password = appDeeplinkPayloadDTO.Password;
            apppDeepLink.ShortUrlKey = shortUrl;
            apppDeepLink.NumberId = number;
            apppDeepLink.UserId = appDeeplinkPayloadDTO.UserId;
            apppDeepLink.TenantId = appDeeplinkPayloadDTO.TenantId;
            if(isAdd) {
                apppDeepLink.UserAccessCount = 0;
                apppDeepLink.ID = Guid.NewGuid();
            }

            return apppDeepLink;
        }

        /// <summary>
        /// Map the deeplink generator model to deeplink entity.
        /// </summary>
        /// <param name="appDeeplinkPayloadDTO"></param>
        /// <param name="number"></param>
        /// <param name="shortUrl"></param>
        /// <param name="isAdd"></param>
        /// <returns></returns>
        private AppDeeplinkAccessLog mapPropertiesInAcesssLog(Guid appDeeplinkId, string accessError, bool accessGrant, string accessorIPAddress) {
            AppDeeplinkAccessLog apppDeepLink = new AppDeeplinkAccessLog();
            apppDeepLink.AccessorIPAddress = accessorIPAddress;
            apppDeepLink.AccessTimestamp = DateTime.UtcNow;
            apppDeepLink.AppDeeplinkId = appDeeplinkId;
            apppDeepLink.AccessNotGrantedReason = accessError;
            apppDeepLink.AccessGranted = accessGrant;

            return apppDeepLink;
        }

        /// <summary>
        /// Generating exception of a max instance limit reached to access a url.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private EwpValidationException GenerateSecurityException(string message) {
            EwpError error = new EwpError();
            error.ErrorType = ErrorType.Validation;
            EwpErrorData errorData = new EwpErrorData();
            errorData.ErrorSubType = (int)ValidationErrorSubType.MaxInstanceLimit;
            errorData.Message = message;
            error.EwpErrorDataList.Add(errorData);
            EwpValidationException exc = new EwpValidationException(message, error.EwpErrorDataList);

            return exc;
        }

        #endregion Support
    }
}
